# Factors
## 1. Codebase
This app is version controlled with git, therefore there exists a 1 to 1 relationship between codebase and app.

## 2. Dependencies
Dependencies are added via nuget, and via dotnet publish, all dependencies are bundled with the app. Only the dotnet runtime is required to execute this application.

## 3. Config
Settings for the BurgerKing API are stored as environment variables, and are automatically injected via dotnet.
