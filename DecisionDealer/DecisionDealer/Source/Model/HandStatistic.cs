namespace DecisionDealer.Model
{
    public class HandStatistic
    {
        public double WinPercentage
        {
            get
            {
                return (double)Wins / (double)SampleSize * 100.0;
            }
        }

        public double TiePercentage
        {
            get
            {
                return (double)Ties / (double)SampleSize * 100.0;
            }
        }

        public double TieEquity
        {
            get
            {
                return (double)TiePercentage / TieSplit;
            }
        }

        public double Equity
        {
            get
            {
                return WinPercentage + TieEquity;
            }
        }

        public int SampleSize { get; set; }
        public int Wins { get; set; }
        public int Ties { get; set; }
        public double TieSplit { get; set; } = 1;

        public HandStatistic()
        {
            SampleSize = 0;
            Wins = 0;
            Ties = 0;
        }

        public HandStatistic(int sampleSize, int wins, int ties)
        {
            SampleSize = sampleSize;
            Wins = wins;
            Ties = ties;
        }
    }
}
