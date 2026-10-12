namespace OllamaClient;

using Models;

/// <summary>
/// Client for REST Ollama API
/// </summary>
public interface IOllamaHttpClient
{
    /// <summary>
    /// Create a model from an existing model (<see cref="CreateModelRequest.From"/>) or from GGUF/safetensors files
    /// (<see cref="CreateModelRequest.Files"/>). Files must be uploaded first with <see cref="PushBlob"/>.
    /// </summary>
    /// <param name="request">Create Model Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>The status of the created model</returns>
    Task<CreateModelResponse> Create(CreateModelRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Create a model from an existing model (<see cref="CreateModelRequest.From"/>) or from GGUF/safetensors files
    /// (<see cref="CreateModelRequest.Files"/>), streaming the progress. Files must be uploaded first with <see cref="PushBlob"/>.
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
    /// Get the Ollama server version.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Version response</returns>
    Task<VersionResponse> GetVersion(CancellationToken cancellationToken);

    /// <summary>
    /// List models that are currently loaded into memory.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Running models</returns>
    Task<PsResponse> GetRunningModels(CancellationToken cancellationToken);

    /// <summary>
    /// Get the server status, e.g. whether cloud models are available.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Status response</returns>
    Task<StatusResponse> GetStatus(CancellationToken cancellationToken);

    /// <summary>
    /// Check whether a file blob exists on the server. Used before <see cref="PushBlob"/> to skip uploads the server already has.
    /// </summary>
    /// <param name="digest">SHA256 digest of the blob, in the form "sha256:&lt;hex&gt;"</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>True if the blob exists</returns>
    Task<bool> BlobExists(string digest, CancellationToken cancellationToken);

    /// <summary>
    /// Upload a file (e.g. a GGUF or safetensors file) to the server as a blob, so it can be referenced in
    /// <see cref="CreateModelRequest.Files"/>. The server rejects the upload if the content does not match the digest.
    /// </summary>
    /// <param name="digest">SHA256 digest of the content, in the form "sha256:&lt;hex&gt;"</param>
    /// <param name="content">The file content</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    Task PushBlob(string digest, Stream content, CancellationToken cancellationToken);

    /// <summary>
    /// Generate embeddings from a model using the legacy <c>/api/embeddings</c> endpoint.
    /// </summary>
    /// <param name="embeddingsRequest">Embeddings Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Embeddings Response</returns>
    [Obsolete("Use Embed(EmbedRequest, CancellationToken) instead. The /api/embeddings endpoint is legacy and does not support batch input.")]
    Task<EmbeddingsResponse> GetEmbeddings(EmbeddingsRequest embeddingsRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Generate embeddings from a model using the current <c>/api/embed</c> endpoint.
    /// Supports a single string or a list of strings as input.
    /// </summary>
    /// <param name="embedRequest">Embed Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Embed Response containing one float array per input text</returns>
    Task<EmbedResponse> Embed(EmbedRequest embedRequest, CancellationToken cancellationToken);
}
