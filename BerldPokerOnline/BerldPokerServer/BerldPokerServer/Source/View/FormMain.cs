using BerldPokerServer.NetworkUtilities;
using BerldPokerServer.Poker;
using BerldPokerServer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BerldPokerServer.View
{
    public partial class FormMain : Form
    {
        #region Fields

        private UtilityServer _server;
        private List<string> _connectedClients = new List<string>();
        bool _isShown = true;

        #endregion

        #region Initialization

        public FormMain(bool isHidden)
        {
            InitializeServer();
            InitializeUI();

            if (isHidden)
            {
                _isShown = false;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(_isShown);
        }

        private void InitializeServer()
        {
            IPAddress address;
            string ip = "";

            try
            {
                ip = File.ReadAllText("ip.txt");
            }
            catch
            { }


            if (!IPAddress.TryParse(ip, out address))
            {
                ip = UtilityServer.GetLocalIp().ToString();
            }

            try
            {
                _server = new UtilityServer(ip, 3535, $"Server{Environment.MachineName}");
                _server.ClientConnected += OnClientConnected;
                _server.ClientDisconnected += OnClientDisconnected;
                _server.ReceivedMessage += OnMessageReceived;
                _server.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }

        private void InitializeUI()
        {
            InitializeComponent();
            Icon = Resources.Server;
            Text = $"BerldPoker Server | {_server.IP}:{_server.Port}";
        }

        #endregion

        #region Event methods

        private void OnMessageReceived(string userNameSource, string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    OnMessageReceived(userNameSource, message);
                });

                return;
            }

            string[] args = message.Split(';');
            PokerPlayer player;
            PokerTable table;

            PokerPlayer currentPlayer = PokerSalon.Players.FirstOrDefault(c => c.Name == userNameSource);
            PokerTable affectedTable = PokerSalon.Tables.FirstOrDefault(c => c.Players.Contains(currentPlayer));
            int playerIndex = -1;

            if (currentPlayer != null)
            {
                playerIndex = affectedTable.Players.IndexOf(currentPlayer);
            }

            switch (args[0])
            {
                case "GetTableInfo":

                    string tableInfo = "TableInfo";

                    for (int i = 0; i < PokerSalon.Tables.Count; i++)
                    {
                        tableInfo += $";{PokerSalon.Tables[i].ToString()}";
                    }

                    _server.SendMessage(userNameSource, tableInfo);

                    break;

                case "CreateNewTable":

                    table = new PokerTable();
                    player = new PokerPlayer(userNameSource);
                    table.SeatPlayer(player);
                    PokerSalon.Tables.Add(table);
                    UpdateTableListBox();

                    _server.SendMessage(userNameSource, "TableCreated");
                    break;

                case "JoinTable":

                    int tableIndex;

                    if (int.TryParse(args[1], out tableIndex))
                    {
                        if (tableIndex >= 0 && tableIndex < PokerSalon.Tables.Count)
                        {
                            if (PokerSalon.Tables[tableIndex].ToAct >= 0)
                            {
                                _server.SendMessage(userNameSource, "TableInProgressError");
                                return;
                            }

                            if (PokerSalon.Tables[tableIndex].HasFreeSeats)
                            {
                                for (int i = 0; i < PokerSalon.Tables[tableIndex].Players.Count; i++)
                                {
                                    string name = PokerSalon.Tables[tableIndex].Players[i].Name;
                                    _server.SendMessage(name, $"NewPlayer;{userNameSource}");
                                }

                                player = new PokerPlayer(userNameSource);

                                string tableData = $"TableJoined;{Serialize(PokerSalon.Tables[tableIndex])}";
                                tableData = tableData.Replace(Environment.NewLine, "");
                                _server.SendMessage(userNameSource, tableData);

                                PokerSalon.Tables[tableIndex].SeatPlayer(player);
                            }
                            else
                            {
                                _server.SendMessage(userNameSource, "TableFullError");
                                return;
                            }

                            UpdateTableListBox();
                            return;
                        }

                        _server.SendMessage(userNameSource, "TableNotExistError");
                        return;
                    }

                    _server.SendMessage(userNameSource, "InvalidOperation");

                    break;

                case "TableLeft":

                    if (currentPlayer != null)
                    {
                        SendToAllExcept(affectedTable, $"PlayerLeft;{playerIndex}", playerIndex);

                        if (affectedTable.ToAct < 0)
                        {
                            affectedTable.Players.Remove(currentPlayer);

                            if (affectedTable.Players.Count(c => c.Chips > 0) == 0)
                            {
                                affectedTable.Players.Clear();
                            }
                        }
                        else
                        {
                            currentPlayer.IsFolded = true;
                            currentPlayer.Chips = 0;

                            if (affectedTable.ToAct == playerIndex)
                            {
                                affectedTable.Fold();
                            }

                            SetNextIfOver(affectedTable);
                        }

                        if (affectedTable.Players.Count == 0)
                        {
                            UpdateTableListBox();
                            PokerSalon.Tables.Remove(affectedTable);
                        }

                        UpdateTableListBox();
                    }
                    break;

                case "Deal":

                    affectedTable.StartNewRound();

                    for (int i = 0; i < affectedTable.Players.Count; i++)
                    {
                        player = affectedTable.Players[i];

                        string cardInfo = "Deal";
                        cardInfo += $";{(int)player.Card1.Value};{(int)player.Card1.Suit}";
                        cardInfo += $";{(int)player.Card2.Value};{(int)player.Card2.Suit}";
                        _server.SendMessage(affectedTable.Players[i].Name, cardInfo);
                    }

                    break;

                case "Fold":

                    SendToAllExceptToAce(affectedTable, "Fold");
                    affectedTable.Fold();


                    SetNextIfOver(affectedTable);
                    break;

                case "CheckCall":

                    SendToAllExceptToAce(affectedTable, "CheckCall");
                    affectedTable.CheckCall();


                    SetNextIfOver(affectedTable);
                    break;

                case "BetRaise":

                    SendToAllExceptToAce(affectedTable, $"BetRaise;{args[1]}");
                    affectedTable.BetRaise(int.Parse(args[1]));


                    SetNextIfOver(affectedTable);
                    break;
            }
        }

        private void OnClientConnected(string userName)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { OnClientConnected(userName); });
                return;
            }

            AddClient(userName);
        }

        private void OnClientDisconnected(string userName)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { OnClientDisconnected(userName); });
                return;
            }

            PokerPlayer player = PokerSalon.Players.FirstOrDefault(c => c.Name == userName);

            if (player != null)
            {
                PokerTable affectedTable = PokerSalon.Tables.FirstOrDefault(c => c.Players.Contains(player));
                int playerIndex = affectedTable.Players.IndexOf(player);

                SendToAllExcept(affectedTable, $"PlayerLeft;{playerIndex}", playerIndex);

                if (affectedTable.ToAct < 0)
                {
                    affectedTable.Players.Remove(player);

                    if (affectedTable.Players.Count(c => c.Chips > 0) == 0)
                    {
                        affectedTable.Players.Clear();
                    }
                }
                else
                {
                    player.IsFolded = true;
                    player.Chips = 0;

                    if (affectedTable.ToAct == playerIndex)
                    {
                        affectedTable.Fold();
                    }

                    SetNextIfOver(affectedTable);
                }

                if (affectedTable.Players.Count == 0)
                {
                    UpdateTableListBox();
                    PokerSalon.Tables.Remove(affectedTable);
                    UpdateTableListBox();
                }
            }

            RemoveClient(userName);
        }

        private void On_buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateClientListBox();
            UpdateTableListBox();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region Other methods

        private void SendToAllExcept(PokerTable table, string message, int exception)
        {
            for (int i = 0; i < table.Players.Count; i++)
            {
                if (i == exception)
                {
                    continue;
                }

                _server.SendMessage(table.Players[i].Name, message);
            }
        }

        private void SendToAllExceptToAce(PokerTable table, string message)
        {
            for (int i = 0; i < table.Players.Count; i++)
            {
                if (i == table.ToAct)
                {
                    continue;
                }

                _server.SendMessage(table.Players[i].Name, message);
            }
        }

        private void SendToAll(PokerTable table, string message)
        {
            for (int i = 0; i < table.Players.Count; i++)
            {
                _server.SendMessage(table.Players[i].Name, message);
            }
        }

        private void SetNextIfOver(PokerTable table)
        {
            if (table.ToAct != -1)
            {
                return;
            }

            if(table.Players.Count(c => c.IsFolded == false) == 0)
            {
                PokerSalon.Tables.Remove(table);
            }

            bool isEarly;
            int state = table.SetAfterDealer(out isEarly);
            string cardInfo = "";

            if (state == 0)
            {
                cardInfo = "Flop";
                cardInfo += $";{(int)table.Flop[0].Value};{(int)table.Flop[0].Suit}";
                cardInfo += $";{(int)table.Flop[1].Value};{(int)table.Flop[1].Suit}";
                cardInfo += $";{(int)table.Flop[2].Value};{(int)table.Flop[2].Suit}";

                SendToAll(table, cardInfo);

                if (!isEarly) return;
            }

            if (state <= 1)
            {
                cardInfo = $"Turn;{(int)table.Turn.Value};{(int)table.Turn.Suit}";
                SendToAll(table, cardInfo);

                if (!isEarly) return;
            }

            if (state <= 2)
            {
                cardInfo = $"River;{(int)table.River.Value};{(int)table.River.Suit}";
                SendToAll(table, cardInfo);

                if (!isEarly) return;
            }

            if (state <= 3)
            {
                PokerPlayer[] showDownPlayers = table.Players.Where(c => c.IsFolded == false).ToArray();
                IHandValue[] showDownValues = new IHandValue[showDownPlayers.Length];

                if(showDownPlayers.Length == 0)
                {
                    PokerSalon.Tables.Remove(table);
                    return;
                }

                for (int i = 0; i < showDownValues.Length; i++)
                {
                    Card[] cards = new Card[7]
                    {
                        showDownPlayers[i].Card1,
                        showDownPlayers[i].Card2,
                        table.Flop[0],
                        table.Flop[1],
                        table.Flop[2],
                        table.Turn,
                        table.River
                    };

                    showDownValues[i] = PokerEngine.GetHandValue(cards);
                }

                List<List<IHandValue>> valueRanking = PokerEngine.GetRanking(showDownValues);
                List<List<int>> indexRanking = new List<List<int>>();

                for (int i = 0; i < valueRanking.Count; i++)
                {
                    indexRanking.Add(new List<int>());
                }

                for (int allI = 0; allI < showDownValues.Length; allI++)
                {
                    for (int i = 0; i < valueRanking.Count; i++)
                    {
                        if (valueRanking[i].Contains(showDownValues[allI]))
                        {
                            indexRanking[i].Add(table.Players.IndexOf(showDownPlayers[allI]));
                            break;
                        }
                    }
                }

                table.Finish(indexRanking, Array.ConvertAll(showDownValues, c => c.ToString()));

                string winnerInfo = "Finish;";

                winnerInfo += Serialize(indexRanking);
                winnerInfo = winnerInfo.Replace(Environment.NewLine, "");
                winnerInfo += ";";

                for (int i = 0; i < showDownPlayers.Length; i++)
                {
                    if (i == showDownPlayers.Length - 1)
                    {
                        winnerInfo += $"{showDownValues[i].ToString()}|{(int)showDownPlayers[i].Card1.Value}|{(int)showDownPlayers[i].Card1.Suit}|{(int)showDownPlayers[i].Card2.Value}|{(int)showDownPlayers[i].Card2.Suit}";
                        break;
                    }

                    winnerInfo += $"{showDownValues[i].ToString()}|{(int)showDownPlayers[i].Card1.Value}|{(int)showDownPlayers[i].Card1.Suit}|{(int)showDownPlayers[i].Card2.Value}|{(int)showDownPlayers[i].Card2.Suit}|";
                }

                SendToAll(table, winnerInfo);
            }
        }

        private string Serialize<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        private void AddClient(string userName)
        {
            _connectedClients.Add(userName);
            UpdateClientListBox();
        }

        private void RemoveClient(string userName)
        {
            _connectedClients.Remove(userName);
            UpdateClientListBox();
        }

        private void UpdateClientListBox()
        {
            _listBoxConnectedClients.Items.Clear();

            for (int i = 0; i < _connectedClients.Count; i++)
            {
                _listBoxConnectedClients.Items.Add($"Player {i}  {_connectedClients[i]}");
            }
        }

        private void UpdateTableListBox()
        {
            _listBoxTables.Items.Clear();

            foreach (PokerTable table in PokerSalon.Tables)
            {
                _listBoxTables.Items.Add(table.ToString());
            }
        }

        #endregion
    }
}
