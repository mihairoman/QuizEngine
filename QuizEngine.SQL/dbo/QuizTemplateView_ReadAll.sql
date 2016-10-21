CREATE PROCEDURE [dbo].[QuizTemplateView_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT DISTINCT dbo.QuizTemplates.QuizTemplateGUID, dbo.QuizTemplates.TypeName, count(dbo.Questions.QuestionGUID)
	FROM dbo.QuizTemplates JOIN dbo.TemplateQuestionLinks ON dbo.QuizTemplates.QuizTemplateGUID= dbo.TemplateQuestionLinks.QuizTemplateGUID
	JOIN dbo.Questions ON dbo.Questions.QuestionGUID = dbo.TemplateQuestionLinks.QuestionGUID
	GROUP BY dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName
  
END