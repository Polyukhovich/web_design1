using System;
using System.IO;
using System.Security.Cryptography;

namespace SignatureLibrary
{
    public static class MySignature
    {
        // Клас для роботи з цифровим підписом
        private const string ContainerName = "MyRsaContainer"; 
        private static readonly CspParameters CspParams = new CspParameters
        {
            KeyContainerName = ContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore,
        };

        // Метод для генерації цифрового підпису
        public static byte[] GenerateSignature(byte[] inputData)
        {
            using (var rsaProvider = new RSACryptoServiceProvider(CspParams))
            {
                rsaProvider.PersistKeyInCsp = true; 

                // Створення форматера для підпису
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsaProvider);

                rsaFormatter.SetHashAlgorithm(nameof(SHA512));  

                byte[] hashedData = SHA512.HashData(inputData);  // Обчислення хешу вхідних даних

                return rsaFormatter.CreateSignature(hashedData);  // Створення цифрового підпису
            }
        }
        // Метод для експорту публічного ключа у файл
        public static void ExportPublicKey(string filePath)
        {
            using (var rsaProvider = new RSACryptoServiceProvider(CspParams))
            {
                rsaProvider.PersistKeyInCsp = true;

                File.WriteAllText(filePath, rsaProvider.ToXmlString(false));  // Запис публічного ключа в файл
            }
        }
        // Метод для перевірки цифрового підпису
        public static bool VerifySignature(string publicKeyFilePath, byte[] inputData, byte[] signature)
        {
            using (var rsaProvider = new RSACryptoServiceProvider(CspParams))
            {
                rsaProvider.FromXmlString(File.ReadAllText(publicKeyFilePath));  // Зчитування публічного ключа

                rsaProvider.PersistKeyInCsp = true;

                // Створення форматера для перевірки підпису
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsaProvider);

                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));  // Встановлення алгоритму хешування

                byte[] hashedData = SHA512.HashData(inputData);  // Обчислення хешу вхідних даних

                return rsaDeformatter.VerifySignature(hashedData, signature);  // Перевірка цифрового підпису
            }
        }

    }
}
