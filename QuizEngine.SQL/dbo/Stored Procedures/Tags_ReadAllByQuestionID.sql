CREATE PROCEDURE [dbo].[Tags_ReadAllByQuestionID]
	@QuestionGUID uniqueidentifier
AS
BEGIN
	SELECT t.TagGUID, t.TagName FROM [dbo].[Tags] t
	INNER JOIN [dbo].[QuestionTags] qt ON qt.TagGUID = t.TagGUID
	WHERE qt.QuestionGUID = @QuestionGUID
END