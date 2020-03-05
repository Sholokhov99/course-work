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
using System.IO;
using Appearance;
using SqlQueryProcessing;
using ErrorInPrograms;
using SecurityModule;

namespace Diploma2._0
{
    public partial class NewUser : StyleWindows
    {
        //  Классы
        Sql SQL = new Sql();
        Form1 FORM1 = new Form1();
        Security SECURITY = new Security();


        // Структуры
        TempUser TEMPUSER = new TempUser();

        private int _steeps = 0;                            // Номер страницы (на которой находится пользователь)
        private const int _minimalLengthInDataTextbox = 4;  // Минимальная длина заполнения TextBox (не всех)

        private Color _nowIsStage = Color.FromArgb(255, 37, 25, 92);
        private Color _thisStageHasNotPassed = Color.FromArgb(255, 150, 139, 200);
        private Color _thisStageHasPassed = StyleWindows._mainColor;



        //
        //  Запрещенные символы для ввода в TextBox
        //
        List<char> _errorLetter = new List<char>()
        {
            '\'',
            '!',
            '@',
            '#',
            '\"',
            '№',
            '$',
            ';',
            '%',
            '^',
            ':',
            '&',
            '?',
            '*',
            '(',
            ')',
            '+',
            '\\',
            '.',
            ',',
            '/',
            '\n',
            '_'
        };

        /*
            Создания пользователя для ребенка 
        */
        //
        //  Переменные
        //
        private const string _steepOne = "Выбор создаваемого аккаунта"; 
        private const string _steepTwo = "Конфиденциальные данные"; 
        private const string _steepThree = "Оформление рабочей области"; 
        private const string _steepFour= "Ограничение работы за компьютером"; 
        private const string _steepFive= "Режим шпиона";
        //
        //  Первый этап создания пользователя (Личные данные)
        //

        //
        // Login
        //
        private TextBox _loginTEXTBOX = new TextBox();                                          // Поле ввода логина пользователя
        private Label _loginLABEL = new Label();                                                // Текст подсказывающий вводимый текст в поле (логин)
        private Label _errorLoginLABEL = new Label();                                           // Ошибка ввода логина пользователя
        //
        //  Password
        //
        private TextBox _passwordTEXTBOX = new TextBox();                                       // Поле ввода пароля
        private Label _passwordLABEL = new Label();                                             // Текст подсказывающий вводимый текст в поле (пароль)
        private Label _errorPasswordLABEL = new Label();                                        // Ошибка ввода пароля пользователя
        //
        //  Confrim password
        //
        private TextBox _confirmationPasswordTEXTBOX = new TextBox();                           // Текстовое поле подтверждающее 
        private Label _confrimPasswordLABEL = new Label();                                      // Текст подсказывающий вводимый текст в поле (подтверждение пароля)
        private Label _confirmationPasswordLABEL = new Label();                                 // Текст подсказывающий вводимый текст в поле (Подтверждение пароля)
        //
        //  Secret text
        //
        private TextBox _secretTextTEXTBOX = new TextBox();                                     // Секретный вопрос пользователя
        private Label _secretTextLABEL = new Label();                                           // Ответ на секретный вопрос
        private Label _errorSecrtTextLABEL = new Label();                                       // Ошибка при заполнении секретного вопроса
        //
        //  Secret word
        //
        private TextBox _secretWordTEXTBOX = new TextBox();                                     // Поле ввода ответа на секретный вопрос
        private Label _secretWordLABEL = new Label();                                           // Текст подсказывающий вводимый текст в поле (секретное слово)
        private Label _errorSecretWordLABEL = new Label();                                      // Ошибка ввода ответа на секретный вопрос
        //
        //  Name
        //
        private TextBox _nameTEXTBOX = new TextBox();                                           // Поле ввода имени пользователя
        private Label _nameLABEL = new Label();                                                 // Текст подсказывающий вводимый текст в поле (Имя пользователя)
        private Label _errorNameLABEL = new Label();                                            // Огибка ввода имени пользователя
        //
        // Surname
        //
        private TextBox _surnameTEXTBOX = new TextBox();                                        // Поле ввода фамилии пользователя
        private Label _surnameLABEL = new Label();                                              // Текст подсказывающий вводимый текст в поле (фамилия пользователя)
        private Label _errorSurnameLABEL = new Label();                                         // Ошибка ввода фамилии пользователя
        //
        //  _contentPANEL
        //
        private Panel _contentPANEL = new Panel();                                              // Панель размещения объектов
        //
        // Griphics
        //
        private Graphics graphics;                                                              // Отрисовка элементов на форме
        
        
        //
        //  Второй этап создания пользователя (оформление)
        //
        private Panel _contentSteepsThreePANEL = new Panel();
        private Panel panel1 = new Panel();                                                     // Область размещения настройки цвета программы
        private Button _newColorBUTTON = new Button();                                          // Вывоз окна смены цвета
        private PictureBox _testColorPANEL = new PictureBox();                                  // Демонстрация выбранного цвета приложения
        private Panel _topBorderColorDialogPANEL = new Panel();                                 // Верхняя обводка области размещения цвета программы
        private Panel _leftBorderColorDialogPANEL = new Panel();                                // Левая обводка области размещения цвета программы
        private Panel _bottomBorderColorDialogPANEL = new Panel();                              // Нижняя обводка области размещения цвета программы
        private Panel _rightBorderColorDialogPANEL = new Panel();                               // Правая обводка области размещения цвета программы
        private FlowLayoutPanel _positionCheckedBoxSteepThreeFLP = new FlowLayoutPanel();       // Набор CheckBox 
        // CheckBox звукового эффекта
        private FlowLayoutPanel _checkedScrollSoundEffectFLP = new FlowLayoutPanel();           // Область размещения CkeckBox
        private Label _soundEffectsLABEL = new Label();                                         // Надпись звукового эффекта
        private Panel _boxCheckSoundEffectPANEL = new Panel();                                  // Отрисовка элемента CheckBox звукового эффекта
        private PictureBox _pointConditionCheckSoundEffectPICTUREBOX = new PictureBox();        // Ползунок оповещающий включение или отключение функции
        private Label _conditionCheckSoundEffectLABEL = new Label();                            // Надпись оповещающая состояние ползунка
        // CheckBox повышенного звукового эффекта
        private FlowLayoutPanel _checkedScroMaxVolumeFLP = new FlowLayoutPanel();               // Область размещения CkeckBox повышенного звукового эффекта
        private Label _maxVolumeLABEL = new Label();                                            // Надпись повышенного звукового эффекта
        private Panel _boxCheckMaxVolumePANEL = new Panel();                                    // Отрисовка элемента CheckBox повышенного звукового эффекта
        private PictureBox _pointConditionCheckMaxVolumePICTUREBOX = new PictureBox();          // Ползунок оповещающий состояния CheckBox
        private Label _conditionCheckMaxVolumeLABEL = new Label();                              // Надпись оповещающая состояние CheckBox
        // CheckBox проблемы со зрением
        private FlowLayoutPanel _checkedScrollEyeProblemFLP = new FlowLayoutPanel();            // Область размещения CkeckBox повышенного звукового эффекта
        private Label _eyeProblemLABEL = new Label();                                           // Надпись повышенного звукового эффекта
        private Panel _boxCheckEyeProblemPANEL = new Panel();                                   // Отрисовка элемента CheckBox повышенного звукового эффекта
        private PictureBox _pointConditionCheckEyeProblemPICTUREBOX = new PictureBox();         // Ползунок оповещающий состояния CheckBox
        private Label _conditionCheckEyeProblemLABEL = new Label();                             // Надпись оповещающая состояние CheckBox
        private System.Windows.Forms.Timer _effectAppearanceMaxVolumeTIMER = new System.Windows.Forms.Timer();                            // Эффект появления CheckBox (повышенный звук)
        private FontDialog fontDialog1 = new FontDialog();                                      // Диалоговое окно отвечающее за шрифт 
        private ColorDialog colorDialog1 = new ColorDialog();                                   // Диалоговое окно настройки цвета программы
        private Button _settingFontBUTTON = new Button();                                       // Кнопка открывающая настройки шрифта
        // Анимация
        private System.Windows.Forms.Timer _soundEffectTICK = new System.Windows.Forms.Timer();                                           // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _maxVolumeTIMER = new System.Windows.Forms.Timer();                                            // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _eyeProblemTIMER = new System.Windows.Forms.Timer();                                           // Эффект перемещения ползунка
        // Переменные
        private int _heightMaxVolume;                                                           // Высота формы максимального звукового оповещения
        private bool _onSoundEffect = false;                                                    // Состояние ползунка звукового эффекта
        private bool _onMaxVolume = false;                                                      // Состояние ползунка максимального звукового эффекта
        private bool _onEyeProblem = false;                                                     // Состояние ползунка проблем с зрением
        private bool _closeMaxVolume = false;
        private int radius = 20;                                                                // Градус закругления формы
        private Color _offColor = Color.Red;                                                    // Цвет отвечающий за отображение не ативный ползунок


        //
        //  Третий этап создания пользователя (ограничение времени)
        //
        // Ограничение работы компьютера по дням недели
        private Panel _calendarPANEL = new Panel();                                                             // Панель размещения настройки времени по дням недели
        private FlowLayoutPanel _timeTableFLOWLAYOUTPANEL = new FlowLayoutPanel();                              // Текстовое поле указывающее диапозон времени              
        private FlowLayoutPanel _timeLabelFLOWLAYOUTPANEL = new FlowLayoutPanel();                              // Панель размещения временных промежутков
        private FlowLayoutPanel _weekdayLabelFLOWLAYOUTPANEL = new FlowLayoutPanel();                           // Текстовое поле обозначающее день недели
        private Panel _contentFourSteepFLP = new Panel();                                   // Область размещения всех элементов
        // CheckBox блокировка аккаунта
        private FlowLayoutPanel _checkedScrollBlockAccountFLP = new FlowLayoutPanel();                          // Область размещения CkeckBox
        private Label _BloclAccountLABEL = new Label();                                                         // Надпись CheckBox
        private Panel _boxCheckBloclAccountPANEL = new Panel();                                                 // Отрисовка элемента CheckBox
        private PictureBox _pointConditionCheckBloclAccountPICTUREBOX = new PictureBox();                       // Ползунок указывающий состояние CheckBox
        private Label _conditionCheckBloclAccountLABEL = new Label();                                           // Текст подсказывающий состояние CheckBox
        // CheckBox оповещение о истичении времени
        private FlowLayoutPanel _checkedScrollnotificationTheExpirationOfTheTimeFLP = new FlowLayoutPanel();    // Область размещения CkeckBox
        private Label _notificationTheExpirationOfTheTimeLABEL = new Label();                                   // Надпись CheckBox
        private Panel _boxChecknotificationTheExpirationOfTheTimePANEL = new Panel();                           // Отрисовка элемента CheckBox
        private PictureBox _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX = new PictureBox(); // Ползунок указывающий состояние CheckBox
        private Label _conditionChecknotificationTheExpirationOfTheTimeLABEL = new Label();                     // Текст подсказывающий состояние CheckBox
        // ComboBox бездействие приложения
        private Label _incationLABEL = new Label();                                                             // Надпись оповещающая значение ComboBox
        private ComboBox _incationCOMBOBOX = new ComboBox();                                                    // Список допустимого бездействия пользователя
        // CheckBox оповещающий о ограничении времени
        private FlowLayoutPanel _checkedScrollTimeLimitPerDayFLP = new FlowLayoutPanel();                       // Область размещения CkeckBox
        private Label _timeLimitPerDayLABEL = new Label();                                                      // Надпись CheckBox
        private Panel _boxCheckTimeLimitPerDayPANEL = new Panel();                                              // Отрисовка элемента CheckBox
        private PictureBox _pointConditionCheckTimeLimitPerDayPICTUREBOX = new PictureBox();                    // Ползунок указывающий состояние CheckBox
        private Label _conditionCheckTimeLimitPerDayLABEL = new Label();                                        // Текст подсказывающий состояние CheckBox
        // Панель настройки ограничения времени по минутам
        private FlowLayoutPanel LimitForTheDayFLP = new FlowLayoutPanel();                                      // Панель размещения элементов ограничения времени по минутам
        private Label _timePerDayLABEL = new Label();                                                           // Текст подсказывающий значение элементов 
        private TextBox _hoursTEXTBOX = new TextBox();                                                          // Поле ввода времени (часы)
        private Label _shortHourLABEL = new Label();                                                            // Текстовый элемент расшифровывающий текстовое поле (_hoursTEXTBOX)
        private TextBox _minutesTEXTBOX = new TextBox();                                                        // Поле ввода времени (минуты)
        private Label _shortMinuteTEXTBOX = new Label();                                                        // Текстовый элемент расшифровывающий текстовое поле (_hoursTEXTBOX)
        // Анимация
        private System.Windows.Forms.Timer _blockAccountTIMER = new System.Windows.Forms.Timer();                                                         // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _notificationTheExpirationOfTheTimeTIMER = new System.Windows.Forms.Timer();                                   // Эффект перемещения ползунка
        private System.Windows.Forms.Timer timeLimitPerDayTIMER = new System.Windows.Forms.Timer();                                                       // Эффект перемещения ползунка
        // Переменные
        private List<string> _mondayHold = new List<string>();                                                  // Понедельник
        private List<string> _tuesdayHold = new List<string>();                                                 // Вторник
        private List<string> _environsHold = new List<string>();                                                // Среда
        private List<string> _thursdayHold = new List<string>();                                                // Четверг
        private List<string> _fridayHold = new List<string>();                                                  // Пятница
        private List<string> _saturdayHold = new List<string>();                                                // Суббота
        private List<string> _sundayHold = new List<string>();                                                  // Воскресенье



        private List<string> _date = new List<string>();                                                        // Общий заблокированный список по дням недели
        private List<Control> _elementsDate = new List<Control>();                                              // Данные созданных кнопок и временной шкалы
        private bool _onBlockAccount = false;                                                                   // Состояние CheckBox
        private bool _onNotificationTheExpirationOfTheTime = false;                                             // Состояние CheckBox
        private bool _onTimeLimitPerDay = false;                                                                // Состояние CheckBox
        private Color _borderColor = Color.FromArgb(59, 46, 117);                                               // Цвет обводки 
        private Color _mouseOverBackColor = Color.FromArgb(111, 101, 158);                                      // Цвет изменения цвета при наведении мышкой на кнопку
        //
        //  Пятый этап - запрет на полноценное использование пк
        //
        private FlowLayoutPanel _controlPanelFLP = new FlowLayoutPanel();                                       // Форма отображения пятого этапа    
        // Блокировка сайтов
        private Label _blockUrlLABEL = new Label();                                                             // Надпись подсказывающая, что данная форма блокирует сайты
        private TextBox _blockUrlTEXTBOX = new TextBox();                                                       // Полле вода адреса сайта
        private Button _blockWebBUTTON = new Button();                                                          // Кнопка отправки запроса на проверку ввода и блокировку сайта
        private Panel _contetntBlockUrlPANEL = new Panel();                                                     // Форма блокировки сайтов
        private Panel _backPanelBlockUrlPANEL = new Panel();                                                    // Панель размещения управлением формой блокировки сайтов
        private FlowLayoutPanel _listBlockUrlFLP = new FlowLayoutPanel();                                       // Список всех заблокированных сайтов
        private Panel _bottomBorderBlockUrlPANEL = new Panel();                                                 // Обводка формы внизу
        private Panel _rightBorderBlockUrlPANEL = new Panel();                                                  // Обводка формы справа
        private Panel _leftBorderBlockUrlPANEL = new Panel();                                                   // Обводка формы слева
        // Блокировка приложений
        private Label _nameBlockProgramLABEL = new Label();                                                     // Надпись подсказывающая, что данная форма блокирует приложения
        private Panel _nameBlockProgramPANEL = new Panel();                                                     // Форма блокировки приложений
        private Button _blockProgramBUTTON = new Button();                                                      // Кнопка открытия проводника
        private Panel _backPanelBlockProgramPANEL = new Panel();                                                // Панель размещения управления формой блокировки приложений
        private FlowLayoutPanel _listBlockProgramFLP = new FlowLayoutPanel();                                   // Список заблокированных приложений
        private Panel _topBorderBlockProgram = new Panel();                                                     // Обводка формы сверху
        private Panel _leftBorderBlockProgram = new Panel();                                                    // Обводка формы слева
        private Panel _rightBorderBlockProgram = new Panel();                                                   // Обводка формы справа
        private Panel _bottomBorderBlockProgram = new Panel();                                                  // Обводка формы снизу
        private OpenFileDialog openFileDialog = new OpenFileDialog();                                           // Проводник
        private Panel _panelCheckBoxSpyModePANEL = new Panel();                                                 // Панель размещения всех CheckBox
        //  CheckBox - Запись видео
        private Label _recordVideoLABEL = new Label();                                                          // Надпись CheckBox
        private Panel _boxCheckRecordVideoPANEL = new Panel();                                                  // Отрисовка элемента CheckBox
        private PictureBox _pointConditionCheckRecordVideoPICTUREBOX = new PictureBox();                        // Ползунок указывающий состояние CheckBox
        private Label _conditionCheckRecordVideoLABEL = new Label();                                            // Текст подсказывающий состояние CheckBox
        private FlowLayoutPanel _checkedScrollRecordVideoFLP = new FlowLayoutPanel();                           // Область размещения CkeckBox
        // CheckBox - Запись аудио
        private Label _conditionCheckRecordAudioLABEL = new Label();                                            // Текст подсказывающий состояние CheckBox
        private Label _recordAudioLABEL = new Label();                                                          // Надпись CheckBox
        private Panel _boxCheckRecordAudioPANEL = new Panel();                                                  // Отрисовка элемента CheckBox
        private PictureBox _pointConditionCheckRecordAudioPICTUREBOX = new PictureBox();                        // Ползунок указывающий состояние CheckBox
        private FlowLayoutPanel _checkedScrollRecordAudioFLP = new FlowLayoutPanel();                           // Область размещения CkeckBox
        // CheckBox - Снимок экрана
        private FlowLayoutPanel _checkedScrollScreenShootFLP = new FlowLayoutPanel();                           // Область размещения CkeckBox
        private Label _screenShootLABEL = new Label();                                                          // Надпись CheckBox
        private Panel _boxCheckScreenShootPANEL = new Panel();                                                  // Отрисовка элемента CheckBox
        private PictureBox _pointConditionCheckScreenShootPICTUREBOX = new PictureBox();                        // Ползунок указывающий состояние CheckBox
        private Label _conditionCheckScreenShootLABEL = new Label();                                            // Текст подсказывающий состояние CheckBox
        // Всплывающая детальная настройка снимков экрана
        private NumericUpDown _snapshotFrequencyNUMUPDOWN = new NumericUpDown();                                // Частота обновления создания скриншотов
        private Label _snapshotFrequencyLABEL = new Label();                                                    // Информирующая надпись о значении NumericUpDown
        private Label _snapshotFrequencyInSecondsLABEL = new Label();                                           // Надпись указывающая еденицы измерения NumericUpDown
        private Panel _snapshotFrequencyPANEL = new Panel();                                                    // Панель детальных настроек
        //  Анимация
        private System.Windows.Forms.Timer _effectOpenAndCloseSnapFreqTIMER = new System.Windows.Forms.Timer();                                           // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _recordVideoTIMER = new System.Windows.Forms.Timer();                                                          // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _recordAudioTIMER = new System.Windows.Forms.Timer();                                                          // Эффект перемещения ползунка
        private System.Windows.Forms.Timer _screenShootTIMER = new System.Windows.Forms.Timer();                                                          // Эффект перемещения ползунка
        // Переменные
        private List<Control> _blockUrl = new List<Control>();           // Панели заблокированных сайтов
        private List<string> _url = new List<string>();                  // Список всех заблокированных сайтов с локальным IP
        private List<Control> _blockPrograms = new List<Control>();      // Панели заблокированных программ
        private List<string> _program = new List<string>();              // Список заблокированных приложений
        private bool _onRecordVideo = false;
        private bool _onRecordAudio = false;
        private bool _onScreenShoot = false;
        private bool _onEffectOpenAndCloseSnapFreq = false;
        private int _heightSnapshotFrequency = 53;

        private ToolTip _returnTOOLTIP = new ToolTip();
        private ToolTip _nextTOOLTIP = new ToolTip();

