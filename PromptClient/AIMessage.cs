namespace PromptClient;

public struct AIMessage
{
    public AIMessage(AIMessageRole role, string content)
    {
        Role = role;
        Content = content;
    }

    public AIMessage()
    {
        Role = AIMessageRole.System;
        Content = string.Empty;
    }

    public AIMessageRole Role { get; set; }

    public string Content { get; set; }
}
