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
        private static OpenAIAPI api;

        public static void Initialize() {
            SetConfig(UserConfig.api_key, UserConfig.api_url);
        }

        public static void SetConfig(string api_key, string api_url)
        {

            api = new OpenAIAPI(api_key);
            api.ApiUrlFormat = api_url;
        }

        public static Conversation CreateTranslateConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a translation engine, translate the text to Simplified Chinese. Don't output anything other than translation results.");
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        public static Conversation CreateInterpretConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a Japanese teacher, List and explain the vocabulary (except prepositions) and grammar of the given text in Simplified Chinese. Your output consists of three parts: translation, vocabulary, grammar. Don't use English and romaji.");
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        async public static void StreamResponse(Conversation conversation, Action<string> callback)
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
