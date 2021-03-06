﻿using System;

namespace Классы
{
    class Point3D
    {
        private int x;
        private int y;
        private int z;

        public Point3D()
        {
            x = y = z = 5;
        }

        public static Point3D Create(int x, int y, int z)
        {
            try
            {
                if (x % 5 != 0 && y % 5 != 0 && z % 5 != 0)
                    throw new Exception("Ни одна из координат не кратна 5. Создается объект с координатами (5, 5, 5)");
                return new Point3D(x, y, z);
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка: {0}", error.Message);
                return new Point3D();
            }
        }

        private Point3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }

        public static Point3D operator +(Point3D obj1, Point3D obj2)
            {
            return new Point3D(obj1.x + obj2.x, obj1.y + obj2.y, obj2.z + obj1.z);
            }

        public static Point3D operator -(Point3D obj1, Point3D obj2)
        {
            return new Point3D(obj1.x - obj2.x, obj1.y - obj2.y, obj2.z - obj1.z);
        }

        public static Point3D operator ++(Point3D obj)
        {
            obj.x++;
            obj.y++;
            obj.z++;
            return obj;
        }

        public static Point3D operator --(Point3D obj)
        {
            obj.x--;
            obj.y--;
            obj.z--;
            return obj;
        }

        public static bool operator >=(Point3D obj1, Point3D obj2)
        {
            return (obj1.x >= obj2.x && obj1.y >= obj2.y && obj1.z >= obj2.z);
        }

        public static bool operator <=(Point3D obj1, Point3D obj2)
        {
            return (obj1.x <= obj2.x || obj1.y <= obj2.y || obj1.z <= obj2.z);
        }

        public static bool operator &(Point3D obj1, Point3D obj2)
        {
            return (obj1.x >= 0 && obj1.y >= 0 && obj1.z >= 0) & (obj2.x >= 0 && obj2.y >= 0 && obj2.z >= 0);
        }

        public static bool operator |(Point3D obj1, Point3D obj2)
        {
            return (obj1.x >= 0 && obj1.y >= 0 && obj1.z >= 0) | (obj2.x >= 0 && obj2.y >= 0 && obj2.z >= 0);
        }

        public static bool operator true(Point3D obj)
        {
            if (obj.x > 0 && obj.y < 0 && obj.z < 0) return true;
            return false;
        }

        public static bool operator false(Point3D obj)
        {
            if (!(obj.x > 0 && obj.y < 0 && obj.z < 0)) return true;
            return false;
        }

        public void Print_Point()
        {
            Console.WriteLine("Координаты точки: x={0} y={1} z={2}", x, y, z);
        }

        public void Move_Point(int move, int len)
        {
            switch (move)
            {
                case 1:
                    x += len;
                    break;
                case 2:
                    y += len;
                    break;
                case 3:
                    z += len;
                    break;
            }
        }

        public double Len_Vec
        {
            get
            {
                return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            }
        }

        public void Sum(Point3D obj)
        {
            this.x += obj.x;
            this.y += obj.y;
            this.z += obj.z;
        }

        public void Sum(int n)
        {
            x += n;
            y += n;
            z += n;
        }

