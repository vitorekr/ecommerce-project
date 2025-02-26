# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto e restaurar dependências
COPY CatalogService.csproj ./
RUN dotnet restore CatalogService.csproj

# Copiar todo o código, garantindo que a pasta Migrations seja copiada
COPY . .

# Verificar se a pasta Migrations/ foi copiada corretamente
RUN ls -la Migrations/

# Compilar e publicar o projeto
RUN dotnet build ECommerce-Microservices.sln -c Release
RUN dotnet publish ECommerce-Microservices.sln -c Release -o out

# Etapa 2: Runtime (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os arquivos compilados da etapa de build
COPY --from=build /app/out .

# Copiar as migrations para garantir que estão disponíveis
COPY --from=build /app/Migrations/ ./Migrations/

# Copiar o script de entrada
COPY docker-entrypoint.sh /app/docker-entrypoint.sh

# Garantir que o script tenha permissão de execução
RUN chmod +x /app/docker-entrypoint.sh

# Expor a porta da API
EXPOSE 5000

# Definir o script como ponto de entrada
ENTRYPOINT ["/app/docker-entrypoint.sh"]
