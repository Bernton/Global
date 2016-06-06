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
            float size = (float)(Width / 22.22);

            if (size > 70)
            {
                size = 70;
            }

            _labelTitle.Height = Height / 3;
            _labelTitle.Location = new Point(0, 0);

            _labelTitle.Font = new Font(_labelTitle.Font.FontFamily, size);
        }

        private void OnButtonBack_Click(object sender, EventArgs e)
        {
            ((FormMain)Parent).SwitchInterface(new MenuInterface((FormMain)Parent));
        }
    }
}
