
CREATE PROCEDURE [dbo].[ArchivedQuestions_Delete]

	@ArchivedQuestionGUID uniqueidentifier
	
AS
BEGIN
	
	DELETE
	FROM [ArchivedQuestions]
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
END



