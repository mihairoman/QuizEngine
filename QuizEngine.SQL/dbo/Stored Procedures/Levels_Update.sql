--Levels Update
create PROCEDURE [dbo].[Levels_Update]
	@LevelGUID uniqueidentifier, 
	@LevelName nvarchar(MAX),
	@Difficulty smallint

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Levels
	SET LevelName = @LevelName, Difficulty=@Difficulty
	WHERE LevelGUID = @LevelGUID

END