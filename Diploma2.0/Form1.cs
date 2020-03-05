using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Appearance;
using SqlQueryProcessing;
using ErrorInPrograms;
using SecurityModule;
using System.IO;
using Microsoft.Win32;

namespace Diploma2._0
{
    public partial class Form1 : Form
    {
        // Подключение к классам и библиотекам
        StyleWindows STYLEWINDOWS = new StyleWindows();
        Sql SQL = new Sql();
        Security.Backup SECURITY_BACKUP = new Security.Backup();

        Security SECURITY = new Security();


        List<Form> TEMP = new List<Form>();

        private int _endTime = -1;
        private bool _hearingProblems = false;

        Security.WorkingWithDateSystem SYSTEMTIME = new Security.WorkingWithDateSystem(DateTime.Now.Year, DateTime.Now.Month, 
            DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        Form testFORN = new Form();

        private FlowLayoutPanel _messageContentFLP = new FlowLayoutPanel();

        //
        //  Оформление личного кабинета
        //
        private Panel _leftMenuPANEL = new Panel();
        private Panel _borderLeftMenuPANEL = new Panel();
        private FlowLayoutPanel _menuFLP = new FlowLayoutPanel();
        private PictureBox _messagesPICTUREBOX = new PictureBox();
        private PictureBox _playPICTUREBOX = new PictureBox();
        private PictureBox _settingPICTUREBOX = new PictureBox();
        private PictureBox _settingChildPICTUREBOX = new PictureBox();
        private PictureBox _helpPICTUREBOX = new PictureBox();
        private PictureBox _controlPanelPICTUREBOX = new PictureBox();
        private PictureBox _logOutPICTUREBOX = new PictureBox();
        private FlowLayoutPanel _controlWindowFLP = new FlowLayoutPanel();
        private Panel _topPANEL = new Panel();
        private Label _namePage = new Label();
        private FlowLayoutPanel _userNameFLP = new FlowLayoutPanel();
        public PictureBox _logogUser = new PictureBox();
        private Label _loginUser = new Label();
        private System.Windows.Forms.Timer _timePC = new System.Windows.Forms.Timer();
        private Panel _topBorderMenu = new Panel();
        private Label _currentLabguage = new Label();
        private Label _newMessgaeLABEL = new Label();
        private Panel _controlPanelMsgPANEL = new Panel();
        private Panel _messagePanel = new Panel();
        //  Запрос на просмотр оставшегося времени
        private Panel _playPANEL = new Panel();
        private Label _timePlayLABEL = new Label();
        private Button _startTimerBUTTON = new Button();
        private Button _moreTimeBUTTON = new Button();
        private Label _errorLABEL = new Label();
        private bool isOpenMsgElement = false;
        // Подсказк
        //  Форма авторизации
        private ToolTip _logOutTOOLTIP = new ToolTip();
        private ToolTip _offPcTOOLTIP = new ToolTip();
        private ToolTip _rebootPcTOOLTIP = new ToolTip();
        private ToolTip _сhangeUserTOOLTIP = new ToolTip();
        private ToolTip _sleepModeTOOLTIP = new ToolTip();
        private ToolTip _hearingProblemTOOLTIP = new ToolTip();
        private ToolTip _enterInSystemTOOLTIP = new ToolTip();
        private ToolTip _loginTOOLTIP = new ToolTip();
        private ToolTip _passwordTOOLTIP = new ToolTip();
        private ToolTip _settingTOOLTIP = new ToolTip();
        private ToolTip _startTOOLTIP = new ToolTip();
        private ToolTip _helpTOOLTIP = new ToolTip();
        private ToolTip _controlTOOLTIP = new ToolTip();
        private ToolTip _homeTOOLTIP = new ToolTip();

        // Переменные
        private const int _widthLeftPanel = 50;

        // Получение ширины экрана в пикселях
        public static int _widthScreen { get; } = Screen.PrimaryScreen.Bounds.Width;
        // Получение высоты экрана в пикселях
        public static int _heightScreen { get; } = Screen.PrimaryScreen.Bounds.Height;
        //
        //  Данные пользователя
        //
        public static string _Login { get; set; }
        public static string _Password { get; set; }                                             
        public static string _Name { get; set; }
        public static string _Surname { get; set; } 
        public static string _SecretText { get; set; }
        public static string _SecretWord { get; set; }
        public static string _FontFamily { get; set; } 
        public static int _FontSize { get; set; }
        public static string _FontStyle { get; set; }
        public static string _ColorProgram { get; set; }
        public static int _Inaction { get; set; }
        public static int _AlertExpirationTime { get; set; }
        public static int _MaxVolume { get; set; }
        public static int _SoundEffect { get; set; }
        public static int _RecordVideo { get; set; }
        public static int _RecordAudio { get; set; }
        public static int _ScreenShot { get; set; }
        public static int _TimeOutScreenShot { get; set; }
        public static int _TimeWorkPc { get; set; }
        public static int _ByDay { get; set; }
        public static int _Block { get; set; }
        public static int _BlockProgram { get; set; }
        public static int _BlockUrl { get; set; }
        public static string _LastEnter { get; set; }
        public static int _Access { get; set; }
        public static int _AllTime { get; set; }
        public static int _EyeProblems { get; set; }
        public static Image _AvatarUser { get; set; }
        public static Image _backGround { get; set; }
        
        //  Текущая дата
        private DateTime _dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public Form1()
        {
            /*
            SECURITY_BACKUP.CreateBackUp();
            */

            InitializeComponent();
            _nextBUTTON.BackColor = StyleWindows._mainColorLoginForm;
            _nextBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColorLoginForm;
            _nextBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColorLoginForm;
            //
            // _timePC
            //
            this._timePC.Interval = 1000;
            this._timePC.Enabled = true;
            this._timePC.Tick += _timePC_Tick;
            _time.Location = new Point(_widthScreen - _time.Width - 20, _heightScreen - _time.Height - 20);

            //
            //  Настройка подсказок
            //
            // _offPcTOOLTIP
            StyleWindows.SettingToolTip(_offPcTOOLTIP, _offPc, "Выключение компьютера", "Выключение компьютера");
            // _сhangeUserTOOLTIP
            StyleWindows.SettingToolTip(_сhangeUserTOOLTIP, _сhangeUser, "Смена пользователя", "Смена пользователя в\nоперационной системе Windows");
            // _rebootPcTOOLTIP
            StyleWindows.SettingToolTip(_rebootPcTOOLTIP, _rebootPc, "Перезагрузка компьютера", "Перезагрузказрузка компьютера");
            // _sleepModeTOOLTIP
            StyleWindows.SettingToolTip(_sleepModeTOOLTIP, _sleepMode, "Переход в спящий режим", "Переход операционной системы Windows\nв спящий режим");
            // _sleepModeTOOLTIP
            StyleWindows.SettingToolTip(_hearingProblemTOOLTIP, _hearingProblem, "Специальные возможности", "Переключение на версию для\nлюдей с ограниченными возможностями");
            //  _enterInSystemTOOLTIP
            StyleWindows.SettingToolTip(_enterInSystemTOOLTIP, _nextBUTTON, "Авторизация", "Проверка введенных данных\nи существование пользователя");
            // _loginTOOLTIP
            StyleWindows.SettingToolTip(_loginTOOLTIP, _loginTEXTBOX, "Логин пользователя", "Уникальный идентификатор пользователя");
            //_passwordTOOLTIP
            StyleWindows.SettingToolTip(_passwordTOOLTIP, _passwordTEXTBOX, "Пароль пользователя", "Пароль подходящий к уникальному\nидентификатору пользователя");

            /*
            using (StreamReader read = new StreamReader(Properties.Resources.info_RessetPassword))
            {
            }*/

            /*
            ControlPanel controlPanel = new ControlPanel();
            controlPanel.Show();
            */

            //this.TopMost = true;
            _loginPANEL.BackColor = Color.FromArgb(210, 0, 0, 0);
            SetRoundedShape(_loginPANEL, 25);
            SetRoundedShape(_loginTEXTBOX, 13);
            SetRoundedShape(_passwordTEXTBOX, 13);
            //
            //  Выравнивание
            //
            // Форма авторизации
            _loginPANEL.Location = new Point(_widthScreen / 2 - _loginPANEL.Width / 2, _heightScreen / 2 - _loginPANEL.Height / 2);
            _dataLoginFLP.Location = new Point(_loginPANEL.Width / 2 - _dataLoginFLP.Width / 2, _loginPANEL.Height / 2 - _dataLoginFLP.Height / 2 - _menuWindowsFLP.Height / 2);
            Size size = TextRenderer.MeasureText(_resPassLABEL.Text, _resPassLABEL.Font);
            _resPassLABEL.Location = new Point(_dataLoginFLP.Width - size.Width, _dataLoginFLP.Location.Y + _dataLoginFLP.Height);
            //  Форма управления ОС
            _menuWindowsFLP.Location = new Point(_loginPANEL.Width / 2 - _menuWindowsFLP.Width / 2, _resPassLABEL.Location.Y + _resPassLABEL.Height + 5);

            _nextBUTTON.Focus();
        }
        #region Форма авторизации

        public static void NewStyleButton(Button button)
        {
            button.Font = StyleWindows._mainFont;
            button.BackColor = StyleWindows._mainColor;
            button.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            button.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
        }

        //
        // Закругление углов формы
        //
        public void SetRoundedShape(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(StyleWindows._mainColorLoginForm, 2.0f);
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
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);                                                      //
        }
        //
        //  Вывод информации о TextBox
        //
        //  Появоение надписи логи
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

