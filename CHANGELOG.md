# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Multi-target support for `net9.0` and `net10.0` (dropped `net8.0`).
- `IStatefulConversationOllamaService` / `StatefulConversationOllamaService` for stateful multi-turn chat — manages message history automatically.
- Project-level `NuGet.Config` scoped to `nuget.org`, preventing authentication errors from unrelated private feeds.
- `docs/audit.md` baseline audit covering implemented endpoints, dependency state, and CI structure.
- `CHANGELOG.md` (this file).

### Changed
- All NuGet dependencies bumped to latest stable:
  - `Microsoft.Extensions.DependencyInjection.Abstractions` 8.0.1 → 10.0.8
  - `Microsoft.Extensions.Http` 8.0.0 → 10.0.8
  - `coverlet.collector` 6.0.2 → 10.0.1
  - `Microsoft.NET.Test.Sdk` 17.9.0 → 18.6.0
  - `Moq` 4.20.70 → 4.20.72
  - `xunit` 2.7.1 → 2.9.3
  - `xunit.runner.visualstudio` 2.5.8 → 3.1.5
- GitHub Actions workflow updated: `actions/checkout@v4`, `actions/setup-dotnet@v4`, matrix over .NET 9 and 10, added format-check and pack steps, added `pull_request` trigger.
- `HttpContentNdjsonExtensions`: replaced `StreamReader.EndOfStream` check (CA2024) with null-check on `ReadLineAsync` — safer in async context.
- NuGet package icon inclusion changed from `None Update` to `None Remove` + `None Include` with empty `PackagePath` to fix `NU5046` pack error under .NET 10 SDK.

## [1.0.0] - 2024-04-22

### Added
- Initial release.
- `IOllamaHttpClient` with full Ollama REST API coverage: generate, chat (streaming and non-streaming), create model, copy, delete, show, pull, push, embeddings, list models.
- DI integration via `AddOllamaClient()` extension method.
- Configurable base URL via `OllamaConfiguration`.

[Unreleased]: https://github.com/Dev-Art-Solutions/OllamaClient/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/Dev-Art-Solutions/OllamaClient/releases/tag/v1.0.0
