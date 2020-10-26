FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -o out

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "burgerking-calories-calculator.dll", "--urls", "http://0.0.0.0:5000"]