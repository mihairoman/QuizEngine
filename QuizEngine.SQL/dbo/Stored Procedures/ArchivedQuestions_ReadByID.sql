CREATE PROCEDURE [dbo].[ArchivedQuestions_ReadByID]

	@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT ArchivedQuestionGUID, ArchivedQuestionText,QuestionType,LevelGUID,CategoryGUID, QuizGUID from ArchivedQuestions
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
	ORDER BY IndexOrder
END


