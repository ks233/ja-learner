using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class WindowAttacher
    {
        // 导入 Windows API 函数
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        // 根据鼠标位置获取目标窗口句柄hwnd
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point point);

        // 根据hwnd获取窗口标题
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern IntPtr GetParent(IntPtr hWnd);

        // 定义 RECT 结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct Rectangle
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // 定义 POINT 结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;
        }



        public static String GetWindowTitle(IntPtr handle)
        {
            int length = GetWindowTextLength(handle);
            StringBuilder text = new StringBuilder(length + 1);
            GetWindowText(handle, text, text.Capacity);
            return text.ToString();
        }

        public static IntPtr GetHwndByCursorPoint()
        {
            Point cursorPos = new Point();
            cursorPos.X = Cursor.Position.X;
            cursorPos.Y = Cursor.Position.Y;
            // 当前鼠标位置所在窗口的最高父窗口的hwnd
            IntPtr hwnd = WindowFromPoint(cursorPos);
            IntPtr parentHwnd;
            while (true)
            {
                parentHwnd = GetParent(hwnd);
                if (parentHwnd == IntPtr.Zero) break;
                hwnd = parentHwnd;
            }
            return hwnd;
        }

        public static bool MouseDown(MouseButtons button)
        {
            return (Control.MouseButtons & button) == button;
        }

        public static void AttachWindows(Form form, Form dictForm, IntPtr hwnd)
        {
            // 调用 GetWindowRect 函数获取窗口位置和大小
            Rectangle rect;
            if (GetWindowRect(hwnd, out rect))
            {
                form.Top = rect.Bottom;
                form.Left = rect.Left;
                form.Width = rect.Right - rect.Left; // 对齐宽度

                dictForm.Top = rect.Top;
                dictForm.Left = rect.Right;
                dictForm.Height = form.Bottom - rect.Top;
            }
        }
    }
}
