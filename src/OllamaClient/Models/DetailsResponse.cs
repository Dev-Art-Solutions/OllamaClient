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
}
