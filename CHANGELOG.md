# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.1.0] - 2026-10-12

Brings the client in line with the Ollama API as of v0.40.3. Every addition was checked against a live
Ollama server, not only against fixtures.

### Added
- `GetStatus()` → `GET /api/status` (whether cloud models are available).
- `BlobExists(digest)` / `PushBlob(digest, stream)` → `HEAD`/`POST /api/blobs/:digest`, needed to create a model from GGUF or safetensors files.
- `CreateModelRequest`: `Model`, `From`, `Files`, `Template`, `License`, `System`, `Parameters`, `Messages`, `Quantize`, the fields `/api/create` takes now.
- `Think` now also takes an effort level: `Think = ThinkValue.High` (or `"low"`, `"medium"`, `"max"`). `Think = true` still compiles.
- Token log probabilities: `Logprobs` / `TopLogprobs` on `ChatRequest` and `GenerateRequest`; `Logprobs` on the responses.
- `Truncate` and `Shift` on `ChatRequest` and `GenerateRequest`; `Dimensions` on `EmbedRequest`.
- Responses: `DoneReason`, `PromptEvalCachedCount`, `RemoteModel`, `RemoteHost`; `GenerateResponse.ToolCalls`.
- `Message.Thinking`, `Message.ToolName`, `Message.ToolCallId`; `ToolCall.Id`; `FunctionCall.Index`.
- `ShowRequest.Verbose`; `ShowResponse`: `License`, `System`, `ModelInfo`, `ProjectorInfo`, `Capabilities`, `Messages`, `Renderer`, `Parser`, `ModifiedAt`, `RemoteModel`, `RemoteHost`.
- `ModelResponse`: `Model`, `Capabilities`, `RemoteModel`, `RemoteHost`; `DetailsResponse`: `ParentModel`, `ContextLength`, `EmbeddingLength`; `RunningModelResponse.ContextLength`; `PushResponse.Completed`.
- `OptionsRequest.MinP`, `OptionsRequest.DraftNumPredict`.
- `Model` on `DeleteRequest`, `ShowRequest`, `PullRequest` and `PushRequest`. Ollama has deprecated `name`, but the server still falls back to it, so `Name` keeps working.

### Fixed
- `GetEmbeddings()` always returned an empty vector: `/api/embeddings` answers with `embedding`, and the client read `embeddings`.
- `ChatResponse.Thinking` was always null: Ollama returns the chat reasoning trace on the message. The property is now obsolete and forwards to `Message.Thinking`.
- `Create()` with a `Modelfile` fails on any current Ollama with "neither 'from' or 'files' was specified". `Modelfile` and `Path` are now obsolete and point to the new fields.
- `GetModels()` ignored its cancellation token while reading the body.
- `OptionsRequest` sent unset options as explicit `null`s; they are now omitted.

### Changed
- `GenerateRequest.Think` / `ChatRequest.Think` are now `ThinkValue?` instead of `bool?`. Source compatible for assignment, but code compiled against 2.0.x needs to be recompiled.
- `IOllamaHttpClient` gained `GetStatus`, `BlobExists` and `PushBlob`, so custom implementations of the interface must add them.

### Deprecated
- `OptionsRequest` members that Ollama no longer recognizes and ignores: `TfsZ`, `Mirostat`, `MirostatTau`, `MirostatEta`, `PenalizeNewline`, `Numa`, `NumGqa`, `LowVram`, `F16Kv`, `VocabOnly`, `UseMlock`, `RopeFrequencyBase`, `RopeFrequencyScale`.

## [2.0.1] - 2026-07-13

### Fixed
- `ChatResponse.PromptEvalDuration` and `GenerateResponse.PromptEvalDuration` were typed
  `int?`, but Ollama reports durations in **nanoseconds** — Int32 overflows after ~2.1
  seconds. Any prompt that took longer to evaluate (which a cold model always does) failed
  to deserialize with `JsonException: The JSON value could not be converted to
  System.Nullable\`1[System.Int32]. Path: $.prompt_eval_duration`, so the whole response was
  lost even though the model had answered correctly. Both are now `long?`, matching every
  other duration field on those types.

## [2.0.0] - 2026-05-31

