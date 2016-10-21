CREATE PROCEDURE [dbo].[QuestionTagViews_ReadAll]
	 @tagName nvarchar(50)

AS
BEGIN

	SELECT q.Text, t.TagName
FROM Questions q
	INNER JOIN QuestionTags qt ON q.QuestionGUID = qt.QuestionGUID
	INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
WHERE t.TagName = @tagName

END;


