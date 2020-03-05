namespace Diploma2._0
{
    partial class GoToWondows
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._controlPanel = new System.Windows.Forms.Panel();
            this._back = new System.Windows.Forms.PictureBox();
            this._menuFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._сhangeUser = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this._settingPICTUREBOX = new System.Windows.Forms.PictureBox();
            this._opacityTRACKBAR = new System.Windows.Forms.TrackBar();
            this._opacityLABEL = new System.Windows.Forms.Label();
            this._time = new System.Windows.Forms.Label();
            this._timeLeft = new System.Windows.Forms.Label();
            this.unfoldingPICTUREBOX = new System.Windows.Forms.PictureBox();
            this._menuPANEL = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CheckTheTimeOfTheUserTIMER = new System.Windows.Forms.Timer(this.components);
            this._closeMenuTIMER = new System.Windows.Forms.Timer(this.components);
            this._returnInMenuTOOLTIP = new System.Windows.Forms.ToolTip(this.components);
            this._createScreenShot = new System.Windows.Forms.Timer(this.components);
            this._checkHostFile = new System.Windows.Forms.Timer(this.components);
            this._checkBlockPrograms = new System.Windows.Forms.Timer(this.components);
            this._controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._back)).BeginInit();
            this._menuFLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._сhangeUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingPICTUREBOX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._opacityTRACKBAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unfoldingPICTUREBOX)).BeginInit();
            this._menuPANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 2);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 185);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(416, 2);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 183);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(414, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 183);
            this.panel4.TabIndex = 3;
            // 
            // _controlPanel
            // 
            this._controlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this._controlPanel.Controls.Add(this._back);
            this._controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._controlPanel.Location = new System.Drawing.Point(2, 2);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Size = new System.Drawing.Size(412, 25);
            this._controlPanel.TabIndex = 4;
            this._controlPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this._controlPanel_MouseDown);
            this._controlPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this._controlPanel_MouseMove);
            // 
            // _back
            // 
            this._back.BackColor = System.Drawing.Color.Transparent;
            this._back.BackgroundImage = global::Diploma2._0.Properties.Resources.Left_Arrow_128;
            this._back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._back.Cursor = System.Windows.Forms.Cursors.Hand;
            this._back.Location = new System.Drawing.Point(15, -2);
            this._back.Name = "_back";
            this._back.Size = new System.Drawing.Size(30, 30);
            this._back.TabIndex = 14;
            this._back.TabStop = false;
            this._returnInMenuTOOLTIP.SetToolTip(this._back, "Вернуться в меню");
            this._back.Click += new System.EventHandler(this._back_Click);
            // 
            // _menuFLP
            // 
            this._menuFLP.Controls.Add(this._сhangeUser);
            this._menuFLP.Controls.Add(this.pictureBox2);
            this._menuFLP.Controls.Add(this._settingPICTUREBOX);
            this._menuFLP.Controls.Add(this._opacityTRACKBAR);
            this._menuFLP.Controls.Add(this._opacityLABEL);
            this._menuFLP.Location = new System.Drawing.Point(51, 0);
            this._menuFLP.Name = "_menuFLP";
            this._menuFLP.Size = new System.Drawing.Size(272, 36);
            this._menuFLP.TabIndex = 5;
            // 
            // _сhangeUser
            // 
            this._сhangeUser.BackgroundImage = global::Diploma2._0.Properties.Resources.changeUser;
            this._сhangeUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._сhangeUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this._сhangeUser.Location = new System.Drawing.Point(15, 8);
            this._сhangeUser.Margin = new System.Windows.Forms.Padding(15, 8, 0, 0);
            this._сhangeUser.Name = "_сhangeUser";
            this._сhangeUser.Size = new System.Drawing.Size(20, 20);
            this._сhangeUser.TabIndex = 2;
            this._сhangeUser.TabStop = false;
            this._сhangeUser.Click += new System.EventHandler(this._сhangeUser_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Diploma2._0.Properties.Resources.help;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Location = new System.Drawing.Point(50, 8);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(15, 8, 0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // _settingPICTUREBOX
            // 
            this._settingPICTUREBOX.BackgroundImage = global::Diploma2._0.Properties.Resources.setting;
            this._settingPICTUREBOX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._settingPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._settingPICTUREBOX.Location = new System.Drawing.Point(85, 8);
            this._settingPICTUREBOX.Margin = new System.Windows.Forms.Padding(15, 8, 0, 0);
            this._settingPICTUREBOX.Name = "_settingPICTUREBOX";
            this._settingPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._settingPICTUREBOX.TabIndex = 4;
            this._settingPICTUREBOX.TabStop = false;
            this._settingPICTUREBOX.Click += new System.EventHandler(this._settingPICTUREBOX_Click);
            // 
            // _opacityTRACKBAR
            // 
            this._opacityTRACKBAR.Location = new System.Drawing.Point(108, 3);
            this._opacityTRACKBAR.Maximum = 100;
            this._opacityTRACKBAR.Minimum = 20;
            this._opacityTRACKBAR.Name = "_opacityTRACKBAR";
            this._opacityTRACKBAR.Size = new System.Drawing.Size(109, 45);
            this._opacityTRACKBAR.TabIndex = 5;
            this._opacityTRACKBAR.Value = 100;
            this._opacityTRACKBAR.Scroll += new System.EventHandler(this._opacityTRACKBAR_Scroll);
            // 
            // _opacityLABEL
            // 
            this._opacityLABEL.AutoSize = true;
            this._opacityLABEL.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._opacityLABEL.ForeColor = System.Drawing.Color.White;
            this._opacityLABEL.Location = new System.Drawing.Point(223, 8);
            this._opacityLABEL.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this._opacityLABEL.Name = "_opacityLABEL";
            this._opacityLABEL.Size = new System.Drawing.Size(44, 19);
            this._opacityLABEL.TabIndex = 6;
            this._opacityLABEL.Text = "100%";
            // 
            // _time
            // 
            this._time.AutoSize = true;
            this._time.BackColor = System.Drawing.Color.Transparent;
            this._time.Font = new System.Drawing.Font("Calibri", 27F);
            this._time.ForeColor = System.Drawing.Color.White;
            this._time.Location = new System.Drawing.Point(76, 126);
            this._time.Name = "_time";
            this._time.Size = new System.Drawing.Size(176, 44);
            this._time.TabIndex = 11;
            this._time.Text = "Загрузка...";
            // 
            // _timeLeft
            // 
            this._timeLeft.AutoSize = true;
            this._timeLeft.BackColor = System.Drawing.Color.Transparent;
            this._timeLeft.Font = new System.Drawing.Font("Calibri", 21F);
            this._timeLeft.ForeColor = System.Drawing.Color.White;
            this._timeLeft.Location = new System.Drawing.Point(90, 85);
            this._timeLeft.Name = "_timeLeft";
            this._timeLeft.Size = new System.Drawing.Size(235, 35);
            this._timeLeft.TabIndex = 10;
            this._timeLeft.Text = "Осталось времени";
            // 
            // unfoldingPICTUREBOX
            // 
            this.unfoldingPICTUREBOX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.unfoldingPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this.unfoldingPICTUREBOX.BackgroundImage = global::Diploma2._0.Properties.Resources.Arrowhead_Down_128;
            this.unfoldingPICTUREBOX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.unfoldingPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unfoldingPICTUREBOX.Location = new System.Drawing.Point(182, 64);
            this.unfoldingPICTUREBOX.Name = "unfoldingPICTUREBOX";
            this.unfoldingPICTUREBOX.Size = new System.Drawing.Size(25, 18);
            this.unfoldingPICTUREBOX.TabIndex = 12;
            this.unfoldingPICTUREBOX.TabStop = false;
            this.unfoldingPICTUREBOX.Click += new System.EventHandler(this.unfoldingPICTUREBOX_Click);
            // 
            // _menuPANEL
            // 
            this._menuPANEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this._menuPANEL.Controls.Add(this.panel6);
            this._menuPANEL.Controls.Add(this._menuFLP);
            this._menuPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._menuPANEL.Location = new System.Drawing.Point(2, 27);
            this._menuPANEL.Name = "_menuPANEL";
            this._menuPANEL.Size = new System.Drawing.Size(412, 36);
            this._menuPANEL.TabIndex = 13;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 35);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(412, 1);
            this.panel6.TabIndex = 6;
            // 
            // CheckTheTimeOfTheUserTIMER
            // 
            this.CheckTheTimeOfTheUserTIMER.Interval = 60000;
            this.CheckTheTimeOfTheUserTIMER.Tick += new System.EventHandler(this.CheckTheTimeOfTheUserTIMER_Tick);
            // 
            // _closeMenuTIMER
            // 
            this._closeMenuTIMER.Interval = 1;
            this._closeMenuTIMER.Tick += new System.EventHandler(this._closeMenuTIMER_Tick);
            // 
            // _returnInMenuTOOLTIP
            // 
            this._returnInMenuTOOLTIP.ToolTipTitle = "Вернуться в меню";
            // 
            // _createScreenShot
            // 
            this._createScreenShot.Interval = 1;
            this._createScreenShot.Tick += new System.EventHandler(this._createScreenShot_Tick);
            // 
            // _checkHostFile
            // 
            this._checkHostFile.Interval = 1000;
            this._checkHostFile.Tick += new System.EventHandler(this._checkHostFile_Tick);
            // 
            // _checkBlockPrograms
            // 
            this._checkBlockPrograms.Interval = 1000;
            this._checkBlockPrograms.Tick += new System.EventHandler(this._checkBlockPrograms_Tick);
            // 
            // GoToWondows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(416, 187);
            this.Controls.Add(this._menuPANEL);
            this.Controls.Add(this.unfoldingPICTUREBOX);
            this.Controls.Add(this._time);
            this.Controls.Add(this._timeLeft);
            this.Controls.Add(this._controlPanel);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GoToWondows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GoToWondows";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoToWondows_FormClosed);
            this.Load += new System.EventHandler(this.GoToWondows_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoToWondows_KeyDown);
            this._controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._back)).EndInit();
            this._menuFLP.ResumeLayout(false);
            this._menuFLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._сhangeUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingPICTUREBOX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._opacityTRACKBAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unfoldingPICTUREBOX)).EndInit();
            this._menuPANEL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel _controlPanel;
        private System.Windows.Forms.PictureBox _back;
        private System.Windows.Forms.FlowLayoutPanel _menuFLP;
        private System.Windows.Forms.PictureBox _сhangeUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox _settingPICTUREBOX;
        private System.Windows.Forms.Label _time;
        private System.Windows.Forms.Label _timeLeft;
        private System.Windows.Forms.PictureBox unfoldingPICTUREBOX;
        private System.Windows.Forms.Panel _menuPANEL;
        private System.Windows.Forms.Timer CheckTheTimeOfTheUserTIMER;
        private System.Windows.Forms.Timer _closeMenuTIMER;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolTip _returnInMenuTOOLTIP;
        private System.Windows.Forms.TrackBar _opacityTRACKBAR;
        private System.Windows.Forms.Label _opacityLABEL;
        private System.Windows.Forms.Timer _createScreenShot;
        private System.Windows.Forms.Timer _checkHostFile;
        private System.Windows.Forms.Timer _checkBlockPrograms;
    }
}