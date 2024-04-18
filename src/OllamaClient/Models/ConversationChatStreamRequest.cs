namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Conversation Streaming Chat Request
/// </summary>
public class ConversationChatStreamRequest : ChatStreamRequest
{
    /// <summary>
    /// Conversation Id: It should be unique for each conversation
    /// </summary>
    [JsonIgnore]
    public Guid ConversationId { get; set; }
}
