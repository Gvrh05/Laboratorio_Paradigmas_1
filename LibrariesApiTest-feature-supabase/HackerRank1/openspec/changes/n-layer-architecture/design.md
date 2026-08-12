## Context

The current project is a single-project ASP.NET Core Web API where controllers, services, data access, and domain entities are mixed in flat folders with no clear layer boundaries. Controllers depend directly on EF Core and concrete service implementations, making unit testing difficult and violating the Dependency Inversion Principle.

## Goals / Non-Goals

**Goals:**
- Separate concerns into four distinct layers: Presentation, Application, Domain, Infrastructure.
- Introduce dependency inversion — higher layers depend on abstractions defined in lower layers.
- Make the codebase testable by allowing mocking of repository and service interfaces.
- Keep the external API behavior unchanged (same routes, same request/response formats, same HTTP status codes).

**Non-Goals:**
- Split into separate physical projects (solution-level multi-project structure).
- Change API routes, request/response schemas, or HTTP status codes.
- Implement new features beyond the existing scope.

## Decisions

### 1. Namespace-based layering within a single project
**Decision:** Use four namespaces (`Presentation`, `Application`, `Domain`, `Infrastructure`) within the same project instead of separate projects.
**Rationale:** The project is a single-solution HackerRank exercise. Separate projects add unnecessary complexity for this scope while still achieving clean layer separation.
**Alternative:** Separate projects per layer (e.g., `LibraryService.Domain`, `LibraryService.Application`). Rejected because it adds project-file overhead and complicates the build for the exercise scope.

### 2. Domain entities live in the Domain layer
**Decision:** Move `Book` and `Library` entities from `Data/` to `Domain/Entities/`.
**Rationale:** Entities are domain concepts, not data-access concerns. EF Core attributes are an infrastructure detail.
**Alternative:** Keep entities in `Data/` with a comment. Rejected because it conflates domain models with persistence.

### 3. Repository interfaces in Domain, implementations in Infrastructure
**Decision:** Define `ILibraryRepository` and `IBookRepository` in `Domain/Interfaces/`. Implement them in `Infrastructure/Data/`.
**Rationale:** The Domain layer declares what it needs; Infrastructure provides the implementation. This follows the Dependency Inversion Principle.
**Alternative:** Put interfaces alongside implementations in Infrastructure. Rejected because it forces Domain to depend on Infrastructure.

### 4. Service interfaces in Application, implementations in Infrastructure
**Decision:** Define `ILibrariesService` and `IBooksService` in `Application/Interfaces/`. Implement them in `Infrastructure/Services/`.
**Rationale:** Application layer defines the use-case contracts; Infrastructure provides the concrete orchestration logic.
**Alternative:** Keep interfaces in the same project as implementations. Rejected because it breaks the dependency direction.

### 5. JWT and helpers remain in Infrastructure
**Decision:** Keep `TokenGenerator` and `JwtSettings` in `Infrastructure/`.
**Rationale:** Token generation is a cross-cutting infrastructure concern, not domain logic.

### 6. DTOs in Application layer
**Decision:** Move `BookForm`, `LibraryForm`, and `User` DTOs to `Application/DTOs/`.
**Rationale:** DTOs are application-layer contracts between the presentation and application layers.

## Risks / Trade-offs

- **Namespace migration effort**: Moving files and updating namespaces is mechanical but error-prone. Mitigation: Use IDE refactoring tools and run `dotnet build` after each move.
- **Test compatibility**: Existing integration tests reference old namespaces. Mitigation: Update test imports after the refactor.
- **Over-engineering for scope**: A single-project N-Layer structure may be more complex than needed for a small API. Mitigation: The layers are namespace-based, not project-based, keeping build complexity low.

## Migration Plan

1. Create new folder structure for each layer.
2. Move files to appropriate folders and update namespaces.
3. Update `Startup.cs` DI registrations to reference new namespaces.
4. Update controller imports to use Application layer interfaces.
5. Run `dotnet build` to verify compilation.
6. Run existing tests to verify behavior is unchanged.

## Open Questions

- Should `LibraryContext` (EF Core DbContext) be in Infrastructure or should a repository abstraction sit between? Current decision: Infrastructure, with repository interfaces in Domain.
- Should the `AuthenticationService` be in Application or Infrastructure? Current decision: Infrastructure (it is a cross-cutting concern).