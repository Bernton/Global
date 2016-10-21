using DecisionDealer.Model;
using DecisionDealer.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;

namespace DecisionDealer.View
{
    public partial class FormMain : Form
    {
        private Random _rnd = new Random();
        private PokerTable _table = new PokerTable();
        private double _canvasRatio = Resources.Table.Width / (double)Resources.Table.Height;

        private int[] _winners = null;
        private double _cash = 30;

        private int[] _coords = new int[]
        {
            295, 421,
            126, 390,
            105, 263,
            198, 134,
            407, 131,
            616, 131,
            824, 134,
            931, 263,
            888, 390,
            712, 421
        };

        public FormMain()
        {
            InitializeComponent();
            Text = string.Format("DecisionDealer Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString(2));

            ResetTable();
        }

        public void ResetTable()
        {
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

            _table.DealHoleCards();
            _winners = null;
            _panelCanvas.Invalidate();
        }

        private void OnPanelCanvasPaint(object sender, PaintEventArgs e)
        {
            _labelCash.Text = string.Format("{0} $", Math.Round(_cash, 2));

            DrawPokerTable(e.Graphics);
        }

        private void DrawPokerTable(Graphics g)
        {
            double sizeFactor = _panelCanvas.Width / (double)Resources.Table.Width;

            for (int i = 0; i < _table.Players.Length; i++)
            {
                if (_table.Players[i] != null)
                {
                    Image firstCardImage;
                    Image secondCardImage;

                    if (!_table.Players[i].RevealedCards)
                    {
                        firstCardImage = CardImageProvider.GetCardBack();
                        secondCardImage = CardImageProvider.GetCardBack();
                    }
                    else
                    {
                        firstCardImage = CardImageProvider.GetCard(_table.Players[i].HoleCards[0]);
                        secondCardImage = CardImageProvider.GetCard(_table.Players[i].HoleCards[1]);
                    }

                    Point seatPosition = GetAbsoluteSeatPosition(i);

                    Size cardSize = new Size(Round(firstCardImage.Width * sizeFactor), Round(firstCardImage.Height * sizeFactor));

                    g.DrawImage(firstCardImage, seatPosition.X - cardSize.Width, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);
                    g.DrawImage(secondCardImage, seatPosition.X, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);
                    
                    if(_winners != null && _winners.Contains(i))
                    {
                        g.DrawRectangle(new Pen(Brushes.Gray, 3), seatPosition.X - Round(cardSize.Width * 1.05), seatPosition.Y - Round(cardSize.Height / 2 * 1.1), Round(cardSize.Width * 2.1), Round(cardSize.Height * 1.1));
                    }
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

            x = Round(_coords[seatIndex * 2] * sizeFactor);
            y = Round(_coords[seatIndex * 2 + 1] * sizeFactor);

            return new Point(x, y);
        }

        private int Round(double number)
        {
            return (int)Math.Round(number);
        }

        private void OnPanelLayoutSizeChanged(object sender, EventArgs e)
        {
            _panelCanvas.Invalidate();

            if ((int)(_panelLayout.Height * _canvasRatio) >= _panelLayout.Width)
            {
                int y = Round((_panelLayout.Height - _panelCanvas.Height) / 2.0);
                _panelCanvas.Size = new Size(_panelLayout.Width, Round(_panelLayout.Width / _canvasRatio));
                _panelCanvas.Location = new Point(0, y);
            }
            else
            {
                int x = Round((_panelLayout.Width - _panelCanvas.Width) / 2.0);
                _panelCanvas.Size = new Size(Round(_panelLayout.Height * _canvasRatio), _panelLayout.Height);
                _panelCanvas.Location = new Point(x, 0);
            }
        }

        private void OnButtonCallClick(object sender, EventArgs e)
        {
            _winners = _table.Playout();
            int count = _table.Players.Count(c => c != null);

            if(_winners.Contains(0))
            {
                

                _cash += ((double)count / _winners.Length) - 1;
            }
            else
            {
                _cash -=  1;
            }

            _buttonCall.Enabled = false;
            _buttonFold.Enabled = false;
            _buttonNext.Enabled = true;

            _panelCanvas.Invalidate();
        }

        private void OnButtonFoldClick(object sender, EventArgs e)
        {
            _winners = _table.Playout();
            int count = _table.Players.Count(c => c != null);

            _winners = _table.Playout();

            _cash -= 1.0 / count / count;

            _buttonCall.Enabled = false;
            _buttonFold.Enabled = false;
            _buttonNext.Enabled = true;

            _panelCanvas.Invalidate();
        }

        private void OnButtonNextClick(object sender, EventArgs e)
        {
            ResetTable();

            _buttonNext.Enabled = false;
            _buttonCall.Enabled = true;
            _buttonFold.Enabled = true;
        }
    }
}
