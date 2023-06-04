using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            webView.Source = new Uri("http://localhost:5173/dict"); // dev
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
        }
    }
}
