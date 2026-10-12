namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Delete a model and its data.
/// </summary>
public class DeleteRequest
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
}
