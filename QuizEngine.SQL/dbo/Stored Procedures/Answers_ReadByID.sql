
create PROCEDURE [dbo].[Answers_ReadByID]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	
	select QuizGUID,ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID 
	from [Answers] 
	where QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID =@ArchivedChoiceGUID and UserGUID=@UserGUID
	    
END


