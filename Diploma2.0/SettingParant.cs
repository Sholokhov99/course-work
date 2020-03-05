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
using SecurityModule;
using SqlQueryProcessing;
using ErrorInPrograms;
using System.IO;

namespace Diploma2._0
{
    public partial class SettingParant : StyleWindows
    {
        Sql SQL = new Sql();
        StyleWindows STYLEWINDOWS = new StyleWindows();
        Security SECURITY = new Security();
        ToolBox TOOLBOX = new ToolBox();
        SettingChild SETTINGCHILD = new SettingChild();

        Timer _effectOpenHearingProblems = new Timer();
        private bool _openHearing = false;
        private int radius = 20;
        private string hex = "#" + StyleWindows._mainColor.R.ToString("X2") + StyleWindows._mainColor.G.ToString("X2") + StyleWindows._mainColor.B.ToString("X2");

        private List<string> _date = new List<string>();                                                        // Общий заблокированный список по дням недели
        private List<Control> _elementsDate = new List<Control>();                                              // Данные созданных кнопок и временной шкалы
        private Color _borderColor = Color.FromArgb(59, 46, 117);                                               // Цвет обводки 
        private Color _mouseOverBackColor = Color.FromArgb(111, 101, 158);                                      // Цвет изменения цвета при наведении мышкой на кнопку

        // Переменные
        private List<string> _mondayHold = new List<string>();                                                  // Понедельник
        private List<string> _tuesdayHold = new List<string>();                                                 // Вторник
        private List<string> _environsHold = new List<string>();                                                // Среда
        private List<string> _thursdayHold = new List<string>();                                                // Четверг
        private List<string> _fridayHold = new List<string>();                                                  // Пятница
        private List<string> _saturdayHold = new List<string>();                                                // Суббота
        private List<string> _sundayHold = new List<string>();                                                  // Воскресенье

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


        private string _colorProgramTEMP = "#574B90";
        private int _heightMaxVolume = 0;
        private string _formatImage = "PNG";
        private Font _newFontUser = StyleWindows._mainFont;
        private string _fontFamily = StyleWindows._mainFont.Name;
        private string _styleFontFamily = StyleWindows._mainFont.Style.ToString();
        private int _fontSize = Convert.ToInt32(StyleWindows._mainFont.Size);
        private byte[] _bytesAvatar;
        private byte[] _bytesBackImage;

        private int _numberUser = -1;

