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
PRINT N'Rename refactoring operation with key 6c83a205-d8d2-401c-92d7-b1ff83a22534 is skipped, element [dbo].[Questions].[TypeGUID] (SqlSimpleColumn) will not be renamed to QuestionType';


GO
PRINT N'Creating [dbo].[Questions_Insert]...';


GO

---------------------------------------  INSERT A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Insert]
	@QuestionGUID UNIQUEIDENTIFIER,
	@Text NVARCHAR(MAX),
	@TypeGUID smallint,
	@CategoryGUID UNIQUEIDENTIFIER,
	@LevelGUID UNIQUEIDENTIFIER

AS
BEGIN

	INSERT INTO [Questions]
	Values (@QuestionGUID, @Text, @TypeGUID, @CategoryGUID,@LevelGUID)

END;
GO
PRINT N'Creating [dbo].[Questions_ReadAll]...';


GO
---------------------------------------  SELECT ALL QUESTIONS 

CREATE PROCEDURE [dbo].[Questions_ReadAll]
	

AS
BEGIN

	SELECT QuestionGUID, Text, QuestionType, CategoryGUID, LevelGUID 
	FROM [Questions]

END;
GO
PRINT N'Creating [dbo].[Questions_ReadAllView]...';


GO
CREATE PROCEDURE [dbo].[Questions_ReadAllView]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT q.QuestionGUID, q.Text, q.QuestionType, c.CategoryName,l.LevelName
	FROM Questions q
	INNER JOIN Categories c ON q.CategoryGUID = c.CategoryGUID
	INNER JOIN Levels l ON q.LevelGUID = l.LevelGUID

END
GO
PRINT N'Creating [dbo].[Questions_ReadByID]...';


GO

---------------------------------------  SELECT ONE QUESTION 

CREATE PROCEDURE [dbo].[Questions_ReadByID]
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT QuestionGUID, Text, QuestionType, CategoryGUID, LevelGUID
	FROM [Questions]
	WHERE QuestionGUID = @QuestionGUID

END;
GO
PRINT N'Creating [dbo].[Questions_Update]...';


GO

---------------------------------------  UPDATE A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Update]
	@Text NVARCHAR(MAX),
	@QuestionType SMALLINT,
	@CategoryGUID UNIQUEIDENTIFIER,
	@LevelGUID UNIQUEIDENTIFIER,
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	UPDATE Questions 
	SET Text = @Text, QuestionType = @QuestionType, CategoryGUID = @CategoryGUID, LevelGUID = @LevelGUID
	WHERE QuestionGUID = @QuestionGUID

END;
GO
PRINT N'Creating [dbo].[QuestionTags_Delete]...';


GO
--QuestionTags Delete
create PROCEDURE [dbo].[QuestionTags_Delete]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM QuestionTags
	WHERE QuestionGUID=@QuestionGUID AND TagGUID=@TagGUID
END
GO
PRINT N'Creating [dbo].[QuestionTags_Insert]...';


GO
--QuestionTags Insert
create PROCEDURE [dbo].[QuestionTags_Insert]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO QuestionTags VALUES (@QuestionGUID,@TagGUID)
END
GO
PRINT N'Creating [dbo].[QuestionTags_ReadAll]...';


GO
--QuestionTags Delete
CREATE PROCEDURE [dbo].[QuestionTags_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuestionGUID,TagGUID 
	FROM QuestionTags
END
GO
PRINT N'Creating [dbo].[QuestionTags_ReadByID]...';


GO
--QuestionTags Delete
create PROCEDURE [dbo].[QuestionTags_ReadByID]
	@QuestionGUID uniqueidentifier,
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuestionGUID,TagGUID 
	FROM QuestionTags
	WHERE QuestionGUID=@QuestionGUID AND TagGUID=@TagGUID
END
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
PRINT N'Creating [dbo].[Quiz_Delete]...';


GO
CREATE PROCEDURE dbo.Quiz_Delete
	@QuizGUID uniqueidentifier
AS	
BEGIN
	
	SET NOCOUNT ON;
	DELETE FROM Quizes
	WHERE QuizGUID = @QuizGUID
END
GO
PRINT N'Creating [dbo].[Quiz_Insert]...';


GO
CREATE PROCEDURE [dbo].[Quiz_Insert]
	@QuizGUID uniqueidentifier,
	@LevelGUID	uniqueidentifier,
	@CategoryGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO Quizes VALUES(@QuizGUID,@LevelGUID,@CategoryGUID)
END
GO
PRINT N'Creating [dbo].[Quiz_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[Quiz_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT QuizGUID , LevelGUID , CategoryGUID FROM Quizes
END
GO
PRINT N'Creating [dbo].[Quiz_ReadById]...';


GO
CREATE PROCEDURE [dbo].[Quiz_ReadById]
		@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT QuizGUID, LevelGUID, CategoryGUID FROM Quizes
	WHERE QuizGUID = @QuizGUID
   
END
GO
PRINT N'Creating [dbo].[Quiz_Update]...';


GO
CREATE PROCEDURE [dbo].[Quiz_Update]
	@QuizGUID uniqueidentifier,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Quizes] 
		SET LevelGUID = @LevelGUID,
		    CategoryGUID = @CategoryGUID
		WHERE
			QuizGUID  = @QuizGUID
   
END
GO
PRINT N'Creating [dbo].[QuizUserLink_Insert]...';


