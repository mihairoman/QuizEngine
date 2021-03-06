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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column IsCorrect on table [dbo].[ArchivedChoices] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column Value on table [dbo].[ArchivedChoices] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[ArchivedChoices])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column IsCorrect on table [dbo].[Choices] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column Value on table [dbo].[Choices] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Choices])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[QuizUserLinks].[OnlineOrDownloaded] is being dropped, data loss could occur.

The column [dbo].[QuizUserLinks].[OnlineOrDownloanded] on table [dbo].[QuizUserLinks] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[QuizUserLinks])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key 6c83a205-d8d2-401c-92d7-b1ff83a22534 is skipped, element [dbo].[Questions].[TypeGUID] (SqlSimpleColumn) will not be renamed to QuestionType';


GO
PRINT N'Dropping FK_Quizes_Categories...';


GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [FK_Quizes_Categories];


GO
PRINT N'Dropping FK_Quizes_Levels...';


GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [FK_Quizes_Levels];


GO
PRINT N'Altering [dbo].[Answers]...';


GO
ALTER TABLE [dbo].[Answers] ALTER COLUMN [Grade] DECIMAL (5, 2) NULL;


GO
PRINT N'Altering [dbo].[ArchivedChoices]...';


GO
ALTER TABLE [dbo].[ArchivedChoices] ALTER COLUMN [IsCorrect] BIT NOT NULL;

ALTER TABLE [dbo].[ArchivedChoices] ALTER COLUMN [Value] DECIMAL (5, 2) NOT NULL;


GO
PRINT N'Altering [dbo].[Categories]...';


GO
ALTER TABLE [dbo].[Categories] ALTER COLUMN [CategoryName] NVARCHAR (MAX) NOT NULL;


GO
PRINT N'Altering [dbo].[Choices]...';


GO
ALTER TABLE [dbo].[Choices] ALTER COLUMN [IsCorrect] BIT NOT NULL;

ALTER TABLE [dbo].[Choices] ALTER COLUMN [Value] DECIMAL (5, 2) NOT NULL;


GO
PRINT N'Altering [dbo].[Levels]...';


GO
ALTER TABLE [dbo].[Levels] ALTER COLUMN [LevelName] NVARCHAR (MAX) NOT NULL;


GO
PRINT N'Altering [dbo].[QuizUserLinks]...';


GO
ALTER TABLE [dbo].[QuizUserLinks] DROP COLUMN [OnlineOrDownloaded];


GO
ALTER TABLE [dbo].[QuizUserLinks]
    ADD [OnlineOrDownloanded] BIT NOT NULL,
        [IsTaken]             BIT NULL;


GO
PRINT N'Altering [dbo].[Tags]...';


GO
ALTER TABLE [dbo].[Tags] ALTER COLUMN [TagName] NVARCHAR (MAX) NOT NULL;


GO
PRINT N'Altering [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users] ALTER COLUMN [Username] NVARCHAR (MAX) NOT NULL;


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
PRINT N'Altering [dbo].[Answers_DeleteAnswerText]...';


GO
ALTER PROCEDURE [dbo].[Answers_DeleteAnswerText]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET AnswerText=' '
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID
END
GO
PRINT N'Altering [dbo].[Answers_DeleteGrade]...';


GO
ALTER PROCEDURE [dbo].[Answers_DeleteGrade]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade = 0.0 
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID
END
GO
PRINT N'Altering [dbo].[Answers_Insert]...';


GO
ALTER PROCEDURE [dbo].[Answers_Insert]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(3, 1),
@AnswerText nvarchar(MAX),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
INSERT INTO [Answers] VALUES(@QuizGUID,@ArchivedQuestionGUID,@Grade,@AnswerText,@UserGUID,@ArchivedChoiceGUID)	
END
GO
PRINT N'Altering [dbo].[Answers_ReadByID]...';


GO

ALTER PROCEDURE [dbo].[Answers_ReadByID]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	
	select QuizGUID, ArchivedQuestionGUID, Grade, AnswerText, UserGUID, ArchivedChoiceGUID 
	from [Answers] 
	where QuizGUID = @QuizGUID and ArchivedQuestionGUID = @ArchivedQuestionGUID and ArchivedChoiceGUID = @ArchivedChoiceGUID
	    
END
GO
PRINT N'Altering [dbo].[Answers_Update]...';


GO
ALTER PROCEDURE [dbo].[Answers_Update]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(3, 1),
@AnswerText nvarchar(MAX),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade=@Grade,AnswerText=@AnswerText
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID and UserGUID=@UserGUID
END
GO
PRINT N'Altering [dbo].[ArchivedChoices_Insert]...';


GO
ALTER PROCEDURE [dbo].[ArchivedChoices_Insert]
	-- Add the parameters for the stored procedure here
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(4,2)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO ArchivedChoices
	VALUES (@ChoiceGUID,@ArchivedQuestionGUID,@AnswerText,@Value)
END
GO
PRINT N'Altering [dbo].[ArchivedChoices_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[ArchivedChoices_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ChoiceGUID, ArchivedQuestionGUID, AnswerText, Value
	FROM ArchivedChoices

