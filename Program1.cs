using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Генератор псевдовипадкових чисел для різних початкових значень:");
        Random random1 = new Random();
        GenerateAndPrintRandomNumbers(random1);

        Console.WriteLine("\nГенератор псевдовипадкових чисел для однакових початкових значень:");
        int seed;
        do
        {
            Console.Write("Введіть початкове значення (ціле число): ");
        }
        while (!int.TryParse(Console.ReadLine(), out seed));

        Random random2 = new Random(seed);
        GenerateAndPrintRandomNumbers(random2);
    }

    static void GenerateAndPrintRandomNumbers(Random random)
    {
        Console.Write("Введіть кількість чисел для генерації: ");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.Write("Будь ласка, введіть коректну кількість: ");
        }

        for (int i = 0; i < count; i++)
        {
            int randomNumber = random.Next();
            Console.WriteLine(randomNumber);
        }
    }
}
