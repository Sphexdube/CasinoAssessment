using BlackjackGame.Enums;
using BlackjackGame.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackjackGame.Service
{
    public class Casino
    {
        private readonly Deck deck;
        private readonly Player player;
        private readonly Dealer dealer;
        private readonly Hand hand;

        public Casino()
        {
            deck = new Deck();
            player = new Player();
            dealer = new Dealer();
            hand = new Hand();
        }

        /// <summary>
        /// Initialize Deck, deal the player and dealer hands, and display them.
        /// </summary>
        void InitializeHands()
        {
            deck.Initialize();

            PlayerModel.Hand = deck.DealHand();
            DealerModel.HiddenCards = deck.DealHand();
            DealerModel.RevealedCards = new List<Card>();

            // If hand contains two aces, make one Hard.
            if (PlayerModel.Hand[0].face == Face.Ace && PlayerModel.Hand[1].face == Face.Ace)
            {
                PlayerModel.Hand[1].value = 1;
            }

            if (DealerModel.HiddenCards[0].face == Face.Ace && DealerModel.HiddenCards[1].face == Face.Ace)
            {
                DealerModel.HiddenCards[1].value = 1;
            }

            dealer.RevealCard();

            player.WriteHand();
            dealer.WriteHand();
        }

        /// <summary>
        /// Handles everything for the round.
        /// </summary>
        public void StartRound()
        {
            Console.Clear();

            if (!TakeBet())
            {
                EndRound(Result.InvalidBet);
                return;
            }
            Console.Clear();

            InitializeHands();
            TakeActions();

            dealer.RevealCard();

            Console.Clear();
            player.WriteHand();
            dealer.WriteHand();

            PlayerModel.HandsCompleted++;

            if (PlayerModel.Hand.Count == 0)
            {
                EndRound(Result.Surrender);
                return;
            }
            else if (player.GetHandValue() > 21)
            {
                EndRound(Result.PlayerBusts);
                return;
            }

            while (dealer.GetHandValue() <= 16)
            {
                Thread.Sleep(1000);
                DealerModel.RevealedCards.Add(deck.DrawCard());

                Console.Clear();
                player.WriteHand();
                dealer.WriteHand();
            }


            if (player.GetHandValue() > dealer.GetHandValue())
            {
                PlayerModel.Wins++;
                if (hand.IsHandBlackjack(PlayerModel.Hand))
                {
                    EndRound(Result.PlayerBlackjack);
                }
                else
                {
                    EndRound(Result.PlayerWins);
                }
            }
            else if (dealer.GetHandValue() > 21)
            {
                PlayerModel.Wins++;
                EndRound(Result.PlayerWins);
            }
            else if (dealer.GetHandValue() > player.GetHandValue())
            {
                EndRound(Result.DealerWins);
            }
            else
            {
                EndRound(Result.Draw);
            }
        }

        /// <summary>
        /// Ask user for action and perform that action until they stand, double, or bust.
        /// </summary>
        void TakeActions()
        {
            string action;
            do
            {
                Console.Clear();
                player.WriteHand();
                dealer.WriteHand();

                Console.Write("Enter Action (? for help): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                action = Console.ReadLine();
                hand.ResetColor();

                switch (action.ToUpper())
                {
                    case "HIT":
                        PlayerModel.Hand.Add(deck.DrawCard());
                        break;
                    case "STAND":
                        break;
                    case "SURRENDER":
                        PlayerModel.Hand.Clear();
                        break;
                    case "DOUBLE":
                        if (PlayerModel.Chips <= PlayerModel.Bet)
                        {
                            player.AddBet(PlayerModel.Chips);
                        }
                        else
                        {
                            player.AddBet(PlayerModel.Bet);
                        }
                        PlayerModel.Hand.Add(deck.DrawCard());
                        break;
                    default:
                        Console.WriteLine("Valid Moves:");
                        Console.WriteLine("Hit, Stand, Surrender, Double");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;
                }

                if (player.GetHandValue() > 21)
                {
                    foreach (Card card in PlayerModel.Hand)
                    {
                        if (card.value == 11) // Only a soft ace can have a value of 11
                        {
                            card.value = 1;
                            break;
                        }
                    }
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE")
                && !action.ToUpper().Equals("SURRENDER") && player.GetHandValue() <= 21);
        }

        /// <summary>
        /// Take player's bet
        /// </summary>
        /// <returns>Was the bet valid</returns>
        bool TakeBet()
        {
            Console.Write("Current Chip Count: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(PlayerModel.Chips);
            hand.ResetColor();

            Console.Write("Minimum Bet: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(hand.MinimumBet);
            hand.ResetColor();

            Console.Write("Enter bet to begin hand " + PlayerModel.HandsCompleted + ": ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string s = Console.ReadLine();
            hand.ResetColor();

            if (Int32.TryParse(s, out int bet) && bet >= hand.MinimumBet && PlayerModel.Chips >= bet)
            {
                player.AddBet(bet);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Perform action based on result of round and start next round.
        /// </summary>
        /// <param name="result">The result of the round</param>
        void EndRound(Result result)
        {
            switch (result)
            {
                case Result.Draw:
                    player.ReturnBet();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Player and Dealer Push.");
                    break;
                case Result.PlayerWins:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Player Wins " + player.WinBet(false) + " chips");
                    break;
                case Result.PlayerBusts:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player Busts");
                    break;
                case Result.PlayerBlackjack:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Player Wins " + player.WinBet(true) + " chips with Blackjack.");
                    break;
                case Result.DealerWins:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dealer Wins.");
                    break;
                case Result.Surrender:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player Surrenders " + (PlayerModel.Bet / 2) + " chips");
                    PlayerModel.Chips += PlayerModel.Bet / 2;
                    player.ClearBet();
                    break;
                case Result.InvalidBet:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Bet.");
                    break;
            }

            if (PlayerModel.Chips <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine();
                Console.WriteLine("You ran out of Chips after " + (PlayerModel.HandsCompleted - 1) + " rounds.");
                Console.WriteLine("500 Chips will be added and your statistics have been reset.");

                //player = new Player();
            }

            hand.ResetColor();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            StartRound();
        }
    }
}
