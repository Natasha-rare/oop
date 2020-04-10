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
                cards[i] = Generate_Cards(); // заполняем массив
            }
            return cards;
        }

        public Pack Generate_Cards() // генерируем карту
        {
            Pack n;
            n = (Pack)rnd.Next(6, 15);
            if (Repeat(n, this.cards)) return n;
            return Generate_Cards();
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
            //player[0] = 0;
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

        public int Fight(Player player1, int n)
        {
            if (this.cards[n + 1] == 0 || this.cards[n] == 0) return 1;
            if (player1[1 + n] == 0 || player1[n] == 0) return 2;
            this.Show(n);
            player1.Show(n);
            this.Show(1 + n);
            player1.Show(1 + n);
            if ((int)player1[1 + n] > (int)this.cards[1 + n])
            {
                Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Побеждает игрок 2. Он получает {0} {1} {2} {3}", cards[1 + n], player1[1 + n], cards[n], player1[n]);
                return 1;
            }
            if (((int)player1[1 + n] < (int)this.cards[1 + n]))
            {
                Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Побеждает игрок 1. Он получает {0} {1} {2} {3}", cards[1 + n], player1[1 + n], cards[n], player1[n]);
                return 0;
            }

            Console.WriteLine(@"Первый игрок выложил: закрытая и {0}
второй игрок: закрытая и {1}
Ничья. Война продолжается", cards[1 + n], player1[1 + n]);
            return Fight(player1, n++);
                
        }
       
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cards cards = new Cards();
            Player player1 = new Player(cards);
            player1.Print();
            Player player2 = new Player(cards);
            while(player1.Cards_Left != 0 && player2.Cards_Left != 0)
            {
                Console.WriteLine("У первого игрока: {0} карт", player1.Cards_Left);
                Console.WriteLine("У второго игрока: {0} карт", player2.Cards_Left);
                player1.Show();
                //player1.Print();
                //Console.WriteLine("-----");
                player2.Show();
                //player2.Print();
                //Console.WriteLine("-----------------------");
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
                else {
                    Console.WriteLine("Карты одинакого достоинства. Начинается война");
                    if (player1.Fight(player2, 1) == 0)
                    {
                        player2.Give(player1);
                        player2.Give(player1);
                        player2.Give(player1);
                        continue;
                    }
                    if (player1.Fight(player2, 1) == 1)
                    {
                        player1.Give(player2);
                        player1.Give(player2);
                        player1.Give(player2);
                        continue;

                    }
                    if (player1.Fight(player2, 1) == 2)
                    {
                        Console.WriteLine("ИГРА ОКОНЧЕНА! Победил игрок 1");
                        break;
                    }
                    if (player1.Fight(player2, 1) == 3)
                    {
                        Console.WriteLine("ИГРА ОКОНЧЕНА! Победил игрок 2");
                        break;
                    }

                }
                Console.ReadKey();
            }
            //cards.Print();
            //Console.WriteLine("-----------------------------------------------");
            //player1.Print();
            //Console.WriteLine("-----------------------------------------------");
            //cards.Print();
            //player2.Print();
            //cards.Print();

        }
    }
}
