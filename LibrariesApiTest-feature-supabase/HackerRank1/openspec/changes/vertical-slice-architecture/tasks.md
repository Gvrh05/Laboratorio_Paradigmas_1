## 1. Setup Slice Folders

- [ ] 1.1 Create folder structure: Common/Entities, Common/Data, Common/Helpers, Features/Libraries, Features/Books, Features/Authentication, Presentation/Controllers

## 2. Shared Infrastructure (Common)

- [ ] 2.1 Move LibraryContext from Data/ to Common/Data/ and update namespace to `LibraryService.WebAPI.Common.Data`
- [ ] 2.2 Move Book and Library entities from Data/ to Common/Entities/ with namespace `LibraryService.WebAPI.Common.Entities`
- [ ] 2.3 Move JwtSettings from Entities/ to Common/ with namespace `LibraryService.WebAPI.Common`
- [ ] 2.4 Move TokenGenerator from Helpers/ to Common/Helpers/ with namespace `LibraryService.WebAPI.Common.Helpers`
- [ ] 2.5 Move User and remaining DTOs used across slices to Common/DTOs/ with namespace `LibraryService.WebAPI.Common.DTOs`

## 3. Libraries Slice

- [ ] 3.1 Create Features/Libraries handlers for GetLibraries and GetLibraryById
- [ ] 3.2 Create Features/Libraries handlers for CreateLibrary and UpdateLibrary
- [ ] 3.3 Create Features/Libraries handler for DeleteLibrary
- [ ] 3.4 Move library-specific request/response DTOs into Features/Libraries

## 4. Books Slice

- [ ] 4.1 Create Features/Books handler for GetBooksByLibrary
- [ ] 4.2 Create Features/Books handler for AddBookToLibrary
- [ ] 4.3 Move book-specific request/response DTOs into Features/Books

## 5. Authentication Slice

- [ ] 5.1 Create Features/Authentication login handler reusing Common JWT infrastructure
- [ ] 5.2 Move AuthenticationService logic into the Features/Authentication login handler

## 6. Presentation Layer

- [ ] 6.1 Rewrite LibrariesController to delegate to Libraries slice handlers and update namespace to `LibraryService.WebAPI.Presentation.Controllers`
- [ ] 6.2 Rewrite BooksController to delegate to Books slice handlers
- [ ] 6.3 Rewrite AuthController to delegate to the Authentication slice login handler

## 7. Wiring and Configuration

- [ ] 7.1 Update Startup.cs DI registrations to reference Common namespaces and slice handlers
- [ ] 7.2 Update Program.cs namespace if changed
- [ ] 7.3 Update appsettings.json and appsettings.Development.json references if needed

## 8. Verification

- [ ] 8.1 Run `dotnet build` to verify compilation succeeds
- [ ] 8.2 Run `dotnet test` to verify all existing integration tests pass
- [ ] 8.3 Fix any namespace or reference errors from build output