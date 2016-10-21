CREATE PROCEDURE [dbo].[QuizUserLinks_ReadByUserId]
	@UserGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloaded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE UserGUID = @UserGUID
END