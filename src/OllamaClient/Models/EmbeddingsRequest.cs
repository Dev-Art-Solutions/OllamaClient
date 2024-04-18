namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Embeddings Request Model
/// </summary>
public class EmbeddingsRequest
{
    /// <summary>
    /// model: name of model to generate embeddings from
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// prompt: text to generate embeddings for
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = default!;

    /// <summary>
    /// options: additional model parameters listed in the documentation for the Modelfile such as temperature
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OptionsRequest? Options { get; set; }

    /// <summary>
    /// keep_alive: controls how long the model will stay loaded into memory following the request (default: 5m)
    /// </summary>
    [JsonPropertyName("keep_alive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepAlive { get; set; }
}
