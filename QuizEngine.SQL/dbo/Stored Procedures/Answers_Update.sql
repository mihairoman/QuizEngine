CREATE PROCEDURE [dbo].[Answers_Update]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(5, 2),
@AnswerText nvarchar(MAX),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade=@Grade,AnswerText=@AnswerText
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID and UserGUID=@UserGUID
END


