# Etapa 1: Build da aplica��o
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos do projeto
COPY OrderService.csproj ./
RUN dotnet restore OrderService.csproj

# Copiar o restante dos arquivos e compilar
COPY . ./ 
RUN dotnet publish OrderService.sln -c Release -o out

# Etapa 2: Runtime (Execu��o)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar os arquivos da etapa de build
COPY --from=build /app/out .

# Definir a porta padr�o
EXPOSE 5000

# Comando de inicializa��o do container
ENTRYPOINT ["dotnet", "OrderService.dll"]
