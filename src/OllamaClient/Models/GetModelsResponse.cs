namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// List Local Models
/// </summary>
public class GetModelsResponse
{
    /// <summary>
    /// List models that are available locally.
    /// </summary>
    [JsonPropertyName("models")]
    public ModelResponse[] Models { get; set; } = [];
}
