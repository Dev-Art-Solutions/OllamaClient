// Embeddings sample — generate vector embeddings for one or more texts.
// Run: dotnet run --project samples/Embeddings/Embeddings.csproj

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

// --- Single string ---
var single = await client.Embed(new EmbedRequest
{
    Model = "nomic-embed-text",
    Input = JsonSerializer.SerializeToElement("The quick brown fox jumps over the lazy dog")
}, CancellationToken.None);

Console.WriteLine($"Single embedding — {single.Embeddings[0].Count} dimensions");

// --- Batch of strings ---
var batch = await client.Embed(new EmbedRequest
{
    Model = "nomic-embed-text",
    Input = JsonSerializer.SerializeToElement(new[]
    {
        "Cats are independent pets.",
        "Dogs are loyal companions.",
        "Fish are low-maintenance animals."
    })
}, CancellationToken.None);

Console.WriteLine($"Batch embeddings — {batch.Embeddings.Count} vectors, each {batch.Embeddings[0].Count} dimensions");
