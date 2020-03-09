using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фоновая_4
{
    class Parallelepiped
    {
        private int sh;
        private int g;
        private int h;
        private int x;
        private int y;

        public Parallelepiped()
        {
            sh = 6;
            g = 7;
            h = 5;
            x = y = 0;
        }

        private Parallelepiped(int sh, int g, int h, int x, int y)
        {
            this.sh = sh;
            this.g = g;
            this.h = h;
            this.x = x;
            this.y = y;
        }

        public static Parallelepiped Create(int sh, int g, int h, int x, int y)
        {
            try
            {
                if (sh < 0 || g < 0 || h < 0)
                    throw new Exception("Длины сторон должна быть положительными. Создается объект по умолчанию");
                return new Parallelepiped(sh, g, h, x, y);
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка: {0}", error.Message);
                return new Parallelepiped();
            }
        }

        //Вывод длин сторон
        public void Print()
        {
            Console.WriteLine("Ширина={0} Длина(глубина)={1} Высота={2}", sh, g, h);
        }

        // Объем
        public int Volume
        {
            get
            {
                return sh * g * h;
            }

        }
        public double Diagonal
        {
            get
            {
                return Math.Sqrt(g * g + sh * sh + h * h);
            }
        }


        // Площадь поверхности
        public int Full_Area
        {
            get
            {
                return 2 * (sh * h + h * g + g * sh);
            }

        }

        public int Perimetr
        {
            get
            {
                return 4 * (sh + h + g);
            }
        }

        public int Bottom_Area
        {
            get
            {
                return sh * g;
            }
        }

        public int Front_Area
        {
            get
            {
                return sh * h;
            }
        }

        public int Side_Area
        {
            get
            {
                return sh * h;
            }
        }


        public int Sh
        {
            get
            {
                return sh;
            }
            set
            {
                try
                {
                    if (value > 0) sh = value;
                    else throw new Exception("Ширина должна быть положительной");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int H
        {
            get
            {
                return sh;
            }
            set
            {
                try
                {
                    if (value > 0) h = value;
                    else throw new Exception("Высота должна быть положительной");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public double Radius
        {
            get
            {
                if (h == sh && sh == g && g == h) return Math.Sqrt(g * g + sh * sh + h * h) / 2;
                return -1;
            }
        }

        public int G
        {
            get
            {
                return g;
            }
            set
            {
                try
                {
                    if (value > 0) g = value;
                    else throw new Exception("Глубина должна быть положительной");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int X
        {
            get
            {
                return x;
            }
            
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public int Cube
        {
            get
            {
                if (h == sh && sh == g && g == h) return 1;
                return 0;
            }
        }


        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public void Resize(int side, int value)
        {
            switch (side)
            {
                case 1:
                    sh += value;
                    break;
                case 2:
                    g += value;
                    break;
            }
        }

        public Parallelepiped Cross(Parallelepiped obj)
        {
            int left = Math.Max(this.x, obj.x);
            int top = Math.Min(this.y + this.sh, obj.y + obj.sh);
            int right = Math.Min(this.x + this.g, obj.x + obj.g);
            int bottom = Math.Max(this.y, obj.y);
            int g = right - left;
            int width = top - bottom;
            return new Parallelepiped(width, g, this.h, left, bottom);
        }
    }
    class Program
    {

        static Parallelepiped Create_Figure()
        {
            Console.WriteLine("Вы хотите самостоятельно ввести длины сторон параллепипеда (1)\nили создать фигуру по умолчанию (2)?");
            int answer;
            string s;
            // Просим выбрать конструктор объекта
            do
            {
                Console.WriteLine("Введите выбранный вариант (1 или 2)");
                s = Console.ReadLine();
                if (s == "543210") Environment.Exit(0);
            }
            while (!int.TryParse(s, out answer) || (answer != 1 && answer != 2));
            Console.WriteLine("answer= {0}", answer);
            int sh, h, g, x, y;
            if (answer == 1)
            {
                //Вводим координаты точки
                Console.WriteLine("Введите длины сторон параллелипипеда");

                //Console.WriteLine("Введите длины сторон прямоугольника");
                do
                {
                    Console.Write("ширина=");
                    s = Console.ReadLine();
                    if (s == "543210") Environment.Exit(0);
                }
                while (!int.TryParse(s, out sh));

                do
                {
                    Console.Write("глубина=");
                    s = Console.ReadLine();
                    if (s == "543210") Environment.Exit(0);
                }
                while (!int.TryParse(s, out g));

                do
                {
                    Console.Write("высота=");
                    s = Console.ReadLine();
                    if (s == "543210") Environment.Exit(0);
                }
                while (!int.TryParse(s, out h));

                do
                {
                    Console.Write("Точка вставки(x)=");
                    s = Console.ReadLine();
                    if (s == "543210") Environment.Exit(0);
                }
                while (!int.TryParse(s, out x));

                do
                {
                    Console.Write("Точка вставки(y)=");
                    s = Console.ReadLine();
                    if (s == "543210") Environment.Exit(0);
                }
                while (!int.TryParse(s, out y));
                return Parallelepiped.Create(sh, g, h, x, y);
            }

            return new Parallelepiped();
        }

        static void Peresek(Parallelepiped figure, Parallelepiped figure_2)
        {
            Parallelepiped new_f = figure.Cross(figure_2);
            new_f.Print();
            if (new_f.G <= 0 || new_f.Sh <= 0) // Если отрицательные значения => не переесеаются
                Console.WriteLine("Фигуры не пересекаются");
            else
                Console.WriteLine("Пересечиние: g={0} sh={1}", new_f.G, new_f.Sh);
        }
        static void Main(string[] args)
        {
            string s;
            int value;
            Parallelepiped figure = Create_Figure();
            figure.Print();
            Console.WriteLine("Объем = {0}", figure.Volume);
            Console.WriteLine("Площадь поверхности = {0}", figure.Full_Area);
            Console.WriteLine("Диагональ равна {0:n2}", figure.Diagonal);
            Console.WriteLine("Периметр равен {0}", figure.Perimetr);
            if (figure.Radius == -1) Console.WriteLine("Вокруг фигуры нельзя описать шар");
            else Console.WriteLine("Радиус описанного круга равен {0}", figure.Radius);
            //Console.WriteLine("Ширина={0}", figure.Sh);
            //Console.WriteLine("Высота={0}", figure.H);
            //Console.WriteLine("Длина(глубина)={0}", figure.G);

            if (figure.Cube == 1) Console.WriteLine("Это куб");
            else Console.WriteLine("Это не куб");

            do
            {
                Console.Write("Новая ширина=");
                s = Console.ReadLine();
                if (s == "543210") Environment.Exit(0);
            }
            while (!int.TryParse(s, out value));
            figure.Sh = value;

            do
            {
                Console.Write("Новая глубина=");
                s = Console.ReadLine();
                if (s == "543210") Environment.Exit(0);
            }
            while (!int.TryParse(s, out value));
            figure.G = value;

            do
            {
                Console.Write("Новая высота=");
                s = Console.ReadLine();
                if (s == "543210") Environment.Exit(0);
            }
            while (!int.TryParse(s, out value));
            figure.H = value;

            int answer;
            do
            {
                Console.WriteLine("Введите сторону, которую вы хотите изменить (ширина (1) или глубина (2))");
                s = Console.ReadLine();
            }
            while (!int.TryParse(s, out answer) || (answer != 1 && answer != 2));

            do
            {
                Console.WriteLine("Введите величину, на которую вы хотите изменить прямоугольник");
                s = Console.ReadLine();
            }
            while (!int.TryParse(s, out value));
            //Завершение работы
            if (value == 543210) Environment.Exit(0);
            figure.Resize(answer, value);
            while (true)
            {
                Parallelepiped figure_2 = Create_Figure();
                Peresek(figure, figure_2);
            }
            

        }
    }
}
