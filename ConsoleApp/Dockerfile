# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura as dependências
COPY ConsoleApp/ConsoleApp.csproj ./ConsoleApp/
COPY Domain/Domain.csproj ./Domain/
RUN dotnet restore ./ConsoleApp/ConsoleApp.csproj

# Copia todo o código fonte e recursos necessários
COPY . .

# Publica o projeto
WORKDIR /src/ConsoleApp
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Define o entrypoint
ENTRYPOINT ["dotnet", "ConsoleApp.dll"]
