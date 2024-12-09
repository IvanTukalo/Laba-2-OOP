using System;
using System.Text;
using static MyMatrix;

partial class Program
{
    static void DoBlock_1()
    {
        Console.WriteLine("Введіть першу матрицю у вигляді чисел, розділених пробілами і символами переходу на новий рядок:");
        var matrix1 = ReadMatrix();

        Console.WriteLine("\nВведіть другу матрицю у вигляді чисел, розділених пробілами і символами переходу на новий рядок:");
        var matrix2 = ReadMatrix();

        Console.WriteLine("\nВведені матриці:");
        Console.WriteLine("Матриця 1:");
        Console.WriteLine(matrix1);
        Console.WriteLine("Матриця 2:");
        Console.WriteLine(matrix2);

        Console.WriteLine("\n=== Виберіть дію ===");
        Console.WriteLine("1 - Додати матриці");
        Console.WriteLine("2 - Помножити матриці");
        Console.WriteLine("3 - Транспонувати першу матрицю");
        Console.WriteLine("4 - Транспонувати другу матрицю");
        Console.WriteLine("5 - Обчислити визначник першої матриці");
        Console.WriteLine("6 - Обчислити визначник другої матриці");
        Console.WriteLine("7 - Змінити значення першої матриці");
        Console.WriteLine("8 - Змінити значення другої матриці");
        Console.WriteLine("0 - Вихід");

        while (true)
        {
            Console.Write("\nВаш вибір: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nРезультат додавання:");
                        Console.WriteLine(matrix1 + matrix2);
                        break;
                    case "2":
                        Console.WriteLine("\nРезультат множення:");
                        Console.WriteLine(matrix1 * matrix2);
                        break;
                    case "3":
                        Console.WriteLine("\nТранспонована перша матриця:");
                        Console.WriteLine(matrix1.GetTransponedCopy());
                        break;
                    case "4":
                        Console.WriteLine("\nТранспонована друга матриця:");
                        Console.WriteLine(matrix2.GetTransponedCopy());
                        break;
                    case "5":
                        Console.WriteLine("\nВизначник першої матриці:");
                        Console.WriteLine(matrix1.CalcDeterminant());
                        break;
                    case "6":
                        Console.WriteLine("\nВизначник другої матриці:");
                        Console.WriteLine(matrix2.CalcDeterminant());
                        break;
                    case "7":
                        Console.WriteLine("\nЗміна значення у першій матриці:");
                        ModifyMatrix(matrix1);
                        break;
                    case "8":
                        Console.WriteLine("\nЗміна значення у другій матриці:");
                        ModifyMatrix(matrix2);
                        break;
                    case "0":
                        Console.WriteLine("\nЗавершую...");
                        return;
                    default:
                        Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
    static MyMatrix ReadMatrix()
    {
        while (true)
        {
            Console.Write("Введіть матрицю (порожній рядок для завершення вводу):\n");
            string input = ReadMultilineInput();
            try
            {
                return new MyMatrix(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка створення матриці: {ex.Message}");
                Console.WriteLine("Спробуйте ще раз.");
            }
        }
    }

    static string ReadMultilineInput()
    {
        var inputBuilder = new System.Text.StringBuilder();
        string line;

        while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
        {
            inputBuilder.AppendLine(line);
        }

        return inputBuilder.ToString();
    }
    static void DoBlock_2()
    {
        MyTime t1 = GetTime("t1");
        MyTime t2 = GetTime("t2");

        Console.WriteLine($"t1: {t1}");
        Console.WriteLine($"t2: {t2}");

        Console.WriteLine("Час t1: " + t1);
        Console.WriteLine("Час t2: " + t2);
        Console.WriteLine("Різниця між t1 і t2: " + MyTime.Difference(t1, t2) + " секунд");
        Console.WriteLine("t1 + 1 секунда: " + t1.AddOneSecond());
        Console.WriteLine("t1 + 1 хвилина: " + t1.AddOneMinute());
        Console.WriteLine("t1 + 1 година: " + t1.AddOneHour());
        Console.WriteLine("Виберіть метод введення:");
        Console.WriteLine("1. Введення вручну");
        Console.WriteLine("2. Випадковий вибір");
        int choice = int.Parse(Console.ReadLine());

        int totalSeconds = 0;

        if (choice == 1)
        {
            // Користувач вводить кількість секунд вручну
            Console.WriteLine("Введіть кількість секунд:");
            totalSeconds = int.Parse(Console.ReadLine());
        }
        else if (choice == 2)
        {
            // Користувач вводить мінімум і максимум для випадкового вибору
            Console.WriteLine("Введіть мінімум для секунд:");
            int minSeconds = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть максимум для секунд:");
            int maxSeconds = int.Parse(Console.ReadLine());

            Random rand = new Random();
            totalSeconds = rand.Next(minSeconds, maxSeconds);
            Console.WriteLine($"Випадковий вибір: {totalSeconds} секунд");
        }
        Console.WriteLine("t1 + " + totalSeconds + " секунд: " + t1.AddSeconds(totalSeconds));
        Console.WriteLine(MyTime.WhatLesson());
    }

    static MyTime GetTime(string label)
    {
        Console.WriteLine($"Як ви хочете задати час для {label}? (1 - вручну, 2 - випадковий):");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            return MyTime.InputFromConsole(label);
        }
        else if (choice == "2")
        {
            return MyTime.GenerateRandomTime();
        }
        else
        {
            Console.WriteLine("Невірний вибір! Буде використано випадковий час.");
            return MyTime.GenerateRandomTime();
        }
    }

    class MyTime
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public MyTime(int hour, int minute, int second)
        {
            if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
                throw new ArgumentException("Неприпустимий час.");

            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2}:{Second:D2}";
        }

        public static MyTime InputFromConsole(string label)
        {
            Console.WriteLine($"Введіть час для {label} (година хвилина секунда, через пробіл):");
            string[] parts = Console.ReadLine().Split();
            int h = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            int s = int.Parse(parts[2]);
            return new MyTime(h, m, s);
        }

        public static MyTime GenerateRandomTime()
        {
            Random random = new Random();
            int h = random.Next(0, 24);
            int m = random.Next(0, 60);
            int s = random.Next(0, 60);
            return new MyTime(h, m, s);
        }

        // Нестатичний метод для обчислення секунд з початку доби
        public int TimeSinceMidnight()
        {
            return Hour * 3600 + Minute * 60 + Second;
        }

        // Статичний метод залишаємо для сумісності
        public static MyTime TimeSinceMidnight(int seconds)
        {
            const int secPerDay = 86400;
            seconds %= secPerDay;
            if (seconds < 0) seconds += secPerDay;
            
            int h = seconds / 3600;
            int m = (seconds / 60) % 60;
            int s = seconds % 60;

            return new MyTime(h, m, s);
        }

        public MyTime AddOneSecond() => AddSeconds(1);
        public MyTime AddOneMinute() => AddSeconds(60);
        public MyTime AddOneHour() => AddSeconds(3600);

        public MyTime AddSeconds(int seconds)
        {
            int totalSeconds = TimeSinceMidnight() + seconds;
            return TimeSinceMidnight(totalSeconds);
        }

        public static int Difference(MyTime t1, MyTime t2)
        {
            return t1.TimeSinceMidnight() - t2.TimeSinceMidnight();
        }

        public static string WhatLesson()
        {
            MyTime now = GetTime("now");
            Console.WriteLine($"Зараз: {now}");

            int totalSeconds = now.TimeSinceMidnight();

            if (totalSeconds < new MyTime(8, 0, 0).TimeSinceMidnight())
                return "Пари ще не почалися";
            else if (totalSeconds < new MyTime(9, 20, 0).TimeSinceMidnight())
                return "1-а пара";
            else if (totalSeconds < new MyTime(9, 40, 0).TimeSinceMidnight())
                return "Перерва між 1-ю та 2-ю парами";
            else if (totalSeconds < new MyTime(11, 0, 0).TimeSinceMidnight())
                return "2-а пара";
            else if (totalSeconds < new MyTime(11, 20, 0).TimeSinceMidnight())
                return "Перерва між 2-ю та 3-ю парами";
            else if (totalSeconds < new MyTime(12, 40, 0).TimeSinceMidnight())
                return "3-я пара";
            else if (totalSeconds < new MyTime(13, 0, 0).TimeSinceMidnight())
                return "Перерва між 3-ю та 4-ю парами";
            else if (totalSeconds < new MyTime(14, 20, 0).TimeSinceMidnight())
                return "4-а пара";
            else if (totalSeconds < new MyTime(15, 40, 0).TimeSinceMidnight())
                return "Перерва між 4-ю та 5-ю парами";
            else if (totalSeconds < new MyTime(16, 0, 0).TimeSinceMidnight())
                return "5-а пара";
            else if (totalSeconds < new MyTime(16, 20, 0).TimeSinceMidnight())
                return "Перерва між 5-ю та 6-ю парами";
            else if (totalSeconds < new MyTime(17, 40, 0).TimeSinceMidnight())
                return "6-а пара";
            else
                return "Пари вже скінчилися";
        }
    }
    static void Main(string[] args)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        int choice;
        do
        {
            Console.WriteLine("-Для виконання блоку 1 - MyMatrix");
            Console.WriteLine("-Для виконання блоку 2 - my_time ООП");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Виконую блок 1 - MyMatrix");
                    DoBlock_1();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("Виконую блок 2 - my_time ООП");
                    DoBlock_2();
                    Console.WriteLine();
                    break;
                case 0:
                    Console.WriteLine("Зараз завершимо, тільки натисніть будь ласка ще раз Enter");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Команда ``{0}'' не розпізнана. Зробіь, будь ласка, вибір із 1, 2 і 0.", choice);
                    Console.WriteLine();
                    break;
            }
        } while (choice != 0);
    }
}