#include <Arduino.h>
#include <EEPROM.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>
#include <math.h>


struct ButtonEvent {
  bool shortPress;
  bool longPress;
};


LiquidCrystal_I2C lcd(0x27, 16, 2);


const byte PIN_SENSOR = 2;   // D2 = interrupt
const byte PIN_PWM    = 9;   // D9 = PWM output
const byte PIN_POT    = A2;  // A2 = setpoint pot
const byte PIN_BTN    = 4;   // КНОПКА на D4 (на GND, INPUT_PULLUP)


const uint16_t PULSES_PER_REV  = 1;
const uint32_t GLITCH_US       = 300;
const uint32_t STOP_TIMEOUT_US = 200000; // для readRPM()


const float RPM_MIN = 300.0f;
const float RPM_MAX = 2200.0f;

// ===== Коэффициенты ПИД =====
float Kp = 0.20f;
float Ki = 0.80f;
float Kd = 0.00f;


const uint32_t PID_PERIOD_MS    = 20;
const uint32_t SERIAL_PERIOD_MS = 100;
const uint32_t LCD_PERIOD_MS    = 200;


const uint32_t STARTUP_PULSE_TIMEOUT_MS = 1500;  // delta_t
const uint32_t RUN_PULSE_TIMEOUT_MS     = 700;   // delta_t2
const int      SP_WARN_MIN_RPM          = 450;   // минимальная скорость для аварии
const uint32_t WARNING_RESET_HOLD_MS    = 1500;  // delta_t3


volatile uint32_t lastEdgeUs = 0;
volatile uint32_t periodUs = 0;
volatile uint32_t lastGoodPulseUs = 0;
volatile uint32_t pulseCount = 0;

void isrPulse() {
  uint32_t now = micros();
  uint32_t p = now - lastEdgeUs;
  lastEdgeUs = now;

  if (p > GLITCH_US) {
    periodUs = p;
    lastGoodPulseUs = now;
    pulseCount++;
  }
}

static inline float clampf(float x, float lo, float hi) {
  if (x < lo) return lo;
  if (x > hi) return hi;
  return x;
}

static inline uint32_t lastPulseAgeMs() {
  uint32_t lastPulseUs;
  noInterrupts();
  lastPulseUs = lastGoodPulseUs;
  interrupts();

  if (lastPulseUs == 0) return 0xFFFFFFFFu;

  uint32_t nowUs = micros();
  uint32_t ageUs = (uint32_t)(nowUs - lastPulseUs);
  return ageUs / 1000u;
}

static inline float readRPM() {
  uint32_t per, lastPulse;
  noInterrupts();
  per = periodUs;
  lastPulse = lastGoodPulseUs;
  interrupts();

  uint32_t nowUs = micros();

  if (lastPulse == 0 || (uint32_t)(nowUs - lastPulse) > STOP_TIMEOUT_US) return 0.0f;
  if (per == 0) return 0.0f;

  return (1e6f / (float)per) * (60.0f / (float)PULSES_PER_REV);
}


static int sp_rpm_int = 0;
static int last_sp_rpm_int = 0;

static inline int readPotAdcAvg16() {
  uint32_t sum = 0;
  for (uint8_t i = 0; i < 16; i++) sum += analogRead(PIN_POT);
  return (int)(sum / 16);
}

static inline int computeSetpointRpm() {
  int adc = readPotAdcAvg16(); // 0..1023
  float sp = RPM_MIN + ((float)adc / 1023.0f) * (RPM_MAX - RPM_MIN);
  int sp_i = (int)(sp + 0.5f);

  const int deadRpm = 10;
  if (abs(sp_i - last_sp_rpm_int) <= deadRpm) return last_sp_rpm_int;

  last_sp_rpm_int = sp_i;
  return sp_i;
}

// ===== PID state =====
float integrator = 0.0f;
float prevMeas = 0.0f;
uint8_t lastPwm = 0;

// ================= Конфигурация ПЗУ =================
struct PidConfig {
  uint32_t magic;
  float kp;
  float ki;
  float kd;
};

const uint32_t PID_MAGIC = 0x50494431UL; // "PID1"
const int EEPROM_ADDR = 0;

static void sendConfig() {
  Serial.print("CFG,");
  Serial.print(Kp, 6); Serial.print(",");
  Serial.print(Ki, 6); Serial.print(",");
  Serial.println(Kd, 6);
}

