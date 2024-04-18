using System.Text.Json.Serialization;

namespace OllamaClient.Models;

/// <summary>
/// Generate Response
/// </summary>
public class GenerateResponse
{
    /// <summary>
    /// model: the model name
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// created_at: the creation time
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// response: empty if the response was streamed, if not streamed, this will contain the full response
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; set; } = default!;

    /// <summary>
    /// done: true if the request is done(used by streaming to indicate the last message)
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; set; }

    /// <summary>
    /// context: an encoding of the conversation used in this response, this can be sent in the next request to keep a conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Context { get; set; }
    /// <summary>
    /// total_duration: time spent generating the response
    /// </summary>
    [JsonPropertyName("total_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TotalDuration { get; set; }

    /// <summary>
    /// load_duration: time spent in nanoseconds loading the model
    /// </summary>
    [JsonPropertyName("load_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LoadDuration { get; set; }

    /// <summary>
    /// prompt_eval_count: number of tokens in the prompt
    /// </summary>
    [JsonPropertyName("prompt_eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PromptEvalCount { get; set; }

    /// <summary>
    /// prompt_eval_duration: time spent in nanoseconds evaluating the prompt
    /// </summary>
    [JsonPropertyName("prompt_eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PromptEvalDuration { get; set; }

    /// <summary>
    /// eval_count: number of tokens the response
    /// </summary>
    [JsonPropertyName("eval_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EvalCount { get; set; }

    /// <summary>
    /// eval_duration: time in nanoseconds spent generating the response
    /// </summary>
    [JsonPropertyName("eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? EvalDuration { get; set; }
}


