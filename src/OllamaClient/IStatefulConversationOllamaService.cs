namespace OllamaClient;

using Models;

/// <summary>
/// Stateful conversation Ollama service
/// </summary>
public interface IStatefulConversationOllamaService
{
    /// <summary>
    /// Send message for conversation and get streamless response
    /// </summary>
    /// <param name="chatRequest">Conversation chat request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response for conversation</returns>
    Task<ChatResponse> SendChat(ConversationChatStreamlessRequest chatRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Send message for conversation and get stream response
    /// </summary>
    /// <param name="chatRequest">Conversation chat request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Stream response for conversation</returns>
    IAsyncEnumerable<ChatResponse> SendChatStream(ConversationChatStreamRequest chatRequest, CancellationToken cancellationToken);
}
