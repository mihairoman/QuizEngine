CREATE PROCEDURE [dbo].[Quiz_CountAllRandom]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM Quizes q
	WHERE q.QuizGUID not in (select qt.QuizTemplateGUID from QuizTemplates qt where qt.QuizTemplateGUID=q.QuizGUID)
END
