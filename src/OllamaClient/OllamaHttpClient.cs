namespace OllamaClient;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

using Models;
using Extensions;
using OllamaClient.Exceptions;

public class OllamaHttpClient(IHttpClientFactory httpClientFactory) : IOllamaHttpClient
{
    public async Task<CopyResponse> Copy(CopyRequest copyRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync("copy", copyRequest, (response) => new CopyResponse() { IsSuccessful = response!.IsSuccessStatusCode }, cancellationToken);
    }

    public async Task<CreateModelResponse> Create(CreateModelRequest request, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<CreateModelResponse, CreateModelRequest>("create", request, null, cancellationToken);
    }

    public IAsyncEnumerable<CreateModelResponse> Create(CreateModelStreamRequest request, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<CreateModelResponse, CreateModelStreamRequest>("create", request, cancellationToken);
    }

    public async Task<DeleteResponse> Delete(DeleteRequest deleteRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync("delete", deleteRequest, (response) => new DeleteResponse() { IsSuccessful = response!.IsSuccessStatusCode }, cancellationToken);
    }

    public async Task<GenerateResponse> Generate(GenerateRequest request, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<GenerateResponse, GenerateRequest>("generate", request, null, cancellationToken);
    }

    public IAsyncEnumerable<GenerateResponse> Generate(GenerateStreamRequest request, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<GenerateResponse, GenerateStreamRequest>("generate", request, cancellationToken);
    }

    public async Task<EmbeddingsResponse> GetEmbeddings(EmbeddingsRequest embeddingsRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<EmbeddingsResponse, EmbeddingsRequest>("embeddings", embeddingsRequest, default, cancellationToken);
    }

    public async Task<GetModelsResponse> GetModels(CancellationToken cancellationToken)
    {
        using var httpClient = httpClientFactory.CreateClient(nameof(OllamaHttpClient));
        var response = await httpClient.GetAsync($"api/tags", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new OllamaException(await response.Content.ReadAsStringAsync());
        }

        return (await response.Content.ReadFromJsonAsync<GetModelsResponse>())!;
    }

    public async Task<PullResponse> Pull(PullRequest pullRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<PullResponse, PullRequest>("pull", pullRequest, null, cancellationToken);
    }

    public IAsyncEnumerable<PullResponse> Pull(PullStreamRequest pullRequest, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<PullResponse, PullStreamRequest>("pull", pullRequest, cancellationToken);
    }

    public async Task<PushResponse> Push(PushRequest pushRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<PushResponse, PushRequest>("push", pushRequest, null, cancellationToken);
    }

    public IAsyncEnumerable<PushResponse> Push(PushStreamRequest pushRequest, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<PushResponse, PushStreamRequest>("push", pushRequest, cancellationToken);
    }

    public async Task<ChatResponse> SendChat(ChatRequest chatRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<ChatResponse, ChatRequest>("chat", chatRequest, default, cancellationToken);
    }

    public IAsyncEnumerable<ChatResponse> SendChat(ChatStreamRequest chatRequest, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<ChatResponse, ChatStreamRequest>("chat", chatRequest, cancellationToken);
    }

    public async Task<ShowResponse> Show(ShowRequest showRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<ShowResponse, ShowRequest>("show", showRequest, null, cancellationToken);
    }

    private async Task<TResult> PostAsJsonAsync<TResult, TRequest>(string action, TRequest value, Func<HttpResponseMessage, TResult>? factory = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = httpClientFactory.CreateClient(nameof(OllamaHttpClient));
        var response = await httpClient.PostAsJsonAsync($"api/{action}", value, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new OllamaException(await response.Content.ReadAsStringAsync());
        }

        return (factory != null ? factory(response) : (await response.Content.ReadFromJsonAsync<TResult>(cancellationToken))!);
    }

    private async IAsyncEnumerable<TResult> StreamingPostAsJsonAsync<TResult, TRequest>(string action, TRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    where TRequest : StreamingRequest
    {
        using var client = httpClientFactory.CreateClient(nameof(OllamaHttpClient));
        var response = await client.PostAsJsonAsync($"api/{action}", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new OllamaException(await response.Content.ReadAsStringAsync());
        }

        await foreach (var value in response.Content.ReadFromNdjsonAsync<TResult>(cancellationToken))
        {
            yield return value;
        }
    }
}
