CREATE PROCEDURE [dbo].[FreeTextQuestionsView_ReadAll] 
@QuizGUID uniqueidentifier , 
@UserGUID uniqueidentifier 
AS
SELECT aq.QuizGUID, ans.UserGUID, aq.ArchivedQuestionGUID, ans.ArchivedChoiceGUID, aq.QuestionType, aq.ArchivedQuestionText,ans.AnswerText,ans.Grade
From ArchivedQuestions aq, Answers ans
Where aq.QuizGUID=@QuizGUID
and ans.ArchivedQuestionGUID=aq.ArchivedQuestionGUID 
and ans.UserGUID=@UserGUID