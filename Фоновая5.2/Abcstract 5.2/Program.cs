using System;

namespace Abcstract_5._2
{
    class Program
    {
        abstract class Transport
        {
            public string name;
            public int mass;
            public int extra;
            public int capacity;
            public int massGoods;


            public Transport(string name, int mass, int extra, int capacity, int massGoods)
            {
                this.name = name;
                this.mass = mass;
                this.extra = extra;
                this.capacity = capacity;
                this.massGoods = massGoods;
            }

            string Name
            {
                get; set;
            }

            int FullMass
            {
                get; set;
            }

            int MassGoods
            {
                get; set;
            }

            void Extra() { }

        }

        class Wheel : Transport
        {
            int wheelsCount; // количество колес
            int typeP; //тип привода
            public Wheel(string name, int mass, int extra, int capacity, int massGoods, int wheelsCount, int typeP) 
                : base(name, mass, extra, capacity, massGoods)
            {
                this.wheelsCount = wheelsCount;
                this.typeP = typeP;
            }

            string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            int FullMass
            {
                get {
                    return mass + massGoods;
                }
                set
                {
                    if (value < capacity)
                    {
                        massGoods = value;
                    }
                }
            }

            int MassGoods
            {
                get
                {
                    return massGoods;
                }
            }

            void Extra() { Console.WriteLine(typeP * 2 * wheelsCount - massGoods); }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
