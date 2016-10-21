--QuestionTags Insert
create PROCEDURE [dbo].[QuestionTags_Insert]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO QuestionTags VALUES (@QuestionGUID,@TagGUID)
END