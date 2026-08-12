## Purpose

Manage books within a library — add new books and list all books belonging to a specific library.

## ADDED Requirements

### Requirement: User can list all books for a library
The system SHALL return all books associated with the given library ID.

#### Scenario: Successful retrieval of books for a library
- **WHEN** a GET request is sent to `/api/libraries/{libraryId}/books` and the library exists
- **THEN** the system returns HTTP 200 with a JSON array of all books for that library

#### Scenario: List books for a library that does not exist
- **WHEN** a GET request is sent to `/api/libraries/{libraryId}/books` where the library does not exist
- **THEN** the system returns HTTP 404

### Requirement: User can add a book to a library
The system SHALL add a new book to the specified library.

#### Scenario: Successful book creation
- **WHEN** a POST request is sent to `/api/libraries/{libraryId}/books` with a valid book JSON body and the library exists
- **THEN** the system returns HTTP 201 with the created book JSON object

#### Scenario: Add book to a library that does not exist
- **WHEN** a POST request is sent to `/api/libraries/{libraryId}/books` where the library does not exist
- **THEN** the system returns HTTP 404