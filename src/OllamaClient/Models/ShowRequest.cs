using System.Text.Json.Serialization;

namespace OllamaClient.Models;

/// <summary>
/// Show information request
/// </summary>
public class ShowRequest
{
    /// <summary>
    /// name: name of the model to show
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
