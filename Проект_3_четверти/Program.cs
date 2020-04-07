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
            Pack[] new_cards = new Pack[18];
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

        private void Move_Cards() // передвигаем в начало оставшиеся карты
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
            int i;
            for (i = 0; i < 36; i++)
            {
                if (player.cards[i] == 0) break;
            }
            player.Cards_Left = player.Cards_Left + 1;
            player.cards[i] = this.cards[0];
            this.cards_left -= 1;
            Move_Cards();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cards cards = new Cards();
            cards.Print();
            Console.WriteLine("-----------------------------------------------");
            Player player1 = new Player(cards);
            player1.Print();
            Console.WriteLine("-----------------------------------------------");
            cards.Print();
            Player player2 = new Player(cards);
            player2.Print();
            cards.Print();

        }
    }
}
