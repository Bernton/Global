using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TimePac
{
    public partial class FormMain : Form
    {
        public List<Score> HighScores { get; set; }

        private string _userName;

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        private Control ActiveInterface
        {
            get
            {
                return Controls[0];
            }
        }

        public FormMain()
        {
            if (File.Exists("scores.dat"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));
                FileStream stream = new FileStream("scores.dat", FileMode.Open);
                HighScores = (List<Score>)serializer.Deserialize(stream);

                stream.Dispose();
            }
            else
            {
                HighScores = new List<Score>();
            }

            InitializeComponent();
            SwitchInterface(new UsernameInterface(this, ""));
        }

        public void SwitchInterface(UserControl interfaceControl)
        {
            Controls.Add(interfaceControl);

            if (Controls.Count == 2)
            {
                Controls.RemoveAt(0);
            }

            InitializeInterface();
        }

        private void InitializeInterface()
        {
            ActiveInterface.Size = ClientSize;
        }

        private void FormMainResize(object sender, EventArgs e)
        {
            ActiveInterface.Size = ClientSize;
        }

        private void OnFormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));
            FileStream stream = new FileStream("scores.dat", FileMode.Create);
            serializer.Serialize(stream, HighScores);

            stream.Dispose();
        }
    }
}
