using System;
using System.IO;
using System.Text;
using pz_76;
namespace ConsoleApp3
{
    internal class Program
    {
        public static void Main()
        {
            RsaWithRsaParameterKey();
        }

        private static void RsaWithRsaParameterKey()
        {
            var rsaParams = new RSAWithRSAParameterKey();
            const string original = "I congratulate Pugach Nazar and Tymokhin Roman on the upcoming holidays";

           
            rsaParams.AssignNewKey("Poliukhovych_publicKey.xml", "Poliukhovych_privateKey.xml");

            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParams.DecryptData(encrypted);
            File.WriteAllBytes("encryptedText.txt", encrypted);
            Console.WriteLine("RSA Encryption Demonstration in .NET");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("In Memory Key");
            Console.WriteLine();
            Console.WriteLine(" Original Text = " + original);
            Console.WriteLine(" Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine(" Decrypted Text = " + Encoding.Default.GetString(decrypted));
        }
    }
}