        //
        //  Завершительный этап создания пользователя
        //
        private Label _createUserFineStepLABEL = new Label();
        private Panel _fineStepPANEL = new Panel();
        private Label _createNewUserFineStepLABEL = new Label();
        //
        //  Сохраненные данные пользователя
        //
        struct TempUser
        {
            //  Конфиденциальные данные
            public string Login { get; set; }                       // Логин пользователя
            public string Password { get; set; }                    // Пароль пользователя
            public string SecretText { get; set; }                  // Секретный вопрос пользователя
            public string SecretWord { get; set; }                  // Ответ на секретный вопрос
            public string Name { get; set; }                        // Имя пользователя
            public string Surname { get; set; }                     // Фамилия пользователя
            // Внешний вид программы
            public string FontFamily { get; set; }                  // Шрифт пользователя
            public string StyleFontFamily { get; set; }             // Начертание текста
            public int SizeFont { get; set; }                       // Размер шрифта
            public string ColorProgram { get; set; }                // Основной цвет программы
            public int SoundEffect { get; set; }                   // Звуковые эффекты приложения
            public int MaxSoundEffect { get; set; }                // Увеличенная громкость приложения
            public int VersionForTheVisuallyImpaired { get; set; } // Версия для слабовидящих
            public int Block { get; set; }                         // Блокировка аккаунта
            public int NotificetionExperiationTime { get; set; }   // Оповещение о истечении времени
            public int Inaction { get; set; }                       // Бездействие
            public int TimeConstraint { get; set; }                 // Ограничение по времени
            public int ByDay { get; set; }                         // Ограничение пользования пк по дням
            public int BlockProgram { get; set; }
            public int BlockUrl { get; set; }
            public int RecordVideo { get; set; }
            public int RecordAudio { get; set; }
            public int ScreenShot { get; set; }
            public int TimeSnapshot { get; set; }                   // Частота создания снимков экрана
            public int Access { get; set; }
        }
        public NewUser()
        {
            StartSettings();
        }

        private void StartSettings()
        {
            InitializeComponent();
            _namePageLABEL.Text = "Создание нового пользователя";
            //_childAccountBUTTON

            //
            //  Настройка начальных кнопок 
            //
            Form1.NewStyleButton(_childAccountBUTTON);
            Form1.NewStyleButton(_parantAccountBUTTON);

            Size size = TextRenderer.MeasureText(_childAccountBUTTON.Text, StyleWindows._mainFont);
            size = TextRenderer.MeasureText(_parantAccountBUTTON.Text, StyleWindows._mainFont);
            int widthContentButton = _childAccountBUTTON.Width - 17;
            _childAccountBUTTON.Size = new Size(_childAccountBUTTON.Width, (size.Width > widthContentButton) ? size.Height * (size.Width / widthContentButton + 2) : _childAccountBUTTON.Height);

            size = TextRenderer.MeasureText(_parantAccountBUTTON.Text, StyleWindows._mainFont);
            _parantAccountBUTTON.Size = new Size(_parantAccountBUTTON.Width, (size.Width > widthContentButton) ? size.Height * (size.Width / widthContentButton + 2) : _parantAccountBUTTON.Height);



            // _returnTOOLTIP
            StyleWindows.SettingToolTip(_returnTOOLTIP, _returnBUTTON, "Назад", "Переместиться на шаг назад\n(Все введенные данныхе сохранятся)");
            // _nextTOOLTIP
            StyleWindows.SettingToolTip(_nextTOOLTIP, _nextBUTTON, "Вперед", "Перейти к следующиму этапу\n регистрации пользователя");
            _stepsPageButtonsFLP.Location = new Point(this.Width - _stepsPageButtonsFLP.Width - 20, _panelStagePANEL.Height / 2 - _stepsPageButtonsFLP.Height / 2);


            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 15, _closeWindowPICTUREBOX.Location.Y);
            //
            //  Выравнивание элементов
            //
            _nameSteepLABEL.Text = _steepOne;
            LocationNameSteep();
            _panelSwitchAccountPANEL.Location = new Point(_contentStepsPANEL.Width / 2 - _panelSwitchAccountPANEL.Width / 2, _contentStepsPANEL.Height / 2 - _panelSwitchAccountPANEL.Height / 2);
        }
        private void ViewCountStep(int isChild)
        {
            if (isChild == 1)
            {
                _steepOnePICTUREBOX.Visible = true;
                _lineOneNewSteepPICTUREBOX.Visible = true;
                _steepTwoPICTUREBOX.Visible = true;
                _lineTwoNewSteepPICTUREBOX.Visible = true;
                _steepThreePICTUREBOX.Visible = true;
                _lineThreeNewSteepPICTUREBOX.Visible = true;
                _steepFourPICTUREBOX.Visible = true;
                _stageFLP.Width = 178;
            }
            else if (isChild == 0)
            {
                _steepOnePICTUREBOX.Visible = true;
                _lineOneNewSteepPICTUREBOX.Visible = true;
                _steepTwoPICTUREBOX.Visible = true;
                _stageFLP.Width = 85;
            }
            else if(isChild == -1)
            {
                _steepOnePICTUREBOX.Visible = false;
                _lineOneNewSteepPICTUREBOX.Visible = false;
                _steepTwoPICTUREBOX.Visible = false;
                _lineTwoNewSteepPICTUREBOX.Visible = false;
                _steepThreePICTUREBOX.Visible = false;
                _lineThreeNewSteepPICTUREBOX.Visible = false;
                _steepFourPICTUREBOX.Visible = false;
                _stageFLP.Width = 178;
            }
        }
        //
        //  Выравнивание наименования этапа
        //
        private void LocationNameSteep() => _nameSteepLABEL.Location = new Point(_panelNameSteepPANEL.Width / 2 - _nameSteepLABEL.Width / 2, _panelNameSteepPANEL.Height / 2 - _nameSteepLABEL.Height / 2);

        #region Оформление создания пользователя для ребенка
        private void CreateStyleSecretDataUser()
        {
            int padding = 10;
            int marginLeft = 3;
            const string error = "Данное поле содержит ошибку";


            #region Login
            // 
            // _loginTEXTBOX
            // 
            _loginTEXTBOX.BackColor = Color.White;
            _loginTEXTBOX.BorderStyle = BorderStyle.None;
            _loginTEXTBOX.Font = StyleWindows._mainFont;
            _loginTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, padding);
            _loginTEXTBOX.MaxLength = 30;
            _loginTEXTBOX.Name = "_loginTEXTBOX";
            _loginTEXTBOX.Size = new Size(180, 20);
            _loginTEXTBOX.TabIndex = 1;
            // 
            // _loginLABEL
            // 
            _loginLABEL.BackColor = Color.White;
            _loginLABEL.AutoSize = true;
            _loginLABEL.Font = StyleWindows._mainFont;
            _loginLABEL.Location = new Point(marginLeft, padding);
            _loginLABEL.Name = "_loginLABEL";
            _loginLABEL.TabIndex = 0;
            _loginLABEL.Text = "Логин:";
            // 
            // _errorLoginLABEL
            // 
            _errorLoginLABEL.AutoSize = true;
            _errorLoginLABEL.Font = StyleWindows._mainFont;
            _errorLoginLABEL.ForeColor = Color.Red;
            _errorLoginLABEL.Location = new Point(_loginTEXTBOX.Location.X + _loginTEXTBOX.Width + padding, _loginTEXTBOX.Location.Y);
            _errorLoginLABEL.Name = "_errorLoginLABEL";
            _errorLoginLABEL.TabIndex = 2;
            _errorLoginLABEL.Text = error;
            _errorLoginLABEL.Visible = false;
            #endregion

            #region Password
            // 
            // _passwordTEXTBOX
            // 
            _passwordTEXTBOX.BackColor = System.Drawing.Color.White;
            _passwordTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _passwordTEXTBOX.Font = StyleWindows._mainFont;
            _passwordTEXTBOX.Location = new System.Drawing.Point(_confirmationPasswordTEXTBOX.Location.X, _loginTEXTBOX.Location.Y + _loginTEXTBOX.Height + padding);
            _passwordTEXTBOX.MaxLength = 30;
            _passwordTEXTBOX.Name = "_passwordTEXTBOX";
            _passwordTEXTBOX.Size = new System.Drawing.Size(180, 20);
            _passwordTEXTBOX.TabIndex = 4;
            _passwordTEXTBOX.UseSystemPasswordChar = true;
            // 
            // _passwordLABEL
            // 
            _passwordLABEL.AutoSize = true;
            _passwordLABEL.Font = StyleWindows._mainFont;
            _passwordLABEL.Location = new Point(marginLeft, _passwordTEXTBOX.Location.Y);
            _passwordLABEL.Name = "_passwordLABEL";

            _passwordLABEL.TabIndex = 3;
            _passwordLABEL.Text = "Пароль:";
            // 
            // _errorPasswordLABEL
            // 
            _errorPasswordLABEL.BackColor = Color.White;
            _errorPasswordLABEL.AutoSize = true;
            _errorPasswordLABEL.Font = StyleWindows._mainFont;
            _errorPasswordLABEL.ForeColor = Color.Red;
            _errorPasswordLABEL.Location = new Point(_passwordTEXTBOX.Location.X + _passwordTEXTBOX.Width + padding, _passwordTEXTBOX.Location.Y);
            _errorPasswordLABEL.Name = "_errorPasswordLABEL";
            _errorPasswordLABEL.TabIndex = 18;
            _errorPasswordLABEL.Text = error;
            _errorPasswordLABEL.Visible = false;
            #endregion

            #region confrim password
            // 
            // _confrimPasswordLABEL
            // 
            _confrimPasswordLABEL.AutoSize = true;
            _confrimPasswordLABEL.Font = StyleWindows._mainFont;
            _confrimPasswordLABEL.Location = new Point(marginLeft, 59);
            _confrimPasswordLABEL.Name = "_confrimPasswordLABEL";
            _confrimPasswordLABEL.Size = new Size(172, 19);
            _confrimPasswordLABEL.TabIndex = 5;
            _confrimPasswordLABEL.Text = "Подтверждение пароля:";
            // 
            // _confirmationPasswordTEXTBOX
            // 
            Size size = TextRenderer.MeasureText(_confrimPasswordLABEL.Text, StyleWindows._mainFont);
            _confirmationPasswordTEXTBOX.BackColor = Color.White;
            _confirmationPasswordTEXTBOX.BorderStyle = BorderStyle.None;
            _confirmationPasswordTEXTBOX.Font = StyleWindows._mainFont;
            _confirmationPasswordTEXTBOX.Location = new Point(_confrimPasswordLABEL.Location.X + size.Width + 10, 59);
            _confirmationPasswordTEXTBOX.MaxLength = 30;
            _confirmationPasswordTEXTBOX.Name = "_confirmationPasswordTEXTBOX";
            _confirmationPasswordTEXTBOX.Size = new Size(180, 20);
            _confirmationPasswordTEXTBOX.TabIndex = 6;
            _confirmationPasswordTEXTBOX.UseSystemPasswordChar = true;
            // 
            // _confirmationPasswordLABEL
            // 
            _confirmationPasswordLABEL.BackColor = Color.White;
            _confirmationPasswordLABEL.AutoSize = true;
            _confirmationPasswordLABEL.Font = StyleWindows._mainFont;
            _confirmationPasswordLABEL.ForeColor = Color.Red;
            _confirmationPasswordLABEL.Location = new Point(_confirmationPasswordTEXTBOX.Location.X + _confirmationPasswordTEXTBOX.Width + padding, _confirmationPasswordTEXTBOX.Location.Y);
            _confirmationPasswordLABEL.Name = "_confirmationPasswordLABEL";
            _confirmationPasswordLABEL.TabIndex = 14;
            _confirmationPasswordLABEL.Text = "Пароли не совпадают";
            _confirmationPasswordLABEL.Visible = false;
            #endregion

