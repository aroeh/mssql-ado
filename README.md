# mssql-ado
Basic reference repo for helper classes and libraries focused on Microsoft SQL Server using ADO


## Demonstrated Features
- Clean Architecture concept
- Class to handle SQL Specific operations with a more user friendly wrapper class
- Generic class to handle SQL Operations and generically map responses using reflection
- Text Commands
- Stored Procedure Commands
- Transactions
- DataReader
- SQL Adapter DataTable Fill

# Dependencies
- Docker Desktop
- Docker Compose V2
- SQL Server Management Studio (SSMS) or Azure Data Studio

# Getting Started
1. Setup the database and run setup scripts
2. Run the solution


## MSSQL Setup
I haven't found a better way to handle the MSSQL setup just yet, but for now these are the steps for setting the MS SQL Containers

1. Run the docker compose command to generate all images and start all containers
```
docker compose up -d
```

2. Once the mssql-data container is running, connect to using either SSMS or Azure Data Studio
    > You can connect using the following connection string
    ```
    Persist Security Info=False;User ID=sa;Password=YourStrong@Passw0rd;Server=localhost;Encrypt=True;
    ```

3. You should be in the master database at this point.  Open a new query editor and run the script in the `setup.sql` file
> This file contains all script to create the Samples database, tables, insert seed data, and stored procedures


# Run the Solution
The easiest way to run this solution is to use docker compose as that will build the api project and provide containers for the data.  But there are other options as well.


## API Dockerfiles
There are 2 Dockerfiles present in the CosmosDb.NoSql.API project:
- Dockerfile
- Dockerfile_NoBuild

> Dockerfile is the initial setup default within the repository.

Each one demonstrates a different approach and potential use case.

### Dockerfile
Dockerfile is typically the default kind of file that Visual Studio auto generates with adding container support to a project.  
This example handles building the solution in a base image and then publishing the code to a runtime image.
This could be useful when wanting to debug and letting the docker runtime handle all of the work, or if you don't want to manually build and publish code before spinning up a new image and container.

### Dockerfile_NoBuild
This is a much smaller and much more simple docker file.  It requires published code to have already been produced to copy into the runtime image.
The Docker build and container spin up is much faster since it doesn't have to build the solution in the image itself.  
This scenario is useful in pipeline scenarios where the code may have already been built and published by prior tasks in a job.

To use this docker file in the solution, do the following:

1. In a command line, navigate to the directory containing the CosmosDb.NoSql.API.csproj
```
cd <path>\CosmosDb.NoSql.API
```

> Alter the path variable to match your local environment

2. Build the project either at the project or solution level
```
dotnet build
```

3. Publish the code into a directory using the Release configuration
```
dotnet publish CosmosDb.NoSql.API.csproj -c Release -o publish /p:UseAppHost=false
```

> This creates a new directory at `<path>\CosmosDb.NoSql.API\publish`

4. Update the docker-compose.yaml section for webapi.
    - Change the value of build.docker from CosmosDb.NoSql.API/Dockerfile to `CosmosDb.NoSql.API/Dockerfile_NoBuild`


## Docker Compose
1. Optional - build all containers in the compose yaml
```
docker compose build
```
> To build a specific container use `docker compose build <service-name>`

2. Compose up the containers
```
docker compose up
```
> If you do not want to debug, the add the -d parameter.  `docker compose up -d`
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

### Clean Up
Once containers are no longer needed you can remove them all using the compose down command
```
docker compose down
```

> Images can also be deleted using the compose down command
```
docker compose down --rmi "all"
```


# References
- [Deploy and Connect to MSSQL](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment?view=sql-server-ver16&pivots=cs1-bash)
- [MSSQL Docker Installation](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash)
- [MSSQL Container Github](https://github.com/microsoft/mssql-docker)
- [MSSQL Docker Samples](https://docs.docker.com/samples/ms-sql/)
