CREATE PROCEDURE Answer_ReadByQuizUser
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;

   
	SELECT QuizGUID,ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID FROM dbo.Answers
	WHERE Answers.QuizGUID = @QuizGUID AND Answers.UserGUID = @UserGUID
END
GO
