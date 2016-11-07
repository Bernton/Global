using SVGCardRender.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVGCardRender
{
    public partial class FormMain : Form
    {
        private Random _random = new Random();
        private CardImageProvider _cardImageProvider = new CardImageProvider(Resources.CardSprite_SpecialColor_HighRes);


        public FormMain()
        {
            InitializeComponent();
            DisplayRandomCard();
        }


        private void OnPictureBoxClick(object sender, EventArgs e)
        {
            DisplayRandomCard();
        }

        private void OnFormMainKeyDown(object sender, KeyEventArgs e)
        {
            DisplayRandomCard();
        }

        private void DisplayRandomCard()
        {
            _pictureBox.Image = _cardImageProvider.GetCard(_random.Next(14), _random.Next(4));
        }
    }
}
