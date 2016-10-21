CREATE PROCEDURE [dbo].[FreeTextQuizView_CountAllFilteredQuizzes] 
AS
BEGIN
	SET NOCOUNT ON;
	Select COUNT(*)
	FROM Users, QuizUserLinks, Levels, Categories, Quizes
	WHERE QuizUserLinks.UserGUID = Users.UserGUID
	AND Quizes.QuizGUID=QuizUserLinks.QuizGUID
	AND Quizes.LevelGUID=Levels.LevelGUID
	AND Quizes.CategoryGUID=Categories.CategoryGUID
	AND Result IS NULL
	AND OnlineOrDownloaded = 1
	AND IsTaken = 1
END