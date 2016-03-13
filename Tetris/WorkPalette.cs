using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Timers;
using System.Runtime.InteropServices;
using Windows;
using System.Media;
using System.Threading;

namespace Tetris
{
    public class WorkPalette : Control
    {
        private int horizontal = 45;
        public int Horizontal
        {
            get { return horizontal; }
            set
            {
                if (value == 0) throw new ArgumentException("无效的Horizontal");
                horizontal = value;
                Size = new Size(horizontal * pixels, vertical * pixels);
            }
        }
        private int vertical = 45;
        public int Vertical
        {
            get { return vertical; }
            set
            {
                if (value == 0) throw new ArgumentException("无效的Vertical");
                vertical = value;
                Size = new Size(horizontal * pixels, vertical * pixels);
            }
        }
        private int pixels = 12;
        public int Pixels
        {
            get { return pixels; }
            set
            {
                if (value == 0) throw new ArgumentException("无效的Pixels");
                pixels = value;
                Size = new Size(horizontal * pixels, vertical * pixels);
            }
        }

        private Block workBlock = null;
        public Block WorkBlock
        {
            get { return workBlock; }
            set { workBlock = value; }
        }

        private Block nextBlock = null;
        public Block NextBlock
        {
            get { return nextBlock; }
            set { nextBlock = value; }
        }

        private Color[,] colorList = null;
        private Size cacheSize = Size.Empty;
        private SoundPlayer soundPlayer = null;
        private int IDEvent = 0;
        private PreviewPalette previewPalette = null;
        public PreviewPalette PreviewPalette
        {
            get { return previewPalette; }
            set { previewPalette = value; }
        }

        public WorkPalette(Setting setting)
        {
            this.LoadSetting(setting);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_CONTROLPARENT;
                cp.Style |= NativeMethods.WS_CHILD | NativeMethods.WS_CLIPSIBLINGS;
                return cp;
            }
        }

        public WorkPalette()
        {
            this.Size = new Size(45 * 12, 45 * 12);
            this.cacheSize = this.Size;
            this.colorList = new Color[45, 45];
        }

        public void LoadSetting(Setting setting)
        {
            this.setting = setting;
            if (setting.AllowVoice) soundPlayer = new SoundPlayer();
            this.Size = new Size(setting.Horizontal * setting.Pixels, setting.Vertical * setting.Pixels);
            this.cacheSize = this.Size;
            this.clientRectangle = new Rectangle(0, 0, cacheSize.Width, cacheSize.Height);
            this.colorList = new Color[setting.Horizontal, setting.Vertical];
            this.workBlock = new Block(setting);
            this.workBlock.X = setting.Horizontal / 2;
            this.Reset();
        }

        private Rectangle clientRectangle = Rectangle.Empty;
        public new Rectangle ClientRectangle
        {
            get { return clientRectangle; }
        }

        private Setting setting = null;
        public Setting Setting
        {
            get { return setting; }
        }

