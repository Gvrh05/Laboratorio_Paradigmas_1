## Context

The current project is a single-project ASP.NET Core Web API organized by technical layer (Controllers, Services, Data, DTOs). Each feature's logic is scattered across these layers, making a single use case hard to follow in isolation. The goal is to reorganize around vertical slices: one self-contained folder per feature that owns everything needed to handle that use case.

## Goals / Non-Goals

**Goals:**
- Organize code by feature (Libraries, Books, Authentication) instead of by technical layer.
- Each vertical slice contains its own request/response DTOs, validation, handler logic, and data access.
- Keep shared infrastructure (EF Core DbContext, JWT token generation, system entities) in a common area referenced by the slices.
- Controllers act as thin adapters that map HTTP requests to slice handlers.
- Keep the external API behavior unchanged (same routes, same request/response formats, same HTTP status codes).

**Non-Goals:**
- Split into separate physical projects.
- Change API routes, request/response schemas, or HTTP status codes.
- Introduce a mediator/CQRS framework (e.g., MediatR) as a hard requirement — the slice handler pattern can be plain classes that controllers call directly.
- Implement new features beyond the existing scope.

## Decisions

### 1. Feature-based slices within a single project
**Decision:** Organize namespaces by feature slice: `LibraryService.WebAPI.Features.Libraries`, `...Features.Books`, `...Features.Authentication`.
**Rationale:** Vertical Slice Architecture groups code by use case so each feature is self-contained and readable end-to-end.
**Alternative:** Keep layer-based folders and merely add feature sub-folders. Rejected because it preserves the very scattering the change is meant to remove.

### 2. Plain handler classes instead of a Mediator framework
**Decision:** Each slice exposes plain handler classes (e.g., `CreateLibraryHandler`, `DeleteLibraryHandler`) that controllers invoke directly. No MediatR or CQRS dependency.
**Rationale:** The project is small with synchronous CRUD use cases; adding MediatR layers is unnecessary complexity. Handlers keep the use-case logic isolated without a framework.
**Alternative:** MediatR with request/response records. Rejected because it adds a dependency and indirection with little benefit at this scale. This decision can be revisited if slices grow.

### 3. Per-slice DTOs
**Decision:** Move request/response models into their owning slice (e.g., `CreateLibraryRequest` in Features/Libraries) rather than a shared DTO folder.
**Rationale:** Vertical slices own their input/output contracts. Shared cross-slice DTOs belong in the Common area.
**Alternative:** Keep a central DTO folder. Rejected because it effectively reintroduces the layer-based organization.

### 4. Shared infrastructure in a Common area
**Decision:** Keep `LibraryContext`, EF Core configuration, `JwtSettings`, and `TokenGenerator` in a `LibraryService.WebAPI.Common` namespace used by all slices.
**Rationale:** DbContext and JWT are cross-cutting infrastructure reused across slices; duplicating them per feature would be wasteful.
**Alternative:** Put DbContext inside each slice. Rejected because persistence is shared across features.

### 5. System entities live in Common
**Decision:** Move `Book`, `Library`, and `User` entities to `Common/Entities` (shared by all slices).
**Rationale:** Entities are shared domain concepts referenced by multiple feature slices — they are not the private concern of a single slice.
**Alternative:** Duplicate entity definitions per slice. Rejected because it creates data consistency risks.

### 6. Controllers as thin adapters
**Decision:** Controllers receive the request DTO, call the slice handler, and map the result to an HTTP response. They contain no business or data-access logic.
**Rationale:** The presentation boundary stays thin; all use-case logic lives in the feature slice.

## Risks / Trade-offs

- **Namespace migration effort**: Moving files and updating namespaces is mechanical but error-prone. Mitigation: Use IDE refactoring tools and run `dotnet build` after each move.
- **Test compatibility**: Existing integration tests reference old namespaces. Mitigation: Update test imports after the refactor.
- **Slice ambiguity**: Small features (e.g., "get all libraries") could become tiny slices with near-duplicate handler code. Mitigation: Group by meaningful use case (the CRUD operations on Libraries form one slice module).
- **Over-engineering for scope**: Vertical slice structure may be more verbose than a small API needs. Mitigation: A shared Common area avoids per-slice duplication of infrastructure.

## Migration Plan

1. Create the Common area (DbContext, entities, JWT infrastructure).
2. Create the Libraries slice (request/response DTOs, handlers for CRUD).
3. Create the Books slice (request/response DTOs, handlers for list and add).
4. Create the Authentication slice (login handler, token generation).
5. Rewrite controllers to delegate to slice handlers.
6. Update Startup.cs DI registrations.
7. Run `dotnet build` to verify compilation.
8. Run existing tests to verify behavior is unchanged.

## Open Questions

- Should slices share a common `BaseHandler` or stay fully independent? Current decision: independent handler classes; a base class can be introduced later if duplication appears.
- Should the `AuthenticationService` remain a shared service or become an Authentication slice handler? Current decision: fold its logic into the Authentication slice's login handler.