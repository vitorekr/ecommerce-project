#!/bin/sh
set -e

# Executar as migrações antes de iniciar o serviço
echo "Aplicando migrações..."
dotnet ef database update

# Iniciar a aplicação
echo "Iniciando a aplicação..."
exec dotnet CatalogService.dll
