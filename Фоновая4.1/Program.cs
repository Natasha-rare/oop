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

        public Parallelepiped(int sh, int g, int h, int x, int y)
        {
            this.sh = sh;
            this.g = g;
            this.h = h;
            this.x = x;
            this.y = y;
        }

        public Parallelepiped(int sh, int g, int x, int y)
        {
            this.sh = sh;
            this.g = g;
            this.h = 0;
            this.x = x;
            this.y = y;
        }
        //Вывод длин сторон
        public void Print()
        {
            Console.WriteLine("Ширина={0} Длина(глубина)={1} Высота={2}", sh, g, h);
        }

        // Объем
        public int Volume()
        {
            return sh * g * h;
        }

        // Площадь поверхности
        public int Area()
        {
            return 2 * (sh * h + h * g + g * sh);
        }

        public int Get_Sh()
        {
            return sh;
        }

        public int Get_H()
        {
            return h;
        }

        public int Get_G()
        {
            return g;
        }

        public int Get_X()
        {
            return x;
        }

        public int Get_Y()
        {
            return y;
        }

        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public void Resize(string side, int value)
        {
            if (side == "глубина") g += value;
            else sh += value;
        }

        public Parallelepiped Equal(Parallelepiped obj)
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
        static bool Cross(int x11, int x12, int x21, int x22)
        {
            return (x21 <= x11 && x11 <= x22) || (x11 <= x21 && x21 <= x12);
        }

        static Parallelepiped Create_Figure()
        {
            Console.WriteLine("Вы хотите самостоятельно ввести длины сторон параллепипеда (1)\nили создать фигуру по умолчанию (2)?");
            int answer;
            // Просим выбрать конструктор объекта
            do
            {
                Console.WriteLine("Введите выбранный вариант (1 или 2)");
                answer = int.Parse(Console.ReadLine());
                if (answer == 543210) Environment.Exit(0);
            }
            while (answer != 1 && answer != 2);

            if (answer == 1)
            {
                // Вводим координаты точки
                // Console.WriteLine("Введите длины сторон параллелипипеда");

                Console.WriteLine("Введите длины сторон прямоугольника");

                Console.Write("ширина=");
                int sh = int.Parse(Console.ReadLine());

                //Завершение работы
                if (sh == 543210) Environment.Exit(0);

                Console.Write("длина=");
                int g = int.Parse(Console.ReadLine());

                //Console.Write("высота=");
                //int h = int.Parse(Console.ReadLine());

                Console.Write("Точка вставки(x)=");
                int x = int.Parse(Console.ReadLine());

                Console.Write("Точка вставки(y)=");
                int y = int.Parse(Console.ReadLine());
                
                return new Parallelepiped(sh, g, x, y);
            }
            return new Parallelepiped();
        }

        static void Peresek(Parallelepiped figure, Parallelepiped figure_2)
        {
            if (Cross(figure.Get_X(), figure.Get_X() + figure.Get_Sh(), figure_2.Get_X(), figure_2.Get_X() + figure_2.Get_Sh()))
            {
                Parallelepiped new_f = figure.Equal(figure_2);
                new_f.Print();
                if (new_f.Get_G() <= 0 || new_f.Get_Sh() <= 0) Console.WriteLine("Фигуры не пересекаются");
                else Console.WriteLine("Пересечиние: g={0} sh={1}", new_f.Get_G(), new_f.Get_Sh());
            }
            else
                Console.WriteLine("Фигуры не пересекаются");
        }
        static void Main(string[] args)
        {
            Parallelepiped figure = Create_Figure();
            figure.Print();
            //Console.WriteLine("Объем = {0}", figure.Volume());
            //Console.WriteLine("Площадь поверхности = {0}", figure.Area());
            //Console.WriteLine("Ширина={0}", figure.Get_Sh());
            //Console.WriteLine("Высота={0}", figure.Get_H());
            //Console.WriteLine("Длина(глубина)={0}", figure.Get_G());

            //string answer;
            //do
            //{
            //    Console.WriteLine("Введите сторону, которую вы хотите изменить (ширина или глубина)");
            //    answer = Console.ReadLine();
            //}
            //while (answer != "ширина" && answer != "глубина");

            //int value;
            //Console.WriteLine("Введите величину, на которую вы хотите изменить прямоугольник");
            //value = int.Parse(Console.ReadLine());
            ////Завершение работы
            //if (value == 543210) Environment.Exit(0);
            //figure.Resize(answer, value);
            while (true)
            {
                Parallelepiped figure_2 = Create_Figure();
                Peresek(figure, figure_2);
            }
            
            //figure_2.Print();
            
        }
    }
}
