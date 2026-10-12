namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Server Status Response Model (<c>GET /api/status</c>)
/// </summary>
public class StatusResponse
{
    /// <summary>
    /// cloud: whether Ollama cloud models are available on this server
    /// </summary>
    [JsonPropertyName("cloud")]
    public CloudStatus Cloud { get; set; } = new();
}

/// <summary>
/// Cloud availability of an Ollama server
/// </summary>
public class CloudStatus
{
    /// <summary>
    /// disabled: true if cloud models are disabled on this server
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    /// <summary>
    /// source: where the disabled setting comes from (e.g. "none", "env", "config")
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }
}
