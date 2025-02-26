FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY APIGateway.csproj ./
RUN dotnet add package Ocelot --version 19.0.0
RUN dotnet restore

COPY ocelot.json /app/ocelot.json

# Copiar todo o código-fonte e compilar
COPY . ./
RUN dotnet publish APIGateway.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8000
ENTRYPOINT ["dotnet", "APIGateway.dll"]
