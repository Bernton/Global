using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BerldPokerServer.NetworkUtilities
{
    public class UtilityServer
    {
        #region Nested types

        public delegate void ClientConnectedDefinition(string userName);
        public delegate void ClientDisconnectedDefinition(string userName);
        public delegate void ClientMessageReceived(string userName, string message);

        #endregion

        #region Fields

        private bool _isServerRunning;
        private readonly string _serverName;
        private readonly ushort _port;
        private readonly IPAddress _ip;
        private Thread _clientHandler;
        private TcpListener _tcpListener;
        private List<UtilityUser> _users = new List<UtilityUser>();

        #endregion

        #region Events and Properties

        /// <summary>
        /// Triggers when client connected to the server
        /// </summary>
        public event ClientConnectedDefinition ClientConnected;

        /// <summary>
        /// Triggers when client disconnected from the server
        /// </summary>
        public event ClientDisconnectedDefinition ClientDisconnected;

        /// <summary>
        /// Triggers when client wants to send message
        /// </summary>
        public event ClientMessageReceived ReceivedMessage;

        public IPAddress IP
        {
            get { return _ip; }
        }

        public ushort Port
        {
            get { return _port; }
        }

        /// <summary>
        /// Read only. Returns the name of the server
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
        }

        /// <summary>
        /// Read only. Returns bool that determines if server is running or not
        /// </summary>
        public bool IsRunning
        {
            get { return _isServerRunning; }
        }

        #endregion

        #region Initializing


        /// <summary>
        /// Creates a new instance of UtilityServer, takes name only
        /// </summary>
        /// <param name="serverName"></param>
        public UtilityServer(string serverName)
        {
            _ip = GetLocalIp();
            _port = (ushort)GetFreeTcpPort();
            _serverName = serverName;
        }

        /// <summary>
        /// Creates a new instance of UtilityServer, takes the local ip as socket ip-address.
        /// </summary>
        /// <param name="port">Port of the socket</param>
        /// <param name="serverName">Name of the server (Can not be changed afterwards)</param>
        public UtilityServer(ushort port, string serverName)
        {
            _ip = GetLocalIp();
            _port = port;
            _serverName = serverName;
        }

        /// <summary>
        /// Creates a new instance of UtilityServer.
        /// </summary>
        /// <param name="ip">Ip-address of the socket</param>
        /// <param name="port">Port of the socket</param>
        /// <param name="serverName">Name of the server (Can not be changed afterwards)</param>
        public UtilityServer(string ip, ushort port, string serverName)
        {
            // Check if given ip is valid
            if (!(IPAddress.TryParse(ip, out _ip))) throw new ArgumentException();

            _port = port;
            _serverName = serverName;
        }

        #endregion

        #region Server operation methods

        /// <summary>
        /// Starts the server and bind the socket.
        /// </summary>
        public void Start()
        {
            if (!_isServerRunning)
            {
                try
                {
                    // Bind socket
                    _tcpListener = new TcpListener(_ip, _port);
                    _tcpListener.Start();
                    _isServerRunning = true;

                    // Start client handler
                    _clientHandler = new Thread(() => ClientHandler());
                    _clientHandler.IsBackground = true;
                    _clientHandler.Start();
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Closes the server and releases the socket.
        /// </summary>
        public void Close()
        {
            if (_isServerRunning)
            {
                _tcpListener.Stop();
                _tcpListener = null;

                _isServerRunning = false;

                if (_clientHandler != null) _clientHandler.Abort();
                _clientHandler = null;
            }
        }

        /// <summary>
        /// Sends a message to a connected client.
        /// </summary>
        /// <param name="destinationUserName">Username of client</param>
        /// <param name="message">Message to send</param>
        public void SendMessage(string destinationUserName, string message)
        {
            if (_isServerRunning)
            {
                string toSend = "INCOMING_MSG" + ";" + _serverName + ";" + message;
                UtilityUser destination = _users.Where(c => c.UserName.Equals(destinationUserName)).Select(c => c).FirstOrDefault();

                if (destination == null)
                {
                    Thread.Sleep(100);
                    destination = _users.Where(c => c.UserName.Equals(destinationUserName)).Select(c => c).FirstOrDefault();

                    if (destination == null)
                    {
                        return;
                    }
                }

                try
                {
                    StreamWriter clientWriter = new StreamWriter(destination.Client.GetStream());
                    clientWriter.WriteLine(toSend);
                    clientWriter.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Sends a message to all connected clients.
        /// </summary>
        /// <param name="message">Message to send</param>
        public void SendMessageToAll(string message)
        {
            if (_isServerRunning)
            {
                string toSend = "INCOMING_MSG" + ";" + _serverName + ";" + message;

                foreach (UtilityUser user in _users)
                {
                    StreamWriter clientWriter = new StreamWriter(user.Client.GetStream());
                    clientWriter.WriteLine(toSend);
                    clientWriter.Flush();
                }
            }
        }

        #endregion

        #region Thread methods

        /// <summary>
        /// Handles incoming connection request
        /// </summary>
        private void ClientHandler()
        {
            while (_isServerRunning)
            {
                TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                Thread clientConnectionHandler = new Thread(() => ClientConnectionHandler(tcpClient));
                clientConnectionHandler.Start();
            }
        }

        /// <summary>
        /// Handles the individual client connections
        /// </summary>
        /// <param name="tcpClient">TcpClient who connected to tcp socket</param>
        private void ClientConnectionHandler(TcpClient tcpClient)
        {
            UtilityUser sourceUser = null;

            try
            {
                Stream tcpClientStream = tcpClient.GetStream();
                StreamWriter strWriter = new StreamWriter(tcpClientStream);
                StreamReader strReader = new StreamReader(tcpClientStream);

                string clientMessage = strReader.ReadLine();
                string[] response = clientMessage.Split(';');

                if (response[0] == "CONNECT_REQUEST")
                {
                    string userName = response[1];
                    sourceUser = new UtilityUser(tcpClient, userName);

                    if (!_users.TrueForAll(c => c.UserName != userName) || userName == _serverName)
                    {
                        strWriter.WriteLine("ERROR" + ";" + _serverName + ";" + "Name is taken!");
                        strWriter.Flush();

                        strWriter.WriteLine("DISCONNECTED");
                        strWriter.Flush();

                        tcpClient.Close();

                        return;
                    }

                    strWriter.WriteLine("CONNECTED");
                    strWriter.Flush();

                    _users.Add(sourceUser);
                    if (ClientConnected != null) ClientConnected(sourceUser.UserName);
                }
                else
                {
                    tcpClient.Close();
                    return;
                }

                while (_isServerRunning)
                {
                    clientMessage = strReader.ReadLine();
                    response = clientMessage.Split(';');

                    if (response[0] == "LEAVE_REQUEST" && response.Length == 1)
                    {
                        _users.Remove(sourceUser);
                        if (ClientDisconnected != null) ClientDisconnected(sourceUser.UserName);

                        return;
                    }
                    else if (response[0] == "SEND_MSG")
                    {
                        string message = clientMessage.Substring(response[0].Length + 1);
                        ReceivedMessage?.Invoke(sourceUser.UserName, message);
                    }
                }
            }
            catch (Exception ex)
            {
                Type type = ex.GetType();

                if (type == typeof(IOException) || type == typeof(NullReferenceException))
                {
                    if (sourceUser == null) sourceUser = _users.FirstOrDefault(c => c.Client.Equals(tcpClient));

                    if (ClientDisconnected != null) ClientDisconnected(sourceUser.UserName);
                    _users.Remove(sourceUser);
                }

                else throw ex;
            }
        }

        #endregion

        #region Other methods

        /// <summary>
        /// Returns the local ip as IPAddress
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIp()
        {
            IPHostEntry ipHostEntry;
            ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in ipHostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            return IPAddress.Parse("127.0.0.1");
        }

        /// <summary>
        /// Returns next available tcp port
        /// </summary>
        /// <returns></returns>
        public static int GetFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        #endregion
    }
}
