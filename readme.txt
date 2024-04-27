docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Abcd=1234" -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server

SSMS 
Server Name: localhost,1433
Username: sa
Password: Abcd=1234



a partir da pasta infrastructure
dotnet ef migrations add FirstMigration -s ../LibraryManager.API -o Persistence/Migrations

dotnet ef database update -s ../LibraryManager.API