
---------------------------------------  INSERT A CHOISE
CREATE PROCEDURE [dbo].[Choices_Insert]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(5,2) = null,
	@IsCorrect bit = null,
	@ChoicePosition int = null
AS
BEGIN

	INSERT
	INTO [Choices]
	VALUES(@ChoiceGUID,@QuestionGUID,@AnswerText,@Value,@IsCorrect,@ChoicePosition)

END;


