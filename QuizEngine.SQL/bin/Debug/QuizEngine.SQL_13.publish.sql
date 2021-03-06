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
PRINT N'Rename refactoring operation with key 444fb63f-dd6d-4015-a505-d2aa1cfc3ea8 is skipped, element [dbo].[LinkPermission].[Id] (SqlSimpleColumn) will not be renamed to Link';


GO
PRINT N'Creating [dbo].[LinkPermission]...';


GO
CREATE TABLE [dbo].[LinkPermission] (
    [Link]           NVARCHAR (50)    NOT NULL,
    [PermissionGUID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Link] ASC)
);


GO
PRINT N'Altering [dbo].[FreeTextQuizView_ReadAll]...';


GO

ALTER PROCEDURE [dbo].[FreeTextQuizView_ReadAll]

	@SortExpression nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);
	
	Set @SqlText = ' SELECT Users.UserGUID, Quizes.QuizGUID, Username, QuizDate, CategoryName, LevelName
	FROM Users, QuizUserLinks, Levels, Categories, Quizes
	WHERE QuizUserLinks.UserGUID = Users.UserGUID
	AND Quizes.QuizGUID=QuizUserLinks.QuizGUID
	AND Quizes.LevelGUID=Levels.LevelGUID
	AND Quizes.CategoryGUID=Categories.CategoryGUID
	AND Result IS NULL
	AND OnlineOrDownloaded = 1';
	

	IF (@SortExpression IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
	END

	EXECUTE sp_executesql @SqlText
END
GO
PRINT N'Altering [dbo].[Question_ReadAllFiltered]...';


GO
ALTER PROCEDURE [dbo].[Question_ReadAllFiltered]
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
GO
PRINT N'Creating [dbo].[FreeTextQuestionsView_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[FreeTextQuestionsView_ReadAll] 
@QuizGUID uniqueidentifier , 
@UserGUID uniqueidentifier 
AS
SELECT aq.QuizGUID, ans.UserGUID, aq.ArchivedQuestionGUID, ans.ArchivedChoiceGUID, aq.QuestionType, aq.ArchivedQuestionText,ans.AnswerText,ans.Grade
From ArchivedQuestions aq, Answers ans
Where aq.QuizGUID=@QuizGUID
and ans.ArchivedQuestionGUID=aq.ArchivedQuestionGUID 
and ans.UserGUID=@UserGUID
GO
PRINT N'Creating [dbo].[LinkPermission_ReadLinkPermissionsByUserGuid]...';


GO
CREATE PROCEDURE [dbo].[LinkPermission_ReadLinkPermissionsByUserGuid]
	@userGUID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT Link, lp.PermissionGUID
	FROM [dbo].[LinkPermission] lp
	INNER JOIN [dbo].[Permissions] p
	ON p.PermissionGUID=lp.PermissionGUID
	WHERE p.UserGUID=@userGUID;
END
GO
PRINT N'Creating [dbo].[Question_CountAllQuestions]...';


GO


CREATE PROCEDURE [dbo].[Question_CountAllQuestions]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(q.QuestionGUID) as QuestionCount from [dbo].Questions q
END
GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '444fb63f-dd6d-4015-a505-d2aa1cfc3ea8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('444fb63f-dd6d-4015-a505-d2aa1cfc3ea8')

GO

GO
PRINT N'Update complete.';


GO
