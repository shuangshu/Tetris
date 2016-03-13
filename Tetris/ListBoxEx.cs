using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    public class ListBoxEx : ListBox
    {
        private static Color lightColor = Color.FromArgb(243, 248, 251);
        private static Color hightColor = Color.FromArgb(54, 141, 205);

        private ToolTip toolTip = null;

        private SolidBrush hightBrush = null;
        private SolidBrush lightBrush = null;
        private Pen normalPen = null;

        private ListBoxItem currentItem = null;
        private ListBoxItem previousItem = null;

        public ListBoxEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.ItemHeight = 30;
            this.normalPen = new Pen(Color.FromArgb(128, 128, 128));
            this.hightBrush = new SolidBrush(hightColor);
            this.lightBrush = new SolidBrush(lightColor);
            this.toolTip = new ToolTip();
            this.toolTip.ReshowDelay = 10;
            this.toolTip.UseFading = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int Y = 0;
            if (Items != null && Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Y += ItemHeight;
                    if (e.Location.Y >= (Y - ItemHeight) && e.Location.Y <= Y)
                    {
                        currentItem = (ListBoxItem)Items[i];
                        break;
                    }
                }
                if (currentItem != null && previousItem != currentItem)
                {
                    previousItem = currentItem;
                    toolTip.Show(currentItem.Text, this, e.Location.X + 25, e.Location.Y);
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            Graphics g = e.Graphics;
            ListBoxItem drawItem = null;
            if (Items != null && Items.Count > 0 && e.Index != -1)
            {
                if (Items[e.Index] is ListBoxItem)
                {
                    drawItem = Items[e.Index] as ListBoxItem;
                }
            }
            if (drawItem != null)
            {
                if (e.Index % 2 == 0)
                {
                    g.FillRectangle(lightBrush, e.Bounds);
                    if ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight || (e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        g.FillRectangle(hightBrush, e.Bounds);

                    }
                }
                else
                {
                    g.FillRectangle(SystemBrushes.Window, e.Bounds);
                    if ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight || (e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        g.FillRectangle(hightBrush, e.Bounds);
                    }
                }
                g.DrawRectangle(normalPen, 8, e.Bounds.Y + 2, 24, 24);
                using (SolidBrush s = new SolidBrush(drawItem.Color))
                {
                    g.FillRectangle(s, 9, e.Bounds.Y + 3, 23, 23);
                }

                int stringX = (int)(drawItem.Text.Length * SystemFonts.DialogFont.SizeInPoints) + e.Bounds.X + 50;
                if (stringX > Size.Width)
                {
                    StringBuilder text = new StringBuilder((string)drawItem.Text.Clone());
                    stringX = (int)((stringX - Size.Width) / SystemFonts.DialogFont.SizeInPoints);
                    text.Remove(stringX, drawItem.Text.Length - stringX);
                    for (int i = 0; i < stringX; i++) text.Append(".");
                    g.DrawString(text.ToString(), SystemFonts.DialogFont, SystemBrushes.WindowText, e.Bounds.X + 50, e.Bounds.Y + 8);
                }
                else
                {
                    g.DrawString(drawItem.Text, SystemFonts.DialogFont, SystemBrushes.WindowText, e.Bounds.X + 50, e.Bounds.Y + 8);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (normalPen != null) normalPen.Dispose();
            if (hightBrush != null) hightBrush.Dispose();
            if (lightBrush != null) lightBrush.Dispose();
            base.Dispose(disposing);
        }
    }

    public class ListBoxItem
    {
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public ListBoxItem(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }
    }
}
