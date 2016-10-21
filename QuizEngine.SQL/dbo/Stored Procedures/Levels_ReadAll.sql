CREATE PROCEDURE [dbo].[Levels_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	select LevelGUID,LevelName,Difficulty 
	From dbo.Levels
	ORDER BY LevelName
END