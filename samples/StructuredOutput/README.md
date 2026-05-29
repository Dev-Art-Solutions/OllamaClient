# StructuredOutput

Demonstrates structured JSON output: passes a JSON Schema to `ChatRequest.Format` so the model returns a strongly typed response, which is then deserialized into a C# record.

**Model required:** `llama3.1` (or any model that supports structured outputs).

```bash
ollama pull llama3.1
dotnet run --project samples/StructuredOutput/StructuredOutput.csproj
```
