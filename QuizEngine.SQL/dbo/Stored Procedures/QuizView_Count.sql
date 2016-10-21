CREATE PROCEDURE  [dbo].[QuizView_Count]
	@UserGUID as uniqueidentifier = NULL
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @SqlText nvarchar(MAX);
	DECLARE @Parameters nvarchar(max);
	SET @Parameters = N'@FoundUserGuid uniqueidentifier';
	SET @SqlText = 'SELECT COUNT(*)
	FROM Quizes JOIN dbo.QuizUserLinks ON Quizes.QuizGUID = dbo.QuizUserLinks.QuizGUID
		 JOIN dbo.Users ON Users.UserGUID = dbo.QuizUserLinks.UserGUID , dbo.Levels , dbo.Categories
	WHERE Quizes.LevelGUID = Levels.LevelGUID AND Quizes.CategoryGUID = Categories.CategoryGUID';
	IF (@UserGUID is not NULL)
		BEGIN
			SET @SqlText = @SqlText + ' AND dbo.QuizUserLinks.UserGUID= @FoundUserGUID ';	
		END
     EXECUTE sp_executesql @SqlText, @Parameters,
			@FoundUserGUID   = @UserGUID;	
END
