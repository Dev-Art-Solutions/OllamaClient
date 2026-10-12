namespace OllamaClient.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Generate a response for a given prompt with a provided model.
/// </summary>
public class GenerateRequest : StreamingRequest
{
    /// <summary>
    /// model: (required) the model name
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; } = default!;

    /// <summary>
    /// prompt: the prompt to generate a response for
    /// </summary>
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = default!;

    /// <summary>
    /// raw: if true no formatting will be applied to the prompt. You may choose to use the raw parameter if you are specifying a full templated prompt in your request to the API
    /// </summary>
    [JsonPropertyName("raw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Raw { get; set; }

    /// <summary>
    /// format: the format to return a response in. Pass the string "json" for unstructured JSON mode,
    /// or a JSON Schema object (as a <see cref="JsonElement"/>) for structured outputs.
    /// Note: instruct the model to use JSON in the prompt; otherwise it may generate excessive whitespace.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? Format { get; set; }

    /// <summary>
    /// images: (optional) a list of base64-encoded images (for multimodal models such as llava)
    /// </summary>
    [JsonPropertyName("images")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Images { get; set; }

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
    /// system: system message to (overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? System { get; set; }

    /// <summary>
    /// template: the prompt template to use (overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// suffix: the text to insert after the model's response (fill-in-the-middle)
    /// </summary>
    [JsonPropertyName("suffix")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Suffix { get; set; }

    /// <summary>
    /// context: token IDs returned from a previous /generate response, used to keep a short conversational memory
    /// </summary>
    [JsonPropertyName("context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Context { get; set; }

    /// <summary>
    /// think: enables thinking for reasoning models. Either a boolean, or an effort level
    /// ("low", "medium", "high", "max") for models that support it. The trace is returned in <see cref="GenerateResponse.Thinking"/>.
    /// </summary>
    [JsonPropertyName("think")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ThinkValue? Think { get; set; }

    /// <summary>
    /// truncate: if true (the server default), truncate the prompt from the start when it exceeds the context length; if false, return an error instead
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
    /// logprobs: if true, return log probabilities of the generated tokens in <see cref="GenerateResponse.Logprobs"/>
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

    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;
}

