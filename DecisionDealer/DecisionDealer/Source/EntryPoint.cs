using DecisionDealer.View;
using System;
using System.Windows.Forms;

namespace DecisionDealer
{
    public static class EntryPoint
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
