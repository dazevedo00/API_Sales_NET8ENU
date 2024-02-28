# API_Sales_NET8

## Project Description

This project is an ongoing application aiming to address various scenarios and patterns in .NET 8. Some of the elements used include DTOs, Pattern Strategy, Integration Tests, Code First, and ILogger. One pending task is to delve deeper into the Mediator pattern.

## Technologies Used

- **Language:** C#
- **.NET Version:** 8
- **Libraries and Frameworks:**
  - Entity Framework Core
  - ILogger

## Environment and Database Configuration

### Step 1: Prerequisites

Ensure that Entity Framework Core is installed in your project. Additionally, a local database server needs to be set up.

### Step 2: Database Server Configuration

Open the `appsettings.json` file and modify the connection string under `ConnectionStrings` for the desired server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourNewServer;Database=DatabaseName;Trusted_Connection=True;"
  }
}
```

### Step 3: Database Update

1. Open the Package Manager Console in Visual Studio.
2. Navigate to your project directory.
   ```
   PM> cd Path/To/Your/Project
   ```
3. Execute the command to apply migrations.
   ```
   PM> Update-Database
   ```

### Changing the Database Server

If you wish to change the database server, follow these steps:

1. Open the `appsettings.json` file.
2. Modify the value of `Server` in the connection string under `ConnectionStrings` to the new address.
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=NewServerAddress;Database=DatabaseName;Trusted_Connection=True;"
     }
   }
   ```
3. Remember to apply migrations again after the change.

## Pending Tasks

- Deepen understanding of the Mediator pattern.

---

Feel free to add more sections as needed, such as execution instructions, development environment setup, among others.