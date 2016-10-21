--QuestionTags Delete
create PROCEDURE [dbo].[QuestionTags_ReadByID]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuestionGUID,TagGUID 
	FROM QuestionTags
	WHERE QuestionGUID=@QuestionGUID AND TagGUID=@TagGUID
END