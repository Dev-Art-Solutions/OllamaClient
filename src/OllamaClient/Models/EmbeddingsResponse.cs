namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Embeddings Response
/// </summary>
public class EmbeddingsResponse
{
    /// <summary>
    /// embedding: the embedding vector
    /// </summary>
    [JsonPropertyName("embedding")]
    public decimal[] Embedding { get; set; } = [];
}

