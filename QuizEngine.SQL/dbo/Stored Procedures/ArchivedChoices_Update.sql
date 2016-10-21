CREATE PROCEDURE [dbo].[ArchivedChoices_Update]
	
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(5,2) = null,
	@IsCorrect bit = null

AS
BEGIN
	SET NOCOUNT ON;

   UPDATE [ArchivedChoices] 
   SET 
		AnswerText = @AnswerText,
		Value = @Value,
		IsCorrect = @IsCorrect
	WHERE 
		ChoiceGUID = @ChoiceGUID 
		AND ArchivedQuestionGUID = @ArchivedQuestionGUID
	
END