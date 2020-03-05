using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Appearance;
using SqlQueryProcessing;
using ErrorInPrograms;
using System.Drawing.Imaging;

namespace Diploma2._0
{
    public partial class ControlPanel : StyleWindows
    {
        Sql SQL = new Sql();
        NewUser NEWUSER = new NewUser();
        ToolBox TOOLBOX = new ToolBox();
        List<Control> _screensShotControl = new List<Control>();
        List<Image> _screenShots = new List<Image>();
        Form WINDOWSIMAGELIST = new Form();

        private bool _openImageList = false;

        SaveFileDialog saveScreenShot = new SaveFileDialog();

        FlowLayoutPanel _checkedScrollScreenShotFLP = new FlowLayoutPanel();
        Label _screenShotLABEL = new Label();
        Panel _boxCheckScreenShotPANEL = new Panel();
        PictureBox _pointConditionCheckScreenShotPICTUREBOX = new PictureBox();
        Label _conditionCheckScreenShotLABEL = new Label();
        Panel _screenShotPANEL = new Panel();

        Label _timeOutScreenLABEL = new Label();
        NumericUpDown _timeOutScreenShotNUMERICUPDOWN = new NumericUpDown();
        Label _timeOutScreenSecondsLABEL = new Label();
        Panel _numericUpDownPANEL = new Panel();

        private bool _screenShot = false;
        private bool _recordVideo = false;
        private int _screenShotTimeOut = 0;
        //
        //  Открытые функции
        // 
        private bool _previouslyWasOpenBlockUrl = false;
        private bool _previouslyWasOpenBlockProgram = false;
        private bool _previouslyWasOpenScreenShot = false;
        private bool _previouslyWasOpenVideo = false;
        //
        //  Элементы блокировок сайта
        //
        private Label _blockUrlLABEL = new Label();
        private FlowLayoutPanel _listBlockUrlFLP = new FlowLayoutPanel();
        private Panel _bottomBorderBlockUrlPANEL = new Panel();
        private Panel _rightBorderBlockUrlPANEL = new Panel();
        private Panel _leftBorderBlockUrlPANEL = new Panel();
        private Panel _contetntBlockUrlPANEL = new Panel();
        private TextBox _blockUrlTEXTBOX = new TextBox();
        private Button _blockWebBUTTON = new Button();
        private Panel _backPanelBlockUrlPANEL = new Panel();
        private Panel _topBorderBlockUrlPANEL = new Panel();
        private Panel _topTwoBorderBlockUrlPANEL = new Panel();
        private FlowLayoutPanel _controlPanelFLP = new FlowLayoutPanel();
        // Переменные
        List<Control> _blockUrl = new List<Control>();
        List<string> _url = new List<string>();
        private int _numberFunction = -1;
        private int _numberUser = -1;

        //
        //  Элементы блокировки приложения
        //
        private FlowLayoutPanel _listBlockProgramFLP = new FlowLayoutPanel();
        private Panel _rightBorderBlockProgram = new Panel();
        private Panel _topBorderBlockProgram = new Panel();
        private Panel _leftBorderBlockProgram = new Panel();
        private Panel _bottomBorderBlockProgram = new Panel();
        private Panel _nameBlockProgramPANEL = new Panel();
        private Panel _backPanelBlockProgramPANEL = new Panel();
        private Button _blockProgramBUTTON = new Button();
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        // переменные
        List<Control> _blockPrograms = new List<Control>();
        List<string> _program = new List<string>();

        //
        //  Снимки экрана
        //
        private Panel _contentScreenShot = new Panel();
        private FlowLayoutPanel _panelScreenShotFLP = new FlowLayoutPanel();
        private DateTimePicker _datePickerScreenShot = new DateTimePicker();
        //
        //  Элементы просмотра видео пользователя
        //
        private Panel _videoPANEL = new Panel();
        private Label _conditionCheckVideoLABEL = new Label();
        private PictureBox _pointConditionCheckVideoPICTUREBOX = new PictureBox();
        private FlowLayoutPanel _checkedScrollVideoFLP = new FlowLayoutPanel();
        private Panel _boxCheckVideoPANEL = new Panel();
        private Label _videoLABEL = new Label();
        FlowLayoutPanel _panelVideoFLP = new FlowLayoutPanel();
        Panel _videoContentPANEL = new Panel();
        List<string> _titleVideo = new List<string>();

        //
        // Переменные
        //
        public ControlPanel()
        {
            InitializeComponent();
            SettingStyleWindow();
            _usersLISTBOX.Font = StyleWindows._mainFont;
            _functLISTBOX.Font = _usersLISTBOX.Font;
        }

        private void SettingStyleWindow()
        {
            //
            //  Изменение оформления окна
            //
            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 15, this._closeWindowPICTUREBOX.Location.Y);
            _namePageLABEL.Text = "Детальная блокировка аккаунта";
            
            _informationLABEL.Location = new Point(_contentPanel.Width / 2 - _informationLABEL.Width / 2, _contentPanel.Height / 2 - _informationLABEL.Height / 2);
            var data = SQL.ListChild();
            if (data.Count > 0)
            {
                foreach (string value in data)
                    _usersLISTBOX.Items.Add(value);
            }
            else
            {
                int x = _usersLISTBOX.Location.X;
                int y = _usersLISTBOX.Location.Y;
                int width = _usersLISTBOX.Width;
                int height = _usersLISTBOX.Height;
                _listUsersCleaLABEL.Location = new Point(x + (width / 2) - _listUsersCleaLABEL.Width / 2, y + (height / 2) - _listUsersCleaLABEL.Height / 2);
                _listUsersCleaLABEL.Visible = true;
            }
        }

