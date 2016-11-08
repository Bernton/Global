using DecisionDealer.Model;
using DecisionDealer.Properties;
using DecisionDealer.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace DecisionDealer.View
{
    public partial class FormMain : Form
    {
        #region Fields

        private bool _isCall;
        private bool _decisionMade;
        private double _ratio;
        private int[] _seatCoordinates;
        private int _initialCardWidth = 75;
        private int _initialCardHeight = 109;
        private FormMainViewModel _vm;
        private ProgressDialog _progressDialog;

        #endregion

        #region Constructors

        public FormMain()
        {
            _progressDialog = new ProgressDialog("Calculating Probabilities", "Calculating...", false);
            _vm = new FormMainViewModel();
            _vm.PercentComplete += OnPercentComplete;
            _ratio = Resources.Table.Width / (double)Resources.Table.Height;
            _seatCoordinates = new int[]
            {
                333, 411, 176, 400, 130, 273, 240, 154, 427, 151,
                600, 151, 778, 154, 886, 273, 848, 395, 692, 411
            };

            InitializeComponent();
            Text = _vm.FormCaption;
            _labelCorrect.Text = _vm.ResetTable(_textBoxFrequency.Text, ResetButtons);
            _labelResult.Text = "";

            CardImageProvider.ResizeCardWidth = _initialCardWidth;
            CardImageProvider.ResizeCardHeight = _initialCardHeight;

#if !CUSTOM
            CardImageProvider.Rescale();
#endif

            _panelCanvas.Invalidate();
        }

        #endregion

        #region Event handler methods

        private void OnFormMainKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    if (_buttonFold.Enabled)
                    {
                        OnButtonFoldClick(null, null);
                    }

                    break;

                case Keys.W:

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

        private void OnPanelCanvasPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawPokerTable(e.Graphics);
        }

        private void OnPanelLayoutSizeChanged(object sender, EventArgs e)
        {
            if ((int)(_panelLayout.Height * _ratio) >= _panelLayout.Width)
            {
                int y = Round((_panelLayout.Height - _panelCanvas.Height) / 2.0);
                _panelCanvas.Size = new Size(_panelLayout.Width, Round(_panelLayout.Width / _ratio));
                _panelCanvas.Location = new Point(0, y);
            }
            else
            {
                int x = Round((_panelLayout.Width - _panelLayout.Height * _ratio) / 2.0);
                _panelCanvas.Size = new Size(Round(_panelLayout.Height * _ratio), _panelLayout.Height);
                _panelCanvas.Location = new Point(x, 0);
            }

            CardImageProvider.ResizeCardWidth = Round(_initialCardWidth * (double)_panelCanvas.Width / (double)Resources.Table.Width);
            CardImageProvider.ResizeCardHeight = Round(_initialCardHeight * (double)_panelCanvas.Height / (double)Resources.Table.Height);

#if !CUSTOM
            CardImageProvider.Rescale();
#endif

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

        private void OnButtonNextClick(object sender, EventArgs e)
        {
            _labelCorrect.Text = _vm.ResetTable(_textBoxFrequency.Text, ResetButtons);
            _panelCanvas.Invalidate();

            _decisionMade = false;
            _labelResult.Visible = false;
            _buttonNext.Enabled = false;
            _buttonFold.Enabled = true;
            _buttonCall.Enabled = true;
        }

        #endregion

        #region Other methods

        private void OnPercentComplete(int percentComplete, int iterationsComplete)
        {
            _progressDialog.StatusText = $"Calculating... ({percentComplete} %)";
            _progressDialog.ProgressBarPercentage = percentComplete;
        }

        private void ResetButtons()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ResetButtons(); });
                return;
            }

            if (_decisionMade)
            {
                _labelResult.Text = _vm.ShowResult(_isCall);
                _labelResult.Visible = true;
            }

            _progressDialog.Hide();
        }

        private void ShowResult(bool isCall)
        {
            _decisionMade = true;

            if (_vm.HandStatistics != null)
            {
                _labelResult.Text = _vm.ShowResult(isCall);
                _labelResult.Visible = true;
            }
            else
            {
                _isCall = isCall;
                _progressDialog.Show();
            }

            _buttonCall.Enabled = false;
            _buttonFold.Enabled = false;
            _buttonNext.Enabled = true;

            _panelCanvas.Invalidate();
        }

        private void DrawPokerTable(Graphics g)
        {
            double sizeFactor = _panelCanvas.Height / (double)Resources.Table.Height;

            Font resultFont = new Font("Arial", Round(18 * sizeFactor));
            string result;
            double average = 0;
            double maxEquityDifference = 0;

            if (_vm.HandStatistics != null && _vm.DisplayEquities)
            {
                average = 100.0 / _vm.HandStatistics.Length;
                maxEquityDifference = (_vm.HandStatistics.Max(c => c.Equity) - _vm.HandStatistics.Min(c => c.Equity));
            }

            for (int i = 0; i < _vm.Table.Players.Count; i++)
            {


                Point seatPosition = GetAbsoluteSeatPosition(_vm.Table.Players[i].SeatNumber - 1);

#if CUSTOM
                Card[] sorted = new Card[2];

                int firstValue = 6;
                int secondValue = 6;

                if (_vm.Table.Players[i].HoleCards[0] != null)
                    firstValue = (int)_vm.Table.Players[i].HoleCards[0].Value;

                if (_vm.Table.Players[i].HoleCards[1] != null)
                    secondValue = (int)_vm.Table.Players[i].HoleCards[1].Value;

                if (firstValue > secondValue)
                {
                    sorted[0] = _vm.Table.Players[i].HoleCards[0];
                    sorted[1] = _vm.Table.Players[i].HoleCards[1];
                }
                else
                {
                    sorted[0] = _vm.Table.Players[i].HoleCards[1];
                    sorted[1] = _vm.Table.Players[i].HoleCards[0];
                }

                DrawCard(g, sorted[0], seatPosition.X - CardImageProvider.ResizeCardWidth, seatPosition.Y - Round(CardImageProvider.ResizeCardHeight / 2.0), CardImageProvider.ResizeCardWidth, CardImageProvider.ResizeCardHeight);
                DrawCard(g, sorted[1], seatPosition.X + 2, seatPosition.Y - Round(CardImageProvider.ResizeCardHeight / 2.0), CardImageProvider.ResizeCardWidth, CardImageProvider.ResizeCardHeight);
#else
                Image firstCardImage = null;
                Image secondCardImage = null;

                int firstValue = 6;
                int secondValue = 6;

                if (_vm.Table.Players[i].HoleCards[0] != null)
                    firstValue = (int)_vm.Table.Players[i].HoleCards[0].Value;

                if (_vm.Table.Players[i].HoleCards[1] != null)
                    secondValue = (int)_vm.Table.Players[i].HoleCards[1].Value;

                if (firstValue > secondValue)
                {
                    firstCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[0]);
                    secondCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[1]);
                }
                else
                {
                    firstCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[1]);
                    secondCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[0]);
                }

                g.DrawImage(firstCardImage, seatPosition.X - firstCardImage.Width, seatPosition.Y - Round(firstCardImage.Height / 2.0));
                g.DrawImage(secondCardImage, seatPosition.X + 2, seatPosition.Y - Round(secondCardImage.Height / 2.0));
