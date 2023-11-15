using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
namespace ConsoleApp2
{
    class Program
    {
        static void Main()
        {
            UserManager userManager = new UserManager();

            while (true)
            {
                Console.WriteLine("1. Реєстрація нового користувача");
                Console.WriteLine("2. Автентифікація");
                Console.WriteLine("3. Вихід");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        userManager.RegisterUser();
                        break;

                    case "2":
                        userManager.AuthenticateUser();
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
    }
}

