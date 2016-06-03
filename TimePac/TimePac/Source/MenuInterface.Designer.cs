namespace TimePac
{
    partial class MenuInterface
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
            this._buttonStart = new System.Windows.Forms.Button();
            this._labelUsername = new System.Windows.Forms.Label();
            this._buttonHighScores = new System.Windows.Forms.Button();
            this._buttonAbout = new System.Windows.Forms.Button();
            this._buttonBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _buttonStart
            // 
            this._buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonStart.Location = new System.Drawing.Point(275, 182);
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Size = new System.Drawing.Size(250, 70);
            this._buttonStart.TabIndex = 0;
            this._buttonStart.Text = "Start";
            this._buttonStart.UseVisualStyleBackColor = true;
            this._buttonStart.Click += new System.EventHandler(this.OnButtonStartClick);
            // 
            // _labelUsername
            // 
            this._labelUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelUsername.Location = new System.Drawing.Point(0, 0);
            this._labelUsername.Name = "_labelUsername";
            this._labelUsername.Size = new System.Drawing.Size(800, 151);
            this._labelUsername.TabIndex = 3;
            this._labelUsername.Text = "Hello there!";
            this._labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _buttonHighScores
            // 
            this._buttonHighScores.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonHighScores.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonHighScores.Location = new System.Drawing.Point(275, 282);
            this._buttonHighScores.Name = "_buttonHighScores";
            this._buttonHighScores.Size = new System.Drawing.Size(250, 64);
            this._buttonHighScores.TabIndex = 1;
            this._buttonHighScores.Text = "Highscores";
            this._buttonHighScores.UseVisualStyleBackColor = true;
            this._buttonHighScores.Click += new System.EventHandler(this.OnButtonHighScoresClick);
            // 
            // _buttonAbout
            // 
            this._buttonAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonAbout.Location = new System.Drawing.Point(275, 382);
            this._buttonAbout.Name = "_buttonAbout";
            this._buttonAbout.Size = new System.Drawing.Size(250, 64);
            this._buttonAbout.TabIndex = 2;
            this._buttonAbout.Text = "About";
            this._buttonAbout.UseVisualStyleBackColor = true;
            this._buttonAbout.Click += new System.EventHandler(this.OnButtonAboutClick);
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
            this._buttonBack.Click += new System.EventHandler(this.OnButtonBackClick);
            // 
            // MenuInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._buttonBack);
            this.Controls.Add(this._buttonAbout);
            this.Controls.Add(this._buttonHighScores);
            this.Controls.Add(this._labelUsername);
            this.Controls.Add(this._buttonStart);
            this.Name = "MenuInterface";
            this.Size = new System.Drawing.Size(800, 600);
            this.Resize += new System.EventHandler(this.MenuInterfaceResize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonStart;
        private System.Windows.Forms.Label _labelUsername;
        private System.Windows.Forms.Button _buttonHighScores;
        private System.Windows.Forms.Button _buttonAbout;
        private System.Windows.Forms.Button _buttonBack;
    }
}
