using System;

namespace Task5
{
    public class Program
    {
        public static void Main()
        {
            int[][] groups = new int[][]
            {
                new int[] { 80, 90, 70, 60, 100 },
                new int[] { 50, 60, 70, 80, 95 },
                new int[] { 90, 95, 100, 96, 97 }
            };

            PrintGroupStatistics(groups);
        }

        public static double GetAverage(int[] marks)
        {
            if (marks == null || marks.Length == 0)
                throw new ArgumentException("Масив оцінок пустий або null.");

            double sum = 0;
            foreach (int mark in marks)
                sum += mark;

            return sum / marks.Length;
        }

        public static int GetMin(int[] marks)
        {
            if (marks == null || marks.Length == 0)
                throw new ArgumentException("Масив оцінок пустий або null.");

            int min = marks[0];
            foreach (int mark in marks)
                if (mark < min)
                    min = mark;

            return min;
        }

        public static int GetMax(int[] marks)
        {
            if (marks == null || marks.Length == 0)
                throw new ArgumentException("Масив оцінок пустий або null.");

            int max = marks[0];
            foreach (int mark in marks)
                if (mark > max)
                    max = mark;

            return max;
        }

        public static void PrintGroupStatistics(int[][] groups)
        {
            if (groups == null || groups.Length == 0)
            {
                Console.WriteLine("Групи відсутні.");
                return;
            }

            for (int i = 0; i < groups.Length; i++)
            {
                int[] marks = groups[i];
                double avg = GetAverage(marks);
                int min = GetMin(marks);
                int max = GetMax(marks);

                Console.WriteLine($"Група {i + 1}: Середній = {avg:F0}, Мінімальний = {min}, Максимальний = {max}");
            }
        }
    }
}
