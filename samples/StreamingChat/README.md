# StreamingChat

Demonstrates token-by-token streaming chat using `IOllamaHttpClient.SendChat` with `ChatStreamRequest`. Tokens are written to the console as they arrive via `IAsyncEnumerable<ChatResponse>`.

**Model required:** `llama3.2` (or any chat model — edit `Program.cs` to change it).

```bash
ollama pull llama3.2
dotnet run --project samples/StreamingChat/StreamingChat.csproj
```
