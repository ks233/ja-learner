using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace ja_learner
{
    public partial class MainForm : Form
    {
        DictForm dictForm;

        TextAnalyzer textAnalyzer = new TextAnalyzer();
        private string sentence = "";
        private bool immersiveMode = false;
        public bool ImmersiveMode
        {
            get { return immersiveMode; }
            set
            {
                if (value)
                {
                    webView.Parent = this;
                    tabControl.Hide();
                    panel1.Hide();
                    FormBorderStyle = FormBorderStyle.None;
                }
                else
                {
                    webView.Parent = tabControl.TabPages[0];
                    tabControl.Show();
                    panel1.Show();
                    FormBorderStyle = FormBorderStyle.Sizable;
                }
                immersiveMode = value;
            }
        }
        public string Sentence
        {
            get { return sentence; }
            set
            {
                sentence = value;
                dictForm.UpdateTranslationPanelText(sentence);
                UpdateMecabResult(RunMecab());
                if (checkBoxAutoTranslate.Checked)
                {
                    _ = TranslateSentence();
                }

            }
        }

        public MainForm()
        {
            InitializeComponent();
        }
        private async Task InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webView.CoreWebView2.Profile.PreferredColorScheme = CoreWebView2PreferredColorScheme.Light;
            // 处理打开新窗口（用默认浏览器打开）
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            // 接收js消息（查词典）
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化 webview
            await InitializeWebView();
            GptCaller.Initialize();
#if DEBUG
            webView.Source = new Uri("http://localhost:5173/"); // dev
            Text += " - Debug -";
#else
            // 初始化 HTTP 服务器
            HttpServer.StartServer();
            webView.Source = new Uri($"http://localhost:{HttpServer.Port}/"); // build
            Text += $" - {HttpServer.Port} -";
#endif

            // 初始化 DictForm
            dictForm = new DictForm(this);
            dictForm.Show();
            dictForm.Hide();
            UpdateExtraPromptCombobox();

            // 初始化 MainForm
            if (Program.APP_SETTING.HttpProxy != string.Empty)
            {
                checkBoxUseProxy.Enabled = true;
                checkBoxUseProxy.Text = $"HTTP代理：{Program.APP_SETTING.HttpProxy}";
            }
            comboBoxTranslator.SelectedIndex = 0;
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // 防止WebView2打开新窗口

            // 使用默认浏览器打开链接
            Process.Start(new ProcessStartInfo(e.Uri) { UseShellExecute = true });
        }
        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();
            // 来自webview的特殊按键事件处理
            if (message == "DBLCLICK")
            {
                ImmersiveMode = !ImmersiveMode;
                return;
            }
            else if (message == "MOUSEDOWN")
            {
                // if (ImmersiveMode)
                {
                    Drag();
                }
                return;
            }
            dictForm.SearchText(message);
            dictForm.ShowAndFocus();
            Focus();
        }

        private string RunMecab()
        {
            return textAnalyzer.AnalyzeResultToJson(textAnalyzer.Analyze(sentence));
        }

        private async void UpdateMecabResult(string json)
        {
            await webView.ExecuteScriptAsync($"updateData({json})");
        }

        private async void AppendTranslationText(string text)
        {
            await webView.ExecuteScriptAsync($"appendTranslationText('{text}')");
        }
        private async void ClearTranslationText()
        {
            await webView.ExecuteScriptAsync($"clearTranslationText()");
        }


        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (timerWindowAttach.Enabled)
            {
                heightAfter = Height;
                if (dictForm.Visible)
                {
                    dictForm.Width = Right - WindowAttacher.TargetWindowRect.Right;
                }
            }
        }


        private Point locationBefore; // 记录普通模式下窗口的位置
        private Size sizeBefore; // 记录普通模式下窗口的大小
        private int heightAfter = 200; // 附着模式时，窗体通常会比较矮

        private void checkBoxWindowAttach_CheckedChanged(object sender, EventArgs e)
        {
            WindowAttacher.TargetHwnd = IntPtr.Parse(textBoxHwnd.Text);

            if (checkBoxWindowAttach.Checked)
            {
                // 记录普通状态的窗口位置，切换到吸附状态下的窗口位置
                sizeBefore = Size;
                locationBefore = Location;
                Height = heightAfter;
                timerWindowAttach.Enabled = true;
            }
            else
            {
                timerWindowAttach.Enabled = false;
                heightAfter = Height;
                Size = sizeBefore;
                Location = locationBefore;
            }
        }

        private void timerWindowAttach_Tick(object sender, EventArgs e)
        {
            try
            {
                WindowAttacher.AttachWindows(this, dictForm);
            }
            catch
            {
                WindowAttach = false;
            }
        }

        private bool windowAttach = false;

        public bool WindowAttach
        {
            get
            {
                return windowAttach;
            }
            set
            {
                windowAttach = value;
                checkBoxWindowAttach.Checked = windowAttach;
                timerWindowAttach.Enabled = windowAttach;
            }
        }

        private void checkBoxClipboardMode_CheckedChanged(object sender, EventArgs e)
        {
            ClipBoardMode = checkBoxClipboardMode.Checked;
            if (ClipBoardMode)
            {
                Sentence = Clipboard.GetText(TextDataFormat.UnicodeText).Trim().Replace("　", "");
            }
        }
        private void timerGetClipboard_Tick(object sender, EventArgs e)
        {
            string newSentence = Clipboard.GetText(TextDataFormat.UnicodeText).Trim().Replace("　", "");
            if (newSentence != "" && newSentence != Sentence)
            {
                Sentence = newSentence;
            }
        }
        public bool ClipBoardMode
        {
            get
            {
                return checkBoxClipboardMode.Checked;
            }
            set
            {
                checkBoxClipboardMode.Checked = value;
                timerGetClipboard.Enabled = value;
                btnInputText.Enabled = !value;
            }
        }

        private void btnInputText_Click(object sender, EventArgs e)
        {
            Sentence = Interaction.InputBox("手动输入", "输入句子", "", 0, 0);
        }

        private void timerSelectWindow_Tick(object sender, EventArgs e)
        {
            IntPtr hwnd = WindowAttacher.GetHwndByCursorPoint();
            textBoxHwnd.Text = hwnd.ToString();

            // 判断鼠标是否按下
            bool mouseDown = WindowAttacher.MouseDown(MouseButtons.Left);

            if (mouseDown)
            {
                timerSelectWindow.Enabled = false;
            }
        }

        private void btnSelectWindow_Click(object sender, EventArgs e)
        {
            WindowAttach = false;
            timerSelectWindow.Enabled = true;
        }

        private void checkBoxDark_CheckedChanged(object sender, EventArgs e)
        {
            webView.CoreWebView2.Profile.PreferredColorScheme = checkBoxDark.Checked ? CoreWebView2PreferredColorScheme.Dark : CoreWebView2PreferredColorScheme.Light;
        }

        private void buttonShowDictForm_Click(object sender, EventArgs e)
        {
            if (!dictForm.Visible)
            {
                dictForm.ShowAndFocus();
            }
            else
            {
                dictForm.Hide();
            }
        }


        private void textBoxHwnd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IntPtr hwnd = IntPtr.Parse(textBoxHwnd.Text);
                string windowTitle = WindowAttacher.GetWindowTitle(hwnd);
                checkBoxWindowAttach.Text = $"与【{windowTitle}】对齐";
                // 判断窗口句柄是不是自己的
                if (hwnd == Handle || hwnd == dictForm.Handle)
                {
                    checkBoxWindowAttach.Enabled = false;
                    return;
                }
                checkBoxWindowAttach.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private Task TranslateSentence()
        {
            if (comboBoxTranslator.Text == "ChatGPT")
            {
                var chat = GptCaller.CreateTranslateConversation(sentence);
                ClearTranslationText();
                GptCaller.StreamResponse(chat, res => AppendTranslationText(res));
            }
            else if (comboBoxTranslator.Text == "谷歌生草机")
            {
                // 这个接口效果真的好烂。。。用来翻译片假名还可以，翻译句子真就生草机
                await webView.ExecuteScriptAsync($"runGoogleTrans(\"{sentence.Replace("\r\n", "")}\")");
            }
            else if (comboBoxTranslator.Text == "谷歌翻译")
            {
                // 效果与网页版一致的API，不知道能用多久
                await webView.ExecuteScriptAsync($"runGoogleTransTk(\"{sentence.Replace("\r\n", "")}\")");
            }
        }

        async private void buttonTranslate_Click(object sender, EventArgs e)
        {
            await TranslateSentence();
        }

        private void checkBoxTopmost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBoxTopmost.Checked;
        }

        private void buttonUpdateExtraPrompt_Click(object sender, EventArgs e)
        {
            UserConfig.UpdateExtraPrompt();
            MessageBox.Show($"已重新读取文件{UserConfig.ExtraPromptFilename}:\n" + UserConfig.ExtraPrompt);
        }

        private void checkBoxUseExtraPrompt_CheckedChanged(object sender, EventArgs e)
        {
            UserConfig.useExtraPrompt = checkBoxUseExtraPrompt.Checked;
        }

        private void comboBoxExtraPrompts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxExtraPrompts.SelectedIndex != -1)
            {
                checkBoxUseExtraPrompt.Enabled = true;
            }
            UserConfig.ExtraPromptFilename = comboBoxExtraPrompts.Text;
        }

        private void UpdateExtraPromptCombobox()
        {
            string text = comboBoxExtraPrompts.Text;
            comboBoxExtraPrompts.Items.Clear();
            string[] extra_prompt_files = UserConfig.GetExtraPromptFiles();
            foreach (string filename in extra_prompt_files)
            {
                comboBoxExtraPrompts.Items.Add(filename);

                if (filename == text)
                {
                    comboBoxExtraPrompts.SelectedIndex = comboBoxExtraPrompts.Items.Count - 1;
                }
            }
            if (comboBoxExtraPrompts.SelectedIndex == -1)
            {
                checkBoxUseExtraPrompt.Enabled = false;
            }
        }

        private void comboBoxExtraPrompts_Click(object sender, EventArgs e)
        {
            UpdateExtraPromptCombobox();
        }

        async private void checkBoxTranslateKatakana_CheckedChanged(object sender, EventArgs e)
        {
            string param = checkBoxTranslateKatakana.Checked ? "true" : "false";
            await webView.ExecuteScriptAsync($"setTranslateKatakana({param})");
        }

        // https://stackoverflow.com/questions/31199437/borderless-and-resizable-form-c
        // 从↑抄来的代码，无边框模式下可调整窗口大小
        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 1;
        const int HTCAPTION = 2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if (m.Result == (IntPtr)HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }
                    break;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x40000;
                return cp;
            }
        }

        // 窗口拖拽
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Drag()
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void checkBoxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            UserConfig.UseProxy = checkBoxUseProxy.Checked;
            GptCaller.SetProxy(UserConfig.UseProxy);
        }
    }
}