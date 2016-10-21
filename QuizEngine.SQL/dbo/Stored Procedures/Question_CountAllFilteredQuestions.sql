CREATE PROCEDURE [dbo].[Question_CountAllFilteredQuestions] 
		@Category AS uniqueidentifier = null ,
	@Level AS uniqueidentifier = null,
	@Tag AS NVARCHAR(MAX)  = null,
	@Type AS NVARCHAR(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);
	
	Set @SqlText = '
		Select COUNT(DISTINCT q.QuestionGUID)
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

	

	

	SET @ParmDefinition = N'@FoundTag NVARCHAR(MAX), @FoundType NVARCHAR(MAX),@FoundCategory uniqueidentifier,@FoundLevel uniqueidentifier';

	EXECUTE sp_executesql @SqlText, @ParmDefinition,
                      @FoundTag = @Tag,
					  @FoundType = @Type,
					  @FoundCategory = @Category,
					  @FoundLevel = @Level
					  
END