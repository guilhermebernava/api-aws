<div align="center">
  <h1>API-AWS Project</h1>
  <p><i>Development and deployment of a .NET 6 project with a focus on Amazon EC2 and RDS PostgreSQL.</i></p>
</div>

---

<div align="left">
  <h2>🚀 Overview</h2>
</div>

This .NET 6 project is an application that demonstrates the construction of a REST API using JWT for authentication and authorization. The project utilizes Entity Framework to manage access to a PostgreSQL database hosted on Amazon RDS. The repository pattern is applied for data management.

<br>

<div align="left">
  <h2>🛠️ Technologies Used</h2>
</div>

- **.NET 6**
- **Entity Framework Core**
- **Amazon EC2**
- **Amazon RDS**
- **xUnit**
- **AutoMapper**
- **FluentValidation**
- **Jwt**

<br>

<div align="left">
  <h2>🏗️ Development Environment Setup</h2>
</div>

Before starting development and deployment of the project, you need to set up your development environment. Make sure you have:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)
- An AWS account to configure EC2 and RDS.

  ### EC2 and RDS should be created using AWS services.

<br>

<div align="left">
  <h2>📂 Project Structure</h2>
</div>

Each project has its folder, and its tests are together in these folders.

```
├── src/
│   ├── ApiAws.Api/Aws.Api                 # Application API
│   ├── ApiAws.Infra/Aws.Infra             # Data access layer
│   ├── ApiAws.Domain/Aws.Domain           # Domain layer
│   ├── ApiAws.Services/Aws.Services       # Services layer
│   └── ApiAws.Api.sln                     # Visual Studio solution
│
├── .gitignore                   # .gitignore file
├── README.md                    # This file
└── appsettings.json             # Application settings
```

Customize the project structure according to your application's needs.

<br>

<div align="left">
  <h2>📦 Configuration and Deployment</h2>
</div>

1. Clone this repository inside your EC2 instance with .NET and dotnet ef installed:

   ```bash
   git clone https://github.com/guilhermebernava/api-aws.git
   ```

2. Configure environment variables in the `appsettings.json` file with the database connection information.

3. Run Entity Framework migrations to create the database schema:

   ```bash
   dotnet ef database update
   ```

4. Ensure that the files within `launchSettings.json` are configured to access your EC2's URL.

   ```json
   "Prod": {
     "commandName": "Project",
     "dotnetRunMessages": true,
     "launchBrowser": true,
     "launchUrl": "swagger",
     "applicationUrl": "https://+:443;http://+:80",
     "environmentVariables": {
       "ASPNETCORE_ENVIRONMENT": "Prod"
     }
   }
   ```

5. Run the application on your EC2 instance:

   ```bash
   sudo dotnet run
   ```
</div>
