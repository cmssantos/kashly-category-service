# GitHub Copilot Instructions for .NET Projects

Please follow these guidelines when generating code for this repository:

## General Principles
- Always write C# code that adheres to SOLID principles and Clean Code practices.
- Prioritize readability, maintainability, and scalability in all code suggestions.
- Ensure code is self-explanatory; minimize comments unless clarifying complex business logic or non-obvious design decisions.

## Naming Conventions
- Use descriptive, intention-revealing names for classes, methods, variables, and properties.
- Apply PascalCase for class, interface, and method names.
- Use camelCase for local variables and parameters.
- Avoid abbreviations and single-letter names except for common conventions (e.g., `i` for loop counters).

## Code Structure & Organization
- Keep methods and functions short, focused, and single-purpose.
- Structure classes so each has a clear, distinct responsibility.
- Use interfaces and abstractions to decouple components and facilitate testing.
- Prefer composition over inheritance where appropriate.

## Control Flow & Logic
- Favor guard clauses to reduce nesting and improve code clarity.
- Avoid deeply nested conditionals and long methods.
- Handle exceptions explicitly and responsibly; never silently swallow errors.
- Return early when possible to simplify logic.

## Comments & Documentation
- Only add comments to explain *why* something is done, not *what* is done-well-written code should make the intent clear.
- Remove redundant, outdated, or obvious comments.
- Prefer XML documentation comments for public APIs if documentation is necessary.

## Consistency & Formatting
- Adhere to .NET coding standards and project-specific style guides.
- Ensure consistent indentation and whitespace usage.
- Organize using directives and namespaces logically.

## Code Quality
- Avoid code duplication and dead code.
- Write code that is easily testable; prefer dependency injection and separation of concerns.
- When suggesting tests, use xUnit or the project's preferred testing framework.
- Suggest meaningful unit and integration tests where relevant.

## Security & Performance
- Follow secure coding practices; never expose sensitive data in code.
- Optimize for performance only when necessary and never at the expense of readability or maintainability.

## Example Patterns
- Use async/await for asynchronous operations.
- Prefer LINQ for collection manipulation when it improves clarity.
- Use records and immutable types for value objects where appropriate.

---

> **Summary:**  
> Generate clean, modern, idiomatic C# code for .NET projects. Follow SOLID and Clean Code principles. Minimize comments by writing self-explanatory code. Prioritize clarity, maintainability, and testability in all suggestions.

