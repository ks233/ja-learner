﻿using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ja_learner
{
    public partial class DictForm : Form
    {
        public Form mainForm;
        public DictForm()
        {
            InitializeComponent();
        }

        public DictForm(Form _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }
        private async Task InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webView.CoreWebView2.Profile.PreferredColorScheme = CoreWebView2PreferredColorScheme.Light;
            // 处理打开新窗口（用默认浏览器打开）
            webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }
        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // 防止WebView2打开新窗口

            // 使用默认浏览器打开链接
            Process.Start(new ProcessStartInfo(e.Uri) { UseShellExecute = true });
        }

        public async void SearchText(string text)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPageDict"];
            string result = await webView.ExecuteScriptAsync($"searchText('{text}')");
        }

        private void DictForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 隐藏而不是关闭
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        public void ShowAndFocus()
        {
            Show();
            WindowState = FormWindowState.Normal; // 从最小化状态到普通状态
            BringToFront();
            Activate();
        }

        private async void DictForm_Load(object sender, EventArgs e)
        {
            await InitializeWebView();
#if DEBUG
            webView.Source = new Uri("http://localhost:5173/dict"); // dev
#else
            webView.Source = new Uri($"http://localhost:{HttpServer.Port}/dict"); // build
#endif
        }

        async public void UpdateTranslationPanelText(string text)
        {
            await webView.ExecuteScriptAsync($"setCurrentText('{text}')");
            translationPanel.UpdateText(text);
        }

        async private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();
            if (message == "ANKI_INIT")
            {
                if (Program.APP_SETTING.AnkiEnabled)
                {
                    await webView.ExecuteScriptAsync($"setAnkiEnabled(true)");
                    AnkiOptions o = Program.APP_SETTING.Anki;
                    await webView.ExecuteScriptAsync($"setAnkiConfig('{o.Deck}','{o.Model}','{o.FieldNames.Word}','{o.FieldNames.Example}','{o.FieldNames.Explain}')");
                }
                else
                {
                    await webView.ExecuteScriptAsync($"setAnkiEnabled(false)");
                }
                return;
            }
        }
    }
}
