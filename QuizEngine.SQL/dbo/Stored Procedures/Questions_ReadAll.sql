---------------------------------------  SELECT ALL QUESTIONS 

CREATE PROCEDURE [dbo].[Questions_ReadAll]
	

AS
BEGIN

	SELECT QuestionGUID, Text, QuestionType, CategoryGUID, LevelGUID 
	FROM [Questions]

END;


