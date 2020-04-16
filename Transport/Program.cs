using System;

namespace Transport
{
    enum Carcase // тип кузова
    {
        Седан=0, Купе, Хэчбек, Универсал, Кабриолет
    }


    class Transport
    {
        protected string name; // название
        protected int spend_fuel; // расход топлива
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

        public int Need_Fuel(int length) // количество топлива на расстояние
        {
            return (length * spend_fuel) / 100;
        }

        public void Print()
        {
            Console.WriteLine(@"Название: {0}
Расход топлива: {1}
Вместительность бака: {2}", name, spend_fuel, container_fuel);
        }
    }


    class PassengerCar: Transport // Легковой Автомобиль
    {
        private Carcase body; //тип кузова
        private int passengers; // количество пассажиров в машине
        static int[] max_pas = { 5, 5, 5, 5, 3 };


        public PassengerCar() // конструктор по умолчанию
            :base()
        {
            body = Carcase.Седан;
            passengers = 2;
        }

        public PassengerCar(Carcase body, int passengers, string name, int spend, int container) // конструктор с параметрами
            :base(name, spend, container)
        {
            this.body = body;
            this.passengers = passengers;
        }

        //public int Percent(int people) // процент загрузки
        //{
        //    return people / passengers * 100;
        //}

        public double Percent
        {
            get { return passengers / (double)max_pas[(int)body] * 100; }
        }


        new public double Need_Fuel(int length)
        {
            return (length * spend_fuel * (1 -this.Percent / 100)) / 100;
        }

        public int Passengers
        {
            get { return passengers; }
            set
            {
                try
                {
                    if (value <= max_pas[(int)body]) passengers = value;
                    else
                        throw new Exception("Введенное количество пассажиров " +
                            "превышает возможное кол-во");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("***\nАвтомобиль\n***");
            base.Print();
            Console.WriteLine(@"Тип кузова: {0}
Кол-во пассажиров: {1}
Заполненность автомобиля: {2:f1}%", Convert.ToString(body), passengers, Percent);
        }

    }

    class Lorry : Transport // Грузовик
    {
        private int capacity; // Грузоподъемность
        private int weight; // Масса груза

        public Lorry()
            : base()
        {
            capacity = 100;
            weight = 90;
        }

        public Lorry(int capacity, int weight, string name, int spend, int container)
            :base(name, spend, container)
        {
            try
            {
                if (weight > capacity) throw new Exception("Вес больше грузоподъемности. " +
                    "Он уменьшается на 10 кг");
                this.weight = weight;
                this.capacity = capacity;
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка: {0}", error.Message);
            }
        }

        public double Percent
        {
            get { return weight / (double)capacity * 100; }
        }

        new public double Need_Fuel(int length)
        {
            return (length * spend_fuel * (1 - this.Percent / 100) / 100);
        }

        public int Weight
        {

            get { return weight; }
            set
            {
                try
                {
                    if (value <= capacity) capacity = value;
                    else
                        throw new Exception("Введенная масса груза превышает грузоподъемность");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("***\nГрузовик\n***");
            base.Print();
            Console.WriteLine(@"Грузоподъемность: {0}
Масса груза: {1}
Загруженность автомобиля: {2:f1}%", capacity, weight, Percent);
        }

    }

    class Bus : Transport
    {
        private int passengers; // Количество перевозимых пассажиров
        private int price; // Стоимость проезда (за одного пассажира)
        static private int max_pas = 60; // максимальная загруженность

        public Bus()
            :base()
        {
            passengers = 1;
            price = 20;
        }

        public Bus(int passengers, int price, string name, int spend, int container)
            :base(name, spend, container)
        {
            this.passengers = passengers;
            this.price = price;
        }

        public double Percent
        {
            get { return passengers / (double)max_pas * 100; }
        }

        public int Money // выручка
        {
            get { return passengers * price; }
        }

        new public double Need_Fuel(int length)
        {
            return (length * spend_fuel * (1 - this.Percent / 100) / 100);
        }

        public void Print()
        {
            Console.WriteLine("***\nАвтобус\n***");
            base.Print();
            Console.WriteLine(@"Цена проезда: {0}
Кол-во пассажиров: {1}
Загруженность автобуса: {2:f1}%", price, passengers, Percent);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            string [] car_names = {"Audi", "BMW", "KIA"};
            string[] lorry_names = { "КАМАЗ", "ГАЗ", "МАЗ" };
            string[] bus_names = { "KIA", "ГАЗ", "BAW" };

            Lorry [] lorries = new Lorry[3];
            PassengerCar[] cars = new PassengerCar[3];
            Bus[] buses = new Bus[3];
            Carcase a = Carcase.Седан;

            for (int i=0; i < 3; i++)
            {
                cars[i] = new PassengerCar(a, (i + 2), car_names[i], (i + 1) * 10, (i + 1) * 11);
                lorries[i] = new Lorry((i + 1) * 350, (i + 1) * 200 + 10, lorry_names[i], (i + 3) * 10, (i + 3) * 12);
                buses[i] = new Bus(i + 5, 30 + (i + 1) * 5, bus_names[i], (i + 2) * 10, (i + 2) * 11);
            }

            for (int j = 0; j < 3; j++)
            {
                cars[j].Print();
                Console.WriteLine("Количество топлива на 100 км: {0}", cars[j].Need_Fuel(100));
                Console.WriteLine();
                lorries[j].Print();
                Console.WriteLine("Количество топлива на 100 км: {0}", lorries[j].Need_Fuel(100));
                Console.WriteLine();
                buses[j].Print();
                Console.WriteLine("Количество топлива на 100 км: {0}", buses[j].Need_Fuel(100));
                Console.WriteLine("\n");
            }

        }
    }
}
