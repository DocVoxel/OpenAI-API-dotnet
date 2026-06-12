# Purpose

Git submodule containing an unofficial C#/.NET SDK for OpenAI APIs (`OpenAI_API.sln`).

## Ownership

- Owns `OpenAI_API/` library and `OpenAI_Tests/`
- Main YourNews app consumption of OpenAI is wired outside this folder unless explicitly integrating the submodule

## Local Contracts

- Separate solution: `chatbotapi/OpenAI_API.sln`
- Third-party/unofficial wrapper; upstream README documents API surface (chat, completions, images, embeddings)
- Submodule changes should stay isolated from main YourNews layer conventions unless deliberately linked

## Work Guidance

- See `chatbotapi/README.md` for API usage examples
- LICENSE: check `LICENSE.md` before redistribution changes

## Verification

- `OpenAI_Tests/` in submodule solution

## Child DOX Index

None.
