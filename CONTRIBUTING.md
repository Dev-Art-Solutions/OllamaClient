# Contributing

Thank you for your interest in contributing to OllamaClient!

## Getting started

1. Fork the repository and create a branch from `main`.
2. Make your changes in `src/`.
3. Run `dotnet build src && dotnet test src && dotnet format src --verify-no-changes` before pushing.
4. Open a pull request against `main` with a clear description of the change and why it is needed.

## Reporting issues

Please open a [GitHub issue](https://github.com/Dev-Art-Solutions/OllamaClient/issues) with:
- A clear title and description.
- Steps to reproduce (for bugs).
- The Ollama version (`ollama --version`) and .NET version (`dotnet --version`) you are using.

## Code style

- Follow the existing conventions in the codebase.
- Run `dotnet format` before committing.
- Keep public API surface minimal; new endpoints should mirror the [Ollama REST API](https://github.com/ollama/ollama/blob/main/docs/api.md) naming.
