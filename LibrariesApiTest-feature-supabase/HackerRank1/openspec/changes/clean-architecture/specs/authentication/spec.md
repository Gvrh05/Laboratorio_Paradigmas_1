## Purpose

Authenticate users and issue JWT tokens for authorized access to the API.

## ADDED Requirements

### Requirement: User can log in with valid credentials
The system SHALL authenticate a user with email and password and return a JWT token.

#### Scenario: Successful login with valid credentials
- **WHEN** a POST request is sent to `/login` with a valid email and password
- **THEN** the system returns HTTP 200 with a JSON object containing a JWT token

### Requirement: User cannot log in with invalid credentials
The system SHALL reject authentication for invalid email or password.

#### Scenario: Login with invalid credentials
- **WHEN** a POST request is sent to `/login` with an invalid email or password
- **THEN** the system returns HTTP 401 Unauthorized with no token