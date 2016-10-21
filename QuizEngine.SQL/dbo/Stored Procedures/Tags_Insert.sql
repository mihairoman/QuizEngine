--QuestionTags Insert
create PROCEDURE [dbo].[Tags_Insert]
	@TagGUID uniqueidentifier, 
	@TagName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Tags] VALUES (@TagGUID, @TagName)
END