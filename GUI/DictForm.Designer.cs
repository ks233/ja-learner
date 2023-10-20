namespace ja_learner
{
    partial class DictForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabControl1 = new TabControl();
            tabPageDict = new TabPage();
            tabPageGpt = new TabPage();
            translationPanel = new GUI.TranslationPanel();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            tabControl1.SuspendLayout();
            tabPageDict.SuspendLayout();
            tabPageGpt.SuspendLayout();
            SuspendLayout();
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(3, 3);
            webView.Name = "webView";
            webView.Size = new Size(410, 425);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageDict);
            tabControl1.Controls.Add(tabPageGpt);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(424, 461);
            tabControl1.TabIndex = 1;
            // 
            // tabPageDict
            // 
            tabPageDict.Controls.Add(webView);
            tabPageDict.Location = new Point(4, 26);
            tabPageDict.Name = "tabPageDict";
            tabPageDict.Padding = new Padding(3);
            tabPageDict.Size = new Size(416, 431);
            tabPageDict.TabIndex = 0;
            tabPageDict.Text = "词典";
            tabPageDict.UseVisualStyleBackColor = true;
            // 
            // tabPageGpt
            // 
            tabPageGpt.Controls.Add(translationPanel);
            tabPageGpt.Location = new Point(4, 26);
            tabPageGpt.Name = "tabPageGpt";
            tabPageGpt.Padding = new Padding(3);
            tabPageGpt.Size = new Size(416, 431);
            tabPageGpt.TabIndex = 1;
            tabPageGpt.Text = "内容解析";
            tabPageGpt.UseVisualStyleBackColor = true;
            // 
            // translationPanel
            // 
            translationPanel.Dock = DockStyle.Fill;
            translationPanel.Location = new Point(3, 3);
            translationPanel.Name = "translationPanel";
            translationPanel.Size = new Size(410, 425);
            translationPanel.TabIndex = 0;
            // 
            // DictForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 461);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            Name = "DictForm";
            Text = "KS的日语学习工具";
            FormClosing += DictForm_FormClosing;
            Load += DictForm_Load;
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            tabControl1.ResumeLayout(false);
            tabPageDict.ResumeLayout(false);
            tabPageGpt.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private TabControl tabControl1;
        private TabPage tabPageDict;
        private TabPage tabPageGpt;
        private GUI.TranslationPanel translationPanel;
    }
}