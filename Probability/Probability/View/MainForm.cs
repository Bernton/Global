using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Probability.Model;
using System.Numerics;
using System.Globalization;

namespace Probability.View
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            _textBoxN.ContextMenu = new ContextMenu();
            _textBoxR.ContextMenu = new ContextMenu();
        }

        private void OnButtonCalculateClick(object sender, EventArgs e)
        {
            BigInteger result;
            int n = int.Parse(_textBoxN.Text);
            int r = int.Parse(_textBoxR.Text);

            if(_checkBoxOrder.Checked)
            {
                result = Prob.Permutation(n, r, _checkBoxRepitition.Checked);
            }
            else
            {
                result = Prob.Combination(n, r, _checkBoxRepitition.Checked);
            }

            _richTextBoxResult.Text = result.ToString("N0", Application.CurrentCulture);
            _richTextBoxResultSci.Text = result.ToString("0.###E+0", Application.CurrentCulture);

            toolTip.SetToolTip(_richTextBoxResult, _richTextBoxResult.Text);
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                return;
            }

            if (((TextBox)sender).Text.Length > 3)
            {
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                return;
            }

            if(e.KeyValue > 95 && e.KeyValue < 106)
            {
                return;
            }

            e.SuppressKeyPress = true;
        }
    }
}
