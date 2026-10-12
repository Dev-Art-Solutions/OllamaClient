namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Download a model from the ollama library. Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.
/// </summary>
public class PullRequest : StreamingRequest
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
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;
}
