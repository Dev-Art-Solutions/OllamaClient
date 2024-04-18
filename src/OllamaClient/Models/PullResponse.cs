namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Status of pulled model from the ollama library.
/// </summary>
public class PullResponse
{
    /// <summary>
    /// status: Status of pulled model
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;

    /// <summary>
    /// digest: the expected SHA256 digest of the file
    /// </summary>
    [JsonPropertyName("digest")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Digest { get; set; }

    /// <summary>
    /// total: the total number of bytes of the model
    /// </summary>
    [JsonPropertyName("total")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Total { get; set; }

    /// <summary>
    /// completed: complated number of bytes of the model
    /// </summary>
    [JsonPropertyName("completed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Completed { get; set; }
}

