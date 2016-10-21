CREATE PROCEDURE [dbo].[QuizesPerUserView_ReadAll]
	@UserGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT q1.QuizGUID, q1.QuizDate, c.CategoryName, l.LevelName, q1.OnlineOrDownloaded, q1.Result, q1.IsTaken
	FROM QuizUserLinks q1
		INNER JOIN Quizes q2 ON q1.QuizGUID = q2.QuizGUID
		INNER JOIN Levels l ON q2.LevelGUID = l.LevelGUID
		INNER JOIN Categories c ON q2.CategoryGUID = c.CategoryGUID
	WHERE q1.UserGUID = @UserGUID
	ORDER BY Q1.QuizDate
END