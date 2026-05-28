# OllamaClient — API Gap Analysis

_Generated 2026-05-28 as part of Phase 2 work. Based on [Ollama API docs](https://github.com/ollama/ollama/blob/main/docs/api.md) vs the current client surface documented in `docs/audit.md`._

---

## 1. Implemented (matches current Ollama API)

| Endpoint | Client method | Notes |
|----------|--------------|-------|
| `POST /api/generate` | `Generate(GenerateRequest)` / `Generate(GenerateStreamRequest)` | Both variants present |
| `POST /api/chat` | `SendChat(ChatRequest)` / `SendChat(ChatStreamRequest)` | Both variants present |
| `POST /api/create` | `Create(CreateModelRequest)` / `Create(CreateModelStreamRequest)` | Both variants present |
| `GET /api/tags` | `GetModels()` | — |
| `POST /api/show` | `Show(ShowRequest)` | — |
| `POST /api/copy` | `Copy(CopyRequest)` | — |
| `DELETE /api/delete` | `Delete(DeleteRequest)` | ⚠ Uses HTTP POST, not DELETE — silent bug |
| `POST /api/pull` | `Pull(PullRequest)` / `Pull(PullStreamRequest)` | Both variants present |
| `POST /api/push` | `Push(PushRequest)` / `Push(PushStreamRequest)` | Both variants present |
| `POST /api/embeddings` | `GetEmbeddings(EmbeddingsRequest)` | Legacy endpoint — still works |
| `Message.Images` | `Message.Images` as `List<string>?` | Already implemented |

---

## 2. Missing (priority order)

### HIGH — directly affects modern usage

| Feature | Endpoint / Field | Phase-2 task |
|---------|-----------------|-------------|
| **Tool / function calling** | `tools` array in `POST /api/chat` request; `tool_calls` in response `message` | 2.2 |
| **Structured outputs (JSON schema `format`)** | `format` currently accepts only `string`; Ollama now accepts a JSON schema object | 2.3 |
| **New embed endpoint** | `POST /api/embed` — supports batch input (`input` as string or array), replaces `/api/embeddings` | 2.5 |
| **Thinking / reasoning (`think`)** | `think` bool on `POST /api/generate` and `POST /api/chat`; `thinking` string in streaming response chunks | 2.6 |
| **Running models list** | `GET /api/ps` | 2.7 |
| **Version endpoint** | `GET /api/version` | 2.7 |

### MEDIUM — request/response field gaps

| Field | Location | Notes |
|-------|----------|-------|
| `suffix` | `GenerateRequest` | Text inserted after the model response (fill-in-the-middle) |
| `context` | `GenerateRequest` / `GenerateResponse` | Opaque context token list for stateless follow-up generations |
| `think` | `GenerateRequest`, `ChatRequest` | Bool; enables reasoning trace |
| `thinking` | `GenerateResponse`, `ChatResponse` streaming chunks | Reasoning trace string returned when `think: true` |
| `tool_calls` | `Message` | Array of tool calls returned by the assistant |
| `template` | `ShowResponse` | Prompt template string returned by `/api/show` |
| `system` | `ShowResponse` | System prompt string returned by `/api/show` |
| `parameters` | `ShowResponse` | Modelfile parameter block string |
| `messages` | `ShowResponse` | List of messages (chat history) returned by `/api/show` |
| `modified_at` | `ModelResponse` | Model modification timestamp (present in `/api/tags` response items) |
| `expires_at` | Running model entry (`GET /api/ps` response) | When the model is scheduled to be unloaded |
| `size_vram` | Running model entry | VRAM consumed by the running model |

### LOW — blob management (rarely needed by library consumers)

| Endpoint | Notes |
|----------|-------|
| `HEAD /api/blobs/:digest` | Check whether a blob exists on the server |
| `POST /api/blobs/:digest` | Push a blob (used when creating models with local FROM/ADAPTER files) |

---

## 3. Changed / broken

| Item | Current behaviour | Ollama spec | Fix |
|------|-------------------|-------------|-----|
| `DELETE /api/delete` | Implemented via HTTP `POST` | Spec requires HTTP `DELETE` | Change `HttpMethod` to `DELETE`; keep request body (Ollama still reads it on DELETE) |
| `format` field on chat/generate | Accepts only `string?` | Now accepts `string \| object` (JSON schema) | Change type to `JsonElement?` or `object?` with custom converter |
| `/api/embed` vs `/api/embeddings` | Only `/api/embeddings` (legacy) is implemented | `/api/embed` is the current endpoint; accepts `input` as string or `string[]` | Add new method; mark old as `[Obsolete]` |

---

## 4. Implementation order for Phase 2 PRs

1. **PR 2.2** — Tool calling (`Tool`, `ToolCall`, `Function` models; `ChatRequest.Tools`; `Message.ToolCalls`; sample)
2. **PR 2.3** — Structured outputs (`format` as `JsonElement?`; sample)
3. **PR 2.4** — Multimodal images — already implemented (`Message.Images`); add sample + verify `GenerateRequest.Images` too
4. **PR 2.5** — `/api/embed` (`EmbedRequest`/`EmbedResponse`; mark `GetEmbeddings` as `[Obsolete]`)
5. **PR 2.6** — `think` parameter + `Thinking` response field + `suffix`/`context` on generate
6. **PR 2.7** — `GET /api/version`, `GET /api/ps`; fix `DELETE /api/delete` HTTP method
