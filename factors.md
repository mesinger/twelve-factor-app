# Factors
## 1. Codebase
This app is version controlled with git, therefore there exists a 1 to 1 relationship between codebase and app.

## 2. Dependencies
Dependencies are added with nuget. With dotnet publish, all dependencies are bundled with the app. Only the dotnet runtime is required to execute this application on a host machine.

## 3. Config
Settings for the BurgerKing API / database are stored as environment variables, and are automatically injected by dotnet.

## 4. Backing services
Both the burgerking api and the database are accessible via a repository interface defined in the domain layer. Configurations, like connection strings, are provided by environment variables.

## 5. Build, release, run
--

## 6. Processes
The app is stateless, any data that needs to persist is stored in a database.

## 7. Port binding
The application runs with kestrel, which is the default self-contained web server for dotnet applications. Port configuration can be achieved with a command line argument.
```
dotnet app.dll --urls http://0.0.0.0:port
```
As the app runs inside a docker container, the port of the app has to be exposed, therefore adding an additional layer of port binding.<br/>
Furthermore, a good approach would be to expose the application not directly, but behind a reverse proxy like nginx.<br/>
That said, there are three layers of port binding possible:
* Via kestrel
* Via port exposing with docker
* Via nginx

## 8. Concurrency
As this app satisfies point *6. Processes*, and already provides a Dockerfile as a starting point, it could be quite easily scaled horizontally with technologies like docker swarm. Such orchestration services would not only manage horizontal deployments, but also manage restarts of crashed applications and many other things.

## 9. Disposability
--

## 10. Dev/prod parity
Keeping the tools gap minimal by, in this example, only using Postgres for all dev/staging/production environments.<br/>
Time and personnell gaps are reduced by using virtualization technologies like docker, and by using the advantages of CI/CD tooling.

## 11. Logs
On dotnet, logging is made very simple with Microsofts Logging Abstractions. By default it prints logs to stdout.<br/>
However, i do not aggree with 12factor, that doing logging configuration in code is that much of a problem. Unfortunately, i never tried to pipe my apps logs from stdout, but it sound rather uncomfortable to me. Having different logging configruations for dev/staging/production seems understandable, however i am not sure, if it is, in general, neccessary to configure the logging for all e.g. production deploys individual (besides connections strings to something like Exceptionless).

## 12. Admin process
--
