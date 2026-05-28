namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Response model for <c>GET /api/version</c>.
/// </summary>
public class VersionResponse
{
    /// <summary>
    /// version: the Ollama server version string
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = default!;
}
