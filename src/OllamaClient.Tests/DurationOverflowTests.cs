namespace OllamaClient.Tests;

using System.Text.Json;
using OllamaClient.Models;

/// <summary>
/// Ollama reports every duration in <em>nanoseconds</em>. Int32 tops out at 2,147,483,647,
/// which is only ~2.1 seconds — so <c>prompt_eval_duration</c> typed as <c>int?</c> parsed
/// fine against a warm model and threw
/// <c>JsonException: The JSON value could not be converted to System.Nullable`1[System.Int32]</c>
/// the moment a prompt took longer than that. Cold models overflow it every time.
/// </summary>
public class DurationOverflowTests
{
    // 3.4 seconds in nanoseconds — comfortably past int.MaxValue (~2.147s).
    private const long ColdPromptEvalNanos = 3_400_000_000;

    [Fact]
    public void ChatResponseParsesAPromptEvalDurationBeyondInt32()
    {
        var json = $$"""
        {
          "model": "gemma:2b",
          "message": { "role": "assistant", "content": "hi" },
          "done": true,
          "total_duration": 5000000000,
          "prompt_eval_count": 26,
          "prompt_eval_duration": {{ColdPromptEvalNanos}},
          "eval_count": 12,
          "eval_duration": 1200000000
        }
        """;

        var response = JsonSerializer.Deserialize<ChatResponse>(json);

        Assert.NotNull(response);
        Assert.Equal(ColdPromptEvalNanos, response!.PromptEvalDuration);
        Assert.Equal(26, response.PromptEvalCount);
    }

    [Fact]
    public void GenerateResponseParsesAPromptEvalDurationBeyondInt32()
    {
        var json = $$"""
        {
          "model": "gemma:2b",
          "response": "hi",
          "done": true,
          "prompt_eval_duration": {{ColdPromptEvalNanos}},
          "eval_duration": 1200000000
        }
        """;

        var response = JsonSerializer.Deserialize<GenerateResponse>(json);

        Assert.NotNull(response);
        Assert.Equal(ColdPromptEvalNanos, response!.PromptEvalDuration);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(1_000L)]
    [InlineData(2_147_483_647L)]      // int.MaxValue — the last value the old type survived
    [InlineData(2_147_483_648L)]      // the first one it did not
    [InlineData(600_000_000_000L)]    // ten minutes
    public void PromptEvalDurationRoundTripsAcrossTheInt32Boundary(long nanos)
    {
        var json = $$"""{"prompt_eval_duration": {{nanos}}}""";

        Assert.Equal(nanos, JsonSerializer.Deserialize<ChatResponse>(json)!.PromptEvalDuration);
        Assert.Equal(nanos, JsonSerializer.Deserialize<GenerateResponse>(json)!.PromptEvalDuration);
    }
}
