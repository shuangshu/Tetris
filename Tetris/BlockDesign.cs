using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

namespace Tetris
{
    public class BlockDesign : Control
    {
        private int[,] encodeList;
        private Color colorSelection;
        public Color ColorSelection
        {
            get { return colorSelection; }
            set
            {
                colorSelection = value;
                Invalidate();
            }
        }
        public string Style
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    for (int a = 0; a < value.Length; a++)
                    {
                        encodeList[a % 5, a / 5] = (value[a] == '1') ? 1 : 0;
                    }
                }
                Invalidate();
            }
            get
            {
                StringBuilder list = new StringBuilder(25);
                for (int a = 0; a < encodeList.Length; a++)
                {
                    list.Append(encodeList[a % 5, a / 5] == 1 ? "1" : "0");
                }
                return list.ToString();
            }
        }

        private int blockIndex = -1;
        public int BlockIndex
        {
            get { return blockIndex; }
            set { blockIndex = value; }
        }

        private Graphics graphics = null;

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

        public BlockDesign()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            encodeList = new int[5, 5];
            BackColor = SystemColors.Window;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128)))
            {
                for (int a = 0; a <= 5; a++) g.DrawLine(pen, 0, a * 32, 160, a * 32);
                for (int b = 0; b <= 5; b++) g.DrawLine(pen, b * 32, 0, b * 32, 160);
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (encodeList[x, y] == 1)
                        {
                            using (SolidBrush s = new SolidBrush(colorSelection))
                            {
                                g.FillRectangle(s, x * 32 + 2, y * 32 + 2, 29, 29);
                            }
                        }
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int X = e.X / 32;
            int Y = e.Y / 32;
            int value = 0;
            if (colorSelection != null)
            {
                value = encodeList[X, Y] = encodeList[X, Y] == 1 ? 0 : 1;
                Color color = value == 1 ? colorSelection : SystemColors.Window;
                using (SolidBrush s = new SolidBrush(color))
                {
                    graphics.FillRectangle(s, X * 32 + 2, Y * 32 + 2, 29, 29);
                }
            }
        }
    }
}
