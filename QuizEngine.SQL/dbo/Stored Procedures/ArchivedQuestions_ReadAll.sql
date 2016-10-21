CREATE PROCEDURE [dbo].[ArchivedQuestions_ReadAll]

	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT ArchivedQuestionGUID, ArchivedQuestionText,QuestionType,LevelGUID,CategoryGUID, QuizGUID from ArchivedQuestions
	ORDER BY IndexOrder

END