### Added
- Multi-target support for `net9.0` and `net10.0`; `net8.0` support dropped ([2f344d0]).
- `IStatefulConversationOllamaService` / `StatefulConversationOllamaService`: higher-level service that automatically manages conversation history for multi-turn chat sessions ([a57d195]).
- Project-level `NuGet.Config` scoped to `nuget.org`, preventing authentication errors from unrelated private feeds ([5f53cab]).
- `CHANGELOG.md` (this file) ([de42221]).
- Tool/function calling support: `Tool`, `FunctionDefinition`, `FunctionCall`, and `ToolCall` models; `ChatRequest.Tools` and `ChatRequest.ToolChoice` properties ([920c144]).
- Structured outputs: the `format` property on `GenerateRequest` and `ChatRequest` now accepts a JSON Schema object in addition to the `"json"` string ([dd23e60]).
- Multimodal/vision support: `Message.Images` accepts a list of base-64-encoded image strings for vision models ([f3506f4]).
- `/api/embed` endpoint via `Embed(EmbedRequest)` / `EmbedAsync(EmbedRequest)` — supports batch embedding, the modern alternative to `/api/embeddings` ([7ae481d]).
- `think` parameter on generate/chat requests and `Thinking` field on responses for reasoning models ([b3d3199]).
- `GenerateRequest.Suffix` for fill-in-the-middle generation ([b3d3199]).
- `GET /api/version` → `GetVersion()` returning `VersionResponse` ([f4a69d9]).
- `GET /api/ps` → `GetRunningModels()` returning `PsResponse` ([f4a69d9]).
- XML documentation on all public types and members; documentation XML file emitted in release builds ([ad38d93]).
- Seven runnable sample projects covering the full feature surface: `BasicChat`, `StreamingChat`, `ToolCalling`, `StructuredOutput`, `Multimodal`, `Embeddings`, `MiniRag` ([c69dae3], [920c144], [dd23e60], [f3506f4]).
- Dependabot configuration for NuGet and GitHub Actions ([ac7a538]).
- GitHub issue templates (bug report, feature request) and pull request template ([9c2ffe2]).

### Changed
- All NuGet dependencies bumped to latest stable ([1171d53]):
  - `Microsoft.Extensions.DependencyInjection.Abstractions` 8.0.1 → 10.0.8
  - `Microsoft.Extensions.Http` 8.0.0 → 10.0.8
  - `coverlet.collector` 6.0.2 → 10.0.1
  - `Microsoft.NET.Test.Sdk` 17.9.0 → 18.6.0
  - `Moq` 4.20.70 → 4.20.72
  - `xunit` 2.7.1 → 2.9.3
  - `xunit.runner.visualstudio` 2.5.8 → 3.1.5
- GitHub Actions workflow updated: `actions/checkout@v4`, `actions/setup-dotnet@v4`, matrix over .NET 9 and 10, added format-check and pack steps, added `pull_request` trigger ([ed320de]).
- `GenerateRequest.Context` type corrected from `string` to `int[]` to match the Ollama API spec ([b3d3199]).
- `CONTRIBUTING.md` expanded with local setup, Conventional Commits guide, and branch/PR workflow ([c20d577]).

### Deprecated
- `GetEmbeddings(EmbeddingsRequest)` / `GetEmbeddingsAsync(EmbeddingsRequest)` (wraps `/api/embeddings`) is now marked `[Obsolete]` — migrate to `Embed(EmbedRequest)` which uses the newer `/api/embed` endpoint ([7ae481d]).

### Fixed
- `DELETE /api/delete` now uses HTTP `DELETE` instead of `POST`, matching the Ollama API spec ([f4a69d9]).
- `HttpContentNdjsonExtensions`: replaced `StreamReader.EndOfStream` check with a null-check on `ReadLineAsync` return value — eliminates CA2024 warning and is safer in async context ([7ebbc1f]).
- NuGet package icon inclusion changed from `None Update` to `None Remove` + `None Include` with empty `PackagePath`, fixing `NU5046` pack error under .NET 10 SDK ([ed320de]).

## [1.0.0] - 2024-04-22

### Added
- Initial release.
- `IOllamaHttpClient` with full Ollama REST API coverage: generate, chat (streaming and non-streaming), create model, copy, delete, show, pull, push, embeddings, list models.
- DI integration via `AddOllamaClient()` extension method.
- Configurable base URL via `OllamaConfiguration`.

[Unreleased]: https://github.com/Dev-Art-Solutions/OllamaClient/compare/v2.1.0...HEAD
[2.1.0]: https://github.com/Dev-Art-Solutions/OllamaClient/compare/v2.0.1...v2.1.0
[2.0.1]: https://github.com/Dev-Art-Solutions/OllamaClient/compare/v2.0.0...v2.0.1
[2.0.0]: https://github.com/Dev-Art-Solutions/OllamaClient/compare/v1.0.0...v2.0.0
[1.0.0]: https://github.com/Dev-Art-Solutions/OllamaClient/releases/tag/v1.0.0
[2f344d0]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/2f344d0
[a57d195]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/a57d195
[5f53cab]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/5f53cab
[de42221]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/de42221
[920c144]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/920c144
[dd23e60]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/dd23e60
[f3506f4]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/f3506f4
[7ae481d]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/7ae481d
[b3d3199]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/b3d3199
[f4a69d9]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/f4a69d9
[ad38d93]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/ad38d93
[c69dae3]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/c69dae3
[ac7a538]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/ac7a538
[9c2ffe2]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/9c2ffe2
[1171d53]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/1171d53
[ed320de]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/ed320de
[7ebbc1f]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/7ebbc1f
[c20d577]: https://github.com/Dev-Art-Solutions/OllamaClient/commit/c20d577
