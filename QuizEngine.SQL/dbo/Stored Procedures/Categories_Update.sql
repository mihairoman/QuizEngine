--Categories Update
create PROCEDURE [dbo].[Categories_Update]
	@CategoryGUID uniqueidentifier, 
	@CategoryName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Categories
	SET CategoryName = @CategoryName
	WHERE CategoryGUID = @CategoryGUID
END