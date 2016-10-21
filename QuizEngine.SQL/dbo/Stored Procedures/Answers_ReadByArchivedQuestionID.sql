
Create PROCEDURE [dbo].[Answers_ReadByArchivedQuestionID]
@ArchivedQuestionID	uniqueidentifier
AS
BEGIN
	select QuizGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID from dbo.[Answers]
END
