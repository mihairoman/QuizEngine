create PROCEDURE [dbo].[Levels_ReadByID]
	@LevelGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	select LevelGUID,LevelName,Difficulty 
	From dbo.Levels
	WHERE LevelGUID = @LevelGUID
END