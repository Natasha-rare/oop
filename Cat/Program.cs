using System;

namespace Cat
{
    class Cat
    {
        private int age;
        private int health;
        private int happy;

        public Cat()
        {
            age = 1;
            health = happy = 50;
        }

        public static Cat Create(int age, int health, int happy)
        {
            try
            {
                if (age <= 0 || health <= 0 || happy <= 0 || health > 100 || happy > 100)
                    throw new Exception(@"Показатели должы быть положительными, а здоровье и счастье должны быть меньше 100%.
Создается кот 1 года с показателями здоровья и счастья 50%.");
                return new Cat(age, health, happy);
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка: {0}", error.Message);
                return new Cat();
            }
        }

        private Cat(int age, int health, int happy)
        {
            this.age = age;
            this.health = health;
            this.happy = happy;
        }

        public int Age
        {
            get { return age; }

            set
            {
                try
                {
                    if (value >= 0) age = value;
                    else throw new Exception("Присваимое значение должно быть не отрицательным");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                try
                {
                    if (value >= 0) health = value;
                    else throw new Exception("Присваимое значение должно быть не отрицательным");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public int Happy
        {
            get { return happy; }
            set
            {
                try
                {
                    if (value >= 0) happy = value;
                    else throw new Exception("Присваимое значение должно быть не отрицательным");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка: {0}", error.Message);
                }
            }
        }

        public void Eat(int food)
        {
            happy += (int)food / 100 * 40;
            health += (int)food / 100 * 75;
            if (health > 100)
            {
                Console.WriteLine("^˵◕ω◕˵^ Котик слишком хорошо себя чувствует. Он вырос на 1 год");
                health = 50;
                age += 1;
            }

            if (happy > 100)
            {
                Console.WriteLine("^˵◕ω◕˵^ Котик очень счастлив. Это делает его здоровее еще на 10%");
                health += 10;
                happy = 50;
            }
        }

        public void Play(int minutes)
        {
            happy += minutes;
            if (minutes > 30) health -= (minutes - 30);
            if (health <= 10) Console.WriteLine("Ой, ой, ой умирает котик мой (×﹏×)");

            if (happy > 100)
            {
                Console.WriteLine("^˵◕ω◕˵^ Котик очень счастлив. Это делает его здоровее еще на 10%");
                health += 10;
                happy = 50;
            }

            if (health > 100)
            {
                Console.WriteLine("^˵◕ω◕˵^ Котик слишком хорошо себя чувствует. Он вырос на 1 год");
                health = 50;
                age += 1;
            }
        }

        public int Beauty
        {
            get
            {
                return (int)(health + happy + (50 - age)) / 3;
            }
        }

        public void Run(int metres)
        {
            happy -= metres / 2;
            if (metres < 200) health += metres / 10;
            else health -= (metres - 200) / 10;

            if (health > 100)
            {
                Console.WriteLine("^˵◕ω◕˵^ Котик слишком хорошо себя чувствует. Он вырос на 1 год");
                health = 50;
                age += 1;
            }

            if (happy <= 0) Console.WriteLine("Ой, ой, ой котику очень грустно(×﹏×). Покорми его или поиграй с ним");

        }

        public void Print()
        {
            Console.WriteLine("Котику {0} лет он здоров на {1}% и счастлив на {2}%", age, health, happy);
        }

        public void Play(Cat cat2)
        {
            this.Happy = this.Happy + 10;
            cat2.Happy = cat2.Happy + 10;
            if (this.Health > cat2.Health)
            {
                cat2.Health = cat2.Health + 10;
                this.Health = this.Health - 10;
                Console.WriteLine("Первый котик здоровее второго. Он отдал 10% своего здоровья второму");
            }
            else
            {
                cat2.Health = cat2.Health - 10;
                this.Health = this.Health + 10;
                Console.WriteLine("Второй котик здоровее первого. Он отдал 10% своего здоровья первому");
            }
        }

        class Program
        {
            static Cat Create_Cat()
            {
                Console.WriteLine("Вы хотите самостоятельно ввести показатели кота (1)\nили создать кота по умолчанию (2)?");
                byte answer;
                // Просим выбрать конструктор объекта
                do
                {
                    Console.WriteLine("Введите выбранный вариант (1 или 2)");
                    answer = byte.Parse(Console.ReadLine());
                }
                while (answer != 1 && answer != 2);
                int age, health, happy;
                string s;
                if (answer == 1)
                {
                    // Вводим координаты точки
                    Console.WriteLine("Введите показатели котика");

                    do
                    {
                        Console.Write("age=");
                        s = Console.ReadLine();
                    } while (!int.TryParse(s, out age));

                    do
                    {
                        Console.Write("health=");
                        s = Console.ReadLine();
                    } while (!int.TryParse(s, out health));

                    do
                    {
                        Console.Write("happy=");
                        s = Console.ReadLine();
                    } while (!int.TryParse(s, out happy));
                    return Cat.Create(age, health, happy);
                }
                return new Cat();
            }


            static void Main(string[] args)
            {
                Cat cat = Create_Cat();
                cat.Print();

                cat.Play(40);
                cat.Run(100);
                cat.Eat(50);
                Console.WriteLine("Котик поиграл 40 минут, пробежал 100 метров и съел 50 грамм корма");
                cat.Print();

                Console.WriteLine("Котик красив на {0}%", cat.Beauty);

                Cat cat2 = Create_Cat();
                cat.Play(cat2);
                cat.Print();
                cat2.Print();

            }
        }
    }
}
