namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Show information request
/// </summary>
public class ShowRequest
{
    /// <summary>
    /// model: name of the model
    /// </summary>
    [JsonPropertyName("model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Model { get; set; }

    /// <summary>
    /// name: legacy alias of <see cref="Model"/>; the server uses it only when <see cref="Model"/> is empty
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// verbose (optional): if true, include the full tensor and tokenizer data in <see cref="ShowResponse.ModelInfo"/>
    /// </summary>
    [JsonPropertyName("verbose")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Verbose { get; set; }
}
