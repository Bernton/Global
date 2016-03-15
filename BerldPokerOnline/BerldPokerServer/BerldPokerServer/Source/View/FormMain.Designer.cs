namespace BerldPokerServer.View
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
            this._listBoxConnectedClients = new System.Windows.Forms.ListBox();
            this._listBoxTables = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listBoxConnectedClients
            // 
            this._listBoxConnectedClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listBoxConnectedClients.Enabled = false;
            this._listBoxConnectedClients.FormattingEnabled = true;
            this._listBoxConnectedClients.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._listBoxConnectedClients.Location = new System.Drawing.Point(12, 12);
            this._listBoxConnectedClients.Name = "_listBoxConnectedClients";
            this._listBoxConnectedClients.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this._listBoxConnectedClients.Size = new System.Drawing.Size(573, 160);
            this._listBoxConnectedClients.TabIndex = 0;
            this._listBoxConnectedClients.UseTabStops = false;
            // 
            // _listBoxTables
            // 
            this._listBoxTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listBoxTables.Enabled = false;
            this._listBoxTables.FormattingEnabled = true;
            this._listBoxTables.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._listBoxTables.Location = new System.Drawing.Point(12, 187);
            this._listBoxTables.Name = "_listBoxTables";
            this._listBoxTables.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this._listBoxTables.Size = new System.Drawing.Size(573, 251);
            this._listBoxTables.TabIndex = 1;
            this._listBoxTables.UseTabStops = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 449);
            this.Controls.Add(this._listBoxTables);
            this.Controls.Add(this._listBoxConnectedClients);
            this.Name = "FormMain";
            this.Text = "BerldPoker Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _listBoxConnectedClients;
        private System.Windows.Forms.ListBox _listBoxTables;
    }
}