        private void SwitchingFunctions(int numberFunction, ListBox user)
        {
            switch (numberFunction)
            {
                case 0:
                    if (!_previouslyWasOpenBlockUrl)
                    {
                        _contentPanel.Controls.Clear();
                        _numberFunction = numberFunction;
                        _previouslyWasOpenBlockUrl = true;
                        CreateItemsBlockingWebSite();

                        _url.AddRange(SQL.SelectBlockUrlChild(user.SelectedItem.ToString()));



                        foreach (string url in _url)
                        {
                            Control value = CreatePanelBlockUrl(url, _foreColor, _blockUrl, _listBlockUrlFLP, true);
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
                    }
                    else if (_numberFunction != numberFunction)
                    {
                        _numberFunction = numberFunction;
                        _contentPanel.Controls.Clear();
                        _contentPanel.Controls.Add(_controlPanelFLP);
                    }
                    break;
                case 1:
                    if (!_previouslyWasOpenBlockProgram)
                    {
                        _contentPanel.Controls.Clear();
                        _contentPanel.Controls.Remove(_contentScreenShot);
                        _previouslyWasOpenBlockProgram = true;
                        CreateItemsBlockingPrograms();

                        var data = SQL.LoadBlockProgramUser(_usersLISTBOX.SelectedItem.ToString());


                        string way = "";
                        string nameProgram = "";
                        for (int track = 0; track < data.Count; track += 2)
                        {
                            way = data[track + 1];

                            nameProgram = NEWUSER.GetTheProgramName(way);

                            if (File.Exists(way))
                            {
                                Image icon = NEWUSER.IconPrograms(way);
                                Control value = CreatePanelBlockProgram(nameProgram, _foreColor, _blockPrograms, _listBlockProgramFLP, icon, true);
                                _blockPrograms.Add(value);
                                _program.Add(nameProgram);
                                _program.Add(way);
                                _listBlockProgramFLP.Controls.Add(value);
                            }
                            else
                            {
                                if (MessageBox.Show("Программа по данному путе не существует, удалить ее из базы данных?\n" + way, "Программа не существует", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    SQL.DeleteInvalidProgram(_usersLISTBOX.SelectedItem.ToString(), way);
                                }
                            }
                        }
                    }
                    else if (_numberFunction != numberFunction)
                    {
                        _numberFunction = numberFunction;
                        _contentPanel.Controls.Clear();
                        //_backPanelBlockProgramPANEL.Dispose();

                        CreateItemsBlockingPrograms();
                        _contentPanel.Controls.Add(_backPanelBlockProgramPANEL);
                    }
                    break;
                case 2:
                    if (!_previouslyWasOpenVideo)
                    {
                        _contentPanel.Controls.Clear();
                        _previouslyWasOpenVideo = true;
                        CreatingVideoViewingElements();
                        var data = SQL.SelectVideoPlayUser(_usersLISTBOX.SelectedItem.ToString());

                        if(data == 1)
                        {
                            _pointConditionCheckVideoPICTUREBOX_Click(null, null);
                        }



                        _titleVideo = SecurityModule.Security.SearchVideoFiles(Application.StartupPath + @"\Video\", _usersLISTBOX.SelectedItem.ToString());

                        int number = 0;
                        foreach (string value in _titleVideo)
                        {
                            _panelVideoFLP.Controls.Add(CreateVideoPanel(value, number));
                            number++;
                        }
                        _contentPanel.Controls.Add(_videoContentPANEL );
                    }
                    else if (_numberFunction != numberFunction)
                    {
                        _numberFunction = numberFunction;
                        _contentPanel.Controls.Clear();

                        _contentPanel.Controls.Add(_videoContentPANEL);
                    }
                    break;
                case 3:
                    if (!_previouslyWasOpenScreenShot)
                    {
                        _contentPanel.Controls.Clear();
                        _previouslyWasOpenBlockProgram = true;
                        _contentScreenShot.Controls.Add(_panelScreenShotFLP);
                        CreateItemsScreenShot();
                        LoadScreenShots(user);

                        var data = SQL.SelectPositionScreenShot(_usersLISTBOX.SelectedItem.ToString());
                        _screenShot = (data[0] == 1) ? false : true;
                        _pointConditionCheckScreenShotPICTUREBOX_Click(null, null);
                        _screenShotTimeOut = data[1];
                        _timeOutScreenShotNUMERICUPDOWN.Value = _screenShotTimeOut / 1000;


                        _contentPanel.Controls.Add(_contentScreenShot);
                    }
                    else if (_numberFunction != numberFunction)
                    {
                        _numberFunction = numberFunction;
                        _contentPanel.Controls.Clear();


                        _contentPanel.Controls.Add(_contentScreenShot);
                    }
                    break;
            }
        }

        private void TestRecord()
        {
            if (!VideoChannelProcessing.RecordVideo.TestRecord(Application.StartupPath + @"\Video"))
            {
                MessageBox.Show("На компьютере отсутствует приложение Microsoft Expression Encoder" +
                    "\nДанный компонент можно установить по ссылке:\nhttps://www.microsoft.com/en-us/download/details.aspx?id=18974", "Данная функция не доступна", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _recordVideo = false;
                _checkedScrollVideoFLP.Refresh();
                _recordVideoTIMER.Enabled = true;

            }
            else
            {
                MessageBox.Show("Все компоненты установлены", "Функция работает корректно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //https://www.microsoft.com/en-us/download/details.aspx?id=18974
        }

        private void ClearData(int numberUser, int numberFunction)
        {
            _contentPanel.Controls.Clear();
            _listBlockUrlFLP.Controls.Clear();
            _controlPanelFLP.Controls.Clear();
            _contentScreenShot.Controls.Clear();
            _backPanelBlockProgramPANEL.Controls.Clear();
            _panelVideoFLP.Controls.Clear();
            _videoContentPANEL.Controls.Clear();
            _panelScreenShotFLP.Controls.Clear();

            _blockWebBUTTON.Dispose();

            _screenShot = false;
            _recordVideo = false;
            _screenShotTimeOut = 0;
            _previouslyWasOpenBlockUrl = false;
            _previouslyWasOpenBlockProgram = false;
            _previouslyWasOpenScreenShot = false;
            _previouslyWasOpenVideo = false;
            _blockUrl.Clear();
            _url.Clear();
            _numberFunction = numberFunction;
            _numberUser = numberUser;
            _blockPrograms.Clear();
            _program.Clear();

            _titleVideo.Clear();
        }

        private void CreatingVideoViewingElements()
        {
            // 
            // _screenShotLABEL
            // 
            _videoLABEL.AutoSize = true;
            _videoLABEL.Font = StyleWindows._mainFont;
            _videoLABEL.Location = new Point(0, 2);
            _videoLABEL.Margin = new Padding(0, 2, 3, 0);
            _videoLABEL.Name = "_videoLABEL";
            _videoLABEL.Size = new Size(261, 19);
            _videoLABEL.TabIndex = 0;
            _videoLABEL.Text = "Запись экрана";
            // 
            // _boxCheckEyeProblemPANEL
            // 
            _boxCheckVideoPANEL.BackColor = System.Drawing.Color.White;
            _boxCheckVideoPANEL.Controls.Add(_pointConditionCheckVideoPICTUREBOX);
            _boxCheckVideoPANEL.Controls.Add(_conditionCheckVideoLABEL);
            _boxCheckVideoPANEL.Location = new Point(267, 3);
            _boxCheckVideoPANEL.Name = "_boxCheckVideoPANEL";
            _boxCheckVideoPANEL.Size = new Size(40, 18);
            _boxCheckVideoPANEL.TabIndex = 0;
            ToolBox.SetRoundedShape(_boxCheckVideoPANEL, 20);
            // 
            // _checkedScrollScreenShotFLP
            // 
            _checkedScrollVideoFLP.AutoSize = true;
            _checkedScrollVideoFLP.Controls.Add(_videoLABEL);
            _checkedScrollVideoFLP.Controls.Add(_boxCheckVideoPANEL);
            _checkedScrollVideoFLP.Location = new Point(0, 0);
            _checkedScrollVideoFLP.Name = "_checkedScrollVideoFLP";
            Size size = TextRenderer.MeasureText(_videoLABEL.Text, StyleWindows._mainFont);
            _checkedScrollVideoFLP.Size = new Size(size.Width + _boxCheckVideoPANEL.Width + 5, size.Height);
            _checkedScrollVideoFLP.TabIndex = 32;
            _checkedScrollVideoFLP.Paint += _checkedScrollVideoFLP_Paint;
            _checkedScrollVideoFLP.BackColor = Color.White;
            // 
            // _pointConditionCheckEyeProblemPICTUREBOX
            // 
            _pointConditionCheckVideoPICTUREBOX.BackColor = Color.Transparent;
            _pointConditionCheckVideoPICTUREBOX.Cursor = Cursors.Hand;
            _pointConditionCheckVideoPICTUREBOX.Location = new Point(0, 0);
            _pointConditionCheckVideoPICTUREBOX.Margin = new Padding(0);
            _pointConditionCheckVideoPICTUREBOX.Name = "_pointConditionCheckVideoPICTUREBOX";
            _pointConditionCheckVideoPICTUREBOX.Size = new Size(20, 20);
            _pointConditionCheckVideoPICTUREBOX.TabIndex = 0;
            _pointConditionCheckVideoPICTUREBOX.TabStop = false;
            _pointConditionCheckVideoPICTUREBOX.Click += _pointConditionCheckVideoPICTUREBOX_Click;
            _pointConditionCheckVideoPICTUREBOX.Paint += _pointConditionCheckVideoPICTUREBOX_Paint;
            // 
            // _conditionCheckEyeProblemLABEL
            // 
            _conditionCheckVideoLABEL.AutoSize = true;
            _conditionCheckVideoLABEL.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            _conditionCheckVideoLABEL.ForeColor = Color.Red;
            _conditionCheckVideoLABEL.Location = new Point(16, 2);
            _conditionCheckVideoLABEL.Name = "_conditionCheckVideoLABEL";
            _conditionCheckVideoLABEL.Size = new Size(22, 14);
            _conditionCheckVideoLABEL.TabIndex = 7;
            _conditionCheckVideoLABEL.Text = "Off";
            //
            //  topHr
            //
            Panel topHr = new Panel()
            {
                BackColor = Color.Silver,
                Size = new Size(0, 1),
                Dock = DockStyle.Bottom,
            };
            //
            //  _videoPANEL
            //
            _videoPANEL.Dock = DockStyle.Top;
            _videoPANEL.Size = new Size(0, _checkedScrollVideoFLP.Height + 5);
            _videoPANEL.BackColor = this.BackColor;
            _videoPANEL.Name = "_videoPANEL";
            _videoPANEL.Controls.Add(_checkedScrollVideoFLP);
            _videoPANEL.Controls.Add(topHr);
            //
            //  _panelVideoFLP
            //
            _panelVideoFLP.BackColor = this.BackColor;
            _panelVideoFLP.Dock = DockStyle.Fill;
            _panelVideoFLP.AutoScroll = true;
            _panelVideoFLP.Name = "_panelVideoFLP";
            //
            //  _videoContentPANEL
            //
            _videoContentPANEL.BackColor = this.BackColor;
            _videoContentPANEL.Dock = DockStyle.Fill;
            _videoContentPANEL.Name = "_videoContentPANEL";
            _videoContentPANEL.Controls.Add(_panelVideoFLP);
            _videoContentPANEL.Controls.Add(_videoPANEL);
        }

        private void _checkedScrollVideoFLP_Paint(object sender, PaintEventArgs e) => ToolBox.CheckedShrollCheckBoxGraphics(_recordVideo, _boxCheckVideoPANEL, sender, e);

        #region Record video checkbox

        private void _pointConditionCheckVideoPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, _recordVideo);

        private void _recordVideoTIMER_Tick(object sender, EventArgs e) => ToolBox.MovingTheStatusSlider(_recordVideo, _pointConditionCheckVideoPICTUREBOX, _conditionCheckVideoLABEL, _boxCheckVideoPANEL, _recordVideoTIMER);

        private void _pointConditionCheckVideoPICTUREBOX_Click(object sender, EventArgs e)
        {

            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_recordVideo)
            {
                _recordVideo = false;
                _checkedScrollVideoFLP.Refresh();
            }
            else
            {
                TestRecord();
                _recordVideo = true;
                _checkedScrollVideoFLP.Refresh();
            }
            _recordVideoTIMER.Enabled = true;
        }

        #endregion

        #region Screen shot
        private void LoadScreenShots(ListBox user)
        {
            _panelScreenShotFLP.Controls.Clear();
            _screensShotControl.Clear();
            var data = SQL.SelectScreenShotsUser(user.SelectedItem.ToString(), _datePickerScreenShot.Value.ToShortDateString());
            //CreateImageList();


            int step = 0;
            for (int track = 0; track < data.Count; track += 3)
            {
                MemoryStream image_stream = new MemoryStream((byte[])data[track + 1]);
                _screensShotControl.Add(CreateImageList(Encoding.ASCII.GetString(data[track]), image_stream, Encoding.ASCII.GetString(data[track + 2]), track));
                if (track == data.Count - 3) break;
                step++;
            }

            foreach (Control value in _screensShotControl)
                _panelScreenShotFLP.Controls.Add(value);
        }

        private int IndexDeleteFile(int id)
        {
            string holdId = "";
            bool isId = false;
            int numberElement = 0;
            foreach (Control cnt in _panelScreenShotFLP.Controls)
            {
                Panel panel = cnt as Panel;
                for (int index = 0; index < panel.Name.Length; index++)
                {
                    if (!isId)
                    {
                        if (panel.Name[index] == '_') isId = true;
                    }
                    else
                    {
                        if (panel.Name[index] == '_')
                        {
                            if (holdId == id.ToString())
                            {
                                return numberElement;
                            }
                            else
                            {
                                isId = false;
                                holdId = "";
                                break;
                            }
                        }
                        else
                        {
                            holdId += panel.Name[index];
                        }
                    }
                }
                numberElement++;
            }
            return numberElement;
        }
        private int IndexPictureBox(int id)
        {
            string holdId = "";
            bool isId = false;
            int numberElement = 0;
            foreach (Control cnt in _panelScreenShotFLP.Controls)
            {
                Panel panel = cnt as Panel;
                foreach (Control picture in panel.Controls)
                {
                    PictureBox img = picture as PictureBox;

                    for (int index = 0; index < img.Name.Length; index++)
                    {
                        if (!isId)
                        {
                            if (img.Name[index] == '_') isId = true;
                        }
                        else
                        {
                            if (img.Name[index] == '_')
                            {
                                if (holdId == id.ToString())
                                {
                                    return numberElement;
                                }
                                else
                                {
                                    isId = false;
                                    holdId = "";
                                    break;
                                }
                            }
                            else
                            {
                                holdId += img.Name[index];
                            }
                        }
                    }
                    numberElement++;

                }
            }
            return numberElement;
        }

        private int IndexPicture(string name)
        {
            bool isnumber = false;
            bool isId = false;
            string holdNumber = "";
            string holdId = "";
            for (int index = 0; index < name.Length; index++)
            {
                if (!isId)
                {
                    if (name[index] == '_') isId = true;
                }
                else
                {
                    if (!isnumber)
                    {
                        if (name[index] == '_') isnumber = true;
                        else
                        {
                            holdId += name[index];
                        }
                    }
                    else
                    {
                        holdNumber += name[index];
                    }
                }
            }
            return Convert.ToInt32(holdNumber);
        }
        private Control CreateImageList(string time, MemoryStream pictureByte, string id, int number)
        {
            //107; 133
            //
            //  screenShot
            //
            Image image = Image.FromStream(pictureByte);
            _screenShots.Add(image);
            _screenShotIMAGELIST.Images.Add(image);
            //
            //  saveImage
            //
            PictureBox saveImage = new PictureBox()
            {
                Size = new Size(13, 13),
                Name = $"saveImage_{id}_{number}",
                Cursor = Cursors.Hand,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = Properties.Resources.icon_save,
            };
            saveImage.Click += (s, e) =>
            {
                saveScreenShot.CreatePrompt = false;
                saveScreenShot.OverwritePrompt = true;
                saveScreenShot.Title = "Сохранение изображения";
                saveScreenShot.InitialDirectory = Application.StartupPath;
                saveScreenShot.CheckPathExists = true;
                saveScreenShot.AddExtension = true;
                saveScreenShot.Filter = "Image files(*.jpg)|*.jpg";
                saveScreenShot.ShowHelp = false;



                if (saveScreenShot.ShowDialog() == DialogResult.OK)
                {
                    // Получение номера картинки
                    int indexPicture = IndexPicture(saveImage.Name);
                    _screenShots[indexPicture / 3].Save(saveScreenShot.FileName, ImageFormat.Jpeg);
                    
                }
            };
            //
            //  deleteImage
            //
            PictureBox deleteImage = new PictureBox()
            {
                Size = new Size(13, 13),
                Name = $"deleteImage_{id}_{number}",
                Cursor = Cursors.Hand,
            };
            deleteImage.Paint += (s, e) =>
            {
                Graphics graphics = e.Graphics;
                Pen pen = new Pen(Color.Red, 2.0F);
                graphics.DrawLine(pen, 1, 1, deleteImage.Width - 2, deleteImage.Height - 2);
                graphics.DrawLine(pen, deleteImage.Width - 2, 1, 1, deleteImage.Height - 2);
            };
            deleteImage.Click += (s, e) =>
            {
                StyleWindows.SoundClick(Form1._SoundEffect);
                if (Warning.DeleteFile() == DialogResult.OK)
                {
                    PictureBox picture = (PictureBox)s;

                    // Получение id удаляемой картинки
                    int idPicture = IndexPicture(picture.Name);
                    // Получение номера удаляемых элементов
                    int numberDeleteFile = IndexDeleteFile(idPicture);
                    // Удаление панели 
                    _panelScreenShotFLP.Controls.RemoveAt(numberDeleteFile);
                    // Удаление картники из массива
                    _screensShotControl.RemoveAt(numberDeleteFile);
                    // Удаление картинки из базы данных
                    if (SQL.DeleteScreenShot(_usersLISTBOX.SelectedItem.ToString(), idPicture) > 0)
                    {
                        Warning.SaveDataUser();
                    }
                }
            };
            //
            //  screenShot
            //
            PictureBox screenShot = new PictureBox()
            {
                Size = new Size(90, 90),
                Name = $"screenShot_{id}_{number}",
                BorderStyle = BorderStyle.FixedSingle,
                BackgroundImage = image,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = Cursors.Hand,
            };
            screenShot.Click += ScreenShot_Click;
            //
            //  timeScreen
            //
            Label timeScreen = new Label()
            {
                Name = $"timeScreen_{id}_{number}",
                Text = time,
                Font = StyleWindows._mainFont,
                AutoSize = true,
            };
            //
            //  contentImageScreen
            //
            Panel contentImageScreen = new Panel()
            {
                Name = $"contentImageScreen_{id}_{number}",
                Size = new Size(107, 147),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };
            deleteImage.Location = new Point(contentImageScreen.Width - deleteImage.Width - 5, 5);
            saveImage.Location = new Point(deleteImage.Location.X - saveImage.Width - 5, deleteImage.Location.Y);

            screenShot.Location = new Point(contentImageScreen.Width / 2 - screenShot.Width / 2, deleteImage.Location.Y + deleteImage.Height + 5);
            timeScreen.Location = new Point(contentImageScreen.Width / 2 - timeScreen.Width / 2, screenShot.Location.Y + screenShot.Height + 5);

            contentImageScreen.Controls.Add(deleteImage);
            contentImageScreen.Controls.Add(saveImage);
            contentImageScreen.Controls.Add(screenShot);
            contentImageScreen.Controls.Add(timeScreen);
            return contentImageScreen;
        }

        private object GetAll(ControlPanel controlPanel, Type type)
        {
            throw new NotImplementedException();
        }

        private void ScreenShot_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            PictureBox picture = (PictureBox)sender;

            if (!_openImageList)
            {
                WindowImageList windowImageList = new WindowImageList(_screenShots);
                WINDOWSIMAGELIST = windowImageList;
                WINDOWSIMAGELIST.ShowDialog();
            }
        }

        private Control CreateVideoPanel(string nameVideo, int number)
        {
            PictureBox videoIcon = new PictureBox()
            {
                Size = new Size(90, 90),
                Name = "videoIcon_" + number,
                BorderStyle = BorderStyle.FixedSingle,
                BackgroundImage = Properties.Resources.Document_01_128,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = Cursors.Hand,
            };
            videoIcon.Click += VideoIcon_Click;
            Label titleVideo = new Label()
            {
                Name = "titleVideo_" + number,
                Text = nameVideo,
                Font = StyleWindows._mainFont,
                AutoSize = true,
            };
            ToolTip titleVideoTOOLTIP = new ToolTip();
            StyleWindows.SettingToolTip(titleVideoTOOLTIP, titleVideo, "Название файла", titleVideo.Text);
            //
            //  contentImageScreen
            //
            Panel contentVideo = new Panel()
            {
                Name = "contentVideo_" + number,
                Size = new Size(107, 133),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
            };
            videoIcon.Location = new Point(contentVideo.Width / 2 - videoIcon.Width / 2, 5);
            titleVideo.Location = new Point(contentVideo.Width / 2 - titleVideo.Width / 2, videoIcon.Location.Y + videoIcon.Height + 5);

            contentVideo.Controls.Add(videoIcon);
            contentVideo.Controls.Add(titleVideo);
            return contentVideo;
        }

        private void VideoIcon_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            PictureBox picture = (PictureBox)sender;
            string temp = "";
            bool isNumber = false;
            int numberVideo;
            foreach (char value in picture.Name)
            {
                if (isNumber)
                {
                    temp += value;
                }
                else
                {
                    if (value == '_')
                    {
                        isNumber = true;
                    }
                }
            }
            try
            {
                numberVideo = Convert.ToInt32(temp);
            }
            catch
            {
                numberVideo = 0;
            }
            VideoPlay.MainWindow media = new VideoPlay.MainWindow(Application.StartupPath + @"\Video\" + _titleVideo[numberVideo]);
            media.ShowDialog();
        }

        private void CreateItemsScreenShot()
        {

            #region CheckBox ToolBox
            // 
            // _screenShotLABEL
            // 
            _screenShotLABEL.AutoSize = true;
            _screenShotLABEL.Font = StyleWindows._mainFont;
            _screenShotLABEL.Location = new Point(0, 2);
            _screenShotLABEL.Margin = new Padding(0, 2, 3, 0);
            _screenShotLABEL.Name = "_screenShotLABEL";
            _screenShotLABEL.Size = new Size(261, 19);
            _screenShotLABEL.TabIndex = 0;
            _screenShotLABEL.Text = "Снимок экрана";

            // 
            // _boxCheckEyeProblemPANEL
            // 
            _boxCheckScreenShotPANEL.BackColor = System.Drawing.Color.White;
            _boxCheckScreenShotPANEL.Controls.Add(_pointConditionCheckScreenShotPICTUREBOX);
            _boxCheckScreenShotPANEL.Controls.Add(_conditionCheckScreenShotLABEL);
            _boxCheckScreenShotPANEL.Location = new Point(267, 3);
            _boxCheckScreenShotPANEL.Name = "_boxCheckScreenShotPANEL";
            _boxCheckScreenShotPANEL.Size = new Size(40, 18);
            _boxCheckScreenShotPANEL.TabIndex = 0;
            ToolBox.SetRoundedShape(_boxCheckScreenShotPANEL, 20);
            // 
            // _checkedScrollScreenShotFLP
            // 
            _checkedScrollScreenShotFLP.AutoSize = true;
            _checkedScrollScreenShotFLP.Controls.Add(_screenShotLABEL);
            _checkedScrollScreenShotFLP.Controls.Add(_boxCheckScreenShotPANEL);
            _checkedScrollScreenShotFLP.Location = new Point(0, 0);
            _checkedScrollScreenShotFLP.Name = "_checkedScrollScreenShotFLP";

            Size size = TextRenderer.MeasureText(_screenShotLABEL.Text, StyleWindows._mainFont);
            _checkedScrollScreenShotFLP.Size = new Size(size.Width + _boxCheckScreenShotPANEL.Width + 5, size.Height);

            _checkedScrollScreenShotFLP.TabIndex = 32;
            _checkedScrollScreenShotFLP.Paint += _checkedScrollScreenShotFLP_Paint;
            _checkedScrollScreenShotFLP.BackColor = Color.White;
            // 
            // _pointConditionCheckEyeProblemPICTUREBOX
            // 
            _pointConditionCheckScreenShotPICTUREBOX.BackColor = Color.Transparent;
            _pointConditionCheckScreenShotPICTUREBOX.Cursor = Cursors.Hand;
            _pointConditionCheckScreenShotPICTUREBOX.Location = new Point(0, 0);
            _pointConditionCheckScreenShotPICTUREBOX.Margin = new Padding(0);
            _pointConditionCheckScreenShotPICTUREBOX.Name = "_pointConditionCheckScreenShotPICTUREBOX";
            _pointConditionCheckScreenShotPICTUREBOX.Size = new Size(20, 20);
            _pointConditionCheckScreenShotPICTUREBOX.TabIndex = 0;
            _pointConditionCheckScreenShotPICTUREBOX.TabStop = false;

            _pointConditionCheckScreenShotPICTUREBOX.Click += _pointConditionCheckScreenShotPICTUREBOX_Click;
            _pointConditionCheckScreenShotPICTUREBOX.Paint += _pointConditionCheckScreenShotPICTUREBOX_Paint;
            // 
            // _conditionCheckEyeProblemLABEL
            // 
            _conditionCheckScreenShotLABEL.AutoSize = true;
            _conditionCheckScreenShotLABEL.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            _conditionCheckScreenShotLABEL.ForeColor = Color.Red;
            _conditionCheckScreenShotLABEL.Location = new Point(16, 2);
            _conditionCheckScreenShotLABEL.Name = "_conditionCheckScreenShotLABEL";
            _conditionCheckScreenShotLABEL.Size = new Size(22, 14);
            _conditionCheckScreenShotLABEL.TabIndex = 7;
            _conditionCheckScreenShotLABEL.Text = "Off";
            //
            //  _screenShotPANEL
            //
            _screenShotPANEL.Dock = DockStyle.Top;
            _screenShotPANEL.Size = new Size(0, _checkedScrollScreenShotFLP.Height + 10);
            _screenShotPANEL.BackColor = Color.White;

            _screenShotPANEL.Controls.Add(_checkedScrollScreenShotFLP);
            _screenShotPANEL.Controls.Add(_numericUpDownPANEL);
            #endregion
            //
            //  _timeOutScreenLABEL
            //
            _timeOutScreenLABEL.AutoSize = true;
            _timeOutScreenLABEL.Font = StyleWindows._mainFont;
            _timeOutScreenLABEL.Text = "Частота сохранения";
            _timeOutScreenLABEL.Location = new Point(0, 0);
            //
            //  _timeOutScreenShotLIXTBOX
            //
            _timeOutScreenShotNUMERICUPDOWN.Font = StyleWindows._mainFont;
            _timeOutScreenShotNUMERICUPDOWN.Minimum = 2;
            _timeOutScreenShotNUMERICUPDOWN.Maximum = 999;
            _timeOutScreenShotNUMERICUPDOWN.DecimalPlaces = 0;
            size = TextRenderer.MeasureText("999", StyleWindows._mainFont);
            _timeOutScreenShotNUMERICUPDOWN.Size = new Size(size.Width + 10, size.Height);
            size = TextRenderer.MeasureText(_timeOutScreenLABEL.Text, StyleWindows._mainFont);
            _timeOutScreenShotNUMERICUPDOWN.Location = new Point(_timeOutScreenLABEL.Location.X + size.Width, 0);
            //
            //  _timeOutScreenSecondsLABEL
            //
            _timeOutScreenSecondsLABEL.AutoSize = true;
            _timeOutScreenSecondsLABEL.Font = StyleWindows._mainFont;
            _timeOutScreenSecondsLABEL.Text = "Секунд";
            _timeOutScreenSecondsLABEL.Location = new Point(_timeOutScreenShotNUMERICUPDOWN.Location.X + _timeOutScreenShotNUMERICUPDOWN.Width, 0);
            //
            //  _numericUpDownPANEL
            //
            _numericUpDownPANEL.Location = new Point(_checkedScrollScreenShotFLP.Width + 20, 0);
            _numericUpDownPANEL.BackColor = Color.White;
            _numericUpDownPANEL.AutoSize = true;
            _numericUpDownPANEL.Controls.Add(_timeOutScreenLABEL);
            _numericUpDownPANEL.Controls.Add(_timeOutScreenShotNUMERICUPDOWN);
            _numericUpDownPANEL.Controls.Add(_timeOutScreenSecondsLABEL);


            //
            //  datePickerScreenShot
            //
            DateTimePicker datePickerScreenShot = new DateTimePicker()
            {
                Name = "datePickerScreenShot",
                Dock = DockStyle.Top,
                Font = StyleWindows._mainFont,
            };
            datePickerScreenShot.ValueChanged += DatePickerScreenShot_ValueChanged;
            _datePickerScreenShot = datePickerScreenShot;
            //
            //  panelScreenShotFLP
            //
            FlowLayoutPanel panelScreenShotFLP = new FlowLayoutPanel()
            {
                Name = "panelScreenShotFLP",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true,
            };
            _panelScreenShotFLP = panelScreenShotFLP;
            //
            //  contentScreenShot
            //
            Panel contentScreenShot = new Panel()
            {
                Dock = DockStyle.Fill,
                Name = "conbentScreenShot",
                BackColor = Color.Transparent,

            };
            contentScreenShot.Controls.Add(panelScreenShotFLP);
            contentScreenShot.Controls.Add(datePickerScreenShot);
            contentScreenShot.Controls.Add(_screenShotPANEL);
            
            _contentScreenShot = contentScreenShot;
        }

        private void _pointConditionCheckScreenShotPICTUREBOX_Paint(object sender, PaintEventArgs e) => ToolBox.PointConditionCheckBoxGraphics(sender, e, _screenShot);

        private void _pointConditionCheckScreenShotPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_screenShot)
            {
                _screenShot = false;
                _checkedScrollScreenShotFLP.Refresh();
            }
            else
            {
                _screenShot = true;
                _checkedScrollScreenShotFLP.Refresh();
            }
            _screenShotTIMER.Enabled = true;
        }

        private void _checkedScrollScreenShotFLP_Paint(object sender, PaintEventArgs e)
        {
            ToolBox.CheckedShrollCheckBoxGraphics(_screenShot, _boxCheckScreenShotPANEL, sender, e);
        }

        private void DatePickerScreenShot_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker date = (DateTimePicker)sender;

            /*
             SelectScreenShotsUser(string login, string date)
             */
            LoadScreenShots(_usersLISTBOX);
        }
        private void _screenShotTIMER_Tick(object sender, EventArgs e) => ToolBox.MovingTheStatusSlider(_screenShot, _pointConditionCheckScreenShotPICTUREBOX, _conditionCheckScreenShotLABEL, _boxCheckScreenShotPANEL, _screenShotTIMER);
        #endregion

