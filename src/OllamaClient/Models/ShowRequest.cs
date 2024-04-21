namespace OllamaClient.Models;

using System.Text.Json.Serialization;

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
