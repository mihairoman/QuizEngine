CREATE PROCEDURE [dbo].[Categories_CountUsingQuestions]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT c.CategoryGUID, c.CategoryName, COUNT(q.QuestionGUID) AS NumberOfQuestions
	FROM Categories c
	LEFT JOIN Questions q ON c.CategoryGUID = q.CategoryGUID
	GROUP BY c.CategoryGUID, c.CategoryName
	ORDER BY c.CategoryName
	    
END