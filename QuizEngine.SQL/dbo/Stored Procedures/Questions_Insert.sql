
---------------------------------------  INSERT A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Insert]
	@QuestionGUID UNIQUEIDENTIFIER,
	@Text NVARCHAR(MAX),
	@QuestionType smallint,
	@CategoryGUID UNIQUEIDENTIFIER,
	@LevelGUID UNIQUEIDENTIFIER

AS
BEGIN

	INSERT INTO [Questions]
	Values (@QuestionGUID, @Text, @QuestionType, @CategoryGUID,@LevelGUID)

END;


