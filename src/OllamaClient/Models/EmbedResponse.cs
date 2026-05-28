namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Response model for the <c>POST /api/embed</c> endpoint.
/// </summary>
public class EmbedResponse
{
    /// <summary>
    /// model: the model that produced the embeddings
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// embeddings: one float array per input text
    /// </summary>
    [JsonPropertyName("embeddings")]
    public List<List<float>> Embeddings { get; set; } = [];

    /// <summary>
    /// total_duration: time spent generating the embeddings (nanoseconds)
    /// </summary>
    [JsonPropertyName("total_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TotalDuration { get; set; }

    /// <summary>
    /// load_duration: time spent loading the model (nanoseconds)
    /// </summary>
    [JsonPropertyName("load_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LoadDuration { get; set; }

    /// <summary>
    /// prompt_eval_count: number of tokens in the input
    /// </summary>
    [JsonPropertyName("prompt_eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PromptEvalCount { get; set; }
}
