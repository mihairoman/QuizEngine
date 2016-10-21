--QuestionTags Delete
create PROCEDURE [dbo].[QuestionTags_Delete]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM QuestionTags
	WHERE QuestionGUID=@QuestionGUID AND TagGUID=@TagGUID
END