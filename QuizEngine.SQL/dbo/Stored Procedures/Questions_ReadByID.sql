
---------------------------------------  SELECT ONE QUESTION 

CREATE PROCEDURE [dbo].[Questions_ReadByID]
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT QuestionGUID, Text, QuestionType, CategoryGUID, LevelGUID
	FROM [Questions]
	WHERE QuestionGUID = @QuestionGUID

END;


