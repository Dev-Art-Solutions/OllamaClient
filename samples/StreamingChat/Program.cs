// Streaming chat sample — tokens are printed to the console as they arrive.
// Run: dotnet run --project samples/StreamingChat/StreamingChat.csproj

using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

var stream = client.SendChat(new ChatStreamRequest
{
    Model = "llama3.2",
    Messages =
    [
        new Message { Role = "user", Content = "Count from 1 to 10 with a short comment on each number." }
    ]
}, CancellationToken.None);

await foreach (var chunk in stream)
{
    if (chunk.Message?.Content is { Length: > 0 } token)
        Console.Write(token);
}

Console.WriteLine();
