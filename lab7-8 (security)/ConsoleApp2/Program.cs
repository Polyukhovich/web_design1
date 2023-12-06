using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace pz_76
{
    public class RSAWithRSAParameterKey
    {
        // Публічний ключ RSA
        private RSAParameters _publicKey;

        // Приватний ключ RSA
        private RSAParameters _privateKey;

        // Створення нового ключа та збереження його в файл
        public void AssignNewKey(string publicKeyPath, string privateKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;

                // Експорт параметрів публічного ключа
                var publicKey = rsa.ExportParameters(false);

                // Експорт параметрів приватного ключа
                var privateKey = rsa.ExportParameters(true);

                // Збереження публічного та приватного ключа в файли
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));

                // Збереження параметрів публічного та приватного ключа для подальшого використання
                _publicKey = publicKey;
                _privateKey = privateKey;
            }
        }

        // Збереження публічного ключа в файл
        public void SavePublicKeyToFile(string publicKeyPath)
        {
            File.WriteAllText(publicKeyPath, GetPublicKeyXml());
        }

        // Отримання XML-рядка публічного ключа
        private string GetPublicKeyXml()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_publicKey);
                return rsa.ToXmlString(false);
            }
        }

        // Збереження приватного ключа в файл
        public void SavePrivateKeyToFile(string privateKeyPath)
        {
            File.WriteAllText(privateKeyPath, GetPrivateKeyXml());
        }

        // Отримання XML-рядка приватного ключа
        private string GetPrivateKeyXml()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_privateKey);
                return rsa.ToXmlString(true);
            }
        }

        // Завантаження приватного ключа з файлу
        public void LoadPrivateKeyFromFile(string privateKeyPath)
        {
            _privateKey = GetKeyParametersFromXml(File.ReadAllText(privateKeyPath));
        }

        // Завантаження публічного ключа з файлу
        public void LoadPublicKeyFromFile(string publicKeyPath)
        {
            _publicKey = GetKeyParametersFromXml(File.ReadAllText(publicKeyPath));
        }

        // Отримання параметрів ключа з XML-рядка
        private RSAParameters GetKeyParametersFromXml(string xml)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(xml);
                return rsa.ExportParameters(false);
            }
        }

        // Ім'я контейнера ключа
        const string ContainerName = "MyContainer";

        // Створення нового ключа із використанням контейнера
        public void AssignNewKey()
        {
            // Параметри контейнера
            CspParameters cspParams = new CspParameters(1)
            {
                KeyContainerName = ContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };

            // Створення RSACryptoServiceProvider з параметрами контейнера
            var rsa = new RSACryptoServiceProvider(cspParams)
            {
                PersistKeyInCsp = true
            };
        }

        // Шифрування даних
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;

                // Імпорт публічного ключа та шифрування даних
                rsa.ImportParameters(_publicKey);
                cipherbytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherbytes;
        }

        // Розшифрування даних
        public byte[] DecryptData(string privateKeyPath, byte[] dataToDecrypt)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                plain = rsa.Decrypt(dataToDecrypt, true);
            }
            return plain;
        }

    }
}
