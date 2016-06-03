namespace TimePac
{
    partial class AboutInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonBack = new System.Windows.Forms.Button();
            this._labelUsername = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonBack
            // 
            this._buttonBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonBack.Location = new System.Drawing.Point(275, 482);
            this._buttonBack.Name = "_buttonBack";
            this._buttonBack.Size = new System.Drawing.Size(250, 64);
            this._buttonBack.TabIndex = 4;
            this._buttonBack.Text = "Back";
            this._buttonBack.UseVisualStyleBackColor = true;
            this._buttonBack.Click += new System.EventHandler(this.OnButtonBack_Click);
            // 
            // _labelUsername
            // 
            this._labelUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelUsername.Location = new System.Drawing.Point(0, 0);
            this._labelUsername.Name = "_labelUsername";
            this._labelUsername.Size = new System.Drawing.Size(800, 151);
            this._labelUsername.TabIndex = 5;
            this._labelUsername.Text = "About TimePac";
            this._labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._labelUsername);
            this.Controls.Add(this._buttonBack);
            this.Name = "AboutInterface";
            this.Size = new System.Drawing.Size(800, 600);
            this.Resize += new System.EventHandler(this.AboutInterfaceResize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button _buttonBack;
        private System.Windows.Forms.Label _labelUsername;
    }
}
