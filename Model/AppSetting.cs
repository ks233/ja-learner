namespace ja_learner;

public class AppSettingOptions
{
    public int Port { get; set; } = 8080;
    public string HttpProxy { get; set; } = string.Empty;
    public GPTOptions? GPT { get; set; }
    public bool AnkiEnabled { get; set; } = false;
    public AnkiOptions? Anki { get; set; }
}

public class GPTOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = string.Empty;
    public string ExtraPromptDir { get; set; } = string.Empty;
}

public class AnkiOptions
{
    public string AnkiConnectUrl { get; set; } = string.Empty;
    public string Deck { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public class Fields
    {
        public string Word { get; set; } = string.Empty;
        public string Example { get; set; } = string.Empty;
        public string Explain { get; set; } = string.Empty;
    };
    public Fields? FieldNames { get; set; }
}