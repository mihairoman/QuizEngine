CREATE PROCEDURE [dbo].[ArchivedChoices_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ChoiceGUID, ArchivedQuestionGUID, AnswerText, Value, IsCorrect
	FROM ArchivedChoices
	ORDER BY ChoicePosition

END