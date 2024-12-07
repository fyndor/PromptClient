namespace PromptClient.OpenAI;

public class OpenAIClientConfig
{
    public string? Model { get; set; }
    public int? MaxCompletionTokens { get; set; }
    public double? Temperature { get; set; }
    public double? TopP { get; set; }
    public bool? Stream { get; set; }
    public bool? Store { get; set; }
    public object? Metadata { get; set; }
    public double? FrequencyPenalty { get; set; }
    public double? PresencePenalty { get; set; }
    public Dictionary<string, double>? LogitBias { get; set; }
    public bool? LogProbs { get; set; }
    public int? TopLogProbs { get; set; }
    public int? N { get; set; }
    public List<string>? Modalities { get; set; }
    public object? Prediction { get; set; }
    public object? Audio { get; set; }
    public object? ResponseFormat { get; set; }
    public int? Seed { get; set; }
    public string? ServiceTier { get; set; }
    public string[]? Stop { get; set; }
    public string? ToolChoice { get; set; }
    public bool? ParallelToolCalls { get; set; }
    public string? User { get; set; }
}
