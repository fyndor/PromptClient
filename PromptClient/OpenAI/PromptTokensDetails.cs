using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct PromptTokensDetails
{
    [JsonPropertyName("audio_tokens")]
    public int AudioTokens { get; set; }

    [JsonPropertyName("cached_tokens")]
    public int CachedTokens { get; set; }
}
