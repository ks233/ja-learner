namespace ja_learner
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerWindowAlign = new System.Windows.Forms.Timer(components);
            timerSelectWindow = new System.Windows.Forms.Timer(components);
            tabPageSettings = new TabPage();
            textBoxHwnd = new TextBox();
            btnSelectWindow = new Button();
            checkBoxAlignWindow = new CheckBox();
            tabPageText = new TabPage();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabControl = new TabControl();
            timerGetClipboard = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            buttonTranslate = new Button();
            buttonShowDictForm = new Button();
            checkBoxDark = new CheckBox();
            btnInputText = new Button();
            checkBoxClipboardMode = new CheckBox();
            checkBoxAutoTranslate = new CheckBox();
            tabPageSettings.SuspendLayout();
            tabPageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            tabControl.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // timerWindowAlign
            // 
            timerWindowAlign.Interval = 15;
            timerWindowAlign.Tick += timerWindowAlign_Tick;
            // 
            // timerSelectWindow
            // 
            timerSelectWindow.Tick += timerSelectWindow_Tick;
            // 
            // tabPageSettings
            // 
            tabPageSettings.Controls.Add(textBoxHwnd);
            tabPageSettings.Controls.Add(btnSelectWindow);
            tabPageSettings.Controls.Add(checkBoxAlignWindow);
            tabPageSettings.Location = new Point(4, 26);
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.Padding = new Padding(3);
            tabPageSettings.Size = new Size(574, 310);
            tabPageSettings.TabIndex = 0;
            tabPageSettings.Text = "系统设置";
            tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // textBoxHwnd
            // 
            textBoxHwnd.Location = new Point(89, 15);
            textBoxHwnd.Name = "textBoxHwnd";
            textBoxHwnd.ReadOnly = true;
            textBoxHwnd.Size = new Size(100, 23);
            textBoxHwnd.TabIndex = 1;
            textBoxHwnd.Text = "114514";
            textBoxHwnd.TextChanged += textBoxHwnd_TextChanged;
            // 
            // btnSelectWindow
            // 
            btnSelectWindow.Location = new Point(8, 15);
            btnSelectWindow.Name = "btnSelectWindow";
            btnSelectWindow.Size = new Size(75, 23);
            btnSelectWindow.TabIndex = 4;
            btnSelectWindow.Text = "选择窗口";
            btnSelectWindow.UseVisualStyleBackColor = true;
            btnSelectWindow.Click += btnSelectWindow_Click;
            // 
            // checkBoxAlignWindow
            // 
            checkBoxAlignWindow.AutoSize = true;
            checkBoxAlignWindow.Enabled = false;
            checkBoxAlignWindow.Location = new Point(195, 17);
            checkBoxAlignWindow.Name = "checkBoxAlignWindow";
            checkBoxAlignWindow.Size = new Size(87, 21);
            checkBoxAlignWindow.TabIndex = 3;
            checkBoxAlignWindow.Text = "与窗口对齐";
            checkBoxAlignWindow.UseVisualStyleBackColor = true;
            checkBoxAlignWindow.CheckedChanged += checkBoxAlignWindow_CheckedChanged;
            // 
            // tabPageText
            // 
            tabPageText.Controls.Add(webView);
            tabPageText.Location = new Point(4, 26);
            tabPageText.Name = "tabPageText";
            tabPageText.Padding = new Padding(3);
            tabPageText.Size = new Size(574, 310);
            tabPageText.TabIndex = 1;
            tabPageText.Text = "分词断句";
            tabPageText.UseVisualStyleBackColor = true;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(3, 3);
            webView.Name = "webView";
            webView.Size = new Size(568, 304);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabPageText);
            tabControl.Controls.Add(tabPageSettings);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(582, 340);
            tabControl.TabIndex = 2;
            // 
            // timerGetClipboard
            // 
            timerGetClipboard.Interval = 15;
            timerGetClipboard.Tick += timerGetClipboard_Tick;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxAutoTranslate);
            panel1.Controls.Add(buttonTranslate);
            panel1.Controls.Add(buttonShowDictForm);
            panel1.Controls.Add(checkBoxDark);
            panel1.Controls.Add(btnInputText);
            panel1.Controls.Add(checkBoxClipboardMode);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 340);
            panel1.Name = "panel1";
            panel1.Size = new Size(582, 32);
            panel1.TabIndex = 3;
            // 
            // buttonTranslate
            // 
            buttonTranslate.Location = new Point(248, 4);
            buttonTranslate.Name = "buttonTranslate";
            buttonTranslate.Size = new Size(93, 23);
            buttonTranslate.TabIndex = 8;
            buttonTranslate.Text = "GPT参考翻译";
            buttonTranslate.UseVisualStyleBackColor = true;
            buttonTranslate.Click += buttonTranslate_Click;
            // 
            // buttonShowDictForm
            // 
            buttonShowDictForm.Location = new Point(170, 4);
            buttonShowDictForm.Name = "buttonShowDictForm";
            buttonShowDictForm.Size = new Size(72, 23);
            buttonShowDictForm.TabIndex = 7;
            buttonShowDictForm.Text = "词典窗口";
            buttonShowDictForm.UseVisualStyleBackColor = true;
            buttonShowDictForm.Click += buttonShowDictForm_Click;
            // 
            // checkBoxDark
            // 
            checkBoxDark.Anchor = AnchorStyles.Right;
            checkBoxDark.AutoSize = true;
            checkBoxDark.Location = new Point(495, 6);
            checkBoxDark.Name = "checkBoxDark";
            checkBoxDark.Size = new Size(75, 21);
            checkBoxDark.TabIndex = 6;
            checkBoxDark.Text = "深色模式";
            checkBoxDark.UseVisualStyleBackColor = true;
            checkBoxDark.Click += checkBoxDark_CheckedChanged;
            // 
            // btnInputText
            // 
            btnInputText.Location = new Point(96, 4);
            btnInputText.Name = "btnInputText";
            btnInputText.Size = new Size(68, 23);
            btnInputText.TabIndex = 1;
            btnInputText.Text = "手动输入";
            btnInputText.UseVisualStyleBackColor = true;
            btnInputText.Click += btnInputText_Click;
            // 
            // checkBoxClipboardMode
            // 
            checkBoxClipboardMode.AutoSize = true;
            checkBoxClipboardMode.Location = new Point(3, 6);
            checkBoxClipboardMode.Name = "checkBoxClipboardMode";
            checkBoxClipboardMode.Size = new Size(87, 21);
            checkBoxClipboardMode.TabIndex = 0;
            checkBoxClipboardMode.Text = "读取剪贴板";
            checkBoxClipboardMode.UseVisualStyleBackColor = true;
            checkBoxClipboardMode.CheckedChanged += checkBoxClipboardMode_CheckedChanged;
            // 
            // checkBoxAutoTranslate
            // 
            checkBoxAutoTranslate.Anchor = AnchorStyles.Right;
            checkBoxAutoTranslate.AutoSize = true;
            checkBoxAutoTranslate.Location = new Point(414, 6);
            checkBoxAutoTranslate.Name = "checkBoxAutoTranslate";
            checkBoxAutoTranslate.Size = new Size(75, 21);
            checkBoxAutoTranslate.TabIndex = 9;
            checkBoxAutoTranslate.Text = "自动翻译";
            checkBoxAutoTranslate.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 372);
            Controls.Add(panel1);
            Controls.Add(tabControl);
            Name = "MainForm";
            Text = "KS的日语学习工具";
            Load += Form1_Load;
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            tabPageText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            tabControl.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerWindowAlign;
        private System.Windows.Forms.Timer timerSelectWindow;
        private TabPage tabPageSettings;
        private TabPage tabPageText;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private TabControl tabControl;
        private System.Windows.Forms.Timer timerGetClipboard;
        private TextBox textBoxHwnd;
        private Button btnSelectWindow;
        private CheckBox checkBoxAlignWindow;
        private Panel panel1;
        private Button buttonShowDictForm;
        private CheckBox checkBoxDark;
        private Button btnInputText;
        private CheckBox checkBoxClipboardMode;
        private GUI.TranslationPanel translationPanel;
        private Button buttonTranslate;
        private CheckBox checkBoxAutoTranslate;
    }
}