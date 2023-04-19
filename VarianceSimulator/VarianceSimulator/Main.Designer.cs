namespace VarianceSimulator
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonRoll = new System.Windows.Forms.Button();
            this._textBoxRollAmount = new System.Windows.Forms.TextBox();
            this._labelPossibilies = new System.Windows.Forms.Label();
            this._textBoxPossibilies = new System.Windows.Forms.TextBox();
            this._labelEdge = new System.Windows.Forms.Label();
            this._textBoxEdge = new System.Windows.Forms.TextBox();
            this._labelDollarPerRoll = new System.Windows.Forms.Label();
            this._chart = new VarianceSimulator.BufferedPanel();
            this.SuspendLayout();
            // 
            // _buttonRoll
            // 
            this._buttonRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRoll.Location = new System.Drawing.Point(742, 12);
            this._buttonRoll.Name = "_buttonRoll";
            this._buttonRoll.Size = new System.Drawing.Size(103, 23);
            this._buttonRoll.TabIndex = 1;
            this._buttonRoll.Text = "Roll X times for 1$";
            this._buttonRoll.UseVisualStyleBackColor = true;
            this._buttonRoll.Click += new System.EventHandler(this.OnButtonRollClick);
            // 
            // _textBoxRollAmount
            // 
            this._textBoxRollAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxRollAmount.Location = new System.Drawing.Point(742, 41);
            this._textBoxRollAmount.Name = "_textBoxRollAmount";
            this._textBoxRollAmount.Size = new System.Drawing.Size(103, 20);
            this._textBoxRollAmount.TabIndex = 4;
            this._textBoxRollAmount.Text = "1";
            // 
            // _labelPossibilies
            // 
            this._labelPossibilies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._labelPossibilies.AutoSize = true;
            this._labelPossibilies.Location = new System.Drawing.Point(739, 76);
            this._labelPossibilies.Name = "_labelPossibilies";
            this._labelPossibilies.Size = new System.Drawing.Size(94, 13);
            this._labelPossibilies.TabIndex = 5;
            this._labelPossibilies.Text = "Possibilies ( 1 / X )";
            // 
            // _textBoxPossibilies
            // 
            this._textBoxPossibilies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxPossibilies.Location = new System.Drawing.Point(742, 92);
            this._textBoxPossibilies.Name = "_textBoxPossibilies";
            this._textBoxPossibilies.Size = new System.Drawing.Size(103, 20);
            this._textBoxPossibilies.TabIndex = 6;
            this._textBoxPossibilies.Text = "2";
            // 
            // _labelEdge
            // 
            this._labelEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._labelEdge.AutoSize = true;
            this._labelEdge.Location = new System.Drawing.Point(742, 124);
            this._labelEdge.Name = "_labelEdge";
            this._labelEdge.Size = new System.Drawing.Size(49, 13);
            this._labelEdge.TabIndex = 7;
            this._labelEdge.Text = "Edge (%)";
            // 
            // _textBoxEdge
            // 
            this._textBoxEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxEdge.Location = new System.Drawing.Point(742, 140);
            this._textBoxEdge.Name = "_textBoxEdge";
            this._textBoxEdge.Size = new System.Drawing.Size(103, 20);
            this._textBoxEdge.TabIndex = 8;
            this._textBoxEdge.Text = "0";
            // 
            // _labelDollarPerRoll
            // 
            this._labelDollarPerRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._labelDollarPerRoll.AutoSize = true;
            this._labelDollarPerRoll.Location = new System.Drawing.Point(751, 386);
            this._labelDollarPerRoll.Name = "_labelDollarPerRoll";
            this._labelDollarPerRoll.Size = new System.Drawing.Size(61, 26);
            this._labelDollarPerRoll.TabIndex = 9;
            this._labelDollarPerRoll.Text = "0 $ per roll\r\non average";
            // 
            // _chart
            // 
            this._chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._chart.BackColor = System.Drawing.Color.White;
            this._chart.Location = new System.Drawing.Point(12, 12);
            this._chart.Name = "_chart";
            this._chart.Size = new System.Drawing.Size(724, 400);
            this._chart.TabIndex = 0;
            this._chart.Paint += new System.Windows.Forms.PaintEventHandler(this.OnChartPaint);
            this._chart.Resize += new System.EventHandler(this.OnChartResize);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 427);
            this.Controls.Add(this._labelDollarPerRoll);
            this.Controls.Add(this._textBoxEdge);
            this.Controls.Add(this._labelEdge);
            this.Controls.Add(this._textBoxPossibilies);
            this.Controls.Add(this._labelPossibilies);
            this.Controls.Add(this._textBoxRollAmount);
            this.Controls.Add(this._buttonRoll);
            this.Controls.Add(this._chart);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "Main";
            this.Text = "Variance Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BufferedPanel _chart;
        private System.Windows.Forms.Button _buttonRoll;
        private System.Windows.Forms.TextBox _textBoxRollAmount;
        private System.Windows.Forms.Label _labelPossibilies;
        private System.Windows.Forms.TextBox _textBoxPossibilies;
        private System.Windows.Forms.Label _labelEdge;
        private System.Windows.Forms.TextBox _textBoxEdge;
        private System.Windows.Forms.Label _labelDollarPerRoll;

    }
}

