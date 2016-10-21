CREATE PROCEDURE [dbo].[QuizUserPreview_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.Quizes.QuizGUID, dbo.Levels.LevelName, dbo.Categories.CategoryName, count(dbo.ArchivedQuestions.ArchivedQuestionGUID) AS QuestionNumber
	FROM dbo.Levels JOIN dbo.Quizes ON dbo.Levels.LevelGUID = dbo.Quizes.LevelGUID JOIN dbo.Categories ON dbo.Quizes.CategoryGUID = dbo.Categories.CategoryGUID
		JOIN dbo.ArchivedQuestions ON dbo.ArchivedQuestions.QuizGUID = dbo.Quizes.QuizGUID
	GROUP BY dbo.Quizes.QuizGUID, dbo.Levels.LevelName, dbo.Categories.CategoryName

END