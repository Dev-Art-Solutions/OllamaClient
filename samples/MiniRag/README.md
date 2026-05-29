# MiniRag

Demonstrates a minimal Retrieval-Augmented Generation (RAG) pipeline: embed a small knowledge base, find the most relevant chunk via cosine similarity, then pass it as context to the LLM.

**Models required:** `nomic-embed-text` (embeddings) and `llama3.2` (chat).

```bash
ollama pull nomic-embed-text && ollama pull llama3.2
dotnet run --project samples/MiniRag/MiniRag.csproj
```
