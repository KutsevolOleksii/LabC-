using System;

namespace Task4
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Введіть сторону a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введіть сторону b: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Введіть сторону c: ");
            double c = double.Parse(Console.ReadLine());

            if (!IsValidTriangle(a, b, c))
            {
                Console.WriteLine("Трикутник не існує.");
                return;
            }

            Console.WriteLine($"Периметр: {GetPerimeter(a, b, c)}");
            Console.WriteLine($"Площа: {GetArea(a, b, c)}");
            Console.WriteLine($"Тип: {GetTriangleType(a, b, c)}");
        }

        public static bool IsValidTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return false;

            return a + b > c && a + c > b && b + c > a;
        }

        public static double GetPerimeter(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c))
                throw new ArgumentException("Невірні сторони трикутника.");

            return a + b + c;
        }

        public static double GetArea(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c))
                throw new ArgumentException("Невірні сторони трикутника.");

            double p = (a + b + c) / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static string GetTriangleType(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c))
                throw new ArgumentException("Невірні сторони трикутника.");

            if (a == b && b == c)
                return "рівносторонній";

            double[] sides = { a, b, c };
            Array.Sort(sides);
            if (Math.Abs(sides[0] * sides[0] + sides[1] * sides[1] - sides[2] * sides[2]) < 0.0001)
                return "прямокутний";

            if (a == b || a == c || b == c)
                return "рівнобедрений";

            return "довільний";
        }
    }
}
