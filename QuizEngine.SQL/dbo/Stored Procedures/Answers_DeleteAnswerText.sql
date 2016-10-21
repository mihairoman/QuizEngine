CREATE PROCEDURE [dbo].[Answers_DeleteAnswerText]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET AnswerText=' '
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID and UserGUID=@UserGUID

END


