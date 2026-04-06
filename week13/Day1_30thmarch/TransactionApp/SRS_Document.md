# Software Requirements Specification (SRS)
## Transaction History Web Application with Secure API

---

## 1. Introduction

### 1.1 Purpose
This document describes the functional and non-functional requirements for the Transaction History Web Application. The system allows authenticated users to securely view, add, edit, and delete their transaction history.

### 1.2 Scope
The system consists of:
- A frontend web interface (HTML, CSS, JavaScript)
- A backend REST API (ASP.NET Core Web API)
- A relational database (SQL Server)
- JWT-based authentication

### 1.3 Technologies Used
- Frontend: HTML5, CSS3, JavaScript
- Backend: ASP.NET Core Web API (.NET 8)
- Database: SQL Server LocalDB
- ORM: Entity Framework Core
- Authentication: JWT (JSON Web Tokens)
- Mapping: AutoMapper
- Pattern: DTO (Data Transfer Object)

---

## 2. Overall Description

### 2.1 Product Perspective
The application is a web-based system where users can log in securely and manage their personal transaction records. The frontend communicates with the backend API using HTTP requests with JWT authentication.

### 2.2 User Classes
| User Type | Description |
|-----------|-------------|
| Registered User | Can login, view, add, edit and delete their own transactions |
| System Admin | Can manage database directly |

### 2.3 System Architecture
@"
# Software Requirements Specification (SRS)
## Transaction History Web Application with Secure API

---

## 1. Introduction

### 1.1 Purpose
This document describes the functional and non-functional requirements for the Transaction History Web Application. The system allows authenticated users to securely view, add, edit, and delete their transaction history.

### 1.2 Scope
The system consists of:
- A frontend web interface (HTML, CSS, JavaScript)
- A backend REST API (ASP.NET Core Web API)
- A relational database (SQL Server)
- JWT-based authentication

### 1.3 Technologies Used
- Frontend: HTML5, CSS3, JavaScript
- Backend: ASP.NET Core Web API (.NET 8)
- Database: SQL Server LocalDB
- ORM: Entity Framework Core
- Authentication: JWT (JSON Web Tokens)
- Mapping: AutoMapper
- Pattern: DTO (Data Transfer Object)

---

## 2. Overall Description

### 2.1 Product Perspective
The application is a web-based system where users can log in securely and manage their personal transaction records. The frontend communicates with the backend API using HTTP requests with JWT authentication.

### 2.2 User Classes
| User Type | Description |
|-----------|-------------|
| Registered User | Can login, view, add, edit and delete their own transactions |
| System Admin | Can manage database directly |

### 2.3 System Architecture
`
[Browser] <--HTTP--> [ASP.NET Core API] <--EF Core--> [SQL Server DB]
`

---

## 3. Functional Requirements

### FR-01: User Login
- The system shall provide a login form with username and password fields
- The system shall validate that fields are not empty before submitting
- The system shall return a JWT token on successful login
- The system shall show an error message on invalid credentials

### FR-02: JWT Authentication
- The system shall generate a JWT token upon successful login
- The token shall expire after 24 hours
- All protected endpoints shall require a valid JWT token
- The system shall return 401 Unauthorized if token is missing or invalid

### FR-03: View Transactions
- The system shall display all transactions belonging to the logged-in user
- Transactions shall be displayed in a table with Amount, Date and Type columns
- The system shall only return transactions belonging to the authenticated user
- Transactions shall be ordered by date descending

### FR-04: Add Transaction
- The system shall provide a form to add a new transaction
- The form shall have Amount (number) and Type (Credit/Debit) fields
- The system shall validate that amount is not empty and greater than 0
- The new transaction shall be saved to the database with the current date
- The table shall refresh automatically after adding

### FR-05: Edit Transaction
- The system shall provide an Edit button for each transaction row
- Clicking Edit shall open a modal popup with current values
- The user shall be able to update Amount and Type
- Changes shall be saved to the database immediately
- The table shall refresh automatically after editing

### FR-06: Delete Transaction
- The system shall provide a Delete button for each transaction row
- Clicking Delete shall show a confirmation dialog
- On confirmation, the transaction shall be permanently deleted from database
- The table shall refresh automatically after deleting

