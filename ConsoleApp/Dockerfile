FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ConsoleApp/ConsoleApp.csproj ./ConsoleApp/
COPY Domain/Domain.csproj ./Domain/
RUN dotnet restore ./ConsoleApp/ConsoleApp.csproj

COPY . .

WORKDIR /src/ConsoleApp
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ConsoleApp.dll"]
