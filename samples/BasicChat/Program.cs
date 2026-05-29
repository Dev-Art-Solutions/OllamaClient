// Basic chat sample — single-turn, non-streaming chat request.
// Run: dotnet run --project samples/BasicChat/BasicChat.csproj

using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

var response = await client.SendChat(new ChatRequest
{
    Model = "llama3.2",
    Messages =
    [
        new Message { Role = "user", Content = "Why is the sky blue? Answer in one sentence." }
    ]
}, CancellationToken.None);

Console.WriteLine(response.Message?.Content);