        #region Block web site

        private void CreateItemsBlockingWebSite()
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
            // _topBorderBlockUrlPANEL
            // 
            _topBorderBlockUrlPANEL.BackColor = StyleWindows._mainBorderColor;
            _topBorderBlockUrlPANEL.Dock = DockStyle.Top;
            _topBorderBlockUrlPANEL.Location = new Point(2, 186);
            _topBorderBlockUrlPANEL.Name = "_topBorderBlockUrlPANEL";
            _topBorderBlockUrlPANEL.Size = new Size(557, 2);
            _topBorderBlockUrlPANEL.TabIndex = 4;
            // 
            // _topTwoBorderBlockUrlPANEL
            // 
            _topTwoBorderBlockUrlPANEL.BackColor = StyleWindows._mainBorderColor;
            _topTwoBorderBlockUrlPANEL.Dock = DockStyle.Top;
            _topTwoBorderBlockUrlPANEL.Location = new Point(2, 186);
            _topTwoBorderBlockUrlPANEL.Name = "_topTwoBorderBlockUrlPANEL";
            _topTwoBorderBlockUrlPANEL.Size = new Size(557, 2);
            _topTwoBorderBlockUrlPANEL.TabIndex = 4;
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
            _contetntBlockUrlPANEL.Controls.Add(_bottomBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_topBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_listBlockUrlFLP);
            _contetntBlockUrlPANEL.Controls.Add(_backPanelBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_rightBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_leftBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Controls.Add(_topTwoBorderBlockUrlPANEL);
            _contetntBlockUrlPANEL.Location = new Point(0, 0);
            _contetntBlockUrlPANEL.Name = "_contetntBlockUrlPANEL";
            _contetntBlockUrlPANEL.Size = new Size(_contentPanel.Width - 10, _contentPanel.Height - 10);
            _contetntBlockUrlPANEL.TabIndex = 1;
            // 
            // _blockWebBUTTON
            // 
            Button blockWebBUTTON = new Button();
            blockWebBUTTON.BackColor = StyleWindows._mainColor;
            blockWebBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            blockWebBUTTON.FlatAppearance.BorderSize = 2;
            blockWebBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            blockWebBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            blockWebBUTTON.ForeColor = System.Drawing.Color.White;
            blockWebBUTTON.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            blockWebBUTTON.Cursor = Cursors.Hand;
            blockWebBUTTON.Name = "blockWebBUTTON";
            blockWebBUTTON.Size = new System.Drawing.Size(130, 27);
            blockWebBUTTON.TabIndex = 1;
            blockWebBUTTON.Text = "Заблокировать";
            blockWebBUTTON.UseVisualStyleBackColor = false;
            blockWebBUTTON.Click += new System.EventHandler(this._blockWebBUTTON_Click);
            _blockWebBUTTON = blockWebBUTTON;
            // 
            // _blockUrlTEXTBOX
            // 
            this._blockUrlTEXTBOX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._blockUrlTEXTBOX.Font = StyleWindows._mainFont;
            this._blockUrlTEXTBOX.Location = new System.Drawing.Point(3,3);
            this._blockUrlTEXTBOX.Size = new System.Drawing.Size(_contentPanel.Width - _blockWebBUTTON.Width - 10, 20);
            if (_blockUrlTEXTBOX.Height < _blockWebBUTTON.Height)
            {
                _blockUrlTEXTBOX.Location = new Point(3, _blockWebBUTTON.Height - _blockUrlTEXTBOX.Height);
                this._blockWebBUTTON.Location = new System.Drawing.Point(_blockUrlTEXTBOX.Width + 5, 0);
            }
            else
            {
                this._blockWebBUTTON.Location = new System.Drawing.Point(_blockUrlTEXTBOX.Width + 5, _blockUrlTEXTBOX.Height - _blockWebBUTTON.Height);
            }

            this._blockUrlTEXTBOX.Name = "_blockUrlTEXTBOX";
            this._blockUrlTEXTBOX.TabIndex = 0;
            //  _blockWebBUTTON
            //this._blockWebBUTTON.Location = new System.Drawing.Point(_blockUrlTEXTBOX.Width + 5, 0);
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
            this._backPanelBlockUrlPANEL.Cursor = Cursors.IBeam;
            this._backPanelBlockUrlPANEL.Click += _backPanelBlockUrlPANEL_Click;
            //this._backPanelBlockUrlPANEL.Paint += new System.Windows.Forms.PaintEventHandler(this._backPanelBlockUrlPANEL_Paint);
            // 
            // _controlPanelFLP
            // 
            //_controlPanelFLP.Controls.Add(_blockUrlLABEL);
            _controlPanelFLP.Controls.Add(_contetntBlockUrlPANEL);
            _controlPanelFLP.BackColor = Color.White;
            _controlPanelFLP.Name = "_controlPanelFLP";
            _controlPanelFLP.Size = new Size(_contentPanel.Width - 10, _contentPanel.Height);
            _controlPanelFLP.Dock = DockStyle.Fill;
            _controlPanelFLP.Location = new Point(0, 0);
            _controlPanelFLP.TabIndex = 0;

            _contentPanel.Controls.Add(_controlPanelFLP);
        }

