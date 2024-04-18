namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Delete a model and its data.
/// </summary>
public class DeleteRequest
{
    /// <summary>
    /// name: model name to delete
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
