using System;
using System.Windows;
using System.Windows.Controls;
using PracticalWork4_Variant3.Utils;

namespace PracticalWork4_Variant3.Pages
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!InputValidators.TryParseDouble(TbX.Text, out double x) ||
                    !InputValidators.TryParseDouble(TbY.Text, out double y))
                    throw new ArgumentException("Заполните поля x и y корректными числами.");

                double fx;
                if (RbSh.IsChecked == true) fx = Math.Sinh(x);
                else if (RbX2.IsChecked == true) fx = x * x;
                else fx = Math.Exp(x);

                double delta = x - y;

                double c;
                const double eps = 1e-12;

                if (Math.Abs(delta) < eps)
                {
                    c = fx * fx + y * y + Math.Sin(y);
                }
                else if (delta > 0)
                {
                    c = Math.Pow(fx - y, 2.0) + Math.Cos(y);
                }
                else
                {
                    // tg(y) не определён при cos(y) = 0
                    if (Math.Abs(Math.Cos(y)) < 1e-10)
                        throw new ArgumentException("tg(y) не определён при cos(y) = 0. Измените y.");

                    c = Math.Pow(y - fx, 2.0) + Math.Tan(y);
                }

                TbResult.Text = c.ToString("G15");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbX.Text = "";
            TbY.Text = "";
            TbResult.Text = "";
            RbSh.IsChecked = true;
        }
    }
}
