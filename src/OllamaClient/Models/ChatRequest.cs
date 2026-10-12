namespace OllamaClient.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Chat Request Model
/// </summary>
public class ChatRequest : StreamingRequest
{
    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;

    /// <summary>
    /// model: (required) the model name
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// messages: the messages of the chat, this can be used to keep a chat memory
    /// </summary>
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = [];

    /// <summary>
    /// format: the format to return a response in. Pass the string "json" for unstructured JSON mode,
    /// or a JSON Schema object (as a <see cref="JsonElement"/>) for structured outputs.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? Format { get; set; }

    /// <summary>
    /// options: additional model parameters listed in the documentation for the Modelfile such as temperature
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OptionsRequest? Options { get; set; }

    /// <summary>
    /// keep_alive: controls how long the model will stay loaded into memory following the request (default: 5m)
    /// </summary>
    [JsonPropertyName("keep_alive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? KeepAlive { get; set; }

    /// <summary>
    /// tools (optional): list of tools the model may call; requires a model that supports tool calling
    /// </summary>
    [JsonPropertyName("tools")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Tool>? Tools { get; set; }

    /// <summary>
    /// think: enables thinking for reasoning models. Either a boolean, or an effort level
    /// ("low", "medium", "high", "max") for models that support it. The trace is returned in <see cref="Message.Thinking"/>.
    /// </summary>
    [JsonPropertyName("think")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ThinkValue? Think { get; set; }

    /// <summary>
    /// truncate: if true (the server default), truncate the chat history from the start when it exceeds the context length; if false, return an error instead
    /// </summary>
    [JsonPropertyName("truncate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Truncate { get; set; }

    /// <summary>
    /// shift: if true (the server default), shift the context window when generation reaches the context length; if false, stop generating instead
    /// </summary>
    [JsonPropertyName("shift")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Shift { get; set; }

    /// <summary>
    /// logprobs: if true, return log probabilities of the generated tokens in <see cref="ChatResponse.Logprobs"/>
    /// </summary>
    [JsonPropertyName("logprobs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Logprobs { get; set; }

    /// <summary>
    /// top_logprobs: number of most likely alternative tokens (0-20) to return at each position; requires <see cref="Logprobs"/>
    /// </summary>
    [JsonPropertyName("top_logprobs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TopLogprobs { get; set; }
}
