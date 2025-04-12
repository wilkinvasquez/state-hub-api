# StateHub API

### Description

This RESTful API provides programmatic access to the database of governmental entities in the Dominican Republic. It supports full CRUD operations (create, read, update, and delete) for managing entities, ensuring secure and standardized communication for integration with other platforms, applications, or government systems.

## Install Entity Framework command line tool

**_`dotnet tool install --global dotnet-ef`_**

Then go to the root of the project.

### Add migration

**_`dotnet ef migrations add <migration-name> --startup-project SB.StateHub.API --project SB.StateHub.Infrastructure --context MainDbContext`_**

### Update database

**_`dotnet ef database update --startup-project SB.StateHub.API --project SB.StateHub.Infrastructure --context MainDbContext`_**

### Run the project

**_`dotnet run --project SB.StateHub.API`_**
