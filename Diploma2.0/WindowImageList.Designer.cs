namespace Diploma2._0
{
    partial class WindowImageList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowImageList));
            this._leftArrowPanel = new System.Windows.Forms.Panel();
            this._leftArrow = new System.Windows.Forms.PictureBox();
            this._rightArrowPanel = new System.Windows.Forms.Panel();
            this._rightArrow = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._leftArrowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._leftArrow)).BeginInit();
            this._rightArrowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rightArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // _leftArrowPanel
            // 
            this._leftArrowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._leftArrowPanel.Controls.Add(this._leftArrow);
            this._leftArrowPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftArrowPanel.Location = new System.Drawing.Point(2, 0);
            this._leftArrowPanel.Name = "_leftArrowPanel";
            this._leftArrowPanel.Size = new System.Drawing.Size(35, 1022);
            this._leftArrowPanel.TabIndex = 2;
            // 
            // _leftArrow
            // 
            this._leftArrow.BackgroundImage = global::Diploma2._0.Properties.Resources.Arrowhead_Left_01_128;
            this._leftArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._leftArrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this._leftArrow.Location = new System.Drawing.Point(-5, 271);
            this._leftArrow.Name = "_leftArrow";
            this._leftArrow.Size = new System.Drawing.Size(40, 40);
            this._leftArrow.TabIndex = 5;
            this._leftArrow.TabStop = false;
            this._leftArrow.Click += new System.EventHandler(this._leftArrow_Click);
            // 
            // _rightArrowPanel
            // 
            this._rightArrowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._rightArrowPanel.Controls.Add(this._rightArrow);
            this._rightArrowPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightArrowPanel.Location = new System.Drawing.Point(1243, 0);
            this._rightArrowPanel.Name = "_rightArrowPanel";
            this._rightArrowPanel.Size = new System.Drawing.Size(35, 1022);
            this._rightArrowPanel.TabIndex = 3;
            // 
            // _rightArrow
            // 
            this._rightArrow.BackgroundImage = global::Diploma2._0.Properties.Resources.Arrowhead_Right_01_128;
            this._rightArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._rightArrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rightArrow.Location = new System.Drawing.Point(0, 271);
            this._rightArrow.Name = "_rightArrow";
            this._rightArrow.Size = new System.Drawing.Size(40, 40);
            this._rightArrow.TabIndex = 4;
            this._rightArrow.TabStop = false;
            this._rightArrow.Click += new System.EventHandler(this._rightArrow_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(2, 1022);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1276, 2);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 1024);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1278, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 1024);
            this.panel4.TabIndex = 6;
            // 
            // WindowImageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this._rightArrowPanel);
            this.Controls.Add(this._leftArrowPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "WindowImageList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Галерея";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.WindowImageList_Resize);
            this._leftArrowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._leftArrow)).EndInit();
            this._rightArrowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._rightArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel _leftArrowPanel;
        private System.Windows.Forms.Panel _rightArrowPanel;
        private System.Windows.Forms.PictureBox _rightArrow;
        private System.Windows.Forms.PictureBox _leftArrow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}