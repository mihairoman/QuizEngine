--QuestionTags Update
create PROCEDURE [dbo].[Tags_Update]
	@TagGUID uniqueidentifier,
	@TagName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Tags
	SET TagName = @TagName
	WHERE TagGUID = @TagGUID
END