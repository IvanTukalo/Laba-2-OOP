using System;
using System.Text;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

partial class Program
{
    static void DoBlock_1()
    {

    }

    static void DoBlock_2()
    {
        MyTime t1 = GetTime("t1");
        MyTime t2 = GetTime("t2");
        MyTime now = GetTime("now");

        Console.WriteLine($"t1: {t1}");
        Console.WriteLine($"t2: {t2}");
        Console.WriteLine($"now: {now}");

        Console.WriteLine("Час t1: " + t1);
        Console.WriteLine("Час t2: " + t2);

        // Тестування різниці в секундах
        Console.WriteLine("Різниця між t1 і t2: " + MyTime.Difference(t1, t2) + " секунд");

        // Додавання секунди, хвилини та години
        Console.WriteLine("t1 + 1 секунда: " + t1.AddOneSecond());
        Console.WriteLine("t1 + 1 хвилина: " + t1.AddOneMinute());
        Console.WriteLine("t1 + 1 година: " + t1.AddOneHour());

        // Додавання довільної кількості секунд
        Console.WriteLine("t1 + 5000 секунд: " + t1.AddSeconds(5000));

        // Визначення, яка зараз пара
        Console.WriteLine("Зараз: " + now + " - " + MyTime.WhatLesson(now));
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

        // Конструктор з валідацією значень
        public MyTime(int hour, int minute, int second)
        {
            if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
                throw new ArgumentException("Неприпустимий час.");

            Hour = hour;
            Minute = minute;
            Second = second;
        }

        // Метод перетворення в рядок "h:mm:ss"
        public override string ToString()
        {
            return $"{Hour}:{Minute:D2}:{Second:D2}";
        }

        // Перетворює час у кількість секунд від початку доби

        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public MyTime(int hour, int minute, int second)
        {
            Hour = (hour + 24) % 24;  // Година в діапазоні [0, 23]
            Minute = (minute + 60) % 60;
            Second = (second + 60) % 60;
        }

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2}:{Second:D2}";
        }

        // Статичний метод для вводу часу з консолі
        public static MyTime InputFromConsole(string label)
        {
            Console.WriteLine($"Введіть час для {label} (година хвилина секунда, через пробіл):");
            string[] parts = Console.ReadLine().Split();
            int h = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            int s = int.Parse(parts[2]);
            return new MyTime(h, m, s);
        }

        // Статичний метод для генерації випадкового часу
        public static MyTime GenerateRandomTime()
        {
            Random random = new Random();
            int h = random.Next(0, 24);
            int m = random.Next(0, 60);
            int s = random.Next(0, 60);
            return new MyTime(h, m, s);
        }
        public static int TimeSinceMidnight(MyTime t)
        {
            return t.Hour * 3600 + t.Minute * 60 + t.Second;
        }

        // Перетворює кількість секунд у об'єкт MyTime
        public static MyTime TimeSinceMidnight(int seconds)
        {
            const int secPerDay = 86400; // 24 * 60 * 60
            seconds %= secPerDay;
            if (seconds < 0) seconds += secPerDay;

            int h = seconds / 3600;
            int m = (seconds / 60) % 60;
            int s = seconds % 60;

            return new MyTime(h, m, s);
        }

        // Додає одну секунду до об'єкта
        public MyTime AddOneSecond()
        {
            return AddSeconds(1);
        }

        // Додає одну хвилину до об'єкта
        public MyTime AddOneMinute()
        {
            return AddSeconds(60);
        }

        // Додає одну годину до об'єкта
        public MyTime AddOneHour()
        {
            return AddSeconds(3600);
        }

        // Додає довільну кількість секунд (можуть бути від'ємні)
        public MyTime AddSeconds(int seconds)
        {
            int totalSeconds = TimeSinceMidnight(this) + seconds;
            return TimeSinceMidnight(totalSeconds);
        }

        // Обчислює різницю між двома часами (у секундах)
        public static int Difference(MyTime t1, MyTime t2)
        {
            return TimeSinceMidnight(t1) - TimeSinceMidnight(t2);
        }

        // Визначає, яка пара зараз триває, або коли йде перерва
        public static string WhatLesson(MyTime t)
        {
            int totalSeconds = TimeSinceMidnight(t);

            if (totalSeconds < TimeSinceMidnight(new MyTime(8, 0, 0)))
                return "Пари ще не почалися";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(9, 20, 0)))
                return "1-а пара";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(9, 40, 0)))
                return "Перерва між 1-ю та 2-ю парами";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(11, 0, 0)))
                return "2-а пара";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(11, 20, 0)))
                return "Перерва між 2-ю та 3-ю парами";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(12, 40, 0)))
                return "3-я пара";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(13, 0, 0)))
                return "Перерва між 3-ю та 4-ю парами";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(14, 20, 0)))
                return "4-а пара";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(15, 40, 0)))
                return "Перерва між 4-ю та 5-ю парами";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(16, 0, 0)))
                return "5-а пара";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(16, 20, 0)))
                return "Перерва між 5-ю та 6-ю парами";
            else if (totalSeconds < TimeSinceMidnight(new MyTime(17, 40, 0)))
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
            Console.WriteLine("-Для виконання блоку 2 - my_frac ООП");

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