CREATE PROCEDURE [dbo].[Users_WithoutQuizID_CountAll]
    @QuizGUID UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM (
		(SELECT distinct u.UserGUID, u.Username, u.Usertype, u.IsActive 
		FROM Users u
		where u.IsActive=1)
		except 
		( select u.UserGUID, u.Username, u.Usertype, u.IsActive
		from QuizUserLinks q 
		inner join Users u on u.UserGUID=q.UserGUID
		where q.QuizGUID=@QuizGUID )) x
END
