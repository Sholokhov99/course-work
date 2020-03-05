using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appearance;

namespace Diploma2._0
{
    public partial class Information : StyleWindows
    {
        private int _positionYButton { get; set; }
        private int _positionYLinePage { get; set; } = 0;
        private int _steepLinew { get; set; } = 0;
        private const int _speedLine = 10;
        private const string _wayFold  = @"\temp\html\";
        private string _way { get; } = Application.StartupPath + _wayFold + "temp.html";
        public Information()
        {
            InitializeComponent();

            _linePage.BackColor = StyleWindows._mainColor;
            _namePageLABEL.Text = "Помощь";
            _iconWindowFLP.BackgroundImage = Properties.Resources.help;
            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 15, _closeWindowPICTUREBOX.Location.Y);
            this.TopMost = true;
            // Закрузка контента
            CreateHtmlDocument(Properties.Resources.info_FirstSteps);
            //_webContent.Navigate(CreateHtmlDocument(Properties.Resources.info_RessetPassword));

        }

        private void CreateResourceFiles(string fileResource)
        {
            using (FileStream file = new FileStream(_wayFold + @"img\", FileMode.Create))
            {
                byte[] array = Encoding.UTF8.GetBytes(fileResource);
                file.Write(array, 0, array.Length);

                //file.Write(way)
            }
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void CreateHtmlDocument(string fileResource)
        {
            /*
            MessageBox.Show(fileResource);
            switch (fileResource)
            {
                case "Properties.Resources.info_FirstSteps":
                    Bitmap bitmap = new Bitmap(Properties.Resources.info_firststep_autenfication);
                    ImageCodecInfo myImageCodecInfo = GetEncoderInfo(_wayFold + @"img\");
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    bitmap.Save("info_firststep_autenfication", myImageCodecInfo, myEncoderParameters);
                    MessageBox.Show("");

                    //CreateResourceFiles(Properties.Resources.info_firststep_autenfication);
                    break;
            }*/
            /*new Thread(() =>
            {*/
                using (FileStream file = new FileStream(_way, FileMode.Create))
                {
                    byte[] array = Encoding.UTF8.GetBytes(fileResource);
                    file.Write(array, 0, array.Length);

                    //file.Write(way)
                }
                try
                {
                    _webContent.Navigate(_way);
                }
                catch
                { }
            /*})
            {
                Priority = ThreadPriority.AboveNormal,
            }.Start();*/
        }

        //
        //  Определение позиции нажатой кнопки
        //
        private void DifinifionPositionButton(Control location)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _positionYButton = location.Location.Y;
            _locationLinePage.Enabled = true;
        }
        //
        //  Передвижение линии указывающая текущую страницу
        //
        private void _locationLinePage_Tick(object sender, EventArgs e)
        {
            if (_positionYLinePage != _positionYButton)
            {
                // Движение линии вниз
                if (_positionYLinePage < _positionYButton)
                {
                    // Определение движении линии, если позиция кнопки не делится на 10
                    if ((_positionYButton - _speedLine) < _positionYLinePage)
                        _positionYLinePage++;
                    else
                        _positionYLinePage += _speedLine;

                    _linePage.Location = new Point(0, _positionYLinePage);
                }
                else
                {
                    // Дивжение линии вниз
                    if ((_positionYButton + _speedLine) > _positionYLinePage)
                        _positionYLinePage--;
                    else
                        _positionYLinePage -= _speedLine;

                    _linePage.Location = new Point(0, _positionYLinePage);
                }
            }
            else
            {
                _locationLinePage.Enabled = false;
                _steepLinew = 0;
            }
        }

        //
        //  Первые шаги
        //

        private void _firstSteepBUTTON_Click(object sender, EventArgs e)
        {
            //Определение позиции кнопки
            DifinifionPositionButton(_firstSteepBUTTON);
            Image image = Properties.Resources.info_firststep_autenficatio;
            image.Save(Application.StartupPath + _wayFold + @"img\info_firststep_autenfication.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //info_firststep_privateAccount
            image = Properties.Resources.info_firststep_errorLogin;
            image.Save(Application.StartupPath + _wayFold + @"img\info_firststep_errorLogin.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            image = Properties.Resources.info_firststep_privateAccount;
            image.Save(Application.StartupPath + _wayFold + @"img\info_firststep_privateAccount.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            CreateHtmlDocument(Properties.Resources.info_FirstSteps);
            /*
            Bitmap bitmap = new Bitmap(Properties.Resources.info_firststep_autenfication);
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo(_wayFold + @"img\");
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            bitmap.Save("info_firststep_autenfication.png", myImageCodecInfo, myEncoderParameters);*/
        }
        //
        //  Забыли пароль
        //
        private void _resetPasswordBUTTON_Click(object sender, EventArgs e)
        {
            //Определение позиции кнопки
            DifinifionPositionButton(_resetPasswordBUTTON);
            CreateHtmlDocument(Properties.Resources.info_RessetPassword);
    
        }
        //
        //  Включение дополнительных функций аккаунта ребенка
        //
        private void _settingFunctionChildBUTTON_Click(object sender, EventArgs e)
        {
            //Определение позиции кнопки
            DifinifionPositionButton(_settingFunctionChildBUTTON);
            CreateHtmlDocument(Properties.Resources.info_editSettingChild);
        }
        //
        //  Проблемы с авторизацией
        //
        private void _problemLoginBUTTON_Click(object sender, EventArgs e)
        {
            //Определение позиции кнопки
            DifinifionPositionButton(_problemLoginBUTTON);
            CreateHtmlDocument(Properties.Resources.info_problemsAutenfication);
        }
        //
        //  Обновление приложения
        //
        private void _updateProgramBUTTON_Click(object sender, EventArgs e)
        {

            //Определение позиции кнопки
            DifinifionPositionButton(_updateProgramBUTTON);
            CreateHtmlDocument(Properties.Resources.info_updateProgram);
        }
        //
        //  Удаление приложения
        //
        private void _deleteProgramBUTTON_Click(object sender, EventArgs e)
        {
            DifinifionPositionButton(_deleteProgramBUTTON);
            CreateHtmlDocument(Properties.Resources.info_deleteProgram);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _webContent_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /*
            if(_webContent.Document.GetElementById("codeproject") != null) return;
            HtmlElement hElement = _webContent.Document.CreateElement("img");
            hElement.SetAttribute("src", Application.StartupPath + _wayFold + @"img\info_firststep_autenfication.png");
            hElement.Id = "codeproject";
            _webContent.Document.Body.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, hElement);*/
        }

        private void label2_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            Feedback feedback = new Feedback();
            feedback.ShowDialog();
        }

        private void _hearingProblemsBUTTON_Click(object sender, EventArgs e)
        {
            DifinifionPositionButton(_hearingProblemsBUTTON);
            CreateHtmlDocument(Properties.Resources.info_hearingProblems);
        }
        // 31

    }
}
