using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;
using Appearance;
using ErrorInPrograms;

namespace Diploma2._0
{
    public partial class Feedback : StyleWindows
    {
        private string _technicalSupportMail { get;  } = "ksis.technical.mail@gmail.com";
        public Feedback()
        {
            InitializeComponent();
            //
            //  Настройка окна
            //
            _namePageLABEL.Text = "Обратная связь";
            _iconWindowFLP.BackgroundImage = Properties.Resources.Message_Mail_128;
            _closeWindowPICTUREBOX.Location = new Point(ClientSize.Width - _closeWindowPICTUREBOX.Width - 15, _closeWindowPICTUREBOX.Location.Y);
            ContentAppealRTB.Focus();

            ContentAppealRTB.Font = StyleWindows._mainFont;
            _nameUserTEXTBOX.Font = StyleWindows._mainFont;
            _surnameUserTEXTBOX.Font = StyleWindows._mainFont;
            _mailTEXTBOX.Font = StyleWindows._mainFont;
            _passwordTEXTBOX.Font = StyleWindows._mainFont;
            _messageSubjectTEXTBOX.Font = StyleWindows._mainFont;

            //
            //  Настройка шрифтов элементов
            //

            //
            //  Позиция элементов
            //
            try
            {
                if (Form1._Login.Length > 0 && Form1._Surname.Length > 0)
                {
                    _nameUserTEXTBOX.Text = Form1._Name;
                    _surnameUserTEXTBOX.Text = Form1._Surname;
                }
            }
            catch
            {
            }
        }

