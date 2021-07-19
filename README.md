# Clean Base

## Introduction

This is a simple .NET 5 WebApi project base with the following features implemented:
1. Swagger with Firebase login
1. Serilog Logging to Amazon CloudWatch
1. Encrypted fields in AppSettings
1. Soft Delete and Audit Columns
1. Multitenancy support
1. Data Seeding
1. Localization

## Projects
1. `Clean.Web` - webapi
1. `Clean.Seed` - console app for db seeding 
1. `Clean.CryptoTool` - console app for AES encryption/decryption 

## EF - Code First Migration
### Updating Database Schema
Below commands are using `Visual Studio Package Manager Console`. For `EF Core CLI`, you can search for the equivalent online.
1. `$env:ASPNETCORE_ENVIRONMENT='xxx'` (replace `xxx` with environment name. EF will read the connection string from `appsettings.xxx.json`)
1. `add-migration <MigrationName> -StartupProject Clean.Web -project Clean.Migrations`
1. `update-database -StartupProject Clean.Web -project Clean.Migrations`

### Reverting Database Schema
Run EF update database commands. Below commands are using `Visual Studio Package Manager Console`. For `EF Core CLI`, you can search for the equivalent online.
1. `$env:ASPNETCORE_ENVIRONMENT='xxx'` (replace `xxx` with environment name. EF will read the connection string from `appsettings.xxx.json`)


If the wrong migration has already been applied to the database: 
1. `update-database -StartupProject Clean.Web -project Clean.Migrations -migration <NameOfLastSuccessfulMigration>`


To remove the wrong migration from the code:
1. `remove-migration -StartupProject Clean.Web -project Clean.Migrations`

## Additional Notes
1. For local development, it is recommended to store the encryption key in secret manager.
1. For local development running in Docker, AWS credentials is retrieved from secret manager.


