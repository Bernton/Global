using System.Net.Sockets;

namespace BerldPokerServer.NetworkUtilities
{
    public class UtilityUser
    {
        #region Global variables and properties

        // TcpClient where all the transmission is done
        private TcpClient _tcpClient;

        // Name of the user
        private string _userName;

        public TcpClient Client // Read only
        {
            get { return _tcpClient; }
        }

        public string UserName // Read only
        {
            get { return _userName; }
        }

        #endregion

        #region Initializing

        /// <summary>
        /// Creates a new instance of UtilityUser.
        /// </summary>
        /// <param name="client">TcpClient of the user</param>
        /// <param name="userName">Name of the user</param>
        public UtilityUser(TcpClient client, string userName)
        {
            _tcpClient = client;
            _userName = userName;
        }

        #endregion
    }
}
