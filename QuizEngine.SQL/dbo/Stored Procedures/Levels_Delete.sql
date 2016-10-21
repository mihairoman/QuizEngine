create PROCEDURE [dbo].[Levels_Delete]
	@LevelGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	Delete From dbo.Levels
	WHERE LevelGUID = @LevelGUID
END