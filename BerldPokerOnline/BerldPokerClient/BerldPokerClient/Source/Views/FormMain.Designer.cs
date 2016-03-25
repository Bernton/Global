namespace BerldPokerClient.Views
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
            this._listBoxTables = new System.Windows.Forms.ListBox();
            this._buttonCreateNew = new System.Windows.Forms.Button();
            this._buttonJoinSelected = new System.Windows.Forms.Button();
            this._buttonRefresh = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _listBoxTables
            // 
            this._listBoxTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listBoxTables.FormattingEnabled = true;
            this._listBoxTables.Location = new System.Drawing.Point(12, 12);
            this._listBoxTables.Name = "_listBoxTables";
            this._listBoxTables.Size = new System.Drawing.Size(446, 368);
            this._listBoxTables.TabIndex = 0;
            // 
            // _buttonCreateNew
            // 
            this._buttonCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCreateNew.Location = new System.Drawing.Point(477, 70);
            this._buttonCreateNew.Name = "_buttonCreateNew";
            this._buttonCreateNew.Size = new System.Drawing.Size(119, 23);
            this._buttonCreateNew.TabIndex = 1;
            this._buttonCreateNew.Text = "Create new table";
            this._buttonCreateNew.UseVisualStyleBackColor = true;
            this._buttonCreateNew.Click += new System.EventHandler(this.On_buttonCreateNew_Click);
            // 
            // _buttonJoinSelected
            // 
            this._buttonJoinSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonJoinSelected.Location = new System.Drawing.Point(477, 12);
            this._buttonJoinSelected.Name = "_buttonJoinSelected";
            this._buttonJoinSelected.Size = new System.Drawing.Size(119, 23);
            this._buttonJoinSelected.TabIndex = 2;
            this._buttonJoinSelected.Text = "Join as player";
            this._buttonJoinSelected.UseVisualStyleBackColor = true;
            this._buttonJoinSelected.Click += new System.EventHandler(this.On_buttonJoinSelected_Click);
            // 
            // _buttonRefresh
            // 
            this._buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRefresh.Location = new System.Drawing.Point(477, 357);
            this._buttonRefresh.Name = "_buttonRefresh";
            this._buttonRefresh.Size = new System.Drawing.Size(119, 23);
            this._buttonRefresh.TabIndex = 3;
            this._buttonRefresh.Text = "Refresh";
            this._buttonRefresh.UseVisualStyleBackColor = true;
            this._buttonRefresh.Click += new System.EventHandler(this.On_buttonRefresh_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(477, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Join as observer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 398);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._buttonRefresh);
            this.Controls.Add(this._buttonJoinSelected);
            this.Controls.Add(this._buttonCreateNew);
            this.Controls.Add(this._listBoxTables);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormTableDialog_FormClosing);
            this.Shown += new System.EventHandler(this.OnFormTableDialog_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _listBoxTables;
        private System.Windows.Forms.Button _buttonCreateNew;
        private System.Windows.Forms.Button _buttonJoinSelected;
        private System.Windows.Forms.Button _buttonRefresh;
        private System.Windows.Forms.Button button1;
    }
}