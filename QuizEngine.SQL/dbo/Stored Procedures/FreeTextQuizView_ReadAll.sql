CREATE PROCEDURE [dbo].[FreeTextQuizView_ReadAll]

	@SortExpression nvarchar(max),
	@FoundrowPerPage INT = NULL,
	@FoundPageNumber INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);

	SET @ParmDefinition = N'@FoundPageNumber INT,@FoundrowPerPage INT';
	
	Set @SqlText = ' SELECT Users.UserGUID, Quizes.QuizGUID, Username, QuizDate, CategoryName, LevelName
	FROM Users, QuizUserLinks, Levels, Categories, Quizes
	WHERE QuizUserLinks.UserGUID = Users.UserGUID
	AND Quizes.QuizGUID=QuizUserLinks.QuizGUID
	AND Quizes.LevelGUID=Levels.LevelGUID
	AND Quizes.CategoryGUID=Categories.CategoryGUID
	AND Result IS NULL
	AND OnlineOrDownloaded = 1
	AND IsTaken=1';
	

	IF (@SortExpression IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
	IF (@FoundPageNumber IS NOT NULL and @FoundrowPerPage IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' OFFSET  ((@FoundPageNumber-1)* @FoundrowPerPage)  ROWS FETCH NEXT @FoundrowPerPage ROWS ONLY';
	END
	END

	EXECUTE sp_executesql @SqlText, @ParmDefinition,
							@FoundrowPerPage = @FoundrowPerPage,
							@FoundPageNumber = @FoundPageNumber;
                    
END