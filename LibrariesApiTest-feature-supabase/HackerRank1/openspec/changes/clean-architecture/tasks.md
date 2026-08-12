## 1. Setup Layer Folders

- [x] 1.1 Create folder structure: Domain/Entities, Domain/Interfaces, Application/Interfaces, Application/DTOs, Infrastructure/Data, Infrastructure/Services, Infrastructure/Helpers, Presentation/Controllers

## 2. Domain Layer (Center — no dependencies)

- [x] 2.1 Move Book and Library entities from Data/ to Domain/Entities/ and update namespace to `LibraryService.WebAPI.Domain.Entities`
- [x] 2.2 Remove EF Core `[Key]` attributes from Book and Library entities — rely on convention-based configuration
- [x] 2.3 Create ILibraryRepository interface in Domain/Interfaces/ with methods: GetById, GetAll, Add, Update, Delete
- [x] 2.4 Create IBookRepository interface in Domain/Interfaces/ with methods: GetByLibraryId, GetById, Add, Update, Delete

## 3. Infrastructure Layer (depends on Domain)

- [ ] 3.1 Move LibraryContext from Data/ to Infrastructure/Data/ and update namespace to `LibraryService.WebAPI.Infrastructure.Data`
- [ ] 3.2 Add Fluent API entity configuration in LibraryContext.OnModelCreating for Book and Library
- [ ] 3.3 Implement ILibraryRepository in Infrastructure/Data/ using LibraryContext
- [ ] 3.4 Implement IBookRepository in Infrastructure/Data/ using LibraryContext
- [ ] 3.5 Move TokenGenerator from Helpers/ to Infrastructure/Helpers/ and update namespace to `LibraryService.WebAPI.Infrastructure.Helpers`
- [ ] 3.6 Move JwtSettings from Entities/ to Infrastructure/ and update namespace to `LibraryService.WebAPI.Infrastructure`
- [ ] 3.7 Move LibrariesService implementation from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`
- [ ] 3.8 Move BooksService implementation from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`
- [ ] 3.9 Move AuthenticationService from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`

## 4. Application Layer (depends on Domain)

- [ ] 4.1 Move ILibrariesService interface from Services/ to Application/Interfaces/ and update namespace to `LibraryService.WebAPI.Application.Interfaces`
- [ ] 4.2 Move IBooksService interface from Services/ to Application/Interfaces/ and update namespace to `LibraryService.WebAPI.Application.Interfaces`
- [ ] 4.3 Move IAuthenticationService interface from Services/ to Application/Interfaces/ and update namespace to `LibraryService.WebAPI.Application.Interfaces`
- [ ] 4.4 Move DTOs (BookForm, LibraryForm, User) from DTOs/ to Application/DTOs/ and update namespace to `LibraryService.WebAPI.Application.DTOs`

## 5. Presentation Layer (depends on Application)

- [ ] 5.1 Update LibrariesController to use ILibrariesService from Application layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 5.2 Update BooksController to use IBooksService and ILibrariesService from Application layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 5.3 Update AuthController to use IAuthenticationService and JwtSettings from Infrastructure layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`

## 6. Wiring and Configuration

- [ ] 6.1 Update Startup.cs DI registrations to reference new namespaces for all services, repositories, and DbContext
- [ ] 6.2 Update Program.cs namespace if changed
- [ ] 6.3 Update appsettings.json and appsettings.Development.json references if needed

## 7. Verification

- [ ] 7.1 Run `dotnet build` to verify compilation succeeds
- [ ] 7.2 Run `dotnet test` to verify all existing integration tests pass
- [ ] 7.3 Fix any namespace or reference errors from build output