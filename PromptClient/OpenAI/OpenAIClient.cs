using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DevDotNetSdk.Models;

namespace PromptClient.OpenAI;

public class OpenAIClient : IAIClient, IDisposable
{
    private const string DefaultModel = TokenizerModels.Gpt4o;
    private const double DefaultTemperature = 0.7;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient? _httpClient;
    private readonly OpenAIClientConfig _defaultConfig;
    private readonly string _apiKey;
    private bool _disposedValue;

    public OpenAIClient(HttpClient httpClient, AICredentials creds, OpenAIClientConfig? config = null)
    {
        if (string.IsNullOrWhiteSpace(creds.ApiKey))
        {
            throw new Exception("API key cannot be null or empty.");
        }
        _jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        _httpClient = httpClient;
        _apiKey = creds.ApiKey;
        _defaultConfig = config ?? CreateDefaultConfig();
        _httpClient.BaseAddress = new Uri("https://api.openai.com");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    private static OpenAIClientConfig CreateDefaultConfig() => new();

    public async Task<Result<AIClientResponse, AIClientError>> SendAsync(
        List<AIMessage> messages, OpenAIClientConfig? requestConfig = null)
    {
        var completionRes = await SendRawAsync(messages, requestConfig); 
        if (completionRes.IsError)
        {
            return Result<AIClientResponse, AIClientError>.Fail(completionRes.UnwrapError());
        }
        var completion = completionRes.Unwrap();
        return GetClientResponse(completion);
    }

    public async Task<Result<ChatCompletionResponse, AIClientError>> SendRawAsync(
        List<AIMessage> messages,
        OpenAIClientConfig? requestConfig = null)
    {
        var mergedConfig = MergeConfigs(_defaultConfig, requestConfig);
        var request = new ChatCompletionRequest(
            Model: mergedConfig.Model ?? DefaultModel,
            Messages: messages,
            MaxCompletionTokens: mergedConfig.MaxCompletionTokens,
            Temperature: mergedConfig.Temperature ?? DefaultTemperature,
            TopP: mergedConfig.TopP,
            Stream: mergedConfig.Stream,
            Store: mergedConfig.Store,
            Metadata: mergedConfig.Metadata,
            FrequencyPenalty: mergedConfig.FrequencyPenalty,
            PresencePenalty: mergedConfig.PresencePenalty,
            LogitBias: mergedConfig.LogitBias,
            LogProbs: mergedConfig.LogProbs,
            TopLogProbs: mergedConfig.TopLogProbs,
            N: mergedConfig.N,
            Modalities: mergedConfig.Modalities,
            Prediction: mergedConfig.Prediction,
            Audio: mergedConfig.Audio,
            ResponseFormat: mergedConfig.ResponseFormat,
            Seed: mergedConfig.Seed,
            ServiceTier: mergedConfig.ServiceTier,
            Stop: mergedConfig.Stop,
            ToolChoice: mergedConfig.ToolChoice,
            ParallelToolCalls: mergedConfig.ParallelToolCalls,
            User: mergedConfig.User
        );
        try
        {
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            using var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient!.PostAsync("/v1/chat/completions", requestContent);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return Result<ChatCompletionResponse, AIClientError>.Fail(new AIClientHttpError(response.StatusCode, errorMessage));
            }
            var chatResponse = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            return Result<ChatCompletionResponse, AIClientError>.Ok(chatResponse);
        }
        catch (Exception ex)
        {
            return Result<ChatCompletionResponse, AIClientError>.Fail(new AIClientUnknownError(ex.ToString()));
        }
    }

