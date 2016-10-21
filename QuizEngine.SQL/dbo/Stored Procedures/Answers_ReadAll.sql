create PROCEDURE [dbo].[Answers_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;	
	select QuizGUID,ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID 
	from [Answers]	    
END


