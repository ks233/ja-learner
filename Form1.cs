using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ja_learner
{
    public partial class Form1 : Form
    {

        // 导入 Windows API 函数
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

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



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e) {
            if(timerWindowAlign.Enabled)
            {
                heightAfter = this.Height;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private System.Drawing.Point locationBefore; // 记录普通模式下窗口的位置
        private Size sizeBefore; // 记录普通模式下窗口的大小
        private int heightAfter = 200; // 附着模式时，窗体通常会比较矮

        private void timerWindowAlign_Tick(object sender, EventArgs e)
        {
            int processId = int.Parse(textBox1.Text);
            try
            {
                // 获取进程对象
                Process process = Process.GetProcessById(processId);

                // 获取主窗口句柄
                IntPtr mainWindowHandle = process.MainWindowHandle;

                // 调用 GetWindowRect 函数获取窗口位置和大小
                Rectangle rect;
                if (GetWindowRect(mainWindowHandle, out rect))
                {
                    this.Top = rect.Bottom;
                    this.Left = rect.Left;
                    this.Width = rect.Right - rect.Left; // 对齐宽度
                }
            }
            finally
            {

            }

        }

        private void btnSelectWindow_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxClipboardMode_CheckedChanged(object sender, EventArgs e)
        {
            btnInputText.Enabled = !checkBoxClipboardMode.Checked;
        }

        private void checkBoxAlignWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlignWindow.Checked)
            {
                sizeBefore = this.Size;
                locationBefore = this.Location;
                this.Height = heightAfter;
                timerWindowAlign.Enabled = true;
            }
            else
            {
                timerWindowAlign.Enabled = false;
                heightAfter = this.Height;
                this.Size = sizeBefore;
                this.Location = locationBefore;
            }

        }
    }
}