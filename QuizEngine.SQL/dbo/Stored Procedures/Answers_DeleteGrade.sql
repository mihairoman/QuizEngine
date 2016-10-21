CREATE PROCEDURE [dbo].[Answers_DeleteGrade]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade = 0.0 
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID and UserGUID=@UserGUID
END


