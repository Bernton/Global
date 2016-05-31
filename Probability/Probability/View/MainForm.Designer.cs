namespace Probability.View
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this._labelShowN = new System.Windows.Forms.Label();
            this._labelShowR = new System.Windows.Forms.Label();
            this._textBoxN = new System.Windows.Forms.TextBox();
            this._textBoxR = new System.Windows.Forms.TextBox();
            this._labelShowResult = new System.Windows.Forms.Label();
            this._buttonCalculate = new System.Windows.Forms.Button();
            this._checkBoxOrder = new System.Windows.Forms.CheckBox();
            this._checkBoxRepitition = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this._richTextBoxResultSci = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _labelShowN
            // 
            this._labelShowN.AutoSize = true;
            this._labelShowN.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelShowN.Location = new System.Drawing.Point(29, 26);
            this._labelShowN.Name = "_labelShowN";
            this._labelShowN.Size = new System.Drawing.Size(28, 31);
            this._labelShowN.TabIndex = 0;
            this._labelShowN.Text = "n";
            this.toolTip.SetToolTip(this._labelShowN, "Number of things to choose from.");
            // 
            // _labelShowR
            // 
            this._labelShowR.AutoSize = true;
            this._labelShowR.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelShowR.Location = new System.Drawing.Point(29, 93);
            this._labelShowR.Name = "_labelShowR";
            this._labelShowR.Size = new System.Drawing.Size(25, 31);
            this._labelShowR.TabIndex = 1;
            this._labelShowR.Text = "r";
            this.toolTip.SetToolTip(this._labelShowR, "Amount to be chosen.");
            // 
            // _textBoxN
            // 
            this._textBoxN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxN.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxN.Location = new System.Drawing.Point(143, 23);
            this._textBoxN.Name = "_textBoxN";
            this._textBoxN.Size = new System.Drawing.Size(308, 39);
            this._textBoxN.TabIndex = 0;
            this._textBoxN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
            // 
            // _textBoxR
            // 
            this._textBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxR.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxR.Location = new System.Drawing.Point(143, 90);
            this._textBoxR.Name = "_textBoxR";
            this._textBoxR.Size = new System.Drawing.Size(308, 39);
            this._textBoxR.TabIndex = 1;
            this._textBoxR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
            // 
            // _labelShowResult
            // 
            this._labelShowResult.AutoSize = true;
            this._labelShowResult.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelShowResult.Location = new System.Drawing.Point(29, 210);
            this._labelShowResult.Name = "_labelShowResult";
            this._labelShowResult.Size = new System.Drawing.Size(84, 31);
            this._labelShowResult.TabIndex = 5;
            this._labelShowResult.Text = "Result";
            // 
            // _buttonCalculate
            // 
            this._buttonCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonCalculate.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonCalculate.Location = new System.Drawing.Point(35, 343);
            this._buttonCalculate.Name = "_buttonCalculate";
            this._buttonCalculate.Size = new System.Drawing.Size(416, 39);
            this._buttonCalculate.TabIndex = 6;
            this._buttonCalculate.Text = "Calculate";
            this._buttonCalculate.UseVisualStyleBackColor = true;
            this._buttonCalculate.Click += new System.EventHandler(this.OnButtonCalculateClick);
            // 
            // _checkBoxOrder
            // 
            this._checkBoxOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkBoxOrder.AutoSize = true;
            this._checkBoxOrder.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._checkBoxOrder.Location = new System.Drawing.Point(319, 153);
            this._checkBoxOrder.Name = "_checkBoxOrder";
            this._checkBoxOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._checkBoxOrder.Size = new System.Drawing.Size(132, 25);
            this._checkBoxOrder.TabIndex = 2;
            this._checkBoxOrder.Text = "Order matters";
            this._checkBoxOrder.UseVisualStyleBackColor = true;
            // 
            // _checkBoxRepitition
            // 
            this._checkBoxRepitition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkBoxRepitition.AutoSize = true;
            this._checkBoxRepitition.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._checkBoxRepitition.Location = new System.Drawing.Point(350, 184);
            this._checkBoxRepitition.Name = "_checkBoxRepitition";
            this._checkBoxRepitition.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._checkBoxRepitition.Size = new System.Drawing.Size(101, 25);
            this._checkBoxRepitition.TabIndex = 3;
            this._checkBoxRepitition.Text = "Repitition";
            this._checkBoxRepitition.UseVisualStyleBackColor = true;
            // 
            // _richTextBoxResult
            // 
            this._richTextBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._richTextBoxResult.BackColor = System.Drawing.SystemColors.Window;
            this._richTextBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._richTextBoxResult.Font = new System.Drawing.Font("Arial", 20.25F);
            this._richTextBoxResult.Location = new System.Drawing.Point(35, 244);
            this._richTextBoxResult.Name = "_richTextBoxResult";
            this._richTextBoxResult.ReadOnly = true;
            this._richTextBoxResult.Size = new System.Drawing.Size(416, 39);
            this._richTextBoxResult.TabIndex = 4;
            this._richTextBoxResult.Text = "";
            // 
            // _richTextBoxResultSci
            // 
            this._richTextBoxResultSci.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._richTextBoxResultSci.BackColor = System.Drawing.SystemColors.Window;
            this._richTextBoxResultSci.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._richTextBoxResultSci.Font = new System.Drawing.Font("Arial", 20.25F);
            this._richTextBoxResultSci.Location = new System.Drawing.Point(35, 289);
            this._richTextBoxResultSci.Multiline = false;
            this._richTextBoxResultSci.Name = "_richTextBoxResultSci";
            this._richTextBoxResultSci.ReadOnly = true;
            this._richTextBoxResultSci.Size = new System.Drawing.Size(416, 39);
            this._richTextBoxResultSci.TabIndex = 5;
            this._richTextBoxResultSci.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 401);
            this.Controls.Add(this._richTextBoxResultSci);
            this.Controls.Add(this._richTextBoxResult);
            this.Controls.Add(this._checkBoxRepitition);
            this.Controls.Add(this._checkBoxOrder);
            this.Controls.Add(this._buttonCalculate);
            this.Controls.Add(this._labelShowResult);
            this.Controls.Add(this._textBoxR);
            this.Controls.Add(this._textBoxN);
            this.Controls.Add(this._labelShowR);
            this.Controls.Add(this._labelShowN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Combination & Permutation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelShowN;
        private System.Windows.Forms.Label _labelShowR;
        private System.Windows.Forms.TextBox _textBoxN;
        private System.Windows.Forms.TextBox _textBoxR;
        private System.Windows.Forms.Label _labelShowResult;
        private System.Windows.Forms.Button _buttonCalculate;
        private System.Windows.Forms.CheckBox _checkBoxOrder;
        private System.Windows.Forms.CheckBox _checkBoxRepitition;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RichTextBox _richTextBoxResult;
        private System.Windows.Forms.RichTextBox _richTextBoxResultSci;
    }
}

