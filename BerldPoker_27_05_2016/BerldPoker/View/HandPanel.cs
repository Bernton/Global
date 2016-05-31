using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BerldPoker
{
    public class HandPanel : Panel
    {
        private bool _isWinner;

        public bool IsWinner
        {
            get
            {
                return _isWinner;
            }

            set
            {
                _isWinner = value;

                if (value)
                {
                    BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    BorderStyle = BorderStyle.None;
                }
            }
        }

        public int HandIndex { get; private set; }

        private Label _labelCard1;
        private Label _labelCard2;
        private Label _labelHandNumber;
        private Label _labelHandValue;
        private PictureBox _pictureBoxCard1;
        private PictureBox _pictureBoxCard2;

        public HandPanel(Card card1, Card card2, IHandValue value, int handIndex)
        {
            InitializeComponent();
            HandIndex = handIndex;

            Size = new Size(240, 230);

            _labelCard1.Text = card1.ToString();
            _labelCard2.Text = card2.ToString();
            _labelHandNumber.Text = "Hand " + (handIndex + 1);
            _pictureBoxCard1.Image = CardImageProvider.GetImage(card1);
            _pictureBoxCard2.Image = CardImageProvider.GetImage(card2);

            _labelHandValue.Text = value.ToString();
        }

        private void InitializeComponent()
        {
            _labelCard1 = new Label();
            _labelCard2 = new Label();
            _labelHandNumber = new Label();
            _labelHandValue = new Label();
            _pictureBoxCard1 = new PictureBox();
            _pictureBoxCard2 = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)(_pictureBoxCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_pictureBoxCard1)).BeginInit();
            SuspendLayout();
            // 
            // _pictureBoxCard2
            // 
            _pictureBoxCard2.Location = new System.Drawing.Point(132, 40);
            _pictureBoxCard2.Name = "_pictureBoxCard2";
            _pictureBoxCard2.Size = new System.Drawing.Size(79, 124);
            _pictureBoxCard2.TabIndex = 28;
            _pictureBoxCard2.TabStop = false;
            // 
            // _labelCard1
            // 
            _labelCard1.AutoSize = false;
            _labelCard1.Location = new Point(14, 170);
            _labelCard1.Name = "_labelCard1";
            _labelCard1.Size = new Size(104, 15);
            _labelCard1.TabIndex = 0;
            _labelCard1.Text = "label1";
            _labelCard1.TextAlign = ContentAlignment.MiddleCenter;
            _labelCard1.TextChanged += new System.EventHandler(this.labels_TextChanged);
            // 
            // _labelCard2
            // 
            _labelCard2.AutoSize = false;
            _labelCard2.Location = new System.Drawing.Point(119, 170);
            _labelCard2.Name = "_labelCard2";
            _labelCard2.Size = new System.Drawing.Size(104, 15);
            _labelCard2.TabIndex = 1;
            _labelCard2.TextAlign = ContentAlignment.MiddleCenter;
            _labelCard2.Text = "label2";
            _labelCard2.TextChanged += new System.EventHandler(this.labels_TextChanged);
            // 
            // _labelHandNumber
            // 
            _labelHandNumber.AutoSize = true;
            _labelHandNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _labelHandNumber.Location = new System.Drawing.Point(84, 10);
            _labelHandNumber.Name = "_labelHandNumber";
            _labelHandNumber.Size = new System.Drawing.Size(57, 16);
            _labelHandNumber.TabIndex = 12;
            _labelHandNumber.Text = "Hand 1";
            _labelHandNumber.TextChanged += new System.EventHandler(this.labels_TextChanged);
            // 
            // _labelHandValue
            // 
            _labelHandValue.AutoSize = false;
            _labelHandValue.Font = new System.Drawing.Font("Monotype Corsiva", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _labelHandValue.Location = new System.Drawing.Point(5, 200);
            _labelHandValue.Name = "_labelHandValue";
            _labelHandValue.Size = new System.Drawing.Size(230, 20);
            _labelHandValue.TabIndex = 19;
            _labelHandValue.Text = "label19";
            _labelHandValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _labelHandValue.TextChanged += new System.EventHandler(this.labels_TextChanged);
            // 
            // _pictureBoxCard1
            // 
            _pictureBoxCard1.Location = new System.Drawing.Point(27, 40);
            _pictureBoxCard1.Name = "_pictureBoxCard1";
            _pictureBoxCard1.Size = new System.Drawing.Size(79, 124);
            _pictureBoxCard1.TabIndex = 27;
            _pictureBoxCard1.TabStop = false;
            ((System.ComponentModel.ISupportInitialize)(_pictureBoxCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_pictureBoxCard1)).EndInit();
            ResumeLayout(false);

            Controls.Add(_labelCard1);
            Controls.Add(_labelCard2);
            Controls.Add(_labelHandNumber);
            Controls.Add(_labelHandValue);
            Controls.Add(_pictureBoxCard1);
            Controls.Add(_pictureBoxCard2);
        }

        private void labels_TextChanged(object sender, EventArgs e)
        {
            Label currentLabel = sender as Label;

            while (currentLabel.Width < TextRenderer.MeasureText(currentLabel.Text, new Font(currentLabel.Font.FontFamily, currentLabel.Font.Size, currentLabel.Font.Style)).Width)
            {
                currentLabel.Font = new Font(currentLabel.Font.FontFamily, currentLabel.Font.Size - 0.5f, currentLabel.Font.Style);
            }
        }
    }
}
