namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Streamless Chat Request
/// </summary>
public class ChatStreamlessRequest : ChatRequest
{
    /// <summary>
    /// stream: Not stream request
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;
}
