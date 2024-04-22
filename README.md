# OllamaClient
.NET client library for [Ollama](https://github.com/ollama/ollama)  - your gateway to seamless integration with the powerful [Ollama APIs](https://github.com/ollama/ollama). This library provides developers with a straightforward way to interact with [Ollama APIs](https://github.com/ollama/ollama), enabling rapid development of robust applications in C#.

## Build status
![build and test](https://github.com/Dev-Art-Solutions/OllamaClient/actions/workflows/build-and-test.yml/badge.svg)

## Quick Start

### Installation

Install OllamaClient via NuGet Package Manager Console:

```bash
Install-Package OllamaClient
```

### Example
```
using Microsoft.Extensions.DependencyInjection;
using OllamaClient;
using OllamaClient.Extensions;

var serviceProvider = new ServiceCollection()
          .AddOllamaClient()
          .BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IOllamaHttpClient>();

var pullResult = client.Pull(new OllamaClient.Models.PullRequest()
{
    Name = "mistral",
}, CancellationToken.None);

Console.WriteLine(pullResult.Status);

var models = await client.GetModels(CancellationToken.None);

foreach (var model in models.Models)
{
    Console.WriteLine(model.Name);
}

var result = client.SendChat(new OllamaClient.Models.ChatStreamRequest()
{
    Model = models.Models[0].Name,
    Messages = new List<OllamaClient.Models.Message>() { new OllamaClient.Models.Message()
    {
        Content = "Hello, how are you?",
        Role = "user"
    } }
}, CancellationToken.None);

await foreach (var message in result)
{
    if(message.Message is null) continue;

    Console.Write(message.Message.Content);
}
```

