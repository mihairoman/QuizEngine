CREATE PROCEDURE [dbo].[QuizUserLinks_ReadByQuizIDandUserID]
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuizGUID, UserGUID, Result, QuizDate, OnlineOrDownloaded, IsTaken
	FROM QuizUserLinks
	WHERE QuizGUID = @QuizGUID and UserGUID = @UserGUID
   
END