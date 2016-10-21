CREATE PROCEDURE [dbo].[Question_ReadAllFiltered]
	@Category AS uniqueidentifier = null ,
	@Level AS uniqueidentifier = null,
	@Tag AS NVARCHAR(MAX)  = null,
	@Type AS NVARCHAR(MAX) = null,
	@SortExpression AS NVARCHAR(MAX) = null,
	@PageNumber AS INT ,
	@RowspPage AS INT
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);
	
	Set @SqlText = '
		Select DISTINCT q.QuestionGUID, q.Text, q.QuestionType as Type, c.CategoryName as Category ,l.LevelName as Level, [dbo].[QuestionMultipleTags](q.QuestionGUID) as Tag
	 FROM [dbo].Questions q
	 INNER JOIN QuestionTags qt ON q.QuestionGUID = qt.QuestionGUID
	INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
	INNER JOIN Categories c ON q.CategoryGUID = c.CategoryGUID
	INNER JOIN Levels l ON q.LevelGUID = l.LevelGUID';
	

	IF (@Tag IS NOT NULL)
	BEGIN
		SET @SqlText = @SqlText + ' INNER JOIN (SELECT ItemID FROM [dbo].[SplitGuidStringList](@FoundTag)) ft ON ft.ItemID = qt.TagGUID';
	END

	IF (@Type IS NOT NULL)
	BEGIN
		SET @SqlText = @SqlText + ' INNER JOIN (SELECT ItemID FROM [dbo].[SplitStringList] (@FoundType)) qtft on q.QuestionType=qtft.ItemID';
	END

	IF ((@Category IS NOT NULL) OR (@Level IS NOT NULL))
	BEGIN
		SET @SqlText = @SqlText + ' WHERE';

			IF (@Category IS NOT  NULL) 
			BEGIN
				SET @SqlText = @SqlText + ' c.CategoryGUID = @FoundCategory';
			END
			IF ((@Category IS NOT NULL) AND (@Level IS NOT NULL))
			BEGIN 
			SET @SqlText = @SqlText + ' AND';
			END
			IF (@Level IS NOT NULL) 
			BEGIN
				SET @SqlText = @SqlText + ' l.LevelGUID=@FoundLevel';
			END
		END

	IF (@SortExpression IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
	END


	SET @SqlText = @SqlText + ' OFFSET ((@FoundPageNumber - 1) * @FoundRowspPage) ROWS
								FETCH NEXT @FoundRowspPage ROWS ONLY';

	SET @ParmDefinition = N'@FoundTag NVARCHAR(MAX), @FoundType NVARCHAR(MAX),@FoundCategory uniqueidentifier,@FoundLevel uniqueidentifier,@FoundPageNumber INT,@FoundRowspPage INT';

	EXECUTE sp_executesql @SqlText, @ParmDefinition,
                      @FoundTag = @Tag,
					  @FoundType = @Type,
					  @FoundCategory = @Category,
					  @FoundLevel = @Level,
					  @FoundPageNumber=@PageNumber,
					  @FoundRowspPage = @RowspPage;
END