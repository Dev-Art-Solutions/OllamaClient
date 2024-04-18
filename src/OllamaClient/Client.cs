namespace OllamaClient;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

using Models;
using Configuration;
using Extensions;

public class Client(IHttpClientFactory httpClientFactory, OllamaConfiguration configuration) : IClient
{
    public async Task<CopyResponse> Copy(CopyRequest copyRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync("copy", copyRequest, (response) => new CopyResponse() { IsSuccessful = response!.IsSuccessStatusCode }, cancellationToken);
    }

    public IAsyncEnumerable<CreateModelResponse> Create(CreateModelRequest request, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<CreateModelResponse, CreateModelRequest>("create", request, cancellationToken);
    }

    public async Task<DeleteResponse> Delete(DeleteRequest deleteRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync("delete", deleteRequest, (response) => new DeleteResponse() { IsSuccessful = response!.IsSuccessStatusCode }, cancellationToken);
    }

    public IAsyncEnumerable<GenerateResponse> Generate(GenerateRequest request, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<GenerateResponse, GenerateRequest>("generate", request, cancellationToken);
    }

    public async Task<EmbeddingsResponse> GetEmbeddings(EmbeddingsRequest embeddingsRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<EmbeddingsResponse, EmbeddingsRequest>("embeddings", embeddingsRequest, default, cancellationToken);
    }

    public async Task<GetModelsResponse> GetModels(CancellationToken cancellationToken)
    {
        using var httpClient = httpClientFactory.CreateClient(nameof(Client));
        var response = await httpClient.GetFromJsonAsync<GetModelsResponse>($"{configuration.OllamaEndpoint}api/tags", cancellationToken);

        return response!;
    }

    public IAsyncEnumerable<PullResponse> Pull(PullRequest pullRequest, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<PullResponse, PullRequest>("pull", pullRequest, cancellationToken);
    }

    public IAsyncEnumerable<PushResponse> Push(PushRequest pushRequest, CancellationToken cancellationToken)
    {
        return StreamingPostAsJsonAsync<PushResponse, PushRequest>("push", pushRequest, cancellationToken);
    }

    public async Task<ChatResponse> SendChat(ChatStreamlessRequest chatRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<ChatResponse, ChatStreamlessRequest>("chat", chatRequest, default, cancellationToken);
    }

    public async IAsyncEnumerable<ChatResponse> SendChatStreaming(ChatStreamRequest chatRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var httpClient = httpClientFactory.CreateClient(nameof(Client));
        var response = await httpClient.PostAsJsonAsync($"{configuration.OllamaEndpoint}api/chat", chatRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        await foreach (var value in response.Content.ReadFromNdjsonAsync<ChatResponse>(cancellationToken)!)
        {
            yield return value;
        }
    }

    public async Task<ShowResponse> Show(ShowRequest showRequest, CancellationToken cancellationToken)
    {
        return await PostAsJsonAsync<ShowResponse, ShowRequest>("show", showRequest, null, cancellationToken);
    }

    private async Task<TResult> PostAsJsonAsync<TResult, TRequest>(string action, TRequest value, Func<HttpResponseMessage, TResult>? factory = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = httpClientFactory.CreateClient(nameof(Client));
        var response = await httpClient.PostAsJsonAsync($"{configuration.OllamaEndpoint}api/{action}", value, cancellationToken);
        response.EnsureSuccessStatusCode();

        return (factory != null ? factory(response) : (await response.Content.ReadFromJsonAsync<TResult>(cancellationToken))!);
    }

    private async IAsyncEnumerable<TResult> StreamingPostAsJsonAsync<TResult, TRequest>(string action, TRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    where TRequest : StreamingRequest
    {
        using var client = httpClientFactory.CreateClient(nameof(Client));
        var response = await client.PostAsJsonAsync($"{configuration.OllamaEndpoint}api/{action}", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (request.Stream != null && request.Stream.Value)
        {
            await foreach (var value in response.Content.ReadFromNdjsonAsync<TResult>(cancellationToken))
            {
                yield return value;
            }
        }
        else
        {
            yield return (await response.Content.ReadFromJsonAsync<TResult>(cancellationToken))!;
        }
    }
}
