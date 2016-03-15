using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BerldPokerClient.NetworkUtilities
{
    public class UtilityClient
    {
        #region Class definitions

        public delegate void ReceivedMessageDefinition(string source, string message);
        public delegate void ReceivedServerErrorDefinition(string source, string message);

        #endregion

        #region Fields, properties and events

        public event ReceivedMessageDefinition ReceivedMessage;
        public event ReceivedServerErrorDefinition ReceivedServerError;

        private TcpClient _tcpClient;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        private StreamWriter _strWriter;
        private StreamReader _strReader;

        private Thread _incomingMessageHandler;

        private IPAddress _ipServer;
        private ushort _portServer;

        private string _userName;

        private bool _isConnectedToServer;

        public string UserName
        {
            get { return _userName; }
        }

        public bool IsConnected
        {
            get { return _isConnectedToServer; }
        }

        #endregion

        #region Initializing

        public UtilityClient(string userName)
        {
            _userName = userName;
        }

        #endregion

        #region Client operation methods

        public void Connect(string ipServer, ushort portServer)
        {
            try
            {
                if (!_isConnectedToServer)
                {
                    if (!(IPAddress.TryParse(ipServer, out _ipServer))) throw new ArgumentException();
                    _portServer = portServer;

                    string toSend = "CONNECT_REQUEST" + ";" + _userName;

                    _tcpClient = new TcpClient();
                    _tcpClient.Connect(ipServer, portServer);

                    _strWriter = new StreamWriter(_tcpClient.GetStream());
                    _strWriter.WriteLine(toSend);
                    _strWriter.Flush();

                    _strReader = new StreamReader(_tcpClient.GetStream());

                    _isConnectedToServer = true;

                    ReceiveMessages(true);

                    _incomingMessageHandler = new Thread(() => ReceiveMessages());
                    _incomingMessageHandler.IsBackground = true;
                    _incomingMessageHandler.Start();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            if (_isConnectedToServer)
            {
                try
                {
                    string toSend = "LEAVE_REQUEST";

                    _strWriter.WriteLine(toSend);
                    _strWriter.Flush();

                    _tcpClient.Close();
                    _tcpClient = null;
                    _strWriter = null;
                    _strReader = null;
                    _isConnectedToServer = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Environment.Exit(0);
                }
            }
        }

        public void SendMessage(string message)
        {
            if (_isConnectedToServer)
            {
                string toSend = "SEND_MSG" + ";" + message;

                _strWriter.WriteLine(toSend);
                _strWriter.Flush();
            }
        }

        #endregion

        #region Thread methods

        private void ReceiveMessages(bool isConnecting = false)
        {
            while (_isConnectedToServer)
            {
                string serverMessage;

                try
                {
                    serverMessage = _strReader.ReadLine();

                    if (serverMessage == null)
                    {
                        Disconnect();
                        return;
                    }
                }
                catch
                {
                    Disconnect();
                    return;
                }

                string[] response = serverMessage.Split(';');

                if (response[0] == "INCOMING_MSG")
                {
                    string source = response[1];
                    string message = serverMessage.Substring(response[0].Length + source.Length + 2);

                    ReceivedMessage?.Invoke(source, message);
                }
                else if (response[0] == "ERROR")
                {
                    string sourceServer = response[1];
                    string errorMessage = response[2];

                    if (ReceivedServerError != null) ReceivedServerError(sourceServer, errorMessage);
                }
                else if (response[0] == "CONNECTED")
                {
                    if (isConnecting == true)
                    {
                        return;
                    }
                }

                if (serverMessage == "DISCONNECTED")
                    Disconnect();
            }
        }

        #endregion
    }
}
