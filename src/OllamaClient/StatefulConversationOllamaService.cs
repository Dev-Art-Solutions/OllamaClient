namespace OllamaClient;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

using Models;

/// <inheritdoc cref="IStatefulConversationOllamaService"/>
public class StatefulConversationOllamaService(IOllamaHttpClient ollamaClient) : IStatefulConversationOllamaService
{
    private readonly ConcurrentDictionary<Guid, List<Message>> Conversations = new();

    /// <inheritdoc/>
    public async Task<ChatResponse> SendChat(ConversationChatStreamlessRequest chatRequest, CancellationToken cancellationToken)
    {
        var history = Conversations.GetOrAdd(chatRequest.ConversationId, _ => []);
        var newMessages = chatRequest.Messages;

        // Ollama is stateless: the whole conversation has to go out on every turn.
        chatRequest.Messages = [.. history, .. newMessages];
        ChatResponse response;
        try
        {
            response = await ollamaClient.SendChat(chatRequest, cancellationToken);
        }
        finally
        {
            chatRequest.Messages = newMessages;
        }

        // Only a turn that got an answer joins the history, so a failed call can simply be retried.
        history.AddRange(newMessages);
        if (response.Message != null)
        {
            history.Add(response.Message);
        }

        return response;
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<ChatResponse> SendChat(ConversationChatStreamRequest chatRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var history = Conversations.GetOrAdd(chatRequest.ConversationId, _ => []);
        var newMessages = chatRequest.Messages;

        // Each chunk carries a fragment of the reply; the history gets them joined into one assistant message.
        string? role = null;
        var content = new StringBuilder();
        var thinking = new StringBuilder();
        List<ToolCall>? toolCalls = null;
        var completed = false;

        // The stream serializes the request lazily, on the first MoveNext, so the full history
        // has to stay on the request for the whole enumeration, not just for the call.
        chatRequest.Messages = [.. history, .. newMessages];
        try
        {
            await foreach (var response in ollamaClient.SendChat(chatRequest, cancellationToken))
            {
                if (response.Message != null)
                {
                    role ??= response.Message.Role;
                    content.Append(response.Message.Content);
                    thinking.Append(response.Message.Thinking);
                    if (response.Message.ToolCalls is { Count: > 0 } calls)
                    {
                        (toolCalls ??= []).AddRange(calls);
                    }
                }

                completed |= response.Done;
                yield return response;
            }
        }
        finally
        {
            chatRequest.Messages = newMessages;
        }

        if (completed)
        {
            history.AddRange(newMessages);
            history.Add(new Message
            {
                Role = role ?? "assistant",
                Content = content.ToString(),
                Thinking = thinking.Length > 0 ? thinking.ToString() : null,
                ToolCalls = toolCalls,
            });
        }
    }
}
