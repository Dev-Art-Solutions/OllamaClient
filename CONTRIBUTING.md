# Contributing

Thank you for your interest in contributing to OllamaClient!

## Local environment setup

1. Install [.NET 9 SDK](https://dotnet.microsoft.com/download) or newer.
2. Install [Ollama](https://ollama.com/download) and start it (`ollama serve`).
3. Pull a model to run the samples and integration tests against:
   ```bash
   ollama pull llama3.2
   ```
4. Clone the repository and restore dependencies:
   ```bash
   git clone https://github.com/Dev-Art-Solutions/OllamaClient.git
   cd OllamaClient
   dotnet restore src
   ```

## Running the test suite

```bash
dotnet test src
```

The tests hit a live Ollama instance on `http://localhost:11434`. Make sure Ollama is running with at least one model pulled before running tests.

## Branch and PR workflow

1. Create a feature branch from `main`:
   ```bash
   git checkout -b feat/my-change main
   ```
2. Make your changes in `src/`.
3. Verify everything passes locally before pushing:
   ```bash
   dotnet build src -c Release
   dotnet test src
   dotnet format src --verify-no-changes
   ```
4. Push your branch and open a pull request against `main`.
5. Keep PRs focused — one logical change per PR. Do not stack PRs.

## Commit convention

This project uses [Conventional Commits](https://www.conventionalcommits.org/):

| Prefix | When to use |
|--------|-------------|
| `feat:` | New feature or new API endpoint |
| `fix:` | Bug fix |
| `docs:` | Documentation only changes |
| `refactor:` | Code change that is not a fix or feature |
| `test:` | Adding or updating tests |
| `chore:` | Build, CI, dependency updates |

Examples:
```
feat: add GET /api/tags endpoint
fix: correct JSON property name for keep_alive field
docs: add XML doc comments to OllamaHttpClient
```

## Code style

- Follow the existing conventions in the codebase.
- Run `dotnet format src` before committing.
- Keep the public API surface minimal; new endpoints should mirror the [Ollama REST API](https://github.com/ollama/ollama/blob/main/docs/api.md) naming.
- Add XML doc comments (`<summary>`, `<param>`, `<returns>`) to every new public type and member — the Release build treats missing docs (CS1591) as errors.

## Reporting issues

Please open a [GitHub issue](https://github.com/Dev-Art-Solutions/OllamaClient/issues) with:
- A clear title and description.
- Steps to reproduce (for bugs).
- The Ollama version (`ollama --version`) and .NET version (`dotnet --version`) you are using.
