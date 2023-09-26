using System;
using System.IO;
using System.Text;

class BruteforceDecryptor
{
    static void Main()
    {
        string encryptedFilePath = "encrypted.dat"; // Вкажіть шлях до зашифрованого файлу
        string targetPhrase = "Mit21"; // Фраза, яку ми шукаємо
        int passwordLength = 5; // Довжина пароля

        byte[] encryptedData = File.ReadAllBytes(encryptedFilePath);
        byte[] password = new byte[passwordLength];

        Console.WriteLine("Decrypting...");

        for (int i = 0; i <= encryptedData.Length - passwordLength; i++)
        {
            for (int j = 0; j < passwordLength; j++)
            {
                password[j] = (byte)(encryptedData[i + j] ^ (byte)targetPhrase[j]);
            }

            string decryptedText = DecryptText(password, encryptedData);

            if (decryptedText.Contains(targetPhrase))
            {
                Console.WriteLine($"Password found: {Encoding.UTF8.GetString(password)}");
                Console.WriteLine($"Decrypted Text: {decryptedText}");
                return; // Завершити програму після знаходження пароля і тексту
            }
        }

        Console.WriteLine("Password not found.");
    }

    static string DecryptText(byte[] password, byte[] encryptedData)
    {
        byte[] decryptedText = new byte[encryptedData.Length];

        for (int i = 0; i < encryptedData.Length; i++)
        {
            decryptedText[i] = (byte)(encryptedData[i] ^ password[i % password.Length]);
        }

        return Encoding.UTF8.GetString(decryptedText);
    }
}
