CREATE PROCEDURE [dbo].[Questions_ReadByQuizTemplateID]
	@QuizTemplateGUID UNIQUEIDENTIFIER
AS
BEGIN

	SET NOCOUNT ON;

    SELECT dbo.Questions.QuestionGUID, Text, QuestionType, CategoryGUID, LevelGUID 
	FROM [Questions] JOIN [TemplateQuestionLinks] ON dbo.Questions.QuestionGUID = dbo.TemplateQuestionLinks.QuestionGUID
	WHERE dbo.TemplateQuestionLinks.QuizTemplateGUID = @QuizTemplateGUID
END
