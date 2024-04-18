namespace OllamaClient.Models;

using System.Text.Json.Serialization;

public class PullRequest : StreamingRequest
{
    /// <summary>
    /// name: (required) the model name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
