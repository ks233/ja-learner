namespace ja_learner
{
    partial class Form1
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
            panel1 = new Panel();
            checkBoxShowDictForm = new CheckBox();
            checkBoxDark = new CheckBox();
            btnInputText = new Button();
            checkBoxClipboardMode = new CheckBox();
            tabControl = new TabControl();
            timerGetClipboard = new System.Windows.Forms.Timer(components);
            tabPageSettings.SuspendLayout();
            tabPageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            panel1.SuspendLayout();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // timerWindowAlign
            // 
            timerWindowAlign.Interval = 20;
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
            tabPageSettings.Location = new System.Drawing.Point(4, 26);
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.Padding = new Padding(3);
            tabPageSettings.Size = new Size(553, 310);
            tabPageSettings.TabIndex = 0;
            tabPageSettings.Text = "系统设置";
            tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // textBoxHwnd
            // 
            textBoxHwnd.Location = new System.Drawing.Point(89, 15);
            textBoxHwnd.Name = "textBoxHwnd";
            textBoxHwnd.ReadOnly = true;
            textBoxHwnd.Size = new Size(100, 23);
            textBoxHwnd.TabIndex = 1;
            textBoxHwnd.Text = "114514";
            textBoxHwnd.TextChanged += textBoxHwnd_TextChanged;
            // 
            // btnSelectWindow
            // 
            btnSelectWindow.Location = new System.Drawing.Point(8, 15);
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
            checkBoxAlignWindow.Location = new System.Drawing.Point(195, 17);
            checkBoxAlignWindow.Name = "checkBoxAlignWindow";
            checkBoxAlignWindow.Size = new Size(99, 21);
            checkBoxAlignWindow.TabIndex = 3;
            checkBoxAlignWindow.Text = "与该程序对齐";
            checkBoxAlignWindow.UseVisualStyleBackColor = true;
            checkBoxAlignWindow.CheckedChanged += checkBoxAlignWindow_CheckedChanged;
            // 
            // tabPageText
            // 
            tabPageText.Controls.Add(webView);
            tabPageText.Controls.Add(panel1);
            tabPageText.Location = new System.Drawing.Point(4, 26);
            tabPageText.Name = "tabPageText";
            tabPageText.Padding = new Padding(3);
            tabPageText.Size = new Size(553, 310);
            tabPageText.TabIndex = 1;
            tabPageText.Text = "内容分析";
            tabPageText.UseVisualStyleBackColor = true;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new System.Drawing.Point(3, 3);
            webView.Name = "webView";
            webView.Size = new Size(547, 272);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxShowDictForm);
            panel1.Controls.Add(checkBoxDark);
            panel1.Controls.Add(btnInputText);
            panel1.Controls.Add(checkBoxClipboardMode);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(3, 275);
            panel1.Name = "panel1";
            panel1.Size = new Size(547, 32);
            panel1.TabIndex = 0;
            // 
            // checkBoxShowDictForm
            // 
            checkBoxShowDictForm.AutoSize = true;
            checkBoxShowDictForm.Location = new System.Drawing.Point(331, 6);
            checkBoxShowDictForm.Name = "checkBoxShowDictForm";
            checkBoxShowDictForm.Size = new Size(75, 21);
            checkBoxShowDictForm.TabIndex = 7;
            checkBoxShowDictForm.Text = "词典窗口";
            checkBoxShowDictForm.UseVisualStyleBackColor = true;
            checkBoxShowDictForm.CheckedChanged += checkBoxShowDictForm_CheckedChanged;
            // 
            // checkBoxDark
            // 
            checkBoxDark.AutoSize = true;
            checkBoxDark.Location = new System.Drawing.Point(250, 6);
            checkBoxDark.Name = "checkBoxDark";
            checkBoxDark.Size = new Size(75, 21);
            checkBoxDark.TabIndex = 6;
            checkBoxDark.Text = "深色模式";
            checkBoxDark.UseVisualStyleBackColor = true;
            checkBoxDark.CheckedChanged += checkBoxDark_CheckedChanged;
            // 
            // btnInputText
            // 
            btnInputText.Location = new System.Drawing.Point(96, 4);
            btnInputText.Name = "btnInputText";
            btnInputText.Size = new Size(115, 23);
            btnInputText.TabIndex = 1;
            btnInputText.Text = "输入要分析的句子";
            btnInputText.UseVisualStyleBackColor = true;
            btnInputText.Click += btnInputText_Click;
            // 
            // checkBoxClipboardMode
            // 
            checkBoxClipboardMode.AutoSize = true;
            checkBoxClipboardMode.Location = new System.Drawing.Point(3, 6);
            checkBoxClipboardMode.Name = "checkBoxClipboardMode";
            checkBoxClipboardMode.Size = new Size(87, 21);
            checkBoxClipboardMode.TabIndex = 0;
            checkBoxClipboardMode.Text = "剪贴板模式";
            checkBoxClipboardMode.UseVisualStyleBackColor = true;
            checkBoxClipboardMode.CheckedChanged += checkBoxClipboardMode_CheckedChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageText);
            tabControl.Controls.Add(tabPageSettings);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(561, 340);
            tabControl.TabIndex = 2;
            // 
            // timerGetClipboard
            // 
            timerGetClipboard.Tick += timerGetClipboard_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 340);
            Controls.Add(tabControl);
            Name = "Form1";
            Text = "KS的日语学习工具";
            Load += Form1_Load;
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            tabPageText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerWindowAlign;
        private System.Windows.Forms.Timer timerSelectWindow;
        private TabPage tabPageSettings;
        private TabPage tabPageText;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Panel panel1;
        private Button btnInputText;
        private CheckBox checkBoxClipboardMode;
        private TabControl tabControl;
        private System.Windows.Forms.Timer timerGetClipboard;
        private TextBox textBoxHwnd;
        private Button btnSelectWindow;
        private CheckBox checkBoxAlignWindow;
        private CheckBox checkBoxDark;
        private CheckBox checkBoxShowDictForm;
    }
}