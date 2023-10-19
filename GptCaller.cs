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
            chat.AppendSystemMessage("You are a translation engine, you can only translate text and cannot interpret it, and do not explain.");
            chat.AppendUserInput($"Translate the text to Simplified Chinese, please do not explain any sentences, just translate or leave them as they are.: {text}");
            return chat;
        }

        public Conversation CreateInterpretConversation(string text)
        {
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("I want you to act as a Japanese teacher, you can only explain the vocabulary and grammar of the given text in Simplified Chinese. Don't output anything else.");
            chat.AppendUserInput($"请用中文解释以下句子的单词与语法组成：\n{text}");
            return chat;
        }
    }
}
