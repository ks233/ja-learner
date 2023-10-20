using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class GptCaller
    {
        OpenAIAPI api;
        Conversation conversation;

        public GptCaller() {
            api = new OpenAIAPI(UserConfig.api_key);
            api.ApiUrlFormat = UserConfig.api_url;
        }

        public void CreateTranslateConversation(string text)
        {
            conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a translation engine, translate the text to Simplified Chinese. Don't output anything other than translation results.");
            conversation.AppendUserInput($"{text}");
        }

        public void CreateInterpretConversation(string text)
        {
            conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a Japanese teacher, List and explain the vocabulary (except prepositions) and grammar of the given text in Simplified Chinese. Your output consists of three parts: translation, vocabulary, grammar. Don't use English and romaji.");
            conversation.AppendUserInput($"{text}");
        }

        async public void StreamResponse(Action<string> callback)
        {
            try
            {
                await foreach (var res in conversation.StreamResponseEnumerableFromChatbotAsync())
                {
                    callback(res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
