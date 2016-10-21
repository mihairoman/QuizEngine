CREATE PROCEDURE [dbo].[Quiz_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT QuizGUID , LevelGUID , CategoryGUID , Quizes.Time FROM Quizes
END