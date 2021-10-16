using BlackjackGame.Models;
using BlackjackGame.Services;
using System;

namespace BlackjackGame
{
    public class Player
    {
        private readonly Hand hand;

        public Player()
        {
            hand = new Hand();
        }

        /// <summary>
        /// Add Player's chips to their bet.
        /// </summary>
        /// <param name="bet">The number of Chips to bet</param>
        public void AddBet(int bet)
        {
            PlayerModel.Bet += bet;
            PlayerModel.Chips -= bet;
        }

        /// <summary>
        /// Set Bet to 0
        /// </summary>
        public void ClearBet()
        {
            PlayerModel.Bet = 0;
        }

        /// <summary>
        /// Cancel player's bet. They will neither lose nor gain any chips.
        /// </summary>
        public void ReturnBet()
        {
            PlayerModel.Chips += PlayerModel.Bet;
            ClearBet();
        }

        /// <summary>
        /// Give player chips that they won from their bet.
        /// </summary>
        /// <param name="blackjack">If player won with blackjack, player wins 1.5 times their bet</param>
        public int WinBet(bool blackjack)
        {
            int chipsWon;
            if (blackjack)
            {
                chipsWon = (int)Math.Floor(PlayerModel.Bet * 1.5);
            }
            else
            {
                chipsWon = PlayerModel.Bet * 2;
            }

            PlayerModel.Chips += chipsWon;
            ClearBet();
            return chipsWon;
        }

        /// <returns>
        /// Value of all cards in Hand
        /// </returns>
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in PlayerModel.Hand)
            {
                value += card.value;
            }
            return value;
        }

        /// <summary>
        /// Write player's hand to console.
        /// </summary>
        public void WriteHand()
        {
            // Write Bet, Chip, Win, Amount with color, and write Round #
            Console.Write("Bet: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(PlayerModel.Bet + "  ");
            hand.ResetColor();
            Console.Write("Chips: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(PlayerModel.Chips + "  ");
            hand.ResetColor();
            Console.Write("Wins: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(PlayerModel.Wins);
            hand.ResetColor();
            Console.WriteLine("Round #" + PlayerModel.HandsCompleted);

            Console.WriteLine();
            Console.WriteLine("Your Hand (" + GetHandValue() + "):");
            foreach (Card card in PlayerModel.Hand)
            {
                card.WriteDescription();
            }
            Console.WriteLine();
        }
    }
}
