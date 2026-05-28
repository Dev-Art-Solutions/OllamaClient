namespace OllamaClient.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Request model for the <c>POST /api/embed</c> endpoint.
/// </summary>
public class EmbedRequest
{
    /// <summary>
    /// model: name of the model to generate embeddings from
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// input: text or list of texts to embed. Use <see cref="JsonSerializer.SerializeToElement"/> to build the value:
    /// <code>
    /// Input = JsonSerializer.SerializeToElement("single string")
    /// Input = JsonSerializer.SerializeToElement(new[] { "text1", "text2" })
    /// </code>
    /// </summary>
    [JsonPropertyName("input")]
    public JsonElement Input { get; set; }

    /// <summary>
    /// truncate: truncates the end of each input to fit within the model context length (default true)
    /// </summary>
    [JsonPropertyName("truncate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Truncate { get; set; }

    /// <summary>
    /// options: additional model parameters such as temperature
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OptionsRequest? Options { get; set; }

    /// <summary>
    /// keep_alive: controls how long the model stays loaded into memory (default: 5m)
    /// </summary>
    [JsonPropertyName("keep_alive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepAlive { get; set; }
}
