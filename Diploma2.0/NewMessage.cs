using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appearance;
using ErrorInPrograms;
using SqlQueryProcessing;

namespace Diploma2._0
{
    public partial class NewMessage : StyleWindows
    {
        Sql SQL = new Sql();
        bool _thereIsUser = false;
        public NewMessage()
        {
            InitializeComponent();
           
            _namePageLABEL.Name = "Написание сообщения";
            _iconWindowFLP.BackgroundImage = Properties.Resources.Message_Mail_128;
            _closeWindowPICTUREBOX.Location = new Point(ClientSize.Width - _closeWindowPICTUREBOX.Width - 15, _closeWindowPICTUREBOX.Location.Y);
        }

        private void NewMessage_Load(object sender, EventArgs e)
        {
            _usersCOMBOBOX.Font = StyleWindows._mainFont;
            new Thread(() =>
            {
                LoadLoginUsers();
            }).Start();
            _functionPanelFLP.Width = _usersCOMBOBOX.Height + 2;
            _msgRTB.Font = StyleWindows._mainFont;
            _msgRTB.Enter += (s, a) => { _msgRTB.Text = ""; };
            _msgRTB.Leave += (s, a) => { _msgRTB.Text = "Ваше сообщение..."; };
            _saveData.Click += (s, a) =>
            {
                if (_msgRTB.Text.Length > 0)
                {
                    if (_thereIsUser)
                    {
                        SQL.CreateMessage(Form1._Login, _usersCOMBOBOX.SelectedItem.ToString(), _msgRTB.Text);
                        Warning.SaveDataUser();
                    }
                    else
                        Error.ClientError.NotFound();
                }
                else
                {
                    Error.ClientError.BadRequest();
                }
            };
        }

        private void LoadLoginUsers()
        {
            
            var data = SQL.ListChild();
            _usersCOMBOBOX.Items.AddRange(data.ToArray());
            if (_usersCOMBOBOX.Items.Count > -1)
            {
                _usersCOMBOBOX.SelectedIndex = 0;
                _thereIsUser = true;
            }
        }
    }
}
