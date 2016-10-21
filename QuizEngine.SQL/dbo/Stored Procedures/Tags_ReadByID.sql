--QuestionTags Delete
create PROCEDURE [dbo].[Tags_ReadByID]
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	Select TagGUID,TagName FROM Tags
	WHERE TagGUID = @TagGUID
END