using System;

namespace Transport
{
    class Transport
    {
        protected string name; // название
        protected int spend_fuel; // трата топлива
        protected int container_fuel; // емкость топливного бака

        // конструктор по умолчанию
        public Transport()
        {
            spend_fuel = 10;
            container_fuel = 50;
        }

        // конструктор с параметрами
        public Transport(string name, int spend_fuel, int container_fuel)
        {
            this.name = name;
            this.spend_fuel = spend_fuel;
            this.container_fuel = container_fuel;
        }

        public string Name //  название
        {
            get
            {
                return name;
            }
        }

        public int Max_Len // дальность поездки
        {
            get
            {
                return container_fuel / spend_fuel;
            }
        }

        public int Need_Fuel(int length)
        {
            return (length * spend_fuel) / 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
