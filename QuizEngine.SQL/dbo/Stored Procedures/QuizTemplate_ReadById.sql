CREATE PROCEDURE [dbo].[QuizTemplates_ReadById]
@QuizTemplateGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName , dbo.QuizTemplates.Time
	FROM dbo.QuizTemplates
	WHERE QuizTemplateGUID = @QuizTemplateGUID
   
END