### FR-07: Data Security
- Users shall only see their own transactions
- Internal transaction IDs shall be exposed only for edit/delete operations
- Passwords shall be stored as BCrypt hashes, never plain text
- All API communication shall use JWT Bearer tokens

### FR-08: Logout
- The system shall provide a Logout button on the dashboard
- Clicking Logout shall clear the JWT token from local storage
- The user shall be redirected to the login page after logout

---

## 4. Non-Functional Requirements

### NFR-01: Security
- All passwords must be hashed using BCrypt before storing
- JWT tokens must be signed with HMAC-SHA256 algorithm
- API must use HTTPS in production
- CORS policy must be configured to allow only trusted origins

### NFR-02: Performance
- Database queries must filter by UserId at the database level
- API response time should be under 500ms for normal operations
- EF Core queries should use .Where() before .ToList() to avoid loading all records

### NFR-03: Usability
- Login form must show clear error messages for invalid input
- Transaction table must refresh automatically after any change
- Edit modal must pre-fill current values for easy editing
- Confirmation dialog must appear before deleting a transaction

### NFR-04: Maintainability
- DTOs must be used to shape API responses
- AutoMapper must be used to map between entities and DTOs
- Business logic must be separated from controllers
- Code must follow clean architecture principles

### NFR-05: Reliability
- The system must handle unauthorized access gracefully
- The system must return appropriate HTTP status codes
- Database transactions must be atomic

---

## 5. Use Cases

### UC-01: User Login
- Actor: Registered User
- Precondition: User has valid credentials
- Main Flow:
  1. User opens login page
  2. User enters username and password
  3. System validates input
  4. System verifies credentials against database
  5. System generates JWT token
  6. User is redirected to dashboard
- Alternative Flow:
  - If credentials are invalid, system shows error message
  - If fields are empty, system shows validation error

### UC-02: View Transactions
- Actor: Authenticated User
- Precondition: User is logged in with valid JWT token
- Main Flow:
  1. User opens dashboard
  2. System reads JWT token from local storage
  3. System sends GET request with Bearer token
  4. API validates token and extracts UserId
  5. System fetches transactions for that UserId only
  6. Transactions are displayed in table

### UC-03: Add Transaction
- Actor: Authenticated User
- Precondition: User is logged in
- Main Flow:
  1. User enters amount and selects type
  2. User clicks Add Transaction
  3. System validates input
  4. API saves transaction with current UserId and date
  5. Updated transaction list is returned and displayed

### UC-04: Edit Transaction
- Actor: Authenticated User
- Precondition: User is logged in and transaction exists
- Main Flow:
  1. User clicks Edit on a transaction row
  2. Modal popup opens with current values
  3. User updates amount or type
  4. User clicks Save
  5. API updates the transaction in database
  6. Updated list is refreshed in table

### UC-05: Delete Transaction
- Actor: Authenticated User
- Precondition: User is logged in and transaction exists
- Main Flow:
  1. User clicks Delete on a transaction row
  2. Confirmation dialog appears
  3. User confirms deletion
  4. API deletes transaction from database
  5. Updated list is refreshed in table

---

## 6. Database Schema

### Users Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | INT | PRIMARY KEY, IDENTITY |
| Username | NVARCHAR | NOT NULL, UNIQUE |
| PasswordHash | NVARCHAR | NOT NULL |

### Transactions Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | INT | PRIMARY KEY, IDENTITY |
| Amount | DECIMAL(18,2) | NOT NULL |
| Date | DATETIME2 | NOT NULL |
| Type | NVARCHAR | NOT NULL (Credit/Debit) |
| UserId | INT | FOREIGN KEY -> Users.Id |

---

## 7. API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/login | No | Login and get JWT token |
| GET | /api/transactions | Yes | Get all user transactions |
| POST | /api/transactions | Yes | Add new transaction |
| PUT | /api/transactions/{id} | Yes | Update transaction |
| DELETE | /api/transactions/{id} | Yes | Delete transaction |

---

## 8. Security Design

### Authentication Flow
1. User sends username + password to /api/auth/login
2. Server verifies BCrypt hash
3. Server generates JWT with UserId claim
4. Client stores token in localStorage
5. Client sends token in Authorization header for every request
6. Server validates token and extracts UserId
7. Server only returns data belonging to that UserId

---
