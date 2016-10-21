CREATE PROCEDURE [dbo].[ArchivedChoices_ReadByArchivedQuestionID]
	@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
SET NOCOUNT ON;

	SELECT ac.ChoiceGUID, ac.ArchivedQuestionGUID, ac.AnswerText, ac.Value, ac.IsCorrect
	FROM ArchivedChoices ac
	JOIN ArchivedQuestions aq ON aq.ArchivedQuestionGUID = ac.ArchivedQuestionGUID
	WHERE aq.ArchivedQuestionGUID = @ArchivedQuestionGUID
	ORDER BY ac.ChoiceGUID

END