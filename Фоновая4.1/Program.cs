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

        // Конструктор по умолчанию
        public Parallelepiped()
        {
            sh = 6;
            g = 7;
            h = 5;
            x = y = 0;
        }

        // Конструктор с пользовательскими величинами
        public Parallelepiped(int sh, int g, int h, int x, int y)
        {
            this.sh = sh;
            this.g = g;
            this.h = h;
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

        // Получаем значения закрытых полей
        public int Get_Sh() //ширина
        {
            return sh;
        }

        public int Get_H() //высота
        {
            return h;
        }

        public int Get_G() // глубина
        {
            return g;
        }

        public int Get_X() // точка вставки - х
        {
            return x;
        }

        public int Get_Y() // точка вставки - у
        {
            return y;
        }

        // Двигаем фигуру
        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        // Изменение размеров
        public void Resize(int side, int value)
        {
            if (side == 2) g += value;
            else sh += value;
        }

        // Функция, которая ищет пересечение
        public Parallelepiped Cross(Parallelepiped obj)
        {
            int left = Math.Max(this.x, obj.x);
            int top = Math.Min(this.y + this.sh, obj.y + obj.sh);
            int right = Math.Min(this.x + this.g, obj.x + obj.g);
            int bottom = Math.Max(this.y, obj.y);
            int g = right - left;
            int width = top - bottom;
            return new Parallelepiped(width, g, h, left, bottom);
        }
    }
    class Program
    {

        static Parallelepiped Create_Figure()
        {
            Console.WriteLine("Вы хотите самостоятельно ввести длины сторон параллепипеда " +
                "(1)\nили создать фигуру по умолчанию (2)?");
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

                Console.WriteLine("Введите длины сторон параллелипипеда");

                Console.Write("ширина=");
                int sh = int.Parse(Console.ReadLine());

                //Завершение работы
                if (sh == 543210) Environment.Exit(0);

                Console.Write("длина=");
                int g = int.Parse(Console.ReadLine());

                Console.Write("высота=");
                int h = int.Parse(Console.ReadLine());

                Console.Write("Точка вставки(x)=");
                int x = int.Parse(Console.ReadLine());

                Console.Write("Точка вставки(y)=");
                int y = int.Parse(Console.ReadLine());
                
                return new Parallelepiped(sh, g, h, x, y);
            }
            return new Parallelepiped();
        }

        // Проверка пересечения
        static void Peresek(Parallelepiped figure, Parallelepiped figure_2)
        {
            Parallelepiped new_f = figure.Cross(figure_2);
            new_f.Print();
            if (new_f.Get_G() <= 0 || new_f.Get_Sh() <= 0) // Если отрицательные значения => не переесеаются
                Console.WriteLine("Фигуры не пересекаются"); 
            else
                Console.WriteLine("Пересечиние: g={0} sh={1}", new_f.Get_G(), new_f.Get_Sh());
        }

        static void Main(string[] args)
        {
            Parallelepiped figure = Create_Figure(); // Создание
            figure.Print(); // Вывод фигуры
            Console.WriteLine("Объем = {0}", figure.Volume());
            Console.WriteLine("Площадь поверхности = {0}", figure.Area());
            Console.WriteLine("Ширина={0}", figure.Get_Sh());
            Console.WriteLine("Высота={0}", figure.Get_H());
            Console.WriteLine("Длина(глубина)={0}", figure.Get_G());

            int answer;
            do
            {
                
                Console.WriteLine("Введите сторону, которую вы хотите изменить (ширина (1) или глубина (2))");
                answer = int.Parse(Console.ReadLine());
                if (answer == 543210) Environment.Exit(0);
            }
            while (answer != 1 && answer != 2);

            int value;
            Console.WriteLine("Введите величину, на которую вы хотите изменить прямоугольник");
            value = int.Parse(Console.ReadLine());
            //Завершение работы
            if (value == 543210) Environment.Exit(0);
            figure.Resize(answer, value);
            while (true)
            {
                Parallelepiped figure_2 = Create_Figure();
                Peresek(figure, figure_2);
            }
            
            //figure_2.Print();
            
        }
    }
}
