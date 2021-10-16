using BlackjackGame.Services;
using System.Collections.Generic;

namespace BlackjackGame.Models
{
    public static class PlayerModel
    {
        public static int Chips { get; set; } = 500;
        public static int Bet { get; set; }
        public static int Wins { get; set; }
        public static int HandsCompleted { get; set; } = 1;
        public static List<Card> Hand { get; set; }
    }
}