static void loadPidFromEepromOrInit() {
  PidConfig cfg;
  EEPROM.get(EEPROM_ADDR, cfg);

  if (cfg.magic == PID_MAGIC &&
      isfinite(cfg.kp) && isfinite(cfg.ki) && isfinite(cfg.kd)) {
    Kp = cfg.kp;
    Ki = cfg.ki;
    Kd = cfg.kd;
  } else {
    cfg.magic = PID_MAGIC;
    cfg.kp = Kp; cfg.ki = Ki; cfg.kd = Kd;
    EEPROM.put(EEPROM_ADDR, cfg);
  }
}

static void savePidToEeprom() {
  PidConfig cfg;
  cfg.magic = PID_MAGIC;
  cfg.kp = Kp;
  cfg.ki = Ki;
  cfg.kd = Kd;
  EEPROM.put(EEPROM_ADDR, cfg);
}

void handleCommandLine(String line) {
  line.trim();
  if (line.length() == 0) return;

  if (line == "GET") {
    sendConfig();
    return;
  }

  if (line == "SAVE") {
    savePidToEeprom();
    Serial.println("ACK,SAVE");
    return;
  }

  int comma = line.indexOf(',');
  if (comma <= 0) return;

  String cmd = line.substring(0, comma);
  String val = line.substring(comma + 1);

  float x = val.toFloat();

  bool ok = true;
  if (cmd == "P") Kp = x;
  else if (cmd == "I") Ki = x;
  else if (cmd == "D") Kd = x;
  else ok = false;

  if (!ok) return;

  Serial.print("ACK,");
  Serial.print(cmd);
  Serial.print(",");
  if (cmd == "P") Serial.println(Kp, 6);
  else if (cmd == "I") Serial.println(Ki, 6);
  else Serial.println(Kd, 6);
}


enum RunState : uint8_t { ST_STOP = 0, ST_RUN = 1, ST_WARNING = 2 };
RunState state = ST_STOP;


bool warningShown = false;

static void stopMotor() {
  analogWrite(PIN_PWM, 0);
  lastPwm = 0;
  integrator = 0.0f;
  prevMeas = 0.0f;
}

static void enterWarning() {
  state = ST_WARNING;
  stopMotor();
  warningShown = false; // каждый вход в WARNING должен перерисовывать экран
}


static ButtonEvent readButtonEvents() {
  const uint32_t DEBOUNCE_MS = 30;

  static bool stableState = HIGH;
  static bool lastRaw = HIGH;
  static uint32_t lastChangeMs = 0;

  static uint32_t pressStartMs = 0;
  static bool longFired = false;

  ButtonEvent ev;
  ev.shortPress = false;
  ev.longPress  = false;

  bool raw = digitalRead(PIN_BTN);
  uint32_t now = millis();

  if (raw != lastRaw) {
    lastRaw = raw;
    lastChangeMs = now;
  }

  if ((now - lastChangeMs) >= DEBOUNCE_MS && raw != stableState) {
    stableState = raw;

    if (stableState == LOW) { // pressed
      pressStartMs = now;
      longFired = false;
    } else { // released
      if (!longFired) ev.shortPress = true;
    }
  }

  if (stableState == LOW && !longFired) {
    if ((now - pressStartMs) >= WARNING_RESET_HOLD_MS) {
      longFired = true;
      ev.longPress = true;
    }
  }

  return ev;
}


static void lcdPrint16(uint8_t col, uint8_t row, const char* s) {
  lcd.setCursor(col, row);
  for (uint8_t i = 0; i < 16; i++) {
    char c = s[i];
    if (c == '\0') c = ' ';
    lcd.print(c);
  }
}

static void lcdShowWarningOnce() {
  char l0[17], l1[17];
  snprintf(l0, sizeof(l0), "    WARNING!     ");
  snprintf(l1, sizeof(l1), "               ");
  lcdPrint16(0, 0, l0);
  lcdPrint16(0, 1, l1);
}


static void lcdInitScreen() {
  char l0[17], l1[17];
  snprintf(l0, sizeof(l0), "STOP SP:----");
  snprintf(l1, sizeof(l1), "PV:---- PWM:---");
  lcdPrint16(0, 0, l0);
  lcdPrint16(0, 1, l1);
}

void setup() {
  Serial.begin(115200);

  pinMode(PIN_SENSOR, INPUT_PULLUP);
  pinMode(PIN_PWM, OUTPUT);
  pinMode(PIN_BTN, INPUT_PULLUP);

  stopMotor();

  attachInterrupt(digitalPinToInterrupt(PIN_SENSOR), isrPulse, FALLING);

  loadPidFromEepromOrInit();
  sendConfig();

  lcd.init();
  lcd.backlight();
  lcdInitScreen();

  // Initialize SP once
  int adc = readPotAdcAvg16();
  float sp = RPM_MIN + ((float)adc / 1023.0f) * (RPM_MAX - RPM_MIN);
  sp_rpm_int = (int)(sp + 0.5f);
  last_sp_rpm_int = sp_rpm_int;

  state = ST_STOP;
}

