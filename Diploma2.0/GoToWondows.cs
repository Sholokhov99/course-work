using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlQueryProcessing;
using SecurityModule;
using ErrorInPrograms;
using VideoChannelProcessing;
using Appearance;
using System.Diagnostics;

namespace Diploma2._0
{

    public partial class GoToWondows : Form
    {
        Sql SQL = new Sql();
        Temp TEMP = new Temp();
        ScreenShot SCREENSHOT = new ScreenShot();

        Form _settingForm = new Form();
        //Form _
        //
        //  Переменные
        //
        private int _heightMenuPanel { get; } = 36;
        private int x, y;

        private float _fontSizeTimeLeft = 21.0F;
        private float _fontSizeTime = 27.0F;

        private bool _closeMenuPanel = false;
        private bool isCloseFromProgram = false;
        struct Temp
        {
            public string Login { get; set; }
            public int TimeWorkPc { get; set; }
            public int ByDay { get; set; }
            public int Block { get; set; }
            public string LastEnter { get; set; }
            public int AllTime { get; set; }
            public int ScreenShot { get; set; }
            public int TimeOutScreenShot { get; set; }
            public List<string> BlockPrograms { get; set; }
            public int EndTime { get; set; }
        }

        Security SECURITY = new Security();

        public GoToWondows(string login, int screenShot, int timeOutScreenShot)
        {
            InitializeComponent();

            this.Location = new Point(Form1._widthScreen - this.Width, Form1._heightScreen - this.Height * 3);
            
            TEMP.Login = login;

            TEMP.BlockPrograms = SQL.SelectBlockPrograms(TEMP.Login);
            Security.BlockBrogramInRegistry(true, TEMP.BlockPrograms);
            TEMP.ScreenShot = screenShot;
            TEMP.TimeOutScreenShot = timeOutScreenShot;
            if (screenShot == 1)
            {
                _createScreenShot.Interval = timeOutScreenShot;
                _createScreenShot.Enabled = true;

            }
            if (Form1._Access == 1)
            {
                CheckTheTimeOfTheUserTIMER.Enabled = false;
                _time.Text = "...";
            }
            else
            {
                CheckTheTimeOfTheUserTIMER.Enabled = true;
            }
            SaveDataTimeChild();
        }


        private void _createScreenShot_Tick(object sender, EventArgs e)
        {
            if (SCREENSHOT.CreateScreenShot(TEMP.Login) == -1)
            {
                ChangeUser();
            }
        }

        private void SaveDataTimeChild()
        {
            var data = SelectTimeUsers(Form1._Login, Form1._ByDay);
            //
            //  Сохранение измененных данных в памяти программы
            //
            Form1._TimeWorkPc = Convert.ToInt32(data[0]);
            Form1._ByDay = Convert.ToInt32(data[1]);
            Form1._Block = Convert.ToInt32(data[2]);
            Form1._LastEnter = data[3];
            Form1._AllTime = Convert.ToInt32(data[4]);
        }

        private void NewLocationTime() => _time.Location = new Point(Width / 2 - _time.Width / 2, _timeLeft.Height + _timeLeft.Location.Y);

        private List<string> SelectTimeUsers(string login, int byDay)
        {
            List<string> tempData = new List<string>();

            if (byDay == 0)
            {
                /*
                *   TimeWorkPc, ByDay, Block, LastEnter, AllTime
                */
                var data = SQL.SelectTimeUsers(login);
                //
                //  Сохранение временных данных
                //
                TEMP.TimeWorkPc = Convert.ToInt32(data[0]) - 1;
                TEMP.ByDay = Convert.ToInt32(data[1]);
                TEMP.Block = Convert.ToInt32(data[2]);
                TEMP.LastEnter = data[3];
                TEMP.AllTime = Convert.ToInt32(data[4]);
                tempData = data;
                //
                // Проверка состояния аккаунта
                //
                if (TEMP.Block == 0)
                {
                    //
                    // Повторная проверка на блокировку по минутам
                    //
                    if (TEMP.ByDay == 0)
                    {
                        //
                        //  Проверка на необходимость обновления времени
                        //
                        string nowDate = DateTime.Now.ToShortDateString();
                        if (TEMP.LastEnter == nowDate)
                        {
                            if (Security.CheckCodeOperation(SQL.UpdateTimeWorkPc(Form1._Login, TEMP.TimeWorkPc)))
                            {
                                CenterTimeAlignment(TEMP.TimeWorkPc);
                            }
                            else
                            {
                                MessageBox.Show("ОШИБКА!");
                            }
                        }
                        else
                        {
                            var value = SQL.UpdateLastEnter(Form1._Login, nowDate);
                            TEMP.LastEnter = nowDate;
                            CenterTimeAlignment(value);
                        }

                    }
                }

            }
            return tempData;
        }
        //
        //  Конвентирование оставшегося времени пользователя
        //
        public static string ConvertTime(int value)
        {
            const int min = 60;
            int hour = value / min;
            int minute = value % min;
            string text = hour.ToString() + " ";
            // Склонение записи часы
            if (hour == 1 || hour == 21) text += "Час ";
            else
                if (hour > 1 && hour < 5 || hour == 23 || hour == 24) text += "Часа ";
            else text += "Часов ";
            // Склонение записи минуты
            if (minute == 1 || minute == 21 || minute == 31 || minute == 41 || minute == 51) text += minute + " Минута";
            else
                if (minute > 1 && minute < 5 || minute > 22 && minute < 25 || minute > 31 && minute < 35 || minute > 41 && minute < 45 || minute > 51 && minute < 55)
                text += minute + " Минут";
            else
                text += minute + " Минут";

            return text;
        }

