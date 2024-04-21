namespace OllamaClient;

using Models;

/// <summary>
/// Client for REST Ollama API
/// </summary>
public interface IOllamaHttpClient
{
    /// <summary>
    /// Create a model from a Modelfile. It is recommended to set modelfile to the content of the Modelfile rather than just set path. This is a requirement for remote create. Remote model creation must also create any file blobs, fields such as FROM and ADAPTER, explicitly with the server using Create a Blob and the value to the path indicated in the response.
    /// </summary>
    /// <param name="request">Create Model Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>The status of the created model</returns>
    Task<CreateModelResponse> Create(CreateModelRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Create a model from a Modelfile. It is recommended to set modelfile to the content of the Modelfile rather than just set path. This is a requirement for remote create. Remote model creation must also create any file blobs, fields such as FROM and ADAPTER, explicitly with the server using Create a Blob and the value to the path indicated in the response.
    /// </summary>
    /// <param name="request">Create Model Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>The status of the created model</returns>
    IAsyncEnumerable<CreateModelResponse> Create(CreateModelStreamRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Generate a response for a given prompt with a provided model. This is a streaming endpoint, so there will be a series of responses. The final response object will include statistics and additional data from the request.
    /// </summary>
    /// <param name="request">Generate Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Generate Response</returns>
    Task<GenerateResponse> Generate(GenerateRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Generate a response for a given prompt with a provided model. This is a streaming endpoint, so there will be a series of responses. The final response object will include statistics and additional data from the request.
    /// </summary>
    /// <param name="request">Generate Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Generate Response</returns>
    IAsyncEnumerable<GenerateResponse> Generate(GenerateStreamRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Generate the next message in a chat with a provided model. This is not a streaming endpoint, so there will be a single of response. 
    /// </summary>
    /// <param name="chatRequest">Chat Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Chat Response</returns>
    Task<ChatResponse> SendChat(ChatRequest chatRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Generate the next message in a chat with a provided model. This is a streaming endpoint, so there will be a series of responses. The final response object will include statistics and additional data from the request.
    /// </summary>
    /// <param name="chatRequest">Chat Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Chat Response</returns>
    IAsyncEnumerable<ChatResponse> SendChat(ChatStreamRequest chatRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Get a list of available models
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>All available models</returns>
    Task<GetModelsResponse> GetModels(CancellationToken cancellationToken);

    /// <summary>
    /// Copy a model. Creates a model with another name from an existing model.
    /// </summary>
    /// <param name="copyRequest">Copy Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Copy Response(is successful).</returns>
    Task<CopyResponse> Copy(CopyRequest copyRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a model and its data.
    /// </summary>
    /// <param name="deleteRequest">Delete Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Delete Response(is successful)</returns>
    Task<DeleteResponse> Delete(DeleteRequest deleteRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Show information about a model including details, modelfile, template, parameters, license, and system prompt.
    /// </summary>
    /// <param name="showRequest">Show Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Show Model Details Response</returns>
    Task<ShowResponse> Show(ShowRequest showRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Download a model from the ollama library. Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.
    /// </summary>
    /// <param name="pullRequest">Pull model request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Pull model status response</returns>
    Task<PullResponse> Pull(PullRequest pullRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Download a model from the ollama library. Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.
    /// </summary>
    /// <param name="pullRequest">Pull model request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Pull model status response</returns>
    IAsyncEnumerable<PullResponse> Pull(PullStreamRequest pullRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first.
    /// </summary>
    /// <param name="pushRequest">Push model request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Push model status response</returns>
    Task<PushResponse> Push(PushRequest pushRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first.
    /// </summary>
    /// <param name="pushRequest">Push model request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Push model status response</returns>
    IAsyncEnumerable<PushResponse> Push(PushStreamRequest pushRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Generate embeddings from a model
    /// </summary>
    /// <param name="embeddingsRequest">Embeddings Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Embeddings Response</returns>
    Task<EmbeddingsResponse> GetEmbeddings(EmbeddingsRequest embeddingsRequest, CancellationToken cancellationToken);
}
