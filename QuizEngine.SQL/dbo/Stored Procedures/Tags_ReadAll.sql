--QuestionTags Delete
CREATE PROCEDURE [dbo].[Tags_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	Select TagGUID,TagName 
	FROM Tags
	ORDER BY TagName
END