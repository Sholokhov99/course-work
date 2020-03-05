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

namespace Diploma2._0
{
    public partial class WindowImageList : Form
    {
        private int x, y;
        private int _indexImage = 0;
        List<Image> _images = new List<Image>();


        public WindowImageList(List<Image> images)
        {
            InitializeComponent();

            _images = images;
            this.BackgroundImage = _images[0];
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            _rightArrowPanel.BackColor = Color.FromArgb(20, 0, 0, 0);
            _rightArrow.BackColor = Color.FromArgb(0, 0, 0, 0);

            _leftArrow.BackColor = Color.FromArgb(0, 0, 0, 0);
            _leftArrowPanel.BackColor = Color.FromArgb(20, 0, 0, 0);
            LocationArrows();
        }

        private void LocationArrows()
        {
            _rightArrow.Location = new Point(0, this.ClientRectangle.Height / 2 - _rightArrow.Height / 2);
            _leftArrow.Location = new Point(0, this.ClientRectangle.Height / 2 - _rightArrow.Height / 2);
        }

        private void _rightArrow_Click(object sender, EventArgs e)
        {
            _indexImage = (_images.Count - 1 == _indexImage) ? _images.Count - 1 : _indexImage + 1;
            this.BackgroundImage = _images[_indexImage];
        }

        private void _leftArrow_Click(object sender, EventArgs e)
        {
            _indexImage = (_indexImage == 0) ? 0 : _indexImage - 1;
            this.BackgroundImage = _images[_indexImage];
        }

        private void WindowImageList_Resize(object sender, EventArgs e)
        {
            LocationArrows();
        }

        private void _topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point position = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
                this.Location = position;
            }
        }
    }
}
