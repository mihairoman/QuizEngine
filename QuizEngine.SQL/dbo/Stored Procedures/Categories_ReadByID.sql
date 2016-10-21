--Categories Delete
create PROCEDURE [dbo].[Categories_ReadByID]
	@CategoryGUID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;

	SELECT CategoryGUID,CategoryName 
	FROM dbo.Categories 
	WHERE CategoryGUID = @CategoryGUID
END