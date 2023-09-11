using System;

class Program
{
    static void Main()
    {
        // Кількість чисел у послідовності

        // Створення генератора випадкових чисел з поточним часом як початковим значенням
        Random random1 = new Random();
        Console.WriteLine("Sequence for identical initial values:"); // Виводимо заголовок для послідовності з однаковими початковими значеннями
        Console.WriteLine("Please enter the number of numbers you would like to receive:");
        int count1 = Convert.ToInt32(Console.ReadLine());
        GenerateAndDisplayRandomNumbers(random1, count1); // Викликаємо функцію для генерації та виведення послідовності

        // Початкове значення для другого генератора випадкових чисел
        int seed2 = 123;
        Random random2 = new Random(seed2);
        Console.WriteLine("\nSequence for different initial values:"); // Виводимо заголовок для послідовності з різними початковими значеннями

        GenerateAndDisplayRandomNumbers(random2, count1); // Викликаємо функцію для генерації та виведення другої послідовності

        // Програма завершується після виведення послідовностей
    }

    static void GenerateAndDisplayRandomNumbers(Random random, int length)
    {
        for (int i = 0; i < length; i++)
        {
            long randomNumber = (long)random.Next(13, 132); // Генерує числа

            Console.WriteLine($"element {i + 1}: {randomNumber}"); // Виводимо псевдовипадкове число разом з номером елемента
        }
    }
}
