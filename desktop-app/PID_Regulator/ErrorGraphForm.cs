using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PID_Regulator
{
    public partial class ErrorGraphForm : Form
    {
        private const int MaxPoints = 2000;
        private const double WindowSec = 15.0;

        public ErrorGraphForm()
        {
            InitializeComponent();
        }

        public void AddFrame(ComPortParse.TelemetryFrame frame)
        {
            double t = frame.TimeMs / 1000.0;

            // Ошибка в процентах: e% = (SP - PV) / SP * 100
            double ePercent = 0.0;

            int sp = frame.SetpointRpm;
            int pv = frame.MeasuredRpm;

            if (sp > 0)
            {
                ePercent = 100.0 * (sp - pv) / (double)sp;

                // Ограничение на всякий случай (грубая защита от выбросов)
                if (ePercent > 200.0) ePercent = 200.0;
                if (ePercent < -200.0) ePercent = -200.0;
            }

            var s = errorChart.Series["e(t), %"];
            s.Points.AddXY(t, ePercent);

            while (s.Points.Count > MaxPoints)
                s.Points.RemoveAt(0);

            AutoScrollX(t);
            AutoScaleY(t);
        }


        private void AutoScrollX(double tNow)
        {
            var area = errorChart.ChartAreas["ChartArea1"];
            double xmin = Math.Max(0, tNow - WindowSec);
            area.AxisX.Minimum = xmin;
            area.AxisX.Maximum = xmin + WindowSec;

        }

        private void AutoScaleY(double tNow)
        {
            var area = errorChart.ChartAreas["ChartArea1"];
            var s = errorChart.Series["e(t), %"];

            // ищем min/max только в окне времени
            double xmin = tNow - WindowSec;

            double ymin = double.PositiveInfinity;
            double ymax = double.NegativeInfinity;

            for (int i = s.Points.Count - 1; i >= 0; i--)
            {
                var p = s.Points[i];
                if (p.XValue < xmin) break;

                double y = p.YValues[0];
                if (y < ymin) ymin = y;
                if (y > ymax) ymax = y;
            }

            if (double.IsNaN(ymin) || double.IsInfinity(ymin) ||
            double.IsNaN(ymax) || double.IsInfinity(ymax))
                return;


            // запас и защита от "плоского" масштаба
            if (ymax - ymin < 20) { ymax += 10; ymin -= 10; }

            double pad = (ymax - ymin) * 0.15;
            area.AxisY.Minimum = ymin - pad;
            area.AxisY.Maximum = ymax + pad;
        }
    }
}

