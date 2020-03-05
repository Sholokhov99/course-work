using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appearance;
using SqlQueryProcessing;
using ErrorInPrograms;

namespace Diploma2._0
{
    public partial class MoreTime : StyleWindows
    {
        Form1 MAINFORM = new Form1();
        Sql SQL = new Sql();

         

        public MoreTime()
        {
            InitializeComponent();
            MAINFORM.SetRoundedShape(_loginTEXTBOX, 13);
            MAINFORM.SetRoundedShape(_passwordTEXTBOX, 13);
        }

        private void _loginFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(StyleWindows._mainColor, 2.0f);
            int radius = 15;
            int x = _loginTEXTBOX.Location.X;
            int y = _loginTEXTBOX.Location.Y;
            int width = x + _loginTEXTBOX.Width;
            int height = _loginTEXTBOX.Height;
            int padding = 1;
            //
            //  _loginTEXTBOX
            //
            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2));                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x - padding - 1, y + height - (radius / 2) + padding, x - padding - 1, y - padding + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);                                                      //
            //
            //  _passwordTEXTBOX
            //
            x = _passwordTEXTBOX.Location.X;
            y = _passwordTEXTBOX.Location.Y;
            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2));                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x - padding - 1, y + height - (radius / 2) + padding, x - padding - 1, y - padding + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }

        private void _loginTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_loginTEXTBOX.Text == "")
            {
                _loginTEXTBOX.Text = "Логин";
                _loginTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }
        //  Исчезновение надписи логин
        private void _loginTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_loginTEXTBOX.Text == "Логин")
            {
                _loginTEXTBOX.Text = "";
                _loginTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }
        //  Появоение надписи пароль
        private void _passwordTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_passwordTEXTBOX.Text == "")
            {
                _passwordTEXTBOX.UseSystemPasswordChar = false;
                _passwordTEXTBOX.Text = "Пароль";
                _passwordTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }
        //  Исчезновение надписи пароль
        private void _passwordTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_passwordTEXTBOX.Text == "Пароль")
            {
                _passwordTEXTBOX.UseSystemPasswordChar = true;
                _passwordTEXTBOX.Text = "";
                _passwordTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _nextBUTTON_Click(object sender, EventArgs e)
        {
            _loginFLP.Enabled = false;
            var data = SQL.UserAuthorization(_loginTEXTBOX.Text, _passwordTEXTBOX.Text);
            if (data.Count > 1)
            {
                if (data[24] != "1")
                {
                    Error.ClientError.Locked();
                }
                else
                {


                    _loginFLP.Controls.Remove(_loginTEXTBOX);
                    _loginFLP.Controls.Remove(_passwordTEXTBOX);
                }
            }
            else
            {
                if (data.Count == 0)
                {

                }
            }
            // data[24]
            _loginFLP.Enabled = true;
        }
    }
}
