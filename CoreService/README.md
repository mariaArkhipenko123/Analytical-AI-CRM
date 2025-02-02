
# **\_\_** Backend

This application harnesses the power of ASP.NET and a PostgreSQL database project.


```
## Directory Structure

├── 📝 README.md # Project description
├── 🛠 .git # Git repository
├── 🚫 .gitignore # Files ignored by git
├── 🐳 .dockerignore # Files ignored by docker
├── 🔄 bitbucket-pipelines.yml # CI/CD setup with Bitbucket
│
├── 📂 CRM.CoreService.API            # API layer
│   ├── Connected Services              # Service references for external APIs
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Properties                      # Project-specific properties
│   ├── Controllers                     # API endpoints
│   ├── Exceptions                      # Exception handling classes
│   ├── Middleware                      # Custom middleware for request handling
│   ├── appsettings.json                # Configuration file for environment-specific settings
│   ├── CRM.CoreService.API.http      # HTTP request examples for testing
│   ├── Dockerfile                      # Docker configuration for the API
│   ├── Program.cs                      # Application entry point
├── 📂 CRM.CoreService.Application    # Application layer
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── CQRS                            # Command Query Responsibility Segregation logic
│   ├── Exceptions                      # Application-specific exceptions
│   ├── Extensions                      # Extension methods for reusability
│   ├── Interfaces                      # Interfaces for dependency injection
│   ├── GraphQL                         # Queries, Commands and Handlers for GraphQL
│   ├── Models                          # Application data models
│   ├── Services                        # Business logic services
│   ├── Utils                           # Utility classes and helpers
├── 📂 CRM.CoreService.Domain         # Domain models and logic
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Entities                        # Core entities representing database tables
├── 📂 CRM.CoreService.Infrastructure # Infrastructure dependencies
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── AuthenticationService           # Authentication services via different providers
│   ├── Contexts                        # Database context and configuration
│   ├── Exceptions                      # Infrastructure-specific exceptions
│   ├── Extensions                      # Infrastructure-related extensions
│   ├── Migrations                      # Database migrations
│   ├── Repositories                    # Data access layer
│   ├── TokenProviders                  # JWT tokens providers
│   ├── UoW                             # Unit of Work implementation
├── 📂 CRM.CoreService.UnitTests        # Unit tests
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Test files                      # Unit test cases for individual components
├── 📂 CRM.CoreService.IntegrationTests # Integration tests
│   ├── Dependencies                    # NuGet and project dependencies
│   ├── Test files                      # Integration test cases to validate component interaction
```

## 🚀 Getting Started

### **Development Mode**

1. Install the latest version of `.NET Core SDK`.
2. Clone this repository: `git clone <repo-url>`.
3. Open the solution file `crm-core-service.sln` in Visual Studio.
4. Configure the `appsettings.json` file in the `CRM.CoreService.API` folder to connect to your MongoDB instance.
5. Build and run the project.

### **Docker Setup**

1. Install Docker and Docker Compose.
2. Use the provided `docker-compose.yml` (if available).
3. Build and start the services: `docker-compose up --build`.

## 🧪 Testing

### **Unit Tests**

- Navigate to the `CRM.CoreService.UnitTests` directory.
- Run: `dotnet test`.

### **Integration Tests**

- Navigate to the `CRM.CoreService.IntegrationTests` directory.
- Run: `dotnet test`.

## 🔄 Migrations

- To run migrations in a project, specify the migration class in the appropriate folder and write the command
- in the terminal dotnet run -- --migrate
- if you want to roll back the migration dotnet run -- --rollback

## 🔄 CI/CD Integration

- Integrate with a CI/CD tool like GitHub Actions, Azure Pipelines, or Jenkins for automated testing and deployment.

