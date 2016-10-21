CREATE PROCEDURE [dbo].[GeneratedTests_ReadAllPredefined]
    @SortExpression nvarchar(max),
	@FoundrowPerPage INT = NULL,
	@FoundPageNumber INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);

	SET @ParmDefinition = N'@FoundPageNumber INT,@FoundrowPerPage INT';
	
	Set @SqlText = 'SELECT qt.QuizTemplateGUID,qt.TypeName
	FROM QuizTemplates qt';
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

