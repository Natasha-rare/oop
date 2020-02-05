using System;

namespace Классы
{
    class Point3D
    {
        public double x;
        public double y;
        public double z;
        public Point3D()
        {
            x = 0; y = 0; z = 0;
        }

        /*public Point3D()
        {
            x = 0; y = 0; z = 0;
        }*/

        public Point3D(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public void Print_Point()
        {
            Console.WriteLine("x={0} y={1} z={2}", x, y, z);
        }

        public void Move_Point(string move, double len)
        {
            switch (move)
            {
                case "x":
                    x += len;
                    break;
                case "y": y += len;
                    break;
                case "z": z += len;
                    break;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Point3D a = new Point3D();
            a.Print_Point();
            Point3D b = new Point3D(1, 2, 3);
            b.Print_Point();
            Console.Write("Введите ось координат (x, y или z) ");
            string move = Console.ReadLine();
            Console.Write("Введите расстояние ");
            double len = double.Parse(Console.ReadLine());
            a.Move_Point(move, len);
            a.Print_Point();
        }
    }
}
