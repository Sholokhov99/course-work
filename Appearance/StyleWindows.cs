using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Media;
using System.Threading;

namespace Appearance
{
    public partial class StyleWindows : Form
    {
        //
        //  Элементы оформления
        //
        public Panel _controlWIndowPANEL;           // Панель управления окном
        public FlowLayoutPanel _infoWindowFLP;      // Панель содержащая иконку страницы и ее наименование
        public PictureBox _iconWindowFLP;           // Иконка страницы
        public Label _namePageLABEL;                // Наименование страницы
        public Panel _leftBorder;                   // Левая обводка страницы
        public Panel _rightBorder;                  // Правая обводка страницы
        public Panel _topBorder;                    // Верхняя обводка страницы
        public Panel _bottomBorder;                 // Нижняя обводка страницы
        public Panel _underPanelBorder;             // Обводка под панелью управления окном
        public PictureBox _closeWindowPICTUREBOX;   // Кнопка закрытия формы
        //
        //  Внешний вид программы
        //
        public static Color _mainColorText = Color.White;
        public static Color _mainColorLoginForm = ColorTranslator.FromHtml("#574B90");
        public static Color _mainBorderColorLoginForm = Color.FromArgb(255, 59, 46, 117);
        public static Color _mainHoverButtonColorLoginForm = Color.FromArgb(255, 111, 101, 158);

        public static Color _mainColor = ColorTranslator.FromHtml("#574B90");
        public static Color _mainBorderColor = Color.FromArgb(255, 59, 46, 117);
        public static Color _mainHoverButtonColor = Color.FromArgb(255, 111, 101, 158);
        public static Font _mainHeaderFontFamily { get; set; } = new Font(_nameFontFamily, 14.0F, FontStyle.Regular, GraphicsUnit.Point);
        public static Font _titleMainFont { get; set; } = new Font(_nameFontFamily, 17.0F, FontStyle.Bold, GraphicsUnit.Point);
        public static Font _mainFont { get; set; } = new Font(_nameFontFamily, 15.0F, FontStyle.Bold, GraphicsUnit.Point); // Основной стиль шрифта // Основной стиль шрифта
        public static Font _sideFont { get; set; } = new Font(_nameFontFamily, 9.0F, FontStyle.Regular, GraphicsUnit.Point);  // Стиль шрифта допольнительного текста
        public static Color _foreColor = Color.White;
        //
        //  Переменные
        //
        public int x, y;    // Координаты курсора на панели управления окном
        public static string _nameFontFamily { get; set; } = "Calibri";     // Наименование шрифта

        public static void SettingToolTip(ToolTip toolTip, Control element, string title, string message)
        {
            toolTip.IsBalloon = true;
            toolTip.ToolTipTitle = title;
            toolTip.SetToolTip(element, message);
            toolTip.ToolTipIcon = ToolTipIcon.Info;
        }

        public static Graphics DrawLockIconGraphics(object sender, PaintEventArgs e, Control picturebox, Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float paintSize = 2.0F;
            Pen p = new Pen(StyleWindows._mainColor, paintSize);
            Pen p1 = new Pen(Color.White, paintSize);
            SolidBrush brush = new SolidBrush(StyleWindows._mainColor);
            SolidBrush brushElipse = new SolidBrush(Color.White);

            int width = picturebox.Height / 2;
            int height = width;

            graphics.FillRectangle(brush, 0, height, width, height);
            graphics.DrawArc(p, 2, height / 2, width - 4, height, 360, -180);

            int x = picturebox.Height / 3 - 8 / 2;
            int y = height / 2 + picturebox.Height / 3;
            graphics.FillEllipse(brushElipse, x, y, 5, 5);
            x += (5 / 2);
            y += 4;
            Graphics gr1 = e.Graphics;
            gr1.SmoothingMode = SmoothingMode.None;
            gr1.DrawLine(p1, x + 1, y, x + 1, y + 4);

            return graphics;
        }

        public static Size NewSizeButton(Button button, Font font)
        {
            int widthButton = 169;
            int heightButton = 40;

            Size size = TextRenderer.MeasureText(button.Text, font);
            button.Size = new Size(widthButton, (size.Width > 152) ? size.Height * (size.Width / 152 + 2) : heightButton);
            return button.Size;
        }

        public static void SoundClick(int onSound)
        {
                new Thread(() => 
                {
                    if (onSound == 1)
                    {
                        new SoundPlayer(Properties.Resources.Click).Play();
                    }

                })
                {
                    IsBackground = true,
                }.Start();

        }

