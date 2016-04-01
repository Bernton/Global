using BerldPoker.Controls;
using BerldPokerClient.NetworkUtilities;
using BerldPokerClient.Poker;
using BerldPokerClient.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BerldPokerClient.Views
{
    public partial class FormTable : Form
    {
        #region Fields

        private UtilityClient _client;
        private PokerTable _table;
        private PokerPlayer _player;

        private int _playerIndex;
        private bool _isObserver = false;
        private bool _hiddenMode = false;

        #endregion

        #region Initialization

        public FormTable(UtilityClient client)
        {
            _table = new PokerTable();
            _player = new PokerPlayer("You");
            _player.Chips = 1000;
            _player.IsFolded = true;
            _table.Players.Add(_player);
            _playerIndex = _table.Players.Count - 1;

            _client = client;
            _client.ReceivedMessage += OnServerMessageReceived;

            InitializeUI();
            UpdateGUI();
        }

        public FormTable(UtilityClient client, PokerTable table, bool asObserver)
        {
            _isObserver = asObserver;

            _table = table;

            _player = new PokerPlayer("You");
            _player.Chips = 1000;
            _player.IsFolded = true;
            _playerIndex = -1;

            if (!asObserver)
            {
                _table.Players.Add(_player);
                _playerIndex = _table.Players.Count - 1;
            }

            _client = client;
            _client.ReceivedMessage += OnServerMessageReceived;

            InitializeUI();
            UpdateGUI();
        }

        private void InitializeUI()
        {
            InitializeComponent();
            Icon = Resources.Favicon;
        }

        #endregion

        #region Event methods

        private void OnFormTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
            {
                _hiddenMode = !_hiddenMode;

                UpdateGUI();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void OnServerMessageReceived(string source, string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    OnServerMessageReceived(source, message);
                });

                return;
            }

            string[] args = message.Split(';');

            switch (args[0])
            {
                case "NewPlayer":

                    if (_player.Chips > 0)
                    {
                        PokerPlayer player = new PokerPlayer(args[1]);
                        player.Chips = 1000;
                        player.IsFolded = true;

                        _table.Players.Add(player);
                    }

                    break;

                case "PlayerLeft":

                    int leaverIndex = int.Parse(args[1]);

                    if (leaverIndex >= _table.Players.Count)
                    {
                        return;
                    }

                    string leaverName = _table.Players[leaverIndex].Name;

                    _table.RemovePlayer(leaverIndex);

                    MessageBox.Show($"{leaverName} has left the table.", "BerldPokerClient", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

                case "Deal":

                    _table.StartNewRound();

                    if (args.Length != 1)
                    {
                        _player.Card1 = new Card((CardValue)int.Parse(args[1]), (CardSuit)int.Parse(args[2]));
                        _player.Card2 = new Card((CardValue)int.Parse(args[3]), (CardSuit)int.Parse(args[4]));
                    }

                    break;

                case "Fold":

                    _table.Fold();
                    break;

                case "CheckCall":

                    _table.CheckCall();
                    break;

                case "BetRaise":

                    _table.BetRaise(int.Parse(args[1]));
                    break;

                case "Flop":

                    _table.Flop = new Card[3];
                    _table.Flop[0] = new Card((CardValue)int.Parse(args[1]), (CardSuit)int.Parse(args[2]));
                    _table.Flop[1] = new Card((CardValue)int.Parse(args[3]), (CardSuit)int.Parse(args[4]));
                    _table.Flop[2] = new Card((CardValue)int.Parse(args[5]), (CardSuit)int.Parse(args[6]));

                    _table.SetAfterDealer();
                    break;

                case "Turn":

                    _table.Turn = new Card((CardValue)int.Parse(args[1]), (CardSuit)int.Parse(args[2]));
                    _table.SetAfterDealer();
                    break;

                case "River":

                    _table.River = new Card((CardValue)int.Parse(args[1]), (CardSuit)int.Parse(args[2]));
                    _table.SetAfterDealer();
                    break;

                case "Finish":

                    //string tableData = message.Substring(args[0].Length + 1);
                    //XmlSerializer serializer = new XmlSerializer(typeof(PokerTable));
                    //PokerTable table = (PokerTable)serializer.Deserialize(new StringReader(tableData));

                    string rankingData = args[1];

                    string[] cardTextPairs = args[2].Split('|');

                    string[] valueTexts = new string[cardTextPairs.Length / 5];
                    Card[] cards = new Card[cardTextPairs.Length - valueTexts.Length];

                    for (int i = 0; i < cardTextPairs.Length / 5; i++)
                    {
                        valueTexts[i] = cardTextPairs[i * 5];
                        cards[i * 2] = new Card((CardValue)int.Parse(cardTextPairs[i * 5 + 1]), (CardSuit)int.Parse(cardTextPairs[i * 5 + 2]));
                        cards[i * 2 + 1] = new Card((CardValue)int.Parse(cardTextPairs[i * 5 + 3]), (CardSuit)int.Parse(cardTextPairs[i * 5 + 4]));
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<List<int>>));
                    List<List<int>> ranking = (List<List<int>>)serializer.Deserialize(new StringReader(rankingData));

                    _table.Finish(ranking, valueTexts, cards);
                    break;
            }

            UpdateGUI();
        }

        private void OnFormTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.ReceivedMessage -= OnServerMessageReceived;

            if (_player.Chips != 0)
            {
                _client.SendMessage("TableLeft");
            }
        }

        private void On_buttonDeal_Click(object sender, EventArgs e)
        {
            _buttonDeal.Enabled = false;
            _client.SendMessage("Deal");
        }

        private void On_buttonFold_Click(object sender, EventArgs e)
        {
            _client.SendMessage("Fold");
            _table.Fold();

            UpdateGUI();
        }

        private void On_buttonCheckCall_Click(object sender, EventArgs e)
        {
            _client.SendMessage("CheckCall");
            _table.CheckCall();

            UpdateGUI();
        }

        private void On_buttonBetRaise_Click(object sender, EventArgs e)
        {
            int bet;
            bool isValidNumber = int.TryParse(_textBoxChips.Text, out bet);

            if (isValidNumber && (bet >= _table.ChipsToCall + _table.BigBlind || bet >= _player.Chips ))
            {
                if (bet > _player.Chips + _player.ChipsInPot)
                {
                    bet = _player.Chips + _player.ChipsInPot;
                }

                _client.SendMessage($"BetRaise;{bet}");
                _table.BetRaise(bet);

                UpdateGUI();
            }
            else
            {
                MessageBox.Show("Invalid input.", "Berld Poker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void On_panelHands_Resize(object sender, EventArgs e)
        {
            UpdateHandPanel();
        }

        private void On_textBoxChips_TextChanged(object sender, EventArgs e)
        {
            int input;

            if (int.TryParse(_textBoxChips.Text, out input))
            {
                if (input > _player.Chips)
                {
                    _buttonBetRaise.Text = "Raise All In";
                }
                else
                {
                    _buttonBetRaise.Text = "Bet / Raise";
                }
            }
        }

        #endregion

        #region User interface methods

        private void UpdateGUI()
        {
            Text = "BerldPoker Client";

            UpdateHandPanel();

            if (_table.Players.Where(c => c.Chips > 0).ToArray().Length >= 2 && _table.ToAct == -1 && _player.Chips > 0 && !_isObserver)
            {
                _buttonDeal.Enabled = true;
            }
            else if (_table.Players.Count < 2)
            {
                Text = "BerldPoker Client (Waiting for player)";
                _buttonDeal.Enabled = false;
            }
            else
            {
                _buttonDeal.Enabled = false;
            }

            if (_table.Flop != null)
            {
                _pictureBoxFlop1.Image = CardImageProvider.GetImageFromCard(_table.Flop[0]);
                _pictureBoxFlop2.Image = CardImageProvider.GetImageFromCard(_table.Flop[1]);
                _pictureBoxFlop3.Image = CardImageProvider.GetImageFromCard(_table.Flop[2]);
            }
            else
            {
                _pictureBoxFlop1.Image = null;
                _pictureBoxFlop2.Image = null;
                _pictureBoxFlop3.Image = null;
            }

            if (_table.Turn != null)
            {
                _pictureBoxTurn.Image = CardImageProvider.GetImageFromCard(_table.Turn);
            }
            else
            {
                _pictureBoxTurn.Image = null;
            }

            if (_table.River != null)
            {
                _pictureBoxRiver.Image = CardImageProvider.GetImageFromCard(_table.River);
            }
            else
            {
                _pictureBoxRiver.Image = null;
            }

            if (_table.Pots != null && _table.Pots.Count > 0)
            {
                _labelPot.Text = $"Pot: {_table.Pots.Sum(c => c.Chips) + _table.Players.Sum(c => c.ChipsInPot)} $";
            }
            else
            {
                _labelPot.Text = "";
            }

            _labelPlayerCount.Text = $"{_table.Players.Count(c => c.TotalChips > 0)}/9";

            if (_player.Chips <= 0 && _table.ToAct < 0)
            {
                Text = "BerldPoker Client (Knocked Out)";
            }

            if (_table.ToAct >= 0 && _table.Players[_table.ToAct] == _player)
            {
                _buttonFold.Enabled = true;
                _buttonCheckCall.Enabled = true;

                if (_player.Chips + _player.ChipsInPot <= _table.ChipsToCall)
                {
                    _buttonCheckCall.Text = "All In";
                    _buttonBetRaise.Text = "Bet / Raise";
                    _buttonBetRaise.Enabled = false;
                }
                else
                {
                    if (_player.ChipsInPot == _table.ChipsToCall)
                    {
                        _buttonCheckCall.Text = "Check";
                    }
                    else
                    {
                        _buttonCheckCall.Text = "Call";
                    }

                    if (_table.ChipsToCall == 0)
                    {
                        _buttonBetRaise.Text = "Bet";
                    }
                    else
                    {
                        _buttonBetRaise.Text = "Raise";
                    }

                    _buttonBetRaise.Enabled = true;
                }


                _textBoxChips.Enabled = true;
            }
            else
            {
                _buttonFold.Enabled = false;
                _buttonCheckCall.Enabled = false;
                _buttonBetRaise.Enabled = false;
                _textBoxChips.Enabled = false;
            }
        }

        private void UpdateHandPanel()
        {
            int horizontalSpots = _panelHands.Width / 240;

            if (horizontalSpots <= 0)
            {
                return;
            }

            _panelHands.Controls.Clear();

            for (int i = 0; i < _table.Players.Count; i++)
            {
                bool isHidden = false;

                if (i == _playerIndex && _hiddenMode && _table.ToAct != -1)
                {
                    isHidden = true;
                }

                HandPanel handPanel = new HandPanel(_table.Players[i], i == _table.ToAct, i == _table.DealerPosition, isHidden);
                handPanel.Location = new Point((i % horizontalSpots) * 240, (i / horizontalSpots) * 230);

                _panelHands.Controls.Add(handPanel);
            }
        }

        #endregion
    }
}
