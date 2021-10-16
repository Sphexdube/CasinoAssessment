using BlackjackGame.Enums;
using System;

namespace BlackjackGame.Service
{
    /// <summary>
    /// This file contains the code for the Card class.
    /// </summary>
    public class Card
    {
        private readonly Hand hand = new Hand();
        private readonly Suit suit;
        private readonly char symbol;

        public readonly Face face;

        public int value;

        /// <summary>
        /// Initilize Value and Suit Symbol
        /// </summary>
        /// 
        public Card(Suit suit, Face face)
        {
            this.suit = suit;
            this.face = face;

            switch (this.suit)
            {
                case Suit.Clubs:
                    symbol = '♣';
                    break;
                case Suit.Spades:
                    symbol = '♠';
                    break;
                case Suit.Diamonds:
                    symbol = '♦';
                    break;
                case Suit.Hearts:
                    symbol = '♥';
                    break;
            }
            switch (this.face)
            {
                case Face.Ten:
                case Face.Jack:
                case Face.Queen:
                case Face.King:
                    value = 10;
                    break;
                case Face.Ace:
                    value = 11;
                    break;
                default:
                    value = (int)this.face + 1;
                    break;
            }
        }

        /// <summary>
        /// Print out the description of the card, marking Aces as Soft or Hard.
        /// </summary>
        public void WriteDescription()
        {
            if (suit == Suit.Diamonds || suit == Suit.Hearts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (face == Face.Ace)
            {
                if (value == 11)
                {
                    Console.WriteLine(symbol + " Soft " + face + " of " + suit);
                }
                else
                {
                    Console.WriteLine(symbol + " Hard " + face + " of " + suit);
                }
            }
            else
            {
                Console.WriteLine(symbol + " " + face + " of " + suit);
            }
            hand.ResetColor();
        }
    }
}
