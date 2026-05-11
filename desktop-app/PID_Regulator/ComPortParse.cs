using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PID_Regulator
{
    public class ComPortParse
    {
        public struct TelemetryFrame
        {
            public uint TimeMs;
            public int SetpointRpm;
            public int MeasuredRpm;
            public int PwmPercent;

            public int ErrorRpm { get { return SetpointRpm - MeasuredRpm; } }
        }

        public struct PidConfig
        {
            public float Kp;
            public float Ki;
            public float Kd;
        }

        public struct AckFrame
        {
            public string What;
            public float? Value;
            
        }

        public event Action<TelemetryFrame> telemetryFrameReceived;
        public event Action<AckFrame> ackFrameReceived;
        public event Action<PidConfig> pidConfigReceived;

        public bool TryParseTelemetry(string line, out TelemetryFrame telemetryFrame)
        {
            telemetryFrame = new TelemetryFrame();

            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            line = line.Trim();
            string[] parts = line.Split(',');
            if (parts.Length != 5)
            {
                return false;
            }
            if (!parts[0].Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            uint t;
            int sp, pv, pwm;
            if (!uint.TryParse(parts[1], out t))
            {
                return false;
            }
            if (!int.TryParse(parts[2], out sp))
            { 
                return false;
            }

            if (!int.TryParse(parts[3], out pv))
            {
                return false;
            }
            if (!int.TryParse(parts[4], out pwm))
            {
                return false;
            }

            // Нормализация/ограничения (по желанию)
            if (pwm < 0) pwm = 0;
            if (pwm > 100) pwm = 100;

            telemetryFrame.TimeMs = t;
            telemetryFrame.SetpointRpm = sp;
            telemetryFrame.MeasuredRpm = pv;
            telemetryFrame.PwmPercent = pwm;

            return true;
        }


        public bool TryParseAck(string line, out AckFrame ack)
        {
            ack = new AckFrame();
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }
            line = line.Trim();
            var parts = line.Split(',');

            if (parts.Length < 2)
            {
                return false;
            }
            if (!parts[0].Equals("ACK", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            ack.What = parts[1].Trim().ToUpperInvariant();

            if (parts.Length >= 3)
            {
                var s = parts[2].Trim().Replace(',', '.');
                float v;
                if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                {
                    ack.Value = v;
                }
                else
                {
                    ack.Value = null;
                }
            }
            else
            {
                ack.Value = null;
            }
            return true;
        }

        public bool TryParsePidConfig(string line, out PidConfig cfg)
        {
            cfg = new PidConfig();
            if (string.IsNullOrWhiteSpace(line)) return false;

            line = line.Trim();
            var parts = line.Split(',');

            if (parts.Length != 4) return false;
            if (!parts[0].Equals("CFG", StringComparison.OrdinalIgnoreCase)) return false;

            if (!TryParseFloat(parts[1], out cfg.Kp)) return false;
            if (!TryParseFloat(parts[2], out cfg.Ki)) return false;
            if (!TryParseFloat(parts[3], out cfg.Kd)) return false;

            return true;
        }

        private bool TryParseFloat(string s, out float v)
        {
            s = (s ?? "").Trim().Replace(',', '.');
            return float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        }


        public void ProcessLine(string line)
        {
            TelemetryFrame tf;
            if (TryParseTelemetry(line, out tf))
            {
                telemetryFrameReceived?.Invoke(tf);
                return;
            }

            PidConfig cfg;
            if (TryParsePidConfig(line, out cfg))
            {
                pidConfigReceived?.Invoke(cfg);
                return;
            }

            AckFrame ack;
            if (TryParseAck(line, out ack))
            {
                ackFrameReceived?.Invoke(ack);
                return;
            }

        }
    }
}
