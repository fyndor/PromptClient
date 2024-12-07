namespace PromptClient.OpenAI;

public class AIClientUnknownError(string message) : AIClientError(message)
{
}