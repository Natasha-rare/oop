using System;
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
            temperature = FillArray(days[month], month);
        }

        private static int[,] FillArray(int n, int m)
        {
            int[,] temperature = new int[n / 7, 7];
            Random rnd = new Random();
            for (int i = 0; i < (int)n / 7; i++)
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
            for (int i = 0; i <= 7 - day; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0} ", n);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("{0}\t", temperature[0, i]);

                n++;
            }
            Console.WriteLine();
            for (int j = 0; j < days[month - 1] / 7; j++)
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
                if (i != temperature.GetLength(0) - 1)
                    if(Math.Abs(temperature[i, 6] - temperature[i + 1, 0]) > delta)
                        delta = Math.Abs(temperature[i, 6] - temperature[i + 1, 0]);
        }
                
            return delta;
        }

        public int MaxDelta(out int day, out int temp)
        {
            int delta = -100;
            day = temp = 0;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta)
                    {
                        delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                        temp = temperature[i, j];
                        day = (i + 1) * 7 + j + 2 - this.day;
                    }
                        
                }
                if (i != temperature.GetLength(0) - 1)
                    if (Math.Abs(temperature[i, 6] - temperature[i + 1, 0]) > delta)
                    {
                        delta = Math.Abs(temperature[i, 6] - temperature[i + 1, 0]);
                        temp = temperature[i, 6];
                        day = (i + 1) * 7 + 8 - this.day;
                    }
            }
            return delta;
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
            
            MatrixWeather a = Create();
            a.Print();
            int d, t;
            Console.WriteLine(@"Максимальна дельта температур равна {0}, это случилось с {1} на {2} число
температура {1}-го числа составляла {3} градуса(-ов) ", a.MaxDelta(out d, out t), d, d + 1, t);
        }
    }
}
