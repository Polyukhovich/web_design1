using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class SaltedHash
    {
        // Метод для генерації випадкової солі довжиною 32 байти.
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        // Метод для об'єднання двох масивів байтів.
        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        // Метод для обчислення соленої хеш-суми паролю з використанням солі.
        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            }
        }
    }
}
