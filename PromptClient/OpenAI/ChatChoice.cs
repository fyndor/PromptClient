using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct ChatChoice
{
    public int Index { get; set; }
    public ChatMessage Message { get; set; }
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}
