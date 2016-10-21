CREATE PROCEDURE [dbo].[QuizTemplates_Insert]
	@QuizTemplateGUID uniqueidentifier,
	@TypeName nvarchar(max),
	@Time time(7)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.QuizTemplates VALUES (@QuizTemplateGUID , @TypeName, @Time)
   
END