using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BerldPoker
{
    public partial class FormHandStatistics : Form
    {
        public FormHandStatistics(HandRanking ranking)
        {
            InitializeComponent();

            HandRanking sorted = new HandRanking();
            sorted.Hands = ranking.Hands.OrderByDescending(c => c.RatioPercent).ToList();

            double sum = sorted.Hands.Sum(c => c.Count);

            Text = "Hand Statistics (" + sum + " Hands)";

            for (int i = 0; i < sorted.Hands.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell rank = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell hand = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell won = new DataGridViewTextBoxCell();

                rank.Value = i + 1;
                hand.Value = sorted.Hands[i].ToString();
                won.Value = "(" + Math.Round(sorted.Hands[i].RatioPercent, 3) + "%) " + sorted.Hands[i].Won + " / " + sorted.Hands[i].Count;

                row.Cells.Add(rank);
                row.Cells.Add(hand);
                row.Cells.Add(won);

                _dataGridView.Rows.Add(row);
            }

            _dataGridView.Sort(_dataGridView.Columns[0], ListSortDirection.Ascending);
        }
    }
}
