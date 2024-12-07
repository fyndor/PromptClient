using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct ChatCompletionResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public int Created { get; set; }
    public string Model { get; set; }
    public List<ChatChoice> Choices { get; set; }
    public ChatUsage Usage { get; set; }
    [JsonPropertyName("service_tier")]
    public string? ServiceTier { get; set; }
    [JsonPropertyName("system_fingerprint")]
    public string SystemFingerprint { get; set; }
}
