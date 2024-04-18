namespace OllamaClient.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Additional model parameters
/// </summary>
public class OptionsRequest
{
    /// <summary>
    /// num_keep
    /// </summary>
    [JsonPropertyName("num_keep")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumKeep { get; set; }

    /// <summary>
    /// seed: Sets the random number seed to use for generation. Setting this to a specific number will make the model generate the same text for the same prompt. (Default: 0)
    /// </summary>
    [JsonPropertyName("seed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Seed { get; set; }

    /// <summary>
    /// num_predict: Maximum number of tokens to predict when generating text. (Default: 128, -1 = infinite generation, -2 = fill context)
    /// </summary>
    [JsonPropertyName("num_predict")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumPredict { get; set; }

    /// <summary>
    /// top_k: Reduces the probability of generating nonsense. A higher value (e.g. 100) will give more diverse answers, while a lower value (e.g. 10) will be more conservative. (Default: 40)
    /// </summary>
    [JsonPropertyName("top_k")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TopK { get; set; }

    /// <summary>
    /// top_p: Works together with top-k. A higher value (e.g., 0.95) will lead to more diverse text, while a lower value (e.g., 0.5) will generate more focused and conservative text. (Default: 0.9)
    /// </summary>
    [JsonPropertyName("top_p")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? TopP { get; set; }

    /// <summary>
    /// tfs_z: Tail free sampling is used to reduce the impact of less probable tokens from the output. A higher value (e.g., 2.0) will reduce the impact more, while a value of 1.0 disables this setting. (default: 1)
    /// </summary>
    [JsonPropertyName("tfs_z")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? TfsZ { get; set; }

    /// <summary>
    /// typical_p
    /// </summary>
    [JsonPropertyName("typical_p")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? TypicalP { get; set; }

    /// <summary>
    /// repeat_last_n: Sets how far back for the model to look back to prevent repetition. (Default: 64, 0 = disabled, -1 = num_ctx)
    /// </summary>
    [JsonPropertyName("repeat_last_n")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? RepeatLastN { get; set; }

    /// <summary>
    /// temperature: The temperature of the model. Increasing the temperature will make the model answer more creatively. (Default: 0.8)
    /// </summary>
    [JsonPropertyName("temperature")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Temperature { get; set; }

    /// <summary>
    /// repeat_penalty: Sets how strongly to penalize repetitions. A higher value (e.g., 1.5) will penalize repetitions more strongly, while a lower value (e.g., 0.9) will be more lenient. (Default: 1.1)
    /// </summary>
    [JsonPropertyName("repeat_penalty")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? RepeatPenalty { get; set; }

    /// <summary>
    /// presence_penalty
    /// </summary>
    [JsonPropertyName("presence_penalty")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? PresencePenalty { get; set; }

    /// <summary>
    /// frequency_penalty
    /// </summary>
    [JsonPropertyName("frequency_penalty")]
    public float? FrequencyPenalty { get; set; }

    /// <summary>
    /// mirostat: Enable Mirostat sampling for controlling perplexity. (default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0)
    /// </summary>
    [JsonPropertyName("mirostat")]
    public int? Mirostat { get; set; }

    /// <summary>
    /// mirostat_tau: Controls the balance between coherence and diversity of the output. A lower value will result in more focused and coherent text. (Default: 5.0)
    /// </summary>
    [JsonPropertyName("mirostat_tau")]
    public float? MirostatTau { get; set; }

    /// <summary>
    /// mirostat_eta: Influences how quickly the algorithm responds to feedback from the generated text. A lower learning rate will result in slower adjustments, while a higher learning rate will make the algorithm more responsive. (Default: 0.1)
    /// </summary>
    [JsonPropertyName("mirostat_eta")]
    public float? MirostatEta { get; set; }

    /// <summary>
    /// penalize_newline
    /// </summary>
    [JsonPropertyName("penalize_newline")]
    public bool? PenalizeNewline { get; set; }

    /// <summary>
    /// stop: Sets the stop sequences to use. When this pattern is encountered the LLM will stop generating text and return. Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile.
    /// </summary>
    [JsonPropertyName("stop")]
    public List<string>? Stop { get; set; }

    /// <summary>
    /// numa: numa enabled
    /// </summary>
    [JsonPropertyName("numa")]
    public bool? Numa { get; set; }

    /// <summary>
    /// num_ctx: Sets the size of the context window used to generate the next token. (Default: 2048)
    /// </summary>
    [JsonPropertyName("num_ctx")]
    public int? NumCtx { get; set; }

    /// <summary>
    /// num_batch
    /// </summary>
    [JsonPropertyName("num_batch")]
    public int? NumBatch { get; set; }

    /// <summary>
    /// num_gqa: The number of GQA groups in the transformer layer. Required for some models, for example it is 8 for llama2:70b
    /// </summary>
    [JsonPropertyName("num_gqa")]
    public int? NumGqa { get; set; }

    /// <summary>
    /// num_gpu: The number of layers to send to the GPU(s). On macOS it defaults to 1 to enable metal support, 0 to disable.
    /// </summary>
    [JsonPropertyName("num_gpu")]
    public int? NumGpu { get; set; }

    /// <summary>
    /// main_gpu
    /// </summary>
    [JsonPropertyName("main_gpu")]
    public int? MainGpu { get; set; }

    /// <summary>
    /// low_vram
    /// </summary>
    [JsonPropertyName("low_vram")]
    public bool? LowVram { get; set; }

    /// <summary>
    /// f16_kv
    /// </summary>
    [JsonPropertyName("f16_kv")]
    public bool? F16Kv { get; set; }

    /// <summary>
    /// vocab_only
    /// </summary>
    [JsonPropertyName("vocab_only")]
    public bool? VocabOnly { get; set; }

    /// <summary>
    /// use_mmap
    /// </summary>
    [JsonPropertyName("use_mmap")]
    public bool? UseMmap { get; set; }

    /// <summary>
    /// use_mlock
    /// </summary>
    [JsonPropertyName("use_mlock")]
    public bool? UseMlock { get; set; }

    /// <summary>
    /// rope_frequency_base
    /// </summary>
    [JsonPropertyName("rope_frequency_base")]
    public float? RopeFrequencyBase { get; set; }

    /// <summary>
    /// rope_frequency_scale
    /// </summary>
    [JsonPropertyName("rope_frequency_scale")]
    public float? RopeFrequencyScale { get; set; }

    /// <summary>
    /// num_thread: Sets the number of threads to use during computation. By default, Ollama will detect this for optimal performance. It is recommended to set this value to the number of physical CPU cores your system has (as opposed to the logical number of cores).
    /// </summary>
    [JsonPropertyName("num_thread")]
    public int? NumThread { get; set; }
}
