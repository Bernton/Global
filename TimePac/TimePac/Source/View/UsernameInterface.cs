using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
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


            _labelUsername.Font = new Font("Emulogic", 30F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _buttonStart.Font = new Font("Emulogic", 13.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _textBoxUsername.Font = new Font("Emulogic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        
            _textBoxUsername.Text = userName;
        }

        private void OnButtonStartClick(object sender, EventArgs e)
        {
            if (_textBoxUsername.Text.Length > 2)
            {
                if (Regex.IsMatch(_textBoxUsername.Text, @"^[a-zA-Z]+$"))
                {
                    GoToMenu();
                }
                else
                {
                    MessageBox.Show(this, "Username must contain letters only.", "TimePac", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Username must contain more than 2 letters.", "TimePac", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnUsernameInterfaceKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnButtonStartClick(null, null);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Environment.Exit(0);
            }
        }

        private void GoToMenu()
        {
            FormMain parent = (FormMain)Parent;

            parent.KeyDown -= new KeyEventHandler(OnUsernameInterfaceKeyDown);
            parent.UserName = _textBoxUsername.Text.ToUpper();
            parent.SwitchInterface(new MenuInterface(parent));
        }

        private void OnUsernameInterfaceResize(object sender, EventArgs e)
        {
            float size = (float)(Width / 26.67);

            if(size > 45)
            {
                size = 45;
            }

            _labelUsername.Height = Height / 3;
            _labelUsername.Location = new Point(0, 0);

            _labelUsername.Font = new Font(_labelUsername.Font.FontFamily, size);
        }

        private void OnTextBoxUsernameTextChanged(object sender, EventArgs e)
        {
            _textBoxUsername.Text = Regex.Replace(_textBoxUsername.Text , @"\s+", "");
        }
    }
}
