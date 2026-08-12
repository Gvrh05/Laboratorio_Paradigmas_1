## 1. Setup Layer Folders

- [x] 1.1 Create folder structure: Domain/Entities, Domain/Interfaces, Application/Interfaces, Application/DTOs, Infrastructure/Data, Infrastructure/Services, Infrastructure/Helpers, Presentation/Controllers

## 2. Domain Layer

- [x] 2.1 Move Book and Library entities from Data/ to Domain/Entities/ and update namespace to `LibraryService.WebAPI.Domain.Entities`
- [x] 2.2 Create ILibraryRepository interface in Domain/Interfaces/ with methods: GetById, GetAll, Add, Update, Delete
- [x] 2.3 Create IBookRepository interface in Domain/Interfaces/ with methods: GetByLibraryId, GetById, Add, Update, Delete

## 3. Infrastructure Layer

- [x] 3.1 Move LibraryContext from Data/ to Infrastructure/Data/ and update namespace to `LibraryService.WebAPI.Infrastructure.Data`
- [x] 3.2 Move Book and Library EF configurations (if any) to Infrastructure/Data/
- [x] 3.3 Implement ILibraryRepository in Infrastructure/Data/ using LibraryContext
- [x] 3.4 Implement IBookRepository in Infrastructure/Data/ using LibraryContext
- [x] 3.5 Move TokenGenerator from Helpers/ to Infrastructure/Helpers/ and update namespace to `LibraryService.WebAPI.Infrastructure.Helpers`
- [x] 3.6 Move JwtSettings from Entities/ to Infrastructure/ and update namespace to `LibraryService.WebAPI.Infrastructure`

## 4. Application Layer

- [x] 4.1 Move ILibrariesService interface from Services/ to Application/Interfaces/ and update namespace to `LibraryService.WebAPI.Application.Interfaces`
- [x] 4.2 Move IBooksService interface from Services/ to Application/Interfaces/ and update namespace to `LibraryService.WebAPI.Application.Interfaces`
- [x] 4.3 Move LibrariesService implementation from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`
- [x] 4.4 Move BooksService implementation from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`
- [x] 4.5 Move DTOs (BookForm, LibraryForm, User) from DTOs/ to Application/DTOs/ and update namespace to `LibraryService.WebAPI.Application.DTOs`

## 5. Presentation Layer

- [ ] 5.1 Update LibrariesController to use ILibrariesService from Application layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 5.2 Update BooksController to use IBooksService and ILibrariesService from Application layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 5.3 Update AuthController to use IAuthenticationService and JwtSettings from Infrastructure layer and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 5.4 Move AuthenticationService from Services/ to Infrastructure/Services/ and update namespace to `LibraryService.WebAPI.Infrastructure.Services`
- [ ] 5.5 Update IAuthenticationService interface location to Application/Interfaces/ and update namespace

## 6. Wiring and Configuration

- [ ] 6.1 Update Startup.cs DI registrations to reference new namespaces for all services and DbContext
- [ ] 6.2 Update Program.cs namespace if changed
- [ ] 6.3 Update appsettings.json and appsettings.Development.json references if needed

## 7. Verification

- [ ] 7.1 Run `dotnet build` to verify compilation succeeds
- [ ] 7.2 Run `dotnet test` to verify all existing integration tests pass
- [ ] 7.3 Fix any namespace or reference errors from build output