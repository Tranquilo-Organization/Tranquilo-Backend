## Overview

TranquiloSystem is a backend system built using ASP.NET Core Web API, designed with a layered architecture to separate concerns and improve maintainability. The system consists of the following layers:

### 1. API Layer
This layer is responsible for handling HTTP requests and providing endpoints for various operations. Current functionalities include:

- **Controllers:** Defines API endpoints and handles HTTP requests. Currently, the AccountController includes endpoints for user login and registration.
- **Migrations:** Database migration files for managing schema updates.

### 2. Business Logic Layer (BLL)
This layer contains the core business logic, such as handling the data flow between the API and DAL. Key components include:

- **Managers:** Handles business rules and logic.
- **DTOs (Data Transfer Objects):** Used to transfer data between layers.
- **AutoMapper Profiles:** Used to map between DTOs and domain models.

### 3. Data Access Layer (DAL)
This layer manages all interactions with the database, including CRUD operations. It includes:

- **Models:** Represents the database tables and their relationships.
- **DbContext:** Entity Framework Core context to manage database connections and queries.
- **Repositories:** Implements data access logic for CRUD operations and querying the database.

## Technologies Used

- **ASP.NET Core Web API** for building the API.
- **AutoMapper** for object mapping between layers.
- **Entity Framework Core** for database access and management.
- **JWT (JSON Web Token)** for secure authentication.
- **ASP.NET Core Identity** for managing user roles and authentication.
- **Swashbuckle** for generating API documentation with Swagger.

## Dependencies

### API Layer
- `Microsoft.AspNetCore.Authentication.JwtBearer` Version 8.0.8
- `Microsoft.EntityFrameworkCore.Design` Version 8.0.8
- `Microsoft.IdentityModel.Tokens` Version 8.0.2
- `Swashbuckle.AspNetCore` Version 6.4.0
- `System.IdentityModel.Tokens.Jwt` Version 8.0.2

### Business Logic Layer (BLL)
-  `AutoMapper` Version 13.0.1


### Data Access Layer (DAL)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` Version 8.0.8
- `Microsoft.EntityFrameworkCore` Version 8.0.8
- `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.8
- `Microsoft.EntityFrameworkCore.Tools` Version 8.0.8
  
## Installation

1. **Clone the Repo:**

   Clone the repository to your local machine:

   ```bash
   git clone https://github.com/Tranquilo-Organization/Tranquilo-Backend.git

2. **Install packages:**
   ```bash
   dotnet restore

## Configuration

In the `appsettings.json` file, the following configurations are used:
 
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\ProjectModels;Initial Catalog=Tranquilo;Integrated Security=True"
  },
  "Key": "75d19b579976fcd772a2c502b98fdaa4675f7fbda75f238eb0f0c357c574a4e2"
}
```
## Database Scheme
![Database Schema](./Erd/finalErd.PNG)

### Tables:
- ***Identity tables.***
- ***BotConversation.***
- ***Level.***
- ***Message.***
- ***Notification.***
- ***Post***
- ***PostComment.***
- ***Routine.***
- ***SurveyAnswer***
- ***SurveyQuestion***
- ***UserRoutine***
- ***UserScore***

### Current Status
Currently, the authentication endpoints for user registration and login have been implemented. Future features will be added soon.
