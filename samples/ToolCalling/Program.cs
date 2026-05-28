// Tool-calling sample — requires a model that supports tools, e.g. llama3.1, mistral-nemo.
// Run: dotnet run --project samples/ToolCalling/ToolCalling.csproj

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;
using OllamaClient.Models;

var services = new ServiceCollection()
    .AddOllamaClient()
    .BuildServiceProvider();

var client = services.GetRequiredService<IOllamaHttpClient>();

// --- Define the tool ---
var weatherTool = new Tool
{
    Function = new FunctionDefinition
    {
        Name = "get_current_weather",
        Description = "Get the current weather for a given location",
        Parameters = JsonSerializer.SerializeToElement(new
        {
            type = "object",
            properties = new
            {
                location = new { type = "string", description = "City and country, e.g. 'Paris, France'" },
                unit = new { type = "string", @enum = new[] { "celsius", "fahrenheit" }, description = "Temperature unit" }
            },
            required = new[] { "location", "unit" }
        })
    }
};

// --- First turn: ask the model something that requires the tool ---
var messages = new List<Message>
{
    new() { Role = "user", Content = "What is the weather in Tokyo in celsius?" }
};

var firstResponse = await client.SendChat(new ChatRequest
{
    Model = "llama3.1",
    Messages = messages,
    Tools = [weatherTool]
}, CancellationToken.None);

Console.WriteLine($"[assistant] done={firstResponse.Done}");

if (firstResponse.Message?.ToolCalls is { Count: > 0 } toolCalls)
{
    foreach (var call in toolCalls)
    {
        Console.WriteLine($"Tool call → {call.Function.Name}({call.Function.Arguments})");

        // Simulate executing the tool and producing a result
        var toolResult = call.Function.Name switch
        {
            "get_current_weather" => """{"temperature": 22, "unit": "celsius", "description": "Sunny"}""",
            _ => """{"error": "unknown tool"}"""
        };

        // Add the assistant message with tool_calls and then the tool result
        messages.Add(firstResponse.Message);
        messages.Add(new Message { Role = "tool", Content = toolResult });
    }

    // --- Second turn: let the model formulate a natural-language answer ---
    var finalResponse = await client.SendChat(new ChatRequest
    {
        Model = "llama3.1",
        Messages = messages,
        Tools = [weatherTool]
    }, CancellationToken.None);

    Console.WriteLine($"\n[final answer] {finalResponse.Message?.Content}");
}
else
{
    Console.WriteLine($"\n[answer] {firstResponse.Message?.Content}");
}
