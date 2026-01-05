# TodoListApp — Personal Task Planner

A fullstack **personal task planning application** built with **ASP.NET Core Web API** and a **Blazor** frontend.
The application allows users to organize their daily work by creating **task lists** and **tasks inside those lists**.

The project focuses on clean architecture, authentication, and clear separation of responsibilities.
The UI is intentionally **simple and minimal**, with the main focus on functionality and logic.

---

## ✨ Features
- User registration and login
- JWT authentication and authorization
- Create and manage task lists
- Create, update, and delete tasks inside lists
- Layered (Onion/Clean-style) architecture
- REST API with controllers
- Swagger documentation with JWT support
- Global error handling middleware

---

## 🧱 Tech Stack

### Backend
- C# / ASP.NET Core Web API
- REST Controllers
- Entity Framework Core
- ASP.NET Core Identity
- JWT (Bearer authentication)
- Swagger (OpenAPI)

### Database
- SQL Server LocalDB
- Separate DbContexts:
  - UsersDb (Identity & authentication)
  - TodoListDb (application data)

### Frontend
- Blazor
- UI styling libraries (simple and minimal design)

---

## 🏗️ Architecture
The solution follows a **layered Onion (Clean) architecture**:

- **Contracts** — interfaces and abstractions
- **Entities** — domain entities
- **Infrastructure** — EF Core, repositories, Identity
- **Services** — business logic and mappings
- **WebApi** — controllers, middleware, dependency injection

This approach improves maintainability, scalability, and testability.

---

## 🚀 Running the Project (Local)

The project uses **launch profiles** defined in `launchSettings.json`.

### Backend — ASP.NET Core Web API
1. Navigate to the Web API project folder:
```bash
cd TodoListApp.WebApi

2. Run the API using the launch profile:
dotnet run --launch-profile "TodoListApp.WebApi"

The API will be available at:
https://localhost:7180/
http://localhost:5017/

Swagger UI:
https://localhost:7180/swagger

Frontend — Blazor Web App

1. Navigate to the Blazor project folder:
```bash

cd TodoListApp.WebApp

2. Run the Blazor application:

dotnet run --launch-profile "TodoListApp.WebApp"


### Prerequisites
- .NET SDK
- SQL Server LocalDB
- Visual Studio / Rider (optional)

### Clone the repository
```bash
git clone <YOUR_REPOSITORY_URL>
cd <YOUR_PROJECT_FOLDER>

Configuration

Update connection strings in appsettings.json:

{
  "ConnectionStrings": {
    "UsersDb": "Server=(localdb)\\mssqllocaldb;Database=UsersDb;Trusted_Connection=True;",
    "TodoListDb": "Server=(localdb)\\mssqllocaldb;Database=TodoListDb;Trusted_Connection=True;"
  }
}

JWT settings:

{
  "Jwt": {
    "Issuer": "TodoListApp",
    "Audience": "TodoListAppClient",
    "Key": "CHANGE_THIS_SECRET_KEY"
  }
}

Run the Backend

cd TodoListApp.WebApi
dotnet run

Swagger UI:

https://localhost:7180/swagger

Run the Frontend

cd <BLAZOR_PROJECT_FOLDER>
dotnet run

🔐 Authentication

The API uses JWT Bearer authentication.

To access protected endpoints in Swagger:

    Login to obtain a JWT token

    Click Authorize

    Enter:

    Bearer YOUR_TOKEN

🧪 Seed Data

Roles and an admin user are seeded automatically in development mode.
📸 Screenshots

    Add screenshots to the /screenshots folder.

    Login / Register

    Task lists

    Tasks inside a list

    Swagger UI

🗺️ Future Improvements

    Refresh tokens

    Unit and integration tests

    Docker support

    UI/UX improvements