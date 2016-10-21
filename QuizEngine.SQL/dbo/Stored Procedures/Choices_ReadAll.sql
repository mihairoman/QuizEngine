
---------------------------------------  SELECT ALL CHOISES
CREATE PROCEDURE [dbo].[Choices_ReadAll]

AS
BEGIN

SELECT ChoiceGUID, QuestionGUID, AnswerText, Value, IsCorrect 
FROM [Choices] Order by ChoicePosition

END;


