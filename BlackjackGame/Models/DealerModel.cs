using BlackjackGame.Service;
using System.Collections.Generic;

namespace BlackjackGame.Models
{
    public static class DealerModel
    {
        public static List<Card> HiddenCards { get; set; } = new List<Card>();
        public static List<Card> RevealedCards { get; set; } = new List<Card>();
    }
}
