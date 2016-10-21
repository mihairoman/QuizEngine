
---------------------------------------  UPDATE A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Update]
	@Text NVARCHAR(MAX),
	@QuestionType SMALLINT,
	@CategoryGUID UNIQUEIDENTIFIER,
	@LevelGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	UPDATE Questions 
	SET Text = @Text, QuestionType = @QuestionType, CategoryGUID = @CategoryGUID, LevelGUID = @LevelGUID
	WHERE QuestionGUID = @QuestionGUID

END;


