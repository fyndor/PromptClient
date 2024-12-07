using PromptClient.OpenAI;

namespace PromptClient;

public interface IAIClient
{
    Task<Result<AIClientResponse, AIClientError>> SendAsync(
        List<AIMessage> messages,
        OpenAIClientConfig? requestConfig = null);
}
