# File Service

## 📄 Description

The **File Service** is an ASP.NET Web API designed to interact with reports that need to transfer to PDF or Excel file. It accepts reports, create files according reports, and returns the generated files.
The main purpouse of this service is to generate PDF/Exceel files and provide files with reports based on user prompts.

## 📂 Directory Structure
```
crm-file-service
├── 📂 API                                # Handles HTTP requests and responses
│   ├── Controllers                    # API endpoints and logic
│   ├── Exceptions                     # Custom exception handling for API
│   ├── Middleware                     # Request/response middleware
│   ├── Properties                     # Configuration-related files
│   ├── Program.cs                     # Application entry point
│   └── appsettings.json               # Application settings (e.g., database connections)
│
├── 📂 Application                        # Core application logic and services
│   ├── Exceptions                     # Custom exceptions for application layer
│   ├── Extensions                     # Application-wide extensions
│   ├── Interfaces                     # Contracts and abstractions for services
│   ├── Models                         # Data transfer objects and request/response models
│   ├── Services                       # Business logic implementation
│   └── Utils                          # Helper utilities for the application
│       └── Validation                 # Data validation utilities
│
├── 📂 Domain                             # Core entities and domain logic
│   ├── Entities                       # Domain models representing data structures
│   └── Enums                          # Enumerations for domain-specific constants
│
├── 📂 Infrastructure                     # External integrations and implementations
│   ├── Contexts                        # Database context and configuration
│   ├── Exceptions                     # Custom exceptions related to infrastructure
│   ├── Extensions                     # Infrastructure-specific extensions
│   ├── Migrations                      # Database migrations
│   ├── Repositories                    # Data access layer
│   ├── UoW                             # Unit of Work implementation
│   ├── MessageBroker                   # Redis Message Broker implementation
│   └── Services                       # Infrastructure service implementations
│
├── 📂 Unit                               # Unit tests for the application
├── 📂 Integration                        # Integration tests for the application
├── 🚫 .gitignore                         # Files and folders ignored by Git
└── 📝 README.md                          # Project description and setup instructions
```

## 🚀 Getting Started

### **Development Mode**
1. Install the latest version of `.NET Core SDK`.
2. Clone this repository: `git clone <repo-url>`.
3. Open the solution file `crm-logger-service.sln` in Visual Studio.
4. Dotnet run --project CRM.FileService.API


### **Docker Setup**
1. Install Docker and Docker Compose.
2. Use the provided `docker-compose.yml` (if available).
3. Build and start the services: `docker-compose up --build`.

---

## 🧪 Testing

### **Unit Tests**
- Navigate to the `CRM.FileService.Test.Unit` directory.
- Run: `dotnet test`.

### **Integration Tests**
- Navigate to the `CRM.FileService.Test.Integration` directory.
- Run: `dotnet test`.


##  CI/CD Integration

Changes pushed to the **master** branch trigger the following steps:

	1. Automated Testing:
		All unit and integration tests are executed.
	2. Deployment Pipeline Trigger:
		Upon successful test completion, the pipeline triggers the deployment workflow in the crm-deployment repository, initiating the production release process.
