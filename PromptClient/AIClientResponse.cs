namespace PromptClient;

public readonly struct AIClientResponse(string model, AIClientResponseChoice[] choices, int promptTokens, int completionTokens)
{
    public string Model { get; } = model;
    public AIClientResponseChoice[] Choices { get; } = choices; 
    public int PromptTokens { get; } = promptTokens;
    public int CompletionTokens { get; } = completionTokens;
    public int TotalTokens => PromptTokens + CompletionTokens;
}

public readonly struct AIClientResponseChoice(int index, AIClientResponseMessage message)
{
    public int Index { get; } = index;
    public AIClientResponseMessage Message { get; } = message;
}

public readonly struct AIClientResponseMessage(AIMessageRole role, string content)
{
    public AIMessageRole Role { get; } = role;
    public string Content { get; } = content;
}
