namespace PromptClient;

public abstract class AIClientError(string errorMessage)
{

    public string ErrorMessage { get; } = errorMessage;
}
