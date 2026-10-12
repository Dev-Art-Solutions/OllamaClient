# OllamaClient

.NET client library for [Ollama](https://github.com/ollama/ollama) — run large language models locally and integrate them into your C# applications with minimal boilerplate.

[![build and test](https://github.com/Dev-Art-Solutions/OllamaClient/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/Dev-Art-Solutions/OllamaClient/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/OllamaClient.svg)](https://www.nuget.org/packages/OllamaClient/)
[![Downloads](https://img.shields.io/nuget/dt/OllamaClient.svg)](https://www.nuget.org/packages/OllamaClient/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Quick Start

**Install:**

```bash
dotnet add package OllamaClient
```

**Use:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;

var services = new ServiceCollection()
    .AddOllamaClient()          // defaults to http://localhost:11434/
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

// List available models
var models = await client.GetModels(CancellationToken.None);

// Streaming chat
await foreach (var chunk in client.SendChat(new ChatStreamRequest
{
    Model = models.Models[0].Name,
    Messages = [new Message { Role = "user", Content = "Hello!" }]
}, CancellationToken.None))
{
    Console.Write(chunk.Message?.Content);
}
```

## Features

| Endpoint | Method | Streaming |
|----------|--------|-----------|
| `GET /api/tags` | `GetModels()` | — |
| `POST /api/generate` | `Generate()` | `Generate(GenerateStreamRequest)` |
| `POST /api/chat` | `SendChat()` | `SendChat(ChatStreamRequest)` |
| `POST /api/create` | `Create()` | `Create(CreateModelStreamRequest)` |
| `HEAD /api/blobs/:digest` | `BlobExists()` | — |
| `POST /api/blobs/:digest` | `PushBlob()` | — |
| `POST /api/copy` | `Copy()` | — |
| `DELETE /api/delete` | `Delete()` | — |
| `POST /api/show` | `Show()` | — |
| `POST /api/pull` | `Pull()` | `Pull(PullStreamRequest)` |
| `POST /api/push` | `Push()` | `Push(PushStreamRequest)` |
| `POST /api/embed` | `Embed()` | — |
| `POST /api/embeddings` | `GetEmbeddings()` (obsolete) | — |
| `GET /api/ps` | `GetRunningModels()` | — |
| `GET /api/version` | `GetVersion()` | — |
| `GET /api/status` | `GetStatus()` | — |

Also covered: tool calling (with tool-call ids), structured outputs, images, thinking (`Think = true` or an effort level such as `ThinkValue.High`), token log probabilities (`Logprobs`, `TopLogprobs`), `done_reason`, and cloud-model metadata.

### Thinking and log probabilities

```csharp
var response = await client.SendChat(new ChatRequest
{
    Model = "qwen3",
    Messages = [new() { Role = "user", Content = "Is 9.11 bigger than 9.9?" }],
    Think = ThinkValue.Low,      // or true / false
    Logprobs = true,
    TopLogprobs = 3,
}, ct);

Console.WriteLine(response.Message!.Thinking);  // the reasoning trace
Console.WriteLine(response.Message.Content);    // the answer
Console.WriteLine(response.DoneReason);         // "stop" or "length"
```

### Creating a model

`/api/create` no longer takes a Modelfile. Derive from an existing model, or upload GGUF/safetensors files as blobs first:

```csharp
await client.Create(new CreateModelRequest
{
    Model = "mario",
    From = "llama3.2",
    System = "You are Mario from Super Mario Bros.",
    Parameters = new() { ["temperature"] = 0.7 },
}, ct);
```

**Stateful multi-turn chat** is available via `IStatefulConversationOllamaService`, which manages message history automatically across turns.

## Configuration

### Dependency injection (default endpoint)

```csharp
services.AddOllamaClient();
// connects to http://localhost:11434/
```

### Custom base URL

```csharp
services.AddOllamaClient(cfg =>
{
    cfg.OllamaEndpoint = "http://my-ollama-host:11434/";
});
```

### Timeout

Configure the underlying `HttpClient` via the standard `IHttpClientBuilder`:

```csharp
services.AddOllamaClient();
services.AddHttpClient<OllamaHttpClient>()
        .ConfigureHttpClient(c => c.Timeout = TimeSpan.FromMinutes(5));
```

## Examples

See [`src/OllamaClient.QuickStart/`](src/OllamaClient.QuickStart/) for a runnable console app demonstrating pull, list, and streaming chat.

## Compatibility

| OllamaClient | .NET | Minimum Ollama |
|---|---|---|
| 2.1.x | net9.0, net10.0 | API of Ollama 0.40; tested live against 0.34 |
| 2.0.x | net9.0, net10.0 | 0.1.x |
| 1.1.x | net9.0, net10.0 | 0.1.x |
| 1.0.x | net8.0 | 0.1.x |

## Contributing

Pull requests are welcome. Please read [CONTRIBUTING.md](CONTRIBUTING.md) before opening an issue or PR.

## License

MIT — see [LICENSE](LICENSE).
