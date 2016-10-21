CREATE FUNCTION [dbo].[QuestionMultipleTags] 
(
	@QuestionGUID NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @TagList NVARCHAR(MAX)

	SELECT @TagList = COALESCE(@TagList + ', ' , '') + t.TagName
	FROM Tags t
	INNER JOIN QuestionTags qt on t.TagGUID = qt.TagGUID
	INNER JOIN Questions q on qt.QuestionGUID = q.QuestionGUID
	WHERE q.QuestionGUID = @QuestionGUID

	RETURN @TagList
END