        public SettingParant()
        {
            InitializeComponent();
            _namePageLABEL.Text = "Настройки";
            Form1.NewStyleButton(_newUser);
            tabControl1.Font = StyleWindows._mainFont;
            // Закругление краев checkbox
            ToolBox.SetRoundedShape(_boxCheckEyeProblemPANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckHearingProblemsPANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckMaxVolumePANEL, TOOLBOX.radius);
            ToolBox.SetRoundedShape(_boxCheckSoundEffectPANEL, TOOLBOX.radius);

            //  Скрытие элементов
            _positionCheckedBoxSteepThreeFLP.Height = 0;
            _heightMaxVolume = _maxVolumePANEL.Height;
            _maxVolumePANEL.Height = 0;
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




            //
            //
            //
            _loginUserLABEL.Font = StyleWindows._mainFont;
            _passwordUserLABEL.Font = StyleWindows._mainFont;
            _nameUserLABEL.Font = StyleWindows._mainFont;
            _fineNameUserLOGIN.Font = StyleWindows._mainFont;
            _BloclAccountLABEL.Font = StyleWindows._mainFont;
            _notificationTheExpirationOfTheTimeLABEL.Font = StyleWindows._mainFont;
            _incationLABEL.Font = StyleWindows._mainFont;
            _timeLimitPerDayLABEL.Font = StyleWindows._mainFont;
            _timePerDayLABEL.Font = StyleWindows._mainFont;
            _hoursTEXTBOX.Font = StyleWindows._mainFont;
            _shortHourLABEL.Font = StyleWindows._mainFont;
            _minutesTEXTBOX.Font = StyleWindows._mainFont;
            _shortMinuteTEXTBOX.Font = StyleWindows._mainFont;

            _fineNameChildTEXTBOX.Location = new Point(_fineNameUserLOGIN.Width + _fineNameUserLOGIN.Location.X + 10, _fineNameChildTEXTBOX.Location.Y);
            _loginChildTEXTBOX.Location = new Point(_fineNameChildTEXTBOX.Location.X, _loginChildTEXTBOX.Location.Y);
            _passwordChildTEXTBOX.Location = new Point(_fineNameChildTEXTBOX.Location.X, _passwordChildTEXTBOX.Location.Y);
            _nameChildTEXTBOX.Location = new Point(_fineNameChildTEXTBOX.Location.X, _nameChildTEXTBOX.Location.Y);
            _fineNameChildTEXTBOX.Location = new Point(_fineNameChildTEXTBOX.Location.X, _fineNameChildTEXTBOX.Location.Y);
            _inactionChildComboBox.Location = new Point(_incationLABEL.Location.X + _incationLABEL.Width + 10, _inactionChildComboBox.Location.Y);
            _viewPasswordChildPICTUREBOX.Location = new Point(_passwordChildTEXTBOX.Location.X + _passwordChildTEXTBOX.Width + 10, _viewPasswordChildPICTUREBOX.Location.Y);



            _backDataUserPANEL.Height = _positionCheckedBoxSteepThreeFLP.Height;


            //_newUser
            _newUser.Font = StyleWindows._mainFont;
            size = TextRenderer.MeasureText(_newUser.Text, StyleWindows._mainFont);
            int widthButton = 200 - 17;
            if (size.Width > widthButton)
            {
                int i = size.Width / widthButton + 2;

                this._newUser.Size = new System.Drawing.Size(widthButton, size.Height * i);
            }
            else
            {
                this._newUser.Size = new System.Drawing.Size(widthButton, 40);
            }
            _usersLISTBOX.Height = panel2.Height - _newUser.Height - 10;
            //
            //  Получение данных пользователя
            //
            _usersLISTBOX.Font = StyleWindows._mainFont;
            var data = SQL.ListChild();
            _usersLISTBOX.Items.AddRange(data.ToArray());

            _contentFourSteepFLP.Location = new Point(_lineContentPANEL.Width / 2 - _contentFourSteepFLP.Width / 2, _checkedScrollTimeLimitPerDayFLP.Location.Y + _checkedScrollTimeLimitPerDayFLP.Height + 10);

            CreateTimeTable();

        }

        private void SettingParant_Load(object sender, EventArgs e)
        {
            //  Загрузка данных родителя
            LoadDataParance();
            // Закругление краев checkbox
            ToolBox.SetRoundedShape(_boxCheckEyeProblemPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckHearingProblemsPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckMaxVolumePANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckSoundEffectPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckBloclAccountPANEL, radius);
            ToolBox.SetRoundedShape(_boxChecknotificationTheExpirationOfTheTimePANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckTimeLimitPerDayPANEL, radius);
            //  Скрытие элементов
            _positionCheckedBoxSteepThreeFLP.Height = 0;
            //  Настройка таймера
            _effectOpenHearingProblems.Interval = 1;
            _effectOpenHearingProblems.Tick += _effectOpenHearingProblems_Tick;
            //  Перемещение кнопки закрытия окна
            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 15, 4);
        }

        private void LoadDataParance()
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

            _incationCOMBOBOX.SelectedIndex = SwitchInaction(Form1._Inaction);

            if (Form1._MaxVolume == 1 || Form1._SoundEffect == 1 || Form1._EyeProblems == 1)
            {
                _pointConditionCheckHearingProblemsPICTUREBOX_Click(null, null);
                if (Form1._SoundEffect == 1) _pointConditionCheckSoundEffectPICTUREBOX_Click(null, null);
                if (Form1._SoundEffect == 1 && Form1._MaxVolume == 1) _pointConditionCheckMaxVolumePICTUREBOX_Click(null, null);
                if (Form1._EyeProblems == 1) _pointConditionCheckEyeProblemPICTUREBOX_Click(null, null);
            }