        private void CenterTimeAlignment(int value)
        {
            _time.Text = ConvertTime(value);
            NewLocationTime();
        }

        private void ChangeUser()
        {
            if (Form1._RecordVideo == 1)
            {
                RecordVideo.StopRecord();
            }
            CheckTheTimeOfTheUserTIMER.Enabled = false;
            Form1 form1 = new Form1();
            Hide();
            form1.Show();
        }

        private void _сhangeUser_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            ChangeUser();
        }

        private void CheckTheTimeOfTheUserTIMER_Tick(object sender, EventArgs e)
        {
            if (TEMP.ByDay == 0)
            {
                if (TEMP.TimeWorkPc != 0)
                {
                    TEMP.TimeWorkPc--;
                    var codeOperation = SQL.UpdateTimeChild(TEMP.Login, TEMP.TimeWorkPc);
                    CenterTimeAlignment(TEMP.TimeWorkPc);

                }
                else
                {
                    ChangeUser();
                }
            }
            else
            {
                if (TEMP.EndTime < DateTime.Now.Hour)
                {
                    ChangeUser();
                }
            }
            //SaveDataTimeChild();
        }

        private void GoToWondows_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isCloseFromProgram)
            {

                CheckTheTimeOfTheUserTIMER.Enabled = false;
                Form1 form1 = new Form1();
                Hide();
                form1.Show();
                form1.OpenUsersPersonalAccound();
            }
            else
            {
                if (Form1._Access != 1)
                    Security.CommandCMD("shutdown /f /s /t 0");
                else
                    Application.Exit();
            }

        }
        private void PositionTime() => _time.Location = new Point(Width / 2 - _time.Width / 2, _timeLeft.Location.Y + _timeLeft.Height);
        private void PositionTimeLeft() => _timeLeft.Location = new Point(Width / 2 - _timeLeft.Width / 2, unfoldingPICTUREBOX.Location.Y + unfoldingPICTUREBOX.Height);
        private void PositionUnfoldingPicturebox() => unfoldingPICTUREBOX.Location = new Point(Width / 2 - unfoldingPICTUREBOX.Width, _menuPANEL.Location.Y + _menuPANEL.Height);

        private float[] NewSizeTime(Control timeLeft, float fontSizeTL, Control time, float fontSizeT, bool increase)
        {
            float steep = 0.38F;
            if (!increase)
            {
                fontSizeT += steep;
                fontSizeTL += steep;
            }
            else
            {
                fontSizeTL -= steep;
                fontSizeT -= steep;
            }

            timeLeft.Font = new Font("Calibri", fontSizeTL);
            PositionTimeLeft();

            time.Font = new Font("Calibri", fontSizeT);
            PositionTime();
            return new float[] { fontSizeTL, fontSizeT };
        }
        private void _closeMenuTIMER_Tick(object sender, EventArgs e)
        {
            if (_closeMenuPanel)
            {
                if (_menuPANEL.Height != 0)
                {
                    _menuPANEL.Height -= 2;
                    PositionUnfoldingPicturebox();
                    var data = NewSizeTime(_timeLeft, _fontSizeTimeLeft, _time, _fontSizeTime, false);
                    _fontSizeTimeLeft = data[0];
                    _fontSizeTime = data[1];
                }
                else
                {

                    _closeMenuTIMER.Enabled = false;
                }
            }
            else
            {
                if (_menuPANEL.Height != _heightMenuPanel)
                {
                    _menuPANEL.Height += 2;
                    PositionUnfoldingPicturebox();

                    var data = NewSizeTime(_timeLeft, _fontSizeTimeLeft, _time, _fontSizeTime, true);
                    _fontSizeTimeLeft = data[0];
                    _fontSizeTime = data[1];

                }
                else
                {
                    _closeMenuTIMER.Enabled = false;
                }

            }
        }

        private void _controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void _controlPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }

        private void _back_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (Form1._RecordVideo == 1)
            {
                RecordVideo.StopRecord();
            }
            isCloseFromProgram = true;
            //
            //  Закрытие всех окон
            //
            _settingForm.Close();

            this.Close();
        }

        private void _opacityTRACKBAR_Scroll(object sender, EventArgs e)
        {
            this.Opacity = (_opacityTRACKBAR.Value * 0.01);
            _opacityLABEL.Text = _opacityTRACKBAR.Value.ToString() + "%";
            this.Refresh();
        }

        private void _settingPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            SettingChild setting = new SettingChild();
            _settingForm = setting;
            _settingForm.Show();
        }

        private void _checkHostFile_Tick(object sender, EventArgs e)
        {

        }

        private void _checkBlockPrograms_Tick(object sender, EventArgs e)
        {

         
        }

        private void GoToWondows_Load(object sender, EventArgs e)
        {
        }

        private void GoToWondows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Information info = new Information();
                info.ShowDialog();
            }
        }

        private void unfoldingPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _closeMenuPanel = _closeMenuPanel ? false : true;
            _closeMenuTIMER.Enabled = true;
        }
    }
}
