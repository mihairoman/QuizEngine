CREATE PROCEDURE [dbo].[QuizUserLink_Insert]
	@QuizGUID uniqueidentifier,
	@UserGuid uniqueidentifier,
	@Result decimal(5,2) = null,
	@QuizDate   Datetime,
	@OnlineOrDownloaded bit,
	@IsTaken bit = null
AS
BEGIN
	DECLARE  @FoundGUID uniqueidentifier;
	SEt @FoundGUID = (SELECT ql.QuizGUID FROM dbo.QuizUserLinks ql where QL.QuizGUID = @QuizGUID);
	
	if @FoundGUID IS NULL
	BEGIN
		INSERT INTO dbo.QuizUserLinks VALUES (@QuizGUID, @UserGuid , @Result, @QuizDate, @OnlineOrDownloaded, @IsTaken)
	END
END