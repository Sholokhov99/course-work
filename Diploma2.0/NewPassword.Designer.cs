namespace Diploma2._0
{
    partial class NewPassword
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._next = new System.Windows.Forms.Button();
            this._dataTEXTBOX = new System.Windows.Forms.TextBox();
            this._messageFromFormLABEL = new System.Windows.Forms.Label();
            this._secretText = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._controlWIndowPANEL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlWIndowPANEL
            // 
            this._controlWIndowPANEL.Size = new System.Drawing.Size(396, 25);
            // 
            // _leftBorder
            // 
            this._leftBorder.Size = new System.Drawing.Size(2, 193);
            // 
            // _rightBorder
            // 
            this._rightBorder.Location = new System.Drawing.Point(398, 2);
            this._rightBorder.Size = new System.Drawing.Size(2, 193);
            // 
            // _topBorder
            // 
            this._topBorder.Size = new System.Drawing.Size(400, 2);
            // 
            // _bottomBorder
            // 
            this._bottomBorder.Location = new System.Drawing.Point(2, 193);
            this._bottomBorder.Size = new System.Drawing.Size(396, 2);
            // 
            // _underPanelBorder
            // 
            this._underPanelBorder.Size = new System.Drawing.Size(396, 1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._next);
            this.panel1.Controls.Add(this._dataTEXTBOX);
            this.panel1.Location = new System.Drawing.Point(61, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 80);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // _next
            // 
            this._next.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(75)))), ((int)(((byte)(144)))));
            this._next.Cursor = System.Windows.Forms.Cursors.Hand;
            this._next.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(46)))), ((int)(((byte)(117)))));
            this._next.FlatAppearance.BorderSize = 2;
            this._next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(101)))), ((int)(((byte)(158)))));
            this._next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._next.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._next.ForeColor = System.Drawing.Color.White;
            this._next.Location = new System.Drawing.Point(3, 36);
            this._next.Name = "_next";
            this._next.Size = new System.Drawing.Size(180, 40);
            this._next.TabIndex = 1;
            this._next.Text = "Далее";
            this._next.UseVisualStyleBackColor = false;
            this._next.Click += new System.EventHandler(this._next_Click);
            // 
            // _dataTEXTBOX
            // 
            this._dataTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._dataTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._dataTEXTBOX.Location = new System.Drawing.Point(3, 3);
            this._dataTEXTBOX.Name = "_dataTEXTBOX";
            this._dataTEXTBOX.Size = new System.Drawing.Size(180, 20);
            this._dataTEXTBOX.TabIndex = 0;
            // 
            // _messageFromFormLABEL
            // 
            this._messageFromFormLABEL.AutoSize = true;
            this._messageFromFormLABEL.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._messageFromFormLABEL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(75)))), ((int)(((byte)(144)))));
            this._messageFromFormLABEL.Location = new System.Drawing.Point(3, 0);
            this._messageFromFormLABEL.Name = "_messageFromFormLABEL";
            this._messageFromFormLABEL.Size = new System.Drawing.Size(131, 23);
            this._messageFromFormLABEL.TabIndex = 1;
            this._messageFromFormLABEL.Text = "Введите логин";
            // 
            // _secretText
            // 
            this._secretText.AutoSize = true;
            this._secretText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._secretText.Location = new System.Drawing.Point(3, 23);
            this._secretText.Name = "_secretText";
            this._secretText.Size = new System.Drawing.Size(0, 19);
            this._secretText.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._messageFromFormLABEL);
            this.panel2.Controls.Add(this._secretText);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 165);
            this.panel2.TabIndex = 7;
            // 
            // NewPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 195);
            this.Controls.Add(this.panel2);
            this.Name = "NewPassword";
            this.Text = "NewPassword";
            this.Controls.SetChildIndex(this._topBorder, 0);
            this.Controls.SetChildIndex(this._rightBorder, 0);
            this.Controls.SetChildIndex(this._leftBorder, 0);
            this.Controls.SetChildIndex(this._controlWIndowPANEL, 0);
            this.Controls.SetChildIndex(this._bottomBorder, 0);
            this.Controls.SetChildIndex(this._underPanelBorder, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this._controlWIndowPANEL.ResumeLayout(false);
            this._controlWIndowPANEL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _next;
        private System.Windows.Forms.TextBox _dataTEXTBOX;
        private System.Windows.Forms.Label _messageFromFormLABEL;
        private System.Windows.Forms.Label _secretText;
        private System.Windows.Forms.Panel panel2;
    }
}