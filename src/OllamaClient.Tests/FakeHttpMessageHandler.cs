namespace OllamaClient.Tests;

using System.Net;

// FakeHttpMessageHandler to mock HttpClient for testing purposes
public class FakeHttpMessageHandler(HttpResponseMessage fakeResponse) : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(""),
            // Copy fake response details to the actual response
            StatusCode = fakeResponse.StatusCode
        };
        response.Content = fakeResponse.Content;

        var tcs = new TaskCompletionSource<HttpResponseMessage>();
        tcs.SetResult(response);
        return tcs.Task;
    }
}
