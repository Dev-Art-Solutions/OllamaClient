namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first.
/// </summary>
public class PushRequest : StreamingRequest
{
    /// <summary>
    /// name: (required) the model name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// insecure: (optional) allow insecure connections to the library. Only use this if you are pushing to your library during development.
    /// </summary>
    [JsonPropertyName("insecure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Insecure { get; set; }
}
