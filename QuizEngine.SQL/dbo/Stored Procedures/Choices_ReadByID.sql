 
---------------------------------------  SELECT ONE CHOISE

CREATE PROCEDURE [dbo].[Choices_ReadByID]
	@ChoiceGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT ChoiceGUID, QuestionGUID, AnswerText, Value, IsCorrect
	FROM [Choices] 
	WHERE ChoiceGUID = @ChoiceGUID

END;


