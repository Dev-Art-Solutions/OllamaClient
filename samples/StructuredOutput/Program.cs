// Structured output sample — extracts a typed object from free text using a JSON Schema.
// Run: dotnet run --project samples/StructuredOutput/StructuredOutput.csproj

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

// JSON Schema for the structured response
var schema = JsonSerializer.SerializeToElement(new
{
    type = "object",
    properties = new
    {
        name       = new { type = "string" },
        birth_year = new { type = "integer" },
        nationality = new { type = "string" }
    },
    required = new[] { "name", "birth_year", "nationality" }
});

var response = await client.SendChat(new ChatRequest
{
    Model = "llama3.1",
    Format = schema,
    Messages =
    [
        new Message
        {
            Role = "user",
            Content = "Tell me about Marie Curie. Respond only with valid JSON matching the provided schema."
        }
    ]
}, CancellationToken.None);

var json = response.Message?.Content ?? "{}";
Console.WriteLine("Raw JSON:");
Console.WriteLine(json);

var person = JsonSerializer.Deserialize<Person>(json);
Console.WriteLine($"\nParsed: {person?.Name} ({person?.Nationality}, born {person?.BirthYear})");

record Person(
    [property: JsonPropertyName("name")]        string Name,
    [property: JsonPropertyName("birth_year")]  int    BirthYear,
    [property: JsonPropertyName("nationality")] string Nationality
);
