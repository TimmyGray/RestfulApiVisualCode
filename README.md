# RestfulApiVisualCode

ASP.NET Core service for incident/event registration, incident image storage, glossary page management, and cookie-based user authorization.

## Project Overview

This API supports:

- Creating, viewing, updating, and deleting technical events/incidents
- Uploading and retrieving images linked to events
- Managing glossary/information pages for equipment/processes
- Register/login/logout using cookie authentication and role-based authorization

Data is persisted in SQLite with Entity Framework Core migrations.

## Technology Stack

- .NET 10 (ASP.NET Core)
- C# (latest language version from SDK)
- Entity Framework Core 10 + SQLite
- Cookie Authentication / Authorization
- xUnit for unit tests
- Docker Compose (optional containerized run)

## Repository Structure

- `Controllers/` — API controllers (`Events`, `Images`, `Pages`, `Users`)
- `Models/` — domain models (`Event`, `Image`, `Page`, `User`, `Role`)
- `DataBaseContext/` — EF Core `DbContext` and database startup migration logic
- `Migrations/` — EF Core migrations
- `wwwroot/` — static frontend resources
- `RestfulApiVisualCode.Tests/` — unit tests for critical controller behavior

## Prerequisites

- .NET SDK 10.0+
- (Optional) Docker + Docker Compose

## Configuration

Connection string is stored in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Filename = ./database/eventdbstore.db;"
}
```

### Email Notifications

For serious incidents, the API attempts to send an email using SMTP.

Create:

- `ForEmailSending/Email.txt`

With:

1. Sender email (line 1)
2. Sender password (line 2)

If this file does not exist, email sending is skipped by controller error handling.

## Run Locally

```bash
dotnet restore RestfulApiVisualCode.sln
dotnet build RestfulApiVisualCode.sln
dotnet run --project /home/runner/work/RestfulApiVisualCode/RestfulApiVisualCode/RestfulApiVisualCode.csproj
```

By default, app launch settings use `http://localhost:5000`.

## Run Tests

```bash
dotnet test RestfulApiVisualCode.sln
```

Current critical tests cover:

- Safe behavior on missing event reads/deletes
- Validation for future event date
- Bad-request behavior for invalid login
- Bad-request behavior for missing page subheader
- Bad-request behavior for image upload linked to missing event

## Run with Docker

```bash
docker compose up --build
```

Container publishes API on host port `5000`.

## API Notes

- Most domain endpoints require authenticated user
- Delete endpoints for events/pages require `Admin` role
- `UsersController` supports:
  - `POST /Users/login`
  - `POST /Users/register`
  - `GET /Users/logout`

## Development Notes

- Database migrations are applied automatically at startup
- Static files are enabled, with `authorize.html` as the default file
- Legacy startup class was replaced by modern minimal hosting (`Program.cs`)
