namespace Task1
{
    public class Program
    {
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static string GetMessage(int number)
        {
            return IsEven(number)
                ? "Двері відкриваються!"
                : "Двері зачинені...";
        }

        static void Main(string[] args)
        {
            Console.Write("Введіть число: ");
            int number = int.Parse(Console.ReadLine());

            Console.WriteLine(GetMessage(number));
        }
    }
}
