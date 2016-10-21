CREATE PROCEDURE [dbo].[ArchivedChoices_ReadByID]
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ChoiceGUID, ArchivedQuestionGUID, AnswerText, Value, IsCorrect
	FROM ArchivedChoices
	WHERE ChoiceGUID = @ChoiceGUID
		  AND ArchivedQuestionGUID = @ArchivedQuestionGUID
	ORDER BY ChoicePosition
END