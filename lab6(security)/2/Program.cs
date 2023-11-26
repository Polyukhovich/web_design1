using System;
using System.Text;

internal class Program2
{
    // Оголошення делегата, який представляє метод, який приймає масив байтів і повертає масив байтів
    delegate byte[] Method(byte[] message);

    // Метод для виведення результатів шифрування та розшифрування
    static void ShowResult(Method enc, Method dec, byte[] message, string methodname)
    {
        Console.WriteLine(methodname + ":");

        // Виклик методу шифрування
        var EncryptedMsg = enc(message);
        // Виведення зашифрованого повідомлення та його розшифрованого варіанту
        Console.WriteLine($"Encrypted: {Convert.ToBase64String(EncryptedMsg)}" +
            $"\nDecrypted: {Encoding.UTF8.GetString(dec(EncryptedMsg))}\n");
    }

    static void Main(string[] args)
    {
        // Введення повідомлення для шифрування
        Console.Write("Enter message to encrypt: ");
        var msgbytes = Encoding.UTF8.GetBytes(Console.ReadLine());

        // Введення повідомлення для генерації ключа
        Console.Write("Enter message to generate key: ");
        var keymsg = Encoding.UTF8.GetBytes(Console.ReadLine());

        // Введення повідомлення для генерації вектора ініціалізації
        Console.Write("Enter message to generate initialization vector: ");
        var ivmsg = Encoding.UTF8.GetBytes(Console.ReadLine());

        // Ініціалізація об'єктів для DES, AES та Triple DES згідно введених ключів та векторів ініціалізації
        DesEncryption.Init(keymsg, ivmsg);
        AesEncryption.Init(keymsg, ivmsg);
        TripleDesEncryption.Init(keymsg, ivmsg);

        // Виведення ключа та вектора ініціалізації для алгоритму DES
        Console.WriteLine($"\nKey: {Convert.ToBase64String(DesEncryption.GetKey())}\n" +
            $"Initialization vector: {Convert.ToBase64String(DesEncryption.GetInitializationVector())}\n");

        // Виклик методу ShowResult для кожного алгоритму, де шифрується та розшифровується введене повідомлення
        ShowResult(AesEncryption.Encrypt, AesEncryption.Decrypt, msgbytes, "AES");
        ShowResult(DesEncryption.Encrypt, DesEncryption.Decrypt, msgbytes, "DES");
        ShowResult(TripleDesEncryption.Encrypt, TripleDesEncryption.Decrypt, msgbytes, "Triple DES");
    }
}
