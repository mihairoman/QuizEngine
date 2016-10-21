create PROCEDURE [dbo].[Levels_Insert]
	@LevelGUID uniqueidentifier, 
	@LevelName nvarchar(MAX),
	@Difficulty smallint
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Levels] VALUES (@LevelGUID, @LevelName, @Difficulty)
END