namespace OllamaClient.Tests;

using Moq;

using OllamaClient.Models;

public class StatefulConversationOllamaServiceTests
{
    [Fact]
    public async Task SendChat_AddsMessageToConversation()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        var ollamaClientMock = new Mock<IOllamaHttpClient>();
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var chatRequest = new ConversationChatStreamlessRequest
        {
            ConversationId = conversationId,
            Messages = [new Message { Content = "Hello" }]
        };

        var expectedResponse = new ChatResponse { Message = new Message { Content = "Response 1" } };
        var cancellationToken = CancellationToken.None;
        ollamaClientMock.Setup(x => x.SendChat(chatRequest, cancellationToken))
              .ReturnsAsync(expectedResponse);

        // Act
        var response = await service.SendChat(chatRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task SendChatStream_AddsMessageToConversation()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        var ollamaClientMock = new Mock<IOllamaHttpClient>();
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var chatRequest = new ConversationChatStreamRequest
        {
            ConversationId = conversationId,
            Messages = [new Message { Content = "Hello" }]
        };
        var cancellationToken = CancellationToken.None;
        var expectedResponse = new List<ChatResponse>
        {
            new() { Message = new Message { Content = "Response 1" } },
            new() { Message = new Message { Content = "Response 2" } }
        };

        ollamaClientMock
            .Setup(x => x.SendChat(chatRequest, cancellationToken))
            .Returns(ToAsync(expectedResponse));

        // Act & Assert
        int i = 0;
        await foreach (var response in service.SendChat(chatRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse[i++], response);
        }
    }

    [Fact]
    public async Task SendChat_SendsTheWholeHistoryOnTheNextTurn()
    {
        var ollamaClientMock = new Mock<IOllamaHttpClient>();
        var sent = new List<List<string>>();
        ollamaClientMock
            .Setup(x => x.SendChat(It.IsAny<ChatRequest>(), It.IsAny<CancellationToken>()))
            .Callback<ChatRequest, CancellationToken>((r, _) => sent.Add(r.Messages.Select(m => m.Content).ToList()))
            .ReturnsAsync(() => new ChatResponse { Message = new Message { Role = "assistant", Content = $"Answer {sent.Count}" } });
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var id = Guid.NewGuid();

        await service.SendChat(new ConversationChatStreamlessRequest { ConversationId = id, Messages = [new Message { Role = "user", Content = "My name is Ada." }] }, default);
        var second = new ConversationChatStreamlessRequest { ConversationId = id, Messages = [new Message { Role = "user", Content = "What is my name?" }] };
        await service.SendChat(second, default);

        Assert.Equal(["My name is Ada.", "Answer 1", "What is my name?"], sent[1]);
        // The caller's request is left as it was handed in.
        Assert.Equal("What is my name?", Assert.Single(second.Messages).Content);
    }

    [Fact]
    public async Task SendChat_DoesNotRecordATurnThatFailed()
    {
        var ollamaClientMock = new Mock<IOllamaHttpClient>();
        var sent = new List<List<string>>();
        var calls = 0;
        ollamaClientMock
            .Setup(x => x.SendChat(It.IsAny<ChatRequest>(), It.IsAny<CancellationToken>()))
            .Callback<ChatRequest, CancellationToken>((r, _) => sent.Add(r.Messages.Select(m => m.Content).ToList()))
            .Returns(() => ++calls == 1
                ? Task.FromException<ChatResponse>(new HttpRequestException("down"))
                : Task.FromResult(new ChatResponse { Message = new Message { Role = "assistant", Content = "ok" } }));
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var request = new ConversationChatStreamlessRequest { ConversationId = Guid.NewGuid(), Messages = [new Message { Role = "user", Content = "hi" }] };

        await Assert.ThrowsAsync<HttpRequestException>(() => service.SendChat(request, default));
        await service.SendChat(request, default);

        // The retry carries the user message once, not once per attempt.
        Assert.Equal(["hi"], sent[1]);
    }

    [Fact]
    public async Task SendChatStream_SendsTheHistoryAndRecordsTheReplyAsOneMessage()
    {
        var ollamaClientMock = new Mock<IOllamaHttpClient>();
        var sent = new List<List<string>>();
        ollamaClientMock
            .Setup(x => x.SendChat(It.IsAny<ChatStreamRequest>(), It.IsAny<CancellationToken>()))
            .Returns<ChatStreamRequest, CancellationToken>((r, _) => Capture(r, sent));
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var id = Guid.NewGuid();

        await foreach (var _ in service.SendChat(new ConversationChatStreamRequest { ConversationId = id, Messages = [new Message { Role = "user", Content = "My name is Ada." }] }, default))
        {
        }

        await foreach (var _ in service.SendChat(new ConversationChatStreamRequest { ConversationId = id, Messages = [new Message { Role = "user", Content = "What is my name?" }] }, default))
        {
        }

        // The three streamed chunks of the first reply were stored as a single assistant message.
        Assert.Equal(["My name is Ada.", "Hello there.", "What is my name?"], sent[1]);
    }

    private static async IAsyncEnumerable<ChatResponse> Capture(ChatRequest request, List<List<string>> sent)
    {
        // Read the messages when enumeration starts, as the real client does when it serializes the body.
        await Task.Yield();
        sent.Add(request.Messages.Select(m => m.Content).ToList());
        yield return new ChatResponse { Message = new Message { Role = "assistant", Content = "Hello" } };
        yield return new ChatResponse { Message = new Message { Role = "assistant", Content = " there" } };
        yield return new ChatResponse { Message = new Message { Role = "assistant", Content = "." }, Done = true };
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<ChatResponse> ToAsync(List<ChatResponse> list)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        foreach (var item in list)
            yield return item;
    }
}
