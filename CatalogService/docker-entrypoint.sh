#!/bin/sh
set -e

# Executar as migra��es antes de iniciar o servi�o
echo "Aplicando migra��es..."
dotnet ef database update

# Iniciar a aplica��o
echo "Iniciando a aplica��o..."
exec dotnet CatalogService.dll
