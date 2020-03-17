﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фоновая_4._2
{

    enum Month
    {
        January = 1, February, March, April, May, June, July,
        August, September, October, Novrmber, December
    }

    class MatrixWeather
    {
        private int month;
        private int day;
        private int[,] temperature;
        static int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public MatrixWeather()
        {
            month = day = 1;
            temperature = FillArray(days[month - 1], month);
        }

        private static int[,] FillArray(int n, int m)
        {
            int[,] temperature = new int[n / 7 + 1, 7];
            Random rnd = new Random();
            for (int i = 0; i <= (int)n / 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (m == 12 || m == 1 || m == 2)
                        temperature[i, j] = rnd.Next(-25, 0);
                    else if (2 < m && m < 6)
                        temperature[i, j] = rnd.Next(-5, 15);
                    else if (5 < m && m < 9)
                        temperature[i, j] = rnd.Next(10, 30);
                    else if (8 < m && m < 12)
                        temperature[i, j] = rnd.Next(-10, 15);
                }
            }
            return temperature;
        }

        private MatrixWeather(int day, int month)
        {
            this.month = month;
            this.day = day;
            temperature = FillArray(days[month - 1], month);
        }

        public static MatrixWeather Create(int day, int month)
        {
            try
            {
                if (!(day > 0 && month > 0 && day <= 7 && month <= 12))
                    throw new Exception("Такой даты не существует. Устанавливается дата 01.01");
                return new MatrixWeather(day, month);
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка: {0}", error.Message);
                return new MatrixWeather();
            }
        }

        public void Print()
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Пн\tВт\tСр\tЧт\tПт\tСб\tВс");
            for (int i = 0; i < day - 1; i++) Console.Write("\t");
            int n = 1;
            for (int i = day - 1; i < 7; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0} ", n);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("{0}\t", temperature[0, i]);
                n++;
            }

            Console.WriteLine();
            for (int j = 1; j <= days[month - 1] / 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    if (n - 1 < days[month - 1])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0} ", n);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0}\t", temperature[j, k]);
                    }
                    n++;
                }
                Console.WriteLine();
            }
        }

        public int MaxDelta()
        {
            int delta = -100;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta)
                        delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                }
        }
                
            return delta;
        }

        public int MaxDelta(out int day, out int temp)
        {
            int delta = -100;
            day = temp = 1;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta &&
                        temperature[i, j + 1] != -100 && temperature[i, j] != -100)
                    {
                        delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                        temp = temperature[i, j];
                        day = i * 7 + j + 2 - this.day;
                    }
                        
                }
            }
            return delta;
        }

        public int Day
        {
            get
            {
                return day;
            }

            set
            {
                try
                {
                    if (value < 0 || value > 7) throw new Exception("Вы ввели неправильный день недели. Ничего не изменяется");
                    int[] mas = new int[temperature.Length];
                    int k = 0, temp = 0;
                    for (int i = 0; i < temperature.GetLength(0); i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            mas[k] = temperature[i, j];
                            k++;
                        }
                    }
                    
                    if (value > day)
                    {
                        k = value - day;
                        day = value;
                        while (k > 0)
                        {
                            temp = mas[mas.Length - 1];
                            for (int i = mas.Length - 2; i >= 0; i--)
                                mas[i + 1] = mas[i];
                            mas[0] = temp;
                            k--;
                        }

                    }
                    else
                    {
                        k = day - value;
                        day = value;
                        while (k > 0)
                        {
                            temp = mas[0];
                            for (int i = 1; i < mas.Length; i++)
                                mas[i - 1] = mas[i];
                            mas[mas.Length - 1] = temp;
                            k--;
                        }
                    
                    }
                    for (int i = 0; i < temperature.GetLength(0); i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            temperature[i, j] = mas[k];
                            k++;
                        }
                    }

            }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
}
        }

        public int Month
        {
            get
            {
                return month;
            }

            set
            {
                try
                {
                    if (value < 0 || value > 12) throw new Exception("Вы ввели неправильный месяц. Ничего не изменяется");
                    month = value;
                    temperature = FillArray(days[month - 1], month);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int Count_Days
        {
            get { return days[month - 1]; }
        }

        public int [, ] Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                for (int i = 0; i < temperature.GetLength(0); i++)
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            temperature[i, j] = value[i, j];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            temperature[i, j] = NoData;
                        }
                    }
                
                    
            }
        }

        public int Zero_Temp
        {
            get
            {
                int count = 0;
                for (int i = 0; i < temperature.GetLength(0); i++)
                    for (int j = 0; j < 7; j++)
                        if (temperature[i, j] == 0) count++;
                return count;
            }
        }

        public static int NoData
        {
            get
            {
                return -100;
            }
        }

    }

    class Program
    {
        static MatrixWeather Create()
        {
            Console.WriteLine("Вы хотите самостоятельно задать день и месяц? Да(1)/ нет (2)");
            byte answer;
            // Просим выбрать конструктор объекта
            do
            {
                Console.WriteLine("Введите выбранный вариант (1 или 2)");
                answer = byte.Parse(Console.ReadLine());
            }
            while (answer != 1 && answer != 2);
            int day = 0, month = 0;
            string s;
            if (answer == 1)
            {
                // Вводим дату

                Console.WriteLine("Введите месяц");
                do
                {
                    Console.Write("month=");
                    s = Console.ReadLine();
                } while (!int.TryParse(s, out month));

                Console.WriteLine("Введите день недели");

                do
                {
                    Console.Write("day=");
                    s = Console.ReadLine();
                } while (!int.TryParse(s, out day));
                return MatrixWeather.Create(day, month);

            }
            return new MatrixWeather();
        }
        static void Main(string[] args)
        {
            MatrixWeather weather = new MatrixWeather();
            Month m;
            while (true)
            {
                int answer = -1;
                Console.WriteLine(@"Что вы хотите сделать?
1 - Создать объект
2 - Вывести температурные показатели
3 - Вывести месяц
4 - Вывести день (с которого начинается месяц)
5 - задать новый месяц
6 - задать новый день недели
7 - Задать массив температур
8 - Найти максимальную дельту температур
9 - Выйти из программы");
                answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case 1: weather = Create();
                        break;
                    case 2:
                        weather.Print();
                        break;
                    case 3:
                        for (m = Month.January; m <= Month.December; m++)
                            if ((int)m == weather.Month)
                                Console.WriteLine("Выбранный вами месяц {0}", m);
                        break;
                    case 4:
                        Console.WriteLine("Месяц начинается с {0}-го дня", weather.Day);
                        break;
                    case 5:
                        Console.Write("Новый месяц=");
                        weather.Month = int.Parse(Console.ReadLine());
                        break;
                    case 6:
                        Console.Write("Новый день недели=");
                        weather.Day = int.Parse(Console.ReadLine());
                        break;
                    case 7:
                        Console.Write("Задать новый массив по умолчанию (1)/ ввести массив вручную (2)");
                        switch(int.Parse(Console.ReadLine()))
                        {
                            case 1:
                                weather.Temperature = weather.Temperature;
                                break;
                            case 2:
                                int[,] value;
                                int a, b;
                                Console.WriteLine("Введите количество недель, который вы хотите заполнить");
                                a = int.Parse(Console.ReadLine());
                                Console.WriteLine("Введите количество дней, который вы хотите заполнить");
                                b = int.Parse(Console.ReadLine());
                                value = new int[a, b];
                                for (int i = 0; i < a; i++)
                                    for (int j = 0; j < b; j++)
                                    {
                                        Console.Write("Введите температуру: ");
                                        value[i, j] = int.Parse(Console.ReadLine());
                                    }
                                weather.Temperature = value;
                                break;

                        }
                        weather.Month = int.Parse(Console.ReadLine());
                        break;
                    case 8:
                        int d, t;
                        Console.WriteLine(@"Максимальна дельта температур равна {0}, это случилось с {1} на {2} число
                        температура {1}-го числа составляла {3} градуса(-ов) ", weather.MaxDelta(out d, out t), d, d + 1, t);
                        // Console.WriteLine(weather.MaxDelta());
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
