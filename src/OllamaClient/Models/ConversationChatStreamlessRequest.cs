namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Conversation Streamless Chat Request
/// </summary>
public class ConversationChatStreamlessRequest : ChatRequest
{
    /// <summary>
    /// Conversation Id: It should be unique for each conversation
    /// </summary>
    [JsonIgnore]
    public Guid ConversationId { get; set; }
}
