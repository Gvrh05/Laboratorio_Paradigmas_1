## Why

The current codebase mixes concerns across a single flat structure — controllers, services, data access, and domain entities coexist in the same project with no clear layer boundaries. This makes unit testing difficult (controllers depend directly on EF Core), violates the Dependency Inversion Principle, and makes the codebase hard to maintain as it grows.

## What Changes

- Restructure the project into N-Layer architecture with clear separation of concerns: Presentation, Application, Domain, and Infrastructure layers.
- Extract domain entities and repository interfaces into a dedicated Domain layer.
- Move service interfaces to the Application layer; keep implementations in Infrastructure.
- Move data access (EF Core DbContext, migrations) to Infrastructure.
- Move cross-cutting concerns (JWT token generation, configuration) to Infrastructure.
- Controllers remain in Presentation and depend only on Application layer interfaces.
- **BREAKING**: Namespace and folder reorganization will change internal code paths.

## Capabilities

### New Capabilities
- `libraries`: Library CRUD operations — create, read, update, and delete libraries with books.
- `books`: Book management within libraries — add and list books for a given library.
- `authentication`: User login with JWT token generation and validation.

### Modified Capabilities

## Impact

- Internal code structure is reorganized into four layers (Presentation, Application, Domain, Infrastructure).
- No changes to external API routes, request/response formats, or HTTP status codes.
- EF Core and PostgreSQL dependencies move to Infrastructure project.
- Existing tests in `LibraryService.Tests/IntegrationTests.cs` must be updated to reference new namespaces.