# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos do projeto
COPY PaymentService.csproj ./
RUN dotnet restore PaymentService.csproj

# Copiar o restante dos arquivos e compilar
COPY . ./ 
RUN dotnet publish PaymentService.sln -c Release -o out

# Etapa 2: Runtime (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar os arquivos da etapa de build
COPY --from=build /app/out .

# Definir a porta padrão
EXPOSE 5002

# Comando de inicialização do container
ENTRYPOINT ["dotnet", "PaymentService.dll"]
