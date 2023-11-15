using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter password");
            string passwordToHash = "VeryComplexPassword";

            Console.WriteLine("Set Hash Algorithm");
            HashAlgorithmName hashAlgorithm = SetHashAlgorithm("SHA512");

            for (int i = 0; i < 10; i++)
            {
                int step = 50000;
                int variant = 19;
                // Викликаємо метод для хешування паролю з різною кількістю ітерацій.
                HashPassword(passwordToHash, i * step + variant * 10000, hashAlgorithm);
            }
        }

        private static void HashPassword(string passwordToHash, int numberOfRounds, HashAlgorithmName hashAlgorithm)
        {
            var sw = new Stopwatch();
            sw.Start();
            // Використовуємо PBKDF2 для хешування паролю з заданою кількістю ітерацій та алгоритмом.
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, hashAlgorithm);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
            Console.Write("Iterations <" + numberOfRounds + "> Time: " + sw.ElapsedMilliseconds + "ms");
            Console.WriteLine();
        }

        public static HashAlgorithmName SetHashAlgorithm(string type)
        {
            // Метод для вибору алгоритму хешування з переданого типу.
            switch (type)
            {
                case "SHA1":
                    return HashAlgorithmName.SHA1;
                case "SHA256":
                    return HashAlgorithmName.SHA256;
                case "SHA384":
                    return HashAlgorithmName.SHA384;
                case "SHA512":
                    return HashAlgorithmName.SHA512;
                default:
                    return HashAlgorithmName.SHA1;
            }
        }
    }
}
