
CREATE PROCEDURE [dbo].[TemplateQuestionLinks_Insert]
	@TemplateQuizGUID uniqueidentifier,
	@QuestionGUID uniqueidentifier
	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.TemplateQuestionLinks VALUES (@TemplateQuizGUID, @QuestionGUID)
END
