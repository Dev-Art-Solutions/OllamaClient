namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Copy a model. Creates a model with another name from an existing model.
/// </summary>
public class CopyRequest
{
    /// <summary>
    /// Name of the source model
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = default!;

    /// <summary>
    /// Name of the destination model
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; } = default!;
}
