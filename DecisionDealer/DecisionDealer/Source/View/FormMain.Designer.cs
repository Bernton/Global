namespace DecisionDealer.View
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this._buttonFold = new System.Windows.Forms.Button();
            this._buttonCall = new System.Windows.Forms.Button();
            this._labelShowCash = new System.Windows.Forms.Label();
            this._labelCash = new System.Windows.Forms.Label();
            this._panelLayout = new DecisionDealer.View.BufferedPanel();
            this._panelCanvas = new DecisionDealer.View.BufferedPanel();
            this._buttonNext = new System.Windows.Forms.Button();
            this._panelLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonFold
            // 
            this._buttonFold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonFold.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonFold.Location = new System.Drawing.Point(12, 625);
            this._buttonFold.Name = "_buttonFold";
            this._buttonFold.Size = new System.Drawing.Size(150, 44);
            this._buttonFold.TabIndex = 3;
            this._buttonFold.Text = "Fold";
            this._buttonFold.UseVisualStyleBackColor = true;
            this._buttonFold.Click += new System.EventHandler(this.OnButtonFoldClick);
            // 
            // _buttonCall
            // 
            this._buttonCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonCall.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonCall.Location = new System.Drawing.Point(12, 575);
            this._buttonCall.Name = "_buttonCall";
            this._buttonCall.Size = new System.Drawing.Size(150, 44);
            this._buttonCall.TabIndex = 2;
            this._buttonCall.Text = "Call";
            this._buttonCall.UseVisualStyleBackColor = true;
            this._buttonCall.Click += new System.EventHandler(this.OnButtonCallClick);
            // 
            // _labelShowCash
            // 
            this._labelShowCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelShowCash.AutoSize = true;
            this._labelShowCash.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelShowCash.Location = new System.Drawing.Point(188, 598);
            this._labelShowCash.Name = "_labelShowCash";
            this._labelShowCash.Size = new System.Drawing.Size(52, 21);
            this._labelShowCash.TabIndex = 4;
            this._labelShowCash.Text = "Cash:";
            // 
            // _labelCash
            // 
            this._labelCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelCash.AutoSize = true;
            this._labelCash.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCash.Location = new System.Drawing.Point(240, 598);
            this._labelCash.Name = "_labelCash";
            this._labelCash.Size = new System.Drawing.Size(42, 21);
            this._labelCash.TabIndex = 5;
            this._labelCash.Text = "30 $";
            // 
            // _panelLayout
            // 
            this._panelLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelLayout.BackColor = System.Drawing.SystemColors.Control;
            this._panelLayout.Controls.Add(this._panelCanvas);
            this._panelLayout.Location = new System.Drawing.Point(12, 12);
            this._panelLayout.Name = "_panelLayout";
            this._panelLayout.Size = new System.Drawing.Size(1022, 552);
            this._panelLayout.TabIndex = 1;
            this._panelLayout.SizeChanged += new System.EventHandler(this.OnPanelLayoutSizeChanged);
            // 
            // _panelCanvas
            // 
            this._panelCanvas.BackColor = System.Drawing.Color.White;
            this._panelCanvas.BackgroundImage = global::DecisionDealer.Properties.Resources.Table;
            this._panelCanvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._panelCanvas.Location = new System.Drawing.Point(0, 0);
            this._panelCanvas.Name = "_panelCanvas";
            this._panelCanvas.Size = new System.Drawing.Size(1022, 552);
            this._panelCanvas.TabIndex = 0;
            this._panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPanelCanvasPaint);
            // 
            // _buttonNext
            // 
            this._buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonNext.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonNext.Location = new System.Drawing.Point(884, 575);
            this._buttonNext.Name = "_buttonNext";
            this._buttonNext.Size = new System.Drawing.Size(150, 44);
            this._buttonNext.TabIndex = 6;
            this._buttonNext.Text = "Next";
            this._buttonNext.UseVisualStyleBackColor = true;
            this._buttonNext.Click += new System.EventHandler(this.OnButtonNextClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 681);
            this.Controls.Add(this._buttonNext);
            this.Controls.Add(this._labelCash);
            this.Controls.Add(this._labelShowCash);
            this.Controls.Add(this._buttonFold);
            this.Controls.Add(this._buttonCall);
            this.Controls.Add(this._panelLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 420);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DecisionDealer Version X";
            this._panelLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BufferedPanel _panelCanvas;
        private BufferedPanel _panelLayout;
        private System.Windows.Forms.Button _buttonFold;
        private System.Windows.Forms.Button _buttonCall;
        private System.Windows.Forms.Label _labelShowCash;
        private System.Windows.Forms.Label _labelCash;
        private System.Windows.Forms.Button _buttonNext;

    }
}

