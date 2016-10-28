using DecisionDealer.Model;
using DecisionDealer.Properties;
using DecisionDealer.ViewModel;
using ProgressUtility;
using System;
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
                333, 411, 176, 400, 130, 253, 240, 154, 427, 151,
                600, 151, 778, 154, 886, 263, 848, 395, 692, 411
            };

            InitializeComponent();
            Text = _vm.FormCaption;
            _labelCorrect.Text = _vm.ResetTable(_textBoxFrequency.Text, ResetButtons);
            _labelResult.Text = "";
            _panelCanvas.Invalidate();
        }

        #endregion

        #region Event handler methods

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

        private void OnPanelCanvasPaint(object sender, PaintEventArgs e)
        {
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
            }

            _progressDialog.Hide();
        }

        private void ShowResult(bool isCall)
        {
            _decisionMade = true;

            if (_vm.HandStatistics != null)
            {
                _labelResult.Text = _vm.ShowResult(isCall);
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
            double sizeFactor = _panelCanvas.Width / (double)Resources.Table.Width;
            int playerCount = 0;

            Font resultFont = new Font("Arial", Round(18 * sizeFactor));
            string result;
            double average = 0;
            double maxEquityDifference = 0;

            if (_vm.HandStatistics != null && _vm.DisplayEquities)
            {
                average = 100.0 / _vm.HandStatistics.Length;
                maxEquityDifference = (_vm.HandStatistics.Max(c => c.Equity) - _vm.HandStatistics.Min(c => c.Equity));
            }

            for (int i = 0; i < _vm.Table.Players.Length; i++)
            {
                if (_vm.Table.Players[i] != null)
                {


                    Image firstCardImage;
                    Image secondCardImage;

                    if (_vm.Table.Players[i].HoleCards[0] == null)
                    {
                        firstCardImage = CardImageProvider.GetCardBack();
                    }
                    else
                    {
                        firstCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[0]);
                    }

                    if (_vm.Table.Players[i].HoleCards[1] == null)
                    {
                        secondCardImage = CardImageProvider.GetCardBack();
                    }
                    else
                    {
                        secondCardImage = CardImageProvider.GetCard(_vm.Table.Players[i].HoleCards[1]);
                    }

                    Point seatPosition = GetAbsoluteSeatPosition(i);
                    Size cardSize = new Size(Round(firstCardImage.Width * sizeFactor * 0.5), Round(firstCardImage.Height * sizeFactor * 0.5));

                    g.DrawImage(firstCardImage, seatPosition.X - cardSize.Width, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);
                    g.DrawImage(secondCardImage, seatPosition.X, seatPosition.Y - Round(cardSize.Height / 2.0), cardSize.Width, cardSize.Height);

                    if (_vm.HandStatistics != null && _decisionMade && _vm.DisplayEquities)
                    {
                        result = Math.Round(_vm.HandStatistics[playerCount].Equity, 2).ToString() + " %";

                        Color rectColor = new Color();
                        if (_vm.HandStatistics[playerCount].Equity > average)
                        {
                            rectColor = Color.FromArgb(0, 185 + (int)(60 * ((_vm.HandStatistics[playerCount].Equity - average) / maxEquityDifference)), 0);
                        }
                        else
                        {
                            rectColor = Color.FromArgb(185 + (int)(60 * ((average - _vm.HandStatistics[playerCount].Equity) / maxEquityDifference)), 0, 0);
                        }

                        Brush rectBrush = new SolidBrush(rectColor);

                        g.FillRectangle(rectBrush, seatPosition.X - cardSize.Width, seatPosition.Y - resultFont.Size, cardSize.Width * 2, Round(resultFont.Size * 2));
                        g.DrawRectangle(Pens.Gray, seatPosition.X - cardSize.Width, seatPosition.Y - resultFont.Size, cardSize.Width * 2, Round(resultFont.Size * 2));
                        g.DrawString(result, resultFont, Brushes.Black, seatPosition.X - Round(g.MeasureString(result, resultFont).Width / 2.2), seatPosition.Y - Round(resultFont.Size / 1.4));
                    }

                    playerCount++;
                }
            }

            for (int i = 0; i < _vm.Table.CommunityCards.Count; i++)
            {
                Image cardImage = CardImageProvider.GetCard(_vm.Table.CommunityCards[i]);
                g.DrawImage(cardImage, Round(_panelCanvas.Width / 2.0 - (cardImage.Width * 1.1 * sizeFactor * (i - 1.7))), Round(_panelCanvas.Height / 2.0 - (cardImage.Height * sizeFactor / 2.0)), Round(cardImage.Width * sizeFactor), Round(cardImage.Height * sizeFactor));
            }
        }

        private int Round(double number)
        {
            return (int)Math.Round(number);
        }

        private Point GetAbsoluteSeatPosition(int seatIndex)
        {
            double sizeFactor = _panelCanvas.Width / (double)Resources.Table.Width;
            int x;
            int y;

            x = Round((_seatCoordinates[seatIndex * 2] + 5) * sizeFactor);
            y = Round((_seatCoordinates[seatIndex * 2 + 1] - 5) * sizeFactor);

            return new Point(x, y);
        }

        #endregion
    }
}
