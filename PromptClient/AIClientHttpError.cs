using System.Net;

namespace PromptClient;

public class AIClientHttpError(HttpStatusCode statusCode, string errorMessage) 
    : AIClientError(errorMessage)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}