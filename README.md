# efcore-mssql
Basic reference repo for helper classes and libraries focused on Entity Framework Core and SQL Server


## Demonstrated Features
- Clean Architecture
- Entity Framework Core
- Options Pattern
- SQL Project for Publishing
- DbUp Migration


# Dependencies
- MSSQL
- Docker
- Docker Compose V2


# Getting Started
This project relies on a container for the SQL database.  Run the docker compose to start up the database and api, then use a http client of your choice to call the endpoints.


## Run Docker Compose
1. Optional - build all containers in the compose yaml
```
docker compose build
```
> To build a specific container use `docker compose build <service-name>`

2. Compose up the containers
```
docker compose up -d
```
> docker compose up will also build all images if they do not exist, so step 1 is optional

3. Use an http client like Postman or the http files in Visual Studio to send requests to the API

4. Stop the containers when done with testing (or leave them running)
```
docker compose stop
```

> Use the start command to start the containers again
```
docker compose start
```

## Migrate Data

There are 2 different approaches demonstrated in this project for migrating data: SQL Project Publish and DbUp

### Publish

The publish is setup in the project `EfCore.MSSQL.Database.Restuarant`

1. Double click `local.publish.xml`

> Settings are already configured for the profile

2. Clich the Publish button to run the scripts

> This will create the database, tables, and run post deployment scripts to create some test data


### DbUp

Project EfCore.MSSQL.DbUp.Migration uses the DbUp libraries to run sql migrations.
It has a check to ensure that the database will be created before running any scripts.
Scripts have been grouped by run order to ensure that dependent scripts are run last.

1. Select the `Development` profile for the project

2. Run the console app

> You can start a new debug instance or the app or you can build the project and run via cli


#### CLI

1. Build the solution
```
dotnet build
```

2. Navigate to the dll
```
<path>\efcore-mssql\EfCore.MSSQL.DbUp.Migration\bin\Debug\net10.0
```

3. Set the environment variable, ex: Powershell
```#Powershell
$env:DOTNET_ENVIRONMENT = "Development"
```

4. Run the dll
```
dotnet EfCore.MSSQL.DbUp.Migration.dll
```

## Clean Up
Once containers are no longer needed you can remove them all using the compose down command
```
docker compose down
```

> Images can also be deleted using the compose down command
```
docker compose down --rmi "all"
```

# References
- [Getting started with EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- [Database Providers](https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli)
- [Tutorial: Get started with EF Core](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-9.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [DbUp](https://dbup.readthedocs.io/en/latest/)
- [DbUp Github](https://github.com/dbup/dbup)
- [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/usage)
- []()
- []()
https://dotnettutorials.net/lesson/many-to-many-relationships-in-entity-framework-core/

https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
