﻿namespace TimePac
{
    partial class UsernameInterface
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
            this._labelUsername = new System.Windows.Forms.Label();
            this._textBoxUsername = new System.Windows.Forms.TextBox();
            this._buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _labelUsername
            // 
            this._labelUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._labelUsername.BackColor = System.Drawing.Color.Transparent;
            this._labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelUsername.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._labelUsername.Location = new System.Drawing.Point(0, 0);
            this._labelUsername.Name = "_labelUsername";
            this._labelUsername.Size = new System.Drawing.Size(800, 200);
            this._labelUsername.TabIndex = 0;
            this._labelUsername.Text = "Enter Username";
            this._labelUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _textBoxUsername
            // 
            this._textBoxUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._textBoxUsername.BackColor = System.Drawing.Color.WhiteSmoke;
            this._textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._textBoxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxUsername.Location = new System.Drawing.Point(250, 295);
            this._textBoxUsername.MaxLength = 8;
            this._textBoxUsername.Name = "_textBoxUsername";
            this._textBoxUsername.Size = new System.Drawing.Size(300, 49);
            this._textBoxUsername.TabIndex = 0;
            this._textBoxUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._textBoxUsername.TextChanged += new System.EventHandler(this.OnTextBoxUsernameTextChanged);
            // 
            // _buttonStart
            // 
            this._buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._buttonStart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._buttonStart.Location = new System.Drawing.Point(340, 359);
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Size = new System.Drawing.Size(120, 35);
            this._buttonStart.TabIndex = 1;
            this._buttonStart.Text = "Start";
            this._buttonStart.UseVisualStyleBackColor = false;
            this._buttonStart.Click += new System.EventHandler(this.OnButtonStartClick);
            // 
            // UsernameInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::TimePac.Properties.Resources.WallSpriteBackground;
            this.Controls.Add(this._labelUsername);
            this.Controls.Add(this._buttonStart);
            this.Controls.Add(this._textBoxUsername);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "UsernameInterface";
            this.Size = new System.Drawing.Size(800, 600);
            this.Resize += new System.EventHandler(this.OnUsernameInterfaceResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelUsername;
        private System.Windows.Forms.TextBox _textBoxUsername;
        private System.Windows.Forms.Button _buttonStart;
    }
}
