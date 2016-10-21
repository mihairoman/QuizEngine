CREATE PROCEDURE [dbo].[Choices_DeleteByQuestionID]
	@QuestionGUID uniqueidentifier
AS
BEGIN
	DELETE
	FROM [dbo].[Choices]
	WHERE QuestionGUID = @QuestionGUID
END