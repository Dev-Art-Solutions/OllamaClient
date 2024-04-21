namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Upload a model to a model library. Requires registering for ollama.ai and adding a public key first.
/// </summary>
public class PushStreamRequest : PushRequest
{
    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => true;
}
