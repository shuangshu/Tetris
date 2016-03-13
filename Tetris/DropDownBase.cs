using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    public class DropDownBase : Control
    {
        public const int CS_DROPSHADOW = 0x00020000;
        public const int CS_SAVEBITS = 0x0800;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_EX_APPWINDOW = 0x00040000;
        public const int WS_EX_CONTROLPARENT = 0x00010000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_POPUP = unchecked((int)0x80000000);
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WM_KEYFIRST = 0x0100;
        public const int WM_KEYLAST = 0x0108;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_ACTIVATE = 0x0006;
        public const int WA_ACTIVE = 1;
        public const int WA_INACTIVE = 0;
        public const int RDW_FRAME = 0x0400;
        public const int RDW_INVALIDATE = 0x0001;
        public const int SW_HIDE = 0;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_MAX = 10;
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_DRAWFRAME = 0x0020;
        public const int SWP_NOOWNERZORDER = 0x0200;
        public static IntPtr Invalid = (IntPtr)(-1);
        public static IntPtr HWND_TOP = (IntPtr)0;
        public static IntPtr HWND_BOTTOM = (IntPtr)1;
        public static IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        private Control active;
        public Control ActiveControl
        {
            get { return active; }
            set { active = value; }
        }

        private Control owner = null;
        public Control Owner
        {
            get { return owner; }
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW | CS_SAVEBITS;
                cp.Style &= ~(WS_CAPTION | WS_CLIPSIBLINGS);
                cp.ExStyle &= ~(WS_EX_APPWINDOW);
                cp.Style |= WS_POPUP;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg >= WM_KEYFIRST && m.Msg <= WM_KEYLAST)
            {
                DefWndProc(ref m);
                return;
            }
            switch (m.Msg)
            {
                case WM_NCACTIVATE:
                    WmNCActivate(ref m);
                    return;
                case WM_ACTIVATE:
                    {
                        if ((int)m.WParam == WA_ACTIVE) ShowWindow(Handle, SW_SHOW);
                        else if ((int)m.WParam == WA_INACTIVE) ShowWindow(Handle, SW_HIDE);
                        base.WndProc(ref m);
                    }
                    return;
                default:
                    base.WndProc(ref m);
                    return;
            }
        }
        private bool sendingActivateMessage = false;
        private void WmNCActivate(ref Message m)
        {
            if (m.WParam != IntPtr.Zero)
            {

                if (!sendingActivateMessage)
                {
                    sendingActivateMessage = true;
                    try
                    {
                        if (active != null && active.Handle != IntPtr.Zero)
                        {
                            SendMessage(active.Handle, WM_NCACTIVATE, (IntPtr)1, Invalid);
                            RedrawWindow(active.Handle, null, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE);
                        }
                        m.WParam = (IntPtr)WA_ACTIVE;
                    }
                    finally
                    {
                        sendingActivateMessage = false;
                    }
                }
                DefWndProc(ref m);
                return;

            }
            else
            {
                base.WndProc(ref m);
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool RedrawWindow(IntPtr hwnd, ref RECT rcUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool RedrawWindow(IntPtr hwnd, COMRECT rcUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public virtual void Popup(Control control, Point position)
        {
            owner = control;
            Point p = control.PointToScreen(position);
            SetWindowPos(Handle, HWND_TOP, p.X, p.Y, Size.Width, Size.Height, SWP_SHOWWINDOW);
        }
        public virtual void Popup(Control control, Point position, Size size)
        {
            owner = control;
            Point p = control.PointToScreen(position);
            SetWindowPos(Handle, HWND_TOP, p.X, p.Y, size.Width, size.Height, SWP_SHOWWINDOW);
        }

        public void Popdown()
        {
            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_HIDEWINDOW);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public System.Drawing.Size Size
            {
                get
                {
                    return new System.Drawing.Size(this.right - this.left, this.bottom - this.top);
                }
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public COMRECT()
            {
            }

            public COMRECT(System.Drawing.Rectangle r)
            {
                this.left = r.X;
                this.top = r.Y;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }


            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }
    }
}
