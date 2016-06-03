using System;
using System.Windows.Forms;

namespace TimePac
{
    public partial class UsernameInterface : UserControl
    {
        public UsernameInterface(FormMain parent, string userName)
        {
            Parent = parent;

            Parent.KeyDown += new KeyEventHandler(OnUsernameInterfaceKeyDown);
            InitializeComponent();

            _textBoxUsername.Text = userName;
        }

        private void OnButtonStartClick(object sender, EventArgs e)
        {
            GoToMenu();
        }

        private void OnUsernameInterfaceKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _textBoxUsername.Text.Length > 2)
            {
                GoToMenu();
            }
        }

        private void GoToMenu()
        {
            FormMain parent = (FormMain)Parent;

            parent.KeyDown -= new KeyEventHandler(OnUsernameInterfaceKeyDown);
            parent.UserName = _textBoxUsername.Text;
            parent.SwitchInterface(new MenuInterface(parent));
        }
    }
}
