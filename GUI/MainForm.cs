using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MeCab;
using System.Text;
using System.Reflection.Metadata;
using OpenAI_API.Chat;
using Microsoft.VisualBasic;
using System.Windows.Forms.Design;
using System.Net.Http;

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
            // ������´��ڣ���Ĭ��������򿪣�
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            // ����js��Ϣ����ʵ䣩
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // ��ʼ�� webview
            await InitializeWebView();
            GptCaller.Initialize();
#if DEBUG
            webView.Source = new Uri("http://localhost:5173/"); // dev
            Text += " - Debug -";
#else
            // ��ʼ�� HTTP ������
            HttpServer.StartServer();
            webView.Source = new Uri($"http://localhost:{HttpServer.Port}/"); // build
            Text += $" - ��ռ�ö˿ڣ�{HttpServer.Port} -";
#endif

            dictForm = new DictForm(this);
            dictForm.Show();
            dictForm.Hide();
            UpdateExtraPromptCombobox();
            comboBoxTranslator.SelectedIndex = 0;
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // ��ֹWebView2���´���

            // ʹ��Ĭ�������������
            Process.Start(new ProcessStartInfo(e.Uri) { UseShellExecute = true });
        }
        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();
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
                heightAfter = this.Height;
                if (dictForm.Visible)
                {
                    dictForm.Width = this.Right - WindowAttacher.TargetWindowRect.Right;
                }
            }
        }


        private System.Drawing.Point locationBefore; // ��¼��ͨģʽ�´��ڵ�λ��
        private Size sizeBefore; // ��¼��ͨģʽ�´��ڵĴ�С
        private int heightAfter = 200; // ����ģʽʱ������ͨ����Ƚϰ�

        private void checkBoxWindowAttach_CheckedChanged(object sender, EventArgs e)
        {
            WindowAttacher.TargetHwnd = IntPtr.Parse(textBoxHwnd.Text);

            if (checkBoxWindowAttach.Checked)
            {
                // ��¼��ͨ״̬�Ĵ���λ�ã��л�������״̬�µĴ���λ��
                sizeBefore = this.Size;
                locationBefore = this.Location;
                this.Height = heightAfter;
                timerWindowAttach.Enabled = true;
            }
            else
            {
                timerWindowAttach.Enabled = false;
                heightAfter = this.Height;
                this.Size = sizeBefore;
                this.Location = locationBefore;
            }
        }

        private void timerWindowAttach_Tick(object sender, EventArgs e)
        {
            try
            {
                WindowAttacher.AttachWindows(this, dictForm);
            }
            catch (Exception ex)
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
        }
        private void timerGetClipboard_Tick(object sender, EventArgs e)
        {
            string newSentence = Clipboard.GetText(TextDataFormat.UnicodeText).Trim().Replace("��", "");
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
            Sentence = Microsoft.VisualBasic.Interaction.InputBox("�ֶ�����", "�������", "", 0, 0);
        }

        private void timerSelectWindow_Tick(object sender, EventArgs e)
        {
            IntPtr hwnd = WindowAttacher.GetHwndByCursorPoint();
            textBoxHwnd.Text = hwnd.ToString();

            // �ж�����Ƿ���
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
                checkBoxWindowAttach.Text = $"�롾{windowTitle}������";
                // �жϴ��ھ���ǲ����Լ���
                if (hwnd == this.Handle || hwnd == dictForm.Handle)
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
            else if (comboBoxTranslator.Text == "�ȸ����ݻ�")
            {
                // ����ӿ�Ч����ĺ��á�������������Ƭ���������ԣ��������������ݻ�
                await webView.ExecuteScriptAsync($"runGoogleTrans(\"{sentence.Replace("\r\n", "")}\")");
            }
            else if (comboBoxTranslator.Text == "�ȸ跭��")
            {
                // Ч������ҳ��һ�µ�API����֪�����ö��
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
            MessageBox.Show($"�����¶�ȡ�ļ�{UserConfig.ExtraPromptFilename}:\n" + UserConfig.ExtraPrompt);
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
            await webView.ExecuteScriptAsync($"setTranslateKatakana({checkBoxTranslateKatakana.Checked.ToString().ToLower()})");
        }
    }
}