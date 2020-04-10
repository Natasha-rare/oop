using System;

namespace Проект_3_четверти
{
    public enum Pack
    {
        Шестерка=6, Семерка,
        Восьмерка, Девятка, Десятка, Валет, Дама, Король, Туз
    }


    class Cards
    {
        Pack[] cards = new Pack[36];
        static Random rnd = new Random();

        public Cards()
        {
            cards = Shake(cards);
        }

        public Pack[] Shake(Pack[] cards) // Заполняем/ перемешиваем массив
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = Generate_Cards(this.cards); // заполняем массив
            }
            return cards;
        }

        public void Shake()
        {
            Pack[] new_c = new Pack[36];
            for (int i = 0; i < cards.Length; i++)
            {
                new_c[i] = Generate_Cards(new_c); // заполняем массив
            }
            cards = new_c;
        }

        public Pack Generate_Cards(Pack[] cards) // генерируем карту
        {
            Pack n;
            n = (Pack)rnd.Next(6, 15);
            if (Repeat(n, cards)) return n;
            return Generate_Cards(cards);
        }

        public bool Repeat(Pack n, Pack[] cards) // проверка наличия более 4-х карт одинаковой масти
        {
            int count = 0;
            for (int i = 0; i < 36; i++)
            {
                if (n == cards[i]) count += 1;
            }
            return count < 4;
        }

        public void Print() // вывод колоды
        {
            foreach(Pack x in cards)
            {
                Console.WriteLine(Convert.ToString(x));
            }
        }

        public string this[int i]
        {
            get
            {
                if (i < 36)
                {
                    return Convert.ToString(cards[i]);
                }
                return "Такой карты не существует";
            }
        }
        private void Move_Cards() // передвигаем в начало оставшиеся 18 карт
        {
            int k = 18;
            Pack temp;
            while (k > 0)
            {
                temp = cards[0];
                for (int i = 1; i < cards.Length; i++)
                    cards[i - 1] = cards[i];
                cards[cards.Length - 1] = temp;
                k--;
            }
        }

        public Pack[] GetPacks() // забираем 18 кард из колоды
        {
            Pack[] new_cards = new Pack[36];
            for (int i = 0; i < 18; i++)
            {
                new_cards[i] = cards[i];
                cards[i] = 0;
            }
            Move_Cards();
            return new_cards;
        }
    }

    class Player
    {
        private Pack[] cards = new Pack[36]; // для избежания ощибки выхода за границы массива при передаче (розыгрыше) карт
        private int cards_left;

        public Player(Cards pack)
        {
            cards = pack.GetPacks();
            cards_left = 18;
        }

        public void Print() // вывод колоды
        {
            foreach (Pack x in cards)
            {
                Console.WriteLine(Convert.ToString(x));
            }
        }

        public void Show() // вывод карты (переворачиваем карту)
        {
            Console.WriteLine("Перевернутая карта: {0}", Convert.ToString(cards[0]));
        }

        public void Show(int n) // вывод карты (переворачиваем карту)
        {
            Console.WriteLine("Перевернутая карта: {0}", Convert.ToString(cards[n]));
        }

        public void Move_Cards() // передвигаем в начало оставшиеся карты
        {
            for (int i = 1; i < cards.Length; i++)
                cards[i - 1] = cards[i];
            cards[cards.Length - 1] = 0;
        }

        public int Cards_Left
        {
            get { return cards_left; }
            set
            {
                if (value < 36) cards_left = value;
            }
        }

        public void Give(Player player)
        {
            player[player.Cards_Left - 1] = this.cards[0];
            player.Cards_Left = player.Cards_Left + 1;
            player[player.Cards_Left - 1] = player[0];
            player.Move_Cards();
            this.cards[0] = 0;
            this.cards_left -= 1;
            Move_Cards();
        }



        public Pack this[int i]
        {
            get
            {
                return cards[i];
            }
            set
            {
                if (i < 36) cards[i] = value;
            }
        }

        public int Fight(Player player1, Pack[] pack, int n)
        {
            //Console.WriteLine("n={0}", n);

            pack[n - 1] = this.cards[n - 1];
            pack[n] = player1[n - 1];
            // Последовательно показываем карты, а затем добавляем их во временную колоду
            if (this.cards[n] == 0) return 1; // окончание колоды во время войны
            this.Show(n);
            pack[n + 1] = this.cards[n];
            if (player1[n] == 0) return 2; // окончание колоды во время войны
            player1.Show(n);
            pack[n + 2] = player1[n];
            if (this.cards[n + 1] == 0) return 1; // окончание колоды во время войны
            this.Show(1 + n);
            pack[n + 3] = this.cards[n + 1];
            if (player1[n + 1] == 0) return 2; // окончание колоды во время войны
            player1.Show(1 + n);
            pack[n + 4] = player1[n + 1];
            if ((int)player1[1 + n] > (int)this.cards[1 + n]) // Сравниваем масти карт, выложенных вторыми
            {
                Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Побеждает игрок 2. Он получает: ", pack[n + 3], pack[n + 4]);
                for (int i = 0; i < n + 2; i++)
                {
                    player1.Give(this);
                }
                foreach (Pack x in pack)
                {
                    if (x == 0) break;
                    Console.Write("{0} ", x);
                }
                Console.WriteLine();
                return 1;
            }
            if (((int)player1[1 + n] < (int)this.cards[1 + n]))
            {
                Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Побеждает игрок 1. Он получает: ", pack[n + 3], pack[n + 4]);
                for (int i = 0; i < (n + 2); i++)
                {
                    player1.Give(this);
                }
                foreach (Pack x in pack)
                {
                    if (x == 0) break;
                    Console.Write("{0} ", x);
                }
                Console.WriteLine();
                return 0;
            }

            Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Ничья. Война продолжается", cards[1 + n], player1[1 + n]);
            return Fight(player1, pack, n+= 2);
                
        }
       
    }

    class Program
    {
        static public int Game(Player player1, Player player2)
        {
            while (true)
            {
                Console.WriteLine("-----------NEXT-ROUND--------------->>>>>");
                Console.WriteLine("У первого игрока: {0} карт", player1.Cards_Left);
                Console.WriteLine("У второго игрока: {0} карт", player2.Cards_Left);
                player1.Show();
                player2.Show();

                if ((int)player1[0] > (int)player2[0])
                {
                    Console.WriteLine("Первый игрок выиграл тур и получил карты: {0}, {1}", player1[0], player2[0]);
                    player2.Give(player1);
                }
                else if ((int)player1[0] < (int)player2[0])
                {
                    Console.WriteLine("Второй игрок выиграл тур и получил карты: {0}, {1}", player1[0], player2[0]);
                    player1.Give(player2);
                }
                else
                {
                    Console.WriteLine("Карты одинакого достоинства. Начинается война");
                    Console.WriteLine("<<<<<-----------FIGHT--------------->>>>>");
                    Pack[] pack = new Pack[18];
                    int res = player1.Fight(player2, pack, 1);
                    if (res == 2)
                    {
                        Console.WriteLine("У игрока 2 закончились карты. ИГРА ОКОНЧЕНА! Победил игрок 1");
                        break;
                    }
                    if (res == 3)
                    {
                        Console.WriteLine("У игрока 1 закончились карты. ИГРА ОКОНЧЕНА! Победил игрок 2");
                        break;
                    }
                }
                if (player1.Cards_Left < 1)
                {
                    Console.WriteLine("ИГРА ОКОНЧЕНА! Победил игрок 2");
                    Console.WriteLine("Хотите сыграть еще раз? Да(1)/ нет(2)");
                    int answer = int.Parse(Console.ReadLine());
                    if (answer == 2) break;
                }

                else if (player2.Cards_Left < 1)
                {
                    Console.WriteLine("ИГРА ОКОНЧЕНА! Победил игрок 1");
                    Console.WriteLine("Хотите сыграть еще раз? Да(1)/ нет(2)");
                    int answer = int.Parse(Console.ReadLine());
                    if (answer == 2) break;
                    Cards cards = new Cards();
                    return Game(new Player(cards), new Player(cards));
                }
                else if (Console.ReadLine() == "4") return 0;
            }
            return 0;
        }

        static void Main(string[] args)
        {
            int a = -1;
            
            Console.WriteLine(@"Добро пожаловать в игру Пьяница!");
            while (true)
            {
                Console.WriteLine(@"Что вы хотите сделать?
/1 - вывести колоду 1-го игрока
/2 - вывести колоду 2-го игрока
/3 - начать игру
/4 - выйти
/5 - вывести всю колоду
/6 - перемешать колоду");
                string answer = Console.ReadLine();
                Cards cards = new Cards();
                if (answer == "5")
                {
                    cards.Print();
                    continue;
                }
                else if (answer == "6")
                {
                    cards.Shake();
                    cards.Print();
                    continue;
                }
                Player player1 = new Player(cards);
                Player player2 = new Player(cards);
                if (answer == "1")
                {
                    player1.Print();
                    continue;
                }
                else if (answer == "2")
                {
                    player2.Print();
                    continue;
                }
                else if (answer == "3")
                {
                    a = Game(player1, player2);
                    continue;
                }
                else Environment.Exit(0);
                if (a == 0)
                {
                    Console.WriteLine("Спасибо за игру!");
                    Environment.Exit(0);
                }
            }  
        }
    }
}
