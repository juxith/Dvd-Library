Use DvdLibrary
Go

If exists(Select * From Information_Schema.ROUTINES
Where ROUTINE_NAME = 'SelectAllDvds')
Drop Procedure SelectAllDvds
Go

Create Procedure SelectAllDvds As
Begin
	Select * From Dvds
End
go

If exists(Select * From Information_Schema.ROUTINES
Where ROUTINE_NAME = 'DvdInsert')
Drop Procedure DvdInsert
Go

Create Procedure DvdInsert (
	@DvdId int output, 
	@Title nvarchar(max), 
	@ReleaseYear int, 
	@Director nvarchar(20), 
	@Rating nvarchar(5), 
	@Notes nvarchar(50)
)
As
	Insert into Dvds(Title, ReleaseYear, Director, Rating, Notes)
	Values(@Title, @ReleaseYear, @Director, @Rating, @Notes)

	set @DvdId = SCOPE_IDENTITY();
go

If exists(Select * From Information_Schema.ROUTINES
Where ROUTINE_NAME = 'DvdDelete')
Drop Procedure DvdDelete
Go

Create Procedure DvdDelete(
	@DvdId int
)
As
Begin
	Begin Transaction
	Delete From Dvds where DvdId = @DvdId;
	Commit Transaction
End
go


If exists(Select * From Information_Schema.ROUTINES
Where ROUTINE_NAME = 'DvdUpdate')
Drop Procedure DvdUpdate
Go

Create Procedure DvdUpdate(
	@DvdId int, 
	@Title nvarchar(max), 
	@ReleaseYear int, 
	@Director nvarchar(20), 
	@Rating nvarchar(5), 
	@Notes nvarchar(50)
)
As
Begin
	Update Dvds Set
		Title = @Title,
		ReleaseYear = @ReleaseYear,
		Director = @Director,
		Rating = @Rating,
		Notes = @Notes
	where DvdId = @DvdId
End
go
