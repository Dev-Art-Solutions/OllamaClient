using System.Text.Json;
using System.Text.Json.Serialization;

namespace OllamaClient.Models;

/// <summary>
/// Show Model Information
/// </summary>
public class ShowResponse
{
    /// <summary>
    /// modelfile: the contents of the Modelfile
    /// </summary>
    [JsonPropertyName("modelfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Modelfile { get; set; }

    /// <summary>
    /// parameters: parameters of the model
    /// </summary>
    [JsonPropertyName("parameters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parameters { get; set; }

    /// <summary>
    /// template: the prompt template to use (overrides what is defined in the Modelfile)
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// details: model details
    /// </summary>
    [JsonPropertyName("details")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DetailsResponse? Details { get; set; }

    /// <summary>
    /// license: the license of the model
    /// </summary>
    [JsonPropertyName("license")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? License { get; set; }

    /// <summary>
    /// system: the system prompt of the model
    /// </summary>
    [JsonPropertyName("system")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? System { get; set; }

    /// <summary>
    /// model_info: architecture metadata of the model, e.g. "general.architecture" or "llama.context_length"
    /// </summary>
    [JsonPropertyName("model_info")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, JsonElement>? ModelInfo { get; set; }

    /// <summary>
    /// projector_info: metadata of the vision projector, for multimodal models
    /// </summary>
    [JsonPropertyName("projector_info")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, JsonElement>? ProjectorInfo { get; set; }

    /// <summary>
    /// capabilities: what the model can do, e.g. "completion", "tools", "thinking", "vision", "embedding"
    /// </summary>
    [JsonPropertyName("capabilities")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Capabilities { get; set; }

    /// <summary>
    /// messages: messages the model seeds every conversation with
    /// </summary>
    [JsonPropertyName("messages")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Message>? Messages { get; set; }

    /// <summary>
    /// renderer: the built-in prompt renderer the model uses instead of a template, if any
    /// </summary>
    [JsonPropertyName("renderer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Renderer { get; set; }

    /// <summary>
    /// parser: the built-in output parser the model uses, if any
    /// </summary>
    [JsonPropertyName("parser")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parser { get; set; }

    /// <summary>
    /// modified_at: when the model was last modified
    /// </summary>
    [JsonPropertyName("modified_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// remote_model: for a cloud model, the name of the model on the remote host
    /// </summary>
    [JsonPropertyName("remote_model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteModel { get; set; }

    /// <summary>
    /// remote_host: for a cloud model, the host that serves it
    /// </summary>
    [JsonPropertyName("remote_host")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteHost { get; set; }
}
