using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MeCab;
using System.Text;
using System.Reflection.Metadata;
using OpenAI_API.Chat;

namespace ja_learner
{
    public partial class MainForm : Form
    {
        DictForm dictForm;

        TextAnalyzer textAnalyzer = new TextAnalyzer();
        GptCaller gptCaller;
        private string sentence = "";

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
                    TranslateSentence();
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
            UserConfig.ReadConfigFile();
            GptCaller.Initialize();
#if DEBUG
            webView.Source = new Uri("http://localhost:5173/"); // dev
#else
            // 初始化 HTTP 服务器
            HttpServer.StartServer();
            webView.Source = new Uri($"http://localhost:{HttpServer.Port}/"); // build
#endif

            dictForm = new DictForm(this);

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
            dictForm.SearchText(message);
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
            if (timerWindowAlign.Enabled)
            {
                heightAfter = this.Height;
                dictForm.Width = this.Right - WindowAttacher.TargetWindowRect.Right;
            }
        }


        private System.Drawing.Point locationBefore; // 记录普通模式下窗口的位置
        private Size sizeBefore; // 记录普通模式下窗口的大小
        private int heightAfter = 200; // 附着模式时，窗体通常会比较矮

        private void checkBoxAlignWindow_CheckedChanged(object sender, EventArgs e)
        {
            WindowAttacher.TargetHwnd = IntPtr.Parse(textBoxHwnd.Text);
        
            if (checkBoxAlignWindow.Checked)
            {
                // 记录普通状态的窗口位置，切换到吸附状态下的窗口位置
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

        private void timerWindowAlign_Tick(object sender, EventArgs e)
        {
            try
            {
                WindowAttacher.AttachWindows(this, dictForm);
            }
            catch (Exception ex)
            {
                WindowAlign = false;
            }
        }

        private bool windowAlign = false;

        public bool WindowAlign
        {
            get
            {
                return windowAlign;
            }
            set
            {
                windowAlign = value;
                checkBoxAlignWindow.Checked = windowAlign;
                timerWindowAlign.Enabled = windowAlign;
            }
        }

        private void checkBoxClipboardMode_CheckedChanged(object sender, EventArgs e)
        {
            ClipBoardMode = checkBoxClipboardMode.Checked;
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
            Sentence = Microsoft.VisualBasic.Interaction.InputBox("手动输入", "输入句子", "", 0, 0);
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
            WindowAlign = false;
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
                dictForm.Show();
                dictForm.WindowState = FormWindowState.Normal; // 从最小化状态到普通状态
                dictForm.BringToFront();
                dictForm.Activate();
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
                checkBoxAlignWindow.Text = $"与【{windowTitle}】对齐";
                // 判断窗口句柄是不是自己的
                if (hwnd == this.Handle || hwnd == dictForm.Handle)
                {
                    checkBoxAlignWindow.Enabled = false;
                    return;
                }
                checkBoxAlignWindow.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private Task TranslateSentence()
        {
            var chat = GptCaller.CreateTranslateConversation(sentence);
            ClearTranslationText();
            GptCaller.StreamResponse(chat, res => AppendTranslationText(res));
        }

        async private void buttonTranslate_Click(object sender, EventArgs e)
        {
            await TranslateSentence();
        }

        private void checkBoxTopmost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBoxTopmost.Checked;
        }
    }
}