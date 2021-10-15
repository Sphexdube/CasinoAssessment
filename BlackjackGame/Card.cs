using BlackjackGame.Enums;
using System;

namespace BlackjackGame
{
    /// <summary>
    /// This file contains the code for the Card class.
    /// </summary>
    public class Card
    {
        public Suit Suit { get; }
        public Face Face { get; }
        public int Value { get; set; }
        public char Symbol { get; }

        /// <summary>
        /// Initilize Value and Suit Symbol
        /// </summary>
        /// 
        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;

            switch (Suit)
            {
                case BlackjackGame.Enums.Suit.Clubs:
                    Symbol = '♣';
                    break;
                case BlackjackGame.Enums.Suit.Spades:
                    Symbol = '♠';
                    break;
                case BlackjackGame.Enums.Suit.Diamonds:
                    Symbol = '♦';
                    break;
                case BlackjackGame.Enums.Suit.Hearts:
                    Symbol = '♥';
                    break;
            }
            switch (Face)
            {
                case BlackjackGame.Enums.Face.Ten:
                case BlackjackGame.Enums.Face.Jack:
                case BlackjackGame.Enums.Face.Queen:
                case BlackjackGame.Enums.Face.King:
                    Value = 10;
                    break;
                case BlackjackGame.Enums.Face.Ace:
                    Value = 11;
                    break;
                default:
                    Value = (int)Face + 1;
                    break;
            }
        }

        /// <summary>
        /// Print out the description of the card, marking Aces as Soft or Hard.
        /// </summary>
        public void WriteDescription()
        {
            if (Suit == Suit.Diamonds || Suit == Suit.Hearts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (Face == BlackjackGame.Enums.Face.Ace)
            {
                if (Value == 11)
                {
                    Console.WriteLine(Symbol + " Soft " + Face + " of " + Suit);
                }
                else
                {
                    Console.WriteLine(Symbol + " Hard " + Face + " of " + Suit);
                }
            }
            else
            {
                Console.WriteLine(Symbol + " " + Face + " of " + Suit);
            }
            Casino.ResetColor();
        }
    }
}
