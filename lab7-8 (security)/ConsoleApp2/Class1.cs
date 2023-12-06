using System;
using System.IO;
using System.Text;
using pz_76;
namespace ConsoleApp3
{
    internal class Program
    {
        private static RSAWithRSAParameterKey rsaParams;

        public static void Main()
        {
            rsaParams = new RSAWithRSAParameterKey();
            MainMenu();
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("RSA Encryption Demonstration in .NET");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1. Зашифрувати текст та зберегти у файл");
                Console.WriteLine("2. Розшифрувати текст з файлу");
                Console.WriteLine("3. Вийти");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EncryptAndSaveToFile();
                        break;
                    case "2":
                        DecryptFromFile();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private static void EncryptAndSaveToFile()
        {
            Console.Write("Введіть текст для шифрування: ");
            var originalText = Console.ReadLine();
            //
            rsaParams.LoadPublicKeyFromFile("Poliukhovych_publicKey.xml");

            var encryptedText = rsaParams.EncryptData(Encoding.UTF8.GetBytes(originalText));
            File.WriteAllBytes("encryptedText.txt", encryptedText);
            //I congratulate Pugach Nazar and Tymokhin Roman on the upcoming holidays(текст подрібно зашифрувати 
            Console.WriteLine("Текст успішно зашифровано та збережено у файл encryptedText.txt");
            Console.WriteLine();
        }
        private static void DecryptFromFile()
        {
            if (!File.Exists("encryptedText.txt"))
            {
                Console.WriteLine("Файл encryptedText.txt не знайдено. Спочатку зашифруйте текст.");
                Console.WriteLine();
                return;
            }

            Console.Write("Введіть шлях до приватного ключа (наприклад, Poliukhovych_privateKey.xml): ");
            var privateKeyPath = Console.ReadLine();

            if (!File.Exists(privateKeyPath))
            {
                Console.WriteLine("Файл приватного ключа не знайдено. Перевірте шлях та повторіть спробу.");
                Console.WriteLine();
                return;
            }

            var encryptedText = File.ReadAllBytes("encryptedText.txt");
            var decryptedText = rsaParams.DecryptData(privateKeyPath, encryptedText);

            Console.WriteLine("Розшифрований текст: ");
            Console.WriteLine(Encoding.UTF8.GetString(decryptedText));
            Console.WriteLine();
        }


    }
}
