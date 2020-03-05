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
using SecurityModule;
using SqlQueryProcessing;
using ErrorInPrograms;
using System.IO;

namespace Diploma2._0
{
    public partial class SettingChild : StyleWindows
    {
        Security SECURITY = new Security();
        Form1 MAINFORM = new Form1();
        Sql SQL = new Sql();
        ToolBox TOOLBOX = new ToolBox();

        private string _colorProgramTEMP = "#574B90";
        private int _heightMaxVolume = 0;
        private string _formatImage = "PNG";
        private Font _newFontUser = StyleWindows._mainFont;
        private string _fontFamily = StyleWindows._mainFont.Name;
        private string _styleFontFamily = StyleWindows._mainFont.Style.ToString();
        private int _fontSize = Convert.ToInt32(StyleWindows._mainFont.Size);
        private string hex;
        private byte[] _bytesAvatar;
        private byte[] _bytesBackImage;

        //
        //  Состояние CheckBox ToolBox
        //
        // Форма настройки личного аккаунта
        private bool tb_alertEndTime_combobox = false;
        private bool tb_hearingProblems_combobox = false;
        private bool tb_soundEffect_combobox = false;
        private bool tb_maxVolume_combobox = false;
        private bool tb_eyeProblems_combobox = false;
        // Форма настройки аккаунта ребенка
        private bool tb_setAccChild_block_combobox = false;
        private bool tb_setAccChild_alertEndTime_combobox = false;
        private bool tb_setAccChild_notificationTheExpirationOfTheTime_combobox = false;
        private bool tb_setAccChild_soundEffect_combobox = false;
        private bool tb_setAccChild_timeLimitPerDayL_combobox = false;
        private bool tb_setAccChild_eyeProblems_combobox = false;




        ToolTip _newAvatarTOOLTIM = new ToolTip();
        ToolTip _newBackImage = new ToolTip();
        ToolTip _newColorProgram = new ToolTip();
        ToolTip _newFont = new ToolTip();

        Timer _effectOpenHearingProblems = new Timer();
        private bool _openHearing = false;
        public SettingChild()
        {
            InitializeComponent();

            StyleWindows.SettingToolTip(_newAvatarTOOLTIM, _newAvatarPICTUREBOX, "Изменить аватар", "Загрузка новой картинки в качестве\nновой иконки пользователя");
            StyleWindows.SettingToolTip(_newBackImage, _newBackImagePICTUREBOX, "Изменение фона", "Загрузка новой картинки в качестве\nнового фона пользователя");
            StyleWindows.SettingToolTip(_newColorProgram, _colorProgramPICTUREBOX, "Изменение цвета программы", "Смена основного цвета приложения пользователя");
            StyleWindows.SettingToolTip(_newFont, _newFontPICTUREBOX, "Изменение шрифта", "Изменение стиля и размера шрифта пользователя");

            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 15, 4);


            this.TopMost = true;

            LoadDataUser();


            // Закругление краев checkbox
            ToolBox.SetRoundedShape(_boxCheckEyeProblemPANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckHearingProblemsPANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckMaxVolumePANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckSoundEffectPANEL, TOOLBOX.radius);

