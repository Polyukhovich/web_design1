using System.Security.Cryptography;

public class AesEncryption
{
    // Метод для генерації випадкового числа заданої довжини
    public byte[] GenerateRandomNumber(int length)
    {
        // Використовуємо RNGCryptoServiceProvider для створення випадкових чисел
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            // Створюємо байтовий масив заданої довжини
            var randomNumber = new byte[length];
            // Заповнюємо його випадковими числами
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }

    // Метод для шифрування даних методом AES
    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        // Використовуємо AesCryptoServiceProvider для AES-шифрування
        using (var aes = new AesCryptoServiceProvider())
        {
            // Встановлюємо режим шифрування, режим заповнення, ключ і вектор ініціалізації
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            // Використовуємо MemoryStream для зберігання шифрованих даних
            using (var memoryStream = new MemoryStream())
            {
                // Використовуємо CryptoStream для шифрування даних в пам'яті
                var cryptoStream = new CryptoStream(
                    memoryStream,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write);

                // Записуємо дані для шифрування в CryptoStream
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                // Повертаємо результат у вигляді масиву байтів
                return memoryStream.ToArray();
            }
        }
    }

    // Метод для розшифрування даних методом AES
    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        // Використовуємо AesCryptoServiceProvider для AES-розшифрування
        using (var aes = new AesCryptoServiceProvider())
        {
            // Встановлюємо режим розшифрування, режим заповнення, ключ і вектор ініціалізації
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            // Використовуємо MemoryStream для зберігання розшифрованих даних
            using (var memoryStream = new MemoryStream())
            {
                // Використовуємо CryptoStream для розшифрування даних в пам'яті
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Записуємо дані для розшифрування в CryptoStream
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    // Повертаємо результат у вигляді масиву байтів
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
