using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimePac
{
    public partial class AboutInterface : UserControl
    {
        public AboutInterface(FormMain parent)
        {
            Parent = parent;

            InitializeComponent();
        }

        private void AboutInterfaceResize(object sender, EventArgs e)
        {
            //_buttonStart.Location = new Point(Width / 2 - _buttonStart.Width / 2, _buttonStart.Location.Y);
            //_buttonStart.Width = (int)Math.Round(Width * 0.3125, 0);
        }

        private void OnButtonBack_Click(object sender, EventArgs e)
        {
            ((FormMain)Parent).SwitchInterface(new MenuInterface((FormMain)Parent));
        }
    }
}
