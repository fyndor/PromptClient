using System.Text.Json.Serialization;

namespace PromptClient.OpenAI;

public struct CompletionTokenDetails
{
    [JsonPropertyName("accepted_prediction_tokens")]
    public int AcceptedPredictionTokens { get; set; }

    [JsonPropertyName("rejected_prediction_tokens")]
    public int RejectedPredictionTokens { get; set; }

    [JsonPropertyName("audio_tokens")]
    public int AudioTokens { get; set; }

    [JsonPropertyName("reasoning_tokens")]
    public int ReasoningTokens { get; set; }
}
