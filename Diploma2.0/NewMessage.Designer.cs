namespace Diploma2._0
{
    partial class NewMessage
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
            this._functionPanelFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._usersCOMBOBOX = new System.Windows.Forms.ComboBox();
            this._msgRTB = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this._saveData = new System.Windows.Forms.Label();
            this._controlWIndowPANEL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).BeginInit();
            this._functionPanelFLP.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlWIndowPANEL
            // 
            this._controlWIndowPANEL.Size = new System.Drawing.Size(581, 25);
            // 
            // _leftBorder
            // 
            this._leftBorder.Size = new System.Drawing.Size(2, 330);
            // 
            // _rightBorder
            // 
            this._rightBorder.Location = new System.Drawing.Point(583, 2);
            this._rightBorder.Size = new System.Drawing.Size(2, 330);
            // 
            // _topBorder
            // 
            this._topBorder.Size = new System.Drawing.Size(585, 2);
            // 
            // _bottomBorder
            // 
            this._bottomBorder.Location = new System.Drawing.Point(2, 330);
            this._bottomBorder.Size = new System.Drawing.Size(581, 2);
            // 
            // _underPanelBorder
            // 
            this._underPanelBorder.Size = new System.Drawing.Size(581, 1);
            // 
            // _functionPanelFLP
            // 
            this._functionPanelFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._functionPanelFLP.Controls.Add(this._usersCOMBOBOX);
            this._functionPanelFLP.Dock = System.Windows.Forms.DockStyle.Top;
            this._functionPanelFLP.Location = new System.Drawing.Point(2, 28);
            this._functionPanelFLP.Name = "_functionPanelFLP";
            this._functionPanelFLP.Size = new System.Drawing.Size(581, 40);
            this._functionPanelFLP.TabIndex = 6;
            // 
            // _usersCOMBOBOX
            // 
            this._usersCOMBOBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._usersCOMBOBOX.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._usersCOMBOBOX.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._usersCOMBOBOX.FormattingEnabled = true;
            this._usersCOMBOBOX.Location = new System.Drawing.Point(3, 3);
            this._usersCOMBOBOX.Name = "_usersCOMBOBOX";
            this._usersCOMBOBOX.Size = new System.Drawing.Size(197, 33);
            this._usersCOMBOBOX.TabIndex = 0;
            // 
            // _msgRTB
            // 
            this._msgRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._msgRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this._msgRTB.Location = new System.Drawing.Point(2, 68);
            this._msgRTB.Name = "_msgRTB";
            this._msgRTB.Size = new System.Drawing.Size(581, 237);
            this._msgRTB.TabIndex = 7;
            this._msgRTB.Text = "Ваше сообщение...";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(2, 305);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(581, 25);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this._saveData);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(484, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(97, 25);
            this.panel4.TabIndex = 27;
            // 
            // _saveData
            // 
            this._saveData.AutoSize = true;
            this._saveData.Cursor = System.Windows.Forms.Cursors.Hand;
            this._saveData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._saveData.ForeColor = System.Drawing.Color.White;
            this._saveData.Location = new System.Drawing.Point(3, 0);
            this._saveData.Name = "_saveData";
            this._saveData.Size = new System.Drawing.Size(81, 19);
            this._saveData.TabIndex = 16;
            this._saveData.Text = "Отправить";
            // 
            // NewMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 332);
            this.Controls.Add(this._msgRTB);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this._functionPanelFLP);
            this.Name = "NewMessage";
            this.Text = "NewMessage";
            this.Load += new System.EventHandler(this.NewMessage_Load);
            this.Controls.SetChildIndex(this._topBorder, 0);
            this.Controls.SetChildIndex(this._rightBorder, 0);
            this.Controls.SetChildIndex(this._leftBorder, 0);
            this.Controls.SetChildIndex(this._controlWIndowPANEL, 0);
            this.Controls.SetChildIndex(this._bottomBorder, 0);
            this.Controls.SetChildIndex(this._underPanelBorder, 0);
            this.Controls.SetChildIndex(this._functionPanelFLP, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this._msgRTB, 0);
            this._controlWIndowPANEL.ResumeLayout(false);
            this._controlWIndowPANEL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).EndInit();
            this._functionPanelFLP.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _functionPanelFLP;
        private System.Windows.Forms.ComboBox _usersCOMBOBOX;
        private System.Windows.Forms.RichTextBox _msgRTB;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label _saveData;
    }
}