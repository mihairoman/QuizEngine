---------------------------------------  UPDATE A CHOISE

CREATE PROCEDURE [dbo].[Choices_Update]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(5,2) = null,
	@IsCorrect bit = null,
	@ChoicePosition int =null

AS
BEGIN

	UPDATE [Choices] 
	SET QuestionGUID = @QuestionGUID, AnswerText = @AnswerText, Value=@Value, IsCorrect = @IsCorrect, ChoicePosition=@ChoicePosition
	WHERE ChoiceGUID = @ChoiceGUID

END;


