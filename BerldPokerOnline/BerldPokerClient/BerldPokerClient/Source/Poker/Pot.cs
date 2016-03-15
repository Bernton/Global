using System.Collections.Generic;

namespace BerldPokerClient.Poker
{
    public class Pot
    {
        public int Chips { get; set; } = 0;
        public List<int> IndexexToWinFor { get; set; } = new List<int>();
    }
}
