--Categories Delete
CREATE PROCEDURE [dbo].[Categories_ReadAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT CategoryGUID,CategoryName 
	FROM dbo.Categories 
	ORDER BY CategoryName
END