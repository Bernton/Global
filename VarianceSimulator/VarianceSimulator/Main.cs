using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarianceSimulator
{
    public partial class Main : Form
    {
        private Random _random = new Random();
        private List<double> _values = new List<double>();

        public Main()
        {
            InitializeComponent();
            _values.Add(0);
        }

        private void OnChartPaint(object sender, PaintEventArgs e)
        {
            _labelDollarPerRoll.Text = (Math.Round(_values[_values.Count - 1] / _values.Count, 2)) + " $ per roll\non average";

            Graphics g = e.Graphics;
            Pen linePen = new Pen(Color.LightGray, 1);
            Pen middleLinePen = new Pen(Color.Gray, 1);
            Pen chartLinePen = new Pen(Color.Black, 1);

            int chartYMiddle = Round(_chart.Height / 2.0);

            double peak;
            double highestValue = _values.Max();
            double lowestValue = _values.Min();

            if (highestValue > Math.Abs(lowestValue))
            {
                peak = highestValue;
            }
            else
            {
                peak = Math.Abs(lowestValue);
            }

            double YUnitLength = _chart.Height / peak / 2.0;
            double XUnitLength = (double)_chart.Width / (_values.Count - 1);

            int xPart = Round(_chart.Width / 10.0);
            int yPart = Round(_chart.Height / 10.0);

            for (int i = 1; i < 10; i++)
            {
                g.DrawLine(linePen, xPart * i, 0, xPart * i, _chart.Height);
                g.DrawLine(linePen, 0, yPart * i, _chart.Width, yPart * i);
            }

            for (int i = 1; i <= 10; i++)
            {
                int valueX = ((int)((_values.Count - 1) / 10.0 * i));
                g.DrawString(valueX.ToString(), _chart.Font, Brushes.Black, _chart.Width / 10 * i - g.MeasureString(valueX.ToString(), _chart.Font).Width, _chart.Height - _chart.Font.Size * 2);
            }

            for (int i = 0; i < 11; i++)
            {
                int offSet = 0;
                
                int valueY = (int)(peak - ((peak / 5.0 * i)));
                string displayed = valueY + " $";

                offSet = Round(g.MeasureString(displayed, _chart.Font).Height / 10.0 * i);
                g.DrawString(displayed, _chart.Font, Brushes.Black, _chart.Font.Size / 2, (_chart.Height / 10 * i) - offSet);
            }

            g.DrawLine(middleLinePen, 0, chartYMiddle, _chart.Width, chartYMiddle);

            for (int i = 0; i < _values.Count - 1; i++)
            {
                g.DrawLine(chartLinePen, Round(i * XUnitLength), -Round(_values[i] * YUnitLength) + chartYMiddle, Round((i + 1) * XUnitLength), -Round(_values[i + 1] * YUnitLength) + chartYMiddle);
            }
        }

        private void OnChartResize(object sender, EventArgs e)
        {
            _chart.Invalidate();
        }

        private int Round(double number)
        {
            return (int)Math.Round(number);
        }

        private void OnButtonRollClick(object sender, EventArgs e)
        {
            int interation;
            int possibilies;
            double edge;

            if (int.TryParse(_textBoxRollAmount.Text, out interation) &&
                int.TryParse(_textBoxPossibilies.Text, out possibilies) &&
                double.TryParse(_textBoxEdge.Text, out edge))
            {
                for (int i = 0; i < interation; i++)
                {
                    Roll(possibilies, edge);
                }
            }
        }

        private void Roll(int possibilies, double edge)
        {
            if (RandomNumber.GetNextDouble() * (double)possibilies * (double)(1.0 - (edge / 100.0)) < 1.0)
            {
                _values.Add(_values[_values.Count - 1] + (possibilies - 1));
            }
            else
            {
                _values.Add(_values[_values.Count - 1] - 1);
            }

            _chart.Invalidate();
        }
    }
}
