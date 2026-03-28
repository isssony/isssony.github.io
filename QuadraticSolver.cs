using System;

namespace AvaloniaApp
{
    public static class QuadraticSolver
    {
public static (bool hasRoots, double x1, double x2) Solve(double a, double b, double c)
{
    if (a == 0)
        throw new DivideByZeroException("a не может быть 0");

    double d = b * b - 4 * a * c;

    if (d < 0)
        return (false, 0, 0);

    double x1 = (-b + Math.Sqrt(d)) / (2 * a);
    double x2 = (-b - Math.Sqrt(d)) / (2 * a);

    return (true, x1, x2);
}
        

        public static (double a, double b, double c) ParseEquation(string input)
        {
            input = input.Replace(" ", "").Replace("=0", "");
            input = input.Replace("-", "+-");

            double a = 0, b = 0, c = 0;

            var parts = input.Split('+', StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (part.Contains("x^2"))
                    a = ParseCoef(part.Replace("x^2", ""));
                else if (part.Contains("x"))
                    b = ParseCoef(part.Replace("x", ""));
                else
                    c = double.Parse(part);
            }

            return (a, b, c);
        }

        private static double ParseCoef(string s)
        {
            if (string.IsNullOrEmpty(s) || s == "+")
                return 1;
            if (s == "-")
                return -1;

            return double.Parse(s);
        }
    }
}