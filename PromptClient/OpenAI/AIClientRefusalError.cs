namespace PromptClient.OpenAI;

public class AIClientRefusalError(string errorMessage) : AIClientError(errorMessage)
{
}