    private static Result<AIClientResponse, AIClientError> GetClientResponse(ChatCompletionResponse completion)
    {
        var choices = new AIClientResponseChoice[completion.Choices.Count];
        var i = 0;
        foreach (var choice in completion.Choices)
        {
            if (choice.Message.Refusal != null)
            {                
                return Result<AIClientResponse, AIClientError>.Fail(new AIClientRefusalError(choice.Message.Refusal));
            }
            var chatMessage = choice.Message;
            if (chatMessage.Content == null)
            {
                return Result<AIClientResponse, AIClientError>.Fail(new AIClientContentError("No content in message."));
            }
            var choiceMessage = new AIClientResponseMessage(
                StringToRole(chatMessage.Role), 
                chatMessage.Content
            );

            choices[i] = new AIClientResponseChoice(choice.Index, choiceMessage);
            i++;
        }
        var response = new AIClientResponse(
            completion.Model,
            [.. choices],
            completion.Usage.PromptTokens,
            completion.Usage.CompletionTokens
        );

        return Result<AIClientResponse, AIClientError>.Ok(response);
    }

    private static AIMessageRole StringToRole(string role)
    {
        return role switch
        {
            "system" => AIMessageRole.System,
            "user" => AIMessageRole.User,
            "assistant" => AIMessageRole.Assistant,
            _ => throw new Exception($"Unknown role: {role}")
        };
    }

    private static OpenAIClientConfig MergeConfigs(
        OpenAIClientConfig defaultConfig,
        OpenAIClientConfig? overrideConfig)
    {
        return new OpenAIClientConfig
        {
            Model = overrideConfig?.Model ?? defaultConfig.Model,
            MaxCompletionTokens = overrideConfig?.MaxCompletionTokens ?? defaultConfig.MaxCompletionTokens,
            Temperature = overrideConfig?.Temperature ?? defaultConfig.Temperature,
            TopP = overrideConfig?.TopP ?? defaultConfig.TopP,
            Stream = overrideConfig?.Stream ?? defaultConfig.Stream,
            Store = overrideConfig?.Store ?? defaultConfig.Store,
            Metadata = overrideConfig?.Metadata ?? defaultConfig.Metadata,
            FrequencyPenalty = overrideConfig?.FrequencyPenalty ?? defaultConfig.FrequencyPenalty,
            PresencePenalty = overrideConfig?.PresencePenalty ?? defaultConfig.PresencePenalty,
            LogitBias = overrideConfig?.LogitBias ?? defaultConfig.LogitBias,
            LogProbs = overrideConfig?.LogProbs ?? defaultConfig.LogProbs,
            TopLogProbs = overrideConfig?.TopLogProbs ?? defaultConfig.TopLogProbs,
            N = overrideConfig?.N ?? defaultConfig.N,
            Modalities = overrideConfig?.Modalities ?? defaultConfig.Modalities,
            Prediction = overrideConfig?.Prediction ?? defaultConfig.Prediction,
            Audio = overrideConfig?.Audio ?? defaultConfig.Audio,
            ResponseFormat = overrideConfig?.ResponseFormat ?? defaultConfig.ResponseFormat,
            Seed = overrideConfig?.Seed ?? defaultConfig.Seed,
            ServiceTier = overrideConfig?.ServiceTier ?? defaultConfig.ServiceTier,
            Stop = overrideConfig?.Stop ?? defaultConfig.Stop,
            ToolChoice = overrideConfig?.ToolChoice ?? defaultConfig.ToolChoice,
            ParallelToolCalls = overrideConfig?.ParallelToolCalls ?? defaultConfig.ParallelToolCalls,
            User = overrideConfig?.User ?? defaultConfig.User
        };
    }
    
    private record ChatCompletionRequest(
        string Model,
        List<AIMessage> Messages,
        int? MaxCompletionTokens,
        double? Temperature,
        double? TopP,
        bool? Stream,
        bool? Store,
        object? Metadata,
        double? FrequencyPenalty,
        double? PresencePenalty,
        Dictionary<string, double>? LogitBias,
        bool? LogProbs,
        int? TopLogProbs,
        int? N,
        List<string>? Modalities,
        object? Prediction,
        object? Audio,
        object? ResponseFormat,
        int? Seed,
        string? ServiceTier,
        string[]? Stop,
        string? ToolChoice,
        bool? ParallelToolCalls,
        string? User
    );

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                //Don't dispose the HttpClient, it's managed by the caller
                _httpClient = null;
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
