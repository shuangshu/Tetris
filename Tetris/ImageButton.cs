using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace Tetris
{
    public class ImageButton : Control
    {
        private Size imageMargin;
        public Size ImageMargin
        {
            get { return imageMargin; }
            set
            {
                imageMargin = value;
                Invalidate();
            }
        }

        private Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; Invalidate(); }
        }

        private DialogResult dialogResult;
        public virtual DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }

            set
            {
                dialogResult = value;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            ButtonRenderer.DrawButton(g, pevent.ClipRectangle, PushButtonState.Normal);
            if (image != null) g.DrawImage(image, imageMargin.Width, imageMargin.Height, 24, 24);
        }

        protected override void OnClick(EventArgs e)
        {
            Form window = FindForm();
            if (window != null) window.DialogResult = dialogResult;
            base.OnClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            using (Graphics g = CreateGraphics())
            {
                ButtonRenderer.DrawButton(g, ClientRectangle, PushButtonState.Pressed);
                if (image != null) g.DrawImage(image, imageMargin.Width + 2, imageMargin.Height + 2, 24, 24);
            }
        }
    }
}