GO
CREATE PROCEDURE [dbo].[QuizUserLink_Insert]
	@QuizGUID uniqueidentifier,
	@UserGuid uniqueidentifier,
	@Result decimal(5,2),
	@QuizDate   Datetime,
	@OnlineOrDowloanded bit,
	@IsTaken bit
AS
BEGIN
	SET NOCOUNT ON;
		INSERT INTO dbo.QuizUserLinks VALUES (@QuizGUID, @UserGuid , @Result, @QuizDate, @OnlineOrDowloanded, @IsTaken)
END
GO
PRINT N'Creating [dbo].[QuizUserLinks_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[QuizUserLinks_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result, QuizDate , OnlineOrDownloanded , IsTaken   
		FROM dbo.QuizUserLinks
END
GO
PRINT N'Creating [dbo].[QuizUserLinks_ReadByQuizId]...';


GO
CREATE PROCEDURE [dbo].[QuizUserLinks_ReadByQuizId]
	@QuizGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloanded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE QuizGUID = @QuizGUID
END
GO
PRINT N'Creating [dbo].[QuizUserLinks_ReadByUserId]...';


GO
CREATE PROCEDURE [dbo].[QuizUserLinks_ReadByUserId]
	@UserGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
		SELECT QuizGUID, UserGUID, Result , QuizDate , OnlineOrDownloanded , IsTaken
		FROM dbo.QuizUserLinks
		WHERE UserGUID = @UserGUID
END
GO
PRINT N'Creating [dbo].[QuizUserLinks_Update]...';


GO
CREATE PROCEDURE [dbo].[QuizUserLinks_Update]
	@QuizGUID uniqueidentifier,
	@UserGUID uniqueidentifier,
	@Result decimal(5,2),
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
PRINT N'Creating [dbo].[QuizView_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[QuizView_ReadAll]
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
PRINT N'Creating [dbo].[Tags_Delete]...';


GO
--QuestionTags Delete
create PROCEDURE [dbo].[Tags_Delete]
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Tags
	WHERE TagGUID = @TagGUID
END
GO
PRINT N'Creating [dbo].[Tags_Insert]...';


GO
--QuestionTags Insert
create PROCEDURE [dbo].[Tags_Insert]
	@TagGUID uniqueidentifier, 
	@TagName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Tags] VALUES (@TagGUID, @TagName)
END
GO
PRINT N'Creating [dbo].[Tags_ReadAll]...';


GO
--QuestionTags Delete
CREATE PROCEDURE [dbo].[Tags_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	Select TagGUID,TagName 
	FROM Tags
END
GO
PRINT N'Creating [dbo].[Tags_ReadByID]...';


GO
--QuestionTags Delete
create PROCEDURE [dbo].[Tags_ReadByID]
	@TagGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	Select TagGUID,TagName FROM Tags
	WHERE TagGUID = @TagGUID
END
GO
PRINT N'Creating [dbo].[Tags_Update]...';


GO
--QuestionTags Update
create PROCEDURE [dbo].[Tags_Update]
	@TagGUID uniqueidentifier,
	@TagName nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Tags
	SET TagName = @TagName
	WHERE TagGUID = @TagGUID
END
GO
PRINT N'Creating [dbo].[Users_Deactivate]...';


GO
CREATE PROCEDURE [dbo].[Users_Deactivate]
	@UserName NVARCHAR(MAX)
AS
BEGIN

	UPDATE [Users]
	SET IsActive = 0
	WHERE (Username = @UserName) and (Usertype = 0)

END;
GO
PRINT N'Creating [dbo].[Users_Delete]...';


GO

---------------------------------------  DELETE A USER

CREATE PROCEDURE [dbo].[Users_Delete]
	@UserName NVARCHAR(MAX)

AS
BEGIN

	DELETE 
	FROM [Users]
	WHERE Username = @UserName

END;
GO
PRINT N'Creating [dbo].[Users_Insert]...';


GO

---------------------------------------  INSERT A USER

CREATE PROCEDURE [dbo].[Users_Insert]
	@UserGUID UNIQUEIDENTIFIER,
	@UserName NVARCHAR(MAX),
	@UserType SMALLINT,
	@IsActive BIT

AS
BEGIN

	INSERT INTO [Users] (UserGUID, Username, Usertype, IsActive)
	Values (@UserGUID, @UserName, @UserType, @IsActive)

END;
GO
PRINT N'Creating [dbo].[Users_ReadAll]...';


GO

---------------------------------------  SELECT ALL USERS

CREATE PROCEDURE [dbo].[Users_ReadAll]

AS
BEGIN

	SELECT UserGUID, Username, Usertype, IsActive 
	FROM [Users];

END;
GO
PRINT N'Creating [dbo].[Users_ReadByID]...';


GO

---------------------------------------  SELECT ONE USER

CREATE PROCEDURE [dbo].[Users_ReadByID]
@UserGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT UserGUID, Username, Usertype, IsActive 
	FROM [Users]
	WHERE UserGUID = @UserGUID

END;
GO
PRINT N'Creating [dbo].[Users_Update]...';


GO

---------------------------------------  UPDATE A USER

CREATE PROCEDURE [dbo].[Users_Update]
	@UserGUID UNIQUEIDENTIFIER,
	@UserName NVARCHAR(MAX),
	@UserType SMALLINT,
	@IsActive BIT

AS
BEGIN

	UPDATE [Users] 
	SET  Username = @UserName, Usertype = @UserType, IsActive = @IsActive 
	WHERE UserGUID = @UserGUID

END;
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
PRINT N'Update complete.';


GO
