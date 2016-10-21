CREATE PROCEDURE [dbo].[GeneratedTests_ReadAllRandom]
    @SortExpression nvarchar(max),
	@FoundrowPerPage INT = NULL,
	@FoundPageNumber INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);

	SET @ParmDefinition = N'@FoundPageNumber INT,@FoundrowPerPage INT';
	
	Set @SqlText = 'SELECT q.QuizGUID, q.LevelGUID, q.CategoryGUID, l.LevelName, l.Difficulty, c.CategoryName
	FROM Quizes q
	inner join Levels l on q.LevelGUID=l.LevelGUID 
	inner join Categories c on q.CategoryGUID=c.CategoryGUID
	WHERE 
	q.QuizGUID not in (select qt.QuizTemplateGUID from QuizTemplates qt where qt.QuizTemplateGUID=q.QuizGUID)';
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