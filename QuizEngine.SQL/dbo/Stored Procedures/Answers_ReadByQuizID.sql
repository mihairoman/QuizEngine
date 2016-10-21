
create PROCEDURE [dbo].[Answers_ReadByQuizID] 
@QuizGUID	uniqueidentifier
AS
BEGIN
	select ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID from [Answers]
	where QuizGUID=@QuizGUID
END
