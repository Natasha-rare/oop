using System;

namespace Фоновая5._2
{
    class Creature
    {
        protected int x;
        protected int y;
        protected int v;
        private int[,] field = Field(15);
        static Random rnd = new Random();
        public Creature() { }

        public int this[int i, int j]
        {
            get
            {
                if (i < 15 && j < 15 && i >= 0 && j >= 0)
                {
                    return field[i, j];
                }
                return -100;
            }
            set
            {
                if (i < 15 && j < 15 && i >= 0 && j >= 0)
                {
                    field[i, j] = value;
               }
            }
        }

        public virtual int X
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
        public virtual int Y
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
                {
                    Console.WriteLine("Ошибка: " + er.Message);
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
                    if (field[i, j] == 3) Console.Write("ᗧ "); // ᗧ - packman
                    else if (field[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (field[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (field[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }

        public virtual void Move() { }

        static public int[,] Field(int m)
        {
            int[,] field = new int[m, m];
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                    if (x != 0 && y != 1 && x != 5 && y != 5)
                        field[x, y] = rnd.Next(3);
                    else
                        field[x, y] = 0;
            }
            return field;
        }
    }


    class Packman : Creature
    {
        public override void Move()
        {
            Console.WriteLine(@"Введите направление:
вперед - D
назад - A
вверх - W
вниз - X");
            string answer = Console.ReadKey().Key.ToString();
            Console.WriteLine();
            switch (answer)
            {
                case "W":
                    try
                    {
                        if (base[this.x - 1, this.y] != 2)
                        {
                            base[this.x, this.y] = -1;
                            base.x--;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Packman не может туда идти;(");
                    }
                    break;
                case "X":
                    try
                    {
                        if (base[this.x + 1, y] != 2)
                        {
                            base[this.x, y] = -1;
                            this.x++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Packman не может туда идти;(");
                    }
                    break;
                case "A":
                    try
                    {
                        if (base[this.x, this.y - 1] != 2)
                        {
                            base[this.x, this.y] = -1;
                            this.y--;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Packman не может туда идти;(");
                    }
                    break;
                case "D":
                    try
                    {
                        if (base[this.x, this.y + 1] != 2)
                        {
                            base[this.x, this.y] = -1;
                            this.y++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Packman не может туда идти;(");
                    }
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
            base[this.x, this.y] = 3;
        }

        public override void Draw()
        {
            base.Draw();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (base[i, j] == 3) Console.Write("ᗧ "); // ᗧ - packman
                    else if (base[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (base[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (base[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else if (base[i, j] == 5) Console.Write("👻 "); // 👻 - ghost
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }
    }

    class Ghost : Creature
    {
        private int x1;
        private int y1;

        public override int X
        {
            set { this.x1 = 10; }
        }

        public override int Y
        {
            set { this.y1 = 10; }
        }

        public override void Move()
        {
            base[x1, y1] = 0;
            if (base[x1, y1 - 1] != 2) this.y1--;
            else
                if (base[x1 - 1, y1] != 2) this.x1--;
            else
                if (base[x1 + 1, y1] != 2) this.x1++;
            else
                y1++;
            base[x1, y1] = 5;
            if (base[x1, y1] == 3)
            {
                Console.WriteLine("Игра Окончена ;(");
                Environment.Exit(0);
            }
        }

        public override void Draw()
        {
            base.Draw();
            base[x1, y1] = 5;
            Console.WriteLine("{0} {1} {2} {3}", base.x, base.y, x1, y1);
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (base[i, j] == 3) Console.Write("ᗧ "); // ᗧ - packman
                    else if (base[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (base[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (base[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else if (base[i, j] == 5) Console.Write("👻 "); // 👻 - ghost
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }
    }

    class SmartGhost : Creature
    {

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

        static void Play(Packman packman)
        {
            packman.Move();
            packman.Draw();
        }

        static void Play(Packman packman, Ghost ghost)
        {

            packman.Move();
            ghost.Move();
            packman.Draw();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write("{0}-{1} ", packman[i, j], ghost[i, j]);
                }
                Console.WriteLine();
            }
        }
            static void Main(string[] args)
            {
                Console.WriteLine(@"Добро пожаловать в игру Packman! Выбирете режим игры:
1 - Игра без приведений
2 - Игра с простыми приведениями
3 - Игра с умными приведениями
4 - Смешанная игра
5 - Выход");
                string s;
                int answer;
                do
                {
                    Console.WriteLine("Введите выбранный вариант");
                    s = Console.ReadLine();
                }
                while (!int.TryParse(s, out answer) && answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5);

                Packman packman = new Packman();
                Create(packman);
                packman.Draw();
                switch (answer)
                {
                    case 1:
                        while (true)
                        {
                            Play(packman);
                        }

                    case 2:
                        Ghost ghost = new Ghost();
                        ghost.X = ghost.Y = 10;
                        while (true)
                        {
                            Play(packman, ghost);
                        }
                    case 3:
                        SmartGhost smart = new SmartGhost();
                        while (true)
                        {
                            //Play(packman, smart);
                        }

                    case 4:
                        SmartGhost smart1 = new SmartGhost();
                        Ghost ghost1 = new Ghost();
                        break;
                }





            }
        }
    
}