void loop() {
  static uint32_t lastPidMs = 0;
  static uint32_t lastPrintMs = 0;
  static uint32_t lastLcdMs = 0;

  static uint32_t runStartMs = 0;
  static bool waitingStartupPulse = false;

  uint32_t nowMs = millis();


  if (Serial.available()) {
    String line = Serial.readStringUntil('\n');
    handleCommandLine(line);
  }


  sp_rpm_int = computeSetpointRpm();


  ButtonEvent bev = readButtonEvents();

  if (bev.shortPress) {
    if (state == ST_STOP) {
      // STOP -> RUN (attempt start)
      state = ST_RUN;
      runStartMs = nowMs;
      waitingStartupPulse = true;

      integrator = 0.0f;
      prevMeas = readRPM();
    }
    else if (state == ST_RUN) {
      // RUN -> STOP
      state = ST_STOP;
      waitingStartupPulse = false;
      stopMotor();
    }
    else {

    }
  }

  if (bev.longPress) {
    if (state == ST_WARNING) {
      // WARNING reset -> STOP
      state = ST_STOP;
      waitingStartupPulse = false;
      stopMotor();
    }
  }

  if (state == ST_RUN) {
    if (waitingStartupPulse) {
      uint32_t age = lastPulseAgeMs();
      bool havePulseRecently = (age != 0xFFFFFFFFu) && (age < STARTUP_PULSE_TIMEOUT_MS);

      if (havePulseRecently) {
        waitingStartupPulse = false;
      } else {
        if ((nowMs - runStartMs) >= STARTUP_PULSE_TIMEOUT_MS) {
          enterWarning();
        }
      }
    } else {
      if (sp_rpm_int >= SP_WARN_MIN_RPM) {
        uint32_t age = lastPulseAgeMs();
        bool pulseTooOld = (age == 0xFFFFFFFFu) || (age >= RUN_PULSE_TIMEOUT_MS);
        if (pulseTooOld) {
          enterWarning();
        }
      }
    }
  }

  // ----- Обновление ПИД -----
  if (state == ST_RUN && (nowMs - lastPidMs >= PID_PERIOD_MS)) {
    lastPidMs = nowMs;
    const float dt = PID_PERIOD_MS / 1000.0f;

    float sp = (float)sp_rpm_int;

    float y  = readRPM();
    float e  = sp - y;

    float dy = (y - prevMeas) / dt;
    prevMeas = y;

    float P = Kp * e;

    integrator += Ki * e * dt;
    integrator = clampf(integrator, -255.0f, 255.0f);

    float D = -Kd * dy;

    float u = P + integrator + D;
    u = clampf(u, 0.0f, 255.0f);

    lastPwm = (uint8_t)(u + 0.5f);
    analogWrite(PIN_PWM, lastPwm);
  } else if (state != ST_RUN) {

    analogWrite(PIN_PWM, 0);
    lastPwm = 0;
  }

  if (nowMs - lastLcdMs >= LCD_PERIOD_MS) {
    lastLcdMs = nowMs;

    if (state == ST_WARNING) {
      if (!warningShown) {
        lcdShowWarningOnce();
        warningShown = true;
      } else {

      }
    } else {
      warningShown = false; // готовимся к следующему WARNING

      char line0[17];
      char line1[17];

      int pv_i = (int)(readRPM() + 0.5f);
      int pwm_pct = (int)((lastPwm * 100UL + 127) / 255);

      if (state == ST_STOP) {
        snprintf(line0, sizeof(line0), "STOP SP:%4d", sp_rpm_int);
      } else {
        snprintf(line0, sizeof(line0), "RUN  SP:%4d", sp_rpm_int);
      }
      snprintf(line1, sizeof(line1), "PV:%4d PWM:%3d%%", pv_i, pwm_pct);

      lcdPrint16(0, 0, line0);
      lcdPrint16(0, 1, line1);
    }
  }

  // ----- Отправляем телеметрию в ПК -----
  if (nowMs - lastPrintMs >= SERIAL_PERIOD_MS) {
    lastPrintMs = nowMs;

    int sp_i = sp_rpm_int;
    int pv_i = (int)(readRPM() + 0.5f);
    int pwm_pct = (int)((lastPwm * 100UL + 127) / 255);

    Serial.print("T,");
    Serial.print(nowMs);
    Serial.print(",");
    Serial.print(sp_i);
    Serial.print(",");
    Serial.print(pv_i);
    Serial.print(",");
    Serial.println(pwm_pct);
  }
}