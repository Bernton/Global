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
    public partial class MenuInterface : UserControl
    {
        public MenuInterface(FormMain parent)
        {
            Parent = parent;

            InitializeComponent();
            _labelUsername.Text = $"Hello {(Parent as FormMain).UserName}!";
        }

        private void MenuInterfaceResize(object sender, EventArgs e)
        {
            //_buttonStart.Location = new Point(Width / 2 - _buttonStart.Width / 2, _buttonStart.Location.Y);
            //_buttonStart.Width = (int)Math.Round(Width * 0.3125, 0);
        }

        private void OnButtonBackClick(object sender, EventArgs e)
        {
            FormMain parent = (FormMain)Parent;
            parent.SwitchInterface(new UsernameInterface(parent, parent.UserName));
        }

        private void OnButtonAboutClick(object sender, EventArgs e)
        {
            ((FormMain)Parent).SwitchInterface(new AboutInterface(((FormMain)Parent)));
        }

        private void OnButtonStartClick(object sender, EventArgs e)
        {
            ((FormMain)Parent).SwitchInterface(new GameInterface(((FormMain)Parent)));
        }

        private void OnButtonHighScoresClick(object sender, EventArgs e)
        {
            ((FormMain)Parent).HighScores = ((FormMain)Parent).HighScores.OrderBy(c => c.Time).ToList();

            StringBuilder builder = new StringBuilder();

            int rank = 1;

            foreach (Score score in ((FormMain)Parent).HighScores)
            {
                builder.AppendLine("#" + rank + " " + score.Name + " mit " + ((int)score.Time).ToString());
                rank++;
            }

            MessageBox.Show(builder.ToString(), "Rekord-Liste");
        }
    }
}
