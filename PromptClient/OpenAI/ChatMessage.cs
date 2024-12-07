using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct ChatMessage
{
    public string Role { get; set; }
    public string? Content { get; set; }
    public string? Refusal { get; set; }
    [JsonPropertyName("tool_calls")]
    public List<ToolCall> ToolCalls { get; set; }
}
