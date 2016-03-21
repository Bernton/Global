using BerldPokerClient;
using BerldPokerClient.Poker;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BerldPoker.Controls
{
    [DesignerCategory("")]
    public class HandPanel : Panel
    {
        private PictureBox _pictureBoxCard1;
        private PictureBox _pictureBoxCard2;
        private Label _labelChips;
        private Label _labelChipsInPot;
        private Label _labelName;
        private Label _labelHandValue;

        public HandPanel(PokerPlayer player, bool toAct, bool isDealer)
        {
            BackColor = Color.Transparent;
            Size = new Size(240, 230);
            Enabled = !player.IsFolded;

            InitializeComponent();

            _labelName.Text = player.Name;

            if (isDealer)
            {
                _labelName.Text += " (D)";
            }

            _labelName.FitFont();
            _labelChips.Text = player.Chips.ToString() + " $";

            if (player.Card1 != null && player.Card2 != null)
            {
                _pictureBoxCard1.Image = CardImageProvider.GetImageFromCard(player.Card1);
                _pictureBoxCard2.Image = CardImageProvider.GetImageFromCard(player.Card2);
            }
            else if (!player.IsFolded)
            {
                _pictureBoxCard1.Image = CardImageProvider.GetCardBack();
                _pictureBoxCard2.Image = CardImageProvider.GetCardBack();
            }

            if (player.ChipsInPot != 0)
            {
                _labelChipsInPot.Text = player.ChipsInPot.ToString() + " $";
            }
            else
            {
                _labelChipsInPot.Text = "";
            }

            if (player.ValueText != null && player.ValueText != "")
            {
                _labelChipsInPot.Text = player.ValueText;

                _labelChipsInPot.Font = new Font("Monotype Corsiva", 14F, FontStyle.Italic, GraphicsUnit.Point, 0);
                _labelChipsInPot.FitFont();

                if (!player.IsWinner)
                {
                    Enabled = false;
                }
            }

            if (toAct)
            {
                BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void InitializeComponent()
        {
            _pictureBoxCard1 = new PictureBox();
            _pictureBoxCard2 = new PictureBox();
            _labelChips = new Label();
            _labelChipsInPot = new Label();
            _labelName = new Label();
            _labelHandValue = new Label();

            ((ISupportInitialize)_pictureBoxCard2).BeginInit();
            ((ISupportInitialize)_pictureBoxCard1).BeginInit();

            SuspendLayout();

            _pictureBoxCard1.Location = new Point(30, 40);
            _pictureBoxCard1.Size = new Size(82, 118);
            _pictureBoxCard1.SizeMode = PictureBoxSizeMode.Zoom;

            _pictureBoxCard2.Location = new Point(129, 40);
            _pictureBoxCard2.Size = new Size(82, 118);
            _pictureBoxCard2.SizeMode = PictureBoxSizeMode.Zoom;

            _labelChips.Location = new Point(35, 172);
            _labelChips.Size = new Size(174, 25);
            _labelChips.TextAlign = ContentAlignment.MiddleCenter;
            _labelChips.Font = new Font("Arial Unicode MS", 12.5F, FontStyle.Regular, GraphicsUnit.Point, 0);

            _labelChipsInPot.Location = new Point(35, 2);
            _labelChipsInPot.Size = new Size(174, 25);
            _labelChipsInPot.TextAlign = ContentAlignment.MiddleCenter;
            _labelChipsInPot.Font = new Font("Arial Unicode MS", 12.5F, FontStyle.Bold, GraphicsUnit.Point, 0);

            _labelName.Location = new Point(30, 195);
            _labelName.Size = new Size(174, 25);
            _labelName.TextAlign = ContentAlignment.MiddleCenter;
            _labelName.Font = new Font("Arial Unicode MS", 12.5F, FontStyle.Bold, GraphicsUnit.Point, 0);

            _labelHandValue.Location = new Point(35, 2);
            _labelHandValue.Size = new Size(174, 60);
            _labelHandValue.TextAlign = ContentAlignment.TopCenter;
            _labelHandValue.Font = new Font("Arial Unicode MS", 12.5F, FontStyle.Regular, GraphicsUnit.Point, 0); //new Font("Monotype Corsiva", 12.75F, FontStyle.Italic, GraphicsUnit.Point, 0);

            ((ISupportInitialize)_pictureBoxCard2).EndInit();
            ((ISupportInitialize)_pictureBoxCard1).EndInit();

            ResumeLayout(false);

            Controls.Add(_pictureBoxCard1);
            Controls.Add(_pictureBoxCard2);
            Controls.Add(_labelChips);
            Controls.Add(_labelChipsInPot);
            Controls.Add(_labelName);
            Controls.Add(_labelHandValue);
        }
    }
}
