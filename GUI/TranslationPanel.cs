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

        async private void buttonInterpret_Click(object sender, EventArgs e)
        {
            buttonInterpret.Enabled = false;
            GptCaller gptCaller = new GptCaller();
            Conversation chat = gptCaller.CreateInterpretConversation(textBoxSentence.Text);
            try
            {
                string response = "";
                textBoxResult.Text = "";
                await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                {
                    textBoxResult.Text += res.Replace("\n", "\r\n");
                    response += res;
                    textBoxResult.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonInterpret.Enabled = true;
        }
    }
}
