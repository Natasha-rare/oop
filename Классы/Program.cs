using System;

namespace Классы
{
    class Point3D
    {
        private double x;
        private double y;
        private double z;

        public Point3D()
        {
            x = 0; y = 0; z = 0;
        }

        public Point3D(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public void Print_Point()
        {
            Console.WriteLine("Координаты точки: x={0} y={1} z={2}", x, y, z);
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
        static Point3D Create_Point()
        {
            Point3D new_point = new Point3D();
            Console.WriteLine("Вы хотите самостоятельно ввести координаты объекта (1)\nили создать объект по умолчанию (2)?");
            byte answer;
            // Просим выбрать конструктор объекта
            do
            {
                Console.WriteLine("Введите выбранный вариант (1 или 2)");
                answer = byte.Parse(Console.ReadLine());
            }
            while (answer != 1 && answer != 2);

            switch (answer)
            {
                case 1:
                    // Вводим координаты точки
                    Console.WriteLine("Введите координату точки");

                    Console.Write("x=");
                    double x = double.Parse(Console.ReadLine());

                    Console.Write("y=");
                    double y = double.Parse(Console.ReadLine());

                    Console.Write("z=");
                    double z = double.Parse(Console.ReadLine());
                    new_point = new Point3D(x, y, z);
                    break;
                case 2:
                    new_point = new Point3D();
                    break;
            }
            return new_point;
        }
        static void Main(string[] args)
        {
            Point3D new_point = Create_Point(); 
            // Вывод точки
            new_point.Print_Point();
            // Двигаем точку
            Console.Write("Введите ось координат (x, y или z) ");
            string move = Console.ReadLine();
            Console.Write("Введите расстояние ");
            double len = double.Parse(Console.ReadLine());
            new_point.Move_Point(move, len);

            new_point.Print_Point();
        }
    }
}
