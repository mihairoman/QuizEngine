CREATE PROCEDURE [dbo].[Categories_CanBeDeleted]
	@CategoryGUID uniqueidentifier, @ReturnValue bit output
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT q.QuestionGUID FROM Questions q WHERE q.CategoryGUID = @CategoryGUID) OR 
		EXISTS (SELECT aq.ArchivedQuestionGUID FROM ArchivedQuestions aq WHERE aq.CategoryGUID = @CategoryGUID) OR
		EXISTS (SELECT qu.QuizGUID FROM Quizes qu WHERE qu.CategoryGUID = @CategoryGUID)
	BEGIN
		SET @ReturnValue = 0
	END
	ELSE
	BEGIN
		SET @ReturnValue = 1
	END
END