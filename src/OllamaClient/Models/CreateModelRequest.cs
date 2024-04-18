namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Create a new model request
/// </summary>
public class CreateModelRequest : StreamingRequest
{
    /// <summary>
    /// name: name of the model to create
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// modelfile (optional): contents of the Modelfile
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Modelfile { get; set; }

    /// <summary>
    /// path (optional): path to the Modelfile
    /// </summary>
    [JsonPropertyName("path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Path { get; set; }
}
