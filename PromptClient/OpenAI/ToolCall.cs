namespace PromptClient.OpenAI;

public struct ToolCall
{
    public string Id { get; set; }

    public string Type { get; set; }

    public ToolCallFunction Function { get; set; }
}
