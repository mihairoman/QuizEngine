--Categories Insert
create PROCEDURE [dbo].[Categories_Insert]
	@CategoryGUID uniqueidentifier, 
	@CategoryName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Categories] VALUES (@CategoryGUID, @CategoryName)

END