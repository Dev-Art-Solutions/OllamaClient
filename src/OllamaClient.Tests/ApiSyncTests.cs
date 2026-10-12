namespace OllamaClient.Tests;

using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using OllamaClient.Models;

/// <summary>
/// Wire-format checks for the fields and endpoints added to match the Ollama API as of v0.40.
/// The response payloads are real captures from an Ollama server, not hand-written shapes.
/// </summary>
public class ApiSyncTests
{
    [Fact]
    public void ChatResponseParsesDoneReasonLogprobsAndCachedCount()
    {
        // Captured from Ollama with "logprobs": true, "top_logprobs": 2.
        const string json = """
        {"model":"qwen2.5:0.5b","created_at":"2026-10-12T13:06:57.2871448Z","message":{"role":"assistant","content":"Hello!"},"done":true,"done_reason":"length","logprobs":[{"token":"Hello","logprob":-0.05254403129220009,"bytes":[72,101,108,108,111],"top_logprobs":[{"token":"Hello","logprob":-0.05254403129220009,"bytes":[72,101,108,108,111]},{"token":"Hi","logprob":-3.212080478668213,"bytes":[72,105]}]},{"token":"!","logprob":-0.008427867665886879,"bytes":[33],"top_logprobs":[{"token":"!","logprob":-0.008427867665886879,"bytes":[33]},{"token":",","logprob":-5.259746074676514,"bytes":[44]}]}],"total_duration":205137900,"load_duration":6356300,"prompt_eval_count":31,"prompt_eval_cached_count":30,"prompt_eval_duration":189907000,"eval_count":2,"eval_duration":4935000}
        """;

        var response = JsonSerializer.Deserialize<ChatResponse>(json)!;

        Assert.Equal("length", response.DoneReason);
        Assert.Equal(30, response.PromptEvalCachedCount);
        Assert.Equal(2, response.Logprobs!.Count);
        Assert.Equal("Hello", response.Logprobs[0].Token);
        Assert.Equal([72, 101, 108, 108, 111], response.Logprobs[0].Bytes!);
        Assert.Equal("Hi", response.Logprobs[0].TopLogprobs![1].Token);
    }

    [Fact]
    public void ChatResponseReadsThinkingAndToolCallIdsFromTheMessage()
    {
        const string json = """
        {"model":"m","created_at":"2026-10-12T13:00:00Z","message":{"role":"assistant","content":"","thinking":"The user wants the weather.","tool_calls":[{"id":"call_1","function":{"index":0,"name":"get_weather","arguments":{"city":"Sofia"}}}]},"done":true}
        """;

        var response = JsonSerializer.Deserialize<ChatResponse>(json)!;

        Assert.Equal("The user wants the weather.", response.Message!.Thinking);
#pragma warning disable CS0618 // the obsolete top-level property must forward to the message
        Assert.Equal("The user wants the weather.", response.Thinking);
#pragma warning restore CS0618
        var call = Assert.Single(response.Message.ToolCalls!);
        Assert.Equal("call_1", call.Id);
        Assert.Equal(0, call.Function.Index);
        Assert.Equal("Sofia", call.Function.Arguments.GetProperty("city").GetString());
    }

    [Fact]
    public void RunningModelParsesContextLength()
    {
        // Captured from GET /api/ps.
        const string json = """
        {"models":[{"name":"ornith-1.5:35b","model":"ornith-1.5:35b","size":23210376556,"digest":"9f3b89b2521908dd2e6f7a11fa368e62c8f89e1075f22604e4d1a76dd1240fcc","details":{"parent_model":"","format":"gguf","family":"qwen35moe","families":["qwen35moe"],"parameter_size":"35.5B","quantization_level":"Q4_K_M"},"expires_at":"2026-10-12T16:11:57.5455997+03:00","size_vram":22257032231,"context_length":65536}]}
        """;

        var model = Assert.Single(JsonSerializer.Deserialize<PsResponse>(json)!.Models);

        Assert.Equal(65536, model.ContextLength);
        Assert.Equal("", model.Details!.ParentModel);
    }

