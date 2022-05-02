using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Card
    {
        public enum Suit
        {
            DIAMOND,
            CLUB,
            HEART,
            SPADE
        }
        public int value;
        public string name;
        public Suit suit;

        public Card(int value, string name, Suit suit)
        {
            this.value = value;
            this.name = name;
            this.suit = suit;
        }
    }
}
