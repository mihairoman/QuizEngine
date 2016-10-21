CREATE PROCEDURE [dbo].[QuizUserLinks_UpdateFinalResult]
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier,
	@Result decimal(5,2)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.QuizUserLinks
	SET Result = @Result
	WHERE QuizGUID = @QuizGUID and UserGUID=@UserGUID
END