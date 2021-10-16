using BlackjackGame.Models;
using BlackjackGame.Services;
using System;

namespace BlackjackGame
{
    public class Dealer
    {
        private readonly Hand hand;

        public Dealer()
        {
            hand = new Hand();
        }

        /// <summary>
        /// Take the top card from HiddenCards, remove it, and add it to RevealedCards.
        /// </summary> 
        public void RevealCard()
        {
            DealerModel.RevealedCards.Add(DealerModel.HiddenCards[0]);
            DealerModel.HiddenCards.RemoveAt(0);
        }

        /// <returns>
        /// Value of all cards in RevealedCards
        /// </returns>
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in DealerModel.RevealedCards)
            {
                value += card.value;
            }
            return value;
        }

        /// <summary>
        /// Write Dealer's RevealedCards to Console.
        /// </summary>
        public void WriteHand()
        {
            Console.WriteLine("Dealer's Hand (" + GetHandValue() + "):");
            foreach (Card card in DealerModel.RevealedCards)
            {
                card.WriteDescription();
            }
            for (int i = 0; i < DealerModel.HiddenCards.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("<hidden>");
                hand.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
