using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Security;

namespace Lab_lastt
{
    public class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        internal static User Register(string userName, string password, string[] roles = null)
        {
            if (_users.ContainsKey(userName))
            {
                throw new Exception("User already exists!");
            }

            // Генерація солі та обчислення хешу пароля
            var salt = GenerateSalt();
            var passwordHash = ComputeHash(password, salt);

            // Створення користувача та реєстрація його в словнику
            var user = new User
            {
                Login = userName,
                PasswordHash = passwordHash,
                Salt = salt,
                Roles = roles ?? new string[0]
            };

            _users.Add(userName, user);
            return user;
        }

        public static bool CheckPassword(string userName, string password)
        {
            if (!_users.ContainsKey(userName))
            {
                throw new InvalidOperationException($"User with username '{userName}' does not exist.");
            }

            var user = _users[userName];
            var enteredPasswordHash = ComputeHash(password, user.Salt);

            return user.PasswordHash == enteredPasswordHash;
        }

        public static void LogIn(string userName, string password)
        {
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName, "OIBAuth");
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                Thread.CurrentPrincipal = principal;
            }
        }

        public static void OnlyForAdminsFeature()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                throw new SecurityException("User must be a member of Admins to access this feature.");
            }

            Console.WriteLine("You have access to this secure feature.");
        }

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rngCryptoServiceProvider = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private static string ComputeHash(string password, string salt)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            using (var rfc2898DeriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32)); // 32 bytes for a 256-bit key
            }
        }
    }
}
