using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Net;

namespace ja_learner
{
    internal class GptCaller
    {
        private static OpenAIClient client;
        private static OpenAIClientOptions options;
        public static void Initialize()
        {
            options = new()
            {
                Endpoint = new Uri(Program.APP_SETTING.GPT.ApiUrl)
            };
            Debug.WriteLine(Program.APP_SETTING.GPT.ApiUrl);
            client = new(
                new ApiKeyCredential(Program.APP_SETTING.GPT.ApiKey),
                options
            );
        }

        public static void SetProxy(bool useProxy)
        {
            if (useProxy)
            {
                HttpClientHandler handler = new()
                {
                    Proxy = new WebProxy(new Uri(Program.APP_SETTING.HttpProxy)),
                };
                options = new()
                {
                    Endpoint = new Uri(Program.APP_SETTING.GPT.ApiUrl),
                    Transport = new HttpClientPipelineTransport(new HttpClient(handler))
                };
                client = new(new ApiKeyCredential(Program.APP_SETTING.GPT.ApiKey), options);
            }
            else
            {
                options = new()
                {
                    Endpoint = new Uri(Program.APP_SETTING.GPT.ApiUrl)
                };
                client = new(new ApiKeyCredential(Program.APP_SETTING.GPT.ApiKey), options);
            }
        }

        public static async void Chat(string systemPrompt, string userPrompt, Action<string> streamCallback)
        {
            if (UserConfig.useExtraPrompt)
            {
                systemPrompt += UserConfig.ExtraPrompt;
            }
            try
            {
                ChatClient chatClient = client.GetChatClient(Program.APP_SETTING.GPT.Model);
                AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(systemPrompt + "\n" + userPrompt);
                await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
                {
                    if (completionUpdate.ContentUpdate.Count > 0)
                    {
                        streamCallback(completionUpdate.ContentUpdate[0].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async void CreateTranslateConversation(string userPrompt, Action<string> streamCallback)
        {
            string systemPrompt = Program.APP_SETTING.GPT.TranslatePrompt;
            Chat(systemPrompt, userPrompt, streamCallback);
        }

        public static async void CreateInterpretConversation(string userPrompt, Action<string> streamCallback)
        {
            string systemPrompt = Program.APP_SETTING.GPT.ExplainPrompt;
            Chat(systemPrompt, userPrompt, streamCallback);
        }
    }

}
