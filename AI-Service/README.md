# AI Service

## 📄 Description

The **AI Service** is an ASP.NET Web API designed to interact with large language models (LLMs). It accepts prompts, sends them to a configured LLM, and returns the generated responses.
The main purpouse of this service is to generate SQL queries and help build XML/html reports based on user prompts.

## 📂 Directory Structure
```
crm-ai-service
├── 📂 API                                # Handles HTTP requests and responses
│   ├── 📄 Controllers                    # API endpoints and logic
│   ├── ⚠️ Exceptions                     # Custom exception handling for API
│   ├── 🛡️ Middleware                     # Request/response middleware
│   ├── 🏷️ Properties                     # Configuration-related files
│   ├── 🚀 Program.cs                     # Application entry point
│   └── ⚙️ appsettings.json               # Application settings (e.g., database connections)
│
├── 📂 Application                        # Core application logic and services
│   ├── 📂 CQRS                           # Command and Query handlers for business logic
│   ├── ⚠️ Exceptions                     # Custom exceptions for application layer
│   ├── 🛠️ Extensions                     # Application-wide extensions
│   ├── 🔌 Interfaces                     # Contracts and abstractions for services
│   ├── 📦 Models                         # Data transfer objects and request/response models
│   ├── ⚙️ Services                       # Business logic implementation
│   └── 📂 Utils                          # Helper utilities for the application
│       └── ✅ Validation                 # Data validation utilities
│
├── 📂 Domain                             # Core entities and domain logic
│   ├── 🧱 Entities                       # Domain models representing data structures
│   └── 🗂️ Enums                          # Enumerations for domain-specific constants
│
├── 📂 Infrastructure                     # External integrations and implementations
│   ├── ⚠️ Exceptions                     # Custom exceptions related to infrastructure
│   ├── 🛠️ Extensions                     # Infrastructure-specific extensions
│   └── 🔌 Services                       # Infrastructure service implementations
│
├── 📂 Unit                               # Unit tests for the application
├── 📂 Integration                        # Integration tests for the application
├── 🚫 .gitignore                         # Files and folders ignored by Git
└── 📝 README.md                          # Project description and setup instructions
```

## 🚀 Installation

### Prerequisites
- **Docker**: Ensure you have Docker installed on your machine.
- **Secrets**: WIP

### Steps
1. Clone this repository:
   ```sh
   git clone https://github.com/your-repo/ai-microservice.git
   cd ai-microservice
   ```
2. Build docker image and run the container:
   ```sh
   docker build -f crm/ai-service .
   docker run -d -p 8080:80 --name ai-service crm/ai-service
   ```
3. The service will now be available at http://localhost:8080.


## How it works

**WIP**

##  CI/CD Integration

Changes pushed to the **master** branch trigger the following steps:

	1. Automated Testing:
		All unit and integration tests are executed.
	2. Deployment Pipeline Trigger:
		Upon successful test completion, the pipeline triggers the deployment workflow in the crm-deployment repository, initiating the production release process.
