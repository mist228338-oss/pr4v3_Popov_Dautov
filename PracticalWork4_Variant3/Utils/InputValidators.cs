using System;
using System.Globalization;

namespace PracticalWork4_Variant3.Utils
{
    public static class InputValidators
    {
        public static bool TryParseDouble(string s, out double value)
        {
            // Разрешаем ввод с точкой или запятой
            return double.TryParse(s?.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        public static void EnsureNotZero(double value, string name)
        {
            if (Math.Abs(value) < 1e-12) throw new ArgumentException($"{name} не должно быть равно 0.");
        }

        public static void EnsurePositive(double value, string name)
        {
            if (value <= 0) throw new ArgumentException($"{name} должно быть > 0.");
        }

        public static void EnsureStep(double dx)
        {
            if (dx <= 0) throw new ArgumentException("dx должно быть > 0.");
            if (dx > 1e6) throw new ArgumentException("dx слишком большое.");
        }
    }
}
