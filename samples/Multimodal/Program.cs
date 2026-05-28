// Multimodal sample — sends an image to a vision-capable model (e.g. llava, bakllava).
// Run: dotnet run --project samples/Multimodal/Multimodal.csproj -- path/to/image.jpg
//
// The image is read from disk and base64-encoded before being sent.

using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var imagePath = args.Length > 0 ? args[0] : "image.jpg";

if (!File.Exists(imagePath))
{
    Console.Error.WriteLine($"Image not found: {imagePath}");
    Console.Error.WriteLine("Usage: dotnet run -- <path-to-image>");
    return;
}

var imageBase64 = Convert.ToBase64String(await File.ReadAllBytesAsync(imagePath));

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

var result = client.SendChat(new ChatStreamRequest
{
    Model = "llava",
    Messages =
    [
        new Message
        {
            Role   = "user",
            Content = "Describe what you see in this image.",
            Images  = [imageBase64]
        }
    ]
}, CancellationToken.None);

await foreach (var chunk in result)
{
    if (chunk.Message?.Content is { } text)
        Console.Write(text);
}

Console.WriteLine();
