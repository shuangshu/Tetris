using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Tetris
{
    /// <summary>
    /// 颜色选择按钮
    /// </summary>
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class ColorSelection : Button
    {
        public Control ActiveControl
        {
            get
            {
                return colorDropDown.ActiveControl;
            }
            set
            {
                if (value != null && value.Handle != IntPtr.Zero)
                {
                    DropDownBase.SetActiveWindow(value.Handle);
                    colorDropDown.ActiveControl = value;
                }
            }
        }
        private ColorDropDown colorDropDown;

        private Color selection;
        public Color Selection
        {
            get { return selection; }
            set
            {
                selection = value;
                Invalidate();
            }
        }

        private SolidBrush hightBrush = null;
        private Pen normalPen = null;

        private BlockDesign blockDesign;
        public BlockDesign BlockDesign
        {
            get { return blockDesign; }
            set { blockDesign = value; }
        }

        public ColorSelection()
        {
            colorDropDown = new ColorDropDown();
            this.Click += new EventHandler(ColorSelection_Click);
            hightBrush = new SolidBrush(Color.FromArgb(208, 225, 253));
            normalPen = new Pen(Color.FromArgb(128, 128, 128));
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            colorDropDown.ColorChanged += new ColorChangedEventHandler(colorDropDown_ColorChanged);
            selection = ColorData.Colors[0, 0];//默认
        }

        private void colorDropDown_ColorChanged(object sender, Color color)
        {
            Selection = color;
            if (blockDesign != null) blockDesign.ColorSelection = color;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            using (SolidBrush solidBrush = new SolidBrush(selection))
            {
                g.FillRectangle(solidBrush, 10, Size.Height / 2 - 8, 16, 16);
            }
            g.DrawRectangle(normalPen, 8, Size.Height / 2 - 10, 19, 19);
        }

        protected override void Dispose(bool disposing)
        {
            if (hightBrush != null) hightBrush.Dispose();
            if (normalPen != null) normalPen.Dispose();
            base.Dispose(disposing);
        }

        private void ColorSelection_Click(object sender, EventArgs e)
        {
            colorDropDown.Popup(this, new Point(0, Size.Height));
            colorDropDown.Selection = selection;
        }
    }
}
