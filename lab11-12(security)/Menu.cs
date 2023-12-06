using System;
using System.Text;
using Lab_lastt;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Реєстрація користувачів
Protector.Register("Vimeg", "Bruises", new[] { "Maestro" });
Protector.Register("Rose", "particles", new[] { "Virtuoso" });
Protector.Register("Reaper", "Nightmare");
Protector.Register("Tracer", "Cyperspace", new[] { "Newbie" });


// Процедура аутентифікації
static void Authentication()
{
    Console.Write("Введіть ваше ім'я: ");
    var name = Console.ReadLine();
    Console.Write("Введіть пароль: ");
    var password = Console.ReadLine();

    Protector.LogIn(name, password);
}
// Аутентифікація користувача
Authentication();

var p = Thread.CurrentPrincipal;
// Перевірка успішності входу
            if (p == null)
            {
                Console.WriteLine("Неправильний логін або пароль (передивись чи правильно написав)!");
                return;
            }

// Виведення інформації про поточного користувача
Console.WriteLine("\n----Інформація про аутентифікацію користувача----");
Console.WriteLine("Ім'я: {p.Identity.Name}"  );
Console.Write("\n\tСтатус:");
Console.Write((p.IsInRole("Maestro") ? "\n\tMaestro" : ""));
Console.Write((p.IsInRole("Virtuoso") ? "\n\tVirtuoso" : ""));
Console.Write((p.IsInRole("Newbie") ? "\n\tNewbie" : ""));

Console.WriteLine();

// Показ команд для користувача
while (true)
            {
                Console.WriteLine("\nВиберіть команду:" +
                    (p.IsInRole("Maestro") ? "\n1) Дізнайся про свій рівень підписки " : "") +
                    (p.IsInRole("Virtuoso") ? "\n2) Дізнайся про свій рівень підписки" : "") +
                    "\n3)Цей текст доступний всім(натисни і побачиш дещо )" +
                    (p.IsInRole("Newbiel") ? "\n4)Дізнайся про свій рівень підпискиd" : "") +
                    "\n0) Exit");

                char c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (c)
                {
                    case '1':
                        try { MaestroOnlyCommand(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case '2':
                        try { VirtuosoOnlyCommand(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case '3':
                        Console.WriteLine("(^_^)");
                        break;
                    case '4':
                        try { NewbieOnlyCommand(); }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Неіснуюча команда!!!");
                        break;
                }
            }

// Команда, доступна тільки для підписників зі статусом "Мaestro"
static void MaestroOnlyCommand()
{
            if (Thread.CurrentPrincipal == null)
            {
                throw new Exception("Ви не аутентифіковані!!!");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Maestro"))
            {
                throw new Exception("Ви не Maestro!!!");
            }

            Console.WriteLine("__Ви__");
    Console.WriteLine("(@ @)Maestro(@ @)");
    Console.WriteLine("У вас є доступ до особистого спілкування з художником Robinus ;");
    Console.WriteLine("У вас є доступ до закритої групи, у якій ви можете спілкуватись і мати можливість голосування і давати пропозицію автору який арт буде наступним ;");
    Console.WriteLine("У вас є доступ до закритих робіт Robinus;");
}

// Команда, доступна тільки для підписників зі статусом "Virtuoso"
static void VirtuosoOnlyCommand()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new Exception("Ви не аутентифіковані!!!");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Virtuoso"))
            {
                throw new Exception("Ви не в Virtuoso!!!");
            }
         Console.WriteLine("__Ви__");
            Console.WriteLine("¯\\(°_o)/¯Virtuoso¯\\(°_o)/¯");
    Console.WriteLine("У вас є доступ до закритої групи, у якій ви можете спілкуватись і мати можливість голосування і давати пропозицію автору який арт буде наступним ;");
    Console.WriteLine("У вас є доступ до закритих робіт Robinus;");
}

// Команда, доступна тільки для підписників зі статусом "Newbie"
static void NewbieOnlyCommand()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new Exception("Ви не аутентифіковані!!!");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Newbie"))
            {
                throw new Exception("Ви не Newbie!!!");
    }
    Console.WriteLine("__Ви__");
    Console.WriteLine("(ツ)Newbie(ツ)");
    Console.WriteLine("У вас є доступ до закритих робіт Robinus");
}
