CREATE PROCEDURE [dbo].[Quiz_CountAllPredefined]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM dbo.QuizTemplates
	
END