CREATE PROCEDURE dbo.Quiz_Delete
	@QuizGUID uniqueidentifier
AS	
BEGIN
	
	SET NOCOUNT ON;
	DELETE FROM Quizes
	WHERE QuizGUID = @QuizGUID
END
