# OllamaClient — Baseline Audit

_Generated 2026-05-28 as part of Phase 1 refresh work._

---

## 1. Target Frameworks

| Project | TargetFramework |
|---------|----------------|
| `OllamaClient` (library) | `net8.0` |
| `OllamaClient.Tests` | `net8.0` |
| `OllamaClient.QuickStart` | `net8.0` |

All three projects currently single-target `net8.0`.

---

## 2. NuGet Dependencies

### OllamaClient (library)

| Package | Current | Latest |
|---------|---------|--------|
| `Microsoft.Extensions.DependencyInjection.Abstractions` | 8.0.1 | 10.0.8 |
| `Microsoft.Extensions.Http` | 8.0.0 | 10.0.8 |

### OllamaClient.Tests

| Package | Current | Latest |
|---------|---------|--------|
| `coverlet.collector` | 6.0.2 | 10.0.1 |
| `Flurl.Http` | 4.0.2 | 4.0.2 (up to date) |
| `Microsoft.NET.Test.Sdk` | 17.9.0 | 18.6.0 |
| `Moq` | 4.20.70 | 4.20.72 |
| `xunit` | 2.7.1 | 2.9.3 |
| `xunit.runner.visualstudio` | 2.5.8 | 3.1.5 |

### OllamaClient.QuickStart

No direct NuGet dependencies (references `OllamaClient` via project reference).

> **Source:** `dotnet list src package --outdated` using a project-local `NuGet.Config` scoped to `nuget.org`.  
> The global `%APPDATA%\NuGet\NuGet.Config` contained several private/authenticated feeds that were excluded.

---

## 3. CI Workflow (`.github/workflows/build-and-test.yml`)

- **Trigger:** `push` to `main` + manual `workflow_dispatch`
- **Runner:** `ubuntu-latest`
- **Actions:**
  - `actions/checkout@v3` ← outdated (v4 available)
  - `actions/setup-dotnet` ← **missing** (relies on pre-installed SDK)
- **Steps:**
  - `dotnet build src --configuration Release`
  - `dotnet test src --configuration Release --no-build`
- **Missing steps:**
  - No `dotnet format --verify-no-changes`
  - `dotnet pack` is commented out
  - No matrix for multiple .NET versions

---

## 4. README

Current README has two sections:

1. **Title + 1-line description** — functional, but no badge row (only the CI badge).
2. **Quick Start** — install via `Install-Package OllamaClient` + a ~30-line code example showing pull, list models, and streaming chat.

**Gaps:**
- No NuGet version/downloads badges
- No features/endpoints list
- No configuration docs (DI setup options, base URL override, timeout)
- No links to samples
- No compatibility table
- No `CONTRIBUTING.md` reference
- No license section

---

## 5. Implemented Ollama Endpoints

| Endpoint | Method | Streaming variant |
|----------|--------|-------------------|
| `GET /api/tags` | `GetModels()` | — |
| `POST /api/generate` | `Generate(GenerateRequest)` | `Generate(GenerateStreamRequest)` |
| `POST /api/chat` | `SendChat(ChatRequest)` | `SendChat(ChatStreamRequest)` |
| `POST /api/create` | `Create(CreateModelRequest)` | `Create(CreateModelStreamRequest)` |
| `POST /api/copy` | `Copy(CopyRequest)` | — |
| `POST /api/delete` | `Delete(DeleteRequest)` | — |
| `POST /api/show` | `Show(ShowRequest)` | — |
| `POST /api/pull` | `Pull(PullRequest)` | `Pull(PullStreamRequest)` |
| `POST /api/push` | `Push(PushRequest)` | `Push(PushStreamRequest)` |
| `POST /api/embeddings` | `GetEmbeddings(EmbeddingsRequest)` | — |

**Notable gap vs current Ollama API:**
- `POST /api/delete` — the Ollama API actually uses HTTP `DELETE`, but the current implementation uses `POST`. This is a silent bug; Ollama appears to accept it, but it deviates from the spec.
- `POST /api/embed` — the newer Ollama embedding endpoint (the older `/api/embeddings` endpoint still works).
- `POST /api/blobs/:digest` (`HEAD`/`POST`) — blob management not implemented.
- `GET /api/ps` — running processes not implemented.
- `POST /api/version` — version endpoint not implemented.

There is also `IStatefulConversationOllamaService` / `StatefulConversationOllamaService` which wraps `IOllamaHttpClient` and manages conversation history (message accumulation), providing a higher-level stateful chat API.

---

## 6. Open Issue #1

**Title:** "How does it maintain context?"  
**URL:** https://github.com/Dev-Art-Solutions/OllamaClient/issues/1  
**Status:** Open, no comments.

The reporter asks how to maintain conversation context: declaring `ChatRequest` globally causes all previous answers to accumulate in subsequent messages; declaring it locally loses prior context. The question is unanswered.

**Note:** The `IStatefulConversationOllamaService` (added in a later commit) directly addresses this use case by maintaining a message list per instance. The README does not mention it and the issue has not been closed or pointed at this service.
