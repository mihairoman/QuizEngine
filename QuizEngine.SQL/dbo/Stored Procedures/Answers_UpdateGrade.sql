CREATE PROCEDURE [dbo].[Answers_UpdateGrade]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(5, 2),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE dbo.[Answers] SET Grade=@Grade
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID  and UserGUID=@UserGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID
END