            _avatarUserPICTUREBOX.BackgroundImage = Form1._AvatarUser;

        }

        private int SwitchInaction(int value)
        {
            switch (value)
            {
                case 0:
                    return 0;
                case 5:
                    return 1;
                case 10:
                    return 2;
                case 30:
                    return 3;
                case 60:
                    return 4;
                case 120:
                    return 5;
                default:
                    return -1;
            }
        }

        private void _effectOpenHearingProblems_Tick(object sender, EventArgs e)
        {

        }

        private void _lineContentPANEL_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen pen = new Pen(StyleWindows._mainColor, 3.0f);
            //
            //  _loginChildTEXTBOX
            //
            x = _loginChildTEXTBOX.Location.X;
            y = _loginChildTEXTBOX.Location.Y;
            width = _loginChildTEXTBOX.Width;
            height = _loginChildTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
            //
            //  _passwordChildTEXTBOX
            //
            x = _passwordChildTEXTBOX.Location.X;
            y = _passwordChildTEXTBOX.Location.Y;
            width = _passwordChildTEXTBOX.Width;
            height = _passwordChildTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
            //
            //  _nameChildTEXTBOX
            //
            x = _nameChildTEXTBOX.Location.X;
            y = _nameChildTEXTBOX.Location.Y;
            width = _nameChildTEXTBOX.Width;
            height = _nameChildTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
            //
            //  _fineNameChildTEXTBOX
            //
            x = _fineNameChildTEXTBOX.Location.X;
            y = _fineNameChildTEXTBOX.Location.Y;
            width = _fineNameChildTEXTBOX.Width;
            height = _fineNameChildTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen pen = new Pen(StyleWindows._mainColor, 3.0f);
            //
            //  _hoursTEXTBOX
            //
            x = _hoursTEXTBOX.Location.X;
            y = _hoursTEXTBOX.Location.Y;
            width = _hoursTEXTBOX.Width;
            height = _hoursTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
            //
            //  _minutesTEXTBOX
            //
            x = _minutesTEXTBOX.Location.X;
            y = _minutesTEXTBOX.Location.Y;
            width = _minutesTEXTBOX.Width;
            height = _minutesTEXTBOX.Height;
            gr.DrawLine(pen, x - 2, y + height, x + width + 2, y + height);
        }

        private void _viewPasswordChildPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_passwordChildTEXTBOX.UseSystemPasswordChar)
                _passwordChildTEXTBOX.UseSystemPasswordChar = false;
            else
                _passwordChildTEXTBOX.UseSystemPasswordChar = true;
        }

        private void _newUser_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            NewUser newUser = new NewUser();
            newUser.ShowDialog();
        }

        private void _viewPasswordChildPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            Graphics graphics = e.Graphics;
            ToolBox.DrawEye(sender, e, _viewPasswordChildPICTUREBOX.Width, _viewPasswordChildPICTUREBOX.Height, graphics);
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
            _backDataUserPANEL.Height = _positionCheckedBoxSteepThreeFLP.Height + 5;
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

        #endregion

        #region MaxVolume
        private void _checkedScroMaxVolumeFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_maxVolume_combobox, _boxCheckMaxVolumePANEL, sender, e);

        private void _pointConditionCheckMaxVolumePICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_maxVolume_combobox);

        private void _pointConditionCheckMaxVolumePICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_maxVolume_combobox = ToolBox.PointCondition(tb_maxVolume_combobox, _pointConditionCheckMaxVolumePICTUREBOX, _maxVolumeTIMER);
            _checkedScroMaxVolumeFLP.Refresh();
        }
        #endregion

        #region EyeProblem CheckBox
        private void _checkedScrollEyeProblemFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_eyeProblems_combobox, _boxCheckEyeProblemPANEL, sender, e);

        private void _pointConditionCheckEyeProblemPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_eyeProblems_combobox);

        private void _pointConditionCheckEyeProblemPICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_eyeProblems_combobox = ToolBox.PointCondition(tb_eyeProblems_combobox, _pointConditionCheckEyeProblemPICTUREBOX, _eyeProblemsTIMER);
            _checkedScrollEyeProblemFLP.Refresh();
        }

        #endregion

        //
        //  Форма настройки аккаунта детей
        //

        #region BlockAccount CheckBox
        private void _pointConditionCheckBloclAccountPICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_setAccChild_block_combobox = ToolBox.PointCondition(tb_setAccChild_block_combobox, _pointConditionCheckBloclAccountPICTUREBOX, setingAccChild_BlockTIMER);
            _checkedScrollBlockAccountFLP.Refresh();
        }

        private void setingAccChild_BlockTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_setAccChild_block_combobox, _pointConditionCheckBloclAccountPICTUREBOX, _conditionCheckBloclAccountLABEL, _boxCheckBloclAccountPANEL, setingAccChild_BlockTIMER);

        private void _checkedScrollBlockAccountFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_setAccChild_block_combobox, _boxCheckBloclAccountPANEL, sender, e);

        private void _pointConditionCheckBloclAccountPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_setAccChild_block_combobox);
        #endregion

        #region NotificationExpirationTime CheckBox
        private void _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Click(object sender, EventArgs e)
        {
            tb_setAccChild_notificationTheExpirationOfTheTime_combobox = ToolBox.PointCondition(tb_setAccChild_notificationTheExpirationOfTheTime_combobox, _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX, settingAccChildNotificationExpirationYIMER);
            _checkedScrollnotificationTheExpirationOfTheTimeFLP.Refresh();
        }

        private void settingAccChildNotificationExpirationYIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_setAccChild_notificationTheExpirationOfTheTime_combobox, _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX, _conditionChecknotificationTheExpirationOfTheTimeLABEL, _boxChecknotificationTheExpirationOfTheTimePANEL, settingAccChildNotificationExpirationYIMER);

        private void _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_setAccChild_notificationTheExpirationOfTheTime_combobox);

        private void _checkedScrollnotificationTheExpirationOfTheTimeFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_setAccChild_notificationTheExpirationOfTheTime_combobox, _boxChecknotificationTheExpirationOfTheTimePANEL, sender, e);

        #endregion

        #region TimeLimitPerDay
        private void _pointConditionCheckTimeLimitPerDayPICTUREBOX_Click(object sender, EventArgs e)
        {
            
            tb_setAccChild_timeLimitPerDayL_combobox = ToolBox.PointCondition(tb_setAccChild_timeLimitPerDayL_combobox, _pointConditionCheckTimeLimitPerDayPICTUREBOX, settingAccChildLimitTimeUserTIMER);
            _checkedScrollTimeLimitPerDayFLP.Refresh();

            LimitForTheDayFLP.Visible = (tb_setAccChild_timeLimitPerDayL_combobox) ? false : true;
            _contentFourSteepFLP.Visible = (!tb_setAccChild_timeLimitPerDayL_combobox) ? false : true;
            
            /*
            LimitForTheDayFLP.Visible = (tb_setAccChild_timeLimitPerDayL_combobox) ? false : true;
            _contentFourSteepFLP.Visible = (tb_setAccChild_timeLimitPerDayL_combobox) ? true : false;
            */

        }




        private void CreateTimeTable()
        {
            int day = 0; // Определение дня недели
            //
            //  Подсчет созданных кнопок в разные дни недели
            //
            int mondayTrack = 0, tuesdayTrack = 0,
                environsTrack = 0, thursdayTrack = 0,
                fridaytrack = 0, saturdayTrack = 0,
                sundayTrack = 0;
            // 
            // Настройка внешенего вида приложения
            // 
            for (int track = 0; track < 168; track++)
            {
                Button button = new Button();
                button.BackColor = _mainColor;
                button.Cursor = Cursors.Hand;
                button.FlatAppearance.BorderColor = _borderColor;
                button.FlatAppearance.BorderSize = 2;
                button.FlatAppearance.MouseOverBackColor = _mouseOverBackColor;
                button.FlatStyle = FlatStyle.Flat;
                button.Location = new Point(0, 0);
                button.Margin = new Padding(1, 1, 2, 1);
                button.Size = new Size(23, 19);
                button.TabIndex = 33;
                button.UseVisualStyleBackColor = false;
                button.Click += (s, e) =>
                {
                    
                    Button but = (Button)s;
                    string name = button.Name;
                    //MessageBox.Show(button.Name);
                    if (button.BackColor == _mainColor)
                    {
                        button.BackColor = Color.OrangeRed;
                        DefinitionOfTime(button.Name, false);

                    }
                    else
                    {
                        button.BackColor = _mainColor;
                        DefinitionOfTime(button.Name, true);
                    }
                    

                    /*
                    Button button = (Button)sender;
                    _contentStepsPANEL.Controls.Clear();
                    this.Controls.Remove(_panelStagePANEL);
                    this.Controls.Remove(_contentStepsPANEL);
                    StartSettings();
                    this.Refresh();
                    _contentStepsPANEL.Refresh();
                    ClearTempRegistration();*/
                };
                
                //
                //  Задание индивидуального имени кнопке
                //
                switch (day)
                {
                    // ПН
                    case 0:
                        button.Name = "monday_" + mondayTrack;
                        mondayTrack++;
                        break;
                    // ВТ
                    case 1:
                        button.Name = "tuesday_" + tuesdayTrack;
                        tuesdayTrack++;
                        break;
                    // СР
                    case 2:
                        button.Name = "environs_" + environsTrack;
                        environsTrack++;
                        break;
                    // ЧТ
                    case 3:
                        button.Name = "thursday_" + thursdayTrack;
                        thursdayTrack++;
                        break;
                    // ПТ
                    case 4:
                        button.Name = "friday_" + fridaytrack;
                        fridaytrack++;
                        break;
                    // СБ
                    case 5:
                        button.Name = "saturday_" + saturdayTrack;
                        saturdayTrack++;
                        break;
                    // ВС
                    case 6:
                        button.Name = "sunday_" + sundayTrack;
                        sundayTrack++;
                        day = -1;   // Обнуление значения
                        break;
                }
                day++;
                //
                // Добавление кнопки в память программы
                //
                _elementsDate.Add(button);
                _timeTableFLOWLAYOUTPANEL.Controls.Add(button);
            }// Создание кнопок
            //
            //  Создание временной шкалы
            //
            for (int track = 0; track < 25; track++)
            {
                Label label = new Label();
                label.ForeColor = Color.Black;
                label.Font = new Font("Calibri", 12.0F, FontStyle.Regular, GraphicsUnit.Point);
                label.AutoSize = true;
                label.BackColor = Color.Transparent;
                label.Margin = new Padding(1, 1, 1, 1);
                label.Name = "time_" + track;
                //
                //  Проверка на правильную запись времени
                //
                if (track < 10) label.Text = "0" + track + ":00";
                else
                    if (track == 24) label.Text = "00:00";
                else
                    label.Text = track + ":00";
                //
                //  Добавление строчки в память программы
                //
                _elementsDate.Add(label);
                _timeLabelFLOWLAYOUTPANEL.Controls.Add(label);
            }

            //
            //  Добавление на форму дней недели
            //  
            for (int track = 0; track < 7; track++)
            {
                Label labelWeekday = new Label();
                labelWeekday.AutoSize = true;
                labelWeekday.ForeColor = Color.Black;
                labelWeekday.Font = new Font("Calibri", 12.0F, FontStyle.Regular, GraphicsUnit.Point); ;
                labelWeekday.Size = new Size(29, 19);
                labelWeekday.BackColor = Color.Transparent;
                labelWeekday.Margin = new Padding(0, 0, 0, 0);
                labelWeekday.Name = "weekday_" + track;
                //
                //  Задание имени дню недели
                //
                switch (track)
                {
                    case 0:
                        labelWeekday.Text = "ПН";
                        break;
                    case 1:
                        labelWeekday.Text = "ВТ";
                        break;
                    case 2:
                        labelWeekday.Text = "СР";
                        break;
                    case 3:
                        labelWeekday.Text = "ЧТ";
                        break;
                    case 4:
                        labelWeekday.Text = "ПТ";
                        break;
                    case 5:
                        labelWeekday.Text = "СБ";
                        break;
                    case 6:
                        labelWeekday.Text = "ВС";
                        break;
                }
                //
                //  Добавление строчки в память программы
                //
                _elementsDate.Add(labelWeekday);
                _weekdayLabelFLOWLAYOUTPANEL.Controls.Add(labelWeekday);
            }

        }

        private void DefinitionOfTime(string value, bool isDelete)
        {
            string numberButton = "";
            string weekday = "";
            bool isNumber = false;
            //
            // Определение номера нажатой кнокпи и дня недели
            //
            foreach (char letter in value)
            {
                if (letter == '_') isNumber = true;
                else
                    if (!isNumber) weekday += letter;
                else
                    numberButton += letter;

            }
            //
            // Определение времени - когда разрешено пользоваться пк
            //

            switch (numberButton.Length)
            {
                case 1:
                    numberButton = "0" + numberButton + ":00-0" + (Convert.ToInt32(numberButton) + 1).ToString() + ":00";
                    break;

                case 2:
                    if (Convert.ToInt32(numberButton) + 1 == 24) numberButton += ":00-00:00";
                    else
                        numberButton += ":00-" + (Convert.ToInt32(numberButton) + 1).ToString() + ":00";
                    break;
            }
            //
            // Занесение данных в массив
            //
            DeterminationOfTheDayOfTheWeek(weekday, numberButton, isDelete);
        }
        private void DeterminationOfTheDayOfTheWeek(string weekday, string numberButton, bool isDelete)
        {
            switch (weekday)
            {
                // Понедельник
                case "monday":
                    if (!isDelete)
                    {
                        // Добавление элементов
                        _mondayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _mondayHold.Count; index += 2)
                        {
                            if (_mondayHold[index] == numberButton)
                            {
                                _mondayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Вторник
                case "tuesday":
                    if (!isDelete)
                    {
                        // Добавление элементов
                        _tuesdayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _tuesdayHold.Count; index += 2)
                        {
                            if (_tuesdayHold[index] == numberButton)
                            {
                                _tuesdayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Среда
                case "environs":
                    if (!isDelete)
                    {
                        // Добавление элементов
                        _environsHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _environsHold.Count; index += 2)
                        {
                            if (_environsHold[index] == numberButton)
                            {
                                _environsHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Четверг
                case "thursday":
                    if (!isDelete)
                    {
                        _thursdayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _thursdayHold.Count; index += 2)
                        {
                            if (_thursdayHold[index] == numberButton)
                            {
                                _thursdayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Пятница
                case "friday":
                    if (!isDelete)
                    {
                        _fridayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _fridayHold.Count; index += 2)
                        {
                            if (_fridayHold[index] == numberButton)
                            {
                                _fridayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Суббота
                case "saturday":
                    if (!isDelete)
                    {
                        _saturdayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _saturdayHold.Count; index += 2)
                        {
                            if (_saturdayHold[index] == numberButton)
                            {
                                _saturdayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
                // Воскресенье
                case "sunday":
                    if (!isDelete)
                    {
                        _sundayHold.Add(numberButton);
                    }
                    else
                    {
                        // Удаление элементов
                        for (int index = 0; index < _sundayHold.Count; index += 2)
                        {
                            if (_sundayHold[index] == numberButton)
                            {
                                _sundayHold.RemoveAt(index);    // Удаление ячейки с временем
                            }
                        }
                    }
                    break;
            }
        }

        private void settingAccChildLimitTimeUserTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_setAccChild_timeLimitPerDayL_combobox, _pointConditionCheckTimeLimitPerDayPICTUREBOX, _conditionCheckTimeLimitPerDayLABEL, _boxCheckTimeLimitPerDayPANEL, settingAccChildLimitTimeUserTIMER);

        private void settingAccViewLimitTimeTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.AppearanceOfSubItems(tb_setAccChild_timeLimitPerDayL_combobox, _contentFourSteepFLP, 800, settingAccViewLimitTimeTIMER);

        private void _pointConditionCheckTimeLimitPerDayPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, tb_setAccChild_timeLimitPerDayL_combobox);

        private void _checkedScrollTimeLimitPerDayFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(tb_setAccChild_timeLimitPerDayL_combobox, _boxCheckTimeLimitPerDayPANEL, sender, e);
        #endregion

        #endregion

        private void _saveDataBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            this.Enabled = false;

            if (Form1._Login != _loginTEXTBOX.Text)
            {
                int codeOperation = SQL.CheckingLoginForUniqueness(_loginTEXTBOX.Text);
                if (codeOperation == 1)
                {
                    InputNewDataUser();
                }
                else if(codeOperation == 0)
                {
                    Error.ClientError.UserExits();
                }
            }

            this.Enabled = true;
        }
        private void InputNewDataUser()
        {
            if (Security.CheckCodeOperation(SQL.SaveDataUser(Form1._Login, _loginTEXTBOX.Text, _passwordTEXTBOX.Text, _secretTextTEXTBOX.Text, _secretWordTEXTBOX.Text, _fontFamily, _fontSize, _styleFontFamily,
                hex, _incationCOMBOBOX.SelectedIndex, (tb_soundEffect_combobox) ? 1 : 0, (tb_maxVolume_combobox && tb_soundEffect_combobox) ? 1 : 0, (tb_eyeProblems_combobox) ? 1 : 0,
                _bytesAvatar, _formatImage, _bytesBackImage, _formatImage, true)))
            {
                Warning.SaveDataUser();
            }




            /*
            if (Security.CheckCodeOperation(SQL.SaveDataParace(Form1._Login, _loginTEXTBOX.Text, _passwordTEXTBOX.Text, _secretTextTEXTBOX.Text, _secretWordTEXTBOX.Text, _fontFamily, _fontSize, _styleFontFamily, hex,
    _incationCOMBOBOX.SelectedIndex, (tb_soundEffect_combobox) ? 1 : 0, (tb_maxVolume_combobox && tb_soundEffect_combobox) ? 1 : 0, (tb_eyeProblems_combobox) ? 1 : 0, _bytesAvatar, _formatImage,
_bytesBackImage, _formatImage)))
            {
                Warning.SaveDataUser();
            }
            var imgAvatar = SQL.image(_loginTEXTBOX.Text);*/
            /*
            Form1 FORM1 = new Form1();
            FORM1._logogUser.BackgroundImage = imgAvatar;
            this.Refresh();*/
        }

        private void _hearingProblemsTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_hearingProblems_combobox, _pointConditionCheckHearingProblemsPICTUREBOX, _conditionCheckHearingProblemsLABEL, _boxCheckHearingProblemsPANEL, _hearingProblemsTIMER);

        private void _animationScrollTIMER_Tick(object sender, EventArgs e)
        {
            ToolBox.AppearanceOfSubItems(tb_hearingProblems_combobox, _positionCheckedBoxSteepThreeFLP, _checkedScrollEyeProblemFLP.Location.Y + _checkedScrollEyeProblemFLP.Height + 5, _animationScrollTIMER);
            if (!_animationScrollTIMER.Enabled) _positionCheckedBoxSteepThreeFLP.AutoScroll = true;
        }

        private void _soundEffectTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_soundEffect_combobox, _pointConditionCheckSoundEffectPICTUREBOX, _conditionCheckSoundEffectLABEL, _boxCheckSoundEffectPANEL, _soundEffectTIMER);

        private void _animationScrollSoundTIMER_Tick(object sender, EventArgs e)
        {
            ToolBox.AppearanceOfSubItems(tb_soundEffect_combobox, _maxVolumePANEL, _heightMaxVolume, _animationScrollSoundTIMER);
            _positionCheckedBoxSteepThreeFLP.Height = _checkedScrollEyeProblemFLP.Location.Y + _checkedScrollEyeProblemFLP.Height + 5;
            // _maxVolumePANEL

            if (_animationScrollSoundTIMER.Enabled)
            {
                _maxVolumePANEL.Width = _checkedScroMaxVolumeFLP.Width;
                _maxVolumePANEL.AutoSize = false;
            }
            else
            {

                if (!_animationScrollSoundTIMER.Enabled && _maxVolumePANEL.Height == 0)
                {
                    _maxVolumePANEL.AutoSize = false;
                    //_positionCheckedBoxSteepThreeFLP
                }
                else
                {
                    _maxVolumePANEL.AutoSize = true;
                }
            }
        }

        private void _maxVolumeTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_maxVolume_combobox, _pointConditionCheckMaxVolumePICTUREBOX, _conditionCheckMaxVolumeLABEL, _boxCheckMaxVolumePANEL, _maxVolumeTIMER);

        private void _eyeProblemsTIMER_Tick(object sender, EventArgs e) =>
            ToolBox.MovingTheStatusSlider(tb_eyeProblems_combobox, _pointConditionCheckEyeProblemPICTUREBOX, _conditionCheckEyeProblemLABEL, _boxCheckEyeProblemPANEL, _eyeProblemsTIMER);

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

        private void _usersLISTBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_numberUser != _usersLISTBOX.SelectedIndex)
            {
                _numberUser = _usersLISTBOX.SelectedIndex;
                var data = SQL.LoadDataChild(_usersLISTBOX.SelectedItem.ToString());
                //Password, Name, Surname, Block, SoundEffect, Inaction, ByDay
                _loginChildTEXTBOX.Text = _usersLISTBOX.SelectedItem.ToString();
                _passwordChildTEXTBOX.Text = data[0];
                _nameChildTEXTBOX.Text = data[1];
                _fineNameChildTEXTBOX.Text = data[2];
                if (Convert.ToInt32(data[3]) == 1)
                {
                    _pointConditionCheckBloclAccountPICTUREBOX_Click(null, null);
                }
                if (Convert.ToInt32(data[4]) == 1)
                {
                    _pointConditionCheckSoundEffectPICTUREBOX_Click(null, null);
                }
                _inactionChildComboBox.SelectedIndex = SwitchInaction(Convert.ToInt32(data[5]));
                if (Convert.ToInt32(data[6]) == 1)
                {
                    _pointConditionCheckTimeLimitPerDayPICTUREBOX_Click(null, null);
                }

                // Загрузка ограничения по дням недели
                // ПН
                data = SQL.LoadByDay(_usersLISTBOX.SelectedItem.ToString(), 1);
                new System.Threading.Thread(() => { ClickDifinitionOfTime(1, data); }).Start();
                data = SQL.LoadByDay(_usersLISTBOX.SelectedItem.ToString(), 2);
                new System.Threading.Thread(() => { ClickDifinitionOfTime(2, data); }).Start();

            }
        }


        private void ClickDifinitionOfTime(byte weekday, List<string> data)
        {
            byte holdNumberS, holdNumberE;
            int numberButton = 0;
            int hold = 0;
            string week = "";
            switch (weekday)
            {
                case 1:
                    week = "monday_";
                    break;
                case 2:
                    week = "tuesday_";
                    break;
                // Среда
                case 3:
                    week = "environs_";
                    break;
                // Четверг
                case 4:
                    week = "thursday_";
                    break;
                // Пятница
                case 5:
                    week = "friday_";
                    break;
                // Суббота
                case 6:
                    week = "saturday_";
                    break;
                // Воскресенье
                case 7:
                    week = "sunday_";
                    break;
            };
            // Получает список запретов
            for (byte index = 0; index < data.Count; index += 2)
            {
                // Приведение к типу расположения кнопок
                hold = Convert.ToByte(data[index]) + (weekday - 1);
                holdNumberS = Convert.ToByte(hold);

                hold = Convert.ToByte(data[index + 1]) + (weekday - 1);
                holdNumberE = Convert.ToByte(hold);

              // MessageBox.Show($"holdNumberS: {holdNumberS}\n holdNumberE: {holdNumberE}");

                if (holdNumberS != 0 && Convert.ToByte(data[index + 1]) != 0)
                {
                    InsertItIsByDay(holdNumberS, holdNumberE, week, 1);
                }
                else if (holdNumberS == 0)
                {
                    InsertItIsByDay(holdNumberS, holdNumberE, week, 2);
                }
                else
                {
                    new System.Threading.Thread(() =>
                    {
                        for (byte indexButton = holdNumberS; indexButton < 23; indexButton++)
                        {
                            // Выделение разрешенного времени
                            _elementsDate[indexButton * 7].BackColor = Color.OrangeRed;
                            // Добавление данных в массив
                            DefinitionOfTime($"{week}{indexButton * 7}", false);
                        }
                        //weekday
                        DefinitionOfTime($"{week}{168 - 6}", false);
                    })
                    { IsBackground = true, }.Start();
                }
            }
        }

        private void InsertItIsByDay(byte startTime, byte endTime, string weekday, int switchAddTime)
        {
            new System.Threading.Thread(() =>
            {
                //MessageBox.Show(startTime.ToString());
                for (byte indexButton = startTime; indexButton < endTime; indexButton++)
                {
                    if (switchAddTime == 1)
                    {
                        // Выделение разрешенного времени
                        _elementsDate[indexButton * 7].BackColor = Color.OrangeRed;
                        // Добавление данных в массив
                        DefinitionOfTime($"{weekday}{indexButton * 7}", false);
                    }
                    else if (switchAddTime == 2)
                    {
                        // Выделение разрешенного времени
                        _elementsDate[(indexButton == 0) ? 0 : indexButton * 7].BackColor = Color.OrangeRed;
                        // Добавление данных в массив
                        DefinitionOfTime($"{weekday}{((indexButton == 0) ? 0 : indexButton * 7).ToString()}", false);
                    }

                }
            })
            { IsBackground = true, }.Start();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
 
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

        private void _colorProgramPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                hex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            }
        }

        private void SettingParant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Information info = new Information();
                info.ShowDialog();
            }
        }
    }
}