            //  Скрытие элементов
            _positionCheckedBoxSteepThreeFLP.Height = 0;
            _heightMaxVolume = _maxVolumePANEL.Height;
            _maxVolumePANEL.Height = 0;
            //
            //  Настройки шрифтов
            //
            _loginLABEL.Font = StyleWindows._mainFont;
            _passwordLABEL.Font = StyleWindows._mainFont;
            _inactionLABEL.Font = StyleWindows._mainFont;
            _secretTextLABEL.Font = StyleWindows._mainFont;
            _secretWordLABEL.Font = StyleWindows._mainFont;
            //
            //_colorProgramLABEL.Font = StyleWindows._mainFont;
            //
            _inactionLABEL.Font = StyleWindows._mainFont;
            _hearingProblemsLABEL.Font = StyleWindows._mainFont;
            _maxVolumeLABEL.Font = StyleWindows._mainFont;
            _eyeProblemLABEL.Font = StyleWindows._mainFont;
            _soundEffectsLABEL.Font = StyleWindows._mainFont;
            //
            //  Выравнивание элементов
            //
            int margin = 10;
            int marginLeft = 3;
            //  TextBox
            Size size = TextRenderer.MeasureText(_secretTextLABEL.Text, StyleWindows._mainFont);
            int startPositionTextBox = size.Width + margin;
            _loginTEXTBOX.Location = new Point(startPositionTextBox, margin);
            _passwordTEXTBOX.Location = new Point(startPositionTextBox, _loginTEXTBOX.Location.Y + _loginTEXTBOX.Height + margin);
            _secretTextTEXTBOX.Location = new Point(startPositionTextBox, _passwordTEXTBOX.Location.Y + _passwordTEXTBOX.Height + margin);
            _secretWordTEXTBOX.Location = new Point(startPositionTextBox, _secretTextTEXTBOX.Location.Y + _secretTextTEXTBOX.Height + margin);
            _incationCOMBOBOX.Location = new Point(startPositionTextBox, _secretWordTEXTBOX.Location.Y + _secretWordTEXTBOX.Height + margin);
            _checkedScrollHearingProblemsFLP.Location = new Point(marginLeft, _incationCOMBOBOX.Location.Y + _incationCOMBOBOX.Height + margin);
            // Label
            _loginLABEL.Location = new Point(marginLeft, _loginTEXTBOX.Location.Y);
            _passwordLABEL.Location = new Point(marginLeft, _passwordTEXTBOX.Location.Y);
            _inactionLABEL.Location = new Point(marginLeft, _incationCOMBOBOX.Location.Y);
            _secretTextLABEL.Location = new Point(marginLeft, _secretTextTEXTBOX.Location.Y);
            _secretWordLABEL.Location = new Point(marginLeft, _secretWordTEXTBOX.Location.Y);

            _viewPasswordPICTUREBOX.Location = new Point(_passwordTEXTBOX.Location.X + _passwordTEXTBOX.Width + 10, _passwordTEXTBOX.Location.Y + _passwordTEXTBOX.Height - _viewPasswordPICTUREBOX.Height);
            _viewSecretWordPICTUREBOX.Location = new Point(_secretWordTEXTBOX.Location.X + _secretWordTEXTBOX.Width + 10, _secretWordTEXTBOX.Location.Y + _secretWordTEXTBOX.Height - _viewSecretWordPICTUREBOX.Height);
            /*
            Size size = TextRenderer.MeasureText(_colorProgramLABEL.Text, StyleWindows._mainFont);
            _colorProgramTEXTBOX.Location = new Point(_colorProgramLABEL.Location.X + size.Width + 5, _colorProgramTEXTBOX.Location.Y);
            _loginTEXTBOX.Location = new Point(_colorProgramTEXTBOX.Location.X, _loginTEXTBOX.Location.Y);
            _fontFamilyCOMBOBOX.Location = new Point(_colorProgramTEXTBOX.Location.X, _fontFamilyCOMBOBOX.Location.Y);
            _incationCOMBOBOX.Location = new Point(_colorProgramTEXTBOX.Location.X, _incationCOMBOBOX.Location.Y);
            _fontSizeCOMBOBOX.Location = new Point(_colorProgramTEXTBOX.Location.X, _fontSizeCOMBOBOX.Location.Y);
            _unlockLogin.Location = new Point(_loginTEXTBOX.Location.X - _unlockLogin.Width - 5, _unlockLogin.Location.Y);
            */
        }

