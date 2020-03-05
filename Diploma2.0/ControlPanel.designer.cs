namespace Diploma2._0
{
    partial class ControlPanel
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
            this._listUsersCleaLABEL = new System.Windows.Forms.Label();
            this._functLISTBOX = new System.Windows.Forms.ListBox();
            this._backGroundFuncPANEL = new System.Windows.Forms.Panel();
            this._funcNameListBoxLABEL = new System.Windows.Forms.Label();
            this._usersLISTBOX = new System.Windows.Forms.ListBox();
            this._backGroundUsersPANEL = new System.Windows.Forms.Panel();
            this._usersNameListBoxLABEL = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._informationLABEL = new System.Windows.Forms.Label();
            this._contentPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._saveData = new System.Windows.Forms.Label();
            this._screenShotIMAGELIST = new System.Windows.Forms.ImageList(this.components);
            this._screenShotTIMER = new System.Windows.Forms.Timer(this.components);
            this._recordVideoTIMER = new System.Windows.Forms.Timer(this.components);
            this._controlWIndowPANEL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).BeginInit();
            this.panel1.SuspendLayout();
            this._backGroundFuncPANEL.SuspendLayout();
            this._backGroundUsersPANEL.SuspendLayout();
            this._contentPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlWIndowPANEL
            // 
            this._controlWIndowPANEL.Size = new System.Drawing.Size(671, 25);
            // 
            // _leftBorder
            // 
            this._leftBorder.Size = new System.Drawing.Size(2, 404);
            // 
            // _rightBorder
            // 
            this._rightBorder.Location = new System.Drawing.Point(673, 2);
            this._rightBorder.Size = new System.Drawing.Size(2, 404);
            // 
            // _topBorder
            // 
            this._topBorder.Size = new System.Drawing.Size(675, 2);
            // 
            // _bottomBorder
            // 
            this._bottomBorder.Location = new System.Drawing.Point(2, 404);
            this._bottomBorder.Size = new System.Drawing.Size(671, 2);
            // 
            // _underPanelBorder
            // 
            this._underPanelBorder.Size = new System.Drawing.Size(671, 1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._listUsersCleaLABEL);
            this.panel1.Controls.Add(this._functLISTBOX);
            this.panel1.Controls.Add(this._backGroundFuncPANEL);
            this.panel1.Controls.Add(this._usersLISTBOX);
            this.panel1.Controls.Add(this._backGroundUsersPANEL);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(2, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 376);
            this.panel1.TabIndex = 6;
            // 
            // _listUsersCleaLABEL
            // 
            this._listUsersCleaLABEL.AutoSize = true;
            this._listUsersCleaLABEL.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._listUsersCleaLABEL.ForeColor = System.Drawing.Color.DimGray;
            this._listUsersCleaLABEL.Location = new System.Drawing.Point(32, 111);
            this._listUsersCleaLABEL.Name = "_listUsersCleaLABEL";
            this._listUsersCleaLABEL.Size = new System.Drawing.Size(118, 26);
            this._listUsersCleaLABEL.TabIndex = 8;
            this._listUsersCleaLABEL.Text = "Список пуст";
            this._listUsersCleaLABEL.Visible = false;
            // 
            // _functLISTBOX
            // 
            this._functLISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._functLISTBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this._functLISTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._functLISTBOX.FormattingEnabled = true;
            this._functLISTBOX.HorizontalScrollbar = true;
            this._functLISTBOX.ItemHeight = 19;
            this._functLISTBOX.Items.AddRange(new object[] {
            "Блокировка сайта",
            "Блокировка приложений",
            "Запись видео",
            "Снимки экрана"});
            this._functLISTBOX.Location = new System.Drawing.Point(0, 245);
            this._functLISTBOX.Name = "_functLISTBOX";
            this._functLISTBOX.Size = new System.Drawing.Size(180, 131);
            this._functLISTBOX.TabIndex = 3;
            this._functLISTBOX.Click += new System.EventHandler(this._functLISTBOX_Click);
            // 
            // _backGroundFuncPANEL
            // 
            this._backGroundFuncPANEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this._backGroundFuncPANEL.Controls.Add(this._funcNameListBoxLABEL);
            this._backGroundFuncPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._backGroundFuncPANEL.Location = new System.Drawing.Point(0, 189);
            this._backGroundFuncPANEL.Name = "_backGroundFuncPANEL";
            this._backGroundFuncPANEL.Size = new System.Drawing.Size(180, 56);
            this._backGroundFuncPANEL.TabIndex = 2;
            // 
            // _funcNameListBoxLABEL
            // 
            this._funcNameListBoxLABEL.AutoSize = true;
            this._funcNameListBoxLABEL.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._funcNameListBoxLABEL.ForeColor = System.Drawing.Color.White;
            this._funcNameListBoxLABEL.Location = new System.Drawing.Point(33, 16);
            this._funcNameListBoxLABEL.Name = "_funcNameListBoxLABEL";
            this._funcNameListBoxLABEL.Size = new System.Drawing.Size(81, 23);
            this._funcNameListBoxLABEL.TabIndex = 0;
            this._funcNameListBoxLABEL.Text = "Функции";
            // 
            // _usersLISTBOX
            // 
            this._usersLISTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._usersLISTBOX.Dock = System.Windows.Forms.DockStyle.Top;
            this._usersLISTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._usersLISTBOX.FormattingEnabled = true;
            this._usersLISTBOX.HorizontalScrollbar = true;
            this._usersLISTBOX.ItemHeight = 19;
            this._usersLISTBOX.Location = new System.Drawing.Point(0, 56);
            this._usersLISTBOX.Name = "_usersLISTBOX";
            this._usersLISTBOX.Size = new System.Drawing.Size(180, 133);
            this._usersLISTBOX.TabIndex = 0;
            this._usersLISTBOX.Click += new System.EventHandler(this._functLISTBOX_Click);
            // 
            // _backGroundUsersPANEL
            // 
            this._backGroundUsersPANEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this._backGroundUsersPANEL.Controls.Add(this._usersNameListBoxLABEL);
            this._backGroundUsersPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._backGroundUsersPANEL.Location = new System.Drawing.Point(0, 0);
            this._backGroundUsersPANEL.Name = "_backGroundUsersPANEL";
            this._backGroundUsersPANEL.Size = new System.Drawing.Size(180, 56);
            this._backGroundUsersPANEL.TabIndex = 1;
            // 
            // _usersNameListBoxLABEL
            // 
            this._usersNameListBoxLABEL.AutoSize = true;
            this._usersNameListBoxLABEL.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._usersNameListBoxLABEL.ForeColor = System.Drawing.Color.White;
            this._usersNameListBoxLABEL.Location = new System.Drawing.Point(33, 16);
            this._usersNameListBoxLABEL.Name = "_usersNameListBoxLABEL";
            this._usersNameListBoxLABEL.Size = new System.Drawing.Size(123, 23);
            this._usersNameListBoxLABEL.TabIndex = 0;
            this._usersNameListBoxLABEL.Text = "Пользователи";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(75)))), ((int)(((byte)(144)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(180, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 376);
            this.panel2.TabIndex = 4;
            // 
            // _informationLABEL
            // 
            this._informationLABEL.AutoSize = true;
            this._informationLABEL.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._informationLABEL.ForeColor = System.Drawing.Color.DimGray;
            this._informationLABEL.Location = new System.Drawing.Point(74, 180);
            this._informationLABEL.Name = "_informationLABEL";
            this._informationLABEL.Size = new System.Drawing.Size(327, 26);
            this._informationLABEL.TabIndex = 7;
            this._informationLABEL.Text = "Выберите функцию и пользователя";
            // 
            // _contentPanel
            // 
            this._contentPanel.AutoScroll = true;
            this._contentPanel.Controls.Add(this._informationLABEL);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(184, 28);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(489, 376);
            this._contentPanel.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 406);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(675, 25);
            this.panel3.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._saveData);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(452, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(223, 25);
            this.panel4.TabIndex = 27;
            // 
            // _saveData
            // 
            this._saveData.AutoSize = true;
            this._saveData.Cursor = System.Windows.Forms.Cursors.Hand;
            this._saveData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._saveData.ForeColor = System.Drawing.Color.White;
            this._saveData.Location = new System.Drawing.Point(126, 3);
            this._saveData.Name = "_saveData";
            this._saveData.Size = new System.Drawing.Size(81, 19);
            this._saveData.TabIndex = 16;
            this._saveData.Text = "Сохранить";
            this._saveData.Click += new System.EventHandler(this._saveData_Click);
            // 
            // _screenShotIMAGELIST
            // 
            this._screenShotIMAGELIST.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this._screenShotIMAGELIST.ImageSize = new System.Drawing.Size(90, 90);
            this._screenShotIMAGELIST.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _screenShotTIMER
            // 
            this._screenShotTIMER.Interval = 1;
            this._screenShotTIMER.Tick += new System.EventHandler(this._screenShotTIMER_Tick);
            // 
            // _recordVideoTIMER
            // 
            this._recordVideoTIMER.Interval = 1;
            this._recordVideoTIMER.Tick += new System.EventHandler(this._recordVideoTIMER_Tick);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(675, 431);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "ControlPanel";
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this._topBorder, 0);
            this.Controls.SetChildIndex(this._rightBorder, 0);
            this.Controls.SetChildIndex(this._leftBorder, 0);
            this.Controls.SetChildIndex(this._controlWIndowPANEL, 0);
            this.Controls.SetChildIndex(this._bottomBorder, 0);
            this.Controls.SetChildIndex(this._underPanelBorder, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this._contentPanel, 0);
            this._controlWIndowPANEL.ResumeLayout(false);
            this._controlWIndowPANEL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._backGroundFuncPANEL.ResumeLayout(false);
            this._backGroundFuncPANEL.PerformLayout();
            this._backGroundUsersPANEL.ResumeLayout(false);
            this._backGroundUsersPANEL.PerformLayout();
            this._contentPanel.ResumeLayout(false);
            this._contentPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel _backGroundUsersPANEL;
        private System.Windows.Forms.Label _usersNameListBoxLABEL;
        private System.Windows.Forms.ListBox _usersLISTBOX;
        private System.Windows.Forms.ListBox _functLISTBOX;
        private System.Windows.Forms.Panel _backGroundFuncPANEL;
        private System.Windows.Forms.Label _funcNameListBoxLABEL;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _informationLABEL;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Label _listUsersCleaLABEL;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label _saveData;
        private System.Windows.Forms.ImageList _screenShotIMAGELIST;
        private System.Windows.Forms.Timer _screenShotTIMER;
        private System.Windows.Forms.Timer _recordVideoTIMER;
    }
}