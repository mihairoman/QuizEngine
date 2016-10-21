CREATE PROCEDURE [dbo].[QuizResultView_ReadResult]
@QuizGUID uniqueidentifier,
@UserGUID uniqueidentifier
AS
BEGIN
 
  DECLARE @UserAnswers TABLE (
	QuizGuid uniqueidentifier,
	ArchivedQuestionGuid uniqueidentifier,
	Answered bit,
	ArchivedChoiceGuid uniqueidentifier,
	IsCorrect bit,
	Value decimal(5,2),
	Grade decimal(5,2)
)

INSERT INTO @UserAnswers
 SELECT  AQ.QuizGUID,
		 AQ.ArchivedQuestionGUID,
		 (CASE WHEN (A.ArchivedQuestionGUID is null) THEN 0 ELSE 1 END ) as  Answered,
		 AC.ChoiceGUID,
		 AC.IsCorrect,
		 AC.Value,
		 A.Grade
 FROM  ArchivedQuestions as AQ 
 INNER JOIN ArchivedChoices as AC ON AQ.ArchivedQuestionGUID = AC.ArchivedQuestionGUID
 LEFT JOIN Answers as A ON A.ArchivedQuestionGUID = AC.ArchivedQuestionGUID AND AC.ChoiceGUID = a.ArchivedChoiceGUID
 WHERE @QuizGUID = AQ.QuizGUID 
		AND
	   @UserGUID = a.UserGUID
 ORDER BY AQ.ArchivedQuestionGUID, ChoiceGUID

 SELECT      AQ.QuizGUID,
			 AQ.ArchivedQuestionGUID as QuestionID,
			 "Result" = 
			 CASE AQ.QuestionType 
				 WHEN 2 then				  			 
					 CASE (SELECT Answered FROM @UserAnswers as UA where ua.ArchivedQuestionGuid = AQ.ArchivedQuestionGUID and ua.IsCorrect=1)
																																		 WHEN 1 THEN 1
																																		 ELSE 0
					 END 
				WHEN 1 THEN							
					CASE
					WHEN EXISTS(
								SELECT count(UA.Answered) 
								as PartialResult
								FROM @UserAnswers as UA 
								WHERE ua.ArchivedQuestionGuid = AQ.ArchivedQuestionGUID
								and ua.IsCorrect=1
								HAVING count(UA.Answered)  = (SELECT count(AC1.ChoiceGUID) FROM ArchivedChoices as AC1
								WHERE ac1.ArchivedQuestionGUID=AQ.ArchivedQuestionGUID and AC1.IsCorrect=1)) 
																																THEN 1
																																ELSE 0
					END	
				WHEN 4 THEN 
					(SELECT UA.Value FROM @UserAnswers as UA WHERE ua.ArchivedQuestionGuid = AQ.ArchivedQuestionGUID and ua.Answered=1)				
				WHEN 0 THEN 
					(SELECT UA.Grade from @UserAnswers as UA WHERE ua.ArchivedQuestionGuid = AQ.ArchivedQuestionGUID and ua.Answered=1)
				WHEN 3 then 
					(SELECT UA.Grade from @UserAnswers as UA WHERE ua.ArchivedQuestionGuid = AQ.ArchivedQuestionGUID and ua.Answered=1)					
			END		 
	 FROM ArchivedQuestions as AQ
	 WHERE AQ.QuizGUID = @QuizGUID
	 	
END
