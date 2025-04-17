# StateHub API

### .NET v8.0.300

### Description

This RESTful API provides programmatic access to the database of governmental entities in the Dominican Republic. It supports full CRUD operations (create, read, update, and delete) for managing entities, ensuring secure and standardized communication for integration with other platforms, applications, or government systems.

## Run the project

Go to the root of the project:

**_`/src`_**

Then run:

**_`dotnet run --project SB.StateHub.API`_**

## Using Entity Framework command line tool

**_`dotnet tool install --global dotnet-ef --version 8.0.3`_**

### Add migration

**_`dotnet ef migrations add <migration-name> --startup-project SB.StateHub.API --project SB.StateHub.Infrastructure --context MainDbContext`_**

### Update database

**_`dotnet ef database update --startup-project SB.StateHub.API --project SB.StateHub.Infrastructure --context MainDbContext`_**

