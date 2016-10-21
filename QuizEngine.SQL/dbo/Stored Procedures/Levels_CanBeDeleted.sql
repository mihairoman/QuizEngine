CREATE PROCEDURE [dbo].[Levels_CanBeDeleted]
	@LevelGUID uniqueidentifier, @ReturnValue bit output
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT q.QuestionGUID FROM Questions q WHERE q.LevelGUID = @LevelGUID) OR
		EXISTS (SELECT qu.QuizGUID FROM Quizes qu WHERE qu.LevelGUID = @LevelGUID)
	BEGIN
		SET @ReturnValue = 0
	END
	ELSE
	BEGIN
		SET @ReturnValue = 1
	END
END