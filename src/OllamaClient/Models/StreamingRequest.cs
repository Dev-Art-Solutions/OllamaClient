namespace OllamaClient.Models;

using System.Text.Json.Serialization;

public abstract class StreamingRequest
{
    /// <summary>
    /// stream: (optional) if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Stream { get; set; }
}
