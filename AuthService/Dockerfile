FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar apenas os arquivos do projeto primeiro
COPY AuthService.csproj ./
RUN dotnet restore

# Copiar o restante do código e compilar
COPY . ./
RUN dotnet publish AuthService.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5003
ENTRYPOINT ["dotnet", "AuthService.dll"]
