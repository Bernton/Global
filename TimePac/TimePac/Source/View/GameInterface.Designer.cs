namespace TimePac
{
    partial class GameInterface
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
            this.components = new System.ComponentModel.Container();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._panelGame = new TimePac.GamePanel();
            this.SuspendLayout();
            // 
            // _timer
            // 
            this._timer.Interval = 1;
            this._timer.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // _panelGame
            // 
            this._panelGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._panelGame.BackColor = System.Drawing.SystemColors.ControlDark;
            this._panelGame.Location = new System.Drawing.Point(100, 0);
            this._panelGame.Name = "_panelGame";
            this._panelGame.Size = new System.Drawing.Size(600, 600);
            this._panelGame.TabIndex = 0;
            this._panelGame.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPanelGamePaint);
            // 
            // GameInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::TimePac.Properties.Resources.WallSpriteBackground;
            this.Controls.Add(this._panelGame);
            this.Name = "GameInterface";
            this.Size = new System.Drawing.Size(800, 600);
            this.Resize += new System.EventHandler(this.OnGameInterfaceResize);
            this.ResumeLayout(false);

        }

        #endregion

        private GamePanel _panelGame;
        private System.Windows.Forms.Timer _timer;
    }
}
