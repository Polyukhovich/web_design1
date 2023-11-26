using System.Security.Cryptography;

// Клас для шифрування методом TripleDES
public class TripleDesEncryption
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

    // Метод для шифрування даних методом TripleDES
    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        // Використовуємо TripleDESCryptoServiceProvider для TripleDES-шифрування
        using (var des = new TripleDESCryptoServiceProvider())
        {
            // Встановлюємо режим шифрування, режим заповнення, ключ і вектор ініціалізації
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;

            // Використовуємо MemoryStream для зберігання шифрованих даних
            using (var memoryStream = new MemoryStream())
            {
                // Використовуємо CryptoStream для шифрування даних в пам'яті
                var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);

                // Записуємо дані для шифрування в CryptoStream
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                // Повертаємо результат у вигляді масиву байтів
                return memoryStream.ToArray();
            }
        }
    }

    // Метод для розшифрування даних методом TripleDES
    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        // Використовуємо TripleDESCryptoServiceProvider для TripleDES-розшифрування
        using (var des = new TripleDESCryptoServiceProvider())
        {
            // Встановлюємо режим розшифрування, режим заповнення, ключ і вектор ініціалізації
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;

            // Використовуємо MemoryStream для зберігання розшифрованих даних
            using (var memoryStream = new MemoryStream())
            {
                // Використовуємо CryptoStream для розшифрування даних в пам'яті
                using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write))
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