        private void _messagePANEL_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen p = new Pen(_mainColor, 3.0f);
            // _nameUserTEXTBOX
            x = _nameUserTEXTBOX.Location.X;
            y = _nameUserTEXTBOX.Location.Y;
            width = _nameUserTEXTBOX.Width;
            height = _nameUserTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // _surnameUserTEXTBOX
            x = _surnameUserTEXTBOX.Location.X;
            y = _surnameUserTEXTBOX.Location.Y;
            width = _surnameUserTEXTBOX.Width;
            height = _surnameUserTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // _mailTEXTBOX
            x = _mailTEXTBOX.Location.X;
            y = _mailTEXTBOX.Location.Y;
            width = _mailTEXTBOX.Width;
            height = _mailTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // _passwordTEXTBOX
            x = _passwordTEXTBOX.Location.X;
            y = _passwordTEXTBOX.Location.Y;
            width = _passwordTEXTBOX.Width;
            height = _passwordTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // _messageSubjectTEXTBOX
            x = _messageSubjectTEXTBOX.Location.X;
            y = _messageSubjectTEXTBOX.Location.Y;
            width = _messageSubjectTEXTBOX.Width;
            height = _messageSubjectTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);

        }

        private void _nameUserTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_nameUserTEXTBOX.Text == "Имя")
            {
                _nameUserTEXTBOX.Text = "";
                _nameUserTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _nameUserTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_nameUserTEXTBOX.Text == "")
            {
                _nameUserTEXTBOX.Text = "Имя";
                _nameUserTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }

        private void _surnameUserTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_surnameUserTEXTBOX.Text == "Фамилия")
            {
                _surnameUserTEXTBOX.Text = "";
                _surnameUserTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _surnameUserTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_surnameUserTEXTBOX.Text == "")
            {
                _surnameUserTEXTBOX.Text = "Фамилия";
                _surnameUserTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }

        private void _mailTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_mailTEXTBOX.Text == "Mail")
            {
                _mailTEXTBOX.Text = "";
                _mailTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _mailTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_mailTEXTBOX.Text == "")
            {
                _mailTEXTBOX.Text = "Mail";
                _mailTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }

        private void _messageSubjectTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_messageSubjectTEXTBOX.Text == "Тема обращения")
            {
                _messageSubjectTEXTBOX.Text = "";
                _messageSubjectTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _messageSubjectTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_messageSubjectTEXTBOX.Text == "")
            {
                _messageSubjectTEXTBOX.Text = "Mail";
                _messageSubjectTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }


        private void ContentAppealRTB_Enter(object sender, EventArgs e)
        {
            if (ContentAppealRTB.Text == "Содержание обращения...")
            {
                ContentAppealRTB.Text = "";
                ContentAppealRTB.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void ContentAppealRTB_Leave(object sender, EventArgs e)
        {
            if (ContentAppealRTB.Text == "")
            {
                ContentAppealRTB.Text = "Содержание обращения...";
                ContentAppealRTB.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }

        private string DefinitionOfMail(string mail)
        {
            bool isMail = false;
            string host = "";
            foreach (char value in mail)
            {
                if (isMail)
                {
                    host += value;
                }
                else if (value == '@') isMail = true;
            }
            return host;
        }

        private bool CheckHostMail(string host, string mail, string password, string title, string body, string name, string surname)
        {
            switch (host)
            {
                case "gmail.com":
                    CreateMessage(host, mail, password, title, body, name, surname, _technicalSupportMail);
                    return true;
                default:
                    return false;
            }
        }

        private void CreateMessage(string host, string mail, string password, string title, string body, string name, string surname, string toMail)
        {
            var fromAddress = new MailAddress(mail, name + " " + surname);
            var toAddress = new MailAddress(toMail, "Support");
            string fromPassword = password;
            string subject = title;
            string bodyMessage = "<table border='0' style=\"border - bottom: 2px solid black; font-size: 22px; font-family: Calibri\"><tr><td>Имя: </td> <td>" + name + "</td></tr><tr><td>Фамилия: </td><td>" + surname + "</td></tr><tr><td>Mail: </td><td>" + mail + "</td></tr></table><h2>Тема: " + title + "</h2><p style=\"border: 0.5px solid silver; width: 600px\">" + body + "</p>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = bodyMessage,
                IsBodyHtml = true,
            })
            {
                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Сообщение успешно отправлено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void _saveData_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            this.Enabled = false;
            //   videoyrokyrok@yandex.ru;12345678qwerty
            string supportMail = @"ksis22@yandex.ru";
            string supportPassword = @"lbr100ru";

            if(!CheckHostMail(DefinitionOfMail(_mailTEXTBOX.Text), _mailTEXTBOX.Text, _passwordTEXTBOX.Text, _messageSubjectTEXTBOX.Text, ContentAppealRTB.Text, _nameUserTEXTBOX.Text, _surnameUserTEXTBOX.Text))
            {
                MessageBox.Show("Упс");
            }

            
            // ksis22@yandex.ru
            /*
            string supportMail = @"ksis22@yandex.ru";
            string supportPassword = @"lbr100ru";

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress From = new MailAddress(supportMail, "Техническая поддержка");
            MailAddress To = new MailAddress(supportMail);
            MailMessage msg = new MailMessage(From, To);
            msg.Subject = "Тестовое письмо";
            msg.Body = "Test";
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential(_technicalSupportMail, "Lfybbk123ru");
            smtp.EnableSsl = true;
            
            try
            {
                smtp.Send(msg);
                MessageBox.Show("Сообщение успешно отправлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }*/
            this.Enabled = true;
        }

        private void _passwordTEXTBOX_Enter(object sender, EventArgs e)
        {
            if (_passwordTEXTBOX.Text == "Пароль")
            {
                _passwordTEXTBOX.UseSystemPasswordChar = true;
                _passwordTEXTBOX.Text = "";
                _passwordTEXTBOX.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void _passwordTEXTBOX_Leave(object sender, EventArgs e)
        {
            if (_passwordTEXTBOX.Text == "")
            {
                _passwordTEXTBOX.UseSystemPasswordChar = false;
                _passwordTEXTBOX.Text = "Пароль";
                _passwordTEXTBOX.ForeColor = Color.FromArgb(98, 98, 98);
            }
        }
    }
}