        public void LoadDataUser()
        {
            _loginTEXTBOX.Text = Form1._Login;
            _passwordTEXTBOX.Text = Form1._Password;
            _secretTextTEXTBOX.Text = Form1._SecretText;
            _secretWordTEXTBOX.Text = Form1._SecretWord;

            Color color = StyleWindows._mainColor;
            hex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Image image = Form1._AvatarUser;
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                _bytesAvatar = memoryStream.ToArray();
            }
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Image image = Form1._backGround;
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                _bytesBackImage = memoryStream.ToArray();
            }


            switch (Form1._Inaction)
            {
                case 0:
                    _incationCOMBOBOX.SelectedIndex = 0;
                    break;
                case 5:
                    _incationCOMBOBOX.SelectedIndex = 1;
                    break;
                case 10:
                    _incationCOMBOBOX.SelectedIndex = 2;
                    break;
                case 30:
                    _incationCOMBOBOX.SelectedIndex = 3;
                    break;
                case 60:
                    _incationCOMBOBOX.SelectedIndex = 4;
                    break;
                case 120:
                    _incationCOMBOBOX.SelectedIndex = 5;
                    break;
            }
            if (Form1._MaxVolume == 1 || Form1._SoundEffect == 1 || Form1._EyeProblems == 1)
            {
                _pointConditionCheckHearingProblemsPICTUREBOX_Click(null, null);
                if (Form1._SoundEffect == 1) _pointConditionCheckSoundEffectPICTUREBOX_Click(null, null);
                if (Form1._SoundEffect == 1 && Form1._MaxVolume == 1) _pointConditionCheckMaxVolumePICTUREBOX_Click(null, null);
                if (Form1._EyeProblems == 1) _pointConditionCheckEyeProblemPICTUREBOX_Click(null, null);
            }

            _avatarUserPICTUREBOX.BackgroundImage = Form1._AvatarUser;
        }

        private void SaveDataUser(string newLogin, bool newUser)
        {
            int innaction = 0;

            switch (_incationCOMBOBOX.SelectedIndex)
            {
                case 0: innaction = 0; break;
                case 1: innaction = 5; break;
                case 2: innaction = 10; break;
                case 3: innaction = 30; break;
                case 4: innaction = 60; break;
                case 5: innaction = 120; break;
                default: innaction = 0; break;
            }
            /*
            int codeOperation = SQL.SaveNewSettingUser(Form1._Login, newLogin, _fontFamilyCOMBOBOX.SelectedItem.ToString(), _fontSizeCOMBOBOX.SelectedItem.ToString(), _colorProgramTEXTBOX.Text,
                    innaction, Convert.ToInt32(_alertEndTimeCOMBOBOX.Checked), Convert.ToInt32(_maxVolumeEffectCHECKBOX.Checked), Convert.ToInt32(_soundEffectsCOMBOBOX.Checked), Convert.ToInt32(_problemsFromEageCOMBOBOX.Checked));
            
            
            if (codeOperation == 1)
            {
                Form1._FontFamily = _fontFamilyCOMBOBOX.SelectedItem.ToString();
                Form1._FontSize = Convert.ToInt32(_fontSizeCOMBOBOX.SelectedItem);
                Form1._ColorProgram = _colorProgramTEXTBOX.Text;
                Form1._Inaction = innaction;
                Form1._AlertExpirationTime = (_alertEndTimeCOMBOBOX.Checked) ? 1 : 0;
                if (newUser) Form1._Login = _loginTEXTBOX.Text;

                MessageBox.Show("Все данные были успешно обновлены", "Операция прошла успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (codeOperation == 0)
                {
                    MessageBox.Show("Во время внесения изменений возникла ошибка", "Операция завершилась ошибкой", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */
        }
        private void _saveDataBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            panel1.Enabled = false;
            if (_loginTEXTBOX.Enabled)
            {
                if (SQL.CheckForUniqueLogin(_loginTEXTBOX.Text) == 1)
                {
                    SaveDataUser(_loginTEXTBOX.Text, true);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                }
            }
            else
            {
                SaveDataUser(Form1._Login, false);
            }


            panel1.Enabled = true;
        }



        #region Отрисовка CheckBox
        //
        //  Нстройка личного аккаунта
        //

        #region HearingProblems Combobox
        private void _checkedScrollHearingProblemsFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_hearingProblems_combobox, _boxCheckHearingProblemsPANEL, sender, e);

        private void _pointConditionCheckHearingProblemsPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_hearingProblems_combobox);

        private void _pointConditionCheckHearingProblemsPICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_hearingProblems_combobox = ToolBox.PointCondition(tb_hearingProblems_combobox, _pointConditionCheckHearingProblemsPICTUREBOX, _hearingProblemsTIMER);
            _checkedScrollHearingProblemsFLP.Refresh();
            _animationScrollTIMER.Enabled = true;
        }

        private void _hearingProblemsTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_hearingProblems_combobox, _pointConditionCheckHearingProblemsPICTUREBOX, _conditionCheckHearingProblemsLABEL, _boxCheckHearingProblemsPANEL, _hearingProblemsTIMER);

        //
        //  Появление панели специальных возможностей
        //
        private void _animationScrollTIMER_Tick(object sender, EventArgs e)
        {
            ToolBox.AppearanceOfSubItems(tb_hearingProblems_combobox, _positionCheckedBoxSteepThreeFLP, _checkedScrollEyeProblemFLP.Location.Y + _checkedScrollEyeProblemFLP.Height + 5, _animationScrollTIMER);
            if (!_animationScrollTIMER.Enabled) _positionCheckedBoxSteepThreeFLP.AutoScroll = true;
        }
        #endregion

        #region SoundEffect Combobox
        private void _checkedScrollSoundEffectFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_soundEffect_combobox, _boxCheckSoundEffectPANEL, sender, e);

        private void _pointConditionCheckSoundEffectPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_soundEffect_combobox);

        private void _pointConditionCheckSoundEffectPICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_soundEffect_combobox = ToolBox.PointCondition(tb_soundEffect_combobox, _pointConditionCheckSoundEffectPICTUREBOX, _soundEffectTIMER);
            _checkedScrollSoundEffectFLP.Refresh();
            _animationScrollSoundTIMER.Enabled = true;
        }

        private void _soundEffectTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_soundEffect_combobox, _pointConditionCheckSoundEffectPICTUREBOX, _conditionCheckSoundEffectLABEL, _boxCheckSoundEffectPANEL, _soundEffectTIMER);
        //
        //  Появление панели о максимальном звуке оповещении
        //
        private void _animationScrollSoundTIMER_Tick(object sender, EventArgs e)
        {


            ToolBox.AppearanceOfSubItems(tb_soundEffect_combobox, _maxVolumePANEL, _heightMaxVolume, _animationScrollSoundTIMER);
            // _maxVolumePANEL
            if (_animationScrollSoundTIMER.Enabled)
            {
                _maxVolumePANEL.Width = _checkedScroMaxVolumeFLP.Width;
                _maxVolumePANEL.AutoSize = false;
            }
            else
            {
                
                if(!_animationScrollSoundTIMER.Enabled && _maxVolumePANEL.Height == 0)
                    _maxVolumePANEL.AutoSize = false;
                else
                _maxVolumePANEL.AutoSize = true;
            }
            /*
            if (!_animationScrollSoundTIMER.Enabled && _maxVolumePANEL.Height != 0)
            {
                _maxVolumePANEL.AutoSize = true;
            }
            else if (_animationScrollSoundTIMER.Enabled)
            {
                _maxVolumePANEL.AutoSize = false;
            }*/



        }
        #endregion

        #region MaxVolume
        private void _checkedScroMaxVolumeFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_maxVolume_combobox, _boxCheckMaxVolumePANEL, sender, e);

        private void _pointConditionCheckMaxVolumePICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_maxVolume_combobox);

        private void _pointConditionCheckMaxVolumePICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_maxVolume_combobox = ToolBox.PointCondition(tb_maxVolume_combobox, _pointConditionCheckMaxVolumePICTUREBOX, _maxVolumeTIMER);
            _checkedScroMaxVolumeFLP.Refresh();
        }

        private void _maxVolumeTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_maxVolume_combobox, _pointConditionCheckMaxVolumePICTUREBOX, _conditionCheckMaxVolumeLABEL, _boxCheckMaxVolumePANEL, _maxVolumeTIMER);
        #endregion

        #region EyeProblem CheckBox
        private void _checkedScrollEyeProblemFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_eyeProblems_combobox, _boxCheckEyeProblemPANEL, sender, e);

        private void _pointConditionCheckEyeProblemPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_eyeProblems_combobox);

        private void _pointConditionCheckEyeProblemPICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_eyeProblems_combobox = ToolBox.PointCondition(tb_eyeProblems_combobox, _pointConditionCheckEyeProblemPICTUREBOX, _eyeProblemsTIMER);
            _checkedScrollEyeProblemFLP.Refresh();
        }

        private void _eyeProblemsTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_eyeProblems_combobox, _pointConditionCheckEyeProblemPICTUREBOX, _conditionCheckEyeProblemLABEL, _boxCheckEyeProblemPANEL, _eyeProblemsTIMER);
        #endregion

        #endregion


        private void _backDataUserPANEL_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen p = new Pen(_mainColor, 3.0f);
            // Нижняя линия логина
            x = _loginTEXTBOX.Location.X;
            y = _loginTEXTBOX.Location.Y;
            width = _loginTEXTBOX.Width;
            height = _loginTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // Нижняя линия пароля
            x = _passwordTEXTBOX.Location.X;
            y = _passwordTEXTBOX.Location.Y;
            width = _passwordTEXTBOX.Width;
            height = _passwordTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // Нижняя линия секретного вопроса
            x = _secretTextTEXTBOX.Location.X;
            y = _secretTextTEXTBOX.Location.Y;
            width = _secretTextTEXTBOX.Width;
            height = _secretTextTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);
            // Нижняя линия ответа на секретный вопрос
            x = _secretWordTEXTBOX.Location.X;
            y = _secretWordTEXTBOX.Location.Y;
            width = _secretWordTEXTBOX.Width;
            height = _secretWordTEXTBOX.Height;
            gr.DrawLine(p, x - 2, y + height, x + width + 2, y + height);

            // Бездействие
            x = _incationCOMBOBOX.Location.X;
            y = _incationCOMBOBOX.Location.Y;
            width = _incationCOMBOBOX.Width;
            height = _incationCOMBOBOX.Height;
            gr.DrawRectangle(p, x - 1, y - 1, width + 1, height + 1);
        }

        private void _viewPasswordPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ToolBox.DrawEye(sender, e, _viewPasswordPICTUREBOX.Width, _viewPasswordPICTUREBOX.Height, graphics);
        }

        private void _viewSecretWordPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ToolBox.DrawEye(sender, e, _viewSecretWordPICTUREBOX.Width, _viewSecretWordPICTUREBOX.Height, graphics);
        }

        private void _viewPasswordPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _passwordTEXTBOX.UseSystemPasswordChar = (_passwordTEXTBOX.UseSystemPasswordChar) ? false : true;
        }

        private void _viewSecretWordPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _secretWordTEXTBOX.UseSystemPasswordChar = (_secretWordTEXTBOX.UseSystemPasswordChar) ? false : true;
        }

        private void _newAvatarPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Сохранение картинки
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    try
                    {
                        Image image = Image.FromFile(openFileDialog1.FileName);
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        _bytesAvatar = memoryStream.ToArray();

                        // Чтение картинки

                        MemoryStream stream = new MemoryStream();
                        foreach (byte byteImg in _bytesAvatar) stream.WriteByte(byteImg);

                        _avatarUserPICTUREBOX.BackgroundImage = Image.FromStream(stream);
                    }
                    catch
                    {
                        Error.ClientError.BadRequest();
                    }
                }
            }
        }

        private void _saveData_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            this.Enabled = false;
            if (Form1._Login != _loginTEXTBOX.Text)
            {
                int codeOperation = SQL.CheckingLoginForUniqueness(_loginTEXTBOX.Text);
                if (codeOperation == 1)
                {
                    InputNewDataUser();

                    StyleWindows._mainColor = ColorTranslator.FromHtml(hex);
                    this.Refresh();
                    this.Invalidate();
                }
                else if (codeOperation == 0)
                {
                    Error.ClientError.UserExits();
                }
            }
            else
            {
                InputNewDataUser();
            }

            this.Enabled = true;
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                byte[] pData = blob;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                Bitmap bm = new Bitmap(mStream, false);
                mStream.Dispose();
                return bm;
            }
        }

        private void InputNewDataUser()
        {
            if (Security.CheckCodeOperation(SQL.SaveDataUser(Form1._Login, _loginTEXTBOX.Text, _passwordTEXTBOX.Text, _secretTextTEXTBOX.Text, _secretWordTEXTBOX.Text, _fontFamily, _fontSize, _styleFontFamily, hex,
    _incationCOMBOBOX.SelectedIndex, (tb_soundEffect_combobox) ? 1 : 0, (tb_maxVolume_combobox && tb_soundEffect_combobox) ? 1 : 0, (tb_eyeProblems_combobox) ? 1 : 0, _bytesAvatar, _formatImage,
_bytesBackImage, _formatImage, false)))
            {
                Warning.SaveDataUser();
            }
            var imgAvatar = SQL.image(_loginTEXTBOX.Text);
            Form1 FORM1 = new Form1();
            FORM1._logogUser.BackgroundImage = imgAvatar;
            this.Refresh();
        }

        private void _newFontPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _newFontUser = fontDialog1.Font;
                _fontFamily = fontDialog1.Font.Name;
                _fontSize = Convert.ToInt32(fontDialog1.Font.Size);
                _styleFontFamily = fontDialog1.Font.Style.ToString();
            }
        }

        private void _colorProgramPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                hex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            }
        }

        private void _newBackImagePICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //_avatarUserPICTUREBOX
                // Сохранение картинки
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    try
                    {
                        Image image = Image.FromFile(openFileDialog1.FileName);
                        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        _bytesBackImage = memoryStream.ToArray();
                    }
                    catch
                    {
                        Error.ClientError.BadRequest();
                    }
                    /*
                    // Чтение картинки

                    MemoryStream stream = new MemoryStream();
                    foreach (byte byteImg in _bytesAvatar) stream.WriteByte(byteImg);

                    _avatarUserPICTUREBOX.BackgroundImage = Image.FromStream(stream);*/
                }
            }
        }

        private void SettingChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Information info = new Information();
                info.ShowDialog();
            }
        }
    }
}
