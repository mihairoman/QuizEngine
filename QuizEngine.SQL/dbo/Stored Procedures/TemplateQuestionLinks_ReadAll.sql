CREATE PROCEDURE [dbo].[TemplateQuestionLinks_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.TemplateQuestionLinks.QuizTemplateGUID , dbo.TemplateQuestionLinks.QuestionGUID
	FROM dbo.TemplateQuestionLinks
 
END
