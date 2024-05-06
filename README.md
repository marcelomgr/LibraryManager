## Sobre o Projeto

Este é um projeto de biblioteca digital desenvolvido como parte da mentoria .Net Expert, proposta pelo mentor LuisDev (https://www.luisdev.com.br/).

---

## Tecnologias Utilizadas

- .NET 7
- Entity Framework Core
- SQL Server
- UnitOfWork
- Padrão Repository
- CQRS
- Arquitetura Limpa
- Testes Unitários
- Fluent Validation
- Validação de APIs com FluentValidation
- DTO's
- ValueObject
- Integração com API de CEP
- Autenticação e Autorização com JWT

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
dotnet ef migrations add FirstMigration -s ../LibraryManager.API -o Persistence/Migrations
dotnet ef database update -s ../LibraryManager.API
```

Com esse passo a passo você deve conseguir rodar o projeto sem maiores problemas!
