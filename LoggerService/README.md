# CRM Logger Service Backend

This application leverages the power of **.NET** and a **MongoDB** database to manage logging services.

---

## 📂 Directory Structure

```
├── 📝 README.md               # Project description
├── 🛠 .git                    # Git repository
├── 🚫 .gitignore              # Files ignored by git
├── 🐳 .dockerignore           # Files ignored by docker
├── crm-logger-service.sln     # Visual Studio solution
├── MongoDB.png                # MongoDB schema or related image
│
├── 📂 CRM.LoggerService.API            # API layer
│   ├── Connected Services              # Service references for external APIs
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Properties                      # Project-specific properties
│   ├── Controllers                     # API endpoints
│   ├── Exceptions                      # Exception handling classes
│   ├── Middleware                      # Custom middleware for request handling
│   ├── appsettings.json                # Configuration file for environment-specific settings
│   ├── CRM.LoggerService.API.http      # HTTP request examples for testing
│   ├── Dockerfile                      # Docker configuration for the API
│   ├── Program.cs                      # Application entry point
├── 📂 CRM.LoggerService.Application    # Application layer
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── CQRS                            # Command Query Responsibility Segregation logic
│   ├── Exceptions                      # Application-specific exceptions
│   ├── Extensions                      # Extension methods for reusability
│   ├── Interfaces                      # Interfaces for dependency injection
│   ├── Models                          # Application data models
│   ├── Services                        # Business logic services
│   ├── Utils                           # Utility classes and helpers
├── 📂 CRM.LoggerService.Domain         # Domain models and logic
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Entities                        # Core entities representing database tables
│   ├── Enums                           # Enumeration types
├── 📂 CRM.LoggerService.Infrastructure # Infrastructure dependencies
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Data                            # Database context and configuration
│   ├── Exceptions                      # Infrastructure-specific exceptions
│   ├── Extensions                      # Infrastructure-related extensions
│   ├── Migrations                      # Database migrations
│   ├── Repositories                    # Data access layer
│   ├── Services                        # Infrastructure services
│   ├── UoW                             # Unit of Work implementation
├── 📂 CRM.LoggerService.Tests.Unit     # Unit tests
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Test files                      # Unit test cases for individual components
├── 📂 CRM.LoggerService.Tests.Integration # Integration tests
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Test files                      # Integration test cases to validate component interaction
```

---

## 🚀 Getting Started

### **Development Mode**
1. Install the latest version of `.NET Core SDK`.
2. Clone this repository: `git clone <repo-url>`.
3. Open the solution file `crm-logger-service.sln` in Visual Studio.
4. Configure the `appsettings.json` file in the `CRM.LoggerService.API` folder to connect to your MongoDB instance.
5. Build and run the project.

### **Docker Setup**
1. Install Docker and Docker Compose.
2. Use the provided `docker-compose.yml` (if available).
3. Build and start the services: `docker-compose up --build`.

---

## 🧪 Testing

### **Unit Tests**
- Navigate to the `CRM.LoggerService.Tests.Unit` directory.
- Run: `dotnet test`.

### **Integration Tests**
- Navigate to the `CRM.LoggerService.Tests.Integration` directory.
- Run: `dotnet test`.

---
## 🗒️ Additional Notes

- The MongoDB schema or structure is included in the `MongoDB.png` file.
- Follow the environment setup described in `secrets.json` for staging or production deployments.
---
## 🔄 Migrations
- To run migrations in a project, specify the migration class in the appropriate folder and write the command 
- in the terminal dotnet run -- --migrate
- if you want to roll back the migration dotnet run -- --rollback
---

## 🔄 CI/CD Integration

- Integrate with a CI/CD tool like GitHub Actions, Azure Pipelines, or Jenkins for automated testing and deployment.
