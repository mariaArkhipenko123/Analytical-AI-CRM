# CRM Logger Service Backend

This project leverages .NET and follows the clean architecture principles for structured and scalable backend development. It incorporates well-organized layers to separate concerns and improve maintainability

---

## 📂 Directory Structure

```
├── 🖍 README.md                    # Project description
├── 🕠 .git                         # Git repository
├── 🚫 .gitignore                   # Files ignored by git
├── 🐳 .dockerignore                # Files ignored by docker
├── 📆 crm-report-service.sln       # Visual Studio Solution
│
├── 🗂 CRM.ReportService.API        # API layer for handling HTTP requests
│   ├── 🗂 Controllers              # Endpoints for business operations
│   ├── 🗂 Middleware               # Request/response pipeline handlers
│   ├── 🗂 Exceptions               # API-specific exception handling
│   ├── 🕛 appsettings.json         # Application configuration
│   ├── 🕛 CRM.ReportService.API.http # API request examples
│   ├── 🐳 Dockerfile               # Docker configuration for API
│   └── 🚀 Program.cs               # Application entry point
│
├── 🗂 CRM.ReportService.Application # Application logic layer
│   ├── 🗂 CQRS                     # Command and Query segregation
│   ├── 🗂 Exceptions               # Application-specific exception handling
│   ├── 🗂 Extensions               # Extension methods for modularity
│   ├── 🗂 Interfaces               # Abstractions for services and repositories
│   ├── 🗂 Models                   # Data transfer objects (DTOs)
│   ├── 🗂 Services                 # Business logic implementation
│   └── 🗂 Validation               # Input validation logic
│
├── 🗂 CRM.ReportService.Domain      # Domain layer for business entities
│   ├── 🗂 Entities                 # Core domain models
│   └── 🗂 Enums                    # Enumerations for domain logic
│
├── 🗂 CRM.ReportService.Infrastructure # Infrastructure layer for external dependencies
│   ├── 🗂 Contexts                 # Database context and configurations
│   ├── 🗂 Repositories             # Data access logic
│   ├── 🗂 UnitOfWork               # Transactional data handling
│   ├── 🗂 Exceptions               # Infrastructure-specific exception handling
│   └── 🗂 Extensions               # Infrastructure utilities and helpers
│
├── 🗂 CRM.ReportService.Test.Integration # Integration testing project
│
└── 🗂 CRM.ReportService.Test.Unit  # Unit testing project
```

---

## 🚀 Getting Started

### **Development Mode**
1. Install the latest version of `.NET Core SDK`.
2. Clone this repository: `git clone <repo-url>`.
3. Open the solution file `crm-logger-service.sln` in Visual Studio.
4. Dotnet run --project CRM.ReportService.API


### **Docker Setup**
1. Install Docker and Docker Compose.
2. Use the provided `docker-compose.yml` (if available).
3. Build and start the services: `docker-compose up --build`.

---

## 🧪 Testing

### **Unit Tests**
- Navigate to the `CRM.ReportService.Test.Unit` directory.
- Run: `dotnet test`.

### **Integration Tests**
- Navigate to the `CRM.ReportService.Test.Integration` directory.
- Run: `dotnet test`.

---
## 🗒️ Additional Notes

- Ensure environment variables are configured properly in appsettings.json or using Docker ENV files.
- CI/CD pipelines can be integrated using tools like Azure DevOps or GitHub Actions.
- Use docker-compose.override.yml for local development to override production settings.

---
