using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Appearance
{
    public partial class ToolBox
    {
        public Color _offColor { get; } = Color.Red;
        public int radius = 20;

        public ToolBox()
        { }
        //
        //  Закругление краев
        //
        public static void SetRoundedShape(Control control, int radius)
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


        #region CheckBox
        //
        //  Функция перемещения ползунка
        //
        public static void MovingTheStatusSlider(bool check, Control pointCondition, Control conditionCheck, Control boxCheck, Timer timer)
        {
            StyleWindows STYLEWINDOWS = new StyleWindows();

            if (check)
            {
                // Функция включается
                if (pointCondition.Width + 2 != pointCondition.Location.X)
                    pointCondition.Location = new Point(pointCondition.Location.X + 1, 0);
                else
                {
                    conditionCheck.Text = "On";
                    conditionCheck.ForeColor = StyleWindows._mainColor;
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
        //
        //  Отрисовка индикатора состояния CheckBox
        //
        public static void PointConditionCheckBoxGraphics(object sender, PaintEventArgs e, bool check)
        {
            ToolBox TOOLBOX = new ToolBox();
            StyleWindows STYLEWINDOWS = new StyleWindows();

            Graphics gr = e.Graphics;
            SolidBrush brush;
            if (check)
            {
                brush = new SolidBrush(StyleWindows._mainColor);
            }
            else
            {
                brush = new SolidBrush(TOOLBOX._offColor);
            }
            Pen pen = new Pen(Color.Red, 1.0f);
            gr.SmoothingMode = SmoothingMode.HighQuality;

            gr.FillEllipse(brush, 0, 0, 18, 18);
        }
        //
        //  Отрисовка объекта CheckBox
        //
        public static void CheckedShroolCheckBoxGraphics(bool positionCheck, Control control, object sender, PaintEventArgs e)
        {
            StyleWindows STYLEWINDOWS = new StyleWindows();
            ToolBox TOOLBOX = new ToolBox();

            Graphics gr = e.Graphics;
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p;
            if (positionCheck)
            {
                p = new Pen(StyleWindows._mainColor, 2.0f);
            }
            else
            {
                p = new Pen(TOOLBOX._offColor, 2.0f);
            }
            int x = control.Location.X;
            int y = control.Location.Y;
            int width = x + control.Width;
            int height = control.Height;
            int padding = 1;


            gr.DrawLine(p, x + (TOOLBOX.radius / 2) - padding, y - padding, width - (TOOLBOX.radius / 2) + 1, y - padding);                        // top line
            gr.DrawArc(p, width - TOOLBOX.radius + padding, y - padding, TOOLBOX.radius, TOOLBOX.radius, 270, 90);                                         // 
            gr.DrawLine(p, width + padding, y + (TOOLBOX.radius / 2) - 3, width + padding, y + height - (TOOLBOX.radius / 2) + 3);                     // right line    
            gr.DrawArc(p, width - TOOLBOX.radius + padding, y + height - TOOLBOX.radius + padding, TOOLBOX.radius, TOOLBOX.radius, 360, 90);                       //
            gr.DrawLine(p, width - TOOLBOX.radius / 2 + padding, y + height + padding, x + (TOOLBOX.radius / 2) - 1, y + height + padding);        // bottom line
            gr.DrawArc(p, x - padding, y + height - TOOLBOX.radius + padding, TOOLBOX.radius, TOOLBOX.radius, 90, 90);                                     //
            gr.DrawLine(p, x + padding * 2, y + height - (TOOLBOX.radius / 2) + padding * 2, x + padding * 2, y - padding * 2 + (TOOLBOX.radius / 2));     // left line
            gr.DrawArc(p, x - padding, y - padding, TOOLBOX.radius, TOOLBOX.radius, 180, 90);                                                      //
        }
        //
        //  Появление подпунктов
        //
        public static void AppearanceOfSubItems(bool check, Control control, int heightMaxVolume, Timer timer)
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
        #endregion

    }
}
