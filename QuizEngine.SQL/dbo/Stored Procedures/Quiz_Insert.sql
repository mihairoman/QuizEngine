CREATE PROCEDURE [dbo].[Quiz_Insert]
	@QuizGUID uniqueidentifier,
	@LevelGUID	uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@Time time(7) = null
AS
BEGIN
	DECLARE  @FoundGUID uniqueidentifier;
	SEt @FoundGUID = (SELECT q.QuizGUID FROM dbo.Quizes q where q.QuizGUID = @QuizGUID);
	
	if @FoundGUID IS NULL
	BEGIN
		INSERT INTO Quizes VALUES(@QuizGUID,@LevelGUID,@CategoryGUID,@Time)
	END
END