            _confrimPasswordLABEL.Location = new Point(marginLeft, _passwordTEXTBOX.Location.Y + _passwordTEXTBOX.Height + padding);
            _confirmationPasswordTEXTBOX.Location = new Point(_confrimPasswordLABEL.Location.X + size.Width + padding * 2, _passwordTEXTBOX.Location.Y + _passwordTEXTBOX.Height + padding);
            //  Login
            _loginTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, padding);
            _errorLoginLABEL.Location = new Point(_loginTEXTBOX.Location.X + _loginTEXTBOX.Width + padding, _loginTEXTBOX.Location.Y);
            //  Passsword
            _passwordTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, _loginTEXTBOX.Location.Y + _loginTEXTBOX.Height + padding);
            _errorPasswordLABEL.Location = new Point(_passwordTEXTBOX.Location.X + _passwordTEXTBOX.Width + padding, _passwordTEXTBOX.Location.Y);

            #region Secret text
            // 
            // _secretTextTEXTBOX
            // 
            _secretTextTEXTBOX.BackColor = Color.White;
            _secretTextTEXTBOX.BorderStyle = BorderStyle.None;
            _secretTextTEXTBOX.Font = StyleWindows._mainFont;
            _secretTextTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, _confirmationPasswordTEXTBOX.Location.Y + _confirmationPasswordTEXTBOX.Height + padding);
            _secretTextTEXTBOX.MaxLength = 30;
            _secretTextTEXTBOX.Name = "_secretTextTEXTBOX";
            _secretTextTEXTBOX.Size = new Size(180, 20);
            _secretTextTEXTBOX.TabIndex = 20;
            // 
            // _secretTextLABEL
            // 
            _secretTextLABEL.AutoSize = true;
            _secretTextLABEL.Font = StyleWindows._mainFont;
            _secretTextLABEL.Location = new Point(marginLeft, _secretTextTEXTBOX.Location.Y);
            _secretTextLABEL.Name = "_secretTextLABEL";
            _secretTextLABEL.TabIndex = 19;
            _secretTextLABEL.Text = "Секретный вопрос:";
            // 
            // _errorSecrtTextLABEL
            // 
            _errorSecrtTextLABEL.AutoSize = true;
            _errorSecrtTextLABEL.Font = StyleWindows._mainFont;
            _errorSecrtTextLABEL.ForeColor = Color.Red;
            _errorSecrtTextLABEL.Location = new Point(_secretTextTEXTBOX.Location.X + _secretTextTEXTBOX.Width + padding, _secretTextTEXTBOX.Location.Y);
            _errorSecrtTextLABEL.Name = "_errorSecrtTextLABEL";
            _errorSecrtTextLABEL.TabIndex = 21;
            _errorSecrtTextLABEL.Text = error;
            _errorSecrtTextLABEL.Visible = false;
            #endregion

            #region Secret word
            // 
            // _secretWordTEXTBOX
            // 
            _secretWordTEXTBOX.BackColor = Color.White;
            _secretWordTEXTBOX.BorderStyle = BorderStyle.None;
            _secretWordTEXTBOX.Font = StyleWindows._mainFont;
            _secretWordTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, _secretTextTEXTBOX.Location.Y + _secretTextTEXTBOX.Height + padding);
            _secretWordTEXTBOX.MaxLength = 30;
            _secretWordTEXTBOX.Name = "_secretWordTEXTBOX";
            _secretWordTEXTBOX.Size = new Size(180, 20);
            _secretWordTEXTBOX.TabIndex = 8;
            // 
            // _secretWordLABEL
            // 
            _secretWordLABEL.AutoSize = true;
            _secretWordLABEL.Font = StyleWindows._mainFont;
            _secretWordLABEL.Location = new Point(marginLeft, _secretWordTEXTBOX.Location.Y);
            _secretWordLABEL.Name = "_secretWordLABEL";
            _secretWordLABEL.TabIndex = 7;
            _secretWordLABEL.Text = "Секретное слово:";
            // 
            // _errorSecretWordLABEL
            // 
            _errorSecretWordLABEL.BackColor = Color.White;
            _errorSecretWordLABEL.AutoSize = true;
            _errorSecretWordLABEL.Font = StyleWindows._mainFont;
            _errorSecretWordLABEL.ForeColor = Color.Red;
            _errorSecretWordLABEL.Location = new Point(_secretWordTEXTBOX.Location.X + _secretWordTEXTBOX.Width + padding, _secretWordTEXTBOX.Location.Y);
            _errorSecretWordLABEL.Name = "_errorSecretWordLABEL";
            _errorSecretWordLABEL.TabIndex = 15;
            _errorSecretWordLABEL.Text = error;
            _errorSecretWordLABEL.Visible = false;
            #endregion

            #region Name
            // 
            // _nameTEXTBOX
            // 
            _nameTEXTBOX.BackColor = Color.White;
            _nameTEXTBOX.BorderStyle = BorderStyle.None;
            _nameTEXTBOX.Font = StyleWindows._mainFont;
            _nameTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, _secretWordTEXTBOX.Location.Y + _secretWordTEXTBOX.Height + padding);
            _nameTEXTBOX.MaxLength = 30;
            _nameTEXTBOX.Name = "_nameTEXTBOX";
            _nameTEXTBOX.Size = new Size(180, 20);
            _nameTEXTBOX.TabIndex = 10;
            // 
            // _nameLABEL
            // 
            _nameLABEL.AutoSize = true;
            _nameLABEL.Font = StyleWindows._mainFont;
            _nameLABEL.Location = new Point(marginLeft, _nameTEXTBOX.Location.Y);
            _nameLABEL.Name = "_nameLABEL";
            _nameLABEL.TabIndex = 9;
            _nameLABEL.Text = "Имя:";
            // 
            // _errorNameLABEL
            // 
            _errorNameLABEL.BackColor = Color.White;
            _errorNameLABEL.AutoSize = true;
            _errorNameLABEL.Font = StyleWindows._mainFont;
            _errorNameLABEL.ForeColor = Color.Red;
            _errorNameLABEL.Location = new Point(_nameTEXTBOX.Location.X + _nameTEXTBOX.Width + padding, _nameTEXTBOX.Location.Y);
            _errorNameLABEL.Name = "_errorNameLABEL";
            _errorNameLABEL.TabIndex = 16;
            _errorNameLABEL.Text = error;
            _errorNameLABEL.Visible = false;
            #endregion

            #region Surname
            // 
            // _surnameTEXTBOX
            // 
            _surnameTEXTBOX.BackColor = Color.White;
            _surnameTEXTBOX.BorderStyle = BorderStyle.None;
            _surnameTEXTBOX.Font = StyleWindows._mainFont;
            _surnameTEXTBOX.Location = new Point(_confirmationPasswordTEXTBOX.Location.X, _nameTEXTBOX.Location.Y + _nameTEXTBOX.Height + padding);
            _surnameTEXTBOX.MaxLength = 30;
            _surnameTEXTBOX.Name = "_surnameTEXTBOX";
            _surnameTEXTBOX.Size = new Size(180, 20);
            _surnameTEXTBOX.TabIndex = 12;
            // 
            // _surnameLABEL
            // 
            _surnameLABEL.AutoSize = true;
            _surnameLABEL.Font = StyleWindows._mainFont;
            _surnameLABEL.Location = new Point(marginLeft, _surnameTEXTBOX.Location.Y);
            _surnameLABEL.Name = "_surnameLABEL";
            _surnameLABEL.Size = new Size(73, 19);
            _surnameLABEL.TabIndex = 11;
            _surnameLABEL.Text = "Фамилия:";
            // 
            // _errorSurnameLABEL
            // 
            _errorSurnameLABEL.BackColor = Color.White;
            _errorSurnameLABEL.AutoSize = true;
            _errorSurnameLABEL.Font = StyleWindows._mainFont;
            _errorSurnameLABEL.ForeColor = Color.Red;
            _errorSurnameLABEL.Location = new Point(_surnameTEXTBOX.Location.X + _surnameTEXTBOX.Width + padding, _surnameTEXTBOX.Location.Y);
            _errorSurnameLABEL.Name = "_errorSurnameLABEL";
            _errorSurnameLABEL.TabIndex = 17;
            _errorSurnameLABEL.Text = error;
            _errorSurnameLABEL.Visible = false;
            #endregion

            // 
            // _contentPANEL
            // 
            this._contentPANEL.BackColor = System.Drawing.Color.White;
            this._contentPANEL.Controls.Add(this._errorSecrtTextLABEL);
            this._contentPANEL.Controls.Add(this._secretTextLABEL);
            this._contentPANEL.Controls.Add(this._secretTextTEXTBOX);
            this._contentPANEL.Controls.Add(this._errorPasswordLABEL);
            this._contentPANEL.Controls.Add(this._errorSurnameLABEL);
            this._contentPANEL.Controls.Add(this._errorNameLABEL);
            this._contentPANEL.Controls.Add(this._errorSecretWordLABEL);
            this._contentPANEL.Controls.Add(this._confirmationPasswordLABEL);
            this._contentPANEL.Controls.Add(this._surnameLABEL);
            this._contentPANEL.Controls.Add(this._surnameTEXTBOX);
            this._contentPANEL.Controls.Add(this._nameLABEL);
            this._contentPANEL.Controls.Add(this._nameTEXTBOX);
            this._contentPANEL.Controls.Add(this._secretWordLABEL);
            this._contentPANEL.Controls.Add(this._secretWordTEXTBOX);
            this._contentPANEL.Controls.Add(this._confrimPasswordLABEL);
            this._contentPANEL.Controls.Add(this._confirmationPasswordTEXTBOX);
            this._contentPANEL.Controls.Add(this._passwordLABEL);
            this._contentPANEL.Controls.Add(this._passwordTEXTBOX);
            this._contentPANEL.Controls.Add(this._errorLoginLABEL);
            this._contentPANEL.Controls.Add(this._loginLABEL);
            this._contentPANEL.Controls.Add(this._loginTEXTBOX);
            this._contentPANEL.Dock = DockStyle.Fill;
            this._contentPANEL.AutoScroll = true; 
            this._contentPANEL.Location = new System.Drawing.Point(0, 5);
            this._contentPANEL.Name = "_contentPANEL";
            this._contentPANEL.TabIndex = 2;
            this._contentPANEL.Paint += _contentPANEL_Paint;

        }
        //
        //  Получение данных элемента
        //
        private int[] DefinitionCoordinateAndSizeControl(Control var)
        {
            int x = var.Location.X;
            int y = var.Location.Y;
            int width = var.Width;
            int height = var.Height;
            return new int[] { x, y, width, height };
        }
        //
        //  Отрисовка элементов на панели
        //
        private void _contentPANEL_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            if (_steeps == 1)
            {
                ClearPanelBrash(sender, e);
                Pen pen = new Pen(_mainColor, 3.0f);
                /*
                    value[0] - x
                    value[1] - y
                    value[2] - width
                    value[3] - height
                 */


                //  _loginTEXTBOX
                var value = DefinitionCoordinateAndSizeControl(_loginTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _passwordTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_passwordTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _confirmationPasswordTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_confirmationPasswordTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _secretTextTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_secretTextTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _secretWordTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_secretWordTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _nameTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_nameTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
                // _surnameTEXTBOX
                value = DefinitionCoordinateAndSizeControl(_surnameTEXTBOX);
                graphics.DrawLine(pen, value[0] - 2, value[1] + value[3], value[0] + value[2] + 2, value[1] + value[3]);
            }
            else
            {
                if (_steeps == 2)
                {
                    ClearPanelBrash(sender, e);
                }
            }
        }
        #endregion

            
        private void ClearPanelBrash(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            SolidBrush solidBrush = new SolidBrush(Color.White);
            graphics.FillRectangle(solidBrush, 0, 0, _contentPANEL.Width, _contentPANEL.Height);
        }

        private void NewPositionStageFLP() => _stageFLP.Location = new Point(this.Width / 2 - _stageFLP.Width / 2, _stageFLP.Height / 2 - _stageFLP.Height / 2);
        private void _childAccountBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            TEMPUSER.Access = 0;
            ViewCountStep(1);
            _steeps++;
            NewPositionStageFLP();
            this.Refresh();
            _nameSteepLABEL.Text = _steepTwo;
            LocationNameSteep();
            _contentStepsPANEL.Controls.Remove(_panelSwitchAccountPANEL);
            CreateStyleSecretDataUser();
            _contentStepsPANEL.Controls.Add(_contentPANEL);
            // Размещение кнопок управления
            //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentPANEL.Location.Y + _contentPANEL.Height + 10);
            _stepsPageButtonsFLP.Visible = true;
           
        }

        //
        // Проверка длины логина и на пустое значение
        //
        private bool CheckTheLengthOfLogin(string valueString)
        {
            if (valueString.Length > _minimalLengthInDataTextbox)
            {
                // Проверка на специальные символы
                foreach (char value in valueString)
                {
                    foreach (char letter in _errorLetter)
                    {
                        if (value == letter)
                        {
                            _errorLoginLABEL.Visible = true;
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                _errorLoginLABEL.Visible = true;
                return true;
            }
        }
        //
        //  Проверка соответствие двух паролей
        //
        public bool PassComplianceCheck(string pass, string confPass)
        {
            if (confPass != pass)
            {
                _confirmationPasswordLABEL.Visible = true;
                return true;
            }
            else
            {
                _confirmationPasswordLABEL.Visible = false;
                return false;
            }
        }
        //
        //  Проверка длины пароля 
        //
        public bool CheckTheLengthOfPasswordAndAcceptPass(string pass)
        {
            if (pass.Length < _minimalLengthInDataTextbox)
            {
                _errorPasswordLABEL.Visible = true;
                return  true;
            }
            else
            {
                _errorPasswordLABEL.Visible = false;
                return false;
            }
        }
        //
        //  Проверка ввода секретного вопроса
        //
        private bool SecurityQuestionCheck(string value)
        {
            if (value == "")
            {
                _errorSecrtTextLABEL.Visible = true;
                return true;
            }
            else
            {
                _errorSecrtTextLABEL.Visible = false;
                return false;
            }
        }
        //
        //  Проверка ввода ответа на секретный вопрос
        //
        private bool VerificationAnswerSecurityQuestion(string value)
        {
            if (value == "")
            {
                _errorSecretWordLABEL.Visible = true;
                return true;
            }
            else
            {
                _errorSecretWordLABEL.Visible = false;
                return false;
            }
        }
        private bool CheckedNameInput(string value)
        {
            if (value != "")
            {
                if (CheckTrueReadText(value))
                {
                    _errorNameLABEL.Visible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _errorNameLABEL.Visible = true;
                return true;
            }
        }

        private bool CheckedSurnameInput(string value)
        {
            if (value != "")
            {
                if (CheckTrueReadText(value))
                {
                    _errorSurnameLABEL.Visible = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _errorSurnameLABEL.Visible = true;
                return true;
            }
        }

        private void ApplyingСhangesStepThree()
        {
            _contentStepsPANEL.Controls.Remove(_contentFourSteepFLP);
            _steeps++;
            _nameSteepLABEL.Text = _steepFive;
            LocationNameSteep();

            StyleSpyMode();
        }
        private void ClearingDataInStruct()
        {
            TEMPUSER.Login = null;
            TEMPUSER.Password = null;
            TEMPUSER.SecretText = null;
            TEMPUSER.SecretWord = null;
            TEMPUSER.Name = null;
            TEMPUSER.Surname = null;
            TEMPUSER.FontFamily = null;
            TEMPUSER.StyleFontFamily = null;
            TEMPUSER.SizeFont = 0;
            TEMPUSER.ColorProgram = null;
            TEMPUSER.SoundEffect = 0;
            TEMPUSER.MaxSoundEffect = 0;
            TEMPUSER.VersionForTheVisuallyImpaired = 0;
            TEMPUSER.Block = 0;
            TEMPUSER.NotificetionExperiationTime = 0;
            TEMPUSER.Inaction = 0;
            TEMPUSER.TimeConstraint = 0;
            TEMPUSER.ByDay = 0;
            TEMPUSER.BlockProgram = 0;
            TEMPUSER.BlockUrl = 0;
            TEMPUSER.RecordVideo = 0;
            TEMPUSER.RecordAudio = 0;
            TEMPUSER.ScreenShot = 0;
            TEMPUSER.TimeSnapshot = 0;
            TEMPUSER.Access = 0;
    }
        private void _returnBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (TEMPUSER.Access == 0)
            {
                if (_steeps == 1)
                {
                    //
                    //  Выбор создаваемого аккаунта
                    //
                    // Скрытие прошлых элементов
                    _contentStepsPANEL.Controls.Remove(_contentPANEL);
                    ViewCountStep(-1);
                    // 
                    ClearingDataInStruct();

                    _contentStepsPANEL.Controls.Add(_panelSwitchAccountPANEL);
                    this.Refresh();
                    _nameSteepLABEL.Text = _steepOne;
                    LocationNameSteep();
                }
                else if (_steeps == 2)
                {
                    //
                    //  Крнфиденциальные данные
                    //
                    _contentStepsPANEL.Controls.Remove(_contentSteepsThreePANEL);
                    this.Refresh();
                    _contentStepsPANEL.Controls.Add(_contentPANEL);
                    _nameSteepLABEL.Text = _steepTwo;
                    LocationNameSteep();
                }
                else if (_steeps == 3)
                {
                    _contentStepsPANEL.Controls.Remove(_contentFourSteepFLP);
                    this.Refresh();
                    _contentStepsPANEL.Controls.Add(_contentSteepsThreePANEL);
                    _nameSteepLABEL.Text = _steepThree;
                    LocationNameSteep();
                }
                else if (_steeps == 4)
                {
                    _contentStepsPANEL.Controls.Remove(_controlPanelFLP);
                    this.Refresh();
                    _contentStepsPANEL.Controls.Add(_contentSteepsThreePANEL);
                    _nameSteepLABEL.Text = _steepFour;
                    LocationNameSteep();
                }
            }
            
            _steeps--;
        }

        private void NewPositionStage()
        {
            //  Проверка введенных личных данных
            if (_steeps == 1)
            {
                //
                //  Проверка на вверный вод конфиденциальных данных
                //
                List<bool> error = new List<bool>();
                _contentStepsPANEL.Enabled = false;
                // Отправка sql запроса на уникальный login
                // Проверка длины логина и на пустое значение
                error.Add(CheckTheLengthOfLogin(_loginTEXTBOX.Text));
                //  Проверка пароля на пустое значение
                error.Add(CheckTheLengthOfPasswordAndAcceptPass(_passwordTEXTBOX.Text));
                //  Проверка паролей на соответствие
                error.Add(PassComplianceCheck(_passwordTEXTBOX.Text, _confirmationPasswordTEXTBOX.Text));
                //  Проверка на ввод секретного вопроса
                error.Add(SecurityQuestionCheck(_secretTextTEXTBOX.Text));
                //  Проверка на ввод ответа на секретный вопрос
                error.Add(VerificationAnswerSecurityQuestion(_secretWordTEXTBOX.Text));
                //  Проверка ввода имени
                error.Add(CheckedNameInput(_nameTEXTBOX.Text));
                //  Проверка ввода фамилии
                error.Add(CheckedSurnameInput(_surnameTEXTBOX.Text));
                //
                //  Сохранение данных
                //
                bool notError = true;
                foreach (bool value in error)
                {
                    if (value) notError = false;
                }

                if (notError)
                {
                    TEMPUSER.Login = _loginTEXTBOX.Text;
                    TEMPUSER.Password = _passwordTEXTBOX.Text;
                    TEMPUSER.SecretText = _secretTextTEXTBOX.Text;
                    TEMPUSER.SecretWord = _secretWordTEXTBOX.Text;
                    TEMPUSER.Name = _nameTEXTBOX.Text;
                    TEMPUSER.Surname = _surnameTEXTBOX.Text;
                    error.Clear();
                    //
                    //  Удаление элементов с панели элементов
                    //
                    _contentPANEL.Refresh();
                    if (TEMPUSER.Access == 0)
                    {
                        CreateElementsAppearanceUser();

                        _nameSteepLABEL.Text = _steepThree;
                        LocationNameSteep();
                    }
                    else
                    {

                    }
                    _steeps++;
                    _contentStepsPANEL.Controls.Remove(_contentPANEL);
                }

                _contentStepsPANEL.Enabled = true;
            }// Проверка конфиденциальных данных
            else
            {
                if (TEMPUSER.Access == 0)
                {
                    // Проверка указанных данных внешнего вида программы
                    if (_steeps == 2)
                    {
                        // Сохранение данных
                        TEMPUSER.FontFamily = fontDialog1.Font.Name;
                        TEMPUSER.StyleFontFamily = fontDialog1.Font.Style.ToString();
                        TEMPUSER.SizeFont = Convert.ToInt32(fontDialog1.Font.Size);
                        TEMPUSER.ColorProgram = ColorTranslator.ToHtml(colorDialog1.Color);
                        TEMPUSER.SoundEffect = (_onSoundEffect) ? 1 : 0;
                        TEMPUSER.MaxSoundEffect = (_onSoundEffect && _onMaxVolume) ? 1 : 0;
                        TEMPUSER.VersionForTheVisuallyImpaired = (_onEyeProblem) ? 1 : 0;
                        // Убирается панель размещения настроек внешнего вида программы
                        _contentStepsPANEL.Controls.Remove(_contentSteepsThreePANEL);
                        // Инициализация объектов следующего этапа
                        CreateAppearanceFourSteep();
                        _contentStepsPANEL.Controls.Add(_contentFourSteepFLP);
                        _steeps++;
                        _nameSteepLABEL.Text = _steepFour;
                        LocationNameSteep();
                    }// Внешний вид программы
                    else
                    {
                        // Проверка указанных настроек ограничения времени пользователя
                        if (_steeps == 3)
                        {

                            // Проверка ограничения времени на день
                            if (_onTimeLimitPerDay)
                            {
                                //
                                //  Сохранение данных
                                //
                                TEMPUSER.Block = (_onBlockAccount) ? 1 : 0;
                                TEMPUSER.NotificetionExperiationTime = (_onNotificationTheExpirationOfTheTime) ? 1 : 0;
                                TEMPUSER.Inaction = _incationCOMBOBOX.SelectedIndex;
                                TEMPUSER.TimeConstraint = 0;
                                TEMPUSER.ByDay = 1;

                                ApplyingСhangesStepThree();
                            }
                            else
                            {
                                if (_hoursTEXTBOX.Text.Length != 0 || _minutesTEXTBOX.Text.Length != 0)
                                {
                                    int test;
                                    if (int.TryParse(_hoursTEXTBOX.Text, out test) && int.TryParse(_minutesTEXTBOX.Text, out test))
                                    {
                                        if (Convert.ToInt32(_hoursTEXTBOX.Text) >= 0 && Convert.ToInt32(_minutesTEXTBOX.Text) < 60 && Convert.ToInt32(_minutesTEXTBOX.Text) >= 0)
                                        {
                                            //
                                            //  Сохранение данных
                                            //
                                            TEMPUSER.Block = (_onBlockAccount) ? 1 : 0;
                                            TEMPUSER.NotificetionExperiationTime = (_onNotificationTheExpirationOfTheTime) ? 1 : 0;
                                            TEMPUSER.Inaction = _incationCOMBOBOX.SelectedIndex;
                                            TEMPUSER.ByDay = 0;

                                            _contentStepsPANEL.Controls.Remove(_contentSteepsThreePANEL);
                                            ApplyingСhangesStepThree();

                                        }
                                        else
                                        {
                                            MessageBox.Show("Поле минуты и часы должны быть корректно заполнены", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле минуты и часы содержать только цифры", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Поле минуты и часы не должны быть пустыми", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            if (_steeps == 4)
                            {
                                TEMPUSER.RecordVideo = (_onRecordVideo) ? 1 : 0;
                                TEMPUSER.RecordAudio = (_onRecordAudio) ? 1 : 0;
                                TEMPUSER.BlockUrl = (_url.Count != 0) ? 1 : 0;

                                if (_onScreenShoot)
                                {
                                    TEMPUSER.ScreenShot = (_onScreenShoot) ? 1 : 0;
                                    TEMPUSER.TimeSnapshot = Convert.ToInt32(_snapshotFrequencyNUMUPDOWN.Value);
                                }
                                else
                                {
                                    TEMPUSER.ScreenShot = (_onScreenShoot) ? 1 : 0;
                                    TEMPUSER.TimeSnapshot = 0;
                                }
                                if (TEMPUSER.ByDay == 1)
                                {
                                    _date.AddRange(MergeDateUsers(_mondayHold, "Monday"));
                                    _date.AddRange(MergeDateUsers(_tuesdayHold, "Tuesday"));
                                    _date.AddRange(MergeDateUsers(_environsHold, "Environs"));
                                    _date.AddRange(MergeDateUsers(_thursdayHold, "Thursday"));
                                    _date.AddRange(MergeDateUsers(_fridayHold, "Friday"));
                                    _date.AddRange(MergeDateUsers(_saturdayHold, "Saturday"));
                                    _date.AddRange(MergeDateUsers(_sundayHold, "Sunday"));
                                }
                                // Преобразование картинок
                                byte[] avatarBytes;
                                string formatAvatar = "png";
                                string backgroundFormat = "jpeg";
                                byte[] backgroundBytes;
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    // аватарка
                                    Image image = Properties.Resources.logo;
                                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                    avatarBytes = memoryStream.ToArray();
                                    // фон
                                    image = Properties.Resources.fon;
                                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    backgroundBytes = memoryStream.ToArray();
                                }

                                int codeOperation = SQL.CreateNewUserInProgram(TEMPUSER.Login, TEMPUSER.Password, TEMPUSER.Name, TEMPUSER.Surname, TEMPUSER.SecretText, TEMPUSER.SecretWord,
                                    TEMPUSER.FontFamily, TEMPUSER.SizeFont, TEMPUSER.StyleFontFamily, TEMPUSER.ColorProgram, TEMPUSER.Inaction, TEMPUSER.NotificetionExperiationTime,
                                    TEMPUSER.MaxSoundEffect, TEMPUSER.SoundEffect, TEMPUSER.RecordVideo, TEMPUSER.RecordAudio, TEMPUSER.ScreenShot, TEMPUSER.TimeSnapshot, TEMPUSER.TimeConstraint,
                                    TEMPUSER.ByDay, TEMPUSER.Block, TEMPUSER.BlockProgram, _program, TEMPUSER.BlockUrl, _url, DateTime.Now.ToShortDateString(), TEMPUSER.Access, TEMPUSER.TimeConstraint, 
                                    TEMPUSER.VersionForTheVisuallyImpaired, _date, avatarBytes, formatAvatar, backgroundBytes, backgroundFormat);

                                if (codeOperation > 0)
                                {
                                    _contentStepsPANEL.Controls.Remove(_controlPanelFLP);

                                    _panelStagePANEL.Enabled = false;
                                    StyleEndRegistrationUser();

                                    //StyleEndRegistrationUser();
                                }
                                else if (codeOperation == -1)
                                {
                                    Error.ClientError.Unauthorized();
                                }
                                else
                                {
                                    Error.ServerError.UnknowError();
                                }
                                //SQL.CreateUser();
                            }// Режим шпиона
                        }// else (Внешний вид программы)
                    }// else (Настройка личных данных)
                }
                else
                {
                    // Access
                    MessageBox.Show("HHHH");
                }
            }
        }

        private string[] TempStartAndEndTimeDate(string time)
        {
            return new string[2] { time[0].ToString() + time[1].ToString(), time[6].ToString() + time[7].ToString() };
        }
        //
        //  Оптимизация количества записей блокировки по дням недели
        //
        public List<string> MergeDateUsers(List<string> date, string weekday)
        {
            List<string> temp = new List<string>();
            while (date.Count != 0)
            {
                // Выделение начального и конечного времени
                var temp_ititialTime = TempStartAndEndTimeDate(date[0]);
                date.RemoveAt(0);
                for (int index = 0; index < date.Count; index++)
                {
                    var temp_holdTime = TempStartAndEndTimeDate(date[index]);
                    // Проверка на продолжение времени
                    if (temp_holdTime[0] == temp_ititialTime[1])
                    {
                        temp_ititialTime[1] = temp_holdTime[1];
                        date.RemoveAt(index);
                        index--;
                    }
                    else if (temp_holdTime[1] == temp_ititialTime[0])
                    {
                        temp_ititialTime[0] = temp_holdTime[0];
                        date.RemoveAt(index);
                        index--;
                    }
                }
                temp.Add(weekday + '_' + temp_ititialTime[0] + '-' + temp_ititialTime[1]);
            }
            
            return temp;
        }

        private void StyleEndRegistrationUser()
        {
            this.Controls.Remove(_panelNameSteepPANEL);
            this.Controls.Remove(_stagePANEL);
            // 
            // acceptPICTUREBOX
            // 
            PictureBox acceptPICTUREBOX = new PictureBox();
            acceptPICTUREBOX.BackgroundImage = Properties.Resources.Check_256__2_;
            acceptPICTUREBOX.BackgroundImageLayout = ImageLayout.Stretch;
            acceptPICTUREBOX.Name = "acceptPICTUREBOX";
            acceptPICTUREBOX.Size = new Size(60, 60);
            acceptPICTUREBOX.TabIndex = 0;
            acceptPICTUREBOX.TabStop = false;
            // 
            // endRegistrationLABEL
            // 
            Label endRegistrationLABEL = new Label();
            endRegistrationLABEL.Font = StyleWindows._titleMainFont;
            endRegistrationLABEL.Name = "endRegistrationLABEL";
            endRegistrationLABEL.TabIndex = 1;
            endRegistrationLABEL.Text = "Регистрация прошла успешно";
            Size size = TextRenderer.MeasureText(endRegistrationLABEL.Text, endRegistrationLABEL.Font);
            endRegistrationLABEL.Size = size;
            // 
            // moreInfoLABEL
            // 
            Label moreInfoLABEL = new Label();
            moreInfoLABEL.AutoSize = true;
            moreInfoLABEL.Font = StyleWindows._sideFont;
            moreInfoLABEL.ForeColor = SystemColors.ControlDark;
            moreInfoLABEL.MaximumSize = new Size(size.Width, 0);
            moreInfoLABEL.Name = "moreInfoLABEL";
            moreInfoLABEL.TabIndex = 1;
            moreInfoLABEL.Text = "Теперь вы сможете более эффективно следить за действиями вашего ребенка.";
            moreInfoLABEL.TextAlign = ContentAlignment.MiddleCenter;
            //
            // morePANEL
            //
            Panel morePANEL = new Panel();
            morePANEL.Name = "morePANEL";
            Size sizeMorePanel = TextRenderer.MeasureText(moreInfoLABEL.Text, moreInfoLABEL.Font);
            int heightMorePanel = (int)((size.Width / endRegistrationLABEL.Width) + 1) * sizeMorePanel.Height;
            morePANEL.Size = new Size(endRegistrationLABEL.Width + 4, heightMorePanel);
            morePANEL.Controls.Add(moreInfoLABEL);

            // 
            // newUserBUTTON
            // 
            Button newUserBUTTON = new Button();
            newUserBUTTON.ForeColor = Color.White;
            newUserBUTTON.BackColor = StyleWindows._mainColor;
            newUserBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            newUserBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            newUserBUTTON.FlatAppearance.BorderSize = 2;
            newUserBUTTON.FlatStyle = FlatStyle.Flat;
            newUserBUTTON.Font = StyleWindows._sideFont;
            newUserBUTTON.Name = "newUserBUTTON";
            newUserBUTTON.Text = "Зарегистрировать нового пользователя";
            Size sizeButton = TextRenderer.MeasureText(newUserBUTTON.Text, newUserBUTTON.Font);
            newUserBUTTON.Size = new Size(size.Width, (sizeButton.Width > (size.Width - 17)) ? sizeButton.Height * (sizeButton.Width / (size.Width - 17) +1) : 40);
            newUserBUTTON.TabIndex = 1;
            newUserBUTTON.UseVisualStyleBackColor = false;
            newUserBUTTON.Click += NewUserBUTTON_Click;
            // 
            // infoPanelPANEL
            // 
            Panel infoPanelPANEL = new Panel();
            
            infoPanelPANEL.Controls.Add(newUserBUTTON);
            infoPanelPANEL.Controls.Add(morePANEL);
            infoPanelPANEL.Controls.Add(acceptPICTUREBOX);
            infoPanelPANEL.Controls.Add(endRegistrationLABEL);
            infoPanelPANEL.Name = "infoPanelPANEL";
            infoPanelPANEL.TabIndex = 0;
            infoPanelPANEL.Width = size.Width + 16;
            infoPanelPANEL.Width = endRegistrationLABEL.Width + 10;


            acceptPICTUREBOX.Location = new Point(infoPanelPANEL.Width / 2 - acceptPICTUREBOX.Width / 2, 0);
            endRegistrationLABEL.Location = new Point(infoPanelPANEL.Width / 2 - endRegistrationLABEL.Width / 2, acceptPICTUREBOX.Location.Y + acceptPICTUREBOX.Height);
            morePANEL.Location = new Point(infoPanelPANEL.Width / 2 - morePANEL.Width / 2, endRegistrationLABEL.Location.Y + endRegistrationLABEL.Height + 5);
            newUserBUTTON.Location = new Point(endRegistrationLABEL.Location.X, morePANEL.Location.Y + morePANEL.Height + 10);
            infoPanelPANEL.Size = new Size(size.Width + 16, newUserBUTTON.Location.Y + newUserBUTTON.Height);
            infoPanelPANEL.Location = new Point(_contentStepsPANEL.Width / 2 - infoPanelPANEL.Width / 2, _contentStepsPANEL.Height / 2 - infoPanelPANEL.Height / 2);
            //////
            _contentStepsPANEL.Controls.Add(infoPanelPANEL);
        }

        private void NewUserBUTTON_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _contentStepsPANEL.Controls.Clear();
            this.Controls.Remove(_panelStagePANEL);
            this.Controls.Remove(_contentStepsPANEL);
            StartSettings();
            this.Refresh();
            _contentStepsPANEL.Refresh();
            ClearTempRegistration();
        }

        private void ClearTempRegistration()
        {

        }

        private void _nextBUTTON_Click(object sender, EventArgs e)
        {
            
            NewPositionStage();
            this.Refresh();
        }// Функция перемещения между страницами

        //
        //  Проверка на правильный ввод текста
        //
        private bool CheckTrueReadText(string text)
        {
            foreach (char value in _nameTEXTBOX.Text)
            {
                if (value == '0' || value == '1' || value == '2' || value == '3' || value == '4' || value == '5' || value == '6' || value == '7' || value == '8' ||
                    value == '9')
                    return true;
            }

            return false;
        }

        #region Отображение третьего этапа создания пользователя
        private void CreateElementsAppearanceUser()
        {
            int padding = 40;
            // 
            // _contentSteepsThreePANEL
            // 
            this._contentSteepsThreePANEL.BackColor = System.Drawing.Color.White;
            this._contentSteepsThreePANEL.Controls.Add(this._settingFontBUTTON);
            this._contentSteepsThreePANEL.Controls.Add(this._positionCheckedBoxSteepThreeFLP);
            this._contentSteepsThreePANEL.Controls.Add(this.panel1);
            this._contentSteepsThreePANEL.Location = new System.Drawing.Point(4, (padding * (-1)));
            this._contentSteepsThreePANEL.Name = "_contentSteepsThreePANEL";
            this._contentSteepsThreePANEL.Size = new System.Drawing.Size(347, 299);
            this._contentSteepsThreePANEL.TabIndex = 3;
            //
            //  Положение кнопки
            //
            //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentSteepsThreePANEL.Location.Y + _contentSteepsThreePANEL.Height + padding);
            // 
            // _checkedScrollSoundEffectFLP
            // 
            this._checkedScrollSoundEffectFLP.AutoSize = true;
            this._checkedScrollSoundEffectFLP.Controls.Add(this._soundEffectsLABEL);
            this._checkedScrollSoundEffectFLP.Controls.Add(this._boxCheckSoundEffectPANEL);
            this._checkedScrollSoundEffectFLP.Location = new System.Drawing.Point(3, 3);
            this._checkedScrollSoundEffectFLP.Name = "_checkedScrollSoundEffectFLP";
            this._checkedScrollSoundEffectFLP.Size = new System.Drawing.Size(186, 24);
            this._checkedScrollSoundEffectFLP.TabIndex = 30;
            _checkedScrollSoundEffectFLP.BackColor = Color.White;
            this._checkedScrollSoundEffectFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollSoundEffectFLP_Paint);
            // 
            // _soundEffectsLABEL
            // 
            this._soundEffectsLABEL.AutoSize = true;
            this._soundEffectsLABEL.Font = StyleWindows._mainFont;
            this._soundEffectsLABEL.Location = new System.Drawing.Point(0, 2);
            this._soundEffectsLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._soundEffectsLABEL.Name = "_soundEffectsLABEL";
            this._soundEffectsLABEL.Size = new System.Drawing.Size(137, 19);
            this._soundEffectsLABEL.TabIndex = 0;
            this._soundEffectsLABEL.Text = "Звуковые эффекты";
            // 
            // _boxCheckSoundEffectPANEL
            // 
            this._boxCheckSoundEffectPANEL.BackColor = System.Drawing.Color.Transparent;
            this._boxCheckSoundEffectPANEL.Controls.Add(this._pointConditionCheckSoundEffectPICTUREBOX);
            this._boxCheckSoundEffectPANEL.Controls.Add(this._conditionCheckSoundEffectLABEL);
            this._boxCheckSoundEffectPANEL.Location = new System.Drawing.Point(143, 3);
            this._boxCheckSoundEffectPANEL.Name = "_boxCheckSoundEffectPANEL";
            this._boxCheckSoundEffectPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckSoundEffectPANEL.TabIndex = 0;
            // 
            // _pointConditionCheckSoundEffectPICTUREBOX
            // 
            this._pointConditionCheckSoundEffectPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckSoundEffectPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckSoundEffectPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckSoundEffectPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckSoundEffectPICTUREBOX.Name = "_pointConditionCheckSoundEffectPICTUREBOX";
            this._pointConditionCheckSoundEffectPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckSoundEffectPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckSoundEffectPICTUREBOX.TabStop = false;
            this._pointConditionCheckSoundEffectPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckSoundEffectPICTUREBOX_Click);
            this._pointConditionCheckSoundEffectPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckSoundEffectPICTUREBOX_Paint);
            // 
            // _conditionCheckSoundEffectLABEL
            // 
            this._conditionCheckSoundEffectLABEL.AutoSize = true;
            this._conditionCheckSoundEffectLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._conditionCheckSoundEffectLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckSoundEffectLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckSoundEffectLABEL.Name = "_conditionCheckSoundEffectLABEL";
            this._conditionCheckSoundEffectLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckSoundEffectLABEL.TabIndex = 7;
            this._conditionCheckSoundEffectLABEL.Text = "Off";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._testColorPANEL);
            this.panel1.Controls.Add(this._topBorderColorDialogPANEL);
            this.panel1.Controls.Add(this._newColorBUTTON);
            this.panel1.Controls.Add(this._leftBorderColorDialogPANEL);
            this.panel1.Controls.Add(this._bottomBorderColorDialogPANEL);
            this.panel1.Controls.Add(this._rightBorderColorDialogPANEL);
            this.panel1.Location = new System.Drawing.Point(7, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 124);
            this.panel1.TabIndex = 24;
            // 
            // _testColorPANEL
            // 
            this._testColorPANEL.BackColor = StyleWindows._mainBorderColor;
            this._testColorPANEL.Location = new System.Drawing.Point(2, 2);
            this._testColorPANEL.Dock = DockStyle.Fill;
            this._testColorPANEL.Name = "_testColorPANEL";
            this._testColorPANEL.TabIndex = 25;
            this._testColorPANEL.TabStop = false;
            // 
            // _topBorderColorDialogPANEL
            // 
            this._topBorderColorDialogPANEL.BackColor = System.Drawing.Color.Silver;
            this._topBorderColorDialogPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._topBorderColorDialogPANEL.Location = new System.Drawing.Point(2, 0);
            this._topBorderColorDialogPANEL.Name = "_topBorderColorDialogPANEL";
            this._topBorderColorDialogPANEL.Size = new System.Drawing.Size(159, 2);
            this._topBorderColorDialogPANEL.TabIndex = 27;
            // 
            // _newColorBUTTON
            // 
            this._newColorBUTTON.BackColor = StyleWindows._mainColor;
            this._newColorBUTTON.Cursor = System.Windows.Forms.Cursors.Hand;
            this._newColorBUTTON.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._newColorBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._newColorBUTTON.FlatAppearance.BorderSize = 2;
            this._newColorBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            this._newColorBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._newColorBUTTON.Font = StyleWindows._mainFont;
            this._newColorBUTTON.ForeColor = System.Drawing.Color.White;
            this._newColorBUTTON.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this._newColorBUTTON.Name = "_newColorBUTTON";
            this._newColorBUTTON.TabIndex = 23;
            this._newColorBUTTON.Text = "Цвет приложения";
            this._newColorBUTTON.UseVisualStyleBackColor = false;
            this._newColorBUTTON.Click += new System.EventHandler(this._newColorBUTTON_Click);
            this._newColorBUTTON.Size = StyleWindows.NewSizeButton(_newColorBUTTON, StyleWindows._mainFont);
            this.panel1.Size = new System.Drawing.Size(163, 80 + _newColorBUTTON.Height);
            // 
            // _leftBorderColorDialogPANEL
            // 
            this._leftBorderColorDialogPANEL.BackColor = System.Drawing.Color.Silver;
            this._leftBorderColorDialogPANEL.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftBorderColorDialogPANEL.Location = new System.Drawing.Point(0, 0);
            this._leftBorderColorDialogPANEL.Name = "_leftBorderColorDialogPANEL";
            this._leftBorderColorDialogPANEL.Size = new System.Drawing.Size(2, 122);
            this._leftBorderColorDialogPANEL.TabIndex = 26;
            // 
            // _bottomBorderColorDialogPANEL
            // 
            this._bottomBorderColorDialogPANEL.BackColor = System.Drawing.Color.Silver;
            this._bottomBorderColorDialogPANEL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomBorderColorDialogPANEL.Location = new System.Drawing.Point(0, 122);
            this._bottomBorderColorDialogPANEL.Name = "_bottomBorderColorDialogPANEL";
            this._bottomBorderColorDialogPANEL.Size = new System.Drawing.Size(161, 2);
            this._bottomBorderColorDialogPANEL.TabIndex = 29;
            // 
            // _rightBorderColorDialogPANEL
            // 
            this._rightBorderColorDialogPANEL.BackColor = System.Drawing.Color.Silver;
            this._rightBorderColorDialogPANEL.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightBorderColorDialogPANEL.Location = new System.Drawing.Point(161, 0);
            this._rightBorderColorDialogPANEL.Name = "_rightBorderColorDialogPANEL";
            this._rightBorderColorDialogPANEL.Size = new System.Drawing.Size(2, 124);
            this._rightBorderColorDialogPANEL.TabIndex = 28;
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = StyleWindows._mainBorderColor;
            this.colorDialog1.FullOpen = true;
            // 
            // _soundEffectTICK
            // 
            this._soundEffectTICK.Interval = 1;
            this._soundEffectTICK.Tick += new System.EventHandler(this._soundEffectTICK_Tick);
            // 
            // _positionCheckedBoxSteepThreeFLP
            // 
            this._positionCheckedBoxSteepThreeFLP.Controls.Add(this._checkedScrollSoundEffectFLP);
            this._positionCheckedBoxSteepThreeFLP.Controls.Add(this._checkedScroMaxVolumeFLP);
            this._positionCheckedBoxSteepThreeFLP.Controls.Add(this._checkedScrollEyeProblemFLP);
            this._positionCheckedBoxSteepThreeFLP.Location = new System.Drawing.Point(7, panel1.Location.Y + panel1.Height);
            this._positionCheckedBoxSteepThreeFLP.Name = "_positionCheckedBoxSteepThreeFLP";
            this._positionCheckedBoxSteepThreeFLP.Size = new System.Drawing.Size(320, 100);
            this._positionCheckedBoxSteepThreeFLP.TabIndex = 31;
            // 
            // _checkedScroMaxVolumeFLP
            // 
            this._checkedScroMaxVolumeFLP.AutoSize = true;
            this._checkedScroMaxVolumeFLP.Controls.Add(this._maxVolumeLABEL);
            this._checkedScroMaxVolumeFLP.Controls.Add(this._boxCheckMaxVolumePANEL);
            this._checkedScroMaxVolumeFLP.Location = new System.Drawing.Point(20, 33);
            this._checkedScroMaxVolumeFLP.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this._checkedScroMaxVolumeFLP.Name = "_checkedScroMaxVolumeFLP";
            this._checkedScroMaxVolumeFLP.Size = new System.Drawing.Size(221, 24);
            this._checkedScroMaxVolumeFLP.TabIndex = 31;
            this._checkedScroMaxVolumeFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScroMaxVolumeFLP_Paint);
            // 
            // _maxVolumeLABEL
            // 
            this._maxVolumeLABEL.AutoSize = true;
            this._maxVolumeLABEL.Font = StyleWindows._mainFont;
            this._maxVolumeLABEL.Location = new System.Drawing.Point(0, 2);
            this._maxVolumeLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._maxVolumeLABEL.Name = "_maxVolumeLABEL";
            this._maxVolumeLABEL.Size = new System.Drawing.Size(172, 19);
            this._maxVolumeLABEL.TabIndex = 0;
            this._maxVolumeLABEL.Text = "Увеличенная громкость";
            // 
            // _boxCheckMaxVolumePANEL
            // 
            this._boxCheckMaxVolumePANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckMaxVolumePANEL.Controls.Add(this._pointConditionCheckMaxVolumePICTUREBOX);
            this._boxCheckMaxVolumePANEL.Controls.Add(this._conditionCheckMaxVolumeLABEL);
            this._boxCheckMaxVolumePANEL.Location = new System.Drawing.Point(178, 3);
            this._boxCheckMaxVolumePANEL.Name = "_boxCheckMaxVolumePANEL";
            this._boxCheckMaxVolumePANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckMaxVolumePANEL.TabIndex = 0;
            // 
            // _pointConditionCheckMaxVolumePICTUREBOX
            // 
            this._pointConditionCheckMaxVolumePICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckMaxVolumePICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckMaxVolumePICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckMaxVolumePICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckMaxVolumePICTUREBOX.Name = "_pointConditionCheckMaxVolumePICTUREBOX";
            this._pointConditionCheckMaxVolumePICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckMaxVolumePICTUREBOX.TabIndex = 0;
            this._pointConditionCheckMaxVolumePICTUREBOX.TabStop = false;
            this._pointConditionCheckMaxVolumePICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckMaxVolumePICTUREBOX_Click);
            this._pointConditionCheckMaxVolumePICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckMaxVolumePICTUREBOX_Paint);
            // 
            // _conditionCheckMaxVolumeLABEL
            // 
            this._conditionCheckMaxVolumeLABEL.AutoSize = true;
            this._conditionCheckMaxVolumeLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._conditionCheckMaxVolumeLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckMaxVolumeLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckMaxVolumeLABEL.Name = "_conditionCheckMaxVolumeLABEL";
            this._conditionCheckMaxVolumeLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckMaxVolumeLABEL.TabIndex = 7;
            this._conditionCheckMaxVolumeLABEL.Text = "Off";
            // 
            // _checkedScrollEyeProblemFLP
            // 
            this._checkedScrollEyeProblemFLP.AutoSize = true;
            this._checkedScrollEyeProblemFLP.Controls.Add(this._eyeProblemLABEL);
            this._checkedScrollEyeProblemFLP.Controls.Add(this._boxCheckEyeProblemPANEL);
            this._checkedScrollEyeProblemFLP.Location = new System.Drawing.Point(3, 63);
            this._checkedScrollEyeProblemFLP.Name = "_checkedScrollEyeProblemFLP";
            this._checkedScrollEyeProblemFLP.Size = new System.Drawing.Size(310, 24);
            this._checkedScrollEyeProblemFLP.TabIndex = 32;
            this._checkedScrollEyeProblemFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollEyeProblemFLP_Paint);
            // 
            // _eyeProblemLABEL
            // 
            this._eyeProblemLABEL.AutoSize = true;
            this._eyeProblemLABEL.Font = StyleWindows._mainFont;
            this._eyeProblemLABEL.Location = new System.Drawing.Point(0, 2);
            this._eyeProblemLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._eyeProblemLABEL.Name = "_eyeProblemLABEL";
            this._eyeProblemLABEL.Size = new System.Drawing.Size(261, 19);
            this._eyeProblemLABEL.TabIndex = 0;
            this._eyeProblemLABEL.Text = "Версия для сабовидящих";
            // 
            // _boxCheckEyeProblemPANEL
            // 
            this._boxCheckEyeProblemPANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckEyeProblemPANEL.Controls.Add(this._pointConditionCheckEyeProblemPICTUREBOX);
            this._boxCheckEyeProblemPANEL.Controls.Add(this._conditionCheckEyeProblemLABEL);
            this._boxCheckEyeProblemPANEL.Location = new System.Drawing.Point(267, 3);
            this._boxCheckEyeProblemPANEL.Name = "_boxCheckEyeProblemPANEL";
            this._boxCheckEyeProblemPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckEyeProblemPANEL.TabIndex = 0;
            // 
            // _pointConditionCheckEyeProblemPICTUREBOX
            // 
            this._pointConditionCheckEyeProblemPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckEyeProblemPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckEyeProblemPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckEyeProblemPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckEyeProblemPICTUREBOX.Name = "_pointConditionCheckEyeProblemPICTUREBOX";
            this._pointConditionCheckEyeProblemPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckEyeProblemPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckEyeProblemPICTUREBOX.TabStop = false;
            this._pointConditionCheckEyeProblemPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckEyeProblemPICTUREBOX_Click);
            this._pointConditionCheckEyeProblemPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckEyeProblemPICTUREBOX_Paint);
            // 
            // _conditionCheckEyeProblemLABEL
            // 
            this._conditionCheckEyeProblemLABEL.AutoSize = true;
            this._conditionCheckEyeProblemLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._conditionCheckEyeProblemLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckEyeProblemLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckEyeProblemLABEL.Name = "_conditionCheckEyeProblemLABEL";
            this._conditionCheckEyeProblemLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckEyeProblemLABEL.TabIndex = 7;
            this._conditionCheckEyeProblemLABEL.Text = "Off";
            // 
            // _maxVolumeTIMER
            // 
            this._maxVolumeTIMER.Interval = 1;
            this._maxVolumeTIMER.Tick += new System.EventHandler(this._maxVolumeTIMER_Tick);
            // 
            // _eyeProblemTIMER
            // 
            this._eyeProblemTIMER.Interval = 1;
            this._eyeProblemTIMER.Tick += new System.EventHandler(this._eyeProblemTIMER_Tick);
            // 
            // _effectAppearanceMaxVolumeTIMER
            // 
            this._effectAppearanceMaxVolumeTIMER.Interval = 1;
            this._effectAppearanceMaxVolumeTIMER.Tick += new System.EventHandler(this._effectAppearanceMaxVolumeTIMER_Tick);
            // 
            // fontDialog1
            // 
            this.fontDialog1.AllowScriptChange = false;
            this.fontDialog1.AllowVerticalFonts = false;
            this.fontDialog1.Font = StyleWindows._mainFont;
            this.fontDialog1.MaxSize = 16;
            this.fontDialog1.MinSize = 9;
            this.fontDialog1.ShowEffects = false;
            // 
            // _settingFontBUTTON
            // 
            this._settingFontBUTTON.BackColor = StyleWindows._mainColor;
            this._settingFontBUTTON.Cursor = System.Windows.Forms.Cursors.Hand;
            this._settingFontBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._settingFontBUTTON.FlatAppearance.BorderSize = 2;
            this._settingFontBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            this._settingFontBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._settingFontBUTTON.Font = StyleWindows._mainFont;
            this._settingFontBUTTON.ForeColor = System.Drawing.Color.White;
            this._settingFontBUTTON.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._settingFontBUTTON.Location = new System.Drawing.Point(7, 0);
            this._settingFontBUTTON.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this._settingFontBUTTON.Name = "_settingFontBUTTON";
            this._settingFontBUTTON.Size = new System.Drawing.Size(163, 40);
            this._settingFontBUTTON.TabIndex = 32;
            this._settingFontBUTTON.Text = "Настройка шрифта";
            this._settingFontBUTTON.UseVisualStyleBackColor = false;
            this._settingFontBUTTON.Click += new System.EventHandler(this._settingFontBUTTON_Click);
            //
            //  Закругление формы
            //
            ToolBox.SetRoundedShape(_boxCheckSoundEffectPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckMaxVolumePANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckEyeProblemPANEL, radius);
            //
            //  Скрытие элемента повышенной громкости приложения
            //
            _heightMaxVolume = _checkedScroMaxVolumeFLP.Height;
            _checkedScroMaxVolumeFLP.AutoSize = false;
            _checkedScroMaxVolumeFLP.Height = 0;
            //
            //  Отображение всех элементов
            //
            _soundEffectsLABEL.Font = StyleWindows._mainFont;
            _maxVolumeLABEL.Font = StyleWindows._mainFont;
            _eyeProblemLABEL.Font = StyleWindows._mainFont;
            _contentStepsPANEL.Controls.Add(_contentSteepsThreePANEL);

        }
        //
        //  Отображение политры
        //
        private void _newColorBUTTON_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                _testColorPANEL.BackColor = colorDialog1.Color;
            }
        }


        #region CheckBox звуковые оповещения
        //
        //  Рисование кружка состояния
        //
        private void _pointConditionCheckSoundEffectPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, _onSoundEffect);
        //
        //  Перемещение кружка и изменение текста подсказывающий состояние
        //
        private void _soundEffectTICK_Tick(object sender, EventArgs e) => ToolBox.MovingTheStatusSlider(_onSoundEffect, _pointConditionCheckSoundEffectPICTUREBOX, _conditionCheckSoundEffectLABEL, _boxCheckSoundEffectPANEL, _soundEffectTICK);

        //
        //  Обводка формы
        //
        private void _checkedScrollSoundEffectFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(_onSoundEffect, _boxCheckSoundEffectPANEL, sender, e);

        //
        //  Нажатие кнопки перевода с вкл на выкл
        //
        private void _pointConditionCheckSoundEffectPICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onSoundEffect)
            {
                _onSoundEffect = false;
                _closeMaxVolume = true;
            }
            else
            {
                _onSoundEffect = true;
                _closeMaxVolume = false;
            }
            _checkedScrollSoundEffectFLP.Refresh();
            _effectAppearanceMaxVolumeTIMER.Enabled = true;
            _soundEffectTICK.Enabled = true;
        }
        #endregion


        #region Увеличение громкости
        //
        //  Переключение режима
        //
        private void _pointConditionCheckMaxVolumePICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onMaxVolume)
            {
                _onMaxVolume = false;
                //grPanel.Dispose();
                _checkedScroMaxVolumeFLP.Refresh();
            }
            else
            {
                //grPanel.Dispose();
                _onMaxVolume = true;
                _checkedScroMaxVolumeFLP.Refresh();
            }
            _maxVolumeTIMER.Enabled = true;
        }
        //
        //  Обводка формы
        //
        private void _checkedScroMaxVolumeFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (_onMaxVolume)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = _boxCheckMaxVolumePANEL.Location.X;
            int y = _boxCheckMaxVolumePANEL.Location.Y;
            int width = x + _boxCheckMaxVolumePANEL.Width;
            int height = _boxCheckMaxVolumePANEL.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }
        //
        //  Анимация перемещения положения CheckBox
        //
        private void _maxVolumeTIMER_Tick(object sender, EventArgs e)
        {
            if (_onMaxVolume)
            {
                if (_pointConditionCheckMaxVolumePICTUREBOX.Width + 2 != _pointConditionCheckMaxVolumePICTUREBOX.Location.X)
                {
                    _pointConditionCheckMaxVolumePICTUREBOX.Location = new Point(_pointConditionCheckMaxVolumePICTUREBOX.Location.X + 1, 0);
                }
                else
                {
                    _conditionCheckMaxVolumeLABEL.Text = "On";
                    _conditionCheckMaxVolumeLABEL.ForeColor = _mainColor;
                    _conditionCheckMaxVolumeLABEL.Location = new Point(2, 2);
                    _maxVolumeTIMER.Enabled = false;
                }

            }
            else
            {
                if (_pointConditionCheckMaxVolumePICTUREBOX.Location.X != 0)
                {
                    _pointConditionCheckMaxVolumePICTUREBOX.Location = new Point(_pointConditionCheckMaxVolumePICTUREBOX.Location.X - 1, 0);
                }
                else
                {

                    _conditionCheckMaxVolumeLABEL.Text = "Off";
                    _conditionCheckMaxVolumeLABEL.ForeColor = Color.Red;
                    _conditionCheckMaxVolumeLABEL.Location = new Point(_boxCheckMaxVolumePANEL.Width - _conditionCheckMaxVolumeLABEL.Width - 2, 2);

                    _maxVolumeTIMER.Enabled = false;
                }
            }
        }
        //
        //  Отрисовка кружка состояния
        //
        private void _pointConditionCheckMaxVolumePICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (_onMaxVolume)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }
        #endregion


        #region Проблемы с зрением
        //
        //  Обводка CheckBox
        //
        private void _checkedScrollEyeProblemFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (_onEyeProblem)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = _boxCheckEyeProblemPANEL.Location.X;
            int y = _boxCheckEyeProblemPANEL.Location.Y;
            int width = x + _boxCheckEyeProblemPANEL.Width;
            int height = _boxCheckEyeProblemPANEL.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }

        private void _pointConditionCheckEyeProblemPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (_onEyeProblem)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }

        private void _eyeProblemTIMER_Tick(object sender, EventArgs e)
        {
            if (_onEyeProblem)
            {
                if (_pointConditionCheckEyeProblemPICTUREBOX.Width + 2 != _pointConditionCheckEyeProblemPICTUREBOX.Location.X)
                {
                    _pointConditionCheckEyeProblemPICTUREBOX.Location = new Point(_pointConditionCheckEyeProblemPICTUREBOX.Location.X + 1, 0);
                }
                else
                {
                    _conditionCheckEyeProblemLABEL.Text = "On";
                    _conditionCheckEyeProblemLABEL.ForeColor = _mainColor;
                    _conditionCheckEyeProblemLABEL.Location = new Point(2, 2);
                    _eyeProblemTIMER.Enabled = false;
                }

            }
            else
            {
                if (_pointConditionCheckEyeProblemPICTUREBOX.Location.X != 0)
                {
                    _pointConditionCheckEyeProblemPICTUREBOX.Location = new Point(_pointConditionCheckEyeProblemPICTUREBOX.Location.X - 1, 0);
                }
                else
                {

                    _conditionCheckEyeProblemLABEL.Text = "Off";
                    _conditionCheckEyeProblemLABEL.ForeColor = Color.Red;
                    _conditionCheckEyeProblemLABEL.Location = new Point(_boxCheckEyeProblemPANEL.Width - _conditionCheckEyeProblemLABEL.Width - 2, 2);

                    _eyeProblemTIMER.Enabled = false;
                }
            }// MaxVolume
        }
        //
        //  Перемещение ползунка при нажатии
        //
        private void _pointConditionCheckEyeProblemPICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onEyeProblem)
            {
                _onEyeProblem = false;
                _checkedScrollEyeProblemFLP.Refresh();
            }
            else
            {
                _onEyeProblem = true;
                _checkedScrollEyeProblemFLP.Refresh();
            }
            _eyeProblemTIMER.Enabled = true;
        }
        #endregion

        //
        //  Анимация появления CheckBox увеличения громкости
        //
        private void _effectAppearanceMaxVolumeTIMER_Tick(object sender, EventArgs e) => ToolBox.AppearanceOfSubItems(_onMaxVolume, _checkedScroMaxVolumeFLP, _heightMaxVolume, _effectAppearanceMaxVolumeTIMER);

        private void _settingFontBUTTON_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                MessageBox.Show(fontDialog1.Font.Name.ToString());
            }
        }
        #endregion


        #region Отображение четвертого этапа создания пользователя
        private Size SizeComboBoxFLP(string text, Font font, Control flp,Panel panel, int padding)
        {
            Size size = TextRenderer.MeasureText(text, font);
            flp.Size = new Size(size.Width + panel.Width + padding, (size.Height < panel.Height) ? panel.Height + 6 : size.Height + panel.Padding.Top + panel.Padding.Bottom);
            return flp.Size;
        }
        private void CreateAppearanceFourSteep()
        {
            int padding = 10;
            // 
            // _timeTableFLOWLAYOUTPANEL
            // 
            this._timeTableFLOWLAYOUTPANEL.Location = new System.Drawing.Point(80, 39);
            this._timeTableFLOWLAYOUTPANEL.Name = "_timeTableFLOWLAYOUTPANEL";
            this._timeTableFLOWLAYOUTPANEL.Size = new System.Drawing.Size(184, 510);
            this._timeTableFLOWLAYOUTPANEL.TabIndex = 0;
            // 
            // _weekdayLabelFLOWLAYOUTPANEL
            // 
            this._weekdayLabelFLOWLAYOUTPANEL.Location = new System.Drawing.Point(77, 13);
            this._weekdayLabelFLOWLAYOUTPANEL.Name = "_weekdayLabelFLOWLAYOUTPANEL";
            this._weekdayLabelFLOWLAYOUTPANEL.Size = new System.Drawing.Size(204, 20);
            this._weekdayLabelFLOWLAYOUTPANEL.TabIndex = 2;
            // 
            // _contentFourSteepFLP
            // 
            this._contentFourSteepFLP.Controls.Add(this._checkedScrollBlockAccountFLP);
            this._contentFourSteepFLP.Controls.Add(this._checkedScrollnotificationTheExpirationOfTheTimeFLP);
            this._contentFourSteepFLP.Controls.Add(this._incationLABEL);
            this._contentFourSteepFLP.Controls.Add(this._incationCOMBOBOX);
            this._contentFourSteepFLP.Controls.Add(this._checkedScrollTimeLimitPerDayFLP);
            this._contentFourSteepFLP.Controls.Add(this.LimitForTheDayFLP);
            this._contentFourSteepFLP.Controls.Add(this._calendarPANEL);
            this._contentFourSteepFLP.Name = "_contentFourSteepFLP";
            this._contentFourSteepFLP.TabIndex = 32;
            this._contentFourSteepFLP.BackColor = Color.White;
            this._contentFourSteepFLP.Dock = DockStyle.Fill;
            this._contentFourSteepFLP.Paint += _contentFourSteepFLP_Paint;
            this._contentFourSteepFLP.AutoScroll = true;
            //
            //  Положение кнопки
            //
            //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentFourSteepFLP.Location.Y + _contentFourSteepFLP.Height);
            // 
            // _checkedScrollBlockAccountFLP
            // 
            this._checkedScrollBlockAccountFLP.Controls.Add(this._BloclAccountLABEL);
            this._checkedScrollBlockAccountFLP.Controls.Add(this._boxCheckBloclAccountPANEL);
            this._checkedScrollBlockAccountFLP.Location = new System.Drawing.Point(3, padding);
            this._checkedScrollBlockAccountFLP.BackColor = Color.Transparent;
            this._checkedScrollBlockAccountFLP.Name = "_checkedScrollBlockAccountFLP";
            this._checkedScrollBlockAccountFLP.TabIndex = 32;
            this._checkedScrollBlockAccountFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollBlockAccountFLP_Paint);
            // 
            // _boxCheckBloclAccountPANEL
            // 
            this._boxCheckBloclAccountPANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckBloclAccountPANEL.Controls.Add(this._pointConditionCheckBloclAccountPICTUREBOX);
            this._boxCheckBloclAccountPANEL.Controls.Add(this._conditionCheckBloclAccountLABEL);
            this._boxCheckBloclAccountPANEL.Name = "_boxCheckBloclAccountPANEL";
            this._boxCheckBloclAccountPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckBloclAccountPANEL.TabIndex = 0;
            // 
            // _BloclAccountLABEL
            // 
            this._BloclAccountLABEL.AutoSize = true;
            this._BloclAccountLABEL.Font = StyleWindows._mainFont;
            this._BloclAccountLABEL.Location = new System.Drawing.Point(0, 2);
            this._BloclAccountLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._BloclAccountLABEL.Name = "_BloclAccountLABEL";
            this._BloclAccountLABEL.TabIndex = 0;
            this._BloclAccountLABEL.Text = "Заблокировать аккаунт";
            _checkedScrollBlockAccountFLP.Size = SizeComboBoxFLP(_BloclAccountLABEL.Text, StyleWindows._mainFont,
                _checkedScrollBlockAccountFLP, _boxCheckBloclAccountPANEL, padding);
            // 
            // _pointConditionCheckBloclAccountPICTUREBOX
            // 
            this._pointConditionCheckBloclAccountPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckBloclAccountPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckBloclAccountPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckBloclAccountPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckBloclAccountPICTUREBOX.Name = "_pointConditionCheckBloclAccountPICTUREBOX";
            this._pointConditionCheckBloclAccountPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckBloclAccountPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckBloclAccountPICTUREBOX.TabStop = false;
            this._pointConditionCheckBloclAccountPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckBloclAccountPICTUREBOX_Click);
            this._pointConditionCheckBloclAccountPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckBloclAccountPICTUREBOX_Paint);
            // 
            // _conditionCheckBloclAccountLABEL
            // 
            this._conditionCheckBloclAccountLABEL.AutoSize = true;
            this._conditionCheckBloclAccountLABEL.Font = StyleWindows._sideFont;
            this._conditionCheckBloclAccountLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckBloclAccountLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckBloclAccountLABEL.Name = "_conditionCheckBloclAccountLABEL";
            this._conditionCheckBloclAccountLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckBloclAccountLABEL.TabIndex = 7;
            this._conditionCheckBloclAccountLABEL.Text = "Off";
            // 
            // _checkedScrollnotificationTheExpirationOfTheTimeFLP
            // 
            //this._checkedScrollnotificationTheExpirationOfTheTimeFLP.AutoSize = true;
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.BackColor = Color.Transparent;
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.Controls.Add(this._notificationTheExpirationOfTheTimeLABEL);
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.Controls.Add(this._boxChecknotificationTheExpirationOfTheTimePANEL);
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.Name = "_checkedScrollnotificationTheExpirationOfTheTimeFLP";
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.Location = new System.Drawing.Point(_checkedScrollBlockAccountFLP.Location.X, _checkedScrollBlockAccountFLP.Location.Y + _checkedScrollBlockAccountFLP.Height + padding);

            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.TabIndex = 33;
            this._checkedScrollnotificationTheExpirationOfTheTimeFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollnotificationTheExpirationOfTheTimeFLP_Paint);
            // 
            // _boxChecknotificationTheExpirationOfTheTimePANEL
            // 
            this._boxChecknotificationTheExpirationOfTheTimePANEL.BackColor = System.Drawing.Color.White;
            this._boxChecknotificationTheExpirationOfTheTimePANEL.Controls.Add(this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX);
            this._boxChecknotificationTheExpirationOfTheTimePANEL.Controls.Add(this._conditionChecknotificationTheExpirationOfTheTimeLABEL);
            this._boxChecknotificationTheExpirationOfTheTimePANEL.Location = new System.Drawing.Point(258, 3);
            this._boxChecknotificationTheExpirationOfTheTimePANEL.Name = "_boxChecknotificationTheExpirationOfTheTimePANEL";
            this._boxChecknotificationTheExpirationOfTheTimePANEL.Size = new System.Drawing.Size(40, 18);
            this._boxChecknotificationTheExpirationOfTheTimePANEL.TabIndex = 0;
            // 
            // _notificationTheExpirationOfTheTimeLABEL
            // 
            this._notificationTheExpirationOfTheTimeLABEL.AutoSize = true;
            this._notificationTheExpirationOfTheTimeLABEL.Font = StyleWindows._mainFont;
            this._notificationTheExpirationOfTheTimeLABEL.Location = new System.Drawing.Point(0, 2);
            this._notificationTheExpirationOfTheTimeLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._notificationTheExpirationOfTheTimeLABEL.Name = "_notificationTheExpirationOfTheTimeLABEL";
            this._notificationTheExpirationOfTheTimeLABEL.TabIndex = 0;
            this._notificationTheExpirationOfTheTimeLABEL.Text = "Оповещения о истечении времени";

            _checkedScrollnotificationTheExpirationOfTheTimeFLP.Size = SizeComboBoxFLP(_notificationTheExpirationOfTheTimeLABEL.Text, StyleWindows._mainFont,
                _checkedScrollnotificationTheExpirationOfTheTimeFLP, _boxChecknotificationTheExpirationOfTheTimePANEL, padding);
            // 
            // _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX
            // 
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Name = "_pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX";
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.TabIndex = 0;
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.TabStop = false;
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Click += new System.EventHandler(this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Click);
            this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Paint);
            // 
            // _conditionChecknotificationTheExpirationOfTheTimeLABEL
            // 
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.AutoSize = true;
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.Name = "_conditionChecknotificationTheExpirationOfTheTimeLABEL";
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.TabIndex = 7;
            this._conditionChecknotificationTheExpirationOfTheTimeLABEL.Text = "Off";
            // 
            // _incationLABEL
            // 
            this._incationLABEL.AutoSize = true;
            this._incationLABEL.Font = StyleWindows._mainFont;
            this._incationLABEL.Location = new System.Drawing.Point(_checkedScrollnotificationTheExpirationOfTheTimeFLP.Location.X, _checkedScrollnotificationTheExpirationOfTheTimeFLP.Location.Y + _checkedScrollnotificationTheExpirationOfTheTimeFLP.Height + padding);
            this._incationLABEL.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this._incationLABEL.Name = "_incationLABEL";
            this._incationLABEL.Size = new System.Drawing.Size(97, 19);
            this._incationLABEL.TabIndex = 34;
            this._incationLABEL.Text = "Бездействие";
            // 
            // _incationCOMBOBOX
            // 
            this._incationCOMBOBOX.DisplayMember = "0";
            this._incationCOMBOBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._incationCOMBOBOX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._incationCOMBOBOX.Font = StyleWindows._mainFont;
            this._incationCOMBOBOX.FormattingEnabled = true;
            this._incationCOMBOBOX.Items.AddRange(new object[] {
            "Отключено",
            "5 минут",
            "10 минут",
            "30 минут",
            "1 час",
            "2 часа"});
            Size size = TextRenderer.MeasureText(_incationLABEL.Text, StyleWindows._mainFont);
            this._incationCOMBOBOX.Location = new System.Drawing.Point(_incationLABEL.Location.X + size.Width + padding, _incationLABEL.Location.Y);
            this._incationCOMBOBOX.Name = "_incationCOMBOBOX";
            this._incationCOMBOBOX.Size = new System.Drawing.Size(180, 27);
            this._incationCOMBOBOX.TabIndex = 35;
            this._incationCOMBOBOX.Cursor = Cursors.Hand;
            // 
            // _checkedScrollTimeLimitPerDayFLP
            // 
            this._checkedScrollTimeLimitPerDayFLP.AutoSize = true;
            this._checkedScrollTimeLimitPerDayFLP.Controls.Add(this._timeLimitPerDayLABEL);
            this._checkedScrollTimeLimitPerDayFLP.Controls.Add(this._boxCheckTimeLimitPerDayPANEL);
            this._checkedScrollTimeLimitPerDayFLP.Location = new System.Drawing.Point(_checkedScrollnotificationTheExpirationOfTheTimeFLP.Location.X, _incationCOMBOBOX.Location.Y + _incationCOMBOBOX.Height + padding);
            this._checkedScrollTimeLimitPerDayFLP.Name = "_checkedScrollTimeLimitPerDayFLP";
            this._checkedScrollTimeLimitPerDayFLP.Size = new System.Drawing.Size(332, 24);
            this._checkedScrollTimeLimitPerDayFLP.TabIndex = 30;
            this._checkedScrollTimeLimitPerDayFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollTimeLimitPerDayFLP_Paint);
            // 
            // _timeLimitPerDayLABEL
            // 
            this._timeLimitPerDayLABEL.AutoSize = true;
            this._timeLimitPerDayLABEL.Font = StyleWindows._mainFont;
            this._timeLimitPerDayLABEL.Location = new System.Drawing.Point(0, 2);
            this._timeLimitPerDayLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._timeLimitPerDayLABEL.Name = "_timeLimitPerDayLABEL";
            this._timeLimitPerDayLABEL.Size = new System.Drawing.Size(283, 19);
            this._timeLimitPerDayLABEL.TabIndex = 0;
            this._timeLimitPerDayLABEL.Text = "Ограничение времени по дням недели";
            // 
            // _boxCheckTimeLimitPerDayPANEL
            // 
            this._boxCheckTimeLimitPerDayPANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckTimeLimitPerDayPANEL.Controls.Add(this._pointConditionCheckTimeLimitPerDayPICTUREBOX);
            this._boxCheckTimeLimitPerDayPANEL.Controls.Add(this._conditionCheckTimeLimitPerDayLABEL);
            this._boxCheckTimeLimitPerDayPANEL.Location = new System.Drawing.Point(289, 3);
            this._boxCheckTimeLimitPerDayPANEL.Name = "_boxCheckTimeLimitPerDayPANEL";
            this._boxCheckTimeLimitPerDayPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckTimeLimitPerDayPANEL.TabIndex = 0;
            _checkedScrollTimeLimitPerDayFLP.Size = SizeComboBoxFLP(_timeLimitPerDayLABEL.Text, StyleWindows._mainFont,
                _checkedScrollTimeLimitPerDayFLP, _boxCheckTimeLimitPerDayPANEL, padding);
            // 
            // _pointConditionCheckTimeLimitPerDayPICTUREBOX
            // 
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Name = "_pointConditionCheckTimeLimitPerDayPICTUREBOX";
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.TabStop = false;
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckTimeLimitPerDayPICTUREBOX_Click);
            this._pointConditionCheckTimeLimitPerDayPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckTimeLimitPerDayPICTUREBOX_Paint);
            // 
            // _conditionCheckTimeLimitPerDayLABEL
            // 
            this._conditionCheckTimeLimitPerDayLABEL.AutoSize = true;
            this._conditionCheckTimeLimitPerDayLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._conditionCheckTimeLimitPerDayLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckTimeLimitPerDayLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckTimeLimitPerDayLABEL.Name = "_conditionCheckTimeLimitPerDayLABEL";
            this._conditionCheckTimeLimitPerDayLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckTimeLimitPerDayLABEL.TabIndex = 7;
            this._conditionCheckTimeLimitPerDayLABEL.Text = "Off";
            // 
            // LimitForTheDayFLP
            // 
            this.LimitForTheDayFLP.AutoSize = true;
            this.LimitForTheDayFLP.Controls.Add(this._timePerDayLABEL);
            this.LimitForTheDayFLP.Controls.Add(this._hoursTEXTBOX);
            this.LimitForTheDayFLP.Controls.Add(this._shortHourLABEL);
            this.LimitForTheDayFLP.Controls.Add(this._minutesTEXTBOX);
            this.LimitForTheDayFLP.Controls.Add(this._shortMinuteTEXTBOX);
            this.LimitForTheDayFLP.Location = new System.Drawing.Point(25, _checkedScrollTimeLimitPerDayFLP.Location.Y + _checkedScrollTimeLimitPerDayFLP.Height + padding);
            this.LimitForTheDayFLP.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.LimitForTheDayFLP.Name = "LimitForTheDayFLP";
            this.LimitForTheDayFLP.Size = new System.Drawing.Size(265, 26);
            this.LimitForTheDayFLP.TabIndex = 36;
            this.LimitForTheDayFLP.Paint += new System.Windows.Forms.PaintEventHandler(this.LimitForTheDayFLP_Paint);
            // 
            // _timePerDayLABEL
            // 
            this._timePerDayLABEL.AutoSize = true;
            this._timePerDayLABEL.Font = StyleWindows._mainFont;
            this._timePerDayLABEL.Location = new System.Drawing.Point(3, 4);
            this._timePerDayLABEL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this._timePerDayLABEL.Name = "_timePerDayLABEL";
            this._timePerDayLABEL.Size = new System.Drawing.Size(112, 19);
            this._timePerDayLABEL.TabIndex = 14;
            this._timePerDayLABEL.Text = "Время на день";
            // 
            // _hoursTEXTBOX
            // 
            this._hoursTEXTBOX.BackColor = System.Drawing.Color.White;
            this._hoursTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._hoursTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F);
            this._hoursTEXTBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._hoursTEXTBOX.Location = new System.Drawing.Point(121, 3);
            this._hoursTEXTBOX.MaxLength = 2;
            this._hoursTEXTBOX.Name = "_hoursTEXTBOX";
            this._hoursTEXTBOX.ReadOnly = false;
            this._hoursTEXTBOX.Size = new System.Drawing.Size(34, 20);
            this._hoursTEXTBOX.TabIndex = 13;
            this._hoursTEXTBOX.Text = "00";
            this._hoursTEXTBOX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _shortHourLABEL
            // 
            this._shortHourLABEL.AutoSize = true;
            this._shortHourLABEL.Font = StyleWindows._mainFont;
            this._shortHourLABEL.Location = new System.Drawing.Point(161, 2);
            this._shortHourLABEL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this._shortHourLABEL.Name = "_shortHourLABEL";
            this._shortHourLABEL.Size = new System.Drawing.Size(17, 19);
            this._shortHourLABEL.TabIndex = 15;
            this._shortHourLABEL.Text = "ч";
            // 
            // _minutesTEXTBOX
            // 
            this._minutesTEXTBOX.BackColor = System.Drawing.Color.White;
            this._minutesTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._minutesTEXTBOX.Font = new System.Drawing.Font("Calibri", 12F);
            this._minutesTEXTBOX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._minutesTEXTBOX.Location = new System.Drawing.Point(184, 3);
            this._minutesTEXTBOX.MaxLength = 2;
            this._minutesTEXTBOX.Name = "_minutesTEXTBOX";
            this._minutesTEXTBOX.ReadOnly = false;
            this._minutesTEXTBOX.Size = new System.Drawing.Size(34, 20);
            this._minutesTEXTBOX.TabIndex = 17;
            this._minutesTEXTBOX.Text = "00";
            this._minutesTEXTBOX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _shortMinuteTEXTBOX
            // 
            this._shortMinuteTEXTBOX.AutoSize = true;
            this._shortMinuteTEXTBOX.Font = StyleWindows._mainFont;
            this._shortMinuteTEXTBOX.Location = new System.Drawing.Point(224, 2);
            this._shortMinuteTEXTBOX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this._shortMinuteTEXTBOX.Name = "_shortMinuteTEXTBOX";
            this._shortMinuteTEXTBOX.Size = new System.Drawing.Size(38, 19);
            this._shortMinuteTEXTBOX.TabIndex = 18;
            this._shortMinuteTEXTBOX.Text = "мин";
            // 
            // _blockAccountTIMER
            // 
            this._blockAccountTIMER.Interval = 1;
            this._blockAccountTIMER.Tick += new System.EventHandler(this._blockAccountTIMER_Tick);
            // 
            // _notificationTheExpirationOfTheTimeTIMER
            // 
            this._notificationTheExpirationOfTheTimeTIMER.Interval = 1;
            this._notificationTheExpirationOfTheTimeTIMER.Tick += new System.EventHandler(this._notificationTheExpirationOfTheTimeTIMER_Tick);
            // 
            // timeLimitPerDayTIMER
            // 
            // 
            // _calendarPANEL
            // 
            this._calendarPANEL.Controls.Add(this._weekdayLabelFLOWLAYOUTPANEL);
            this._calendarPANEL.Controls.Add(this._timeLabelFLOWLAYOUTPANEL);
            this._calendarPANEL.Controls.Add(this._timeTableFLOWLAYOUTPANEL);
            this._calendarPANEL.Location = new System.Drawing.Point(_checkedScrollTimeLimitPerDayFLP.Location.X, _checkedScrollTimeLimitPerDayFLP.Location.Y + _checkedScrollTimeLimitPerDayFLP.Height + padding);
            this._calendarPANEL.Name = "_calendarPANEL";
            this._calendarPANEL.Size = new System.Drawing.Size(289, 568);
            this._calendarPANEL.TabIndex = 0;
            this._calendarPANEL.Visible = false;
            // 
            // _timeLabelFLOWLAYOUTPANEL
            // 
            this._timeLabelFLOWLAYOUTPANEL.Location = new System.Drawing.Point(15, 30);
            this._timeLabelFLOWLAYOUTPANEL.Name = "_timeLabelFLOWLAYOUTPANEL";
            this._timeLabelFLOWLAYOUTPANEL.Size = new System.Drawing.Size(59, 535);
            this._timeLabelFLOWLAYOUTPANEL.TabIndex = 1;

            this.timeLimitPerDayTIMER.Interval = 1;
            this.timeLimitPerDayTIMER.Tick += new System.EventHandler(this.timeLimitPerDayTIMER_Tick);
            ToolBox.SetRoundedShape(_boxCheckBloclAccountPANEL, radius);
            ToolBox.SetRoundedShape(_boxChecknotificationTheExpirationOfTheTimePANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckTimeLimitPerDayPANEL, radius);
            _incationCOMBOBOX.SelectedIndex = 0;
            _BloclAccountLABEL.Font = StyleWindows._mainFont;
            _notificationTheExpirationOfTheTimeLABEL.Font = StyleWindows._mainFont;
            _incationLABEL.Font = StyleWindows._mainFont;
            _timeLimitPerDayLABEL.Font = StyleWindows._mainFont;
            _timePerDayLABEL.Font = StyleWindows._mainFont;
            _shortHourLABEL.Font = StyleWindows._mainFont;
            _shortMinuteTEXTBOX.Font = StyleWindows._mainFont;
            CreateTimeTable();
        }
        //
        //  Обводка элементов
        //
        private void _contentFourSteepFLP_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen p = new Pen(_mainColor, 3.0f);
            // Стиль шрифта 
            x = _incationCOMBOBOX.Location.X;
            y = _incationCOMBOBOX.Location.Y;
            width = _incationCOMBOBOX.Width;
            height = _incationCOMBOBOX.Height;
            gr.DrawRectangle(p, x - 1, y - 1, width + 1, height + 1);
        }

        #region Динамическое добавление времени в шкалу блокировки времени 
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
                button.Click += Button_Click;
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
        //
        //  Блокировка по времени
        //
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
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
        }
        //
        //  Определение на добавление или удаление из массива данных
        //
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
        //
        //  Добавление и удаление элементов из массива
        //
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
        #endregion


        #region CheckBox Заблокировать аккаунта
        private void LimitForTheDayFLP_Paint(object sender, PaintEventArgs e)
        {
            int x, y, width, height;
            Graphics gr = e.Graphics;
            Pen pen = new Pen(_mainColor, 3.0f);
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
        //
        //  Перемещение ползунка при нажатии
        //
        private void _pointConditionCheckBloclAccountPICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onBlockAccount)
            {
                _onBlockAccount = false;
                _checkedScrollBlockAccountFLP.Refresh();
            }
            else
            {
                _onBlockAccount = true;
                _checkedScrollBlockAccountFLP.Refresh();
            }
            _blockAccountTIMER.Enabled = true;
        }
        //
        //  Обводка CheckBox
        //
        private void _checkedScrollBlockAccountFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (_onBlockAccount)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = _boxCheckBloclAccountPANEL.Location.X;
            int y = _boxCheckBloclAccountPANEL.Location.Y;
            int width = x + _boxCheckBloclAccountPANEL.Width;
            int height = _boxCheckBloclAccountPANEL.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }
        //
        //  Перемещение ползунка
        //
        private void _blockAccountTIMER_Tick(object sender, EventArgs e)
        {
            if (_onBlockAccount)
            {
                if (_pointConditionCheckBloclAccountPICTUREBOX.Width + 2 != _pointConditionCheckBloclAccountPICTUREBOX.Location.X)
                {
                    _pointConditionCheckBloclAccountPICTUREBOX.Location = new Point(_pointConditionCheckBloclAccountPICTUREBOX.Location.X + 1, 0);
                }
                else
                {
                    _conditionCheckBloclAccountLABEL.Text = "On";
                    _conditionCheckBloclAccountLABEL.ForeColor = _mainColor;
                    _conditionCheckBloclAccountLABEL.Location = new Point(2, 2);
                    _blockAccountTIMER.Enabled = false;
                }

            }
            else
            {
                if (_pointConditionCheckBloclAccountPICTUREBOX.Location.X != 0)
                {
                    _pointConditionCheckBloclAccountPICTUREBOX.Location = new Point(_pointConditionCheckBloclAccountPICTUREBOX.Location.X - 1, 0);
                }
                else
                {

                    _conditionCheckBloclAccountLABEL.Text = "Off";
                    _conditionCheckBloclAccountLABEL.ForeColor = Color.Red;
                    _conditionCheckBloclAccountLABEL.Location = new Point(_boxCheckBloclAccountPANEL.Width - _conditionCheckBloclAccountLABEL.Width - 2, 2);

                    _blockAccountTIMER.Enabled = false;
                }
            }
        }
        //
        //  Рисование индикатора состояния
        //
        private void _pointConditionCheckBloclAccountPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (_onBlockAccount)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }

        #endregion

        #region Оповещения о истечении времени
        //
        //  Обводка CheckBox
        //
        private void _checkedScrollnotificationTheExpirationOfTheTimeFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (_onNotificationTheExpirationOfTheTime)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = _boxChecknotificationTheExpirationOfTheTimePANEL.Location.X;
            int y = _boxChecknotificationTheExpirationOfTheTimePANEL.Location.Y;
            int width = x + _boxChecknotificationTheExpirationOfTheTimePANEL.Width;
            int height = _boxChecknotificationTheExpirationOfTheTimePANEL.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }
        //
        //  Перемещение ползунка состояния
        //
        private void _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onNotificationTheExpirationOfTheTime)
            {
                _onNotificationTheExpirationOfTheTime = false;
            }
            else
            {
                _onNotificationTheExpirationOfTheTime = true;
            }
            _checkedScrollnotificationTheExpirationOfTheTimeFLP.Refresh();
            _notificationTheExpirationOfTheTimeTIMER.Enabled = true;
        }
        //
        //  Оформления ползунка состояния
        //
        private void _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (_onNotificationTheExpirationOfTheTime)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }
        //
        //  Анимация перемещения ползунка состояния
        //
        private void _notificationTheExpirationOfTheTimeTIMER_Tick(object sender, EventArgs e)
        {
            if (_onNotificationTheExpirationOfTheTime)
            {
                if (_pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Width + 2 != _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location.X)
                {
                    _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location = new Point(_pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location.X + 1, 0);
                }
                else
                {
                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.Text = "On";
                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.ForeColor = _mainColor;
                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.Location = new Point(2, 2);
                    _notificationTheExpirationOfTheTimeTIMER.Enabled = false;
                }

            }
            else
            {
                if (_pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location.X != 0)
                {
                    _pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location = new Point(_pointConditionChecknotificationTheExpirationOfTheTimePICTUREBOX.Location.X - 1, 0);
                }
                else
                {

                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.Text = "Off";
                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.ForeColor = Color.Red;
                    _conditionChecknotificationTheExpirationOfTheTimeLABEL.Location = new Point(_boxChecknotificationTheExpirationOfTheTimePANEL.Width - _conditionChecknotificationTheExpirationOfTheTimeLABEL.Width - 2, 2);

                    _notificationTheExpirationOfTheTimeTIMER.Enabled = false;
                }
            }
        }

        #endregion

        #region Ограничение по времени

        private void _checkedScrollTimeLimitPerDayFLP_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (_onTimeLimitPerDay)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = _boxCheckTimeLimitPerDayPANEL.Location.X;
            int y = _boxCheckTimeLimitPerDayPANEL.Location.Y;
            int width = x + _boxCheckTimeLimitPerDayPANEL.Width;
            int height = _boxCheckTimeLimitPerDayPANEL.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);
        }
        //
        //  Отрисовка ползунка состояния
        //
        private void _pointConditionCheckTimeLimitPerDayPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (_onTimeLimitPerDay)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }
        //
        //  Перемещение ползунка состояния
        //
        private void _pointConditionCheckTimeLimitPerDayPICTUREBOX_Click(object sender, EventArgs e)
        {
            if (_onTimeLimitPerDay)
            {
                _onTimeLimitPerDay = false;
            }
            else
            {
                _onTimeLimitPerDay = true;
            }
            _checkedScrollTimeLimitPerDayFLP.Refresh();
            timeLimitPerDayTIMER.Enabled = true;
        }
        //
        //  Эффект перемещения ползунка
        //
        private void timeLimitPerDayTIMER_Tick(object sender, EventArgs e)
        {
            if (_onTimeLimitPerDay)
            {
                if (_pointConditionCheckTimeLimitPerDayPICTUREBOX.Width + 2 != _pointConditionCheckTimeLimitPerDayPICTUREBOX.Location.X)
                {
                    _pointConditionCheckTimeLimitPerDayPICTUREBOX.Location = new Point(_pointConditionCheckTimeLimitPerDayPICTUREBOX.Location.X + 1, 0);
                }
                else
                {
                    _conditionCheckTimeLimitPerDayLABEL.Text = "On";
                    _conditionCheckTimeLimitPerDayLABEL.ForeColor = _mainColor;
                    _conditionCheckTimeLimitPerDayLABEL.Location = new Point(2, 2);

                    LimitForTheDayFLP.Visible = false;
                    _calendarPANEL.Visible = true;
                    _contentFourSteepFLP.Height += _calendarPANEL.Height;
                    //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentFourSteepFLP.Location.Y + _contentFourSteepFLP.Height + 10);

                    timeLimitPerDayTIMER.Enabled = false;
                }

            }
            else
            {
                if (_pointConditionCheckTimeLimitPerDayPICTUREBOX.Location.X != 0)
                {
                    _pointConditionCheckTimeLimitPerDayPICTUREBOX.Location = new Point(_pointConditionCheckTimeLimitPerDayPICTUREBOX.Location.X - 1, 0);
                }
                else
                {

                    _conditionCheckTimeLimitPerDayLABEL.Text = "Off";
                    _conditionCheckTimeLimitPerDayLABEL.ForeColor = Color.Red;
                    _conditionCheckTimeLimitPerDayLABEL.Location = new Point(_boxCheckTimeLimitPerDayPANEL.Width - _conditionCheckTimeLimitPerDayLABEL.Width - 2, 2);

                    LimitForTheDayFLP.Visible = true;
                    _calendarPANEL.Visible = false;
                    _contentFourSteepFLP.Height -= _calendarPANEL.Height;
                    //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentFourSteepFLP.Location.Y + _contentFourSteepFLP.Height + 10);

                    timeLimitPerDayTIMER.Enabled = false;
                }
            }
        }

        #endregion

        #endregion

        #region Отображение пятого этапа созданеия пользователя

        private void StyleSpyMode()
        {
            // 
            // _blockUrlLABEL
            // 
            _blockUrlLABEL.AutoSize = true;
            _blockUrlLABEL.Font = StyleWindows._mainHeaderFontFamily;
            _blockUrlLABEL.Location = new Point(3, 0);
            _blockUrlLABEL.Name = "_blockUrlLABEL";
            _blockUrlLABEL.Size = new Size(163, 23);
            _blockUrlLABEL.TabIndex = 0;
            _blockUrlLABEL.Text = "Блокировка сайтов";
            // 
            // _listBlockUrlFLP
            // 
            _listBlockUrlFLP.AutoScroll = true;
            _listBlockUrlFLP.BackColor = Color.White;
            _listBlockUrlFLP.Dock = DockStyle.Fill;
            _listBlockUrlFLP.Location = new Point(2, 27);
            _listBlockUrlFLP.Name = "_listBlockUrlFLP";
            _listBlockUrlFLP.Size = new Size(557, 159);
            _listBlockUrlFLP.TabIndex = 2;
            // 
            // _bottomBorderBlockUrlPANEL
            // 
            _bottomBorderBlockUrlPANEL.BackColor = StyleWindows._mainBorderColor;
            _bottomBorderBlockUrlPANEL.Dock = DockStyle.Bottom;
            _bottomBorderBlockUrlPANEL.Location = new Point(2, 186);
            _bottomBorderBlockUrlPANEL.Name = "_bottomBorderBlockUrlPANEL";
            _bottomBorderBlockUrlPANEL.Size = new Size(557, 2);
            _bottomBorderBlockUrlPANEL.TabIndex = 4;
            // 
            // _rightBorderBlockUrlPANEL
            // 
            _rightBorderBlockUrlPANEL.BackColor = StyleWindows._mainBorderColor;
            _rightBorderBlockUrlPANEL.Dock = DockStyle.Right;
            _rightBorderBlockUrlPANEL.Location = new Point(559, 27);
            _rightBorderBlockUrlPANEL.Name = "_rightBorderBlockUrlPANEL";
            _rightBorderBlockUrlPANEL.Size = new Size(2, 161);
            _rightBorderBlockUrlPANEL.TabIndex = 3;
            // 
            // _leftBorderBlockUrlPANEL
            // 
            this._leftBorderBlockUrlPANEL.BackColor = StyleWindows._mainBorderColor;
            this._leftBorderBlockUrlPANEL.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftBorderBlockUrlPANEL.Location = new System.Drawing.Point(0, 27);
            this._leftBorderBlockUrlPANEL.Name = "_leftBorderBlockUrlPANEL";
            this._leftBorderBlockUrlPANEL.Size = new System.Drawing.Size(2, 161);
            this._leftBorderBlockUrlPANEL.TabIndex = 1;
            // 
            // _contetntBlockUrlPANEL
            // 
            _contetntBlockUrlPANEL.Controls.Add(_listBlockUrlFLP);
            _contetntBlockUrlPANEL.Controls.Add(_bottomBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_rightBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_leftBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_backPanelBlockUrlPANEL);
            _contetntBlockUrlPANEL.Location = new Point(0, 0);
            _contetntBlockUrlPANEL.Name = "_contetntBlockUrlPANEL";
            _contetntBlockUrlPANEL.Size = new Size(561, 188);
            _contetntBlockUrlPANEL.TabIndex = 1;
            // 
            // _blockUrlTEXTBOX
            // 
            this._blockUrlTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._blockUrlTEXTBOX.Font = StyleWindows._mainFont;
            this._blockUrlTEXTBOX.Location = new System.Drawing.Point(3, 3);
            this._blockUrlTEXTBOX.Margin = new System.Windows.Forms.Padding(3, 6, 0, 3);
            this._blockUrlTEXTBOX.Name = "_blockUrlTEXTBOX";
            this._blockUrlTEXTBOX.Size = new System.Drawing.Size(428, 20);
            this._blockUrlTEXTBOX.TabIndex = 0;
            // 
            // _blockWebBUTTON
            // 
            this._blockWebBUTTON.BackColor = StyleWindows._mainColor;
            this._blockWebBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._blockWebBUTTON.FlatAppearance.BorderSize = 2;
            this._blockWebBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            this._blockWebBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._blockWebBUTTON.ForeColor = System.Drawing.Color.White;
            this._blockWebBUTTON.Location = new System.Drawing.Point(431, 0);
            this._blockWebBUTTON.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._blockWebBUTTON.Name = "_blockWebBUTTON";
            this._blockWebBUTTON.Size = new System.Drawing.Size(130, 27);
            this._blockWebBUTTON.TabIndex = 1;
            this._blockWebBUTTON.Text = "Добавить сайт";
            this._blockWebBUTTON.UseVisualStyleBackColor = false;
            this._blockWebBUTTON.Click += new System.EventHandler(this._blockWebBUTTON_Click);
            // 
            // _backPanelBlockUrlPANEL
            // 
            this._backPanelBlockUrlPANEL.BackColor = System.Drawing.Color.White;
            this._backPanelBlockUrlPANEL.Controls.Add(this._blockUrlTEXTBOX);
            this._backPanelBlockUrlPANEL.Controls.Add(this._blockWebBUTTON);
            this._backPanelBlockUrlPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._backPanelBlockUrlPANEL.Location = new System.Drawing.Point(0, 0);
            this._backPanelBlockUrlPANEL.Name = "_backPanelBlockUrlPANEL";
            this._backPanelBlockUrlPANEL.Size = new System.Drawing.Size(561, 27);
            this._backPanelBlockUrlPANEL.TabIndex = 2;
            this._backPanelBlockUrlPANEL.Paint += new System.Windows.Forms.PaintEventHandler(this._backPanelBlockUrlPANEL_Paint);
            // 
            // _nameBlockProgramLABEL
            // 
            this._nameBlockProgramLABEL.AutoSize = true;
            this._nameBlockProgramLABEL.Font = StyleWindows._mainHeaderFontFamily;
            this._nameBlockProgramLABEL.Location = new System.Drawing.Point(3, 217);
            this._nameBlockProgramLABEL.Name = "_nameBlockProgramLABEL";
            this._nameBlockProgramLABEL.Size = new System.Drawing.Size(212, 23);
            this._nameBlockProgramLABEL.TabIndex = 2;
            this._nameBlockProgramLABEL.Text = "Блокировка приложений";
            // 
            // _listBlockProgramFLP
            // 
            this._listBlockProgramFLP.AutoScroll = true;
            this._listBlockProgramFLP.BackColor = System.Drawing.Color.White;
            this._listBlockProgramFLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBlockProgramFLP.Location = new System.Drawing.Point(2, 28);
            this._listBlockProgramFLP.Name = "_listBlockProgramFLP";
            this._listBlockProgramFLP.Size = new System.Drawing.Size(554, 162);
            this._listBlockProgramFLP.TabIndex = 3;
            // 
            // _rightBorderBlockProgram
            // 
            this._rightBorderBlockProgram.BackColor = StyleWindows._mainBorderColor;
            this._rightBorderBlockProgram.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightBorderBlockProgram.Location = new System.Drawing.Point(556, 28);
            this._rightBorderBlockProgram.Name = "_rightBorderBlockProgram";
            this._rightBorderBlockProgram.Size = new System.Drawing.Size(2, 162);
            this._rightBorderBlockProgram.TabIndex = 6;
            // 
            // _topBorderBlockProgram
            // 
            this._topBorderBlockProgram.BackColor = StyleWindows._mainBorderColor;
            this._topBorderBlockProgram.Dock = System.Windows.Forms.DockStyle.Top;
            this._topBorderBlockProgram.Location = new System.Drawing.Point(2, 26);
            this._topBorderBlockProgram.Name = "_topBorderBlockProgram";
            this._topBorderBlockProgram.Size = new System.Drawing.Size(556, 2);
            this._topBorderBlockProgram.TabIndex = 8;
            // 
            // _leftBorderBlockProgram
            // 
            this._leftBorderBlockProgram.BackColor = StyleWindows._mainBorderColor;
            this._leftBorderBlockProgram.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftBorderBlockProgram.Location = new System.Drawing.Point(0, 26);
            this._leftBorderBlockProgram.Name = "_leftBorderBlockProgram";
            this._leftBorderBlockProgram.Size = new System.Drawing.Size(2, 164);
            this._leftBorderBlockProgram.TabIndex = 7;
            // 
            // _bottomBorderBlockProgram
            // 
            this._bottomBorderBlockProgram.BackColor = StyleWindows._mainBorderColor;
            this._bottomBorderBlockProgram.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomBorderBlockProgram.Location = new System.Drawing.Point(0, 190);
            this._bottomBorderBlockProgram.Name = "_bottomBorderBlockProgram";
            this._bottomBorderBlockProgram.Size = new System.Drawing.Size(558, 2);
            this._bottomBorderBlockProgram.TabIndex = 5;
            // 
            // _nameBlockProgramPANEL
            // 
            this._nameBlockProgramPANEL.BackColor = System.Drawing.Color.Transparent;
            this._nameBlockProgramPANEL.Controls.Add(this._blockProgramBUTTON);
            this._nameBlockProgramPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._nameBlockProgramPANEL.Location = new System.Drawing.Point(0, 0);
            this._nameBlockProgramPANEL.Name = "_nameBlockProgramPANEL";
            this._nameBlockProgramPANEL.Size = new System.Drawing.Size(558, 26);
            this._nameBlockProgramPANEL.TabIndex = 1;
            // 
            // _backPanelBlockProgramPANEL
            // 
            this._backPanelBlockProgramPANEL.Controls.Add(this._listBlockProgramFLP);
            this._backPanelBlockProgramPANEL.Controls.Add(this._rightBorderBlockProgram);
            this._backPanelBlockProgramPANEL.Controls.Add(this._topBorderBlockProgram);
            this._backPanelBlockProgramPANEL.Controls.Add(this._leftBorderBlockProgram);
            this._backPanelBlockProgramPANEL.Controls.Add(this._bottomBorderBlockProgram);
            this._backPanelBlockProgramPANEL.Controls.Add(this._nameBlockProgramPANEL);
            this._backPanelBlockProgramPANEL.Location = new System.Drawing.Point(3, 243);
            this._backPanelBlockProgramPANEL.Name = "_backPanelBlockProgramPANEL";
            this._backPanelBlockProgramPANEL.Size = new System.Drawing.Size(558, 192);
            this._backPanelBlockProgramPANEL.TabIndex = 3;
            // 
            // _blockProgramBUTTON
            // 
            this._blockProgramBUTTON.BackColor = StyleWindows._mainColor;
            this._blockProgramBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            this._blockProgramBUTTON.FlatAppearance.BorderSize = 2;
            this._blockProgramBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            this._blockProgramBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._blockProgramBUTTON.ForeColor = System.Drawing.Color.White;
            this._blockProgramBUTTON.Location = new System.Drawing.Point(0, 0);
            this._blockProgramBUTTON.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._blockProgramBUTTON.Name = "_blockProgramBUTTON";
            this._blockProgramBUTTON.Size = new System.Drawing.Size(151, 27);
            this._blockProgramBUTTON.TabIndex = 2;
            this._blockProgramBUTTON.Text = "Добавить приложение";
            this._blockProgramBUTTON.UseVisualStyleBackColor = false;
            this._blockProgramBUTTON.Click += new System.EventHandler(this._blockProgramBUTTON_Click);
            // 
            // _snapshotFrequencyNUMUPDOWN
            // 
            this._snapshotFrequencyNUMUPDOWN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._snapshotFrequencyNUMUPDOWN.Font = StyleWindows._mainFont;
            this._snapshotFrequencyNUMUPDOWN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._snapshotFrequencyNUMUPDOWN.Location = new System.Drawing.Point(4, _snapshotFrequencyLABEL.Height + 10);
            this._snapshotFrequencyNUMUPDOWN.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this._snapshotFrequencyNUMUPDOWN.Name = "_snapshotFrequencyNUMUPDOWN";
            this._snapshotFrequencyNUMUPDOWN.Size = new System.Drawing.Size(61, 23);
            this._snapshotFrequencyNUMUPDOWN.TabIndex = 35;
            this._snapshotFrequencyNUMUPDOWN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._snapshotFrequencyNUMUPDOWN.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _recordVideoLABEL
            // 
            this._recordVideoLABEL.AutoSize = true;
            this._recordVideoLABEL.Font = StyleWindows._mainFont;
            this._recordVideoLABEL.Location = new System.Drawing.Point(0, 2);
            this._recordVideoLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._recordVideoLABEL.Name = "_recordVideoLABEL";
            this._recordVideoLABEL.Size = new System.Drawing.Size(103, 19);
            this._recordVideoLABEL.TabIndex = 0;
            this._recordVideoLABEL.Text = "Запись видео";
            // 
            // _boxCheckRecordVideoPANEL
            // 
            this._boxCheckRecordVideoPANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckRecordVideoPANEL.Controls.Add(this._pointConditionCheckRecordVideoPICTUREBOX);
            this._boxCheckRecordVideoPANEL.Controls.Add(this._conditionCheckRecordVideoLABEL);
            this._boxCheckRecordVideoPANEL.Location = new System.Drawing.Point(109, 3);
            this._boxCheckRecordVideoPANEL.Name = "_boxCheckRecordVideoPANEL";
            this._boxCheckRecordVideoPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckRecordVideoPANEL.TabIndex = 0;
            // 
            // _checkedScrollRecordVideoFLP
            // 
            //this._checkedScrollRecordVideoFLP.AutoSize = true;
            this._checkedScrollRecordVideoFLP.Controls.Add(this._recordVideoLABEL);
            this._checkedScrollRecordVideoFLP.Controls.Add(this._boxCheckRecordVideoPANEL);
            this._checkedScrollRecordVideoFLP.Location = new System.Drawing.Point(7, 3);
            this._checkedScrollRecordVideoFLP.Name = "_checkedScrollRecordVideoFLP";
            this._checkedScrollRecordVideoFLP.Size = new System.Drawing.Size(186, 24);
            this._checkedScrollRecordVideoFLP.TabIndex = 31;
            this._checkedScrollRecordVideoFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollRecordVideoFLP_Paint);
            // 
            // _pointConditionCheckRecordVideoPICTUREBOX
            // 
            this._pointConditionCheckRecordVideoPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckRecordVideoPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckRecordVideoPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckRecordVideoPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckRecordVideoPICTUREBOX.Name = "_pointConditionCheckRecordVideoPICTUREBOX";
            this._pointConditionCheckRecordVideoPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckRecordVideoPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckRecordVideoPICTUREBOX.TabStop = false;
            this._pointConditionCheckRecordVideoPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckRecordVideoPICTUREBOX_Click);
            this._pointConditionCheckRecordVideoPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckRecordVideoPICTUREBOX_Paint);
            // 
            // _conditionCheckRecordVideoLABEL
            // 
            this._conditionCheckRecordVideoLABEL.AutoSize = true;
            this._conditionCheckRecordVideoLABEL.Font = StyleWindows._sideFont;
            this._conditionCheckRecordVideoLABEL.ForeColor = System.Drawing.Color.Red;
            this._conditionCheckRecordVideoLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckRecordVideoLABEL.Name = "_conditionCheckRecordVideoLABEL";
            this._conditionCheckRecordVideoLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckRecordVideoLABEL.TabIndex = 7;
            this._conditionCheckRecordVideoLABEL.Text = "Off";
            // 
            // _snapshotFrequencyLABEL
            // 
            this._snapshotFrequencyLABEL.AutoSize = true;
            this._snapshotFrequencyLABEL.Font = StyleWindows._mainFont;
            this._snapshotFrequencyLABEL.Location = new System.Drawing.Point(0, 0);
            this._snapshotFrequencyLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._snapshotFrequencyLABEL.Name = "_snapshotFrequencyLABEL";
            this._snapshotFrequencyLABEL.Size = new System.Drawing.Size(244, 19);
            this._snapshotFrequencyLABEL.TabIndex = 34;
            this._snapshotFrequencyLABEL.Text = "Частота сохранения изображения";
            // 
            // _screenShootLABEL
            // 
            this._screenShootLABEL.AutoSize = true;
            this._screenShootLABEL.Font = StyleWindows._mainFont;
            this._screenShootLABEL.Location = new System.Drawing.Point(0, 2);
            this._screenShootLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._screenShootLABEL.Name = "_screenShootLABEL";
            this._screenShootLABEL.Size = new System.Drawing.Size(113, 19);
            this._screenShootLABEL.TabIndex = 0;
            this._screenShootLABEL.Text = "Снимок экрана";
            // 
            // _boxCheckScreenShootPANEL
            // 
            this._boxCheckScreenShootPANEL.BackColor = System.Drawing.Color.White;
            this._boxCheckScreenShootPANEL.Controls.Add(this._pointConditionCheckScreenShootPICTUREBOX);
            this._boxCheckScreenShootPANEL.Controls.Add(this._conditionCheckScreenShootLABEL);
            this._boxCheckScreenShootPANEL.Location = new System.Drawing.Point(119, 3);
            this._boxCheckScreenShootPANEL.Name = "_boxCheckScreenShootPANEL";
            this._boxCheckScreenShootPANEL.Size = new System.Drawing.Size(40, 18);
            this._boxCheckScreenShootPANEL.TabIndex = 0;
            // 
            // _pointConditionCheckScreenShootPICTUREBOX
            // 
            this._pointConditionCheckScreenShootPICTUREBOX.BackColor = System.Drawing.Color.Transparent;
            this._pointConditionCheckScreenShootPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._pointConditionCheckScreenShootPICTUREBOX.Location = new System.Drawing.Point(0, 0);
            this._pointConditionCheckScreenShootPICTUREBOX.Margin = new System.Windows.Forms.Padding(0);
            this._pointConditionCheckScreenShootPICTUREBOX.Name = "_pointConditionCheckScreenShootPICTUREBOX";
            this._pointConditionCheckScreenShootPICTUREBOX.Size = new System.Drawing.Size(20, 20);
            this._pointConditionCheckScreenShootPICTUREBOX.TabIndex = 0;
            this._pointConditionCheckScreenShootPICTUREBOX.TabStop = false;
            this._pointConditionCheckScreenShootPICTUREBOX.Click += new System.EventHandler(this._pointConditionCheckScreenShootPICTUREBOX_Click);
            this._pointConditionCheckScreenShootPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._pointConditionCheckScreenShootPICTUREBOX_Paint);
            // 
            // _conditionCheckScreenShootLABEL
            // 
            this._conditionCheckScreenShootLABEL.AutoSize = true;
            this._conditionCheckScreenShootLABEL.Font = StyleWindows._sideFont;
            this._conditionCheckScreenShootLABEL.Location = new System.Drawing.Point(16, 2);
            this._conditionCheckScreenShootLABEL.Name = "_conditionCheckScreenShootLABEL";
            this._conditionCheckScreenShootLABEL.Size = new System.Drawing.Size(22, 14);
            this._conditionCheckScreenShootLABEL.TabIndex = 7;
            this._conditionCheckScreenShootLABEL.Text = "Off";
            // 
            // _checkedScrollScreenShootFLP
            // 
            this._checkedScrollScreenShootFLP.AutoSize = true;
            this._checkedScrollScreenShootFLP.Controls.Add(this._screenShootLABEL);
            this._checkedScrollScreenShootFLP.Controls.Add(this._boxCheckScreenShootPANEL);
            this._checkedScrollScreenShootFLP.Location = new System.Drawing.Point(7, _checkedScrollRecordVideoFLP.Location.Y + _checkedScrollRecordVideoFLP.Height + 5);
            this._checkedScrollScreenShootFLP.Name = "_checkedScrollScreenShootFLP";
            this._checkedScrollScreenShootFLP.Size = new System.Drawing.Size(186, 24);
            this._checkedScrollScreenShootFLP.TabIndex = 33;
            this._checkedScrollScreenShootFLP.Paint += new System.Windows.Forms.PaintEventHandler(this._checkedScrollScreenShootFLP_Paint);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Text files(*.exe) | *exe";
            // 
            // _recordVideoTIMER
            // 
            this._recordVideoTIMER.Interval = 1;
            this._recordVideoTIMER.Tick += new System.EventHandler(this._recordVideoTIMER_Tick);
            // 
            // _recordAudioTIMER
            // 
            this._recordAudioTIMER.Interval = 1;
            this._recordAudioTIMER.Tick += new System.EventHandler(this._recordAudioTIMER_Tick);
            // 
            // _screenShootTIMER
            // 
            this._screenShootTIMER.Interval = 1;
            this._screenShootTIMER.Tick += new System.EventHandler(this._screenShootTIMER_Tick);
            // 
            // _snapshotFrequencyInSecondsLABEL
            // 
            this._snapshotFrequencyInSecondsLABEL.AutoSize = true;
            this._snapshotFrequencyInSecondsLABEL.Font = StyleWindows._mainFont;
            this._snapshotFrequencyInSecondsLABEL.Location = new System.Drawing.Point(_snapshotFrequencyNUMUPDOWN.Location.X + _snapshotFrequencyNUMUPDOWN.Width + 5, _snapshotFrequencyNUMUPDOWN.Location.Y);
            this._snapshotFrequencyInSecondsLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._snapshotFrequencyInSecondsLABEL.Name = "_snapshotFrequencyInSecondsLABEL";
            this._snapshotFrequencyInSecondsLABEL.Size = new System.Drawing.Size(62, 19);
            this._snapshotFrequencyInSecondsLABEL.TabIndex = 36;
            this._snapshotFrequencyInSecondsLABEL.Text = "Секнды";
            // 
            // _snapshotFrequencyPANEL
            // 
            this._snapshotFrequencyPANEL.Controls.Add(this._snapshotFrequencyInSecondsLABEL);
            this._snapshotFrequencyPANEL.Controls.Add(this._snapshotFrequencyLABEL);
            this._snapshotFrequencyPANEL.Controls.Add(this._snapshotFrequencyNUMUPDOWN);
            this._snapshotFrequencyPANEL.Location = new System.Drawing.Point(7, _checkedScrollScreenShootFLP.Location.Y + _checkedScrollScreenShootFLP.Height + 10);
            this._snapshotFrequencyPANEL.Name = "_snapshotFrequencyPANEL";
            this._snapshotFrequencyPANEL.Size = new System.Drawing.Size(_snapshotFrequencyLABEL.Width + 10, _snapshotFrequencyNUMUPDOWN.Location.Y + _snapshotFrequencyNUMUPDOWN.Height + 10);
            this._snapshotFrequencyPANEL.TabIndex = 1;
            this._snapshotFrequencyPANEL.Paint += new System.Windows.Forms.PaintEventHandler(this._snapshotFrequencyPANEL_Paint);
            // 
            // _effectOpenAndCloseSnapFreqTIMER
            // 
            this._effectOpenAndCloseSnapFreqTIMER.Interval = 1;
            this._effectOpenAndCloseSnapFreqTIMER.Tick += new System.EventHandler(this._effectOpenAndCloseSnapFreqTIMER_Tick);
            // 
            // _controlPanelFLP
            // 
            _controlPanelFLP.Controls.Add(_blockUrlLABEL);
            _controlPanelFLP.Controls.Add(_contetntBlockUrlPANEL);
            _controlPanelFLP.Controls.Add(_nameBlockProgramLABEL);
            _controlPanelFLP.Controls.Add(_backPanelBlockProgramPANEL);
            _controlPanelFLP.Controls.Add(_panelCheckBoxSpyModePANEL);
            _controlPanelFLP.Name = "_controlPanelFLP";
            _controlPanelFLP.Size = new Size(564, 630);
            _controlPanelFLP.Location = new Point(this.Width / 2 - _controlPanelFLP.Width / 2, 0);
            _controlPanelFLP.TabIndex = 0;
            // 
            // _panelCheckBoxSpyModePANEL
            // 
            this._panelCheckBoxSpyModePANEL.Controls.Add(this._checkedScrollScreenShootFLP);
            this._panelCheckBoxSpyModePANEL.Controls.Add(this._checkedScrollRecordVideoFLP);
            this._panelCheckBoxSpyModePANEL.Controls.Add(this._snapshotFrequencyPANEL);
            this._panelCheckBoxSpyModePANEL.Location = new System.Drawing.Point(3, _listBlockProgramFLP.Location.Y + _listBlockProgramFLP.Height);
            this._panelCheckBoxSpyModePANEL.Name = "_panelCheckBoxSpyModePANEL";
            this._panelCheckBoxSpyModePANEL.TabIndex = 1;
            this._panelCheckBoxSpyModePANEL.AutoSize = true;
            this._panelCheckBoxSpyModePANEL.Size = new Size(_controlPanelFLP.Width, _snapshotFrequencyPANEL.Height + _snapshotFrequencyPANEL.Location.Y);

            _heightSnapshotFrequency = _snapshotFrequencyPANEL.Height;
            _snapshotFrequencyPANEL.Height = 0;

            _nameBlockProgramLABEL.Margin = new Padding(_controlPanelFLP.Width / 2 - _nameBlockProgramLABEL.Width / 2, 3, 3, 3);
            _blockUrlLABEL.Margin = new Padding(_controlPanelFLP.Width / 2 - _blockUrlLABEL.Width / 2, 3, 3, 3);
            ToolBox.SetRoundedShape(_boxCheckRecordVideoPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckRecordAudioPANEL, radius);
            ToolBox.SetRoundedShape(_boxCheckScreenShootPANEL, radius);
            _contentStepsPANEL.Controls.Add(_controlPanelFLP);
        }
        #region Блокировка приложений

        #region Блокировка сайта

        private void _blockWebBUTTON_Click(object sender, EventArgs e)
        {
            if (CheckForTheCorrectUrl(_blockUrlTEXTBOX.Text))
            {
                _blockWebSite(_blockUrlTEXTBOX.Text, _blockUrl, _listBlockUrlFLP);
                _blockUrlTEXTBOX.Text = "";
            }
            else
                Error.ClientError.BadRequest();
        }
        public bool CheckForTheCorrectUrl(string value)
        {
            bool maybeUrl = false;
            if (value.Length < 4) return false;
            foreach (char letter in value)
            {
                if (letter == ' ') return false;
                if (letter == '.') maybeUrl = true;
            }

            return maybeUrl;

        }
        //
        // Функция блокировки сайта
        //
        public void _blockWebSite(string url, List<Control> controls, FlowLayoutPanel layoutPanel)
        {
            _url.Add(url);

            Control value = CreatePanelBlockUrl(url, _foreColor, controls, layoutPanel, true);
            _listBlockUrlFLP.Controls.Add(value);
            _blockUrl.Add(value);
            if (value.Location.Y + value.Height > _listBlockUrlFLP.Height)
            {
                foreach (Control elements in _blockUrl)
                {
                    elements.Size = new Size(_listBlockUrlFLP.Width - 30, elements.Height);
                }
            }
        }
        //
        //  Создание формы заблокированного сайта
        //
        public Control CreatePanelBlockUrl(string url, Color foreColor, List<Control> control, Control contentFLP, bool createUser)
        {
            int padding = 3;
            int numberElement = control.Count;
            int heightPanel = 40;

            //
            //  panel
            //
            Panel panel = new Panel()
            {
                BackColor = _mainColor,
                Name = "panel_" + numberElement,
                Size = new Size(contentFLP.Width - padding * 2, heightPanel),
                Margin = new Padding(padding),
            };
            //
            //  pictureBox
            //
            PictureBox pictureBox = new PictureBox()
            {
                Size = new Size(15, 15),
                BackColor = Color.Transparent,
                Name = "pictureBox_" + numberElement,
                Cursor = Cursors.Hand,
                Location = new Point(0, panel.Height / 2 - 15 / 2),

            };
            pictureBox.Paint += BlockUrlPICTUREBOX_Paint;
            pictureBox.Click += BlockUrlPICTUREBOX_Click;
            //
            //  controlUrl
            //
            Panel controlUrl = new Panel()
            {
                Dock = DockStyle.Right,
                Width = pictureBox.Width + padding * 3
            };

            Label label = new Label()
            {
                Name = "label_" + numberElement,
                AutoSize = true,
                Text = url,
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Font = StyleWindows._mainFont,
                MaximumSize = new Size(panel.Width - controlUrl.Width - 40, 256),
            };
            //aaaaaaaaaaaaaaaaa.com
            Size size = TextRenderer.MeasureText(label.Text, label.Font);
            MessageBox.Show(size.ToString() + '\n' + panel.Size.ToString());
            panel.Height = (size.Width > panel.Width - controlUrl.Width) ? (panel.Height * (size.Width / (panel.Width - controlUrl.Width - 40)) + 2) + 10: (panel.Height);


            controlUrl.Controls.Add(pictureBox);
            panel.Controls.Add(controlUrl);
            panel.Controls.Add(label);

            return panel;
        }

        public int ScanNumberInBlockUrlList(string name)
        {
            bool isNumber = false;
            string number = "";
            foreach (char value in name)
            {
                if (isNumber) number += value;
                else
                    if (value == '_') isNumber = true;
            }

            return Convert.ToInt32(number);
        }

        private void BlockUrlPICTUREBOX_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            // Выделение номера picturebox
            int numberPictireBox = ScanNumberInBlockUrlList(pictureBox.Name);
            //  Поиск панели
            for (int track = 0; track < _blockUrl.Count; track++)
            {
                if (numberPictireBox == ScanNumberInBlockUrlList(_blockUrl[track].Name))
                {
                    _listBlockUrlFLP.Controls.Remove(_blockUrl[track]);
                    _blockUrl.RemoveAt(track);
                    _url.RemoveAt(track);
                    break;
                }
            }

        }

        //
        //  Отрисовка кнопки удаления добавленного сайта
        //
        private void BlockUrlPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.Red, 2.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.DrawLine(p, 0, 0, 15, 15);
            gr.DrawLine(p, 15, 0, 0, 15);
        }

        private void _backPanelBlockUrlPANEL_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(_mainColor, 7.0f);
            graphics.DrawRectangle(pen, _blockUrlTEXTBOX.Location.X - 1, _blockUrlTEXTBOX.Location.Y - 1, _blockUrlTEXTBOX.Width + 1, _blockUrlTEXTBOX.Height + 1);
        }
        #endregion

        public bool CheckForRepeatedApplicationLock(string name)
        {
            foreach (string value in _program)
                if (value == name) return false;

            return true;
        }
        //
        //  Блокировка приложения при нажатии на кнопку
        //
        private void _blockProgramBUTTON_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                string way = openFileDialog.FileName;

                string nameProgram = GetTheProgramName(way);
                if (CheckForRepeatedApplicationLock(nameProgram))
                {
                    Image icon = IconPrograms(way);
                    Control value = CreatePanelBlockProgram(nameProgram, _foreColor, _blockPrograms, _listBlockProgramFLP, icon, true);
                    _blockPrograms.Add(value);
                    _program.Add(nameProgram);
                    _listBlockProgramFLP.Controls.Add(value);
                }
                else
                    MessageBox.Show("Данная программа уже встречается в списке", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //
        //  Получение иконки приложений
        //
        public Image IconPrograms(string way)
        {
            return Icon.ExtractAssociatedIcon(way).ToBitmap();
        }
        //
        //  Получение имени программы
        //
        public string GetTheProgramName(string way)
        {
            FileInfo file = new FileInfo(way);
            return file.Name;
        }
        public Control CreatePanelBlockProgram(string nameProgram, Color foreColor, List<Control> control, Control contentFLP, Image icon, bool createUser)
        {
            int padding = 3;
            int numberElement = control.Count;
            int heightPanel = 40;
            Panel panel = new Panel();
            panel.BackColor = _mainColor;
            panel.Name = "panel_" + numberElement;
            panel.Size = new Size(contentFLP.Width - padding * 2, heightPanel);
            panel.Margin = new Padding(padding);

            PictureBox iconProgram = new PictureBox()
            {
                Name = "icon_" + numberElement,
                BackColor = Color.Transparent,
                Size = new Size(25, 25),
                Location = new Point(padding * 5, panel.Height / 2 - 25 / 2),
                BackgroundImage = icon,
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            Label label = new Label();
            label.Name = "label_" + numberElement;
            label.AutoSize = true;
            label.Text = nameProgram;
            label.ForeColor = foreColor;
            label.BackColor = Color.Transparent;
            label.Font = _mainFont;
            label.Location = new Point(iconProgram.Location.X + iconProgram.Width + padding * 3, panel.Height / 2 - label.Height / 2);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(15, 15);
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Name = "pictureBox_" + numberElement;
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Location = new Point(panel.Width - padding * 3 - pictureBox.Width, panel.Height / 2 - pictureBox.Height / 2);
            pictureBox.Paint += BlockUrlPICTUREBOX_Paint;
            pictureBox.Click += BlockProgramPICTUREBOX_Click;

            panel.Controls.Add(iconProgram);
            panel.Controls.Add(label);
            panel.Controls.Add(pictureBox);

            return panel;
        }
        private void BlockProgramPICTUREBOX_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            bool isNumber = false;
            string number = "";
            // Получение номера объекта
            foreach (char value in pictureBox.Name)
            {
                if (isNumber) number += value;
                else
                    if (value == '_') isNumber = true;
            }
            int numberConvert = Convert.ToInt32(number);

            if (numberConvert == _blockPrograms.Count)
            {
                _listBlockProgramFLP.Controls.Remove(_blockPrograms[numberConvert - 1]);
                _blockPrograms.RemoveAt(numberConvert - 1);
                _program.RemoveAt(numberConvert - 1);
            }
            else
            {
                _listBlockProgramFLP.Controls.Remove(_blockPrograms[numberConvert]);
                _blockPrograms.RemoveAt(numberConvert);
                _program.RemoveAt(numberConvert);
            }
        }


        //
        //  Отрисовка индикатора состояния CheckBox
        //
        private void PointConditionCheckBoxGraphics(object sender, PaintEventArgs e, bool check)
        {
            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (check)
            {
                brush = new SolidBrush(_mainColor);
            }
            else
            {
                brush = new SolidBrush(_offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }
        //
        //  Отрисовка объекта CheckBox
        //
        private void CheckedShroolCheckBoxGraphics(bool positionCheck, Control control, object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (positionCheck)
            {
                p = new Pen(_mainColor, 2.0f);
            }
            else
            {
                p = new Pen(_offColor, 2.0f);
            }
            int x = control.Location.X;
            int y = control.Location.Y;
            int width = x + control.Width;
            int height = control.Height;
            int padding = 1;


            gr.DrawLine(p, x + (radius / 2) - padding, y - padding, width - (radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - radius + padding, y - padding, radius, radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (radius / 2) - 3, width + padding, y + height - (radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - radius + padding, y + height - radius + padding, radius, radius, 360, 90);                       //
            gr.DrawLine(p, width - radius / 2 + padding, y + height + padding, x + (radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - radius + padding, radius, radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, radius, radius, 180, 90);                                                      //
        }
        //
        //  Функции обводки CheckBox
        //
        private void _checkedScrollRecordVideoFLP_Paint(object sender, PaintEventArgs e) => CheckedShroolCheckBoxGraphics(_onRecordVideo, _boxCheckRecordVideoPANEL, sender, e);

        private void _checkedScrollRecordAudioFLP_Paint(object sender, PaintEventArgs e) => CheckedShroolCheckBoxGraphics(_onRecordAudio, _boxCheckRecordAudioPANEL, sender, e);

        private void _checkedScrollScreenShootFLP_Paint(object sender, PaintEventArgs e) => CheckedShroolCheckBoxGraphics(_onScreenShoot, _boxCheckScreenShootPANEL, sender, e);
        //
        //  Функции отрисовки ползунка состояния CheckBox
        //
        private void _pointConditionCheckRecordVideoPICTUREBOX_Paint(object sender, PaintEventArgs e) => PointConditionCheckBoxGraphics(sender, e, _onRecordVideo);

        private void _pointConditionCheckRecordAudioPICTUREBOX_Paint(object sender, PaintEventArgs e) => PointConditionCheckBoxGraphics(sender, e, _onRecordAudio);

        private void _pointConditionCheckScreenShootPICTUREBOX_Paint(object sender, PaintEventArgs e) => PointConditionCheckBoxGraphics(sender, e, _onScreenShoot);
        //
        //  Функции изменяющая цвет CheckBox при нажатии
        //
        private void _pointConditionCheckRecordVideoPICTUREBOX_Click(object sender, EventArgs e)
        {
            _onRecordVideo = (_onRecordVideo) ? false : true;
            _checkedScrollRecordVideoFLP.Refresh();
            _recordVideoTIMER.Enabled = true;
        }

        private void _pointConditionCheckRecordAudioPICTUREBOX_Click(object sender, EventArgs e)
        {
            _onRecordAudio = (_onRecordAudio) ? false : true;
            _checkedScrollRecordAudioFLP.Refresh();
            _recordAudioTIMER.Enabled = true;
        }

        private void _pointConditionCheckScreenShootPICTUREBOX_Click(object sender, EventArgs e)
        {
            //  _effectOpenAndCloseSnapFreqTIMER
            if (_onScreenShoot)
            {
                _onScreenShoot = false;
                _onEffectOpenAndCloseSnapFreq = true;
            }
            else
            {
                _onScreenShoot = true;
                _onEffectOpenAndCloseSnapFreq = false;
            }
            _checkedScrollScreenShootFLP.Refresh();
            _screenShootTIMER.Enabled = true;
            _effectOpenAndCloseSnapFreqTIMER.Enabled = true;
        }
        //
        //  Анимация перемещения ползунка состояния
        //
        private void _recordVideoTIMER_Tick(object sender, EventArgs e) => MovingTheStatusSlider(_onRecordVideo, _pointConditionCheckRecordVideoPICTUREBOX, _conditionCheckRecordVideoLABEL, _boxCheckRecordVideoPANEL, _recordVideoTIMER);
        private void _recordAudioTIMER_Tick(object sender, EventArgs e) => MovingTheStatusSlider(_onRecordAudio, _pointConditionCheckRecordAudioPICTUREBOX, _conditionCheckRecordAudioLABEL, _boxCheckRecordAudioPANEL, _recordAudioTIMER);
        private void _screenShootTIMER_Tick(object sender, EventArgs e) => MovingTheStatusSlider(_onScreenShoot, _pointConditionCheckScreenShootPICTUREBOX, _conditionCheckScreenShootLABEL, _boxCheckScreenShootPANEL, _screenShootTIMER);
        //
        //  Функция перемещения ползунка
        //
        private void MovingTheStatusSlider(bool check, Control pointCondition, Control conditionCheck, Control boxCheck, System.Windows.Forms.Timer timer)
        {
            if (check)
            {
                // Функция включается
                if (pointCondition.Width + 2 != pointCondition.Location.X)
                    pointCondition.Location = new Point(pointCondition.Location.X + 1, 0);
                else
                {
                    conditionCheck.Text = "On";
                    conditionCheck.ForeColor = _mainColor;
                    conditionCheck.Location = new Point(2, 2);
                    timer.Enabled = false;
                }// Ползунок встал на свое место

            }
            else
            {
                if (pointCondition.Location.X != 0)
                    pointCondition.Location = new Point(pointCondition.Location.X - 1, 0);
                else
                {

                    conditionCheck.Text = "Off";
                    conditionCheck.ForeColor = Color.Red;
                    conditionCheck.Location = new Point(boxCheck.Width - conditionCheck.Width - 2, 2);

                    timer.Enabled = false;
                }// Ползунок встал на свое место
            } // Если функция отключается
        }

        #endregion

        private void AppearanceOfSubItems(bool check, Control control, int heightMaxVolume, System.Windows.Forms.Timer timer)
        {
            if (check)
            {
                if (control.Height != 0)
                {
                    control.Height--;
                }
                else
                {
                    timer.Enabled = false;
                }
            }
            else
            {
                if (control.Height != heightMaxVolume)
                {
                    control.Height++;
                }
                else
                {
                    timer.Enabled = false;
                }
            }
        }
        private void _effectOpenAndCloseSnapFreqTIMER_Tick(object sender, EventArgs e) => AppearanceOfSubItems(_onEffectOpenAndCloseSnapFreq, _snapshotFrequencyPANEL, _heightSnapshotFrequency, _effectOpenAndCloseSnapFreqTIMER);

        private void _snapshotFrequencyPANEL_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Pen pen = new Pen(_mainColor, 3.0f);
            graphics.DrawRectangle(pen, _snapshotFrequencyNUMUPDOWN.Location.X - 1, _snapshotFrequencyNUMUPDOWN.Location.Y - 1, _snapshotFrequencyNUMUPDOWN.Width + 1, _snapshotFrequencyNUMUPDOWN.Height + 1);

        }
        #endregion

        private void _parantAccountBUTTON_Click(object sender, EventArgs e)
        {
            TEMPUSER.Access = 1;
            ViewCountStep(0);
            NewPositionStageFLP();

            _steeps++;
            _nameSteepLABEL.Text = _steepTwo;
            LocationNameSteep();
            _contentStepsPANEL.Controls.Remove(_panelSwitchAccountPANEL);
            CreateStyleSecretDataUser();
            _contentStepsPANEL.Controls.Add(_contentPANEL);
            // Размещение кнопок управления
            //_stepsPageButtonsFLP.Location = new Point(_contentStepsPANEL.Width - _stepsPageButtonsFLP.Width - 20, _contentPANEL.Location.Y + _contentPANEL.Height + 10);

            _stepsPageButtonsFLP.Visible = true;
        }

        #region Отрисовка шагов во время регистрации пользователя

        private Pen ThisColorPen(Pen p, int step, float sizeLine)
        {
            if (_steeps == step)
            {
                p = new Pen(_nowIsStage, sizeLine);
            }
            else if (_steeps > step)
            {
                p = new Pen(_thisStageHasPassed, sizeLine);
            }
            else
            {
                p = new Pen(_thisStageHasNotPassed, sizeLine);
            }
            return p;
        }
        private void CreateLineSteps(object sender, PaintEventArgs e, int width, int height, int step, Graphics gr)
        {
            float sizeLine = 3.0F;
            Pen p = new Pen(StyleWindows._mainColor, sizeLine);
            ThisColorPen(p, step, sizeLine);

            gr.DrawLine(p, 1, width / 2, height / 3 + 1, height / 2);
            gr.DrawLine(p, width / 3 + sizeLine, height / 2, width - 1, height / 2);
        }

        private void CreateElepseSteps(object sender, PaintEventArgs e, int width, int height, string step, Graphics gr)
        {
            gr.SmoothingMode = SmoothingMode.HighQuality;
            int stage = Convert.ToInt32(step);
            float sizeLine = 3.0F;
            Pen p = new Pen(StyleWindows._mainColor, sizeLine);
            p = ThisColorPen(p, stage, sizeLine);

            gr.DrawEllipse(p, 1, 1, width - sizeLine, height - sizeLine);
            // Создание номера этапа
            Font font = new Font(StyleWindows._mainFont.FontFamily, 16.0F, FontStyle.Bold);
            // Узнаем занимаемый размер
            SizeF sizeText = gr.MeasureString(step, font);
            StringFormat drawFormat = new StringFormat();

            float widthText = sizeText.Width;
            float heightText = sizeText.Height;
            gr.DrawString(step, font, new SolidBrush(p.Color), width / 2 - widthText / 2, height / 2 - heightText / 2);
        }

        private void _steepOnePICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "1", graphics);
        }

        private void _lineOneNewSteepPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateLineSteps(sender, e, _lineOneNewSteepPICTUREBOX.Width, _lineOneNewSteepPICTUREBOX.Height, 1, graphics);
        }

        private void _steepTwoPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "2", graphics);
        }

        private void _lineTwoNewSteepPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateLineSteps(sender, e, _lineOneNewSteepPICTUREBOX.Width, _lineOneNewSteepPICTUREBOX.Height, 2, graphics);
        }

        private void _steepThreePICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "3", graphics);
        }

        private void _lineThreeNewSteepPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateLineSteps(sender, e, _lineOneNewSteepPICTUREBOX.Width, _lineOneNewSteepPICTUREBOX.Height, 3, graphics);
        }

        private void _steepFourPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "4", graphics);
        }

        private void _lineFourNewSteepPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateLineSteps(sender, e, _lineOneNewSteepPICTUREBOX.Width, _lineOneNewSteepPICTUREBOX.Height, 4, graphics);
        }

        private void _steepFivePICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "5", graphics);
        }

        private void _lineFiveNewSteepPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateLineSteps(sender, e, _lineOneNewSteepPICTUREBOX.Width, _lineOneNewSteepPICTUREBOX.Height, 5, graphics);
        }

        private void _steepSixPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            CreateElepseSteps(sender, e, _steepOnePICTUREBOX.Width, _steepOnePICTUREBOX.Height, "6", graphics);
        }
        #endregion

        private void DrawArrSteps(object sender, PaintEventArgs e, string text, int width, int height, Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Font font = new Font(StyleWindows._mainFont.FontFamily, 26.0F, FontStyle.Regular);
            SizeF sizeText = graphics.MeasureString(text, font);
            float widthText = sizeText.Width;
            float heightText = sizeText.Height;
            graphics.DrawString(text, font, new SolidBrush(StyleWindows._mainColor), width / 2 - widthText / 2, height / 2 - heightText / 2);
        }
    }
}
