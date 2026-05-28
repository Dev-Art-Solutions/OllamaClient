namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Information about a model that is currently loaded into memory.
/// </summary>
public class RunningModelResponse
{
    /// <summary>
    /// name: model name including tag
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// model: model name
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// size: model size in bytes on disk
    /// </summary>
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// digest: model digest
    /// </summary>
    [JsonPropertyName("digest")]
    public string Digest { get; set; } = default!;

    /// <summary>
    /// details: model details
    /// </summary>
    [JsonPropertyName("details")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DetailsResponse? Details { get; set; }

    /// <summary>
    /// expires_at: when the model will be unloaded from memory
    /// </summary>
    [JsonPropertyName("expires_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// size_vram: VRAM consumed by the model in bytes
    /// </summary>
    [JsonPropertyName("size_vram")]
    public long SizeVram { get; set; }
}
