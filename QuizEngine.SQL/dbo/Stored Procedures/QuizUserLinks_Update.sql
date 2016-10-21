CREATE PROCEDURE [dbo].[QuizUserLinks_Update]
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier,
	@Result decimal(5,2) = null,
	@QuizDate   Datetime,
	@OnlineOrDownloaded bit,
	@IsTaken bit = null
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.QuizUserLinks
	SET UserGUID = @UserGUID,
		Result = @Result,
		QuizDate = @QuizDate,
		OnlineOrDownloaded = @OnlineOrDownloaded,
		IsTaken =@IsTaken
	WHERE QuizGUID = @QuizGUID
END
