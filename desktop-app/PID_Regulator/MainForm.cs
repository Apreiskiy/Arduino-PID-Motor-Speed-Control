using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PID_Regulator
{
    public partial class MainForm : Form
    {
        public SerialPort sp;
        private Timer timer;
        private ComPortParse parser = new ComPortParse();
        private readonly string portName = "COM5";
        private readonly int baudRate = 115200;
        private GraphicsForm graphicsForm;
        private ErrorGraphForm errorGraphForm;

        public MainForm()
        {
            InitializeComponent();
            parser.telemetryFrameReceived += Parser_TelemertyFrame;
            parser.ackFrameReceived += Parser_AckFrameReceived;
            parser.pidConfigReceived += Parser_PidConfigReceived;
            menuStrip.RenderMode = ToolStripRenderMode.Professional;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
            TryOpenPort();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sp != null && sp.IsOpen)
            {
                ExchangeDataRadioButton.Checked = true;
                ExchangeDataRadioButton.Text = "запущен";
                return;
            }
            TryOpenPort();
        }

        private void TryOpenPort()
        {
            try
            {
                bool exists = Array.Exists(SerialPort.GetPortNames(),
                    p => p.Equals(portName, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    ExchangeDataRadioButton.Checked = false;
                    ExchangeDataRadioButton.Text = "не запущен";
                    return;
                }

                if (sp != null) SafeClosePort();

                sp = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                sp.NewLine = "\n";
                sp.ReadTimeout = 500;
                sp.WriteTimeout = 500;
                sp.DtrEnable = true;
                sp.RtsEnable = true;

                sp.DataReceived += Sp_DataReceived;
                sp.Open();
                SendLine("GET");

                ExchangeDataRadioButton.Checked = true;
                ExchangeDataRadioButton.Text = "запущен";
            }
            catch
            {
                ExchangeDataRadioButton.Checked = false;
                ExchangeDataRadioButton.Text = "не запущен";
                SafeClosePort();
            }
        }


        private void SendLine(string cmd)
        {
            try
            {
                if (sp != null && sp.IsOpen)
                {
                    sp.WriteLine(cmd);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка отправки команды:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (sp != null && sp.IsOpen && sp.BytesToRead > 0)
                {
                    string line = sp.ReadLine(); // ждёт до \n
                    parser.ProcessLine(line);
                }
            }
            catch (TimeoutException)
            {
                // игнорируем: пришли байты, но строка ещё не завершилась \n
            }
            catch
            {
                // если реально отвалилось — таймер переподключит
                BeginInvoke(new Action(() =>
                {
                    ExchangeDataRadioButton.Checked = false;
                    ExchangeDataRadioButton.Text = "не запущен";
                }));
                SafeClosePort();
            }
        }

        private void SafeClosePort()
        {
            try
            {
                if (sp != null)
                {
                    sp.DataReceived -= Sp_DataReceived;
                    if (sp.IsOpen)
                    {
                        sp.Close();
                    }
                }
            }
            catch
            {

            }
        }

        private void Parser_TelemertyFrame(ComPortParse.TelemetryFrame frame)
        {
            BeginInvoke(new Action(() =>
            {
                SetSpeedTextBox.Text = frame.SetpointRpm.ToString();
                MeasuredSpeedTextBox.Text = frame.MeasuredRpm.ToString();
                SHIMTextBox.Text = frame.PwmPercent.ToString();

                graphicsForm?.AddFrame(frame);
                errorGraphForm?.AddFrame(frame);
            }));

        }

        private void Parser_AckFrameReceived(ComPortParse.AckFrame ack)
        {
            BeginInvoke(new Action(() =>
            {
                if (ack.What == "SAVE")
                {
                    MessageBox.Show("Коэффициенты сохранены в EEPROM.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (ack.Value.HasValue)
                {
                    MessageBox.Show($"Принято: {ack.What} = {ack.Value.Value.ToString(CultureInfo.InvariantCulture)}", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Принято: {ack.What}", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }));
        }


        private void Parser_PidConfigReceived(ComPortParse.PidConfig cfg)
        {
            BeginInvoke(new Action(() =>
            {
                // Подтягиваем GUI к тому, что реально в МК
                PCoeffNumeric.Value = (decimal)cfg.Kp;
                ICoeffNumeric.Value = (decimal)cfg.Ki;
                DCoeffNumeric.Value = (decimal)cfg.Kd;
            }));
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ProgramInfoToolStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Программный комплекс управления объектом регулирования\n\n" +
                "Назначение:\n" +
                "Реализация алгоритма ПИД-регулирования и визуальный контроль параметров\n" +
                "технологического процесса в режиме реального времени.\n\n" +
                "Функциональные возможности:\n" +
                "• Подключение к устройству по COM-порту\n" +
                "• Прием, парсинг и отображение входных данных\n" +
                "• Расчет и настройка коэффициентов P, I, D\n" +
                "• Отображение текущего значения, задания и управляющего сигнала\n" +
                "• Диагностика соединения и контроль ошибок связи\n\n" +
                "Программа ориентирована на учебные и исследовательские задачи\n" +
                "в области автоматического управления и АСУ ТП.\n\n",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void SelectionPortToolStrip_Click(object sender, EventArgs e)
        {
            PortSelectionNameForm portSelectionNameForm = new PortSelectionNameForm();
            portSelectionNameForm.ShowDialog();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            if (graphicsForm == null || graphicsForm.IsDisposed)
            {
                graphicsForm = new GraphicsForm(this);
                graphicsForm.FormClosed += GraphicsForm_Closed; ; // Сброс ссылки при закрытии формы
                graphicsForm.Show();
            }
            else
            {
                graphicsForm.Activate(); // Если форма уже открыта, просто активируем её
            }
        }

        private void GraphicsForm_Closed(object sender, FormClosedEventArgs e)
        {
            graphicsForm = null;
        }

        private void SavePCoeffButton_Click(object sender, EventArgs e)
        {
            SendPidCoeff('P', PCoeffNumeric.Value);
        }

        private void SaveICoeffButton_Click(object sender, EventArgs e)
        {
            SendPidCoeff('I', ICoeffNumeric.Value);
        }

        private void SaveDCoeffButton_Click(object sender, EventArgs e)
        {
            SendPidCoeff('D', DCoeffNumeric.Value);
        }

        private void SendPidCoeff(char coeff, decimal value)
        {
            var _sp = sp;

            if (_sp == null || !_sp.IsOpen)
            {
                MessageBox.Show("СОМ-порт закрыт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float v = (float)value;
            string cmd = string.Format(CultureInfo.InvariantCulture, "{0},{1}", coeff, v);

            try
            {
                _sp.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePZUButton_Click(object sender, EventArgs e)
        {
            var _sp = sp;

            if (_sp == null || !_sp.IsOpen)
            {
                MessageBox.Show("СОМ-порт закрыт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _sp.WriteLine("SAVE");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowErrorForm()
        {
            if (errorGraphForm == null || errorGraphForm.IsDisposed)
            {
                errorGraphForm = new ErrorGraphForm();
                errorGraphForm.FormClosed += ErrorForm_Closed;
                errorGraphForm.Show();
            }
            else
            {
                errorGraphForm.Activate();
            }
        }

        private void ErrorForm_Closed(object sender, EventArgs e)
        {
            errorGraphForm = null;
        }
    }
}
