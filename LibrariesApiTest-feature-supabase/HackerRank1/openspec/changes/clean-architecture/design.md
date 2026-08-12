## Context

The current project is a single-project ASP.NET Core Web API where controllers, services, data access, and domain entities are mixed in flat folders with no clear layer boundaries. Controllers depend directly on EF Core and concrete service implementations, making unit testing difficult and violating the Dependency Inversion Principle.

## Goals / Non-Goals

**Goals:**
- Implement Clean Architecture with strict dependency inversion: Domain at the center, no outward dependencies.
- Domain entities are pure POCOs with no framework attributes (no EF Core, no serialization attributes).
- Repository interfaces are defined in Domain; Infrastructure provides EF Core implementations.
- Use-case interfaces are defined in Application; Infrastructure provides implementations.
- Controllers depend only on Application layer interfaces.
- Keep the external API behavior unchanged (same routes, same request/response formats, same HTTP status codes).

**Non-Goals:**
- Split into separate physical projects (solution-level multi-project structure).
- Change API routes, request/response schemas, or HTTP status codes.
- Implement new features beyond the existing scope.

## Decisions

### 1. Single-project namespace-based Clean Architecture
**Decision:** Use four namespaces (`Domain`, `Application`, `Infrastructure`, `Presentation`) within the same project instead of separate projects.
**Rationale:** The project is a single-solution HackerRank exercise. Separate projects add unnecessary complexity while namespace-based layering still achieves the core Clean Architecture goal of dependency inversion.
**Alternative:** Separate projects per layer (e.g., `LibraryService.Domain`, `LibraryService.Application`). Rejected because it adds project-file overhead and complicates the build for the exercise scope.

### 2. Persistence-ignorant domain entities
**Decision:** Move `Book` and `Library` entities to `Domain/Entities/` and remove EF Core `[Key]` attributes. Use Fluent API or convention-based configuration in Infrastructure.
**Rationale:** Clean Architecture requires Domain to be framework-agnostic. EF Core attributes tie entities to the ORM.
**Alternative:** Keep entities in `Data/` with EF Core attributes. Rejected because it couples Domain to Infrastructure.

### 3. Repository interfaces in Domain, implementations in Infrastructure
**Decision:** Define `ILibraryRepository` and `IBookRepository` in `Domain/Interfaces/`. Implement them in `Infrastructure/Data/`.
**Rationale:** Domain declares what it needs; Infrastructure provides the implementation. This is the core of Dependency Inversion in Clean Architecture.
**Alternative:** Put interfaces in Application layer. Rejected because repository interfaces are a domain concern — they express how domain entities are accessed.

### 4. Use-case interfaces in Application, implementations in Infrastructure
**Decision:** Define `ILibrariesService` and `IBooksService` in `Application/Interfaces/`. Implement them in `Infrastructure/Services/`.
**Rationale:** Application layer defines the use-case contracts; Infrastructure provides the concrete orchestration logic that uses repository interfaces from Domain.
**Alternative:** Keep interfaces alongside implementations. Rejected because it breaks the dependency direction — Application must not depend on Infrastructure.

### 5. JWT and cross-cutting concerns in Infrastructure
**Decision:** Keep `TokenGenerator`, `JwtSettings`, and `AuthenticationService` in `Infrastructure`.
**Rationale:** Authentication is an infrastructure concern — it deals with external protocols (HTTP, tokens) and external configuration (secrets).
**Alternative:** Put authentication in Application. Rejected because JWT signing and token generation depend on infrastructure concerns (secrets, clock).

### 6. DTOs in Application layer
**Decision:** Move `BookForm`, `LibraryForm`, and `User` DTOs to `Application/DTOs/`.
**Rationale:** DTOs are application-layer contracts between presentation and application layers.

## Risks / Trade-offs

- **Namespace migration effort**: Moving files and updating namespaces is mechanical but error-prone. Mitigation: Use IDE refactoring tools and run `dotnet build` after each move.
- **Test compatibility**: Existing integration tests reference old namespaces. Mitigation: Update test imports after the refactor.
- **Over-engineering for scope**: A single-project Clean Architecture structure may be more complex than needed for a small API. Mitigation: The layers are namespace-based, not project-based, keeping build complexity low.
- **Fluent API configuration**: Moving EF Core attributes off entities means configuration must move to `DbContext.OnModelCreating` or a separate `IEntityTypeConfiguration` class in Infrastructure. Mitigation: Keep Fluent API configuration in `LibraryContext`.

## Migration Plan

1. Create new folder structure for each layer.
2. Move domain entities to Domain, remove EF Core attributes.
3. Create repository interfaces in Domain/Interfaces.
4. Move DbContext and repository implementations to Infrastructure.
5. Move service interfaces to Application, implementations to Infrastructure.
6. Move DTOs to Application/DTOs.
7. Move JWT helpers to Infrastructure.
8. Update controllers to use Application interfaces.
9. Update Startup.cs DI registrations.
10. Run `dotnet build` to verify compilation.
11. Run existing tests to verify behavior is unchanged.

## Open Questions

- Should `LibraryContext.OnModelCreating` use Fluent API for entity configuration or keep it in separate `IEntityTypeConfiguration` classes? Current decision: Fluent API in `OnModelCreating` for simplicity.
- Should `AuthenticationService` be in Application or Infrastructure? Current decision: Infrastructure (it depends on JWT which is infrastructure).