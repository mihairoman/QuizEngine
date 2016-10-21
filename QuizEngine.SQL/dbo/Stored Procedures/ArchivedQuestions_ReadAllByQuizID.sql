CREATE PROCEDURE [dbo].[ArchivedQuestions_ReadAllByQuizID]
	@QuizGUID uniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON;
	SELECT aq.ArchivedQuestionGUID, aq.ArchivedQuestionText, aq.QuestionType, aq.LevelGUID,
		   aq.CategoryGUID, aq.QuizGUID
	FROM ArchivedQuestions aq
	JOIN Quizes q ON q.QuizGUID = aq.QuizGUID
	WHERE q.QuizGUID = @QuizGUID
	ORDER BY aq.IndexOrder
END