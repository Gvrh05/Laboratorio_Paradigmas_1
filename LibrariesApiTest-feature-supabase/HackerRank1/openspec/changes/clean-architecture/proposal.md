## Why

The current codebase has no dependency boundaries — controllers depend directly on EF Core, entities carry persistence attributes, and services mix business logic with data access. This violates the Dependency Inversion Principle and makes the code untestable without a database, tightly coupled to infrastructure, and difficult to evolve.

## What Changes

- Restructure into Clean Architecture with four concentric layers: Domain (center), Application, Infrastructure, and Presentation.
- Domain layer contains pure entities and repository interfaces with zero framework dependencies.
- Application layer defines use-case interfaces and orchestrates domain logic.
- Infrastructure layer implements repository interfaces (EF Core), JWT helpers, and cross-cutting concerns.
- Presentation layer (Controllers) depends only on Application layer abstractions.
- **BREAKING**: Internal namespaces, folder structure, and class locations will change.

## Capabilities

### New Capabilities
- `libraries`: Library CRUD operations — create, read, update, and delete libraries with their books.
- `books`: Book management within libraries — add and list books for a given library.
- `authentication`: User login with JWT token generation and validation.

### Modified Capabilities

## Impact

- Domain entities become persistence-ignorant POCOs (no EF Core attributes).
- Repository interfaces move to Domain; EF Core implementations move to Infrastructure.
- Service interfaces move to Application; implementations move to Infrastructure.
- Controllers depend on Application layer interfaces only.
- No changes to external API routes, request/response formats, or HTTP status codes.
- Existing integration tests must be updated for new namespaces.