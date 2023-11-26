using System;
using System.Runtime.CompilerServices;
using System.Text;

class Program1
{
    static void Main(string[] args)
    {
        var originalText = "Jake the dog and fin the human";
        var des = new DesEncryption();
        var tripleDes = new TripleDesEncryption();
        var aes = new AesEncryption();

        // DES Encryption
        var desKey = des.GenerateRandomNumber(8);
        var desIv = des.GenerateRandomNumber(8);
        var desOriginal = originalText;
        var desEncrypted = des.Encrypt(Encoding.UTF8.GetBytes(desOriginal), desKey, desIv);
        var desDecrypted = des.Decrypt(desEncrypted, desKey, desIv);
        var desDecryptedMessage = Encoding.UTF8.GetString(desDecrypted);

        Console.WriteLine("DES Demonstration in .NET");
        Console.WriteLine("-------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + desOriginal);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(desEncrypted));
        Console.WriteLine("Decrypted Text = " + desDecryptedMessage);

        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();

        // Triple DES Encryption
        var tripleDesKey = tripleDes.GenerateRandomNumber(16);
        var tripleDesIv = tripleDes.GenerateRandomNumber(8);
        var tripleDesOriginal = originalText;
        var tripleDesEncrypted = tripleDes.Encrypt(Encoding.UTF8.GetBytes(tripleDesOriginal), tripleDesKey, tripleDesIv);
        var tripleDesDecrypted = tripleDes.Decrypt(tripleDesEncrypted, tripleDesKey, tripleDesIv);
        var tripleDesDecryptedMessage = Encoding.UTF8.GetString(tripleDesDecrypted);

        Console.WriteLine("Triple DES Demonstration in .NET");
        Console.WriteLine("--------------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + tripleDesOriginal);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(tripleDesEncrypted));
        Console.WriteLine("Decrypted Text = " + tripleDesDecryptedMessage);

        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();

        // AES Encryption
        var aesKey = aes.GenerateRandomNumber(32);
        var aesIv = aes.GenerateRandomNumber(16);
        var aesOriginal = originalText;
        var aesEncrypted = aes.Encrypt(Encoding.UTF8.GetBytes(aesOriginal), aesKey, aesIv);
        var aesDecrypted = aes.Decrypt(aesEncrypted, aesKey, aesIv);
        var aesDecryptedMessage = Encoding.UTF8.GetString(aesDecrypted);

        Console.WriteLine("AES Demonstration in .NET");
        Console.WriteLine("-------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + aesOriginal);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(aesEncrypted));
        Console.WriteLine("Decrypted Text = " + aesDecryptedMessage);

        Console.ReadLine();
    }
}
