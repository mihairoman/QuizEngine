CREATE PROCEDURE [dbo].[QuizTemplates_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName , dbo.QuizTemplates.Time
	FROM   dbo.QuizTemplates
   
END