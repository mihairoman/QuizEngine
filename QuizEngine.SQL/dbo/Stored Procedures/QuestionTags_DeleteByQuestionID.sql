CREATE PROCEDURE [dbo].[QuestionTags_DeleteByQuestionID]
	@QuestionGUID uniqueidentifier
	
AS
BEGIN
	DELETE FROM [dbo].[QuestionTags]
	WHERE QuestionGUID = @QuestionGUID
END
