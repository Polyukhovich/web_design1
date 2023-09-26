using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the text-->");
        string text = Console.ReadLine();
        Console.WriteLine("Enter the key-->");
        string key = Console.ReadLine();

        // Encrypt the text
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        byte[] encryptedText = new byte[textBytes.Length];

        for (int i = 0; i < textBytes.Length; i++)
        {
            encryptedText[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        // Save encrypted text to a file
        using (FileStream encryptStream = new FileStream("encrypted.dat", FileMode.OpenOrCreate))
        {
            encryptStream.Write(encryptedText, 0, encryptedText.Length);
        }

        Console.WriteLine("Text encrypted and written to encrypted.dat.");

        // Decrypt the text
        byte[] decryptedText;

        using (FileStream decryptStream = new FileStream("encrypted.dat", FileMode.Open))
        {
            decryptedText = new byte[decryptStream.Length];
            decryptStream.Read(decryptedText, 0, (int)decryptStream.Length);
        }

        byte[] originalText = new byte[decryptedText.Length];

        for (int i = 0; i < decryptedText.Length; i++)
        {
            originalText[i] = (byte)(decryptedText[i] ^ keyBytes[i % keyBytes.Length]);
        }

        string decryptedTextStr = Encoding.UTF8.GetString(originalText);

        Console.WriteLine("Decrypted Text: " + decryptedTextStr);
    }
}