END
GO
PRINT N'Altering [dbo].[ArchivedChoices_ReadByID]...';


GO
ALTER PROCEDURE [dbo].[ArchivedChoices_ReadByID]
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ChoiceGUID, ArchivedQuestionGUID, AnswerText, Value
	FROM ArchivedChoices
	WHERE ChoiceGUID = @ChoiceGUID
		  AND ArchivedQuestionGUID = @ArchivedQuestionGUID
END
GO
PRINT N'Altering [dbo].[ArchivedChoices_Update]...';


GO
ALTER PROCEDURE [dbo].[ArchivedChoices_Update]
	
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(4,2)

AS
BEGIN
	SET NOCOUNT ON;

   UPDATE [ArchivedChoices] 
   SET 
		AnswerText = @AnswerText,
		Value = @Value
	WHERE 
		ChoiceGUID = @ChoiceGUID 
		AND ArchivedQuestionGUID = @ArchivedQuestionGUID
	
END
GO
PRINT N'Altering [dbo].[QuizView_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[QuizView_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT dbo.Users.Username , dbo.Quizes.QuizGUID , dbo.QuizUserLinks.Result , dbo.Categories.CategoryName , dbo.Levels.LevelName, 
	dbo.QuizUserLinks.UserGUID , QuizDate , OnlineOrDownloanded , IsTaken
	FROM Quizes JOIN dbo.QuizUserLinks ON Quizes.QuizGUID = dbo.QuizUserLinks.QuizGUID
		 JOIN dbo.Users ON Users.UserGUID = dbo.QuizUserLinks.UserGUID , dbo.Levels , dbo.Categories
	WHERE Quizes.LevelGUID = Levels.LevelGUID AND Quizes.CategoryGUID = Categories.CategoryGUID
    ORDER BY Users.UserGUID
END
GO
PRINT N'Altering [dbo].[Choices_Insert]...';


GO

---------------------------------------  INSERT A CHOISE
ALTER PROCEDURE [dbo].[Choices_Insert]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(1,1)

AS
BEGIN

	INSERT
	INTO [Choices]
	VALUES(@ChoiceGUID,@QuestionGUID,@AnswerText,@Value)

END;
GO
PRINT N'Altering [dbo].[Choices_ReadAll]...';


GO

---------------------------------------  SELECT ALL CHOISES
ALTER PROCEDURE [dbo].[Choices_ReadAll]

AS
BEGIN

SELECT ChoiceGUID, QuestionGUID, AnswerText, Value FROM [Choices]

END;
GO
PRINT N'Altering [dbo].[Choices_ReadByID]...';


GO
 
---------------------------------------  SELECT ONE CHOISE

ALTER PROCEDURE [dbo].[Choices_ReadByID]
	@ChoiceGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT ChoiceGUID, QuestionGUID, AnswerText, Value 
	FROM [Choices] 
	WHERE ChoiceGUID = @ChoiceGUID

END;
GO
PRINT N'Altering [dbo].[Choices_Update]...';


GO
---------------------------------------  UPDATE A CHOISE

ALTER PROCEDURE [dbo].[Choices_Update]
	@ChoiceGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER,
	@AnswerText NVARCHAR(MAX),
	@Value DECIMAL(1,1)

AS
BEGIN

	UPDATE [Choices] 
	SET QuestionGUID = @QuestionGUID, AnswerText = @AnswerText, Value=@Value
	WHERE ChoiceGUID = @ChoiceGUID

END;
GO
PRINT N'Altering [dbo].[QuizUserLink_Insert]...';


GO
ALTER PROCEDURE [dbo].[QuizUserLink_Insert]
	@QuizGUID uniqueidentifier,
	@UserGuid uniqueidentifier,
	@Result decimal(4,2),
	@QuizDate   Datetime,
	@OnlineOrDowloanded bit,
	@IsTaken bit
AS
BEGIN
	SET NOCOUNT ON;
		INSERT INTO dbo.QuizUserLinks VALUES (@QuizGUID, @UserGuid , @Result, @QuizDate, @OnlineOrDowloanded, @IsTaken)
END
GO
PRINT N'Altering [dbo].[QuizUserLinks_ReadAll]...';


