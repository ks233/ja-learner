using Microsoft.Web.WebView2.Core;
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

        private async void DictForm_Load(object sender, EventArgs e)
        {
            await InitializeWebView();
#if DEBUG
            webView.Source = new Uri("http://localhost:5173/dict"); // dev
#else
            webView.Source = new Uri("http://localhost:8080/dict"); // build
#endif
        }
    }
}
