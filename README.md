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

   git clone https://github.com/RafaelEtiene/ProductManagerWeb.git
   - cd ProductManagerWeb
   
2. Restaurar pacotes nuget
   - dotnet restore
   
3. Configurar appsettings para conectar com Postgre Container
   "ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Port=5432;Username=userTest;Password=test;Database=productmanagerdb"
   }
   
4. Rodar script para iniciar container do Postgre
   docker-compose up --build
   
5. Rodar projeto Web
   \ProductManagerWeb\ProductManagerWeb

### Instruções para rodar as migrações de banco de dados
1. Não é necessário realizar nenhuma ação pois aplicação foi construída para rodar a migração sempre no início caso a mesma ainda não tenha sido executada.

### Instruções para rodar os testes unitários
1. Entrar no repositório
	cd \ProductManagerWeb

2. Abrir arquivo .sln no Visual Studio

3. Na aba superior 'Test', selecionar o Test Explorer

3. Selecionar opção 'Run all tests' e verificar se tudo está OK.


### Instruções para rodar a partir do Docker	
1. Instalar Docker

2. Realizar login
	docker login
	
2. Rodar container PostgreSql
	docker run --name pgsql-productmanager -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=productmanagerdb -p 5432:5432 -d postgres:15-alpine
	
3. Rodar a aplicação com PostgreSql
	docker run --name productmanager-app -p 8080:80 --link pgsql-productmanager -e ConnectionStrings__DefaultConnection="Host=pgsql-productmanager;Port=5432;Database=productmanagerdb;Username=postgres;Password=postgres;" -d rafaeletiene22/productmanager:v1.0


bash
Copy
Edit
docker run -d -p 80:80 rafiusk/myapp:v1.0

