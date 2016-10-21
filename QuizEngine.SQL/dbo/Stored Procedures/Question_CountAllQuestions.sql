

CREATE PROCEDURE [dbo].[Question_CountAllQuestions]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(q.QuestionGUID) as QuestionCount from [dbo].Questions q
END
