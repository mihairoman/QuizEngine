CREATE PROCEDURE [dbo].[ArchivedQuestions_InsertById]
	@QuestionGUID uniqueidentifier,
	@QuizGUID uniqueidentifier,
	@IndexOrder smallint
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE 

	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier = NEWID(),
	@ArchivedQuestionText nvarchar(max),
	@QuestionType smallint,
	@LevelGUID uniqueidentifier, 
	@CategoryGUID uniqueidentifier

	SELECT @ArchivedQuestionText=Text, @QuestionType=QuestionType, @LevelGUID=LevelGUID, @CategoryGUID=CategoryGUID 
	FROM Questions
	WHERE @QuestionGUID=QuestionGUID
	
	INSERT INTO ArchivedQuestions VALUES(@ArchivedQuestionGUID, @ArchivedQuestionText, @QuestionType, @LevelGUID, @CategoryGUID, @QuizGUID, @IndexOrder)


	INSERT INTO ArchivedChoices
	SELECT NEWID(),
    @ArchivedQuestionGUID,
	C.AnswerText,
	C.Value,
	C.IsCorrect,
	C.ChoicePosition
    FROM Questions as Q 
	INNER JOIN Choices as C ON C.QuestionGUID = @QuestionGUID
	WHERE C.QuestionGUID=Q.QuestionGUID

END
