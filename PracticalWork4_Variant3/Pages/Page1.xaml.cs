using System;
using System.Windows;
using System.Windows.Controls;
using PracticalWork4_Variant3.Utils;

namespace PracticalWork4_Variant3.Pages
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!InputValidators.TryParseDouble(TbX.Text, out double x) ||
                    !InputValidators.TryParseDouble(TbY.Text, out double y) ||
                    !InputValidators.TryParseDouble(TbZ.Text, out double z))
                    throw new ArgumentException("Заполните поля x, y, z корректными числами.");

                // Проверка деления на ноль в arctg(1/(|x-2y|*z))
                InputValidators.EnsureNotZero(z, "z");
                if (Math.Abs(x - 2 * y) < 1e-12)
                    throw new ArgumentException("Для этой формулы требуется x ≠ 2y (иначе деление на 0).");

                double part1 = (x * x + y * y + 2.0) / (1.0 + Math.Pow(Math.Sin(x + y), 2.0));

                double denom = Math.Abs(x - 2 * y) * z;
                InputValidators.EnsureNotZero(denom, "|x-2y|·z");

                double inner = 1.0 / denom;
                double part2_num = x * Math.Abs(y) + Math.Pow(Math.Cos(Math.Atan(inner)), 2.0);
                double part2 = part2_num / (1.0 + x * x * y * y);

                double v = part1 * part2;

                TbResult.Text = v.ToString("G15");
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
            TbZ.Text = "";
            TbResult.Text = "";
        }
    }
}
