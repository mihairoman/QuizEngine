CREATE PROCEDURE [dbo].[TemplateQuestionLinks_ReadByQuizId]
	@TemplateQuizGUID UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.TemplateQuestionLinks.QuizTemplateGUID , dbo.TemplateQuestionLinks.QuestionGUID
	FROM dbo.TemplateQuestionLinks
	WHERE dbo.TemplateQuestionLinks.QuizTemplateGUID =@TemplateQuizGUID
END

