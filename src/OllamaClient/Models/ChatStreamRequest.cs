namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Streaming Chat Request
/// </summary>
public class ChatStreamRequest : ChatRequest
{
    /// <summary>
    /// stream: Streaming request
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => true;
}
