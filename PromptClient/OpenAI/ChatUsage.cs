using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct ChatUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
    [JsonPropertyName("completion_token_details")]
    public CompletionTokenDetails CompletionTokenDetails { get; set; }
    [JsonPropertyName("prompt_tokens_details")]
    public PromptTokensDetails PromptTokensDetails { get; set; }
}
