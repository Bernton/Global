using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BerldPoker
{
    public partial class FormMain : Form
    {
        private Thread _simulateWorker;
        private List<HandRanking> _handStats = new List<HandRanking>();
        private List<Dictionary<Type, int[]>> _comboStats = new List<Dictionary<Type, int[]>>();

        public FormMain()
        {
            InitializeComponent();

            for (int i = 0; i < 22; i++)
            {
                _comboStats.Add(new Dictionary<Type, int[]>());

                _comboStats[i].Add(typeof(HighCard), new int[2]);
                _comboStats[i].Add(typeof(Pair), new int[2]);
                _comboStats[i].Add(typeof(DoublePair), new int[2]);
                _comboStats[i].Add(typeof(TreeOfAKind), new int[2]);
                _comboStats[i].Add(typeof(Straight), new int[2]);
                _comboStats[i].Add(typeof(Flush), new int[2]);
                _comboStats[i].Add(typeof(FullHouse), new int[2]);
                _comboStats[i].Add(typeof(FourOfAKind), new int[2]);
                _comboStats[i].Add(typeof(StraightFlush), new int[2]);
            }
            for (int i = 0; i < 22; i++)
            {
                _handStats.Add(new HandRanking());
            }

            _comboBoxHandAmount.SelectedItem = "3";
            Setup(int.Parse((string)_comboBoxHandAmount.SelectedItem));
        }

        private void AddCardToGrid(Card card)
        {
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewImageCell image = new DataGridViewImageCell();
            DataGridViewTextBoxCell text = new DataGridViewTextBoxCell();

            image.ImageLayout = DataGridViewImageCellLayout.Normal;
            image.Value = CardImageProvider.GetImage(card);

            row.Height = ((Image)image.Value).Height + 16;

            text.Value = card.ToString();

            row.Cells.Add(image);
            row.Cells.Add(text);

            _dataGridView.Rows.Add(row);
        }

        private void Setup(int handAmount, bool threadMode = false)
        {
            if (!threadMode)
            {
                _panelHands.Controls.Clear();
                _dataGridView.Rows.Clear();
            }

            Deck deck = new Deck();
            deck.Shuffle();

            if (!threadMode)
            {
                foreach (Card card in deck.Cards)
                {
                    AddCardToGrid(card);
                }
            }

            IHandValue[] handValues = new IHandValue[handAmount];

            Card[] flop = new Card[3];
            flop[0] = deck.Cards[handAmount * 2 + 1];
            flop[1] = deck.Cards[handAmount * 2 + 2];
            flop[2] = deck.Cards[handAmount * 2 + 3];
            Card turn = deck.Cards[handAmount * 2 + 5];
            Card river = deck.Cards[handAmount * 2 + 7];

            int horizontalSpots = 0;

            if (!threadMode)
            {
                horizontalSpots = _panelHands.Width / 240;
            }

            for (int i = 0; i < handAmount; i++)
            {
                Card card1 = deck.Cards[i];
                Card card2 = deck.Cards[handAmount + i];

                Card[] cards = new Card[7];
                cards[0] = card1;
                cards[1] = card2;
                cards[2] = flop[0];
                cards[3] = flop[1];
                cards[4] = flop[2];
                cards[5] = turn;
                cards[6] = river;

                handValues[i] = PokerEngine.GetHandValue(cards);

                if (!threadMode)
                {
                    HandPanel handPanel = new HandPanel(card1, card2, handValues[i], i);
                    handPanel.Location = new Point((i % horizontalSpots) * 240, 230 * (i / horizontalSpots));

                    _panelHands.Controls.Add(handPanel);
                }
            }

            if (!threadMode)
            {
                label7.Text = flop[0].ToString();
                pictureBox7.Image = CardImageProvider.GetImage(flop[0]);
                label8.Text = flop[1].ToString();
                pictureBox8.Image = CardImageProvider.GetImage(flop[1]);
                label9.Text = flop[2].ToString();
                pictureBox9.Image = CardImageProvider.GetImage(flop[2]);

                label10.Text = turn.ToString();
                pictureBox10.Image = CardImageProvider.GetImage(turn);

                label11.Text = river.ToString();
                pictureBox11.Image = CardImageProvider.GetImage(river);
            }

            IHandValue[] winnerValues = PokerEngine.GetWinnerValues(handValues);

            for (int i = 0; i < handValues.Length; i++)
            {
                Card[] cards = new Card[] { deck.Cards[i], deck.Cards[handAmount + i] };
                cards = cards.OrderByDescending(c => (int)c.Value).ToArray();
                bool isSuited = cards[0].Suit == cards[1].Suit;

                Hand currentHand = _handStats[handAmount - 1].Hands.First(c => c.CardValue1 == cards[0].Value && c.CardValue2 == cards[1].Value && c.IsSuited == isSuited);

                _comboStats[handAmount - 1][handValues[i].GetType()][1]++;
                currentHand.Count++;

                if(handValues[i].GetType() == winnerValues[0].GetType())
                {
                    _comboStats[handAmount - 1][handValues[i].GetType()][0]++;
                }

                if (winnerValues.Contains(handValues[i]))
                {
                    if (!threadMode)
                    {
                        ((HandPanel)_panelHands.Controls[i]).IsWinner = true;
                    }

                    currentHand.Won++;
                }
            }

            if (!threadMode)
            {
                _labelTotalsHands.Text = PokerEngine.Total.ToString();
                _labelRoyalFlush.Text = "(" + Math.Round((double)PokerEngine.RoyalFlush / PokerEngine.Total * 100.0, 4) + "%) " + PokerEngine.RoyalFlush;
                _labelStraightFlush.Text = "(" + Math.Round((double)PokerEngine.StraightFlush / PokerEngine.Total * 100.0, 3) + "%) " + PokerEngine.StraightFlush;
                _labelQuads.Text = "(" + Math.Round((double)PokerEngine.FourOfAKind / PokerEngine.Total * 100.0, 3) + "%) " + PokerEngine.FourOfAKind;
                _labelFullHouse.Text = "(" + Math.Round((double)PokerEngine.FullHouse / PokerEngine.Total * 100.0, 2) + "%) " + PokerEngine.FullHouse;
                _labelFlushes.Text = "(" + Math.Round((double)PokerEngine.Flush / PokerEngine.Total * 100.0, 2) + "%) " + PokerEngine.Flush;
                _labelStraights.Text = "(" + Math.Round((double)PokerEngine.Straight / PokerEngine.Total * 100.0, 2) + "%) " + PokerEngine.Straight;
                _labelSets.Text = "(" + Math.Round((double)PokerEngine.TreeOfAKind / PokerEngine.Total * 100.0, 2) + "%) " + PokerEngine.TreeOfAKind;
                _labelDoublePairs.Text = "(" + Math.Round((double)PokerEngine.DoublePair / PokerEngine.Total * 100.0, 1) + "%) " + PokerEngine.DoublePair;
                _labelPairs.Text = "(" + Math.Round((double)PokerEngine.Pair / PokerEngine.Total * 100.0, 1) + "%) " + PokerEngine.Pair;
                _labelHighCard.Text = "(" + Math.Round((double)PokerEngine.HighCard / PokerEngine.Total * 100.0, 1) + "%) " + PokerEngine.HighCard;

                On_panelHands_Resize(null, null);
            }
        }

        private void On_buttonNew_Click(object sender, System.EventArgs e)
        {
            Setup(int.Parse((string)_comboBoxHandAmount.SelectedItem));
        }

        private void On_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            _dataGridView.ClearSelection();
        }

        private void On_panelHands_Resize(object sender, EventArgs e)
        {
            int horizontalSpots = _panelHands.Width / 240;
            int leftOver = _panelHands.Width % 240;

            if (horizontalSpots == 0)
            {
                return;
            }

            for (int i = 0; i < _panelHands.Controls.Count; i++)
            {
                _panelHands.Controls[i].Location = new Point((i % horizontalSpots) * 240 + (i % horizontalSpots) * (leftOver / horizontalSpots), 232 * (i / horizontalSpots));
            }

            if (_panelHands.HorizontalScroll.Visible || _panelHands.VerticalScroll.Visible)
            {
                _panelHands.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (_panelHands.BorderStyle != BorderStyle.None)
            {
                _panelHands.BorderStyle = BorderStyle.None;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void labels_TextChanged(object sender, EventArgs e)
        {
            Label currentLabel = sender as Label;

            while (currentLabel.Width < TextRenderer.MeasureText(currentLabel.Text, new Font(currentLabel.Font.FontFamily, currentLabel.Font.Size, currentLabel.Font.Style)).Width)
            {
                currentLabel.Font = new Font(currentLabel.Font.FontFamily, currentLabel.Font.Size - 0.5f, currentLabel.Font.Style);
            }
        }

        private void On_buttonHandStats_Click(object sender, EventArgs e)
        {
            FormHandStatistics handStats = new FormHandStatistics(_handStats[int.Parse(_comboBoxHandAmount.Text) - 1]);
            handStats.ShowDialog();
        }

        private void On_buttonAutomatic_Click(object sender, EventArgs e)
        {
            int handAmount = int.Parse((string)_comboBoxHandAmount.SelectedItem);

            if (_buttonAutomatic.Text == "Auto")
            {
                _simulateWorker = new Thread(() =>
                {
                    while (true)
                    {
                       Setup(handAmount, true);
                    }
                });

                _simulateWorker.Start();
                _buttonAutomatic.Text = "Stop";
            }
            else
            {
                _simulateWorker.Abort();
                _buttonAutomatic.Text = "Auto";
                Setup(int.Parse((string)_comboBoxHandAmount.SelectedItem));
            }
        }

        private void On_buttonCombStats_Click(object sender, EventArgs e)
        {
            FormCombStatistics combStats = new FormCombStatistics(_comboStats[int.Parse(_comboBoxHandAmount.Text) - 1]);
            combStats.ShowDialog();
        }
    }
}
