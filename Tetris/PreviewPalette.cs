using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Windows;
using System.Diagnostics;

namespace Tetris
{
    public class PreviewPalette : Control
    {
        private Block previewBlock = null;
        public Block PreviewBlock
        {
            get { return previewBlock; }
            set
            {
                previewBlock = value;
            }
        }

        private Size clientSize = Size.Empty;

        private BorderStyle borderStyle = System.Windows.Forms.BorderStyle.None;
        public BorderStyle BorderStyle
        {
            get
            {
                return borderStyle;
            }

            set
            {
                if (value != borderStyle)
                {
                    borderStyle = value;
                    RecreateHandle();
                }
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                switch (borderStyle)
                {
                    case BorderStyle.Fixed3D:
                        cp.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        break;
                    case BorderStyle.FixedSingle:
                        cp.Style |= NativeMethods.WS_BORDER;
                        break;
                }
                return cp;
            }
        }

        public PreviewPalette()
        {
            this.BackColor = SystemColors.Window;
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle clientRectangle = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
            ControlPaint.DrawBorder(pevent.Graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
            if (previewBlock != null)
            {
                using (SolidBrush colorBrush = new SolidBrush(previewBlock.BlockData.Color))
                {
                    for (int a = 0; a < previewBlock.Length; a++)
                    {
                        Point p = previewBlock[a];
                        Rectangle rectangle = previewBlock.PointToRectangle(p, 24);
                        pevent.Graphics.FillRectangle(colorBrush, rectangle);
                        Rectangle bounds = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
                        ControlPaint.DrawBorder(pevent.Graphics, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                    }
                }
            }
            base.OnPaint(pevent);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_SIZE:
                    {
                        clientSize.Width = NativeMethods.SignedLOWORD((int)m.LParam);
                        clientSize.Height = NativeMethods.SignedHIWORD((int)m.LParam);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public void Clean()
        {
            Rectangle clientRectangle = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
            graphics.FillRectangle(SystemBrushes.Window, clientRectangle);
            ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
        }

        public void Draw(Color color, int pixels)
        {
            if (previewBlock == null) return;
            Rectangle clientRectangle = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
            graphics.FillRectangle(SystemBrushes.Window, clientRectangle);
            ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
            using (SolidBrush colorBrush = new SolidBrush(color))
            {
                for (int a = 0; a < previewBlock.Length; a++)
                {
                    Point p = previewBlock[a];
                    Rectangle rectangle = previewBlock.PointToRectangle(p, pixels);
                    graphics.FillRectangle(colorBrush, rectangle);
                    Rectangle bounds = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
                    ControlPaint.DrawBorder(graphics, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                }
            }
        }
    }
}
