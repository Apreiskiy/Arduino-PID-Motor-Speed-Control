using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PID_Regulator
{
    public partial class GraphicsForm : Form
    {
        private MainForm mainF;

        private bool plotSP = true;
        private bool plotRPM = true;
        private bool plotPWM = true;

        private const int MaxPoints = 2000; // чтобы не лагало
        private bool autoScroll = true;     // автопрокрутка по X включена, пока не сделали zoom

        public GraphicsForm(MainForm mainForm)
        {
            InitializeComponent();
            mainF = mainForm;

            EnableZoom();

            // двойной клик — сброс зума и возврат автопрокрутки
            graphChart.MouseDoubleClick += graphChart_MouseDoubleClick;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ClearCHart();
            SendCommand("RST");
            ResetZoomAndAutoscroll();
        }

        private void setSpeedButton_Click(object sender, EventArgs e)
        {
            plotSP = !plotSP;
            graphChart.Series["Заданная скорость"].Enabled = plotSP;
        }

        private void actualSpeedButton_Click(object sender, EventArgs e)
        {
            plotRPM = !plotRPM;
            graphChart.Series["Фактическая скорость"].Enabled = plotRPM;
        }

        private void shimButton_Click(object sender, EventArgs e)
        {
            plotPWM = !plotPWM;
            graphChart.Series["ШИМ"].Enabled = plotPWM;
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            mainF.ShowErrorForm();
        }

        private void ClearCHart()
        {
            foreach (var s in graphChart.Series)
                s.Points.Clear();
        }

        private void SendCommand(string cmd)
        {
            var _sp = mainF?.sp; // берём актуальный порт из главной формы

            if (_sp == null)
            {
                MessageBox.Show("Порт не создан!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_sp.IsOpen)
            {
                MessageBox.Show("Порт закрыт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _sp.WriteLine(cmd);
            }
            catch
            {
                MessageBox.Show("Ошибка записи в COM-порт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddFrame(ComPortParse.TelemetryFrame frame)
        {
            double tSec = frame.TimeMs / 1000.0;
            int sp = frame.SetpointRpm;
            int rpm = frame.MeasuredRpm;
            int pwm = frame.PwmPercent;

            if (plotSP) graphChart.Series["Заданная скорость"].Points.AddXY(tSec, sp);
            if (plotRPM) graphChart.Series["Фактическая скорость"].Points.AddXY(tSec, rpm);
            if (plotPWM) graphChart.Series["ШИМ"].Points.AddXY(tSec, pwm);

            TrimSeries("Заданная скорость");
            TrimSeries("Фактическая скорость");
            TrimSeries("ШИМ");

            if (autoScroll)
                AutoScrollX(tSec);
        }

        private void TrimSeries(string seriesName)
        {
            var series = graphChart.Series[seriesName];
            while (series.Points.Count > MaxPoints)
                series.Points.RemoveAt(0);
        }

        private void AutoScrollX(double tSec)
        {
            double window = 20.0;
            var area = graphChart.ChartAreas["ChartArea1"];

            area.AxisX.Minimum = Math.Max(0, tSec - window);
            area.AxisX.Maximum = area.AxisX.Minimum + window;
        }


        private void EnableZoom()
        {
            var area = graphChart.ChartAreas["ChartArea1"];

            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;

            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;

            area.AxisX.ScaleView.Zoomable = true;
            area.AxisY.ScaleView.Zoomable = true;

            area.CursorX.SelectionColor = Color.FromArgb(60, Color.Gray);
            area.CursorY.SelectionColor = Color.FromArgb(60, Color.Gray);

            graphChart.SelectionRangeChanged += graphChart_SelectionRangeChanged;
        }

        private void graphChart_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            autoScroll = false;
        }

        private void graphChart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ResetZoomAndAutoscroll();
        }

        private void ResetZoomAndAutoscroll()
        {
            var area = graphChart.ChartAreas["ChartArea1"];
            area.AxisX.ScaleView.ZoomReset();
            area.AxisY.ScaleView.ZoomReset();
            autoScroll = true;
        }

    }
}