        public void Sum(int x, int y, int z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                try
                {
                    if (value >= 0) x = value;
                    else throw new Exception("Присваимое значение должно быть не отрицательным");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }

            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                try
                {
                    if (value >= 0 && value <= 100) y = value;
                    else throw new Exception("Значение не входит в диапазон от нуля до 100. Координате присвоено значение 100");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                    y = 100;
                }

            }
        }

        public int Z
        {
            get
            {
                return z;
            }

            set
            {
                try
                {
                    if (value <= x + y) z = value;
                    else throw new Exception("Превышено возможное значение");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int Increase
        {
            set
            {
                z *= value;
                x *= value;
                y *= value;
            }
        }


    }
    class Program
    {
        static Point3D Create_Point()
        {
            Console.WriteLine("Вы хотите самостоятельно ввести координаты объекта (1)\nили создать объект по умолчанию (2)?");
            byte answer;
            // Просим выбрать конструктор объекта
            do
            {
                Console.WriteLine("Введите выбранный вариант (1 или 2)");
                answer = byte.Parse(Console.ReadLine());
            }
            while (answer != 1 && answer != 2);
            int x = 0, y = 0, z = 0;
            string s;
            if (answer == 1)
            {
                // Вводим координаты точки
                Console.WriteLine("Введите координату точки");

                do
                {
                    Console.Write("x=");
                    s = Console.ReadLine();
                } while (!int.TryParse(s, out x));

                do
                {
                    Console.Write("y=");
                    s = Console.ReadLine();
                } while (!int.TryParse(s, out y));

                do
                {
                    Console.Write("z=");
                    s = Console.ReadLine();
                } while (!int.TryParse(s, out z));
                return Point3D.Create(x, y, z);
            }
            return new Point3D();
        }
        static void Main(string[] args)
        {
            // создаем 2 точки
            Point3D new_point = Create_Point();

            Point3D new_point_2 = Create_Point();

            Console.WriteLine("Первая точка");
            new_point.Print_Point();
            Console.WriteLine("Вторая точка");
            new_point_2.Print_Point();

            // +
            Point3D test_point = new_point + new_point_2;
            Console.WriteLine("Складываем 2 первые точки и получаем новую:");
            test_point.Print_Point();

            // -
            Console.WriteLine("Вычитаем 2 первые точки и получаем новую:");
            test_point = new_point - new_point_2;
            test_point.Print_Point();

            // ++
            Console.WriteLine("Увеличиваем координаты 1-й точки и получаем: ");
            new_point++;
            new_point.Print_Point();

            // --
            Console.WriteLine("Уменьшаем координаты 2-й точки и получаем: ");
            new_point_2--;
            new_point_2.Print_Point();

            // <=
            if (new_point <= new_point_2) Console.WriteLine("Вторая точка выше первой");
            else Console.WriteLine("Первая точка выше второй");

            // >=
            if (new_point & new_point_2) Console.WriteLine("Первая и вторая точки лежат в 1-й четверти выше осей х и у");
            else if (new_point | new_point_2) Console.WriteLine("Первая или вторая точка лежит в 1-й четверти выше осей х и у");
            else Console.WriteLine("Ни первая, ни вторая точка не лежит в 1-й четверти выше осей х и у");

            // true
            if (new_point) Console.WriteLine("Первая точка 'истинная'*");
            else Console.WriteLine("'Истинность'* не соблюдается"); // false

            Console.WriteLine("Точка считается истинной " +
                "если она лежит в 4-й четверти, ниже осей x и y");

            //new_point.Sum(new_point_2);
            //// Вывод точки
            //new_point.Print_Point();
            //int move, len;
            //int x, y, z;
            //string s;
            //new_point.Y = 1022;
            //// Двигаем точку
            //do
            //{
            //    Console.Write("Введите ось координат (x(1), y(2) или z(3)) ");
            //    move = int.Parse(Console.ReadLine());
            //}
            //while (move != 1 && move != 2 && move != 3);

            //do
            //{
            //    Console.Write("Введите расстояние ");
            //    s = Console.ReadLine();
            //} while (!int.TryParse(s, out len));
            //new_point.Move_Point(move, len);

            //new_point.Print_Point();

            //double r = new_point.Len_Vec;
            //Console.WriteLine("Длина радиус-вектора {0}", r);

            //Console.WriteLine("\nСоздаем новую точку\n");

            //Console.WriteLine("После сложения точек координаты большей равны:");
            //new_point.Print_Point();
            //Console.WriteLine();

            //do
            //{
            //    Console.Write("Введите число, на которое нужно увеличить все координаты точки ");
            //    s = Console.ReadLine();
            //} while (!int.TryParse(s, out move));
            //new_point.Sum(move);
            //new_point.Print_Point();
            //Console.WriteLine("Введите числа, на которые нужно изменить координату");
            //do
            //{
            //    Console.Write("x=");
            //    s = Console.ReadLine();
            //} while (!int.TryParse(s, out x));

            //do
            //{
            //    Console.Write("y=");
            //    s = Console.ReadLine();
            //} while (!int.TryParse(s, out y));

            //do
            //{
            //    Console.Write("z=");
            //    s = Console.ReadLine();
            //} while (!int.TryParse(s, out z));
            //new_point.Sum(x, y, z);
            //new_point.Print_Point();
        }
    }
}
