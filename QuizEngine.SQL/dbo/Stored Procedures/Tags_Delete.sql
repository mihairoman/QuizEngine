--QuestionTags Delete
create PROCEDURE [dbo].[Tags_Delete]
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Tags
	WHERE TagGUID = @TagGUID
END