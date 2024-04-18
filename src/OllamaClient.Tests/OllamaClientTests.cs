namespace OllamaClient.Tests;

using System.Net;
using System.Net.Http.Headers;
using Moq;
using Newtonsoft.Json;

using OllamaClient;
using OllamaClient.Configuration;
using OllamaClient.Models;

public class OllamaClientTests
{
    [Fact]
    public async Task GetModels_ReturnsModels()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new Configuration.OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new Models.GetModelsResponse
        {
            Models = [ new Models.ModelResponse()
            {
                Name = "Test",
            }]
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);

        // Act
        var response = await ollamaClient.GetModels(default);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task Copy_ReturnsCopyResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new CopyResponse { IsSuccessful = true };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var copyRequest = new CopyRequest();
        var cancellationToken = default(CancellationToken);

        // Act
        var response = await ollamaClient.Copy(copyRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task GetModels_ThrowsException_WhenHttpClientThrowsException()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var httpClient = new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.InternalServerError)));
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await ollamaClient.GetModels(default));
    }

    [Fact]
    public async Task Copy_ThrowsException_WhenResponseIsNotSuccessStatusCode()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.BadRequest));
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var copyRequest = new CopyRequest();
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await ollamaClient.Copy(copyRequest, cancellationToken));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Create_ReturnsValidResponse(bool stream)
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new CreateModelResponse
        {
        };

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
        };
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(stream ? "application/x-ndjson" : "text/plain");

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var createRequest = new CreateModelRequest
        {
            Stream = stream,
            Modelfile = "file",
            Path = "path"
        };
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        await foreach (var response in ollamaClient.Create(createRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse, response);
        }
    }

    [Fact]
    public async Task Delete_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new DeleteResponse
        {
            IsSuccessful = true
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var deleteRequest = new DeleteRequest();
        var cancellationToken = default(CancellationToken);

        // Act
        var response = await ollamaClient.Delete(deleteRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task GetEmbeddings_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new EmbeddingsResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var embeddingsRequest = new EmbeddingsRequest()
        {
            KeepAlive = "5m",
            Model = "model",
            Options = new OptionsRequest(),
            Prompt = "prompt",
        };
        var cancellationToken = default(CancellationToken);

        // Act
        var response = await ollamaClient.GetEmbeddings(embeddingsRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task Generate_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new GenerateResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var generateRequest = new GenerateRequest()
        {
            Stream = false,
            System = "System",
            Context = "context",
            Format = "json",
            Images = ["image"],
            KeepAlive = "5m",
            Model = "model",
            Options = new OptionsRequest(),
            Prompt = "prompt",
            Raw = false,
            Template = "template"
        };
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        await foreach (var response in ollamaClient.Generate(generateRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse, response);
        }
    }

    [Fact]
    public async Task Pull_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new PullResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var pullRequest = new PullRequest();
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        await foreach (var response in ollamaClient.Pull(pullRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse, response);
        }
    }

    [Fact]
    public async Task Push_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new PushResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var pushRequest = new PushRequest()
        {
            Name = "test",
            Stream = false,
            Insecure = false,
        };
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        await foreach (var response in ollamaClient.Push(pushRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse, response);
        }
    }

    [Fact]
    public async Task Push_ShouldThrow_WhenMediaTypeIsWrong()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new PushResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var pushRequest = new PushRequest()
        {
            Name = "test",
            Stream = true,
            Insecure = false,
        };
        var cancellationToken = default(CancellationToken);

        // Act & Assert

        await Assert.ThrowsAsync<NotSupportedException>(async () =>
        {
            await foreach (var response in ollamaClient.Push(pushRequest, cancellationToken))
            {
            }
        });
    }

    [Fact]
    public async Task SendChat_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new ChatResponse
        {
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var chatRequest = new ChatStreamlessRequest()
        {
            Format = "format",
            KeepAlive = "5m",
            Messages = [new() { Content = "content" }],
            Model = "model",
            Options = new OptionsRequest()
            {
                Seed = 1,
                Stop = ["test"],
                RopeFrequencyScale = 1,
                F16Kv = true,
                FrequencyPenalty = 1,
                PresencePenalty = 1,
                Temperature = 1,
                TopP = 1,
                TopK = 1,
                LowVram = true,
                MainGpu = 1,
                Mirostat = 1,
                MirostatEta = 1,
                MirostatTau = 1,
                Numa = false,
                NumBatch = 1,
                NumCtx = 1,
                NumGpu = 1,
                NumGqa = 1,
                NumKeep = 1,
                NumPredict = 1,
                NumThread = 1,
                PenalizeNewline = true,
                RepeatLastN = 1,
                RepeatPenalty = 1,
                RopeFrequencyBase = 1,
                TfsZ = 1,
                TypicalP = 1,
                UseMlock = true,
                UseMmap = true,
                VocabOnly = true
            },
        };
        var cancellationToken = default(CancellationToken);

        // Act
        var response = await ollamaClient.SendChat(chatRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
        Assert.False(chatRequest.Stream);
    }

    [Fact]
    public async Task SendChatStreaming_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new ChatResponse
        {
            Message = new Message()
            {
                Content = "content",
                Role = "Bot",
                Images = ["image"]
            }
        };

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
        };
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-ndjson");

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var chatRequest = new ChatStreamRequest();
        var cancellationToken = default(CancellationToken);

        // Act & Assert
        Assert.True(chatRequest.Stream);
        await foreach (var response in ollamaClient.SendChatStreaming(chatRequest, cancellationToken))
        {
            Assert.NotNull(response);
            Assert.Equivalent(expectedResponse, response);
        }
    }

    [Fact]
    public async Task Show_ReturnsValidResponse()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configuration = new OllamaConfiguration
        {
            OllamaEndpoint = "http://example.com/"
        };

        var expectedResponse = new ShowResponse
        {
            Details = new DetailsResponse()
            {
                ParameterSize = "size",
                Families = ["families"],
                Family = "family",
                Format = "format",
                QuantizationLevel = "level"
            }
        };

        var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(expectedResponse)) });
        var httpClient = new HttpClient(fakeHttpMessageHandler);
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var ollamaClient = new Client(httpClientFactoryMock.Object, configuration);
        var showRequest = new ShowRequest();
        var cancellationToken = default(CancellationToken);

        // Act
        var response = await ollamaClient.Show(showRequest, cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equivalent(expectedResponse, response);
    }
}