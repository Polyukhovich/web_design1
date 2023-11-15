using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class PBKDF2
    {
        // Метод для генерації випадкової солі довжиною 32 байти.
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        // Метод для обчислення хеш-суми паролю з використанням алгоритму PBKDF2.
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName hashAlgorithm)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(32);
            }
        }

        // Цей метод має бути реалізований. Він є внутрішнім та не реалізованим.
        internal static byte[] HashPassword(byte[] bytes1, byte[] bytes2, int numberOfRounds)
        {
            throw new NotImplementedException();
        }
    }
}
