using System.Collections.Generic;

namespace BerldPokerServer.Poker
{
    public class Pot
    {
        public int Chips { get; set; } = 0;
        public List<int> IndexexToWinFor { get; set; } = new List<int>();

        public Pot()
        {

        }
    }
}
