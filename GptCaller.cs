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


        public GptCaller() {
            api = new OpenAIAPI(UserConfig.api_key);
            api.ApiUrlFormat = UserConfig.api_url;
        }

        public Conversation CreateTranslateConversation(string text)
        {
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are a translation engine, translate the text to Simplified Chinese. Don't output anything other than translation results.");
            chat.AppendUserInput($"{text}");
            return chat;
        }

        public Conversation CreateInterpretConversation(string text)
        {
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are a Japanese teacher, list and explain the vocabulary (except prepositions) and grammar of the given text in Simplified Chinese. Your output consists of three parts: translation, vocabulary, grammar. Don't use English and romaji.");
            chat.AppendUserInput($"{text}");
            return chat;
        }
    }
}
