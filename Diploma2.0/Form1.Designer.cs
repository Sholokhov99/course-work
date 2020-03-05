namespace Diploma2._0
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._loginPANEL = new System.Windows.Forms.Panel();
            this._resPassLABEL = new System.Windows.Forms.Label();
            this._menuWindowsFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._сhangeUser = new System.Windows.Forms.PictureBox();
            this._rebootPc = new System.Windows.Forms.PictureBox();
            this._sleepMode = new System.Windows.Forms.PictureBox();
            this._offPc = new System.Windows.Forms.PictureBox();
            this._hearingProblem = new System.Windows.Forms.PictureBox();
            this._dataLoginFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._loginTEXTBOX = new System.Windows.Forms.TextBox();
            this._passwordTEXTBOX = new System.Windows.Forms.TextBox();
            this._nextBUTTON = new System.Windows.Forms.Button();
            this._checkLanguageTIMER = new System.Windows.Forms.Timer(this.components);
            this._time = new System.Windows.Forms.Label();
            this._loginPANEL.SuspendLayout();
            this._menuWindowsFLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._сhangeUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rebootPc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._sleepMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._offPc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._hearingProblem)).BeginInit();
            this._dataLoginFLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // _loginPANEL
            // 
            this._loginPANEL.BackColor = System.Drawing.Color.Black;
            this._loginPANEL.Controls.Add(this._resPassLABEL);
            this._loginPANEL.Controls.Add(this._menuWindowsFLP);
            this._loginPANEL.Controls.Add(this._dataLoginFLP);
            this._loginPANEL.Location = new System.Drawing.Point(253, 137);
            this._loginPANEL.Name = "_loginPANEL";
            this._loginPANEL.Size = new System.Drawing.Size(259, 215);
            this._loginPANEL.TabIndex = 0;
            // 
            // _resPassLABEL
            // 
            this._resPassLABEL.AutoSize = true;
            this._resPassLABEL.BackColor = System.Drawing.Color.Transparent;
            this._resPassLABEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this._resPassLABEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._resPassLABEL.ForeColor = System.Drawing.Color.White;
            this._resPassLABEL.Location = new System.Drawing.Point(126, 149);
            this._resPassLABEL.Name = "_resPassLABEL";
            this._resPassLABEL.Size = new System.Drawing.Size(104, 15);
            this._resPassLABEL.TabIndex = 3;
            this._resPassLABEL.Text = "Забыли пароль?";
            this._resPassLABEL.Click += new System.EventHandler(this._resPassLABEL_Click);
            // 
            // _menuWindowsFLP
            // 
            this._menuWindowsFLP.BackColor = System.Drawing.Color.Transparent;
            this._menuWindowsFLP.Controls.Add(this._сhangeUser);
            this._menuWindowsFLP.Controls.Add(this._rebootPc);
            this._menuWindowsFLP.Controls.Add(this._sleepMode);
            this._menuWindowsFLP.Controls.Add(this._offPc);
            this._menuWindowsFLP.Controls.Add(this._hearingProblem);
            this._menuWindowsFLP.Location = new System.Drawing.Point(22, 167);
            this._menuWindowsFLP.Name = "_menuWindowsFLP";
            this._menuWindowsFLP.Size = new System.Drawing.Size(205, 28);
            this._menuWindowsFLP.TabIndex = 2;
            // 
            // _сhangeUser
            // 
            this._сhangeUser.BackgroundImage = global::Diploma2._0.Properties.Resources.changeUser;
            this._сhangeUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._сhangeUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this._сhangeUser.Location = new System.Drawing.Point(3, 3);
            this._сhangeUser.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this._сhangeUser.Name = "_сhangeUser";
            this._сhangeUser.Size = new System.Drawing.Size(20, 20);
            this._сhangeUser.TabIndex = 0;
            this._сhangeUser.TabStop = false;
            this._сhangeUser.Click += new System.EventHandler(this._сhangeUser_Click);
            // 
            // _rebootPc
            // 
            this._rebootPc.BackgroundImage = global::Diploma2._0.Properties.Resources.reboot;
            this._rebootPc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._rebootPc.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rebootPc.Location = new System.Drawing.Point(48, 3);
            this._rebootPc.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this._rebootPc.Name = "_rebootPc";
            this._rebootPc.Size = new System.Drawing.Size(20, 20);
            this._rebootPc.TabIndex = 1;
            this._rebootPc.TabStop = false;
            // 
            // _sleepMode
            // 
            this._sleepMode.BackgroundImage = global::Diploma2._0.Properties.Resources.sleepMode;
            this._sleepMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._sleepMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this._sleepMode.Location = new System.Drawing.Point(93, 3);
            this._sleepMode.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this._sleepMode.Name = "_sleepMode";
            this._sleepMode.Size = new System.Drawing.Size(20, 20);
            this._sleepMode.TabIndex = 2;
            this._sleepMode.TabStop = false;
            this._sleepMode.Click += new System.EventHandler(this._sleepMode_Click);
            // 
            // _offPc
            // 
            this._offPc.BackgroundImage = global::Diploma2._0.Properties.Resources.offPc;
            this._offPc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._offPc.Cursor = System.Windows.Forms.Cursors.Hand;
            this._offPc.Location = new System.Drawing.Point(138, 3);
            this._offPc.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this._offPc.Name = "_offPc";
            this._offPc.Size = new System.Drawing.Size(20, 20);
            this._offPc.TabIndex = 3;
            this._offPc.TabStop = false;
            this._offPc.Click += new System.EventHandler(this._offPc_Click);
            // 
            // _hearingProblem
            // 
            this._hearingProblem.BackgroundImage = global::Diploma2._0.Properties.Resources.specIcon;
            this._hearingProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._hearingProblem.Cursor = System.Windows.Forms.Cursors.Hand;
            this._hearingProblem.Location = new System.Drawing.Point(183, 3);
            this._hearingProblem.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this._hearingProblem.Name = "_hearingProblem";
            this._hearingProblem.Size = new System.Drawing.Size(20, 20);
            this._hearingProblem.TabIndex = 4;
            this._hearingProblem.TabStop = false;
            this._hearingProblem.Click += new System.EventHandler(this._hearingProblem_Click);
            // 
            // _dataLoginFLP
            // 
            this._dataLoginFLP.BackColor = System.Drawing.Color.Transparent;
            this._dataLoginFLP.Controls.Add(this._loginTEXTBOX);
            this._dataLoginFLP.Controls.Add(this._passwordTEXTBOX);
            this._dataLoginFLP.Controls.Add(this._nextBUTTON);
            this._dataLoginFLP.Location = new System.Drawing.Point(25, 18);
            this._dataLoginFLP.Name = "_dataLoginFLP";
            this._dataLoginFLP.Size = new System.Drawing.Size(205, 113);
            this._dataLoginFLP.TabIndex = 1;
            this._dataLoginFLP.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // _loginTEXTBOX
            // 
            this._loginTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._loginTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._loginTEXTBOX.Location = new System.Drawing.Point(3, 3);
            this._loginTEXTBOX.Name = "_loginTEXTBOX";
            this._loginTEXTBOX.Size = new System.Drawing.Size(200, 20);
            this._loginTEXTBOX.TabIndex = 2;
            this._loginTEXTBOX.Text = "Логин";
            this._loginTEXTBOX.Enter += new System.EventHandler(this._loginTEXTBOX_Enter);
            this._loginTEXTBOX.Leave += new System.EventHandler(this._loginTEXTBOX_Leave);
            // 
            // _passwordTEXTBOX
            // 
            this._passwordTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._passwordTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._passwordTEXTBOX.Location = new System.Drawing.Point(3, 36);
            this._passwordTEXTBOX.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this._passwordTEXTBOX.Name = "_passwordTEXTBOX";
            this._passwordTEXTBOX.Size = new System.Drawing.Size(200, 20);
            this._passwordTEXTBOX.TabIndex = 4;
            this._passwordTEXTBOX.Text = "Пароль";
            this._passwordTEXTBOX.Enter += new System.EventHandler(this._passwordTEXTBOX_Enter);
            this._passwordTEXTBOX.Leave += new System.EventHandler(this._passwordTEXTBOX_Leave);
            // 
            // _nextBUTTON
            // 
            this._nextBUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(75)))), ((int)(((byte)(144)))));
            this._nextBUTTON.Cursor = System.Windows.Forms.Cursors.Hand;
            this._nextBUTTON.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(46)))), ((int)(((byte)(117)))));
            this._nextBUTTON.FlatAppearance.BorderSize = 2;
            this._nextBUTTON.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this._nextBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextBUTTON.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._nextBUTTON.ForeColor = System.Drawing.Color.White;
            this._nextBUTTON.Location = new System.Drawing.Point(3, 69);
            this._nextBUTTON.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this._nextBUTTON.Name = "_nextBUTTON";
            this._nextBUTTON.Size = new System.Drawing.Size(200, 40);
            this._nextBUTTON.TabIndex = 0;
            this._nextBUTTON.Text = "Авторизироваться";
            this._nextBUTTON.UseVisualStyleBackColor = false;
            this._nextBUTTON.Click += new System.EventHandler(this._nextBUTTON_Click);
            // 
            // _checkLanguageTIMER
            // 
            this._checkLanguageTIMER.Tick += new System.EventHandler(this._checkLanguageTIMER_Tick);
            // 
            // _time
            // 
            this._time.AutoSize = true;
            this._time.BackColor = System.Drawing.Color.Transparent;
            this._time.Font = new System.Drawing.Font("Segoe MDL2 Assets", 62.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._time.ForeColor = System.Drawing.Color.White;
            this._time.Location = new System.Drawing.Point(536, 340);
            this._time.Name = "_time";
            this._time.Size = new System.Drawing.Size(344, 83);
            this._time.TabIndex = 1;
            this._time.Text = "Загрузка...";
            // 
            // Form1
            // 
            this.AcceptButton = this._nextBUTTON;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Diploma2._0.Properties.Resources.fon;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._time);
            this.Controls.Add(this._loginPANEL);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this._loginPANEL.ResumeLayout(false);
            this._loginPANEL.PerformLayout();
            this._menuWindowsFLP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._сhangeUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rebootPc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._sleepMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._offPc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._hearingProblem)).EndInit();
            this._dataLoginFLP.ResumeLayout(false);
            this._dataLoginFLP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _loginPANEL;
        private System.Windows.Forms.FlowLayoutPanel _dataLoginFLP;
        private System.Windows.Forms.TextBox _loginTEXTBOX;
        private System.Windows.Forms.TextBox _passwordTEXTBOX;
        private System.Windows.Forms.Button _nextBUTTON;
        private System.Windows.Forms.FlowLayoutPanel _menuWindowsFLP;
        private System.Windows.Forms.PictureBox _сhangeUser;
        private System.Windows.Forms.PictureBox _rebootPc;
        private System.Windows.Forms.PictureBox _sleepMode;
        private System.Windows.Forms.PictureBox _offPc;
        private System.Windows.Forms.PictureBox _hearingProblem;
        private System.Windows.Forms.Timer _checkLanguageTIMER;
        private System.Windows.Forms.Label _time;
        private System.Windows.Forms.Label _resPassLABEL;
    }
}

