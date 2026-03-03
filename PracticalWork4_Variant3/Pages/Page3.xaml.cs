using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using PracticalWork4_Variant3.Utils;

namespace PracticalWork4_Variant3.Pages
{
    public partial class Page3 : Page
    {
        private Series _series;

        public Page3()
        {
            InitializeComponent();
            InitChart();
        }

        private void InitChart()
        {
            ChartFx.ChartAreas.Clear();
            ChartFx.Series.Clear();

            ChartFx.ChartAreas.Add(new ChartArea("Main"));

            _series = new Series("y(x)")
            {
                ChartType = SeriesChartType.Line,
                IsValueShownAsLabel = false
            };

            ChartFx.Series.Add(_series);
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!InputValidators.TryParseDouble(TbX0.Text, out double x0) ||
                    !InputValidators.TryParseDouble(TbXk.Text, out double xk) ||
                    !InputValidators.TryParseDouble(TbDx.Text, out double dx) ||
                    !InputValidators.TryParseDouble(TbA.Text, out double a) ||
                    !InputValidators.TryParseDouble(TbB.Text, out double b))
                    throw new ArgumentException("Заполните x0, xk, dx, a, b корректными числами.");

                InputValidators.EnsureStep(dx);

                // Поддерживаем движение слева направо и справа налево
                double direction = Math.Sign(xk - x0);
                if (direction == 0) throw new ArgumentException("x0 и xk не должны совпадать.");

                double step = dx * direction;

                const int maxIters = 1_000_000;
                int iters = 0;

                _series.Points.Clear();
                var sb = new StringBuilder();
                sb.AppendLine("   i |                 x |                 y");
                sb.AppendLine("-----+-------------------+-------------------");

                double x = x0;
                while (direction > 0 ? x <= xk + 1e-12 : x >= xk - 1e-12)
                {
                    double arg = a - b * x;

                    // tan(arg) не определён при cos(arg)=0
                    if (Math.Abs(Math.Cos(arg)) < 1e-10)
                        throw new ArgumentException($"tan(a - b*x) не определён (cos≈0) при x = {x:G15}.");

                    double y = 0.1 * a * Math.Pow(x, 3) * Math.Tan(arg);

                    sb.AppendLine($"{iters,4} | {x,17:G10} | {y,17:G10}");
                    _series.Points.AddXY(x, y);

                    x += step;
                    iters++;
                    if (iters > maxIters)
                        throw new ArgumentException("Слишком много итераций. Увеличьте dx или уменьшите интервал.");
                }

                TbOutput.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbX0.Text = "";
            TbXk.Text = "";
            TbDx.Text = "";
            TbA.Text = "";
            TbB.Text = "";

            TbOutput.Text = "";
            _series.Points.Clear();
        }
    }
}
