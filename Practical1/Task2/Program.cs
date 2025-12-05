using System;

namespace Task2
{
    public class Program
    {
        public static void Main()
        {
            int[] numbers = GenerateRandomArray(10, 1, 100);

            Console.WriteLine("Масив:");
            foreach (int n in numbers)
                Console.Write(n + " ");
            Console.WriteLine();

            Console.WriteLine("Сума: " + GetSum(numbers));
            Console.WriteLine("Середнє: " + GetAverage(numbers));
            Console.WriteLine("Мінімум: " + GetMin(numbers));
            Console.WriteLine("Максимум: " + GetMax(numbers));
        }

        public static int[] GenerateRandomArray(int size, int min, int max)
        {
            Random rnd = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
                array[i] = rnd.Next(min, max + 1);

            return array;
        }

        public static int GetSum(int[] numbers)
        {
            int sum = 0;
            foreach (int n in numbers)
                sum += n;
            return sum;
        }

        public static double GetAverage(int[] numbers)
        {
            if (numbers.Length == 0) return 0;
            return (double)GetSum(numbers) / numbers.Length;
        }

        public static int GetMin(int[] numbers)
        {
            int min = numbers[0];
            foreach (int n in numbers)
                if (n < min)
                    min = n;
            return min;
        }

        public static int GetMax(int[] numbers)
        {
            int max = numbers[0];
            foreach (int n in numbers)
                if (n > max)
                    max = n;
            return max;
        }
    }
}
