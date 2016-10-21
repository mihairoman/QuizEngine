CREATE PROCEDURE [dbo].[ArchivedChoices_ReadByQuestionIDS]
	@QuestionGUIDS NVARCHAR(MAX)

AS
BEGIN
	
	SET NOCOUNT ON;

	declare @QuestionIDs table
	(
		QuestionGUID uniqueidentifier
	)

	INSERT INTO @QuestionIDs
		SELECT * FROM dbo.SplitGuidStringList(@QuestionGUIDS)

	SELECT ac.ChoiceGUID, ac.ArchivedQuestionGUID, ac.AnswerText, ac.Value, ac.IsCorrect
	FROM ArchivedChoices ac
		INNER JOIN @QuestionIDs qg ON qg.QuestionGUID = ac.ArchivedQuestionGUID
	ORDER BY ChoicePosition

END