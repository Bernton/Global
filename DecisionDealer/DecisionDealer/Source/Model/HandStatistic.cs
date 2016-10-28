namespace DecisionDealer.Model
{
    public class HandStatistic
    {
        #region Properties

        public int SampleSize { get; set; }
        public int Wins { get; set; }
        public int Ties { get; set; }
        public double TieSplit { get; set; } = 1;

        public double WinPercentage
        {
            get
            {
                return Wins / (double)SampleSize * 100.0;
            }
        }

        public double TiePercentage
        {
            get
            {
                return Ties / (double)SampleSize * 100.0;
            }
        }

        public double TieEquity
        {
            get
            {
                return TiePercentage / TieSplit;
            }
        }

        public double Equity
        {
            get
            {
                return WinPercentage + TieEquity;
            }
        }

        #endregion

        #region Constructors

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

        #endregion
    }
}
