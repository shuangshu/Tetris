using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    public class ColorDropDown : DropDownBase
    {
        private List<ColorData> colorLists;
        private int areaX;
        private int areaY;
        private Color borderColor = Color.FromArgb(128, 128, 128);

        private Pen normalPen = null;
        private SolidBrush hightBrush = null;
        private Pen hightPen = null;

        private ColorData previousColorData = null;
        private ColorData currentColorData = null;

        private Graphics graphics = null;

        private int colorIndex = -1;
        private Color selection;
        public Color Selection
        {
            get { return selection; }
            set
            {
                selection = value;
                if (colorLists.Count > 0)
                {
                    for (int i = 0; i < colorLists.Count; i++)
                    {
                        Color color = colorLists[i].Color;
                        if ((color.R == value.R) && (color.G == value.G) && (color.B == value.B))
                        {
                            colorIndex = i;
                        }
                    }
                }
                Invalidate();
            }
        }

        public event ColorChangedEventHandler ColorChanged;

        public ColorDropDown()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = SystemColors.Window;
            Size = new Size(170, 200);
            normalPen = new Pen(borderColor);
            hightBrush = new SolidBrush(Color.FromArgb(208, 225, 253));
            hightPen = new Pen(Color.FromArgb(49, 106, 197));
            colorLists = new List<ColorData>();
            int index = 0;
            for (int b = 0; b < 6; b++)
            {
                areaY = 42 + b * 20 + 4;
                for (int a = 0; a < 8; a++)
                {
                    areaX = 4 + a * 20 + 4;
                    ColorData colorData = new ColorData(index, areaX, areaY, new Size(14, 14), ColorData.Colors[b, a]);
                    colorLists.Add(colorData);
                    index++;
                }
                areaX = 8;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.graphics = Graphics.FromHwnd(Handle);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.graphics.Dispose();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(normalPen, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            using (SolidBrush solidBrush = new SolidBrush(selection))
            {
                g.FillRectangle(solidBrush, 10, 14, 150, 20);
            }
            g.DrawRectangle(normalPen, 8, 12, 153, 23);
            for (int i = 0; i < colorLists.Count; i++)
            {
                ColorData data = colorLists[i];
                if (i == colorIndex)
                {
                    g.FillRectangle(hightBrush, data.X - 2, data.Y - 2, 14 + 2, 14 + 2);
                    g.DrawRectangle(hightPen, data.X - 3, data.Y - 3, 14 + 5, 14 + 5);
                }
                using (SolidBrush solidBrush = new SolidBrush(data.Color))
                {
                    g.FillRectangle(solidBrush, data.X, data.Y, 14, 14);
                }
                g.DrawRectangle(normalPen, data.X - 1, data.Y - 1, 15, 15);
            }
            int Y = areaY;
            Y += 22;
            g.DrawLine(normalPen, 6, Y, 160, Y);
            Y += 8;
            g.DrawString("自定义颜色", SystemFonts.DefaultFont, SystemBrushes.WindowText, 50, Y);
            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int Y = areaY;
            Y += 30;
            Rectangle rectangle = new Rectangle(6, Y, 160, 30);
            if (rectangle.Contains(e.Location))
            {
                ColorDialog colorDialog = new ColorDialog();
                colorDialog.FullOpen = true;
                colorDialog.AllowFullOpen = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Selection = colorDialog.Color;
                    Popdown();
                    OnColorChanged(Selection);
                }
            }
            else if (currentColorData != null)
            {
                Selection = currentColorData.Color;
                Popdown();
                OnColorChanged(Selection);
            }
            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool contains = false;
            for (int i = 0; i < colorLists.Count; i++)
            {
                ColorData data = colorLists[i];
                Rectangle rectangle = new Rectangle(data.X, data.Y, data.Size.Width, data.Size.Height);
                if (rectangle.Contains(e.Location))
                {
                    currentColorData = data;
                    contains = true;
                    break;
                }
            }
            if (contains)
            {
                if (previousColorData != null && previousColorData != currentColorData && previousColorData.Index != colorIndex)
                {
                    Rectangle previousRectangle = new Rectangle(previousColorData.X, previousColorData.Y, previousColorData.Size.Width, previousColorData.Size.Height);
                    graphics.FillRectangle(SystemBrushes.Window, previousRectangle.X - 2, previousRectangle.Y - 2, previousRectangle.Width + 2, previousRectangle.Height + 2);
                    graphics.DrawRectangle(SystemPens.Window, previousRectangle.X - 3, previousRectangle.Y - 3, previousRectangle.Width + 5, previousRectangle.Height + 5);
                    using (SolidBrush solidBrush = new SolidBrush(previousColorData.Color))
                    {
                        graphics.FillRectangle(solidBrush, previousRectangle);
                    }
                    graphics.DrawRectangle(normalPen, previousRectangle.X - 1, previousRectangle.Y - 1, previousRectangle.Width + 1, previousRectangle.Height + 1);
                }
                if (currentColorData != null)
                {
                    previousColorData = currentColorData;
                    Rectangle currentRectangle = new Rectangle(currentColorData.X, currentColorData.Y, currentColorData.Size.Width, currentColorData.Size.Height);
                    graphics.FillRectangle(hightBrush, currentRectangle.X - 2, currentRectangle.Y - 2, currentRectangle.Width + 2, currentRectangle.Height + 2);
                    graphics.DrawRectangle(hightPen, currentRectangle.X - 3, currentRectangle.Y - 3, currentRectangle.Width + 5, currentRectangle.Height + 5);
                    using (SolidBrush solidBrush = new SolidBrush(currentColorData.Color))
                    {
                        graphics.FillRectangle(solidBrush, currentRectangle);
                    }
                    graphics.DrawRectangle(normalPen, currentRectangle.X - 1, currentRectangle.Y - 1, currentRectangle.Width + 1, currentRectangle.Height + 1);
                }
            }
            else
            {
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected virtual void OnColorChanged(Color color)
        {
            if (ColorChanged != null) ColorChanged(this, color);
        }

        protected override void Dispose(bool disposing)
        {
            if (normalPen != null) normalPen.Dispose();
            if (hightBrush != null) hightBrush.Dispose();
            if (hightPen != null) hightPen.Dispose();
            base.Dispose(disposing);
        }
    }

    public delegate void ColorChangedEventHandler(object sender, Color color);

    public class ColorData
    {
        private int x;
        private int y;
        private Color color;

        private int index;
        public int Index
        {
            get { return index; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Color Color
        {
            get { return color; }
        }

        private Size size;
        public Size Size
        {
            get { return size; }
        }

        public ColorData(int index, int x, int y, Size size)
        {
            this.x = x;
            this.y = y;
            this.index = index;
            this.size = size;
        }

        public ColorData(int x, int y, Size size, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.size = size;
        }

        public ColorData(int index, int x, int y, Size size, Color color)
            : this(x, y, size, color)
        {
            this.index = index;
        }

        public static Color[,] Colors = new Color[,]{
        {
            Color.FromArgb(255,128,128),Color.FromArgb(255,255,128),Color.FromArgb(128,255,128),Color.FromArgb(0,255,128), Color.FromArgb(128,255,255),Color.FromArgb(0,128,255),Color.FromArgb(255,128,192),Color.FromArgb(255,128,255)
        },
        {
            Color.FromArgb(255,0,0),Color.FromArgb(255,255,0),Color.FromArgb(128,255,0),Color.FromArgb(0,255,64), Color.FromArgb(0,255,255),Color.FromArgb(0,128,192),Color.FromArgb(128,128,192),Color.FromArgb(255,0,255)
        },
        {
            Color.FromArgb(128,64,64),Color.FromArgb(255,128,64),Color.FromArgb(0,255,0),Color.FromArgb(0,128,128), Color.FromArgb(0,64,128),Color.FromArgb(128,128,255),Color.FromArgb(128,0,64),Color.FromArgb(255,0,128)
        },
        {
            Color.FromArgb(128,0,0),Color.FromArgb(255,128,0),Color.FromArgb(0,128,0),Color.FromArgb(0,128,64), Color.FromArgb(0,0,255),Color.FromArgb(0,0,160),Color.FromArgb(128,0,128),Color.FromArgb(128,0,255)
        },
        {
            Color.FromArgb(64,0,0),Color.FromArgb(128,64,0),Color.FromArgb(0,64,0),Color.FromArgb(0,64,64), Color.FromArgb(0,0,128),Color.FromArgb(0,0,64),Color.FromArgb(64,0,64),Color.FromArgb(64,0,128)
        },
        {
            Color.FromArgb(0,0,0),Color.FromArgb(128,128,0),Color.FromArgb(128,128,64),Color.FromArgb(128,128,128), Color.FromArgb(64,128,128),Color.FromArgb(192,192,192),Color.FromArgb(64,0,64),Color.FromArgb(255,255,255)
        }
        };
    }
}
