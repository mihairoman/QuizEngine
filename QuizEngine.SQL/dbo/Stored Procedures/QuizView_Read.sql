CREATE PROCEDURE [dbo].[QuizView_Read]
	@PageNumber as int = NULL,
	@RowPerPage as int =NULL,
	@SortExpression as NVARCHAR(MAX) = 'dbo.Quizes.QuizGUID',
	@UserGUID as uniqueidentifier = NULL
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @SqlText as NVARCHAR(MAX);
	DECLARE @Parameters as NVARCHAR(MAX);
	SET @Parameters =N'@FoundPageNumber  INT,@FoundRowPerPage INT, @FoundUserGuid uniqueidentifier';	--SET THE FORMAL PARAMETERS
	SET @SqlText = 'SELECT dbo.Users.Username , dbo.Quizes.QuizGUID , dbo.QuizUserLinks.Result , dbo.Categories.CategoryName , dbo.Levels.LevelName, 
	dbo.QuizUserLinks.UserGUID , QuizDate , OnlineOrDownloaded , IsTaken, dbo.Levels.Difficulty, dbo.Quizes.Time
	FROM Quizes JOIN dbo.QuizUserLinks ON Quizes.QuizGUID = dbo.QuizUserLinks.QuizGUID
		 JOIN dbo.Users ON Users.UserGUID = dbo.QuizUserLinks.UserGUID , dbo.Levels , dbo.Categories
	WHERE Quizes.LevelGUID = Levels.LevelGUID AND Quizes.CategoryGUID = Categories.CategoryGUID';
	IF (@UserGUID is not NULL)
		BEGIN
			SET @SqlText = @SqlText + ' AND dbo.QuizUserLinks.UserGUID= @FoundUserGUID ';
			
		END

    SET @SqlText = @SqlText + ' ORDER BY ' + @SortExpression;
	  
	IF (@PageNumber is not null AND @RowPerPage IS NOT NULL)--OR (@PageNumber != -1 AND @RowPerPage != -1))
		BEGIN
			SET @SqlText = @SqlText + ' OFFSET  ((@FoundPageNumber-1)* @FoundRowPerPage)  ROWS FETCH NEXT @FoundrowPerPage ROWS ONLY';
		END
   EXECUTE sp_executesql @SqlText, @Parameters,
			@FoundPageNumber = @PageNumber,
			@FoundRowPerPage = @RowPerPage,
			@FoundUserGUID   = @UserGUID;			
END