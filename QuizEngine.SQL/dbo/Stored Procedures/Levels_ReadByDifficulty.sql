CREATE PROCEDURE [dbo].[Levels_ReadByDifficulty]
	@Taglist nvarchar(50),
	@Difficulty smallint,
	@Number smallint,
	@Difminus smallint,
	@Difplus smallint,
	@Num smallint,
	@Category nvarchar(50),
	@Questionlist nvarchar(50)
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @count int
	set @count = (select Count(*) from Questions)

	declare @Tags table
	(
		Data nvarchar(250)
	)

	declare @Questions table 
	(
		Data nvarchar(250)
	)

	INSERT INTO @Tags
		SELECT *
		FROM dbo.SplitStringList(@Taglist)

	INSERT INTO @Questions
		SELECT *
		FROM dbo.SplitStringList(@Questionlist)

	IF NOT EXISTS(
		SELECT TOP(@Num) q.QuestionGUID, q.Text, q.QuestionType,q.CategoryGUID,q.LevelGUID, t.TagName
		FROM Questions q 
			INNER JOIN Levels l ON l.LevelGUID = q.LevelGUID
			INNER JOIN Categories c ON c.CategoryGUID = q.CategoryGUID
			INNER JOIN QuestionTags qt ON qt.QuestionGUID = q.QuestionGUID
			INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
			INNER JOIN @Tags tl ON tl.Data = t.TagName
			INNER JOIN @Questions qq ON qq.Data = q.QuestionType
		WHERE l.Difficulty=@Difminus 
		AND c.CategoryName=@Category)
	BEGIN
		SET @Number = @Num + @Number
	END
		
	 IF NOT EXISTS(
		SELECT TOP(@Num) q.QuestionGUID, q.Text, q.QuestionType,q.CategoryGUID,q.LevelGUID, t.TagName
		FROM Questions q 
			INNER JOIN Levels l ON l.LevelGUID = q.LevelGUID
			INNER JOIN Categories c ON c.CategoryGUID = q.CategoryGUID
			INNER JOIN QuestionTags qt ON qt.QuestionGUID = q.QuestionGUID
			INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
			INNER JOIN @Tags tl ON tl.Data = t.TagName
			INNER JOIN @Questions qq ON qq.Data = q.QuestionType
		WHERE l.Difficulty=@Difplus
		AND c.CategoryName=@Category)
	BEGIN
		SET @Number = @Num + @Number
	END

select * from(
	select top(@Number) * from
	(
		SELECT top(@count) q.QuestionGUID, q.Text, q.QuestionType,q.CategoryGUID,q.LevelGUID, [dbo].[QuestionMultipleTags](q.QuestionGUID) as Tags
		FROM Questions q 
			INNER JOIN Levels l ON l.LevelGUID = q.LevelGUID
			INNER JOIN Categories c ON c.CategoryGUID = q.CategoryGUID
			INNER JOIN QuestionTags qt ON qt.QuestionGUID = q.QuestionGUID
			INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
			INNER JOIN @Tags tl ON tl.Data = t.TagName
			INNER JOIN @Questions qq ON qq.Data = q.QuestionType
		WHERE l.Difficulty=@Difficulty
		AND c.CategoryName=@Category
		order by NEWID()) s
		) d
	
	UNION
	select * from(
	select top(@Num) * from
	(
		SELECT   top(@count) q.QuestionGUID, q.Text, q.QuestionType,q.CategoryGUID,q.LevelGUID, [dbo].[QuestionMultipleTags](q.QuestionGUID) as Tags
		FROM Questions q 
			INNER JOIN Levels l ON l.LevelGUID = q.LevelGUID
			INNER JOIN Categories c ON c.CategoryGUID = q.CategoryGUID
			INNER JOIN QuestionTags qt ON qt.QuestionGUID = q.QuestionGUID
			INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
			INNER JOIN @Tags tl ON tl.Data = t.TagName
			INNER JOIN @Questions qq ON qq.Data = q.QuestionType
		WHERE l.Difficulty=@Difminus
		AND c.CategoryName=@Category
		order by NEWID()) a
		) b

	UNION
	select * from (
	select top(@Num) * from
	(

		SELECT top(@count) q.QuestionGUID, q.Text, q.QuestionType,q.CategoryGUID,q.LevelGUID, [dbo].[QuestionMultipleTags](q.QuestionGUID) as Tags
		FROM Questions q 
			INNER JOIN Levels l ON l.LevelGUID = q.LevelGUID
			INNER JOIN Categories c ON c.CategoryGUID = q.CategoryGUID
			INNER JOIN QuestionTags qt ON qt.QuestionGUID = q.QuestionGUID
			INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
			INNER JOIN @Tags tl ON tl.Data = t.TagName
			INNER JOIN @Questions qq ON qq.Data = q.QuestionType
		WHERE l.Difficulty=@Difplus
		AND c.CategoryName=@Category
		order by NEWID()) w
		) x
END