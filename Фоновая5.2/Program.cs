using System;

namespace Фоновая5._2
{
    class Creature
    {
        protected int x;
        protected int y;
        protected int v;
        private int[,] field = Create_Field(15);
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

        public int[,] Field
        {
            get { return this.field; }
            set
            {
                field = value;
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
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (field[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (field[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }

        public virtual void Move() { }

        static public int[,] Create_Field(int m)
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
                        else
                            throw new IndexOutOfRangeException();
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
                        else
                            throw new IndexOutOfRangeException();
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
                        else
                            throw new IndexOutOfRangeException();
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
                        else
                            throw new IndexOutOfRangeException();
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
            base[x, y] = 3;
        }

        public override void Draw()
        {
            base[x, y] = 3;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (base[i, j] == 3) Console.Write("ᗧ "); // ᗧ - packman
                    else if (base[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (base[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (base[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }

        public override int X
        {
            get
            {
                return x;
            }
            set { this.x = value; }
        }

        public override int Y
        {
            get { return y; }
            set { this.y = value; }
        }
    }

    class Ghost : Creature
    {
        private int direction; // направление 0 - влево/назад (по умолчанию)
        // 1 - вправо
        // 2 - вверх
        // 3 - вниз
        private int last;

        public override int X
        {
            get
            {
                return x;
            }
            set { this.x = value; }
        }

        public override int Y
        {
            get { return y; }
            set { this.y = value; }
        }

        public override void Move()
        {
            base[x, y] = last;
            if (x - 1 < 0 || y - 1 < 0 || y + 1 > 14
                || x + 1 > 14 || base[x, y] == 2) direction = (direction + 1) % 4;
            Console.WriteLine(direction);

            switch (direction)
            {
                case 0: --y;
                    break;
                case 1: ++y;
                    break;
                case 2:  --x;
                    break;
                case 3: ++x;
                    break;
            }
            last = base[x, y];
            base[x, y] = 5;
        }

        public override void Draw()
        {
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
        private int [] p = new int [3];
        private int last;

        public override int X
        {
            get
            {
                return x;
            }
            set { this.x = value; }
        }

        public override int Y
        {
            get { return y; }
            set { this.y = value; }
        }

        public override void Draw()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (base[i, j] == 3) Console.Write("ᗧ "); // ᗧ - packman
                    else if (base[i, j] == 2) Console.Write("▦ "); //▦ - стена
                    else if (base[i, j] == 1) Console.Write("❦ "); // ❦ - вишенка
                    else if (base[i, j] == 0) Console.Write("◂ "); // ◂ - еда
                    else if (base[i, j] == 5) Console.Write("👻 "); // 👻 - ghost
                    else if (base[i, j] == 4) Console.Write("☠ "); // ☠ - smart_ghost
                    else Console.Write("⃞ "); // ⃞ - пустота
                }
                Console.WriteLine();
            }
        }

        public override void Move()
        {
            base[x, y] = last;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (base[i, j] == 3)
                    {
                        p[0] = i;
                        p[1] = j;
                        p[2] = 4;
                        break;
                    }
                }
                if (p[2] == 4) break; 
            }
            Console.WriteLine("{0} {1} {2} {3}", x, y, p[0], p[1]);
            if (p[0] > x && p[1] > y)
            {
                if (base[x + 1, y + 1] == 2)
                {
                    if (base[x, y + 1] != 2)
                        ++y;
                    else if (base[x + 1, y] != 2)
                        ++x;
                    else --x;
                }
                else
                {
                    ++x;
                    ++y;
                }
            }
            else if (p[0] < x && p[1] < y)
            {
                if (base[x - 1, y - 1] == 2)
                {
                    if (base[x, y - 1] != 2)
                        --y;
                    else if (base[x - 1, y] != 2)
                        --x;
                    else ++x;
                }
                else
                {
                    --x;
                    --y;
                }
            }
            else if (p[0] > x && p[1] == y && base[x + 1, y] != 2)
            {
                ++x;
            }
            else if (p[0] > y && p[1] == x && base[x, y + 1] != 2)
            {
                ++y;
            }
            else if (p[0] < x && p[1] == y && base[x - 1, y] != 2)
            {
                --x;
            }
            else if (p[0] < y && p[1] == x && base[x, y - 1] != 2)
            {
                --y;
            }
            else
            {
            }
            last = base[x, y];
            base[x, y] = 4;
        }
    }

    class Program
    {
        static void Create(Packman creature)
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
            while (true)
            {
                packman.Move();
                packman.Draw();
            }
        }

        static void Play(Packman packman, Ghost ghost)
        {
            while(true)
            {
                packman.Move();
                ghost.Move();
                //Console.WriteLine("{0} {1} {2} {3}", ghost.X, ghost.Y, packman.X, packman.Y);
                if (ghost.X == packman.X && ghost.Y == packman.Y)
                {
                    Console.WriteLine("Игра Окончена ;(");
                    Environment.Exit(0);
                }
                ghost.Draw();
            }
        }

        static void Play(Packman packman, SmartGhost ghost)
        {
            while (true)
            {
                packman.Move();
                ghost.Move();
                //Console.WriteLine("{0} {1} {2} {3}", ghost.X, ghost.Y, packman.X, packman.Y);
                if (ghost.X == packman.X && ghost.Y == packman.Y)
                {
                    Console.WriteLine("Игра Окончена ;(");
                    Environment.Exit(0);
                }
                ghost.Draw();
            }
        }

        static void Play(Packman packman, Ghost ghost, SmartGhost smart)
        {
            while (true)
            {
                packman.Move();
                ghost.Move();
                smart.Move();
                if (ghost.X == packman.X && ghost.Y == packman.Y)
                {
                    Console.WriteLine("Игра Окончена ;(");
                    Environment.Exit(0);
                }
                smart.Draw();
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
                Creature c = new Creature();
                Packman packman = new Packman();
                packman.Field = c.Field;
                Create(packman);

                packman.Draw();
                switch (answer)
                {
                    case 1:
                        Play(packman);
                        break;
                    case 2:
                        Ghost ghost = new Ghost();
                        ghost.Field = c.Field;
                        ghost.X = ghost.Y = 10;
                        Play(packman, ghost);
                        break;
                    case 3:
                        SmartGhost smart = new SmartGhost();
                        smart.X = smart.Y = 3;
                        smart.Field = c.Field;
                        Play(packman, smart);
                    break;
                    case 4:
                        SmartGhost smart1 = new SmartGhost();
                        Ghost ghost1 = new Ghost();
                        ghost1.X = ghost1.Y = 5;
                        smart1.X = smart1.Y = 3;
                        ghost1.Field = c.Field;
                        smart1.Field = c.Field;
                    Play(packman, ghost1, smart1);
                    break;
                }





            }
        }
    
}
