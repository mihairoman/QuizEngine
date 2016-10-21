CREATE PROCEDURE [dbo].[QuizUserLinks_ReadByQuizId]
	@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloaded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE QuizGUID = @QuizGUID
END