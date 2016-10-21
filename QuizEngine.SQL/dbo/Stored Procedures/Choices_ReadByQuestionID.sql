CREATE PROCEDURE [dbo].[Choices_ReadByQuestionID]
	@QuestionGUID UNIQUEIDENTIFIER
AS
BEGIN
		SELECT TOP 1000 
			  c.ChoiceGUID
			, c.QuestionGUID
			, c.AnswerText
			, c.Value
			, c.IsCorrect
	  FROM [dbo].[Questions] as q
	   JOIN [dbo].[Choices] as c 
	  ON q.[QuestionGUID] = c.[QuestionGUID] 
	  WHERE q.QuestionGUID= @QuestionGUID
	  Order by ChoicePosition
END
GO



