CREATE PROCEDURE [dbo].[ArchivedChoices_Delete]
		@ChoiceGUID uniqueidentifier,
		@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ArchivedChoices
	WHERE  ChoiceGUID = @ChoiceGUID
		   AND ArchivedQuestionGUID = @ArchivedQuestionGUID

END