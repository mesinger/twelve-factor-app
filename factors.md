# Factors
## 1. Codebase
This app is version controlled with git, therefore there exists a 1 to 1 relationship between codebase and app.

## 2. Dependencies
Dependencies are added with nuget. With dotnet publish, all dependencies are bundled with the app. Only the dotnet runtime is required to execute this application on a host machine.

## 3. Config
Settings for the BurgerKing API / database are stored as environment variables, and are automatically injected by dotnet.

## 4. Backing services
Both the burgerking api and the database are accessible via a repository interface defined in the domain layer. Configurations, like connection strings, are provided by environment variables.

## 6. Processes
The app is stateless, any data that needs to persist is stored in a database.

## 7. Port binding
The applications runs with kestrel, which is the default self-contained web server for dotnet applications. Port configuration can be achieved with a command line argument.
```
dotnet app.dll --urls http://0.0.0.0:port
```
As the app runs inside a docker container, the port of the app has to be exposed, therefore adding an additional layer of port binding.<br/>
Furthermore, on a development deploy, running the app only with kestrel would be sufficient. For a staging / production scenario, a good approach would be to expose the application not directly, but behind a reverse proxy like nginx.<br/>
That said, there are three layers of port binding possible:
* Via kestrel
* Via port exposing with docker
* Via nginx

## 8. Concurrency
As this app satisfies point *6. Processes*, and already provides a Dockerfile as starting point, it could be quite easily scaled horizontally with technologies like docker swarm. Such orchestration services would not only make manage deployment, but also manage restarts of crashes and many other things.