    [Fact]
    public void LegacyEmbeddingsResponseReadsTheEmbeddingField()
    {
        // /api/embeddings answers with "embedding" (singular); reading "embeddings" left the vector empty.
        const string json = """{"embedding":[0.39,0.44,-4.29]}""";

        var response = JsonSerializer.Deserialize<EmbeddingsResponse>(json)!;

        Assert.Equal(3, response.Embedding.Length);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void ThinkSerializesABoolean(bool think, string expected)
    {
        var json = JsonSerializer.SerializeToElement(new ChatRequest { Model = "m", Think = think });

        Assert.Equal(expected, json.GetProperty("think").GetRawText());
    }

    [Fact]
    public void ThinkSerializesAnEffortLevel()
    {
        var json = JsonSerializer.SerializeToElement(new GenerateRequest { Model = "m", Think = ThinkValue.High });

        Assert.Equal("high", json.GetProperty("think").GetString());
    }

    [Fact]
    public void UnsetThinkAndNewOptionalFieldsAreOmitted()
    {
        var json = JsonSerializer.SerializeToElement(new ChatRequest { Model = "m" });

        foreach (var name in new[] { "think", "truncate", "shift", "logprobs", "top_logprobs" })
        {
            Assert.False(json.TryGetProperty(name, out _), $"{name} should be omitted when unset");
        }
    }

    [Fact]
    public void CreateRequestSendsModelAndFromWithoutTheLegacyFields()
    {
        var json = JsonSerializer.SerializeToElement(new CreateModelRequest
        {
            Model = "mario",
            From = "llama3.2",
            System = "You are Mario.",
            Parameters = new() { ["temperature"] = 0.2 },
        });

        Assert.Equal("mario", json.GetProperty("model").GetString());
        Assert.Equal("llama3.2", json.GetProperty("from").GetString());
        Assert.Equal(0.2, json.GetProperty("parameters").GetProperty("temperature").GetDouble());
        Assert.False(json.TryGetProperty("name", out _));
        Assert.False(json.TryGetProperty("modelfile", out _));
    }

    [Fact]
    public async Task GetStatusReadsCloudStatus()
    {
        var handler = new RecordingHandler(HttpStatusCode.OK, """{"cloud":{"disabled":false,"source":"none"}}""");

        var status = await CreateClient(handler).GetStatus(default);

        Assert.Equal(HttpMethod.Get, handler.Method);
        Assert.Equal("/api/status", handler.Path);
        Assert.False(status.Cloud.Disabled);
        Assert.Equal("none", status.Cloud.Source);
    }

    [Theory]
    [InlineData(HttpStatusCode.OK, true)]
    [InlineData(HttpStatusCode.NotFound, false)]
    public async Task BlobExistsSendsHeadAndMapsTheStatus(HttpStatusCode status, bool expected)
    {
        var handler = new RecordingHandler(status, "");

        var exists = await CreateClient(handler).BlobExists("sha256:abc", default);

        Assert.Equal(HttpMethod.Head, handler.Method);
        Assert.Equal("/api/blobs/sha256:abc", handler.Path);
        Assert.Equal(expected, exists);
    }

    [Fact]
    public async Task PushBlobPostsTheContent()
    {
        var handler = new RecordingHandler(HttpStatusCode.Created, "");

        await CreateClient(handler).PushBlob("sha256:abc", new MemoryStream(Encoding.UTF8.GetBytes("GGUF")), default);

        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("/api/blobs/sha256:abc", handler.Path);
        Assert.Equal("GGUF", handler.Body);
    }

    [Fact]
    public async Task PushBlobThrowsOnADigestMismatch()
    {
        var handler = new RecordingHandler(HttpStatusCode.BadRequest, """{"error":"digest mismatch"}""");

        await Assert.ThrowsAsync<Exceptions.OllamaException>(
            () => CreateClient(handler).PushBlob("sha256:abc", new MemoryStream([1]), default));
    }

    private static OllamaHttpClient CreateClient(HttpMessageHandler handler)
    {
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(() => new HttpClient(handler, disposeHandler: false) { BaseAddress = new Uri("http://localhost:11434/") });
        return new OllamaHttpClient(factory.Object);
    }

    private sealed class RecordingHandler(HttpStatusCode status, string responseBody) : HttpMessageHandler
    {
        public HttpMethod? Method { get; private set; }

        public string? Path { get; private set; }

        public string? Body { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Method = request.Method;
            Path = request.RequestUri!.AbsolutePath;
            Body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(status) { Content = new StringContent(responseBody, Encoding.UTF8, "application/json") };
        }
    }
}
