# Embeddings

Demonstrates generating vector embeddings via `IOllamaHttpClient.Embed` — both a single string and a batch of strings using the `/api/embed` endpoint.

**Model required:** `nomic-embed-text` (or any embedding model — edit `Program.cs` to change it).

```bash
ollama pull nomic-embed-text
dotnet run --project samples/Embeddings/Embeddings.csproj
```
