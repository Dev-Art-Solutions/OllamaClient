# ToolCalling

Demonstrates function calling (tool use): defines a `get_current_weather` tool, sends a first chat turn that triggers the tool, simulates the tool result, then sends a second turn to get a natural-language answer.

**Model required:** `llama3.1` or another tool-capable model (e.g. `mistral-nemo`).

```bash
ollama pull llama3.1
dotnet run --project samples/ToolCalling/ToolCalling.csproj
```
