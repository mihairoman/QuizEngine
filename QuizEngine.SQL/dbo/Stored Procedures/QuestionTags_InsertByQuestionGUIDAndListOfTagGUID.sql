CREATE PROCEDURE [dbo].[QuestionTags_InsertByQuestionGUIDAndListOfTagGUID]
	@QuestionGUID UNIQUEIDENTIFIER,
    @TagsGUIDList NVARCHAR(MAX)
AS
BEGIN

	

	IF (LEN(@TagsGUIDList) > 0)
	BEGIN
		INSERT INTO [dbo].[QuestionTags] 
			([QuestionGUID],[TagGUID])
		SELECT @QuestionGUID, ItemID 
		FROM dbo.SplitGuidStringList(@TagsGUIDList);
	END;
END;