namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first.
/// </summary>
public class PushRequest : StreamingRequest
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
    /// insecure: (optional) allow insecure connections to the library. Only use this if you are pushing to your library during development.
    /// </summary>
    [JsonPropertyName("insecure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Insecure { get; set; }

    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;
}
