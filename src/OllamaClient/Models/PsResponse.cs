namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Response model for <c>GET /api/ps</c> (list running models).
/// </summary>
public class PsResponse
{
    /// <summary>
    /// models: list of models currently loaded in memory
    /// </summary>
    [JsonPropertyName("models")]
    public List<RunningModelResponse> Models { get; set; } = [];
}
