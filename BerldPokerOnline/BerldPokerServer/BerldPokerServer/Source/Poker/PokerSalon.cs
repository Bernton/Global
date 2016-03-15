using System.Collections.Generic;

namespace BerldPokerServer.Poker
{
    public static class PokerSalon
    {
        public static List<PokerTable> Tables { get; } = new List<PokerTable>();

        public static List<PokerPlayer> Players
        {
            get
            {
                List<PokerPlayer> players = new List<PokerPlayer>();

                foreach (PokerTable table in Tables)
                {
                    players.AddRange(table.Players);
                }

                return players;
            }
        }
    }
}
