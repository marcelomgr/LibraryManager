## Sobre o Projeto

O projeto consiste em um gerenciador de empréstimo de livros de uma biblioteca, composto por três módulos: Livros, Usuários e Empréstimos, abrangendo a aplicação de regras de negócio essenciais entre eles.

---

## Tecnologias Utilizadas

- .NET 7
- Entity Framework Core
- SQL Server
- UnitOfWork
- Padrão Repository
- Generic Repository (Abstração)
- CQRS
- Arquitetura Limpa
- Testes Unitários
- Fluent Validation
- DTO's
- ValueObject
- Integração com API de CEP
- Autenticação e Autorização com JWT (e Autorização via Swagger)

---

## Passo a Passo para Executar o Projeto

### Pré-requisitos

- Docker instalado na máquina
- .NET SDK instalado na máquina

### Passo 1: Baixar e Executar o Contêiner do SQL Server

```bash
docker pull mcr.microsoft.com/mssql/server
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Abcd=1234" -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server
```

### Passo 2: Configurar as Credenciais de Acesso ao Banco de Dados

Abra o SQL Server Management Studio (SSMS)
Configure as seguintes credenciais:
Nome do Servidor: localhost,1433
Nome de Usuário: sa
Senha: Abcd=1234

### Passo 3: Executar as Migrações do Entity Framework Core

```bash
cd Infrastructure
dotnet tool install --global dotnet-ef
dotnet ef migrations add TestMigration -s ../LibraryManager.API -o Persistence/Migrations
dotnet ef database update -s ../LibraryManager.API
```

### Passo 4: Executar a aplicação

- Se necessário, altere o projeto de inicialização para "LibraryManager.API"
- Rode a aplicação
- A action "Users" (Post) está como "AllowAnonymous", portanto deve ser possível efetuar o cadastro de seu usuário sem autenticação (utilize a pemissão "Admin")
- Em seguida, autentique-se pela action "Auth"
- Após a obtenção do token, vá em "Authorize" no swagger, e inclua no campo "bearer coleSeuTokenGeradoAqui"
- Pronto, ao executar as actions, já deve estar autorizado


Com esse passo a passo você deve conseguir rodar o projeto sem maiores problemas!