        private void _backPanelBlockUrlPANEL_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            _blockUrlTEXTBOX.Focus();
        }

        private void _backPanelBlockUrlPANEL_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(_mainColor, 7.0f);
            graphics.DrawRectangle(pen, _blockUrlTEXTBOX.Location.X - 1, _blockUrlTEXTBOX.Location.Y - 1, _blockUrlTEXTBOX.Width + 1, _blockUrlTEXTBOX.Height + 1);
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
            // Проверка на соответствие
            foreach (string url in _url)
            {
                if (url == (SecurityModule.Security.LocalIp() + " " + value)) return false;
            }

            return maybeUrl;

        }



        private void _blockWebBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (CheckForTheCorrectUrl(_blockUrlTEXTBOX.Text))
            {
                _blockWebSite(_blockUrlTEXTBOX.Text, _blockUrl, _listBlockUrlFLP);
                _blockUrlTEXTBOX.Text = "";
            }
            else
                Error.ClientError.BadRequest();
        }

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
            heightPanel = (panel.Height * (size.Width / (panel.Width - controlUrl.Width - 40)) + 2) + 6;
            panel.Height = heightPanel;
            label.Location = new Point(0, 0);
            
            if (size.Width > panel.Width - controlUrl.Width)
            {
                heightPanel = (panel.Height * (size.Width / (panel.Width - controlUrl.Width - 40)) + 2) + 6;
                panel.Height = heightPanel;
                label.Location = new Point(0, 0);
            }
            else
            {
                panel.Height = 40;
                label.Location = new Point(0, panel.Height / 2 - label.Height / 2);
            }


            controlUrl.Controls.Add(pictureBox);
            panel.Controls.Add(controlUrl);
            panel.Controls.Add(label);

            return panel;
        }
        private void CreateItemsBlockingPrograms()
        {
            // 
            // _listBlockProgramFLP
            // 
            FlowLayoutPanel listBlockProgramFLP = new FlowLayoutPanel()
            {
                AutoScroll = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Location = new Point(2, 28),
                Name = "_listBlockProgramFLP",
                Size = new Size(554, 162),
                TabIndex = 3,
            };
            _listBlockProgramFLP = listBlockProgramFLP;
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
            // _blockProgramBUTTON
            // 
            Button blockProgramBUTTON = new Button()
            {
                BackColor = StyleWindows._mainColor,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Margin = new Padding(0, 3, 0, 3),
                Name = "_blockProgramBUTTON",
                Text = "Добавить приложение",
                Size = new Size(151, 27),
                TabIndex = 2,
                UseVisualStyleBackColor = false,
            };
            blockProgramBUTTON.FlatAppearance.BorderColor = StyleWindows._mainBorderColor;
            blockProgramBUTTON.FlatAppearance.BorderSize = 2;
            blockProgramBUTTON.FlatAppearance.MouseOverBackColor = StyleWindows._mainHoverButtonColor;
            blockProgramBUTTON.Click += new System.EventHandler(this._blockProgramBUTTON_Click);
            // 
            // _nameBlockProgramPANEL
            // 
            this._nameBlockProgramPANEL.BackColor = System.Drawing.Color.Transparent;
            this._nameBlockProgramPANEL.Controls.Add(blockProgramBUTTON);
            this._nameBlockProgramPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._nameBlockProgramPANEL.Location = new System.Drawing.Point(0, 0);
            this._nameBlockProgramPANEL.Name = "_nameBlockProgramPANEL";
            this._nameBlockProgramPANEL.Size = new System.Drawing.Size(558, 26);
            this._nameBlockProgramPANEL.TabIndex = 1;
            // 
            // _backPanelBlockProgramPANEL
            // 
            Panel backPanelBlockProgramPANEL = new Panel()
            {
                Location = new Point(3, 3),
                Name = "_backPanelBlockProgramPANEL",
                Size = new Size(_contentPanel.Width - 6, _contentPanel.Height - 9),
                TabIndex = 3,
            };
            backPanelBlockProgramPANEL.Controls.Add(listBlockProgramFLP);
            backPanelBlockProgramPANEL.Controls.Add(this._rightBorderBlockProgram);
            backPanelBlockProgramPANEL.Controls.Add(this._topBorderBlockProgram);
            backPanelBlockProgramPANEL.Controls.Add(this._leftBorderBlockProgram);
            backPanelBlockProgramPANEL.Controls.Add(this._bottomBorderBlockProgram);
            backPanelBlockProgramPANEL.Controls.Add(this._nameBlockProgramPANEL);
            _backPanelBlockProgramPANEL = backPanelBlockProgramPANEL;

            _contentPanel.Controls.Add(_backPanelBlockProgramPANEL);
        }

        //
        //  Блокировка приложения при нажатии на кнопку
        //
        private void _blockProgramBUTTON_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                string way = openFileDialog.FileName;



                string nameProgram = NEWUSER.GetTheProgramName(way);
                if (NEWUSER.CheckForRepeatedApplicationLock(nameProgram))
                {
                    Image icon = NEWUSER.IconPrograms(way);
                    Control value = CreatePanelBlockProgram(nameProgram, _foreColor, _blockPrograms, _listBlockProgramFLP, icon, true);
                    _blockPrograms.Add(value);
                    _program.Add(nameProgram);
                    _program.Add(way);
                    _listBlockProgramFLP.Controls.Add(value);
                }
                else
                    MessageBox.Show("Данная программа уже встречается в списке", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Control CreatePanelBlockProgram(string nameProgram, Color foreColor, List<Control> control, Control contentFLP, Image icon, bool createUser)
        {
            int padding = 3;
            int numberElement = control.Count;
            int heightPanel = 40;
            //
            //  panel
            //
            Panel panel = new Panel();
            panel.BackColor = _mainColor;
            panel.Name = "panel_" + numberElement;
            panel.Size = new Size(contentFLP.Width - padding * 2, heightPanel);
            panel.Margin = new Padding(padding);
            //
            //  iconProgram
            //
            PictureBox iconProgram = new PictureBox()
            {
                Name = "icon_" + numberElement,
                BackColor = Color.Transparent,
                Size = new Size(25, 25),
                Location = new Point(padding * 5, panel.Height / 2 - 25 / 2),
                BackgroundImage = icon,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            //
            //  pictureBox
            //
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(15, 15);
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Name = "pictureBox_" + numberElement;
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Location = new Point(0, panel.Height / 2 - 15 / 2);
            pictureBox.Paint += BlockUrlPICTUREBOX_Paint;
            pictureBox.Click += BlockProgramPICTUREBOX_Click;
            //
            //  controlProgram
            //
            Panel controlProgram = new Panel()
            {
                Dock = DockStyle.Right,
                Width = pictureBox.Width + padding * 3
            };
            //
            //  label
            //
            Label label = new Label();
            label.Name = "label_" + numberElement;
            label.AutoSize = true;
            label.Text = nameProgram;
            label.ForeColor = foreColor;
            label.BackColor = Color.Transparent;
            label.Font = _mainFont;
            label.Location = new Point(iconProgram.Location.X + iconProgram.Width + padding * 3, panel.Height / 2 - label.Height / 2);
            label.MaximumSize = new Size(panel.Width - controlProgram.Width - 40, 256);

            Size size = TextRenderer.MeasureText(label.Text, label.Font);

            //panel.Height = heightPanel;

            if (size.Width > panel.Width - controlProgram.Width)
            {
                panel.Height = (panel.Height * (size.Width / (panel.Width - (controlProgram.Width) - 40)) + 2) + 10;
                label.Location = new Point(iconProgram.Location.X + iconProgram.Width + padding * 3, 0);
            }
            else
            {
                panel.Height = heightPanel;
                label.Location = new Point(iconProgram.Location.X + iconProgram.Width + padding * 3, panel.Height / 2 - label.Height / 2);
            }
            controlProgram.Controls.Add(pictureBox);
            panel.Controls.Add(controlProgram);
            panel.Controls.Add(iconProgram);
            panel.Controls.Add(label);

            return panel;
        }

        private void BlockProgramPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            PictureBox pictureBox = (PictureBox)sender;
            if (MessageBox.Show("Удалить данное приложение?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
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

        private void BlockUrlPICTUREBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            PictureBox pictureBox = (PictureBox)sender;

            if (MessageBox.Show("Удалить данный адрес?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                // Выделение номера picturebox
                int numberPictireBox = NEWUSER.ScanNumberInBlockUrlList(pictureBox.Name);
                //  Поиск панелиs
                for (int track = 0; track < _blockUrl.Count; track++)
                {
                    if (numberPictireBox == NEWUSER.ScanNumberInBlockUrlList(_blockUrl[track].Name))
                    {
                        _listBlockUrlFLP.Controls.Remove(_blockUrl[track]);
                        _blockUrl.RemoveAt(track);
                        _url.RemoveAt(track);
                        break;
                    }
                }
            }

        }

        private void VerifySelectedItem(ListBox user, ListBox funct)
        {
            if (funct.SelectedIndex != -1 && user.SelectedIndex != -1)
            {
                if (user.SelectedIndex != _numberUser)
                {
                    // Сообщение об сбросе всех настроек
                    if (Warning.AllDataWillBeDeleted() == DialogResult.OK)
                    {
                        _numberUser = _usersLISTBOX.SelectedIndex;
                        ClearData(_numberUser, funct.SelectedIndex);
                        SwitchingFunctions(funct.SelectedIndex, _usersLISTBOX);
                    }
                }
                else
                {
                    SwitchingFunctions(funct.SelectedIndex, _usersLISTBOX);
                }

            }
            else
            {
                _numberUser = _usersLISTBOX.SelectedIndex;
                _numberFunction = funct.SelectedIndex;
            }

        }

        private void _functLISTBOX_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            //
            //  проверка выбранного элемента
            //
            VerifySelectedItem(_usersLISTBOX, _functLISTBOX);
            _numberFunction = _functLISTBOX.SelectedIndex;


        }
        #endregion

        private void _saveData_Click(object sender, EventArgs e)
        {
            StyleWindows.SoundClick(Form1._SoundEffect);
            if (_numberFunction == 0)
            {
                List<string> data = _url;

                if (SQL.SaveMoreFunctionAccount(_usersLISTBOX.SelectedItem.ToString(), data, _numberFunction) > 0)
                {
                    Warning.SaveDataUser();
                }
            }
            else if (_numberFunction == 1)
            {
                List<string> data = _program;
                if (SQL.SaveMoreFunctionAccount(_usersLISTBOX.SelectedItem.ToString(), data, _numberFunction) > 0)
                {
                    Warning.SaveDataUser();
                }
            }
            else if (_numberFunction == 2)
            {
                if (SQL.SaveDataRecordVideo(_usersLISTBOX.SelectedItem.ToString(), (_recordVideo) ? 1 : 0) > 0)
                {
                    Warning.SaveDataUser();
                }
            }
            else if (_numberFunction == 3)
            {
                int timeOut = Convert.ToInt32(_timeOutScreenShotNUMERICUPDOWN.Value) * 1000;
                if (SQL.SaveDataScreenShot(_usersLISTBOX.SelectedItem.ToString(), _screenShot, timeOut) > 0)
                {
                    Warning.SaveDataUser();
                }
            }
        }

    }
}
