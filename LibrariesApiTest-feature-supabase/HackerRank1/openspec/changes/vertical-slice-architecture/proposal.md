## Why

The current codebase organizes code by technical layer (Controllers, Services, Data), which scatters the logic for each feature across many folders. A change to one feature — such as deleting a library — touches controllers, services, and data access in different locations, making features hard to understand in isolation and slower to iterate on.

## What Changes

- Restructure into Vertical Slice architecture: each feature is a self-contained "slice" containing its own request/response models, validation, handler logic, and data access, organized by feature rather than by technical layer.
- Group the code into feature slices: `Libraries`, `Books`, and `Authentication`.
- Each slice owns its DTOs and handler logic end-to-end (from HTTP request parsing to persistence).
- Shared infrastructure (EF Core DbContext, JWT token generation) lives in a common `Common`/`Shared` area used by all slices.
- Controllers remain as thin adapters that delegate to slice handlers.
- **BREAKING**: Internal namespaces, folder structure, and class locations will change.

## Capabilities

### New Capabilities
- `libraries`: Library CRUD operations — create, read, update, and delete libraries with their books.
- `books`: Book management within libraries — add and list books for a given library.
- `authentication`: User login with JWT token generation and validation.

### Modified Capabilities

## Impact

- Code is reorganized from layer-based folders into feature-based slice folders.
- Each slice groups request/response DTOs, validation, handler logic, and persistence for one use case.
- Shared EF Core DbContext and JWT infrastructure are kept in a common area referenced by all slices.
- Controllers defer to slice handlers instead of layer services.
- No changes to external API routes, request/response formats, or HTTP status codes.
- Existing integration tests must be updated for new namespaces.