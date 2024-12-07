namespace PromptClient;

public static class TokenizerModels
{
    // Chat models
    public const string Gpt4o = "gpt-4o";
    public const string O1 = "o1";
    public const string Gpt4 = "gpt-4";
    public const string Gpt35Turbo = "gpt-3.5-turbo";
    public const string Gpt35Turbo16k = "gpt-3.5-turbo-16k";
    public const string Gpt35 = "gpt-35"; // Azure deployment name
    public const string Gpt35TurboAzure = "gpt-35-turbo"; // Azure deployment name
    public const string Gpt35Turbo16kAzure = "gpt-35-turbo-16k"; // Azure deployment name

    // Text models
    public const string TextDavinci003 = "text-davinci-003";
    public const string TextDavinci002 = "text-davinci-002";
    public const string TextDavinci001 = "text-davinci-001";
    public const string TextCurie001 = "text-curie-001";
    public const string TextBabbage001 = "text-babbage-001";
    public const string TextAda001 = "text-ada-001";
    public const string Davinci = "davinci";
    public const string Curie = "curie";
    public const string Babbage = "babbage";
    public const string Ada = "ada";

    // Code models
    public const string CodeDavinci002 = "code-davinci-002";
    public const string CodeDavinci001 = "code-davinci-001";
    public const string CodeCushman002 = "code-cushman-002";
    public const string CodeCushman001 = "code-cushman-001";
    public const string DavinciCodex = "davinci-codex";
    public const string CushmanCodex = "cushman-codex";

    // Edit models
    public const string TextDavinciEdit001 = "text-davinci-edit-001";
    public const string CodeDavinciEdit001 = "code-davinci-edit-001";

    // Embeddings
    public const string TextEmbeddingAda002 = "text-embedding-ada-002";
    public const string TextEmbedding3Small = "text-embedding-3-small";
    public const string TextEmbedding3Large = "text-embedding-3-large";

    // Old embeddings
    public const string TextSimilarityDavinci001 = "text-similarity-davinci-001";
    public const string TextSimilarityCurie001 = "text-similarity-curie-001";
    public const string TextSimilarityBabbage001 = "text-similarity-babbage-001";
    public const string TextSimilarityAda001 = "text-similarity-ada-001";
    public const string TextSearchDavinciDoc001 = "text-search-davinci-doc-001";
    public const string TextSearchCurieDoc001 = "text-search-curie-doc-001";
    public const string TextSearchBabbageDoc001 = "text-search-babbage-doc-001";
    public const string TextSearchAdaDoc001 = "text-search-ada-doc-001";
    public const string CodeSearchBabbageCode001 = "code-search-babbage-code-001";
    public const string CodeSearchAdaCode001 = "code-search-ada-code-001";

    // Open source
    public const string Gpt2 = "gpt2";
}
