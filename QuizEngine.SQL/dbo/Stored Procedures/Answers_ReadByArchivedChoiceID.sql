
create PROCEDURE [dbo].[Answers_ReadByArchivedChoiceID]
@ArchivedChoiceGUID	uniqueidentifier
AS
BEGIN
	select QuizGUID,ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID from dbo.[Answers]	
	where ArchivedChoiceGUID=@ArchivedChoiceGUID
END