GO
ALTER PROCEDURE [dbo].[QuizUserLinks_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result, QuizDate , OnlineOrDownloanded , IsTaken   
		FROM dbo.QuizUserLinks
END
GO
PRINT N'Altering [dbo].[QuizUserLinks_ReadByQuizId]...';


GO
ALTER PROCEDURE [dbo].[QuizUserLinks_ReadByQuizId]
	@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloanded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE QuizGUID = @QuizGUID
END
GO
PRINT N'Altering [dbo].[QuizUserLinks_ReadByUserId]...';


GO
ALTER PROCEDURE [dbo].[QuizUserLinks_ReadByUserId]
	@UserGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloanded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE UserGUID = @UserGUID
END
GO
PRINT N'Altering [dbo].[QuizUserLinks_Update]...';


GO
ALTER PROCEDURE [dbo].[QuizUserLinks_Update]
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier,
	@Result decimal(4,2),
	@QuizDate   Datetime,
	@OnlineOrDownloanded bit,
	@IsTaken bit
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.QuizUserLinks
	SET UserGUID = @UserGUID,
		Result = @Result,
		QuizDate = @QuizDate,
		OnlineOrDownloanded = @OnlineOrDownloanded,
		IsTaken =@IsTaken
	WHERE QuizGUID = @QuizGUID
END
GO
PRINT N'Altering [dbo].[Users_Deactivate]...';


GO
ALTER PROCEDURE [dbo].[Users_Deactivate]
	@UserName NVARCHAR(50)
AS
BEGIN

	UPDATE [Users]
	SET IsActive = 0
	WHERE (Username = @UserName) and (Usertype = 0)

END;
GO
PRINT N'Altering [dbo].[ArchivedQuestions_Insert]...';


GO
ALTER PROCEDURE [dbo].[ArchivedQuestions_Insert]
	@ArchivedQuestionGUID uniqueidentifier,
	@ArchivedQuestionText nvarchar(max),
	@QuestionTypeGUID uniqueidentifier,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@QuizGUID uniqueidentifier
	
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO ArchivedQuestions VALUES (@ArchivedQuestionGUID,@ArchivedQuestionText,@QuestionTypeGUID,@LevelGUID,@CategoryGUID,@QuizGUID )
END
GO
PRINT N'Altering [dbo].[Questions_Insert]...';


GO

---------------------------------------  INSERT A QUESTION 

ALTER PROCEDURE [dbo].[Questions_Insert]
	@QuestionGUID UNIQUEIDENTIFIER,
	@Text NVARCHAR(MAX),
	@TypeGUID UNIQUEIDENTIFIER,
	@CategoryGUID UNIQUEIDENTIFIER,
	@LevelGUID UNIQUEIDENTIFIER

AS
BEGIN

	INSERT INTO [Questions]
	Values (@QuestionGUID, @Text, @TypeGUID, @CategoryGUID,@LevelGUID)

END;
GO
PRINT N'Creating [dbo].[QuestionTags_ReadQuestionsByTagName]...';


GO
CREATE PROCEDURE [dbo].[QuestionTags_ReadQuestionsByTagName]
	 @tagName nvarchar(50)

AS
BEGIN

	SELECT q.Text, t.TagName
FROM Questions q
	INNER JOIN QuestionTags qt ON q.QuestionGUID = qt.QuestionGUID
	INNER JOIN Tags t ON t.TagGUID = qt.TagGUID
WHERE t.TagName = @tagName

END;
GO
PRINT N'Creating [dbo].[Users_Delete]...';


GO

---------------------------------------  DELETE A USER

CREATE PROCEDURE [dbo].[Users_Delete]
	@UserName NVARCHAR(50)

AS
BEGIN

	DELETE 
	FROM [Users]
	WHERE Username = @UserName

END;
GO
PRINT N'Refreshing [dbo].[Answers_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Answers_ReadAll]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_Delete]';


GO
PRINT N'Refreshing [dbo].[Categories_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_Delete]';


GO
PRINT N'Refreshing [dbo].[Categories_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_Insert]';


GO
PRINT N'Refreshing [dbo].[Categories_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_ReadAll]';


GO
PRINT N'Refreshing [dbo].[Categories_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_ReadByID]';


GO
PRINT N'Refreshing [dbo].[Categories_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Categories_Update]';


GO
PRINT N'Refreshing [dbo].[Questions_ReadAllView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Questions_ReadAllView]';


GO
PRINT N'Refreshing [dbo].[Choices_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Choices_Delete]';


GO
PRINT N'Refreshing [dbo].[Levels_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Levels_Delete]';


GO
PRINT N'Refreshing [dbo].[Levels_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Levels_Insert]';


GO
PRINT N'Refreshing [dbo].[Levels_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Levels_ReadAll]';


GO
PRINT N'Refreshing [dbo].[Levels_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Levels_ReadByID]';


GO
PRINT N'Refreshing [dbo].[Levels_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Levels_Update]';


GO
PRINT N'Refreshing [dbo].[Tags_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Tags_Delete]';


GO
PRINT N'Refreshing [dbo].[Tags_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Tags_Insert]';


GO
PRINT N'Refreshing [dbo].[Tags_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Tags_ReadAll]';


GO
PRINT N'Refreshing [dbo].[Tags_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Tags_ReadByID]';


GO
PRINT N'Refreshing [dbo].[Tags_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Tags_Update]';


GO
PRINT N'Refreshing [dbo].[Users_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Users_Insert]';


GO
PRINT N'Refreshing [dbo].[Users_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Users_ReadAll]';


GO
PRINT N'Refreshing [dbo].[Users_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Users_ReadByID]';


GO
PRINT N'Refreshing [dbo].[Users_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Users_Update]';


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '6c83a205-d8d2-401c-92d7-b1ff83a22534')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('6c83a205-d8d2-401c-92d7-b1ff83a22534')

GO

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
