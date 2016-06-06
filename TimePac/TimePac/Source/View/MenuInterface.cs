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

            _labelTitle.Font = new Font("Emulogic", 30F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _buttonStart.Font = new Font("Emulogic", 13.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _buttonHighScores.Font = new Font("Emulogic", 13.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _buttonAbout.Font = new Font("Emulogic", 13.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _buttonBack.Font = new Font("Emulogic", 13.0F, FontStyle.Regular, GraphicsUnit.Point, 0);


            _labelTitle.Text = $"Hello {(Parent as FormMain).UserName}!";
        }

        private void MenuInterfaceResize(object sender, EventArgs e)
        {
            float size = (float)(Width / 26.67);

            if (size > 45)
            {
                size = 45;
            }

            _labelTitle.Height = Height / 4;
            _labelTitle.Location = new Point(0, 0);

            _labelTitle.Font = new Font(_labelTitle.Font.FontFamily, size);
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
                builder.AppendLine("#" + rank + " " + score.Name + " with " + ((int)score.Time).ToString());
                rank++;
            }

            MessageBox.Show(builder.ToString(), "Record-List");
        }
    }
}
