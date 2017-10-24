--Drop/Add Login
Use Master
go

IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'DvdLibraryApp')
DROP Login DvdLibraryApp
Go

Create Login DvdLibraryApp with password = 'Testing123'
go

--Drop User
Use DvdLibrary
Go

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'DvdLibraryApp')
DROP USER DvdLibraryApp
Go

Create User DvdLibraryApp For Login DvdLibraryApp
go 

Grant Execute on DbReset to DvdLibraryApp
Grant Execute on SelectAllDvds to DvdLibraryApp
Grant Execute on DvdInsert to DvdLibraryApp
Grant Execute on DvdUpdate to DvdLibraryApp
Grant Execute on DvdDelete to DvdLibraryApp
go

Grant Select on Dvds to DvdLibraryApp
Grant Insert on Dvds to DvdLibraryApp
Grant Update on Dvds to DvdLibraryApp
Grant Delete on Dvds to DvdLibraryApp
go 