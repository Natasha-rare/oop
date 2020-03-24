using System;

namespace point2d
{
    class Point_2D
    {
        private double x;
        private double y;
        private double r;

        public Point_2D()
        {

        }

        public Point_2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double R
        {
            get { return r; }
            set
            {
                r = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public void Move(int way, int length)
        {
            switch (way)
            {
                case 1:
                    y += length;
                    break;
                case 2:
                    y += length / Math.Sqrt(2);
                    x += length / Math.Sqrt(2);
                    break;
                case 3:
                    x += length;
                    break;
                case 4:
                    y -= length / Math.Sqrt(2);
                    x += length / Math.Sqrt(2);
                    break;
                case 5:
                    y -= length;
                    break;
                case 6:
                    y -= length / Math.Sqrt(2);
                    x -= length / Math.Sqrt(2);
                    break;
                case 7:
                    x -= length;
                    break;
                case 8:
                    x -= length / Math.Sqrt(2);
                    y += length / Math.Sqrt(2);
                    break;
            }
        }



        public int Left(Point_2D b)
        {
            if ((this.Y > b.Y && b.X <= this.X) || (b.X > this.X && this.Y <= b.Y))
                return 1;
            return 0;
        }


        public double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public double Shortest_Way(Point_2D K, Point_2D L)
        {
            if (!(IsPointsOnLine(this.X, this.Y, K.X, K.Y, L.X, L.Y)) &&
                !(IsPointsOnLine(this.X, this.Y, K.X, K.Y, L.X + L.R, L.Y)) &&
                !(IsPointsOnLine(this.X, this.Y, K.X, K.Y, L.X, L.Y + L.R)))
                return Distance(K.X, K.Y, this.x, this.y);
            return Math.Min(Distance(K.X, K.Y, L.X, L.Y + L.R) + Distance(L.X, L.Y + L.R, this.x, this.y),
                Distance(K.X, K.Y, L.X + L.R, L.Y) + Distance(L.X + L.R, L.Y, this.x, this.y));
        }

        public bool IsPointsOnLine(double x1, double y1, double x2, double y2, double x3, double y3) // лежат ли точки на одной прямой
        {
            return x3 * (y2 - y1) - y3 * (x2 - x1) == x1 * y2 - x2 * y1;
        }


        public double Compare(Point_2D B, Point_2D C) // B - мальчик C - карлсон
        {
            if ((this.X == B.X && B.X == C.X) || (IsPointsOnLine(this.X, this.Y, B.X, B.Y, C.X, C.Y))) // лежат ли точки на одной прямой
                return 2;
            if ((C.Y > this.Y && B.X > this.X) || (C.Y < this.Y && B.X < C.X))
                return 1;
            return 3;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Point_2D B, K, L;
            string[] mas = Console.ReadLine().Split();
            int[] mas2 = new int[4];

            for (int i = 0; i < 4; i++)
            {
                mas2[i] = int.Parse(mas[i]);
            }
            B = new Point_2D(mas2[0], mas2[1]);
            K = new Point_2D(mas2[2], mas2[3]);
            string[] mas3 = Console.ReadLine().Split();
            L = new Point_2D(int.Parse(mas3[0]), int.Parse(mas3[1]));
            L.R = int.Parse(mas3[2]);
            Console.WriteLine("{0:f3}", (float)B.Shortest_Way(K, L));
        }
    }
}
