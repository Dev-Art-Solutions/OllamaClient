namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Details Response
/// </summary>
public class DetailsResponse
{
    /// <summary>
    /// The format
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Format { get; set; }

    /// <summary>
    /// The family
    /// </summary>
    [JsonPropertyName("family")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Family { get; set; }

    /// <summary>
    /// The families
    /// </summary>
    [JsonPropertyName("families")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Families { get; set; }

    /// <summary>
    /// The parameter size
    /// </summary>
    [JsonPropertyName("parameter_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ParameterSize { get; set; }

    /// <summary>
    /// The quantization level
    /// </summary>
    [JsonPropertyName("quantization_level")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? QuantizationLevel { get; set; }

    /// <summary>
    /// parent_model: the model this model was created from
    /// </summary>
    [JsonPropertyName("parent_model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ParentModel { get; set; }

    /// <summary>
    /// context_length: maximum context length the model supports, in tokens
    /// </summary>
    [JsonPropertyName("context_length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ContextLength { get; set; }

    /// <summary>
    /// embedding_length: size of the model's embedding vectors
    /// </summary>
    [JsonPropertyName("embedding_length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EmbeddingLength { get; set; }
}
