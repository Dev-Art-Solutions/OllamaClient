namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Result of upload to a model library
/// </summary>
public class PushResponse
{
    /// <summary>
    /// status: Status of pushed model
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
}
