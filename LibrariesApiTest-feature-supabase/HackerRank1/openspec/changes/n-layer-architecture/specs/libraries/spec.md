## Purpose

Manage library entities — create, retrieve, update, and delete libraries with their associated metadata.

## ADDED Requirements

### Requirement: User can retrieve all libraries
The system SHALL return a list of all libraries.

#### Scenario: Successful retrieval of all libraries
- **WHEN** a GET request is sent to `/api/libraries`
- **THEN** the system returns HTTP 200 with a JSON array of all libraries

### Requirement: User can retrieve a library by ID
The system SHALL return a single library for a given ID.

#### Scenario: Successful retrieval of a library by ID
- **WHEN** a GET request is sent to `/api/libraries/{libraryId}` where the library exists
- **THEN** the system returns HTTP 200 with the library JSON object

#### Scenario: Library not found
- **WHEN** a GET request is sent to `/api/libraries/{libraryId}` where the library does not exist
- **THEN** the system returns HTTP 404

### Requirement: User can create a new library
The system SHALL accept a new library object and persist it.

#### Scenario: Successful library creation
- **WHEN** a POST request is sent to `/api/libraries` with a valid library JSON body
- **THEN** the system returns HTTP 200 with the created library object

### Requirement: User can update an existing library
The system SHALL update the name and location of an existing library.

#### Scenario: Successful library update
- **WHEN** a PUT request is sent to `/api/libraries/{libraryId}` with a valid library JSON body and the library exists
- **THEN** the system returns HTTP 204 with no body

#### Scenario: Update library that does not exist
- **WHEN** a PUT request is sent to `/api/libraries/{libraryId}` where the library does not exist
- **THEN** the system returns HTTP 404

### Requirement: User can delete a library
The system SHALL remove a library by ID.

#### Scenario: Successful library deletion
- **WHEN** a DELETE request is sent to `/api/libraries/{libraryId}` where the library exists
- **THEN** the system returns HTTP 204 with no body

#### Scenario: Delete library that does not exist
- **WHEN** a DELETE request is sent to `/api/libraries/{libraryId}` where the library does not exist
- **THEN** the system returns HTTP 404