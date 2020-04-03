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
            
            for (int i = 0; i<cards.Length; i++)
            {
                cards[i] = Generate_Cards();
            }
        }

        public Pack Generate_Cards()
        {
            Pack n;
            n = (Pack)rnd.Next(6, 15);
            if (Repeat(n, this.cards))  return n;
            return Generate_Cards();
        }
        public void Print()
        {
            
            foreach(Pack x in cards)
            {
                Console.WriteLine(Convert.ToString(x));
            }
        }

        public bool Repeat(Pack n, Pack [] cards)
        {
            int count = 0;
            foreach(Pack i in cards)
            {
                if (n == i) count += 1;
            }
            return count < 3;
        }
    }

    //class Player
    //{
    //    private int[] cards;
    //    private int number_of_cards;

    //}

    class Program
    {
        static void Main(string[] args)
        {
            Cards c = new Cards();
            c.Print();
        }
    }
}
