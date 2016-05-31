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
    public partial class FormCombStatistics : Form
    {
        public FormCombStatistics(Dictionary<Type, int[]> ranking)
        {
            InitializeComponent();

            Text = "Hand Statistics (" + ranking.Values.Sum(c => c[1]) + " Hands)";

            int sum = ranking.Values.Sum(c => c[1]);
            int winSum = ranking.Values.Sum(c => c[0]);

            foreach (KeyValuePair<Type, int[]> item in ranking)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell combination = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell winRatio = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell occurence = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell total = new DataGridViewTextBoxCell();

                combination.Value = item.Key.Name;
                winRatio.Value = Math.Round((double)item.Value[0] / (double)item.Value[1] * 100, 3);
                occurence.Value = Math.Round((double)item.Value[1] / sum * 100, 3);
                total.Value = (double)winRatio.Value * (double)occurence.Value / 100.0 * ((double)sum / (double)winSum);

                row.Cells.Add(combination);
                row.Cells.Add(winRatio);
                row.Cells.Add(occurence);
                row.Cells.Add(total);


                _dataGridView.Rows.Add(row);
            }

            _dataGridView.Sort(_dataGridView.Columns[3], ListSortDirection.Descending);
        }
    }
}
