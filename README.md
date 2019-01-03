# About my commits
I just did one commit for everything, which is not good. If this is going to be a real life solution, I will have multiple small commits with meaning commit messages.

# Database
I am using SQL Server as data storage, which because I have it setup locally. If this is going to be a real life solution, I will probably consider to use DynamoDB or Azure Cosmos DB.
Also, the user I am going to create, mwebapi, has too much privilege when its used to run the web api application. If this is going to be a real life solution, I will create two different users, one for running db migrator, one for running web api and assign appropriate permission.

## Initial setup
Following scripts will help you to setup db and required user to login db
```
USE master
If(db_id(N'mtest') IS NULL)
BEGIN
	CREATE DATABASE mtest
END;
GO

CREATE LOGIN mwebapi WITH PASSWORD = 'test123' 

GO

USE mtest;
--drop user mwebapi
DROP USER IF EXISTS mwebapi 

CREATE USER mwebapi FOR LOGIN mwebapi;
EXEC sp_addrolemember 'db_datareader', 'mwebapi';
EXEC sp_addrolemember 'db_datawriter', 'mwebapi';
GRANT SELECT, INSERT, UPDATE, DELETE, alter ON SCHEMA::dbo TO mwebapi;
GRANT CREATE TABLE TO mwebapi;
GO
```

## Run migrator

### Configure Connection string
In MTestDB project, find appsettings.json, then configure the connection string to sql server, use the `mwebapi` user as it has enough privilege to create required db objects.

###  `dotnet run`
To kick off db migration


Please notes that migrator will create few customers as initial data, just trying to make things a bit easier.

# Back end
## Quick start

## Config
### General 
Configurations are at appsettings.json, and also available via environment variable.
In the case of having configuration for different environment(i.e. Dev vs Live), two options we can go with:
- use "appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json" where `ASPNETCORE_ENVIRONMENT` will be the target environment name.
- use environment variable in the hosting server with appropriate value.

### Configure CORS
currently I have 
```
           app.UseCors(builder => {
                builder.WithOrigins("http://localhost:3000");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
```
which only works for frontend hosted at `localhost:3000`, if you want to change frontend address, then make sure it is reflected here as well.


## Kick off backend
In the project directory, you can run:

### `dotnet run`

then you can kick off frontend to consume backend service, or use postman to consume backend service from [http://localhost:3000](http://localhost:3000)

### Unit Tests
in the Tests project, run `dotnet test`


## Available endpoints
### `GET api/customers`
Get list of customers, and have filters ordering and pagination built in

### `POST api/customers`
Create a new customer

## Notes
I've been following TDD while develop this app.
I've been following the vertical slice design, or front end back end separation design
There is room to put in user authentication with third party provider such as google single sign on or auth0, but I think it can be future improvement.
DOB in database is UTC time, and I should probably rename the column to DOBUtc to indicate that.
