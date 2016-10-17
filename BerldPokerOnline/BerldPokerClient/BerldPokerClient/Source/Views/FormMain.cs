using BerldPokerClient.NetworkUtilities;
using BerldPokerClient.Poker;
using BerldPokerClient.Properties;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BerldPokerClient.Views
{
    public partial class FormMain : Form
    {
        #region Fields

        private FormTable _table;
        private UtilityClient _client;

        private string _userName;

        #endregion

        #region Initialization

        public FormMain()
        {
            PromptUserName();
            InitializeClient();
            InitializeUI();
        }

        private void PromptUserName()
        {
            string input = Interaction.InputBox("Please enter a username.", "Berld Poker Client", "Guest" + (new Random()).Next(1000));

            if(string.IsNullOrWhiteSpace(input))
            {
                Environment.Exit(0);
            }

            while (input.Length < 3)
            {
                input = Interaction.InputBox("Invalid username. Please enter a new username.", "Berld Poker Client");

                if (string.IsNullOrWhiteSpace(input))
                {
                    Environment.Exit(0);
                }
            }

            _userName = input;
        }

        private void InitializeClient()
        {
            try
            {
                string fileIP = File.ReadAllText("ip.txt");
                IPAddress address;

                if (IPAddress.TryParse(fileIP, out address))
                {
                    _client = new UtilityClient(_userName);
                    _client.ReceivedMessage += OnServerMessageReceived;
                    _client.ReceivedServerError += OnServerErrorReceived;

                    _client.Connect(address.ToString(), 3535);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
        }

        private void InitializeUI()
        {
            InitializeComponent();
            Icon = Resources.Favicon;
        }

        #endregion

        #region Event methods

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
                case "ERROR":
                    OnServerErrorReceived(source, args[1]);
                    break;

                case "InvalidOperation":
                    MessageBox.Show("Invalid operation.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Enabled = true;
                    break;

                case "TableFullError":
                    MessageBox.Show("Selected table is full.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Enabled = true;
                    break;

                case "TableInProgressError":
                    MessageBox.Show("Selected table has already started.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Enabled = true;
                    break;

                case "TableNotExistError":
                    MessageBox.Show("Selected table does not exist anymore.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Enabled = true;

                    On_buttonRefresh_Click(null, null);
                    break;

                case "TableJoined":

                    _client.ReceivedMessage -= OnServerMessageReceived;

                    string tableData = message.Substring(args[0].Length + 1);
                    XmlSerializer serializer = new XmlSerializer(typeof(PokerTable));
                    PokerTable table = (PokerTable)serializer.Deserialize(new StringReader(tableData));

                    Visible = false;
                    _table = new FormTable(_client, table, false, _userName);
                    _table.FormClosed += OnTableClosed;
                    _table.Show();

                    break;

                case "TableJoinedObserver":

                    _client.ReceivedMessage -= OnServerMessageReceived;

                    string tableDataObs = message.Substring(args[0].Length + 1);
                    XmlSerializer serializerObs = new XmlSerializer(typeof(PokerTable));
                    PokerTable tableObs = (PokerTable)serializerObs.Deserialize(new StringReader(tableDataObs));

                    Visible = false;
                    _table = new FormTable(_client, tableObs, true);
                    _table.FormClosed += OnTableClosed;
                    _table.Show();

                    break;

                case "TableCreated":

                    _client.ReceivedMessage -= OnServerMessageReceived;

                    Visible = false;
                    _table = new FormTable(_client, _userName);
                    _table.FormClosed += OnTableClosed;
                    _table.Show();

                    break;

                case "TableInfo":

                    _listBoxTables.Items.Clear();

                    for (int i = 1; i < args.Length; i++)
                    {
                        _listBoxTables.Items.Add(args[i]);
                    }

                    Enabled = true;
                    break;
            }
        }

        private void OnTableClosed(object sender, FormClosedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                _client.ReceivedMessage += OnServerMessageReceived;
                Enabled = true;
                Visible = true;
            });
        }

        private void OnServerErrorReceived(string source, string message)
        {
            MessageBox.Show(message, "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }

        private void OnFormTableDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.Disconnect();
            Environment.Exit(0);
        }

        private void OnFormTableDialog_Shown(object sender, EventArgs e)
        {
            _client.SendMessage("GetTableInfo");
        }

        private void On_buttonCreateNew_Click(object sender, EventArgs e)
        {
            _client.SendMessage("CreateNewTable");
            Enabled = false;
        }

        private void On_buttonRefresh_Click(object sender, EventArgs e)
        {
            _client.SendMessage("GetTableInfo");
            Enabled = false;
        }

        private void On_buttonJoinSelected_Click(object sender, EventArgs e)
        {
            if (_listBoxTables.SelectedIndex != -1)
            {
                _client.SendMessage($"JoinTable;{_listBoxTables.SelectedIndex}");
                Enabled = false;
            }
            else
            {
                MessageBox.Show("Nothing selected.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (_listBoxTables.SelectedIndex != -1)
            {
                _client.SendMessage($"JoinTableObserver;{_listBoxTables.SelectedIndex}");
                Enabled = false;
            }
            else
            {
                MessageBox.Show("Nothing selected.", "Table Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
