CREATE PROCEDURE [dbo].[Quiz_ReadById]
		@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuizGUID, LevelGUID, CategoryGUID , Quizes.Time FROM Quizes
	WHERE QuizGUID = @QuizGUID
   
END