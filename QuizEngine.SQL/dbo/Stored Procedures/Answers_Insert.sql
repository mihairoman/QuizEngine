create PROCEDURE [dbo].[Answers_Insert]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(5, 2),
@AnswerText nvarchar(MAX),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
INSERT INTO [Answers] VALUES(@QuizGUID,@ArchivedQuestionGUID,@Grade,@AnswerText,@UserGUID,@ArchivedChoiceGUID)	
END


