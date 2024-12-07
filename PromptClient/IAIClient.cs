using PromptClient.OpenAI;

namespace PromptClient;

public interface IAIClient
{
    Task<Result<ChatCompletionResponse, string>> SendPromptAsync(
        List<AIMessage> messages,
        OpenAIClientConfig? requestConfig = null);
}
