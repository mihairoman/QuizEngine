CREATE PROCEDURE [dbo].[ArchivedQuestions_Insert]
	@ArchivedQuestionGUID uniqueidentifier,
	@ArchivedQuestionText nvarchar(max),
	@QuestionType smallint,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@QuizGUID uniqueidentifier,
	@IndexOrder smallint
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO ArchivedQuestions VALUES (@ArchivedQuestionGUID,@ArchivedQuestionText,@QuestionType,@LevelGUID,@CategoryGUID,@QuizGUID, @IndexOrder )
END


