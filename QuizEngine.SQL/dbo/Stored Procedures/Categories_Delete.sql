--Categories Delete
create PROCEDURE [dbo].[Categories_Delete]
	@CategoryGUID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Categories 
	WHERE CategoryGUID = @CategoryGUID
END