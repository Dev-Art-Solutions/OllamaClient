namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Log probability of a single token
/// </summary>
public class TokenLogprob
{
    /// <summary>
    /// token: the text representation of the token
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = default!;

    /// <summary>
    /// logprob: the log probability of this token
    /// </summary>
    [JsonPropertyName("logprob")]
    public double Logprob { get; set; }

    /// <summary>
    /// bytes: the raw byte representation of the token
    /// </summary>
    [JsonPropertyName("bytes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Bytes { get; set; }
}

/// <summary>
/// Log probability of a generated token, with the most likely alternatives at that position.
/// Returned when <c>logprobs</c> is enabled on the request.
/// </summary>
public class Logprob : TokenLogprob
{
    /// <summary>
    /// top_logprobs: the most likely tokens at this position (count set by the request's <c>top_logprobs</c>)
    /// </summary>
    [JsonPropertyName("top_logprobs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TokenLogprob>? TopLogprobs { get; set; }
}
