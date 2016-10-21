﻿/*
Deployment script for QuizEngine

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "QuizEngine"
:setvar DefaultFilePrefix "QuizEngine"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Dropping FK_Quizes_ToCategories...';


GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [FK_Quizes_ToCategories];


GO
PRINT N'Dropping FK_Quizes_ToLevels...';


GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [FK_Quizes_ToLevels];


GO
PRINT N'Altering [dbo].[Choices]...';


GO
ALTER TABLE [dbo].[Choices]
    ADD [ChoicePosition] INT NULL;


GO
PRINT N'Altering [dbo].[Quizes]...';


GO
ALTER TABLE [dbo].[Quizes] ALTER COLUMN [CategoryGUID] UNIQUEIDENTIFIER NULL;

ALTER TABLE [dbo].[Quizes] ALTER COLUMN [LevelGUID] UNIQUEIDENTIFIER NULL;


GO
ALTER TABLE [dbo].[Quizes]
    ADD [Time] TIME (7) NULL;


GO
PRINT N'Altering [dbo].[QuizTemplates]...';


GO
ALTER TABLE [dbo].[QuizTemplates]
    ADD [Time] TIME (7) NULL;


GO
PRINT N'Creating FK_Quizes_ToCategories...';


GO
ALTER TABLE [dbo].[Quizes] WITH NOCHECK
    ADD CONSTRAINT [FK_Quizes_ToCategories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]);


GO
PRINT N'Creating FK_Quizes_ToLevels...';


GO
ALTER TABLE [dbo].[Quizes] WITH NOCHECK
    ADD CONSTRAINT [FK_Quizes_ToLevels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID]);


GO
PRINT N'Altering [dbo].[Choices_Insert]...';


GO

---------------------------------------  INSERT A CHOISE
ALTER PROCEDURE [dbo].[Choices_Insert]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(5,2) = null,
	@IsCorrect bit = null,
	@ChoicePosition int = null
AS
BEGIN

	INSERT
	INTO [Choices]
	VALUES(@ChoiceGUID,@QuestionGUID,@AnswerText,@Value,@IsCorrect,@ChoicePosition)

END;
GO
PRINT N'Altering [dbo].[Choices_ReadAll]...';


GO

---------------------------------------  SELECT ALL CHOISES
ALTER PROCEDURE [dbo].[Choices_ReadAll]

AS
BEGIN

SELECT ChoiceGUID, QuestionGUID, AnswerText, Value, IsCorrect 
FROM [Choices] Order by ChoicePosition

END;
GO
PRINT N'Altering [dbo].[Choices_ReadByQuestionID]...';


GO
ALTER PROCEDURE [dbo].[Choices_ReadByQuestionID]
	@QuestionGUID UNIQUEIDENTIFIER
AS
BEGIN
		SELECT TOP 1000 
			  c.ChoiceGUID
			, c.QuestionGUID
			, c.AnswerText
			, c.Value
			, c.IsCorrect
	  FROM [dbo].[Questions] as q
	   JOIN [dbo].[Choices] as c 
	  ON q.[QuestionGUID] = c.[QuestionGUID] 
	  WHERE q.QuestionGUID= @QuestionGUID
	  Order by ChoicePosition
END
GO
PRINT N'Altering [dbo].[Choices_Update]...';


GO
---------------------------------------  UPDATE A CHOISE

ALTER PROCEDURE [dbo].[Choices_Update]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(5,2) = null,
	@IsCorrect bit = null,
	@ChoicePosition int =null

AS
BEGIN

	UPDATE [Choices] 
	SET QuestionGUID = @QuestionGUID, AnswerText = @AnswerText, Value=@Value, IsCorrect = @IsCorrect, ChoicePosition=@ChoicePosition
	WHERE ChoiceGUID = @ChoiceGUID

END;
GO
PRINT N'Altering [dbo].[Quiz_Insert]...';


GO
ALTER PROCEDURE [dbo].[Quiz_Insert]
	@QuizGUID uniqueidentifier,
	@LevelGUID	uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@Time time(7) = null
AS
BEGIN
	DECLARE  @FoundGUID uniqueidentifier;
	SEt @FoundGUID = (SELECT q.QuizGUID FROM dbo.Quizes q where q.QuizGUID = @QuizGUID);
	
	if @FoundGUID IS NULL
	BEGIN
		INSERT INTO Quizes VALUES(@QuizGUID,@LevelGUID,@CategoryGUID,@Time)
	END
END
GO
PRINT N'Altering [dbo].[Quiz_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[Quiz_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT QuizGUID , LevelGUID , CategoryGUID , Quizes.Time FROM Quizes
END
GO
PRINT N'Altering [dbo].[Quiz_ReadById]...';


GO
ALTER PROCEDURE [dbo].[Quiz_ReadById]
		@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuizGUID, LevelGUID, CategoryGUID , Quizes.Time FROM Quizes
	WHERE QuizGUID = @QuizGUID
   
END
GO
PRINT N'Altering [dbo].[QuizTemplates_Insert]...';


GO
ALTER PROCEDURE [dbo].[QuizTemplates_Insert]
	@QuizTemplateGUID uniqueidentifier,
	@TypeName nvarchar(max),
	@Time time(7)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.QuizTemplates VALUES (@QuizTemplateGUID , @TypeName, @Time)
   
END
GO
PRINT N'Altering [dbo].[QuizTemplates_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[QuizTemplates_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName , dbo.QuizTemplates.Time
	FROM   dbo.QuizTemplates
   
END
GO
PRINT N'Altering [dbo].[QuizTemplates_ReadById]...';


GO
ALTER PROCEDURE [dbo].[QuizTemplates_ReadById]
@QuizTemplateGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName , dbo.QuizTemplates.Time
	FROM dbo.QuizTemplates
	WHERE QuizTemplateGUID = @QuizTemplateGUID
   
END
GO
PRINT N'Altering [dbo].[Categories_CountUsingQuestions]...';


GO
ALTER PROCEDURE [dbo].[Categories_CountUsingQuestions]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT c.CategoryGUID, c.CategoryName, COUNT(q.ArchivedQuestionGUID) AS NumberOfQuestions
	FROM Categories c
	LEFT JOIN ArchivedQuestions q ON c.CategoryGUID = q.CategoryGUID
	GROUP BY c.CategoryGUID, c.CategoryName
	ORDER BY c.CategoryName
	    
END
GO
PRINT N'Altering [dbo].[Categories_ReadAll]...';


GO
--Categories Delete
ALTER PROCEDURE [dbo].[Categories_ReadAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT CategoryGUID,CategoryName 
	FROM dbo.Categories 
	ORDER BY CategoryName
END
GO
PRINT N'Altering [dbo].[Levels_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[Levels_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	select LevelGUID,LevelName,Difficulty 
	From dbo.Levels
	ORDER BY LevelName
END
GO
PRINT N'Altering [dbo].[Levels_ReadByDifficulty]...';


GO
ALTER PROCEDURE [dbo].[Levels_ReadByDifficulty]
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
GO
PRINT N'Altering [dbo].[QuizView_Read]...';


GO
ALTER PROCEDURE [dbo].[QuizView_Read]
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
GO
PRINT N'Altering [dbo].[Tags_ReadAll]...';


GO
--QuestionTags Delete
ALTER PROCEDURE [dbo].[Tags_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	Select TagGUID,TagName 
	FROM Tags
	ORDER BY TagName
END
GO
PRINT N'Altering [dbo].[Users_ReadAllFilltered]...';


GO
ALTER PROCEDURE [dbo].[Users_ReadAllFilltered]
	@SortExpression AS NVARCHAR(MAX) = null,
	@FoundPageNumber INT = NULL ,
	@FoundRowspPage INT = NULL
AS
BEGIN

SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);

	DECLARE @ParmDefinition NVARCHAR(MAX);

	DECLARE @SortDirection NVARCHAR(MAX);

	SET @ParmDefinition = N'@FoundPageNumberParam INT,@FoundRowspPageParam INT';

	


	IF (@SortExpression = 'u.UserType ASC' or @SortExpression = 'u.UserType DESC')
		BEGIN

		IF(@SortExpression = 'u.UserType ASC')
			BEGIN
				SET @SortDirection = 'ASC'
			END
		ELSE
			BEGIN
				SET @SortDirection = 'DESC'
			END

		SET @SqlText = '
				SELECT DISTINCT u.UserGUID, u.UserName, u.UserType, u.IsActive
				FROM (SELECT us.UserGUID, us.Username, us.UserType, us.IsActive, ut.TypeName
					  FROM Users us
					  INNER JOIN UserTypes ut
					  ON us.Usertype= ut.TypeID
					  ORDER BY ut.TypeName ' + @SortDirection + '
					  OFFSET ((@FoundPageNumberParam - 1) * @FoundRowspPageParam) ROWS
					  FETCH NEXT @FoundRowspPageParam ROWS ONLY
				 ) u';
		END
	ELSE
		BEGIN
			SET @SqlText = '
				SELECT DISTINCT u.UserGUID, u.UserName, u.UserType, u.IsActive
				FROM [dbo].Users u';

			IF (@SortExpression IS NOT NULL)
			BEGIN
					SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
			END

			SET @SqlText = @SqlText + ' OFFSET ((@FoundPageNumberParam - 1) * @FoundRowspPageParam) ROWS
								FETCH NEXT @FoundRowspPageParam ROWS ONLY';
		END

	EXECUTE sp_executesql 
					  @SqlText,
					  @ParmDefinition,
					  @FoundPageNumber,
					  @FoundRowspPage;
                    
END
GO
PRINT N'Creating [dbo].[Levels_CanBeDeleted]...';


GO
CREATE PROCEDURE [dbo].[Levels_CanBeDeleted]
	@LevelGUID uniqueidentifier, @ReturnValue bit output
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT q.QuestionGUID FROM Questions q WHERE q.LevelGUID = @LevelGUID) OR
		EXISTS (SELECT qu.QuizGUID FROM Quizes qu WHERE qu.LevelGUID = @LevelGUID)
	BEGIN
		SET @ReturnValue = 0
	END
	ELSE
	BEGIN
		SET @ReturnValue = 1
	END
END
GO
PRINT N'Refreshing [dbo].[ArchivedQuestions_InsertById]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedQuestions_InsertById]';


GO
PRINT N'Refreshing [dbo].[Choices_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Choices_Delete]';


GO
PRINT N'Refreshing [dbo].[Choices_DeleteByQuestionID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Choices_DeleteByQuestionID]';


GO
PRINT N'Refreshing [dbo].[Choices_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Choices_ReadByID]';


GO
PRINT N'Refreshing [dbo].[ArchivedQuestions_ReadAllByQuizID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedQuestions_ReadAllByQuizID]';


GO
PRINT N'Refreshing [dbo].[Categories_CanBeDeleted]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_CanBeDeleted]';


GO
PRINT N'Refreshing [dbo].[FreeTextQuizView_CountAllFilteredQuizzes]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[FreeTextQuizView_CountAllFilteredQuizzes]';


GO
PRINT N'Refreshing [dbo].[Quiz_CountAllPredefined]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Quiz_CountAllPredefined]';


GO
PRINT N'Refreshing [dbo].[Quiz_CountAllRandom]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Quiz_CountAllRandom]';


GO
PRINT N'Refreshing [dbo].[Quiz_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Quiz_Delete]';


GO
PRINT N'Refreshing [dbo].[Quiz_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Quiz_Update]';


GO
PRINT N'Refreshing [dbo].[QuizesPerUserView_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizesPerUserView_ReadAll]';


GO
PRINT N'Refreshing [dbo].[QuizUserPreview_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizUserPreview_ReadAll]';


GO
PRINT N'Refreshing [dbo].[QuizView_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizView_ReadAll]';


GO
PRINT N'Refreshing [dbo].[QuizTemplate_Count]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizTemplate_Count]';


GO
PRINT N'Refreshing [dbo].[QuizTemplateView_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizTemplateView_ReadAll]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Quizes] WITH CHECK CHECK CONSTRAINT [FK_Quizes_ToCategories];

ALTER TABLE [dbo].[Quizes] WITH CHECK CHECK CONSTRAINT [FK_Quizes_ToLevels];


GO
PRINT N'Update complete.';


GO