namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Create a new model stream request
/// </summary>
public class CreateModelStreamRequest : CreateModelRequest
{
    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => true;
}
