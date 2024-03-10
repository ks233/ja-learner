using OpenAI_API;
using OpenAI_API.Chat;
using System.Net;

namespace ja_learner
{
    internal class GptCaller
    {
        private static OpenAIAPI api;

        private static IHttpClientFactory defaultFactory;
        private static IHttpClientFactory proxyFactory;

        public static void Initialize()
        {
            api = new(Program.APP_SETTING.GPT.ApiKey) { ApiUrlFormat = Program.APP_SETTING.GPT.ApiUrl };
            defaultFactory = api.HttpClientFactory;
            proxyFactory = new MyHttpClientFactory(Program.APP_SETTING.HttpProxy);
        }

        public static void SetProxy(bool useProxy)
        {
            if (useProxy)
            {
                api.HttpClientFactory = proxyFactory;
            }
            else
            {
                api.HttpClientFactory = defaultFactory;
            }
        }

        public static Conversation CreateTranslateConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a translation engine, translate the text to Simplified Chinese. Don't output anything other than translation results.");
            if (UserConfig.useExtraPrompt)
            {
                AddExtraSystemPrompt(conversation);
            }
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        public static Conversation CreateInterpretConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.AppendSystemMessage("You are a Japanese teacher, List and explain the vocabulary (except prepositions) and grammar of the given text in Simplified Chinese. Your output consists of three parts: translation, vocabulary, grammar. Don't use English and romaji.");
            if (UserConfig.useExtraPrompt)
            {
                AddExtraSystemPrompt(conversation);
            }
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        private static void AddExtraSystemPrompt(Conversation conversation)
        {
            if (UserConfig.ExtraPrompt.Length > 0)
            {
                conversation.AppendSystemMessage(UserConfig.ExtraPrompt);
            }
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

    class MyHttpClientFactory : IHttpClientFactory
    {
        private string proxy;
        public MyHttpClientFactory(string proxy)
        {
            this.proxy = proxy;
        }
        HttpClient IHttpClientFactory.CreateClient(string name)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy($"http://{proxy}")
            };
            var client = new HttpClient(handler);
            return client;
        }
    }

}
