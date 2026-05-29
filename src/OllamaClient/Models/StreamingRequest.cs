namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Base class for requests that support both streaming and non-streaming response modes.
/// </summary>
public abstract class StreamingRequest
{
    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public abstract bool Stream { get; }
}
