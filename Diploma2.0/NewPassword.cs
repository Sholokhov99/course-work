using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appearance;
using SqlQueryProcessing;
using ErrorInPrograms;

namespace Diploma2._0
{
    public partial class NewPassword : StyleWindows
    {
        Form1 MAINFORM = new Form1();
        NewUser NEWUSER = new NewUser();
        Sql SQL = new Sql();
        Temp TEMP = new Temp();

        private const int _widthClientSize = 400;
        private const int _heightClientSize = 195;
        private string _logPassword { get; set; }
        private int _steep = 0;

        struct Temp
        {
            public string Login { get; set; }
            public string SecretText { get; set; }
            public string SecretWord { get; set; }
            public string NewPassword { get; set; }

        }

        public NewPassword(bool hearingProblems)
        {
            InitializeComponent();
            if (!hearingProblems)
            {
                _messageFromFormLABEL.ForeColor = StyleWindows._mainColor;
                _secretText.ForeColor = StyleWindows._mainColor;
                _next.BackColor = StyleWindows._mainColorLoginForm;
                _next.FlatAppearance.BorderColor = StyleWindows._mainBorderColorLoginForm;
                _next.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColorLoginForm;
                _next.ForeColor = StyleWindows._mainColorText;
            }
            else
            {
                _messageFromFormLABEL.ForeColor = StyleWindows._mainColorText;
                _secretText.ForeColor = StyleWindows._mainColorText;
                _next.BackColor = Color.White;
                _next.FlatAppearance.BorderColor = StyleWindows._mainColor;
                _next.FlatAppearance.MouseOverBackColor = Color.White;
                _next.ForeColor = StyleWindows._mainColorText;
            }

            _closeWindowPICTUREBOX.Location = new Point(_widthClientSize - _closeWindowPICTUREBOX.Width - 15, 4);
            LocationMessageFromForm();
            LocationMessageSecretText();
            LocationPanel1();
        }
        //
        //  Выравнивание текста сообщения на форме
        //
        private void LocationMessageFromForm() => _messageFromFormLABEL.Location = new Point(panel2.Width / 2 - _messageFromFormLABEL.Width / 2, 10);
        private void LocationMessageSecretText() => _secretText.Location = new Point(panel2.Width / 2 - _secretText.Width / 2, _messageFromFormLABEL.Location.Y + _messageFromFormLABEL.Height + 5);
        private void LocationPanel1() => panel1.Location = new Point(panel2.Width / 2 - panel1.Width / 2, _secretText.Location.Y + _secretText.Height + 10);

        //
        //  Вывод сообщения о отсутствии введенного значения
        //
        private void ErrorEnterTextBox() => MessageBox.Show("Поле должно быть заполнено\nИ содержать не менее 5-ти символов", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //
        //  Отрисовка элементов на форме
        //
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(_mainColor, 3.0f);
            // Нижняя линия логина
            int x = _dataTEXTBOX.Location.X;
            int y = _dataTEXTBOX.Location.Y;
            int width = _dataTEXTBOX.Width;
            int height = _dataTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // Цвет приложения
        }
        //
        //  Изменение пароля
        //
        private void _next_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _next.Enabled = false;
            _dataTEXTBOX.Enabled = false;
            if (_steep == 0)
            {
                int codeOperation = SQL.CheckingLoginForUniqueness(_dataTEXTBOX.Text);
                if (codeOperation == 0)
                {
                    TEMP.Login = _dataTEXTBOX.Text;
                    _dataTEXTBOX.Text = "";

                    _messageFromFormLABEL.Text = "Введите ответ на секретный вопрос";
                    var data = SQL.SelectSecretTextAndWord(TEMP.Login);

                    TEMP.SecretText = data[0];
                    TEMP.SecretWord = data[1];
                    _dataTEXTBOX.Text = "";
                    _dataTEXTBOX.UseSystemPasswordChar = true;
                    _secretText.Text = TEMP.SecretText;
                    LocationMessageFromForm();
                    LocationMessageSecretText();
                    LocationPanel1();
                    _steep++;
                }
                else if (codeOperation == 1)
                {
                    Error.ClientError.NotFound();
                }
            }
            else if (_steep == 1)
            {
                if (_dataTEXTBOX.Text == TEMP.SecretWord)
                {
                    _messageFromFormLABEL.Text = "Введите новый пароль";
                    _secretText.Text = "";
                    _dataTEXTBOX.Text = "";
                    LocationMessageFromForm();
                    LocationMessageSecretText();
                    LocationPanel1();
                    _steep++;
                }
                else
                {
                    Error.ClientError.BadRequest();
                }
            }
            else if (_steep == 2)
            {
                // PassComplianceCheck(_passwordTEXTBOX.Text, _confirmationPasswordTEXTBOX.Text)
                if (!NEWUSER.CheckTheLengthOfPasswordAndAcceptPass(_dataTEXTBOX.Text))
                {
                    TEMP.NewPassword = _dataTEXTBOX.Text;
                    _messageFromFormLABEL.Text = "Введите подтверждение пароля";
                    _secretText.Text = "";
                    LocationMessageFromForm();
                    LocationMessageSecretText();
                    LocationPanel1();
                    _dataTEXTBOX.Text = "";
                    _steep++;
                }
                else
                {
                    Error.ClientError.BadRequest();
                }
            }
            else if (_steep == 3)
            {
                if (!NEWUSER.PassComplianceCheck(TEMP.NewPassword, _dataTEXTBOX.Text))
                {
                    int codeOperation = SQL.UpdatePassword(TEMP.Login, TEMP.NewPassword);
                    if (codeOperation > 0)
                    {
                        Warning.SaveDataUser();
                    }
                    else if (codeOperation == 0)
                    {
                        Error.ClientError.NotFound();
                    }
                }
                else
                {
                    Error.ClientError.BadRequest();
                }
            }
            _next.Enabled = true;
            _dataTEXTBOX.Enabled = true;
        }
    }
}
