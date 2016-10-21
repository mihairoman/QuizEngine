
Create PROCEDURE [dbo].[ArchivedQuestions_Update]

	@ArchivedQuestionGUID uniqueidentifier,
	@ArchivedQuestionText nvarchar(max),
	@QuestionType smallint,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@QuizGUID uniqueidentifier
	
AS
BEGIN
	
	UPDATE [ArchivedQuestions]
	SET ArchivedQuestionText=@ArchivedQuestionText,
	QuestionType = @QuestionType,
	LevelGUID = @LevelGUID,
	CategoryGUID = @CategoryGUID,
	QuizGUID=@QuizGUID
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
END



