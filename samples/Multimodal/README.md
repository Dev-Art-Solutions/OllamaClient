# Multimodal

Demonstrates vision input: reads an image from disk, base64-encodes it, and sends it alongside a text prompt to a vision-capable model using streaming chat.

**Model required:** `llava` (or another vision model such as `bakllava`).

```bash
ollama pull llava
dotnet run --project samples/Multimodal/Multimodal.csproj -- path/to/image.jpg
```
