using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main()
        {
            // Встановлюємо рядок паролю для тестування.
            const string password = "V3ryC0mpl3xP455w0rd";

            // Генеруємо випадкову сіль для паролю.
            byte[] salt = SaltedHash.GenerateSalt();

            // Виводимо рядок паролю та сіль на консоль.
            Console.WriteLine("Password : " + password);
            Console.WriteLine("Salt = " + Convert.ToBase64String(salt));
            Console.WriteLine();

            // Обчислюємо солену хеш-суму паролю та виводимо її на консоль.
            var hashedPassword1 = SaltedHash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            Console.WriteLine("Hashed Password = " + Convert.ToBase64String(hashedPassword1));

            // Очікуємо введення користувачем для закриття програми.
            Console.ReadLine();
        }
    }
}
