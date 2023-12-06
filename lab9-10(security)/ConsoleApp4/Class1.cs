using System;
using System.IO;
using System.Text;
using SignatureLibrary;
class Program
{
    static void Main()
    {
        // Налаштування кодування консолі на Unicode для правильного відображення тексту
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;
        // Введення користувача в меню цифрового підпису
        char choice = ' ';
        while (choice != 'e')
        {
            
            Console.WriteLine("Меню Цифрового Підпису");
            Console.WriteLine("1. Створити Підпис (1)");
            Console.WriteLine("2. Перевірити Підпис (2)");
            Console.WriteLine("3. Зберегти Публічний Ключ (3)");
            Console.WriteLine("4. Створити Текстовий Документ (4)");
            Console.WriteLine("5. Вийти (0)");

            // Ввід користувача
            Console.Write("Веберіть функцію: ");
            choice = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            // Обробка вибору користувача
            switch (choice)
            {
                case '1':
                    GenerateSignature();
                    break;
                case '2':
                    VerifySignature();
                    break;
                case '3':
                    SavePublicKey();
                    break;
                case '4':
                    CreateTextDocument();
                    break;
                case '0':
                    return;
                default:
                    Console.WriteLine("Error! Invalid Command!");
                    break;
            }
        }
    }
    static void SavePublicKey()
    {
        // Задання імені файлу для збереження публічного ключа
        string defaultFileName = "MyPublicKey.xml";
        string savePath = Path.Combine(Environment.CurrentDirectory, defaultFileName);

        if (File.Exists(savePath))
        {
            Console.WriteLine($"Публічний ключ існує у файлі {defaultFileName}.\n");
        }
        else
        {
            try
            {
                // Створення нового ключа, якщо файл не існує
                MySignature.ExportPublicKey(savePath);
                Console.WriteLine($"Публічний ключ успішно збережено у теці {Environment.CurrentDirectory}.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}. Не вдалося зберегти публічний ключ.\n");
            }
        }
    }

static void GenerateSignature()
    {
        // Ввід назви файлу документа
        Console.Write("Введіть назву файлу документа: ");
        string fileName = Console.ReadLine().Replace("\"", "");

        // Формування шляху до файлу та зчитування вмісту в байтовий масив
        string filePath = fileName;
        byte[] fileBytes;

        try
        {
            fileBytes = File.ReadAllBytes(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}. Файл не знайдено. Повернення до головного меню.\n");
            return;
        }


        Console.Write("Введіть назву файлу підпису: ");
        string signatureFileName = Console.ReadLine().Replace("\"", "");


        byte[] signature = MySignature.GenerateSignature(fileBytes);

        try
        {
            File.WriteAllBytes(signatureFileName, signature);
            Console.WriteLine("Підпис успішно створено та збережено.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}. Не вдалося зберегти файл підпису.\n");
        }
    }

    static void VerifySignature()
    {
        // Ввід назви файлу документа
        Console.Write("Введіть назву файлу документа: ");
        string fileName = Console.ReadLine().Replace("\"", "");

        // Формування шляху до файлу та зчитування вмісту в байтовий масив
        string filePath = fileName;
        byte[] fileBytes;

        try
        {
            fileBytes = File.ReadAllBytes(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}. Файл не знайдено. Повернення до головного меню.\n");
            return;
        }

        // Ввід назви файлу цифрового підпису
        Console.Write("Введіть назву файлу підпису: ");
        string signatureFileName = Console.ReadLine().Replace("\"", "");
        byte[] signature;

        try
        {
            // Зчитування вмісту файлу підпису в байтовий масив
            signature = File.ReadAllBytes(signatureFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}.Файл не знайдено. Повернення до головного меню.\n");
            return;
        }

        // Ввід назви файлу публічного ключа у форматі XML
        Console.Write("\"Введіть назву файлу публічного ключа у форматі XML: ");
        string keyFileName = Console.ReadLine().Replace("\"", "");

        // Перевірка цифрового підпису за допомогою публічного ключа
        bool check;
        try
        {
            check = MySignature.VerifySignature(keyFileName, fileBytes, signature);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}. Не вдалося перевірити підпис. Повернення до головного меню.\n");
            return;
        }

        // Виведення результату перевірки
        Console.WriteLine(check ? "Підпис вірний" : "Підпис не вірний!");
    }

    static void CreateTextDocument()
    {
        // Ввід назви текстового документа
        Console.Write("Введіть назву текстового документа: ");
        string fileName = Console.ReadLine().Replace("\"", "");

        // Ввід текстового вмісту документа
        Console.Write("Введіть текст для документа: ");
        string textContent = Console.ReadLine();

        try
        {
            // Збереження текстового документа
            File.WriteAllText(fileName + ".txt", textContent);
            Console.WriteLine("Текстовий документ успішно створено.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}.Не вдалося створити текстовий документ.\n");
        }
    }
}
