using DecisionDealer.Model;
using DecisionDealer.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace DecisionDealer.View
{
    public partial class FormMain : Form
    {
        private Random _rnd = new Random();
        private PokerTable _table = new PokerTable();
        private double _canvasRatio = Resources.Table.Width / (double)Resources.Table.Height;

        private bool _displayResults = false;
        //private int[] _winners = null;

        private int round = 0;
        private int right = 0;

        private HandStatistic[] _handStats = null;

        private int[] _coords = new int[]
        {
            333, 411,
            176, 400,
            130, 253,
            240, 154,
            427, 151,
            600, 151,
            778, 154,
            886, 263,
            848, 395,
            692, 411
        };

        public FormMain()
        {
            InitializeComponent();
            Text = string.Format("Decision Dealer Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString(2));

            ResetTable();
        }

        public void ResetTable()
        {
            int freqInput;

            if (int.TryParse(_textBoxFreq.Text, out freqInput))
            {
                _table.ShowFrequency = freqInput;
            }

            if (round != 0)
            {
                _labelCorrect.Text = string.Format("Hands correct: {0} %", Math.Round((double)right / round * 100.0), 1);
            }

            round++;

            _table.ResetTable();

            for (int i = 0; i < _rnd.Next(2, _table.Players.Length); i++)
            {
                PokerPlayer player = new PokerPlayer();

                if (i == 0)
                {
                    player.RevealedCards = true;
                    _table.SeatPlayer(player, i);
                }
                else
                {
                    player.RevealedCards = false;
                    _table.SeatPlayer(player);
                }
            }

            _labelResult.Text = "";
            _table.DealHoleCards();
            //_winners = null;
            _panelCanvas.Invalidate();

            PokerSimulationEngine simulator = new PokerSimulationEngine(60000);
            PokerPlayer[] players = _table.Players.Where(c => c != null).ToArray();
            Card[][] holeCards = new Card[players.Length][];

            for (int i = 0; i < players.Length; i++)
            {
                holeCards[i] = (Card[])players[i].HoleCards.Clone();
            }

            Task.Factory.StartNew(() =>
            {
                _handStats = simulator.Simulate(holeCards);

                EntryPoint.SetCaption(0, "GetHandValue()");
                EntryPoint.ReportSections();

                Invoke((MethodInvoker)delegate
                {
                    _buttonCall.Enabled = true;
                    _buttonFold.Enabled = true;
                });
            });
        }

        private void OnPanelCanvasPaint(object sender, PaintEventArgs e)
        {
            DrawPokerTable(e.Graphics);
        }

        private void DrawPokerTable(Graphics g)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            double sizeFactor = _panelCanvas.Width / (double)Resources.Table.Width;
            int playerCount = 0;

            Font resultFont = new Font("Arial", Round(18 * sizeFactor));
            string result;

            for (int i = 0; i < _table.Players.Length; i++)
            {
                if (_table.Players[i] != null)
                {


                    Image firstCardImage;
                    Image secondCardImage;

                    if (_table.Players[i].HoleCards[0] == null)
                    {
                        firstCardImage = CardImageProvider.GetCardBack();
                    }
                    else
                    {
                        firstCardImage = CardImageProvider.GetCard(_table.Players[i].HoleCards[0]);
                    }

                    if (_table.Players[i].HoleCards[1] == null)
                    {
                        secondCardImage = CardImageProvider.GetCardBack();
                    }
                    else
                    {
                        secondCardImage = CardImageProvider.GetCard(_table.Players[i].HoleCards[1]);
                    }

                    Point seatPosition = GetAbsoluteSeatPosition(i);
                    Size cardSize = new Size(Round(firstCardImage.Width * sizeFactor * 0.5), Round(firstCardImage.Height * sizeFactor * 0.5));

                    g.DrawImage(firstCardImage, seatPosition.X - cardSize.Width, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);
                    g.DrawImage(secondCardImage, seatPosition.X, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);

                    if(_handStats != null && _displayResults)
                    {
                        result = Math.Round(_handStats[playerCount].Equity, 2).ToString() + " %";

            g.FillRectangle(Brushes.White, seatPosition.X - cardSize.Width, seatPosition.Y - resultFont.Size, cardSize.Width * 2, Round(resultFont.Size * 2));
                        g.DrawRectangle(Pens.Gray, seatPosition.X - cardSize.Width, seatPosition.Y - resultFont.Size, cardSize.Width * 2, Round(resultFont.Size * 2));
                        g.DrawString(result, resultFont, Brushes.Black, seatPosition.X - Round(g.MeasureString(result, resultFont).Width / 2.2), seatPosition.Y - Round(resultFont.Size / 1.4));
                    }

                    //if (_winners != null && _winners.Contains(i))
                    //{
                    //    g.DrawRectangle(new Pen(Brushes.Gray, 3), seatPosition.X - Round(cardSize.Width * 1.05), seatPosition.Y - Round(cardSize.Height / 2 * 1.1), Round(cardSize.Width * 2.1), Round(cardSize.Height * 1.1));
                    //}

                    playerCount++;
                }
            }

            for (int i = 0; i < _table.CommunityCards.Count; i++)
            {
                Image cardImage = CardImageProvider.GetCard(_table.CommunityCards[i]);
                g.DrawImage(cardImage, Round(_panelCanvas.Width / 2.0 - (cardImage.Width * 1.1 * sizeFactor * (i - 1.7))), Round(_panelCanvas.Height / 2.0 - (cardImage.Height * sizeFactor / 2.0)), Round(cardImage.Width * sizeFactor), Round(cardImage.Height * sizeFactor));
            }
        }

        private Point GetAbsoluteSeatPosition(int seatIndex)
        {
            double sizeFactor = _panelCanvas.Width / (double)Resources.Table.Width;
            int x;
            int y;

            x = Round((_coords[seatIndex * 2] + 5) * sizeFactor);
            y = Round((_coords[seatIndex * 2 + 1] - 5) * sizeFactor);

            return new Point(x, y);
        }

        private int Round(double number)
        {
            return (int)Math.Round(number);
        }

        private void OnPanelLayoutSizeChanged(object sender, EventArgs e)
        {
            if (_panelLayout.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                _panelLayout.Width = Screen.PrimaryScreen.Bounds.Width;
            }

            if ((int)(_panelLayout.Height * _canvasRatio) >= _panelLayout.Width)
            {
                int y = Round((_panelLayout.Height - _panelCanvas.Height) / 2.0);
                _panelCanvas.Size = new Size(_panelLayout.Width, Round(_panelLayout.Width / _canvasRatio));
                _panelCanvas.Location = new Point(0, y);
            }
            else
            {
                int x = Round((_panelLayout.Width - _panelLayout.Height * _canvasRatio) / 2.0);
                _panelCanvas.Size = new Size(Round(_panelLayout.Height * _canvasRatio), _panelLayout.Height);
                _panelCanvas.Location = new Point(x, 0);
            }

            _panelCanvas.Invalidate();
        }

        private void OnButtonCallClick(object sender, EventArgs e)
        {
            ShowResult(true);
        }

        private void OnButtonFoldClick(object sender, EventArgs e)
        {
            ShowResult(false);
        }

        private void ShowResult(bool call)
        {
            _displayResults = true;

            int count = _table.Players.Count(c => c != null);
            double equity = _handStats[0].WinPercentage + _handStats[0].TieEquity;

            string turnout = "";
            bool overEquity = equity > 1.0 / count * 100.0;

            if (call && overEquity)
            {
                right++;
                turnout = "Correct call";
            }
            else if (call && !overEquity)
            {
                turnout = "Wrong call";
            }
            else if (!call && !overEquity)
            {
                right++;
                turnout = "Correct fold";
            }
            else
            {
                turnout = "Wrong fold";
            }

            _labelResult.Text = string.Format("{0}: {1} % (Avg. {2} %)", turnout, Math.Round(equity, 2), Math.Round(1.0 / count * 100.0, 2));

            _buttonCall.Enabled = false;
            _buttonFold.Enabled = false;
            _buttonNext.Enabled = true;

            _panelCanvas.Invalidate();
        }

        private void OnButtonNextClick(object sender, EventArgs e)
        {
            ResetTable();
            _handStats = null;
            _displayResults = false;
            _buttonNext.Enabled = false;
        }

        private void OnFormMainKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F:
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    if (_buttonFold.Enabled)
                    {
                        OnButtonFoldClick(null, null);
                    }
                    break;

                case Keys.C:
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    if (_buttonCall.Enabled)
                    {
                        OnButtonCallClick(null, null);
                    }
                    break;

                case Keys.Enter:
                case Keys.Space:
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    if (_buttonNext.Enabled)
                    {
                        OnButtonNextClick(null, null);
                    }

                    break;

                case Keys.Escape:
                    Environment.Exit(0);
                    break;
            }
        }

        private void OnFormMainClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
