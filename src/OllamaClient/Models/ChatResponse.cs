namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Chat Response Model
/// </summary>
public class ChatResponse
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
    /// message: the message
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Message? Message { get; set; }

    /// <summary>
    /// The reasoning trace of the response. Ollama returns it on the message (<c>message.thinking</c>), never at the
    /// top level of a chat response, so this reads and writes <see cref="Message.Thinking"/>.
    /// </summary>
    [JsonIgnore]
    [Obsolete("Ollama returns the chat reasoning trace on the message. Use Message.Thinking instead.")]
    public string? Thinking
    {
        get => Message?.Thinking;
        set
        {
            if (Message is not null)
            {
                Message.Thinking = value;
            }
        }
    }

    /// <summary>
    /// done: true if the request is done(used by streaming to indicate the last message)
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; set; }

    /// <summary>
    /// done_reason: why generation stopped, e.g. "stop" (natural end or stop sequence), "length" (num_predict or context reached) or "load"/"unload"
    /// </summary>
    [JsonPropertyName("done_reason")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DoneReason { get; set; }

    /// <summary>
    /// logprobs: log probabilities of the generated tokens; present when the request enabled <c>logprobs</c>
    /// </summary>
    [JsonPropertyName("logprobs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Logprob>? Logprobs { get; set; }

    /// <summary>
    /// remote_model: for a cloud model, the name of the model on the remote host
    /// </summary>
    [JsonPropertyName("remote_model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteModel { get; set; }

    /// <summary>
    /// remote_host: for a cloud model, the host that served the request
    /// </summary>
    [JsonPropertyName("remote_host")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteHost { get; set; }

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
    /// prompt_eval_cached_count: number of prompt tokens served from the KV cache instead of being evaluated
    /// </summary>
    [JsonPropertyName("prompt_eval_cached_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PromptEvalCachedCount { get; set; }

    /// <summary>
    /// prompt_eval_duration: time spent in nanoseconds evaluating the prompt.
    /// Nanoseconds overflow Int32 after ~2.1 seconds, so this must stay a long —
    /// as int? it threw a JsonException on any prompt that took longer than that.
    /// </summary>
    [JsonPropertyName("prompt_eval_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? PromptEvalDuration { get; set; }

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
