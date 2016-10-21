CREATE PROCEDURE [dbo].[QuizUserLink_InsertForGeneratingNewLink]
	@QuizGUID uniqueidentifier,
	@UserGuid uniqueidentifier,
	@Result decimal(5,2) = null,
	@QuizDate   Datetime,
	@OnlineOrDownloaded bit,
	@IsTaken bit = null
AS
BEGIN
    INSERT INTO dbo.QuizUserLinks VALUES (@QuizGUID, @UserGuid , @Result, @QuizDate, @OnlineOrDownloaded, @IsTaken)
	
END
