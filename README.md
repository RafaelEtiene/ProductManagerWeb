# ProductManagerWeb

# Produto Manager

Este projeto é um sistema de gerenciamento de produtos, desenvolvido para facilitar a criação, edição e listagem de produtos. A aplicação é construída usando ASP.NET Core e pode ser facilmente configurada e executada localmente, com suporte para migrações de banco de dados e execução de testes unitários. O projeto também oferece uma imagem Docker para facilitar o deploy.

## Descrição

Produto Manager é uma aplicação web que permite aos usuários gerenciar informações sobre produtos. Com essa aplicação, é possível:

- Cadastrar novos produtos.
- Editar dados de produtos existentes.
- Visualizar informações detalhadas sobre os produtos cadastrados.

## Instruções para configurar e executar o projeto localmente

### Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet)
- [Docker](https://www.docker.com/get-started)
- [Visual Studio 2022 ou Visual Studio Code](https://code.visualstudio.com/) para desenvolvimento

### Passos para rodar a aplicação localmente

1. **Clonar o repositório**:

   ### Clone o repositório do projeto para sua máquina local:
   git clone https://github.com/RafaelEtiene/ProductManagerWeb.git
   - cd ProductManagerWeb
   
   ### Restaurar pacotes nuget
   - dotnet restore
   
   ### Configurar appsettings para conectar com Postgre Container
   "ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Port=5432;Username=userTest;Password=test;Database=productmanagerdb"
   }
   
   ### Rodar script para iniciar container do Postgre
  
