namespace Diploma2._0
{
    partial class MoreTime
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
            this._loginTEXTBOX = new System.Windows.Forms.TextBox();
            this._loginFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._passwordTEXTBOX = new System.Windows.Forms.TextBox();
            this._nextBUTTON = new System.Windows.Forms.Button();
            this._errorEnterLABEL = new System.Windows.Forms.Label();
            this._controlWIndowPANEL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).BeginInit();
            this._loginFLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlWIndowPANEL
            // 
            this._controlWIndowPANEL.Size = new System.Drawing.Size(458, 25);
            // 
            // _leftBorder
            // 
            this._leftBorder.Size = new System.Drawing.Size(2, 217);
            // 
            // _rightBorder
            // 
            this._rightBorder.Location = new System.Drawing.Point(460, 2);
            this._rightBorder.Size = new System.Drawing.Size(2, 217);
            // 
            // _topBorder
            // 
            this._topBorder.Size = new System.Drawing.Size(462, 2);
            // 
            // _bottomBorder
            // 
            this._bottomBorder.Location = new System.Drawing.Point(2, 217);
            this._bottomBorder.Size = new System.Drawing.Size(458, 2);
            // 
            // _underPanelBorder
            // 
            this._underPanelBorder.Size = new System.Drawing.Size(458, 1);
            // 
            // _closeWindowPICTUREBOX
            // 
            this._closeWindowPICTUREBOX.Location = new System.Drawing.Point(311, 7);
            // 
            // _loginTEXTBOX
            // 
            this._loginTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._loginTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._loginTEXTBOX.Location = new System.Drawing.Point(3, 3);
            this._loginTEXTBOX.Name = "_loginTEXTBOX";
            this._loginTEXTBOX.Size = new System.Drawing.Size(200, 20);
            this._loginTEXTBOX.TabIndex = 6;
            this._loginTEXTBOX.Text = "Логин";
            this._loginTEXTBOX.Enter += new System.EventHandler(this._loginTEXTBOX_Enter);
            this._loginTEXTBOX.Leave += new System.EventHandler(this._loginTEXTBOX_Leave);
            // 
            // _loginFLP
            // 
            this._loginFLP.Controls.Add(this._loginTEXTBOX);
            this._loginFLP.Controls.Add(this._passwordTEXTBOX);
            this._loginFLP.Controls.Add(this._nextBUTTON);
            this._loginFLP.Location = new System.Drawing.Point(121, 88);
            this._loginFLP.Name = "_loginFLP";
            this._loginFLP.Size = new System.Drawing.Size(207, 119);
            this._loginFLP.TabIndex = 7;
            this._loginFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._loginFLP_Paint);
            // 
            // _passwordTEXTBOX
            // 
            this._passwordTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._passwordTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._passwordTEXTBOX.Location = new System.Drawing.Point(3, 36);
            this._passwordTEXTBOX.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this._passwordTEXTBOX.Name = "_passwordTEXTBOX";
            this._passwordTEXTBOX.Size = new System.Drawing.Size(200, 20);
            this._passwordTEXTBOX.TabIndex = 7;
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
            this._nextBUTTON.TabIndex = 8;
            this._nextBUTTON.Text = "Подтвердить";
            this._nextBUTTON.UseVisualStyleBackColor = false;
            this._nextBUTTON.Click += new System.EventHandler(this._nextBUTTON_Click);
            // 
            // _errorEnterLABEL
            // 
            this._errorEnterLABEL.AutoSize = true;
            this._errorEnterLABEL.BackColor = System.Drawing.Color.Transparent;
            this._errorEnterLABEL.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._errorEnterLABEL.ForeColor = System.Drawing.Color.Red;
            this._errorEnterLABEL.Location = new System.Drawing.Point(117, 66);
            this._errorEnterLABEL.Name = "_errorEnterLABEL";
            this._errorEnterLABEL.Size = new System.Drawing.Size(223, 19);
            this._errorEnterLABEL.TabIndex = 1;
            this._errorEnterLABEL.Text = "Данные введены не корректно";
            this._errorEnterLABEL.Visible = false;
            // 
            // MoreTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 219);
            this.Controls.Add(this._errorEnterLABEL);
            this.Controls.Add(this._loginFLP);
            this.Name = "MoreTime";
            this.Text = "MoreTime";
            this.Controls.SetChildIndex(this._loginFLP, 0);
            this.Controls.SetChildIndex(this._topBorder, 0);
            this.Controls.SetChildIndex(this._rightBorder, 0);
            this.Controls.SetChildIndex(this._leftBorder, 0);
            this.Controls.SetChildIndex(this._controlWIndowPANEL, 0);
            this.Controls.SetChildIndex(this._bottomBorder, 0);
            this.Controls.SetChildIndex(this._underPanelBorder, 0);
            this.Controls.SetChildIndex(this._errorEnterLABEL, 0);
            this._controlWIndowPANEL.ResumeLayout(false);
            this._controlWIndowPANEL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).EndInit();
            this._loginFLP.ResumeLayout(false);
            this._loginFLP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _loginTEXTBOX;
        private System.Windows.Forms.FlowLayoutPanel _loginFLP;
        private System.Windows.Forms.TextBox _passwordTEXTBOX;
        private System.Windows.Forms.Button _nextBUTTON;
        private System.Windows.Forms.Label _errorEnterLABEL;
    }
}