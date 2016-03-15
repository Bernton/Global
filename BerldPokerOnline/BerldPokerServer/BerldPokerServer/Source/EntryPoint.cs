using BerldPokerServer.View;
using System;
using System.Windows.Forms;

namespace BerldPokerServer
{
    public static class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            bool isHidden = false;

            if(args != null && args.Length > 0)
            {
                isHidden = true;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(isHidden));
        }
    }
}
