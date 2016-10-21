CREATE PROCEDURE [dbo].[Quiz_Update]
	@QuizGUID uniqueidentifier,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Quizes] 
		SET LevelGUID = @LevelGUID,
		    CategoryGUID = @CategoryGUID
		WHERE
			QuizGUID  = @QuizGUID
   
END