
---------------------------------------  DELETE A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Delete]
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	DELETE 
	FROM Questions
	WHERE QuestionGUID = @QuestionGUID

END;


