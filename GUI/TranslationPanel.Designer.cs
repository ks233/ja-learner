namespace ja_learner.GUI
{
    partial class TranslationPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            textBoxSentence = new TextBox();
            textBoxResult = new TextBox();
            buttonInterpret = new Button();
            SuspendLayout();
            // 
            // textBoxSentence
            // 
            textBoxSentence.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSentence.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxSentence.Location = new Point(3, 3);
            textBoxSentence.Multiline = true;
            textBoxSentence.Name = "textBoxSentence";
            textBoxSentence.Size = new Size(545, 76);
            textBoxSentence.TabIndex = 0;
            // 
            // textBoxResult
            // 
            textBoxResult.AcceptsReturn = true;
            textBoxResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxResult.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxResult.Location = new Point(3, 114);
            textBoxResult.Multiline = true;
            textBoxResult.Name = "textBoxResult";
            textBoxResult.ScrollBars = ScrollBars.Vertical;
            textBoxResult.Size = new Size(545, 153);
            textBoxResult.TabIndex = 2;
            // 
            // buttonInterpret
            // 
            buttonInterpret.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonInterpret.Location = new Point(3, 85);
            buttonInterpret.Name = "buttonInterpret";
            buttonInterpret.Size = new Size(545, 23);
            buttonInterpret.TabIndex = 3;
            buttonInterpret.Text = "分析";
            buttonInterpret.UseVisualStyleBackColor = true;
            buttonInterpret.Click += buttonInterpret_Click;
            // 
            // TranslationPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonInterpret);
            Controls.Add(textBoxResult);
            Controls.Add(textBoxSentence);
            Name = "TranslationPanel";
            Size = new Size(551, 270);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxSentence;
        private TextBox textBoxResult;
        private Button buttonInterpret;
    }
}
