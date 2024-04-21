namespace OllamaClient;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Models;

public class StatefulConversationOllamaService(IOllamaHttpClient ollamaClient) : IStatefulConversationOllamaService
{
    private readonly ConcurrentDictionary<Guid, List<Message>> Conversations = new();

    public async Task<ChatResponse> SendChat(ConversationChatStreamlessRequest chatRequest, CancellationToken cancellationToken)
    {
        if (!Conversations.TryGetValue(chatRequest.ConversationId, out var messages))
        {
            messages = [];
            Conversations[chatRequest.ConversationId] = messages;
        }

        messages.AddRange(chatRequest.Messages);

        var response = await ollamaClient.SendChat(chatRequest, cancellationToken);

        if (response!.Message != null)
        {
            messages.Add(response.Message);
        }

        return response;
    }

    public async IAsyncEnumerable<ChatResponse> SendChat(ConversationChatStreamRequest chatRequest, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (!Conversations.TryGetValue(chatRequest.ConversationId, out var messages))
        {
            messages = [];
            Conversations[chatRequest.ConversationId] = messages;
        }

        messages.AddRange(chatRequest.Messages);

        await foreach(var response in ollamaClient.SendChat(chatRequest, cancellationToken))
        {
            if (response!.Message != null)
            {
                messages.Add(response!.Message);
            }

            yield return response;
        }
    }
}
