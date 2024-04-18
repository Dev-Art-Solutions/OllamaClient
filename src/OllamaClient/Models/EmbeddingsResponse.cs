namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Embeddings Response
/// </summary>
public class EmbeddingsResponse
{
    /// <summary>
    /// Embeddings array
    /// </summary>
    [JsonPropertyName("embeddings")]
    public decimal[] Embedding { get; set; } = [];
}