#endif
                if (_vm.HandStatistics != null && _decisionMade && _vm.DisplayEquities)
                {
                    result = Math.Round(_vm.HandStatistics[i].Equity, 2).ToString() + " %";

                    Color rectColor = new Color();
                    if (_vm.HandStatistics[i].Equity > average)
                    {
                        rectColor = Color.FromArgb(0, (185 + (int)(60 * ((_vm.HandStatistics[i].Equity - average) / maxEquityDifference))) & 255, 0);
                    }
                    else
                    {
                        rectColor = Color.FromArgb((185 + (int)(60 * ((average - _vm.HandStatistics[i].Equity) / maxEquityDifference))) & 255, 0, 0);
                    }

                    Brush rectBrush = new SolidBrush(rectColor);

                    g.FillRectangle(rectBrush, seatPosition.X - CardImageProvider.ResizeCardWidth, seatPosition.Y + Round(resultFont.Size / 4.0), CardImageProvider.ResizeCardWidth * 2 + 2, Round(resultFont.Size * 2.5));
                    g.DrawRectangle(Pens.Black, seatPosition.X - CardImageProvider.ResizeCardWidth, seatPosition.Y + Round(resultFont.Size / 4.0), CardImageProvider.ResizeCardWidth * 2 + 2, Round(resultFont.Size * 2.5));
                    g.DrawString(result, resultFont, Brushes.Black, seatPosition.X - Round(g.MeasureString(result, resultFont).Width / 2.2), seatPosition.Y + Round(resultFont.Size / 1.5));
                }
            }

            List<Card> sortedCommunityCards = new List<Card>();

            if (_vm.Table.CommunityCards.Count > 0)
            {
                sortedCommunityCards = _vm.Table.CommunityCards.OrderByDescending(c => (int)c.Value).ThenBy(c => (int)c.Suit).ToList();
            }

            for (int i = 0; i < sortedCommunityCards.Count; i++)
            {
#if CUSTOM
                DrawCard(g, sortedCommunityCards[i], Round(_panelCanvas.Width / 2.0 - (CardImageProvider.ResizeCardWidth * 1.05 * (Math.Abs(i - sortedCommunityCards.Count) - 1.7))), Round(_panelCanvas.Height / 2.0 - (CardImageProvider.ResizeCardHeight / 2.0)), CardImageProvider.ResizeCardWidth, CardImageProvider.ResizeCardHeight);
#else
                Image cardImage = CardImageProvider.GetCard(sortedCommunityCards[i]);
                g.DrawImage(cardImage, Round(_panelCanvas.Width / 2.0 - (cardImage.Width * 1.05 * (Math.Abs(i - _vm.Table.CommunityCards.Count) - 1.7))), Round(_panelCanvas.Height / 2.0 - (cardImage.Height / 2.0)), cardImage.Width, cardImage.Height);
#endif
            }
        }

        private void DrawCard(Graphics g, Card card, int x, int y, int width, int height)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (card == null)
            {
                g.FillRoundedRectangle(Brushes.LightCoral, new Rectangle(x, y, width, height), Round(height / 15.0));
                g.DrawRoundedRectangle(Pens.Black, new Rectangle(x, y, width, height), Round(height / 15.0));
                return;
            }

            SolidBrush backBrush = new SolidBrush(Color.White);
            SolidBrush fontBrush = null;
            Font symbolFont = new Font("Tw Cen MT Condensed", Round(height * 0.45));
            Font suitSymbolFont = new Font("Century Schoolbook", Round(height * 0.35));
            string suitSymbol = "";

            switch (card.Suit)
            {
                case CardSuit.Club:
                    suitSymbol = "♣";
                    //backBrush = new SolidBrush(Color.LightGreen);
                    fontBrush = new SolidBrush(Color.DarkGreen);
                    break;

                case CardSuit.Diamond:
                    suitSymbol = "♦";
                    //backBrush = new SolidBrush(Color.LightBlue);
                    fontBrush = new SolidBrush(Color.DarkBlue);
                    break;

                case CardSuit.Heart:
                    suitSymbol = "♥";
                    //backBrush = new SolidBrush(Color.LightCoral);
                    fontBrush = new SolidBrush(Color.DarkRed);
                    break;

                case CardSuit.Spade:
                    suitSymbol = "♠";
                    //backBrush = new SolidBrush(Color.Gray);
                    fontBrush = new SolidBrush(Color.Black);
                    break;
            }

            string symbol = GetPokerSymbol(card.Value);
            SizeF stringSize = g.MeasureString(symbol, symbolFont);
            SizeF suitSymbolSize = g.MeasureString(suitSymbol, suitSymbolFont);

            g.FillRoundedRectangle(Brushes.White, new Rectangle(x, y, width, height), Round(height / 15.0));
            g.DrawRoundedRectangle(Pens.Black, new Rectangle(x, y, width, height), Round(height / 15.0));
            g.DrawString(symbol, symbolFont, fontBrush, Round(x + ((width - stringSize.Width) / 2.0)), Round(y - height / 16.0));
            g.DrawString(suitSymbol, suitSymbolFont, fontBrush, Round(x + ((width - suitSymbolSize.Width) / 1.70)), Round(y + height / 2.4));
        }

        private string GetPokerSymbol(CardValue value)
        {
            switch (value)
            {
                case CardValue.Jack:
                    return "J";

                case CardValue.Queen:
                    return "Q";

                case CardValue.King:
                    return "K";

                case CardValue.Ace:
                    return "A";

                default:
                    return ((int)value + 2).ToString();
            }
        }

        private int Round(double number)
        {
            return (int)Math.Round(number);
        }

        private Point GetAbsoluteSeatPosition(int seatIndex)
        {
            double sizeFactor = _panelCanvas.Height / (double)Resources.Table.Height;
            int x;
            int y;

            x = Round((_seatCoordinates[seatIndex * 2] + 5) * sizeFactor);
            y = Round((_seatCoordinates[seatIndex * 2 + 1] - 5) * sizeFactor);

            return new Point(x, y);
        }

        #endregion
    }
}