        private void InitializeComponent()
        {
            this._controlWIndowPANEL = new System.Windows.Forms.Panel();
            this._closeWindowPICTUREBOX = new System.Windows.Forms.PictureBox();
            this._infoWindowFLP = new System.Windows.Forms.FlowLayoutPanel();
            this._iconWindowFLP = new System.Windows.Forms.PictureBox();
            this._namePageLABEL = new System.Windows.Forms.Label();
            this._leftBorder = new System.Windows.Forms.Panel();
            this._rightBorder = new System.Windows.Forms.Panel();
            this._topBorder = new System.Windows.Forms.Panel();
            this._bottomBorder = new System.Windows.Forms.Panel();
            this._underPanelBorder = new System.Windows.Forms.Panel();
            this._controlWIndowPANEL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).BeginInit();
            this._infoWindowFLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconWindowFLP)).BeginInit();
            this.SuspendLayout();
            // 
            // _controlWIndowPANEL
            // 
            this._controlWIndowPANEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(75)))), ((int)(((byte)(144)))));
            this._controlWIndowPANEL.Controls.Add(this._closeWindowPICTUREBOX);
            this._controlWIndowPANEL.Controls.Add(this._infoWindowFLP);
            this._controlWIndowPANEL.Dock = System.Windows.Forms.DockStyle.Top;
            this._controlWIndowPANEL.Location = new System.Drawing.Point(2, 2);
            this._controlWIndowPANEL.Name = "_controlWIndowPANEL";
            this._controlWIndowPANEL.Size = new System.Drawing.Size(664, 25);
            this._controlWIndowPANEL.TabIndex = 0;
            this._controlWIndowPANEL.MouseDown += new System.Windows.Forms.MouseEventHandler(this._controlWIndowPANEL_MouseDown);
            this._controlWIndowPANEL.MouseMove += new System.Windows.Forms.MouseEventHandler(this._controlWIndowPANEL_MouseMove);
            // 
            // _closeWindowPICTUREBOX
            // 
            this._closeWindowPICTUREBOX.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeWindowPICTUREBOX.Location = new System.Drawing.Point(543, 6);
            this._closeWindowPICTUREBOX.Name = "_closeWindowPICTUREBOX";
            this._closeWindowPICTUREBOX.Size = new System.Drawing.Size(15, 15);
            this._closeWindowPICTUREBOX.TabIndex = 1;
            this._closeWindowPICTUREBOX.TabStop = false;
            this._closeWindowPICTUREBOX.Click += new System.EventHandler(this._closeWindowPICTUREBOX_Click);
            this._closeWindowPICTUREBOX.Paint += new System.Windows.Forms.PaintEventHandler(this._closeWindowPICTUREBOX_Paint);
            // 
            // _infoWindowFLP
            // 
            this._infoWindowFLP.AutoSize = true;
            this._infoWindowFLP.Controls.Add(this._iconWindowFLP);
            this._infoWindowFLP.Controls.Add(this._namePageLABEL);
            this._infoWindowFLP.Location = new System.Drawing.Point(10, 0);
            this._infoWindowFLP.Name = "_infoWindowFLP";
            this._infoWindowFLP.Size = new System.Drawing.Size(143, 25);
            this._infoWindowFLP.TabIndex = 0;
            this._infoWindowFLP.MouseDown += new System.Windows.Forms.MouseEventHandler(this._infoWindowFLP_MouseDown);
            this._infoWindowFLP.MouseMove += new System.Windows.Forms.MouseEventHandler(this._infoWindowFLP_MouseMove);
            // 
            // _iconWindowFLP
            // 
            this._iconWindowFLP.BackgroundImage = global::Appearance.Properties.Resources.setting;
            this._iconWindowFLP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._iconWindowFLP.Location = new System.Drawing.Point(3, 3);
            this._iconWindowFLP.Name = "_iconWindowFLP";
            this._iconWindowFLP.Size = new System.Drawing.Size(18, 18);
            this._iconWindowFLP.TabIndex = 0;
            this._iconWindowFLP.TabStop = false;
            this._iconWindowFLP.MouseDown += new System.Windows.Forms.MouseEventHandler(this._iconWindowFLP_MouseDown);
            this._iconWindowFLP.MouseMove += new System.Windows.Forms.MouseEventHandler(this._iconWindowFLP_MouseMove);
            // 
            // _namePageLABEL
            // 
            this._namePageLABEL.AutoSize = true;
            this._namePageLABEL.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._namePageLABEL.ForeColor = System.Drawing.Color.White;
            this._namePageLABEL.Location = new System.Drawing.Point(24, 2);
            this._namePageLABEL.Margin = new System.Windows.Forms.Padding(0, 2, 3, 0);
            this._namePageLABEL.Name = "_namePageLABEL";
            this._namePageLABEL.Size = new System.Drawing.Size(113, 14);
            this._namePageLABEL.TabIndex = 1;
            this._namePageLABEL.Text = "Название страницы";
            this._namePageLABEL.MouseDown += new System.Windows.Forms.MouseEventHandler(this._namePageLABEL_MouseDown);
            this._namePageLABEL.MouseMove += new System.Windows.Forms.MouseEventHandler(this._namePageLABEL_MouseMove);
            // 
            // _leftBorder
            // 
            this._leftBorder.BackColor = System.Drawing.Color.Black;
            this._leftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftBorder.Location = new System.Drawing.Point(0, 2);
            this._leftBorder.Name = "_leftBorder";
            this._leftBorder.Size = new System.Drawing.Size(2, 380);
            this._leftBorder.TabIndex = 1;
            // 
            // _rightBorder
            // 
            this._rightBorder.BackColor = System.Drawing.Color.Black;
            this._rightBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightBorder.Location = new System.Drawing.Point(666, 2);
            this._rightBorder.Name = "_rightBorder";
            this._rightBorder.Size = new System.Drawing.Size(2, 380);
            this._rightBorder.TabIndex = 2;
            // 
            // _topBorder
            // 
            this._topBorder.BackColor = System.Drawing.Color.Black;
            this._topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this._topBorder.Location = new System.Drawing.Point(0, 0);
            this._topBorder.Name = "_topBorder";
            this._topBorder.Size = new System.Drawing.Size(668, 2);
            this._topBorder.TabIndex = 3;
            // 
            // _bottomBorder
            // 
            this._bottomBorder.BackColor = System.Drawing.Color.Black;
            this._bottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomBorder.Location = new System.Drawing.Point(2, 380);
            this._bottomBorder.Name = "_bottomBorder";
            this._bottomBorder.Size = new System.Drawing.Size(664, 2);
            this._bottomBorder.TabIndex = 4;
            // 
            // _underPanelBorder
            // 
            this._underPanelBorder.BackColor = System.Drawing.Color.DarkGray;
            this._underPanelBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._underPanelBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this._underPanelBorder.Location = new System.Drawing.Point(2, 27);
            this._underPanelBorder.Name = "_underPanelBorder";
            this._underPanelBorder.Size = new System.Drawing.Size(664, 1);
            this._underPanelBorder.TabIndex = 5;
            // 
            // StyleWindows
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(668, 382);
            this.Controls.Add(this._underPanelBorder);
            this.Controls.Add(this._bottomBorder);
            this.Controls.Add(this._controlWIndowPANEL);
            this.Controls.Add(this._leftBorder);
            this.Controls.Add(this._rightBorder);
            this.Controls.Add(this._topBorder);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "StyleWindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StyleWindows_KeyDown);
            this._controlWIndowPANEL.ResumeLayout(false);
            this._controlWIndowPANEL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._closeWindowPICTUREBOX)).EndInit();
            this._infoWindowFLP.ResumeLayout(false);
            this._infoWindowFLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconWindowFLP)).EndInit();
            this.ResumeLayout(false);

        }
        //
        //  Закрытие окна
        //
        private void _closeWindowPICTUREBOX_Click(object sender, EventArgs e) => this.Close();

        public StyleWindows()
        {
            InitializeComponent();

            _controlWIndowPANEL.BackColor = _mainColor;
            _closeWindowPICTUREBOX.Location = new Point(this.Width - _closeWindowPICTUREBOX.Width - 18, 4);
            _namePageLABEL.Margin = new Padding(0, _controlWIndowPANEL.Height / 2 - _namePageLABEL.Height / 2, 0, 0);
            Color _rgbaBorder = Color.FromArgb(200, 0, 0, 0);
            _leftBorder.BackColor = _rgbaBorder;
            _rightBorder.BackColor = _rgbaBorder;
            _topBorder.BackColor = _rgbaBorder;
            _bottomBorder.BackColor = _rgbaBorder;
            _underPanelBorder.BackColor = Color.FromArgb(200, 91, 91, 91); ;
        }
        //
        //  Отрисовка кнопки закрытия формы
        //
        public void _closeWindowPICTUREBOX_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(Color.White, 2.0F);
            gr.DrawLine(p, 0, 0, _closeWindowPICTUREBOX.Width, _closeWindowPICTUREBOX.Height);
            gr.DrawLine(p, _closeWindowPICTUREBOX.Width, 0, 0, _closeWindowPICTUREBOX.Height);
        }
        #region Перемещение окна
        //
        //  Измененние местоположен формы
        //
        public void _controlWIndowPANEL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }
        //
        //  Измененние местоположен формы
        //
        public void _infoWindowFLP_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }
        //
        //  Измененние местоположен формы
        //
        public void _namePageLABEL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }
        //
        //  Измененние местоположен формы
        //
        public void _iconWindowFLP_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }
        //
        //  Получение данных о мпестоположении курсора
        //
        public void _iconWindowFLP_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
        //
        //  Получение данных о мпестоположении курсора
        //
        public void _namePageLABEL_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
        //
        //  Получение данных о мпестоположении курсора
        //
        public void _infoWindowFLP_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void StyleWindows_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //
        //  Получение данных о мпестоположении курсора
        //
        public void _controlWIndowPANEL_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
        #endregion
    }
}
