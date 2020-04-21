using System;

namespace Фоновая5._2
{
    class Creature
    {
        protected int x;
        protected int y;
        protected int v;
        public int[,] field = Field(15);
        static Random rnd = new Random();
        public Creature() { }

        public int X
        {
            get { return this.x; }
            set
            {
                try
                {
                    if (value > 14)
                    {
                        throw new IndexOutOfRangeException("Координата не может быть больше 14. X задается 0");
                    }
                    this.x = value;
                }
                catch (Exception er)
                {
                    Console.WriteLine("Ошибка: " + er.Message);
                    this.x = 0;
                }
            }

        }
        public int Y
        {
            get { return this.y; }
            set
            {
                try
                {
                    if (value > 14)
                    {
                        throw new IndexOutOfRangeException("Координата не может быть больше 14. Y задается 0");
                    }
                    this.y = value;
                }
                catch (Exception er)
                { Console.WriteLine("Ошибка: " + er.Message);
                    this.y = 0;
                }
            }
        }
        public int V
        {
            get { return this.v; }
            set
            {
                try
                {
                    if (value < 0)
                    {
                        throw new Exception("Скорость должна быть положительной. Задается 1");
                    }
                    this.v = value;
                }
                catch (Exception er)
                {
                    Console.WriteLine("Ошибка: " + er.Message);
                    this.v = 1;
                }
            }
        }

        public virtual void Draw()
        {
            field[x, y] = 3;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j] == 3) Console.Write("ᗤ "); // ᗤ - packman
                    else if (field[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (field[i, j] == 0) Console.Write("▨ "); // ▨ - еда
                    else Console.Write("⬜"); // ⬜ - пустота
                }
                Console.WriteLine();
            }
        }

        public virtual void Move() { }

        static public int [, ] Field(int m)
        {
            int[,] field = new int[m, m];
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                    field[x, y] = rnd.Next(2);
            }
            return field;
        }
    }

    class Program
    {
            static void Create(Creature creature)
            {
                Console.WriteLine("Вы хотите самостоятельно ввести координаты? Да(1)/ нет (2)");
                byte answer;

                do
                {
                    Console.WriteLine("Введите выбранный вариант (1 или 2)");
                    answer = byte.Parse(Console.ReadLine());
                }
                while (answer != 1 && answer != 2);
                int x = 0, y = 0;
                string s;
                if (answer == 1)
                {
                    // Вводим дату

                    Console.WriteLine("Введите x");
                    do
                    {
                        Console.Write("x=");
                        s = Console.ReadLine();
                    } while (!int.TryParse(s, out x));

                    Console.WriteLine("Введите y");

                    do
                    {
                        Console.Write("y=");
                        s = Console.ReadLine();
                    } while (!int.TryParse(s, out y));
                    creature.X = x;
                    creature.Y = y;
                }
            }

        static void Main(string[] args)
        {
            Creature packman = new Creature();
            Create(packman);
            packman.Draw();
        }
    }
}
