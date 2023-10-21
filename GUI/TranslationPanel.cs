using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ja_learner.GUI
{
    public partial class TranslationPanel : UserControl
    {
        public TranslationPanel()
        {
            InitializeComponent();
        }

        public void UpdateText(string text)
        {
            textBoxSentence.Text = text;
        }

        private void buttonInterpret_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "";
            buttonInterpret.Enabled = false;
            var chat = GptCaller.CreateInterpretConversation(textBoxSentence.Text);
            GptCaller.StreamResponse(chat, res =>
            {
                textBoxResult.Text += res.Replace("\n", "\r\n");
                textBoxResult.ScrollToCaret();
            });

            buttonInterpret.Enabled = true;
        }
    }
}
