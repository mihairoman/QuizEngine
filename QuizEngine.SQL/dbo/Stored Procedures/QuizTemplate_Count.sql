CREATE PROCEDURE [dbo].[QuizTemplate_Count]
@userGUID as uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;
    SELECT COUNT(*) 
	FROM dbo.QuizTemplates 
	WHERE dbo.QuizTemplates.QuizTemplateGUID not in (select QuizGUID from dbo.QuizUserLinks where UserGUID=@userGUID)
END
