namespace PromptClient.OpenAI;

public class AIClientContentError(string errorMessage) : AIClientError(errorMessage)
{
}
