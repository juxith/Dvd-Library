USE DvdLibrary
GO

if exists(Select * From INFORMATION_SCHEMA.Routines
where routine_name = 'DbReset')
drop procedure DbReset
go 

Create Procedure DbReset As
Begin
	Delete from Dvds;

	DBCC CHECKIDENT ('Dvds', reseed, 1)

	Set Identity_Insert Dvds on;
	insert into Dvds(DvdId, Title, ReleaseYear, Director, Rating, Notes)
	Values('1', 'The Big Bad Wolf', 1990, 'Judy Thao', 'G', 'Three sad little piggies'),
	('2', 'Little Red Riding Hood', 1992, 'Eric Wise', 'PG', 'Sad big bad wolf'),
	('3', 'Full Moon', 1990, 'Eric Ward', 'PG-13', 'The End')
	Set identity_insert Dvds off;
End