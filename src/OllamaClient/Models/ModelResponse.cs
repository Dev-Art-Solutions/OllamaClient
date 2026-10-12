namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Information about a model
/// </summary>
public class ModelResponse
{
    /// <summary>
    /// name: model name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// modified_at: last modification date
    /// </summary>
    [JsonPropertyName("modified_at")]
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// size: model size in bytes
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// digest: the expected SHA256 digest of the file
    /// </summary>
    [JsonPropertyName("digest")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Digest { get; set; }

    /// <summary>
    /// details: model details
    /// </summary>
    [JsonPropertyName("details")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DetailsResponse? Details { get; set; }

    /// <summary>
    /// model: the model name
    /// </summary>
    [JsonPropertyName("model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Model { get; set; }

    /// <summary>
    /// capabilities: what the model can do, e.g. "completion", "tools", "thinking", "vision", "embedding"
    /// </summary>
    [JsonPropertyName("capabilities")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Capabilities { get; set; }

    /// <summary>
    /// remote_model: for a cloud model, the name of the model on the remote host
    /// </summary>
    [JsonPropertyName("remote_model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteModel { get; set; }

    /// <summary>
    /// remote_host: for a cloud model, the host that serves it
    /// </summary>
    [JsonPropertyName("remote_host")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteHost { get; set; }
}
