CREATE PROCEDURE [dbo].[ArchivedChoices_Insert]
	-- Add the parameters for the stored procedure here
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(5,2) = null,
	@IsCorrect bit = null,
	@ChoicePosition int = null
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO ArchivedChoices
	VALUES (@ChoiceGUID,@ArchivedQuestionGUID,@AnswerText,@Value,@IsCorrect,@ChoicePosition)
END