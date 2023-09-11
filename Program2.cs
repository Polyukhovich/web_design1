using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of random numbers to generate: ");
        if (int.TryParse(Console.ReadLine(), out int sequenceLength) && sequenceLength > 0)
        {
            Console.WriteLine($"Generating a cryptographically secure random sequence of {sequenceLength} numbers:");
            // Створення об'єкта RNGCryptoServiceProvider
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < sequenceLength; i++)
                {
                    byte[] randomNumberBytes = new byte[4]; // Створюємо байтовий масив для зберігання генерованого числа
                    // Генеруємо криптографічно стійке випадкове число
                    rng.GetBytes(randomNumberBytes);
                    // Перетворюємо байтовий масив в ціле число (int)
                    int randomNumber = BitConverter.ToInt32(randomNumberBytes, 0);
                    Console.WriteLine($"Element {i + 1}: {randomNumber}"); // Виводимо генероване число разом з номером елемента
                }
            }
            // Завершуємо програму
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for the number of random numbers to generate.");
        }

        Console.ReadLine(); // Очікуємо, поки користувач натисне клавішу перед завершенням програми
    }
}