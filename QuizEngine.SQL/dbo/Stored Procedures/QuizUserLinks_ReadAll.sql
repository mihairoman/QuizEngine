CREATE PROCEDURE [dbo].[QuizUserLinks_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result, QuizDate , OnlineOrDownloaded , IsTaken   
		FROM dbo.QuizUserLinks
END

