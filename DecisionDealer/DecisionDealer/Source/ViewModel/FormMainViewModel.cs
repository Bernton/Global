using DecisionDealer.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DecisionDealer.ViewModel
{
    public class FormMainViewModel
    {
        #region Fields

        private int _round = 0;
        private int _right = 0;

        #endregion

        #region Properties & events

        public event PercentStepComplete PercentComplete;

        public bool DisplayEquities { get; set; }
        public PokerTable Table { get; private set; }
        public HandStatistic[] HandStatistics { get; private set; }

        public string FormCaption
        {
            get
            {
                return string.Format("Decision Dealer Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString(2));
            }
        }

        #endregion

        #region Constructors

        public FormMainViewModel()
        {
            Table = new PokerTable();
        }

        #endregion

        #region Methods

        public string ShowResult(bool isCall)
        {
            if(HandStatistics == null)
            {
                throw new InvalidOperationException("HandStatistics haven't been loaded yet.");
            }

            DisplayEquities = true;

            int count = Table.Players.Count;
            double equity = HandStatistics[0].WinPercentage + HandStatistics[0].TieEquity;
            string turnout = "";
            bool overEquity = equity > 1.0 / count * 100.0;

            if (isCall && overEquity)
            {
                _right++;
                turnout = "Correct call";
            }
            else if (isCall && !overEquity)
            {
                turnout = "Wrong call";
            }
            else if (!isCall && !overEquity)
            {
                _right++;
                turnout = "Correct fold";
            }
            else
            {
                turnout = "Wrong fold";
            }

            return string.Format("{0}: {1} % (Average {2} %)", turnout, Math.Round(equity, 2), Math.Round(1.0 / count * 100.0, 2));
        }

        public string ResetTable(string frequencyTextInput, Action actionAfterReset)
        {
            HandStatistics = null;

            string result = "";

            int frequencyInput;

            if (int.TryParse(frequencyTextInput, out frequencyInput))
            {
                Table.ShowFrequency = frequencyInput;
            }

            if (_round != 0)
            {
                result = string.Format("Hands correct: {0} % ({1}/{2})", Math.Round((double)_right / _round * 100.0, 1), _right, _round);
            }

            _round++;

            Table.ResetTable();

            Random random = new Random();
            int playerAmount = random.Next(3, 11);

            for (int i = 1; i < playerAmount; i++)
            {
                PokerPlayer player = new PokerPlayer();

                if (i == 1)
                {
                    Table.SeatPlayer(player, i);
                }
                else
                {
                    Table.SeatPlayer(player);
                }
            }

            Table.DealHoleCards();

            PokerSimulationEngine simulator = new PokerSimulationEngine(100000);
            simulator.PercentComplete += OnPercentComplete;
            PokerPlayer[] players = Table.Players.Where(c => c != null).ToArray();
            Card[,] holeCards = new Card[players.Length, 2];

            for (int i = 0; i < players.Length; i++)
            {
                Card[] clonedCards = (Card[])players[i].HoleCards.Clone();
                holeCards[i, 0] = clonedCards[0];
                holeCards[i, 1] = clonedCards[1];
            }

            Task.Factory.StartNew(() =>
            {
                HandStatistics = simulator.Simulate(holeCards);

                EntryPoint.SetCaption(0, "GetHandValue()");
                EntryPoint.ReportSections();

                if (actionAfterReset != null)
                {
                    actionAfterReset();
                }
            });

            return result;
        }

        private void OnPercentComplete(int percentComplete, int iterationsComplete)
        {
            PercentComplete?.Invoke(percentComplete, iterationsComplete);
        }

        #endregion
    }
}
