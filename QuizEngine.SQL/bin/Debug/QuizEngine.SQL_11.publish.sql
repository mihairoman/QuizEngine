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
PRINT N'Altering [dbo].[ArchivedChoices]...';


GO
ALTER TABLE [dbo].[ArchivedChoices] ALTER COLUMN [Value] DECIMAL (5, 2) NULL;


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_Delete]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_Insert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_Insert]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_ReadAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_ReadAll]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_ReadByID]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_ReadByID]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_ReadByQuestionIDS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_ReadByQuestionIDS]';


GO
PRINT N'Refreshing [dbo].[ArchivedChoices_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedChoices_Update]';


GO
PRINT N'Refreshing [dbo].[ArchivedQuestions_InsertById]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ArchivedQuestions_InsertById]';


GO
PRINT N'Refreshing [dbo].[QuizResultView_ReadResult]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QuizResultView_ReadResult]';


GO
PRINT N'Update complete.';


GO