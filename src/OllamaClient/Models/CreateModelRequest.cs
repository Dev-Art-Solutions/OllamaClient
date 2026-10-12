namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Create a new model request. Create from an existing model with <see cref="From"/>, or from GGUF/safetensors
/// files uploaded beforehand with <see cref="IOllamaHttpClient.PushBlob"/> and listed in <see cref="Files"/>.
/// </summary>
public class CreateModelRequest : StreamingRequest
{
    /// <summary>
    /// model: name of the model to create
    /// </summary>
    [JsonPropertyName("model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Model { get; set; }

    /// <summary>
    /// name: legacy alias of <see cref="Model"/>; the server uses it only when <see cref="Model"/> is empty
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// from (optional): name of an existing model to create the new model from
    /// </summary>
    [JsonPropertyName("from")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? From { get; set; }

    /// <summary>
    /// files (optional): file name to SHA256 blob digest (e.g. "sha256:...") of the GGUF or safetensors files to create the model from
    /// </summary>
    [JsonPropertyName("files")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Files { get; set; }

    /// <summary>
    /// template (optional): the prompt template for the model
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// license (optional): the license text of the model
    /// </summary>
    [JsonPropertyName("license")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? License { get; set; }

    /// <summary>
    /// system (optional): the system prompt for the model
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? System { get; set; }

    /// <summary>
    /// parameters (optional): default model parameters, e.g. <c>{ "temperature": 0.2, "num_ctx": 8192 }</c>
    /// </summary>
    [JsonPropertyName("parameters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// messages (optional): messages to seed every conversation with
    /// </summary>
    [JsonPropertyName("messages")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Message>? Messages { get; set; }

    /// <summary>
    /// quantize (optional): quantize a non-quantized (e.g. float16) model, e.g. "q4_K_M" or "q8_0"
    /// </summary>
    [JsonPropertyName("quantize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Quantize { get; set; }

    /// <summary>
    /// modelfile: contents of the Modelfile. Ollama no longer accepts this field: a request
    /// carrying only a Modelfile fails with "neither 'from' or 'files' was specified".
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Obsolete("Ollama no longer accepts a Modelfile on /api/create. Use From, Files, Template, System, Parameters etc. instead.")]
    public string? Modelfile { get; set; }

    /// <summary>
    /// path: path to a Modelfile on the server. Ollama no longer accepts this field.
    /// </summary>
    [JsonPropertyName("path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Obsolete("Ollama no longer accepts a Modelfile path on /api/create. Use From or Files instead.")]
    public string? Path { get; set; }

    /// <summary>
    /// stream: if false the response will be returned as a single response object, rather than a stream of objects
    /// </summary>
    [JsonPropertyName("stream")]
    public override bool Stream => false;
}
