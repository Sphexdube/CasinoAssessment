using BlackjackGame.Enums;
using System;
using System.Collections.Generic;

namespace BlackjackGame.Services
{
    public class Hand
    {
        public int MinimumBet { get; } = 10;

        /// <param name="hand">The hand to check</param>
        /// <returns>Returns true if the hand is blackjack</returns>
        public bool IsHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].face == Face.Ace && hand[1].value == 10) return true;
                else if (hand[1].face == Face.Ace && hand[0].value == 10) return true;
            }
            return false;
        }

        /// <summary>
        /// Reset Console Colors to DarkGray on Black
        /// </summary>
        public void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