        private Graphics graphics = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (setting != null)
            {
                using (SolidBrush colorBrush = new SolidBrush(setting.BackColor))
                {
                    g.FillRectangle(colorBrush, e.ClipRectangle);
                }
            }
            ControlPaint.DrawBorder(g, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
            DrawPalette(g, clientRectangle);
            if (workBlock != null && workBlock.BlockData != null)
            {
                using (SolidBrush colorBrush = new SolidBrush(workBlock.BlockData.Color))
                {
                    for (int a = 0; a < workBlock.Length; a++)
                    {
                        Point p = workBlock.BlockData.Points[a];
                        Rectangle rectangle = workBlock.PointToRectangle(p);
                        g.FillRectangle(colorBrush, rectangle);
                        Rectangle bounds = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
                        ControlPaint.DrawBorder(g, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_TIMER:
                    TimerHandle(m.WParam.ToInt32());
                    break;
            }
            base.WndProc(ref m);
        }

        private void TimerHandle(int wParam)
        {
            if (wParam == NativeMethods.ID_TIMER_EVENT)
            {
                ValidateSuccess();
                MoveDown();
            }
        }

        public void BeginLoop()
        {
            if (IsHandleCreated)
            {
                IDEvent = (int)UnsafeNativeMethods.SetTimer(new HandleRef(this, Handle), NativeMethods.ID_TIMER_EVENT, 500, IntPtr.Zero);
            }
        }

        public void EndLoop()
        {
            if (IDEvent != 0)
            {
                UnsafeNativeMethods.KillTimer(new HandleRef(this, Handle), NativeMethods.ID_TIMER_EVENT);
                IDEvent = 0;
            }
        }

        public bool MoveDown()
        {
            int X = workBlock.X;
            int Y = workBlock.Y + 1;
            for (int i = 0; i < workBlock.Length; i++)
            {
                if ((Y - workBlock[i].Y) > (setting.Vertical - 1)) return false;
                if (!colorList[X + workBlock[i].X, Y - workBlock[i].Y].IsEmpty) return false;
            }
            Draw(setting.BackColor, clientRectangle, true);
            workBlock.Y++;
            Draw(workBlock.BlockData.Color, clientRectangle, false);
            return true;
        }

        public void MoveL()
        {
            int X = workBlock.X - 1;
            int Y = workBlock.Y;
            for (int i = 0; i < workBlock.Length; i++)
            {
                if ((X + workBlock[i].X) < 0) return;
                if (!colorList[X + workBlock[i].X, Y - workBlock[i].Y].IsEmpty) return;
            }
            Draw(setting.BackColor, clientRectangle, true);
            workBlock.X--;
            Draw(workBlock.BlockData.Color, clientRectangle, false);
        }

        public void MoveR()
        {
            int X = workBlock.X + 1;
            int Y = workBlock.Y;
            for (int i = 0; i < workBlock.Length; i++)
            {
                if ((X + workBlock[i].X) > setting.Horizontal - 1) return;
                if (!colorList[X + workBlock[i].X, Y - workBlock[i].Y].IsEmpty) return;
            }
            Draw(setting.BackColor, clientRectangle, true);
            workBlock.X++;
            Draw(workBlock.BlockData.Color, clientRectangle, false);
        }

        public void MoveDeasilRotate()
        {
            for (int i = 0; i < workBlock.Length; i++)
            {
                int X = workBlock.X + workBlock[i].Y;
                int Y = workBlock.Y + workBlock[i].X;
                if (X < 0 || X > setting.Horizontal - 1) return;
                if (Y < 0 || Y > setting.Vertical - 1) return;
                if (!colorList[X, Y].IsEmpty) return;
            }
            Draw(setting.BackColor, clientRectangle, true);
            workBlock.DeasilRotate();
            Draw(workBlock.BlockData.Color, clientRectangle, false);
        }

        public void MoveContraRotate()
        {
            for (int i = 0; i < workBlock.Length; i++)
            {
                int X = workBlock.X - workBlock[i].Y;
                int Y = workBlock.Y - workBlock[i].X;
                if (X < 0 || X > setting.Horizontal - 1) return;
                if (Y < 0 || Y > setting.Vertical - 1) return;
                if (!colorList[X, Y].IsEmpty) return;
            }
            Draw(setting.BackColor, clientRectangle, true);
            workBlock.ContraRotate();
            Draw(workBlock.BlockData.Color, clientRectangle, false);
        }

        public void CanRemoveBlock()
        {
            int lowRow = workBlock.Y - workBlock[0].Y;
            int highRow = lowRow;
            for (int i = 0; i < workBlock.Length; i++)
            {
                int Y = workBlock.Y - workBlock[i].Y;
                if (Y < lowRow) lowRow = Y;
                if (Y > highRow) highRow = Y;
            }
            bool needDraw = false;
            for (int a = lowRow; a <= highRow; a++)
            {
                bool rowFull = true;
                for (int b = 0; b < setting.Horizontal; b++)
                {
                    if (colorList[b, a].IsEmpty)
                    {
                        rowFull = false;
                        break;
                    }
                }
                if (rowFull)
                {
                    needDraw = true;
                    for (int k = a; k > 0; k--)
                    {
                        for (int c = 0; c < setting.Horizontal; c++)
                        {
                            colorList[c, k] = colorList[c, k - 1];
                        }
                    }
                    for (int d = 0; d < setting.Horizontal; d++)
                    {
                        colorList[d, 0] = Color.Empty;
                    }
                }
            }
            if (needDraw)
            {
                if (setting.AllowVoice && soundPlayer != null)
                {
                    soundPlayer.Stream = Properties.Resources.deleterow;
                    soundPlayer.Load();
                    soundPlayer.Play();
                }
                DrawPalette(clientRectangle);
            }
        }

        public void ValidateSuccess()
        {
            if (previewPalette == null || previewPalette.PreviewBlock == null) return;
            bool overRow = false;
            for (int i = 0; i < workBlock.Length; i++)
            {
                int x = workBlock.X + workBlock[i].X;
                int y = workBlock.Y - workBlock[i].Y;
                if (y == setting.Vertical - 1)
                {
                    overRow = true;
                    break;
                }
                if (!colorList[x, y + 1].IsEmpty)
                {
                    overRow = true;
                    break;
                }
            }
            if (overRow)
            {
                for (int i = 0; i < workBlock.Length; i++)
                {
                    colorList[workBlock.X + workBlock[i].X, workBlock.Y - workBlock[i].Y] = workBlock.BlockData.Color;
                }
                CanRemoveBlock();
                workBlock = previewPalette.PreviewBlock.Clone();
                workBlock.X = setting.Horizontal / 2;
                int Y = -2;
                for (int i = 0; i < workBlock.Length; i++)
                {
                    if (workBlock[i].Y > Y) Y = workBlock[i].Y;
                }
                workBlock.Y = Y;
                for (int i = 0; i < workBlock.Length; i++)
                {
                    if (!colorList[workBlock.X + workBlock[i].X, workBlock.Y - workBlock[i].Y].IsEmpty)
                    {
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;
                        using (Graphics g = CreateGraphics())
                        {
                            g.DrawString("GAME OVER", new Font("Arial Black", 25f),
                                new SolidBrush(Color.White),
                                new RectangleF(0, setting.Vertical * setting.Pixels / 2 - 100, setting.Horizontal * setting.Pixels, 100), drawFormat);
                        }
                        EndLoop();

                        if (setting.AllowVoice && soundPlayer != null)
                        {
                            soundPlayer.Stream = Properties.Resources.gameover;
                            soundPlayer.Load();
                            soundPlayer.Play();
                        }
                        return;
                    }
                }
                Draw(workBlock.BlockData.Color, clientRectangle, false);
                Block previewBlock = new Block(TetrisSetting.Setting);
                previewBlock.GenerateBlockData();
                previewBlock.X = 2;
                previewBlock.Y = 2;
                previewPalette.PreviewBlock = previewBlock;
                previewPalette.Clean();
                previewPalette.Draw(previewBlock.BlockData.Color, 24);
            }
        }

        public void CreatePalette()
        {
            base.RecreateHandle();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
            if (soundPlayer != null) soundPlayer.Dispose();
            EndLoop();
        }

        public void Clean(Color clean)
        {
            lock (graphics)
            {
                using (SolidBrush colorBrush = new SolidBrush(clean))
                {
                    graphics.FillRectangle(colorBrush, clientRectangle);
                }
                ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
            }
        }

        public void Draw(Color color, Rectangle clientRectangle, bool clearBlock)
        {
            lock (graphics)
            {
                ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                using (SolidBrush colorBrush = new SolidBrush(color))
                {
                    for (int a = 0; a < workBlock.Length; a++)
                    {
                        Point p = workBlock[a];
                        Rectangle rectangle = workBlock.PointToRectangle(p);
                        graphics.FillRectangle(colorBrush, rectangle);
                        if (clearBlock)
                        {
                            using (Pen pen = new Pen(setting.BackColor))
                            {
                                graphics.DrawRectangle(pen, rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
                            }
                        }
                        else
                        {
                            Rectangle bounds = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
                            ControlPaint.DrawBorder(graphics, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            for (int b = 0; b < setting.Vertical; b++)
            {
                for (int a = 0; a < setting.Horizontal; a++)
                {
                    colorList[a, b] = Color.Empty;
                }
            }
        }

        public void DrawPalette(Graphics graphics, Rectangle clientRectangle)
        {
            if (setting == null) return;
            using (SolidBrush colorBrush = new SolidBrush(setting.BackColor))
            {
                graphics.FillRectangle(colorBrush, clientRectangle);
            }
            ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
            for (int b = 0; b < setting.Vertical; b++)
            {
                for (int a = 0; a < setting.Horizontal; a++)
                {
                    if (!colorList[a, b].IsEmpty)
                    {
                        using (SolidBrush colorBrush = new SolidBrush(colorList[a, b]))
                        {
                            graphics.FillRectangle(colorBrush, a * setting.Pixels + 1, b * setting.Pixels + 1, setting.Pixels - 2, setting.Pixels - 2);
                            Rectangle bounds = new Rectangle(a * setting.Pixels, b * setting.Pixels, setting.Pixels - 1, setting.Pixels - 1);
                            ControlPaint.DrawBorder(graphics, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                        }
                    }
                }
            }
        }

        public void DrawPalette(Rectangle clientRectangle)
        {
            if (setting == null) return;
            lock (graphics)
            {
                using (SolidBrush colorBrush = new SolidBrush(setting.BackColor))
                {
                    graphics.FillRectangle(colorBrush, clientRectangle);
                }
                ControlPaint.DrawBorder(graphics, clientRectangle, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                for (int b = 0; b < setting.Vertical; b++)
                {
                    for (int a = 0; a < setting.Horizontal; a++)
                    {
                        if (!colorList[a, b].IsEmpty)
                        {
                            using (SolidBrush colorBrush = new SolidBrush(colorList[a, b]))
                            {
                                graphics.FillRectangle(colorBrush, a * setting.Pixels + 1, b * setting.Pixels + 1, setting.Pixels - 2, setting.Pixels - 2);
                                Rectangle bounds = new Rectangle(a * setting.Pixels, b * setting.Pixels, setting.Pixels - 1, setting.Pixels - 1);
                                ControlPaint.DrawBorder(graphics, bounds, Color.FromKnownColor(KnownColor.SkyBlue), ButtonBorderStyle.Solid);
                            }
                        }
                    }
                }
            }
        }
    }
}
