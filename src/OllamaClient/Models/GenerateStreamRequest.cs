namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Generate a response for a given prompt with a provided model. This is a streaming endpoint, so there will be a series of responses. The final response object will include statistics and additional data from the request.
/// </summary>
public class GenerateStreamRequest : GenerateRequest
{
    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => true;
}
