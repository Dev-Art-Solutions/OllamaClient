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
        var ollamaClientMock = new Mock<IClient>();
        var service = new StatefulConversationOllamaService(ollamaClientMock.Object);
        var chatRequest = new ConversationChatStreamlessRequest
        {
            ConversationId = conversationId,
            Messages = [new Message { Content = "Hello" }]
        };

        var expectedResponse = new ChatResponse { Message = new Message { Content = "Response 1" } };
        var cancellationToken = CancellationToken.None;
        ollamaClientMock.Setup(x=> x.SendChat(chatRequest, cancellationToken))
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
        var ollamaClientMock = new Mock<IClient>();
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
            .Setup(x => x.SendChatStreaming(chatRequest, cancellationToken))
            .Returns(ToAsync(expectedResponse));

        // Act & Assert
        int i = 0;
        await foreach (var response in service.SendChatStream(chatRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse[i++], response);
        }
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<ChatResponse> ToAsync(List<ChatResponse> list)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        foreach (var item in list)
            yield return item;
    }
}
