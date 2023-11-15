using System.Collections.Generic;
using System.Security.Cryptography;
using System;

class UserManager
{
    private Dictionary<string, User> users = new Dictionary<string, User>();

    private int GetVariantNumber()
    {
        return 19;
    }
    public void RegisterUser()
    {
        Console.Write("Введіть логін: ");
        string login = Console.ReadLine();

        if (users.ContainsKey(login))
        {
            Console.WriteLine("Користувач з таким логіном вже існує. Спробуйте інший логін.");
            return;
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        // Генерація випадкової солі
        byte[] salt = GenerateSalt();

        // Використання номера варіанта як числа ітерацій
        int iterations = GetVariantNumber() * 10000;

        // Хешування паролю з використанням солі та ітерацій
        string hashedPassword = HashPassword(password, salt, iterations);

        // Зберігання логіну, хешованого паролю та солі у пам'яті
        users.Add(login, new User { Login = login, HashedPassword = hashedPassword, Salt = Convert.ToBase64String(salt) });

        Console.WriteLine("Користувач зареєстрований успішно.");
    }

    public void AuthenticateUser()
    {
        Console.Write("Введіть логін: ");
        string login = Console.ReadLine();

        if (!users.ContainsKey(login))
        {
            Console.WriteLine("Користувача з таким логіном не існує.");
            return;
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        User storedUser = users[login];

        int iterations = GetVariantNumber() * 10000;

        if (AuthenticateUser(password, storedUser.HashedPassword, storedUser.Salt, iterations))
        {
            Console.WriteLine("Автентифікація успішна!");
        }
        else
        {
            Console.WriteLine("Неправильний логін або пароль.");
        }
    }


    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private string HashPassword(string password, byte[] salt, int iterations)
    {
        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] hash = pbkdf2.GetBytes(32); // 32 байти для SHA-256
            return Convert.ToBase64String(hash);
        }
    }

    private bool AuthenticateUser(string inputPassword, string storedHashedPassword, string storedSalt, int iterations)
    {
        byte[] salt = Convert.FromBase64String(storedSalt);
        string inputHashedPassword = HashPassword(inputPassword, salt, iterations);

        // Порівняння хешованих паролів
        return inputHashedPassword == storedHashedPassword;
    }
}

class User
{
    public string Login { get; set; }
    public string HashedPassword { get; set; }
    public string Salt { get; set; }
}
