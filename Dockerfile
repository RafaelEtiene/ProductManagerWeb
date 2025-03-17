# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo da solution e o projeto
COPY ProductManagerWeb.sln ./
COPY ProductManagerWeb/ProductManagerWeb.csproj ./ProductManagerWeb/

# Restaura as dependências
RUN dotnet restore ProductManagerWeb/ProductManagerWeb.csproj

# Copia o restante dos arquivos
COPY ProductManagerWeb/. ./ProductManagerWeb/

# Define o diretório de trabalho para o build
WORKDIR /src/ProductManagerWeb

# Publica a aplicação
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o build publicado
COPY --from=build /app/publish .

# Expõe a porta (ajuste se sua app rodar em outra)
EXPOSE 8080

# Comando de execução
ENTRYPOINT ["dotnet", "ProductManagerWeb.dll"]