        private void _offPc_Click(object sender, EventArgs e) => Security.CommandCMD("shutdown /f /s /t 0");

        private int EditColorBorder(int color, int value)
        {
            if (color != 0)
            {
                if (color > 245)
                {
                    color -= value * 2;
                }
                else
                {
                    color = (color > value) ? color - value : 0;
                }
            }
            else
            {
                color += value * 3;
            }
            return color;
        }

        private int EditColorHoverButton(int color, int value)
        {
            if (color < 245)
            {
                color += (value * 2 > 255) ? 255 : value * 2;
            }
            else
            {
                color -= value * 3;
            }
            return color;
        }

        private void _nextBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_loginTEXTBOX.Text.Length != 0 && _passwordTEXTBOX.Text.Length != 0 && _loginTEXTBOX.Text != "Логин" && _passwordTEXTBOX.Text != "Пароль")
            {
                var data = SQL.UserAuthorization(_loginTEXTBOX.Text, _passwordTEXTBOX.Text);
                
                if (data.Count > 1)
                {
                    _Login = data[0];
                    _Password = data[1];
                    _Name = data[2];
                    _Surname = data[3];
                    _SecretText = data[4];
                    _SecretWord = data[5];
                    _FontFamily = data[6];
                    _FontSize = Convert.ToInt32(data[7]);
                    _FontStyle = data[8];
                    _ColorProgram = data[9];
                    _Inaction = Convert.ToInt32(data[10]);
                    _AlertExpirationTime = Convert.ToInt32(data[11]);
                    _MaxVolume = Convert.ToInt32(data[12]);
                    _SoundEffect = Convert.ToInt32(data[13]);
                    _RecordVideo = Convert.ToInt32(data[14]);
                    _RecordAudio = Convert.ToInt32(data[15]);
                    _ScreenShot = Convert.ToInt32(data[16]);
                    _TimeOutScreenShot = Convert.ToInt32(data[17]);
                    _TimeWorkPc = Convert.ToInt32(data[18]);
                    _ByDay = Convert.ToInt32(data[19]);
                    _Block = Convert.ToInt32(data[20]);
                    _BlockProgram = Convert.ToInt32(data[21]);
                    _BlockUrl = Convert.ToInt32(data[22]);
                    _LastEnter = data[23];
                    _Access = Convert.ToInt32(data[24]);
                    _AllTime = Convert.ToInt32(data[25]);
                    _EyeProblems = Convert.ToInt32(data[26]);
                    _AvatarUser = SQL.image(_Login);
                    _backGround = SQL.BackGround(_Login);

                    if (_EyeProblems == 0)
                    {
                        this.BackgroundImage = _backGround;

                        StyleWindows._mainColor = ColorTranslator.FromHtml(_ColorProgram);
                        //
                        //  Преобразование обводки
                        //
                        var cvt = new FontConverter();

                        string font = _FontFamily + ";" + _FontSize + "pt;style=" + _FontStyle;

                        //string s = cvt.ConvertToString(StyleWindows._mainFont);
                        StyleWindows._mainFont = cvt.ConvertFromString(font) as Font;
                        // Присвоение стиля заголовков
                        font = _FontFamily + ";" + (_FontSize + 5) + "pt;style=" + _FontStyle;
                        //s = cvt.ConvertToString(StyleWindows._titleMainFont);
                        StyleWindows._titleMainFont = cvt.ConvertFromString(font) as Font;

                        //StyleWindows._mainFont = new Font("Calibri", (int)12, FontStyle.Regular);
                        StyleWindows._mainBorderColor = Color.FromArgb(EditColorBorder(StyleWindows._mainColor.R, 28), EditColorBorder(StyleWindows._mainColor.G, 29), EditColorBorder(StyleWindows._mainColor.B, 27));
                        StyleWindows._mainHoverButtonColor = Color.FromArgb(EditColorHoverButton(StyleWindows._mainColor.R, 24), EditColorHoverButton(StyleWindows._mainColor.G, 26), EditColorHoverButton(StyleWindows._mainColor.B, 14));
                    }
                    else
                    {
                        Color whiteFon = Color.White;
                        this.BackgroundImage = null;
                        this.BackColor = whiteFon;
                        StyleWindows._mainColor = ColorTranslator.FromHtml("#000000");
                        StyleWindows._mainColorLoginForm = StyleWindows._mainColor;
                        StyleWindows._mainColorText = Color.Black;

                        _resPassLABEL.ForeColor = StyleWindows._mainColorText;

                        _nextBUTTON.BackColor = whiteFon;
                        _nextBUTTON.FlatAppearance.BorderColor = StyleWindows._mainColor;
                        _nextBUTTON.FlatAppearance.MouseOverBackColor = whiteFon;
                        _nextBUTTON.ForeColor = StyleWindows._mainColorText;

                        _loginPANEL.BackColor = Color.FromArgb(0, 255, 255, 255);
                        _loginPANEL.BorderStyle = BorderStyle.FixedSingle;
                        SetRoundedShape(_loginPANEL, 1);
                        _menuWindowsFLP.BackColor = StyleWindows._mainColor;
                    }
                    if (_Access == 1)
                    {
                        _loginTEXTBOX.Text = "Логин";
                        _passwordTEXTBOX.Text = "Пароль";
                        _passwordTEXTBOX.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        _loginTEXTBOX.Text = "Логин";
                        _passwordTEXTBOX.Text = "Пароль";
                        _passwordTEXTBOX.UseSystemPasswordChar = false;
                    }
                    OpenUsersPersonalAccound();
                }
                else
                {
                    if(data.Count == 0)
                        Error.ClientError.NotFound();
                }
            }
        }
        public void OpenUsersPersonalAccound()
        {
            //
            //  Скрытие текущих элементов
            //
            this.Controls.Remove(_loginPANEL);


            //
            //  Переход в личный аккаунт пользователя
            //
                _menuWindowsFLP.Controls.Remove(_сhangeUser);
                _menuWindowsFLP.Controls.Remove(_rebootPc);
                _menuWindowsFLP.Controls.Remove(_sleepMode);
                _menuWindowsFLP.Controls.Remove(_offPc);

            StyleUsersPersonalAccount();
            _сhangeUser.Margin = MarginElementsControlWindow(_сhangeUser);
            _rebootPc.Margin = MarginElementsControlWindow(_rebootPc);
            _sleepMode.Margin = MarginElementsControlWindow(_sleepMode);
            _offPc.Margin = MarginElementsControlWindow(_offPc);
        }

        #endregion

        #region Личный кабинет пользователя
        //
        //  Выравнивание элемента по центру в меню
        //
        private Padding MarginElementsControlWindow(Control element)
        {
            return new Padding(_leftMenuPANEL.Width / 2 - element.Width / 2, 0, 0, 15);
        }

        private Padding MarginElementsLeftMenu(Control value)
        {
            return new Padding(_leftMenuPANEL.Width / 2 - value.Width / 2, 20, 0, 50);
        }
        private void StyleUsersPersonalAccount()
        {
                //
                //  Верхнее меню
                //
                //
                // _topPANEL
                //
                this._topPANEL.Dock = DockStyle.Top;
                this._topPANEL.Location = new Point(50, -1);
                this._topPANEL.Size = new Size(_widthScreen - 50, 35);
                //this._topPANEL.BackColor = ColorTranslator.FromHtml("#3F3474");
                this._topPANEL.BackColor = Color.FromArgb(200, 87, 87, 87);
                this._topPANEL.Name = "_topPANEL";
                this._topPANEL.Controls.Add(this._currentLabguage);
                this._topPANEL.Controls.Add(this._namePage);
                this._topPANEL.Controls.Add(this._userNameFLP);
                //
                //  _topBorderMenu
                //
                _topBorderMenu.Dock = DockStyle.Top;
                _topBorderMenu.Size = new Size(1, 1);
                _topBorderMenu.BackColor = Color.FromArgb(120, 158, 158, 158);
                _topBorderMenu.Name = "_topBorderMenu";
                //
                //  _namePage
                //
                _namePage.AutoSize = true;
                _namePage.Font = StyleWindows._mainFont;
                _namePage.BackColor = Color.Transparent;
                _namePage.Name = "_namePage";

                _namePage.Text = (_Access == 1) ? "Аккаунт родителя" : "Аккаунт ребенка";
                _namePage.ForeColor = Color.White;
                _namePage.Location = new Point(_topPANEL.Width / 2 - _namePage.Width / 2, _topPANEL.Height / 2 - _namePage.Height / 2);
                //
                //  _currentLabguage
                //
                _currentLabguage.AutoSize = true;
                _currentLabguage.Font = StyleWindows._mainFont;
                _currentLabguage.BackColor = Color.Transparent;
                _currentLabguage.Name = "_currentLabguage";
                _currentLabguage.ForeColor = Color.White;
                _currentLabguage.TextAlign = ContentAlignment.MiddleCenter;
                _currentLabguage.Location = new Point(50, _topPANEL.Height / 2 - _currentLabguage.Height / 2);
                _checkLanguageTIMER.Enabled = true;
                //
                //  _logogUser
                //
                _logogUser.Name = "_logogUser";
                _logogUser.Size = new Size(30, 30);
                _logogUser.Margin = new Padding(0, 0, 10, 0);
                _logogUser.BackColor = Color.Transparent;
                _logogUser.BorderStyle = BorderStyle.None;
                _logogUser.BackgroundImageLayout = ImageLayout.Stretch;
                _logogUser.BackgroundImage = _AvatarUser;
                SetRoundedShape(_logogUser, 30);
                //
                //  _loginUser
                //
                _loginUser.Name = "_loginUser";
                _loginUser.AutoSize = true;
                _loginUser.BackColor = Color.Transparent;
                _loginUser.Font = StyleWindows._mainFont;
                _loginUser.ForeColor = Color.White;
                _loginUser.Text = _Login;
                _loginUser.Margin = new Padding(0, _topPANEL.Height / 2 - _loginUser.Height / 2, 0, 0);
                //
                //  _userNameFLP
                //
                _userNameFLP.BackColor = Color.Transparent;
                _userNameFLP.Name = "_userNameFLP";
                _userNameFLP.Size = new Size(_loginUser.Width + _logogUser.Width, _logogUser.Height + 1);
                _userNameFLP.Location = new Point(_topPANEL.Width - _userNameFLP.Width - 25, _topPANEL.Height / 2 - _userNameFLP.Height / 2);
                _userNameFLP.Controls.Add(_logogUser);
                _userNameFLP.Controls.Add(_loginUser);
                //
                //  Левое меню
                //
                //
                //  _borderLeftMenuPANEL
                //
                this._borderLeftMenuPANEL.BackColor = Color.White;
                this._borderLeftMenuPANEL.Dock = DockStyle.Left;
                this._borderLeftMenuPANEL.Size = new Size(2, 1);
                this._borderLeftMenuPANEL.Name = "_borderLeftMenuPANEL";
                // 
                // _leftMenuPANEL
                // 
                this._leftMenuPANEL.BackColor = StyleWindows._mainColor;
                this._leftMenuPANEL.Controls.Add(this._controlWindowFLP);
                this._leftMenuPANEL.Controls.Add(this._menuFLP);
                this._leftMenuPANEL.Dock = System.Windows.Forms.DockStyle.Left;
                this._leftMenuPANEL.Name = "_leftMenuPANEL";
                this._leftMenuPANEL.Size = new System.Drawing.Size(50, 623);
                this._leftMenuPANEL.TabIndex = 0;
                // 
                // _messagesPICTUREBOX
                // 
                PictureBox message = new PictureBox();
                message.BackgroundImage = Properties.Resources.icon_menu_home;
                message.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                message.Cursor = System.Windows.Forms.Cursors.Hand;
                message.Location = new System.Drawing.Point(7, 35);
                message.Name = "_messagesPICTUREBOX";
                message.Size = new System.Drawing.Size(35, 35);
                message.Margin = MarginElementsLeftMenu(message);
                message.TabIndex = 0;
                message.TabStop = false;
                message.Click += Message_Click;
                this._messagesPICTUREBOX = message;
                // 
                // _playPICTUREBOX
                // 
                PictureBox play = new PictureBox();
                play.BackgroundImage = Properties.Resources.icon_menu_goToWindows;
                play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                play.Cursor = System.Windows.Forms.Cursors.Hand;
                play.Location = new System.Drawing.Point(7, 108);
                play.Name = "_playPICTUREBOX";
                play.Size = new System.Drawing.Size(35, 35);
                play.Margin = MarginElementsLeftMenu(play);
                play.TabIndex = 1;
                play.TabStop = false;
                //play.Paint += _playPICTUREBOX_Paint;
                play.Click += _playPICTUREBOX_Click;
                this._playPICTUREBOX = play;
                if (_Access == 0)
                {
                    // 
                    // _settingPICTUREBOX
                    //
                    PictureBox setting = new PictureBox();
                    setting.BackgroundImage = Properties.Resources.icon_menu_setting;
                    setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    setting.Cursor = System.Windows.Forms.Cursors.Hand;
                    setting.Location = new System.Drawing.Point(7, 181);
                    setting.Name = "_settingPICTUREBOX";
                    setting.Size = new System.Drawing.Size(35, 35);
                    setting.Margin = MarginElementsLeftMenu(setting);
                    setting.TabIndex = 2;
                    setting.TabStop = false;
                    setting.Click += _settingPICTUREBOX_Click;
                    this._settingPICTUREBOX = setting;
                    StyleWindows.SettingToolTip(_settingTOOLTIP, setting, "Настройки", "Управление личным аккаунтом и\nаккаунтами детей");

                }
                else
                {
                    // 
                    // _settingChildPICTUREBOX
                    // 
                    PictureBox settingCheld = new PictureBox();
                    settingCheld.BackgroundImage = Properties.Resources.icon_menu_setting;
                    settingCheld.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    settingCheld.Cursor = System.Windows.Forms.Cursors.Hand;
                    settingCheld.Location = new System.Drawing.Point(7, 400);
                    settingCheld.Name = "_settingChildPICTUREBOX";
                    settingCheld.Size = new System.Drawing.Size(35, 35);
                    settingCheld.Margin = MarginElementsLeftMenu(settingCheld);
                    settingCheld.TabIndex = 3;
                    settingCheld.TabStop = false;
                    settingCheld.Click += _settingChildPICTUREBOX_Click;
                    _settingChildPICTUREBOX = settingCheld;
                    StyleWindows.SettingToolTip(_settingTOOLTIP, settingCheld, "Настройки", "Управление личным аккаунтом");
                }
                // 
                // _helpPICTUREBOX
                // 
                PictureBox help = new PictureBox();
                help.BackgroundImage = Properties.Resources.icon_menu_help;
                help.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                help.Cursor = System.Windows.Forms.Cursors.Hand;
                help.Location = new System.Drawing.Point(7, 327);
                help.Name = "_helpPICTUREBOX";
                help.Size = new System.Drawing.Size(35, 35);
                help.Margin = MarginElementsLeftMenu(help);
                help.TabIndex = 4;
                help.TabStop = false;
                help.Click += _helpPICTUREBOX_Click;
                _helpPICTUREBOX = help;
                if (_Access == 1)
                {
                    // 
                    // _controlPanelPICTUREBOX
                    // 
                    PictureBox controlPanel = new PictureBox();
                    controlPanel.BackgroundImage = Properties.Resources.icon_menu_controlPanel;
                    controlPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    controlPanel.Cursor = System.Windows.Forms.Cursors.Hand;
                    controlPanel.Location = new System.Drawing.Point(7, _helpPICTUREBOX.Location.Y + _helpPICTUREBOX.Height + 25);
                    controlPanel.Name = "_controlPanelPICTUREBOX";
                    controlPanel.Size = new System.Drawing.Size(35, 35);
                    controlPanel.Margin = MarginElementsLeftMenu(controlPanel);
                    controlPanel.TabIndex = 4;
                    controlPanel.TabStop = false;
                    controlPanel.Click += _controlPanelPICTUREBOX_Click;
                    _controlPanelPICTUREBOX = controlPanel;
                    StyleWindows.SettingToolTip(_controlTOOLTIP, controlPanel, "Дополнительные функции", "Дополнительные функции ограничения\nработы ребенка за компьютером");
                }
                // 
                // _menuFLP
                // 
                this._menuFLP.Controls.Add(this._messagesPICTUREBOX);
                this._menuFLP.Controls.Add(this._playPICTUREBOX);
                this._menuFLP.Controls.Add((_Access == 0) ? this._settingPICTUREBOX : this._settingChildPICTUREBOX);
                this._menuFLP.Controls.Add(this._helpPICTUREBOX);
                this._menuFLP.Controls.Add(this._controlPanelPICTUREBOX);
                this._menuFLP.Dock = System.Windows.Forms.DockStyle.Top;
                this._menuFLP.Location = new System.Drawing.Point(0, 0);
                this._menuFLP.Name = "_menuFLP";
                this._menuFLP.Size = new System.Drawing.Size(0, 520);
                this._menuFLP.TabIndex = 1;
                this._menuFLP.BackColor = StyleWindows._mainColor;


                // 
                // _unLogin
                // 
                PictureBox unLogin = new PictureBox();
                unLogin.BackgroundImage = Properties.Resources.LogOut;
                unLogin.BackgroundImageLayout = ImageLayout.Stretch;
                unLogin.Cursor = Cursors.Hand;
                //unLogin.Location = new System.Drawing.Point(7, 327);
                unLogin.Name = "unLogin";
                unLogin.Size = new Size(20, 20);
                unLogin.Margin = MarginElementsControlWindow(unLogin);
                unLogin.TabIndex = 4;
                unLogin.TabStop = false;
                unLogin.Click += UnLogin_Click;
                StyleWindows.SettingToolTip(_logOutTOOLTIP, unLogin, "Смена пользователя", "Сменить пользователя в приложении");
                _logOutPICTUREBOX = unLogin;

                // 
                // _controlWindowFLP
                // 
                this._controlWindowFLP.Controls.Add(this._logOutPICTUREBOX);
                this._controlWindowFLP.Controls.Add(this._сhangeUser);
                this._controlWindowFLP.Controls.Add(this._rebootPc);
                this._controlWindowFLP.Controls.Add(this._sleepMode);
                this._controlWindowFLP.Controls.Add(this._offPc);
                this._controlWindowFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
                this._controlWindowFLP.Name = "_controlWindowFLP";
                this._controlWindowFLP.Size = new System.Drawing.Size(35, 170);
                this._controlWindowFLP.TabIndex = 2;

                StyleWindows.SettingToolTip(_homeTOOLTIP, message, "Начальный экран", "Вывод личных сообщений");
                StyleWindows.SettingToolTip(_startTOOLTIP, play, "Переход на рабочий стол", "Проверка на доступ перехода\nк рабочему столу");
                StyleWindows.SettingToolTip(_helpTOOLTIP, help, "Помощь", "Документация пользователя");


            //
            //  Форма
            //
                this.Controls.Add(this._topBorderMenu);

                this.Controls.Add(this._topPANEL);

                this.Controls.Add(this._borderLeftMenuPANEL);

                this.Controls.Add(this._leftMenuPANEL);
        }

        private void UnLogin_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            LogOut();
        }

        private void Message_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            LoadMessageChild();
        }

        private void LoadMessageChild()
        {
            
            if (!isOpenMsgElement)
            {
                isOpenMsgElement = true;
                CreateMessageForm();
                var data = SQL.SelectMessageChild(_Login);
                for (int track = 0; track < data.Count; track += 3)
                {
                    //SELECT LoginFrom, Date, Message FROM Message WHERE LoginIn=@login
                    var control = CreateMessageElement(data[track + 2], track, data[track + 1], data[track]);
                    _messageContentFLP.Controls.Add(control);

                    if (track == data.Count - 3) break;
                }
            }
        }

        private Control CreateMessageElement(string message, int index, string date, string sender)
        {
            Size sizeText = TextRenderer.MeasureText("Test", StyleWindows._mainFont);

            Panel contentPANEL = new Panel();
            Label messageLABEL = new Label();
            Panel messagePANEL = new Panel();
            Panel infoPANEL = new Panel();
            Label dateMessage = new Label();
            Label senderMessage = new Label();

            //
            //  contentPANEL
            //
            contentPANEL.Name = "contentPANEL_" + index;
            contentPANEL.Size = new Size(_messageContentFLP.Width - 30, 160);
            contentPANEL.AutoScroll = true;
            //contentPANEL.BackColor = _messageContentFLP.BackColor;
            contentPANEL.BackColor = Color.White;
            contentPANEL.BorderStyle = BorderStyle.FixedSingle;
            //ToolBox.SetRoundedShape(contentPANEL, 20);
            //
            //  messageLABEL
            //
            messageLABEL.Name = "messageLABEL_" + index;
            messageLABEL.AutoSize = true;
            messageLABEL.MaximumSize = new Size(contentPANEL.Width - 30, 0);
            messageLABEL.Text = message;
            messageLABEL.Font = StyleWindows._mainFont;
            //
            //  messagePANEL
            //
            messagePANEL.Name = "messagePANEL_" + index;
            messagePANEL.BackColor = contentPANEL.BackColor;
            messagePANEL.Dock = DockStyle.Fill;
            messagePANEL.AutoScroll = true;
            // messageLABEL.ForeColor = Color.White;
            //messageLABEL.Font = StyleWindows._mainFont,
            //
            //  infoPANEL
            //
            infoPANEL.Name = "infoPANEL_" + index;
            infoPANEL.Size = new Size(0, sizeText.Height + 5);
            infoPANEL.Dock = DockStyle.Bottom;
            infoPANEL.BackColor = contentPANEL.BackColor;
            infoPANEL.BorderStyle = BorderStyle.FixedSingle;
            //
            //  dateMessage
            //
            dateMessage.Name = "dateMessage_" + index;
            dateMessage.Font =  StyleWindows._mainFont;
            dateMessage.AutoSize = true;
            dateMessage.Text = "Дата: " + date;
            dateMessage.Location = new Point(10, infoPANEL.Height / 2 - dateMessage.Height / 2);
            //
            //  senderMessage
            //
            sizeText = TextRenderer.MeasureText(dateMessage.Text, dateMessage.Font);
            senderMessage.Name = "senderMessage_" + index;
            senderMessage.Font =  StyleWindows._mainFont;
            senderMessage.AutoSize = true;
            senderMessage.Text = "Отправитель: " + sender;
            senderMessage.Location = new Point(dateMessage.Location.X + sizeText.Width + 10, infoPANEL.Height / 2 - senderMessage.Height / 2);

            messagePANEL.Controls.Add(messageLABEL);
            infoPANEL.Controls.Add(dateMessage);
            infoPANEL.Controls.Add(senderMessage);

            contentPANEL.Controls.Add(messagePANEL);
            contentPANEL.Controls.Add(infoPANEL);

            return contentPANEL;
        }

        private void CreateMessageForm()
        {
            Panel messagePanel = new Panel();
            Label newMessgaeLABEL = new Label();
            FlowLayoutPanel messageContentFLP = new FlowLayoutPanel();
            Panel controlPanelMsgPANEL = new Panel();

            //
            //  messagePanel
            //
            messagePanel.Name = "_messagePanel";
            messagePanel.Size = new Size(700, 350);
            messagePanel.BackColor = Color.FromArgb(120, 0, 0, 0);
            messagePanel.Location = new Point((_leftMenuPANEL.Width + ClientSize.Width) / 2 - messagePanel.Width / 2, (_topPANEL.Height + ClientSize.Height) / 2 - messagePanel.Height / 2);
            ToolBox.SetRoundedShape(messagePanel, 20);
            _messagePanel = messagePanel;

            //
            //  messageContent
            //
            messageContentFLP.Name = "messageContent";
            messageContentFLP.Size = new Size(messagePanel.Width - 10, messagePanel.Height - 10);
            messageContentFLP.Location = new Point(5, 5);
            messageContentFLP.AutoScroll = true;
            messageContentFLP.BackColor = Color.White;
            ToolBox.SetRoundedShape(messageContentFLP, 20);
            _messageContentFLP = messageContentFLP;

            
            if (Form1._Access == 1)
            {
                //
                //  controlPanelMsgPANEL
                //
                controlPanelMsgPANEL.Dock = DockStyle.Top;
                controlPanelMsgPANEL.Size = new Size(messagePanel.Width - 16, 90);
                controlPanelMsgPANEL.BackColor = messageContentFLP.BackColor;
                controlPanelMsgPANEL.AutoScroll = true;
                _controlPanelMsgPANEL = controlPanelMsgPANEL;
                //
                //  newMessgaeLABEL
                //
                newMessgaeLABEL.AutoSize = true;
                newMessgaeLABEL.Font = StyleWindows._mainFont;
                newMessgaeLABEL.Text = "Написать сообщение";
                Size size = TextRenderer.MeasureText(newMessgaeLABEL.Text, newMessgaeLABEL.Font);
                controlPanelMsgPANEL.Height = size.Height + 3;
                newMessgaeLABEL.ForeColor = Color.Black;
                newMessgaeLABEL.Location = new Point(controlPanelMsgPANEL.Width - size.Width - 25, 0);
                newMessgaeLABEL.Cursor = Cursors.Hand;
                newMessgaeLABEL.Click += (s, e) => { new NewMessage().ShowDialog(); };
                _newMessgaeLABEL = newMessgaeLABEL;
                controlPanelMsgPANEL.Controls.Add(newMessgaeLABEL);
            }
            messageContentFLP.Controls.Add(controlPanelMsgPANEL);
            messagePanel.Controls.Add(messageContentFLP);
            this.Controls.Add(messagePanel);
            
        }

        private void DisposeMsg()
        {
            _newMessgaeLABEL.Dispose();
            _controlPanelMsgPANEL.Dispose();
            _messageContentFLP.Controls.Clear();
            _messageContentFLP.Dispose();
            _messagePanel.Dispose();
        }

        private void _helpPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            Information information = new Information();
            information.ShowDialog();
        }

        private void _controlPanelPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            ControlPanel controlPanel = new ControlPanel();
            controlPanel.ShowDialog();
        }
        SettingParant settingParant;
        private void _settingChildPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            SettingParant settingParant = new SettingParant();
            settingParant.ShowDialog();
        }

        //
        //  Отрисовка оставшегося времени у пользователя
        //
        private void _playPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            DisposeMsg();
            bool openEnter = false;
            int widthButton = 169;
            // 
            // _playPANEL
            // 
            this._playPANEL.BackColor = System.Drawing.Color.FromArgb(200, 0,0,0);
            this._playPANEL.Controls.Add(this._errorLABEL);
            this._playPANEL.Controls.Add(this._moreTimeBUTTON);
            this._playPANEL.Controls.Add(this._startTimerBUTTON);
            this._playPANEL.Controls.Add(this._timePlayLABEL);
            this._playPANEL.Name = "_playPANEL";
            this._playPANEL.Size = new System.Drawing.Size(368, 183);
            this._playPANEL.Location = new System.Drawing.Point(_widthScreen / 2 - _playPANEL.Width / 2 + _leftMenuPANEL.Width, _heightScreen / 2 - _playPANEL.Height / 2 + _topPANEL.Height);
            this._playPANEL.TabIndex = 0;
            SetRoundedShape(_playPANEL, 25);
            // 
            // _errorLABEL
            // 
            this._errorLABEL.BackColor = Color.Transparent;
            this._errorLABEL.Font = StyleWindows._mainFont;
            this._errorLABEL.ForeColor = System.Drawing.Color.Red;
            this._errorLABEL.Size = new System.Drawing.Size(208, 19);
            this._errorLABEL.Location = new System.Drawing.Point(_playPANEL.Width / 2 - _errorLABEL.Width / 2 - 30, 10);
            this._errorLABEL.Name = "_errorLABEL";
            this._errorLABEL.TabIndex = 3;
            this._errorLABEL.Text = "Ваш аккаунта заблокирован!";
            this._errorLABEL.AutoSize = true;
            this._errorLABEL.Visible = (_Block == 1) ? true : false;
            if (_ByDay == 1)
            {
                var data = SQL.SelectTimeWeekdayChild(_Login, DateTime.Now.DayOfWeek.ToString());
                int endTime = -1;
                int nowHour = DateTime.Now.Hour;
                for (int index = 0; index < data.Count; index += 2)
                {
                    if (nowHour >= data[index] && nowHour < data[index + 1])
                    {
                        openEnter = true;
                        this._timePlayLABEL.Text = "Доступ закроется в " + data[index + 1] + ":00";
                        _startTimerBUTTON.Enabled = (_Block == 0) ? true : false;
                        
                        break;
                    }
                }
                if (!openEnter)
                {
                    this._timePlayLABEL.Text = "Доступ закрыт";
                    _startTimerBUTTON.Enabled = false;
                }

            }
            else
            {
                this._timePlayLABEL.Text = "Доступно времени " + GoToWondows.ConvertTime(_TimeWorkPc);
                if (_Block == 1)
                {
                    _startTimerBUTTON.Enabled = false;
                }
                else
                {
                    this._startTimerBUTTON.Enabled = (_TimeWorkPc <= 0) ? false : true;
                }
            }

            // 
            // _timePlayLABEL
            // 
            this._timePlayLABEL.BackColor = Color.Transparent;
            this._timePlayLABEL.Font = StyleWindows._mainFont;
            this._timePlayLABEL.ForeColor = System.Drawing.Color.White;
            this._timePlayLABEL.Size = new System.Drawing.Size(266, 19);
            this._timePlayLABEL.Name = "_timePlayLABEL";
            this._timePlayLABEL.AutoSize = true;
            this._timePlayLABEL.Location = new System.Drawing.Point(_playPANEL.Width / 2 - _timePlayLABEL.Width / 2, _errorLABEL.Location.Y + _errorLABEL.Height + 20);
            this._timePlayLABEL.TabIndex = 0;
            // 
            // _startTimerBUTTON
            // 
            this._startTimerBUTTON.Font = StyleWindows._mainFont;
            this._startTimerBUTTON.Location = new System.Drawing.Point(13, _timePlayLABEL.Location.Y + _timePlayLABEL.Height + 10);
            this._startTimerBUTTON.ForeColor = Color.White;
            this._startTimerBUTTON.Name = "_startTimerBUTTON";
            this._startTimerBUTTON.TabIndex = 1;
            this._startTimerBUTTON.Text = "Перейти на рабочий стол";
            this._startTimerBUTTON.Size = StyleWindows.NewSizeButton(_startTimerBUTTON, StyleWindows._mainFont);
            this._startTimerBUTTON.UseVisualStyleBackColor = true;
            this._startTimerBUTTON.FlatStyle = FlatStyle.Flat;
            this._startTimerBUTTON.FlatAppearance.BorderSize = 2;
            this._startTimerBUTTON.BackColor = StyleWindows._mainColor;
            this._startTimerBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._startTimerBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            
            this._startTimerBUTTON.Click += _startTimerBUTTON_Click;
            // 
            // _moreTimeBUTTON
            // 
            this._moreTimeBUTTON.Font = StyleWindows._mainFont;
            this._moreTimeBUTTON.Location = new System.Drawing.Point(188, _startTimerBUTTON.Location.Y);
            this._moreTimeBUTTON.Name = "_moreTimeBUTTON";
            this._moreTimeBUTTON.Size = new System.Drawing.Size(169, 40);
            this._moreTimeBUTTON.TabIndex = 2;
            this._moreTimeBUTTON.Text = "Продление времени";
            this._moreTimeBUTTON.Size = _startTimerBUTTON.Size;
            this._moreTimeBUTTON.UseVisualStyleBackColor = true;
            this._moreTimeBUTTON.ForeColor = Color.White;
            this._moreTimeBUTTON.FlatStyle = FlatStyle.Flat;
            this._moreTimeBUTTON.BackColor = StyleWindows._mainColor;
            this._moreTimeBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._moreTimeBUTTON.FlatAppearance.BorderSize = 2;
            this._moreTimeBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            this._moreTimeBUTTON.Click += _moreTimeBUTTON_Click;

            this.Controls.Add(_playPANEL);
        }

        private void _moreTimeBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            MessageBox.Show("Данная функция недоступна в бесплатной версии программы", "Отказано в доступе", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            /*
            MoreTime moreTime = new MoreTime();
            moreTime.ShowDialog();*/
        }

        private void _startTimerBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            CheckFileHostsWin();
            // Блокировка приложений в регистре
            
            if (_RecordVideo == 1)
            {
                VideoChannelProcessing.RecordVideo.StartRecord(Application.StartupPath + @"\Video", _Login);
            }
            GoToWondows goToWondows = new GoToWondows(_Login, _ScreenShot, _TimeOutScreenShot);
            goToWondows.Show();
            this.Hide();
        }
        private void CheckFileHostsWin()
        {
            var data = SQL.SelectBlockUrl(_Login);
            if (data.Count != 0)
            {
                string ip = Security.LocalIp();
                using (FileStream file = new FileStream(SECURITY.WayFileHostsWin() + @"\" + SECURITY._hostsName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(file, Encoding.Default))
                    {
                        foreach (string value in data)
                        {
                            foreach (string prefix in Security._listPrefixUrl)
                            {
                                sw.WriteLine(ip + " " + prefix + value);
                            }
                            sw.WriteLine(ip + " " + value);
                        }
                        sw.Close();
                    }
                    file.Close();
                }
            }
        }
        //
        //  Открытие меню настроек
        //
        private void _settingPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            SettingChild settingChild = new SettingChild();
            settingChild.ShowDialog();
        }

        //
        //  Время в реальном времени (_timeLABEL)
        //
        private void _timePC_Tick(object sender, EventArgs e)
        {
            _time.Text = DateTime.Now.ToShortTimeString();
            _time.Location = new Point(_widthScreen - _time.Width - 20, _heightScreen - _time.Height - 20);
        }

        //
        //  Отрисовка кнопки просмотра оставшегося времени за компьютером
        //
        private void _playPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.White, 2.0F);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.DrawLine(p, _playPICTUREBOX.Width, _playPICTUREBOX.Height / 2, 0, 0);
            gr.DrawLine(p, 0, 0, 7, _playPICTUREBOX.Height / 2);
            gr.DrawLine(p, 7, _playPICTUREBOX.Height / 2, 0, _playPICTUREBOX.Height);
            gr.DrawLine(p, 0, _playPICTUREBOX.Height, _playPICTUREBOX.Width, _playPICTUREBOX.Height / 2);
            GC.Collect();
        }
        #endregion

        private void SettingMenuWindowsFLP(Control value)
        {
            _menuWindowsFLP.Controls.Add(value);
            value.Margin = new Padding(3, 3, 22, 3);
        }

        private void LogOut()
        {

            // Закрытие всех открытых форм
            this.BackgroundImage = Properties.Resources.fon;
            _messagesPICTUREBOX.Dispose();
            _playPICTUREBOX.Dispose();
            _settingPICTUREBOX.Dispose();
            _helpPICTUREBOX.Dispose();
            _controlPanelPICTUREBOX.Dispose();
            _logOutPICTUREBOX.Dispose();

            this._leftMenuPANEL.Controls.Clear();
            this._menuFLP.Controls.Clear();
            this.Controls.Clear();
            this.Controls.Add(_loginPANEL);
            this.Controls.Add(_time);
            SettingMenuWindowsFLP(_сhangeUser);
            SettingMenuWindowsFLP(_rebootPc);
            SettingMenuWindowsFLP(_sleepMode);
            SettingMenuWindowsFLP(_offPc);
            _menuWindowsFLP.Controls.Add(_hearingProblem);
            _settingChildPICTUREBOX.Dispose();
            _hearingProblem.Margin = new Padding(3, 3, 0, 3);
            GC.Collect();
        }

        private void _checkLanguageTIMER_Tick(object sender, EventArgs e)
        {
            if (InputLanguage.CurrentInputLanguage.LayoutName != _currentLabguage.Text)
                _currentLabguage.Text = InputLanguage.CurrentInputLanguage.LayoutName;
        }

        private void _hearingProblem_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (!_hearingProblems)
            {
                Color whiteFon = Color.White;
                this.BackgroundImage = null;
                this.BackColor = whiteFon;
                StyleWindows._mainColor = ColorTranslator.FromHtml("#000000");
                StyleWindows._mainColorLoginForm = StyleWindows._mainColor;
                StyleWindows._mainColorText = Color.Black;

                _resPassLABEL.ForeColor = StyleWindows._mainColorText;

                _nextBUTTON.BackColor = whiteFon;
                _nextBUTTON.FlatAppearance.BorderColor = StyleWindows._mainColor;
                _nextBUTTON.FlatAppearance.MouseOverBackColor = whiteFon;
                _nextBUTTON.ForeColor = StyleWindows._mainColorText;

                _loginPANEL.BackColor = Color.FromArgb(0, 255, 255, 255);
                _loginPANEL.BorderStyle = BorderStyle.FixedSingle;
                SetRoundedShape(_loginPANEL, 1);
                _menuWindowsFLP.BackColor = StyleWindows._mainColor;
                _hearingProblems = true;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.fon;
                SetRoundedShape(_loginPANEL, 20);
                StyleWindows._mainColorLoginForm = ColorTranslator.FromHtml("#574B90");
                StyleWindows._mainBorderColorLoginForm = Color.FromArgb(255, 59, 46, 117);
                StyleWindows._mainHoverButtonColorLoginForm = Color.FromArgb(255, 111, 101, 158);

                StyleWindows._mainColor = ColorTranslator.FromHtml("#574B90");
                StyleWindows._mainBorderColor = Color.FromArgb(255, 59, 46, 117);
                StyleWindows._mainHoverButtonColor = Color.FromArgb(255, 111, 101, 158);
                StyleWindows._mainColorText = Color.White;

                _nextBUTTON.BackColor = StyleWindows._mainColorLoginForm;
                _nextBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColorLoginForm;
                _nextBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColorLoginForm;
                _nextBUTTON.ForeColor = StyleWindows._mainColorText;
                _loginPANEL.BackColor = Color.FromArgb(210, 0, 0, 0);
                SetRoundedShape(_loginPANEL, 25);
                _resPassLABEL.ForeColor = StyleWindows._mainColorText;

                _menuWindowsFLP.BackColor = Color.Transparent;
                _hearingProblems = false;
            }
            _time.ForeColor = StyleWindows._mainColorText;
            //this.Refresh();
            //_dataLoginFLP.Refresh();
        }

        private void _resPassLABEL_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            NewPassword newPassword = new NewPassword(_hearingProblems);
            newPassword.ShowDialog();
        }

        private void _сhangeUser_Click(object sender, EventArgs e) => Security.CommandCMD("logoff");

        private void _sleepMode_Click(object sender, EventArgs e) => Security.CommandCMD("shutdown /f /h");

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                MessageBox.Show("");
                Information info = new Information();
                info.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            try
            {
                if (_Access != 1)
                {
                    Security.CommandCMD("shutdown /f /s /t 0");
                }
            }
            catch
            {
                Security.CommandCMD("shutdown /f /s /t 0");
            }
            */
            Application.Exit();
        }
    }
}
