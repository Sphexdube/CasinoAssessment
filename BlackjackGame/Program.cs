using BlackjackGame.Services;
using System;

namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Casino casino = new Casino();
            Hand hand = new Hand();

            hand.ResetColor();
            Console.Title = "♠♥♣♦ Welcome to Blackjack";
            casino.StartRound();
        }
    }
}