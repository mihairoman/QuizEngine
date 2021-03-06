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
PRINT N'Rename refactoring operation with key 6c83a205-d8d2-401c-92d7-b1ff83a22534 is skipped, element [dbo].[Questions].[TypeGUID] (SqlSimpleColumn) will not be renamed to QuestionType';


GO
PRINT N'Creating [dbo].[Answers]...';


GO
CREATE TABLE [dbo].[Answers] (
    [QuizGUID]             UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [Grade]                DECIMAL (5, 2)   NULL,
    [AnswerText]           NVARCHAR (MAX)   NOT NULL,
    [UserGUID]             UNIQUEIDENTIFIER NOT NULL,
    [ArchivedChoiceGUID]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([QuizGUID] ASC, [ArchivedQuestionGUID] ASC, [UserGUID] ASC, [ArchivedChoiceGUID] ASC)
);


GO
PRINT N'Creating [dbo].[ArchivedChoices]...';


GO
CREATE TABLE [dbo].[ArchivedChoices] (
    [ChoiceGUID]           UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [AnswerText]           NVARCHAR (MAX)   NOT NULL,
    [Value]                DECIMAL (5, 2)   NOT NULL,
    [IsCorrect]            BIT              NOT NULL,
    CONSTRAINT [PK_AchivedChoice] PRIMARY KEY CLUSTERED ([ChoiceGUID] ASC)
);


GO
PRINT N'Creating [dbo].[ArchivedQuestions]...';


GO
CREATE TABLE [dbo].[ArchivedQuestions] (
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionText] NVARCHAR (MAX)   NOT NULL,
    [QuestionType]         SMALLINT         NOT NULL,
    [LevelGUID]            UNIQUEIDENTIFIER NOT NULL,
    [CategoryGUID]         UNIQUEIDENTIFIER NOT NULL,
    [QuizGUID]             UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ArchivedQuestions] PRIMARY KEY CLUSTERED ([ArchivedQuestionGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Categories]...';


GO
CREATE TABLE [dbo].[Categories] (
    [CategoryGUID] UNIQUEIDENTIFIER NOT NULL,
    [CategoryName] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Categoryes] PRIMARY KEY CLUSTERED ([CategoryGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Choices]...';


GO
CREATE TABLE [dbo].[Choices] (
    [ChoiceGUID]   UNIQUEIDENTIFIER NOT NULL,
    [QuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [AnswerText]   NVARCHAR (MAX)   NOT NULL,
    [Value]        DECIMAL (5, 2)   NOT NULL,
    [IsCorrect]    BIT              NOT NULL,
    CONSTRAINT [PK_Choices] PRIMARY KEY CLUSTERED ([ChoiceGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Levels]...';


GO
CREATE TABLE [dbo].[Levels] (
    [LevelGUID]  UNIQUEIDENTIFIER NOT NULL,
    [LevelName]  NVARCHAR (MAX)   NOT NULL,
    [Difficulty] SMALLINT         NOT NULL,
    CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED ([LevelGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Questions]...';


GO
CREATE TABLE [dbo].[Questions] (
    [QuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [Text]         NVARCHAR (MAX)   NOT NULL,
    [QuestionType] SMALLINT         NOT NULL,
    [CategoryGUID] UNIQUEIDENTIFIER NOT NULL,
    [LevelGUID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([QuestionGUID] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionTags]...';


GO
CREATE TABLE [dbo].[QuestionTags] (
    [QuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [TagGUID]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_QuestionTags] PRIMARY KEY CLUSTERED ([QuestionGUID] ASC, [TagGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Quizes]...';


GO
CREATE TABLE [dbo].[Quizes] (
    [QuizGUID]     UNIQUEIDENTIFIER NOT NULL,
    [LevelGUID]    UNIQUEIDENTIFIER NOT NULL,
    [CategoryGUID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Quizes] PRIMARY KEY CLUSTERED ([QuizGUID] ASC)
);


GO
PRINT N'Creating [dbo].[QuizUserLinks]...';


GO
CREATE TABLE [dbo].[QuizUserLinks] (
    [QuizGUID]            UNIQUEIDENTIFIER NOT NULL,
    [UserGUID]            UNIQUEIDENTIFIER NOT NULL,
    [Result]              DECIMAL (5, 2)   NULL,
    [QuizDate]            DATETIME         NOT NULL,
    [OnlineOrDownloanded] BIT              NOT NULL,
    [IsTaken]             BIT              NULL,
    CONSTRAINT [PK_TakenQuizes_1] PRIMARY KEY CLUSTERED ([QuizGUID] ASC, [UserGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Tags]...';


GO
CREATE TABLE [dbo].[Tags] (
    [TagGUID] UNIQUEIDENTIFIER NOT NULL,
    [TagName] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED ([TagGUID] ASC)
);


GO
PRINT N'Creating [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [UserGUID] UNIQUEIDENTIFIER NOT NULL,
    [Username] NVARCHAR (MAX)   NOT NULL,
    [Usertype] SMALLINT         NOT NULL,
    [IsActive] BIT              NOT NULL,
    CONSTRAINT [PK__Users__81B7740CE954173C] PRIMARY KEY CLUSTERED ([UserGUID] ASC)
);


GO
PRINT N'Creating DF__Users__Usertype__25869641...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [DF__Users__Usertype__25869641] DEFAULT ((0)) FOR [Usertype];


GO
PRINT N'Creating DF__Users__IsActive__267ABA7A...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [DF__Users__IsActive__267ABA7A] DEFAULT ((1)) FOR [IsActive];


GO
PRINT N'Creating FK_Answers_ArchivedChoice...';


GO
ALTER TABLE [dbo].[Answers] WITH NOCHECK
    ADD CONSTRAINT [FK_Answers_ArchivedChoice] FOREIGN KEY ([ArchivedChoiceGUID]) REFERENCES [dbo].[ArchivedChoices] ([ChoiceGUID]);


GO
PRINT N'Creating FK_Answers_ArchivedQuestions1...';


GO
ALTER TABLE [dbo].[Answers] WITH NOCHECK
    ADD CONSTRAINT [FK_Answers_ArchivedQuestions1] FOREIGN KEY ([ArchivedQuestionGUID]) REFERENCES [dbo].[ArchivedQuestions] ([ArchivedQuestionGUID]);


GO
PRINT N'Creating FK_Answers_TakenQuizes...';


GO
ALTER TABLE [dbo].[Answers] WITH NOCHECK
    ADD CONSTRAINT [FK_Answers_TakenQuizes] FOREIGN KEY ([QuizGUID], [UserGUID]) REFERENCES [dbo].[QuizUserLinks] ([QuizGUID], [UserGUID]);


GO
PRINT N'Creating FK_AchivedChoice_ArchivedQuestions...';


GO
ALTER TABLE [dbo].[ArchivedChoices] WITH NOCHECK
    ADD CONSTRAINT [FK_AchivedChoice_ArchivedQuestions] FOREIGN KEY ([ArchivedQuestionGUID]) REFERENCES [dbo].[ArchivedQuestions] ([ArchivedQuestionGUID]);


GO
PRINT N'Creating FK_ArchivedQuestions_Categories...';


GO
ALTER TABLE [dbo].[ArchivedQuestions] WITH NOCHECK
    ADD CONSTRAINT [FK_ArchivedQuestions_Categories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]);


GO
PRINT N'Creating FK_ArchivedQuestions_Levels...';


GO
ALTER TABLE [dbo].[ArchivedQuestions] WITH NOCHECK
    ADD CONSTRAINT [FK_ArchivedQuestions_Levels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID]);


GO
PRINT N'Creating FK_ArchivedQuestions_Quizes...';


GO
ALTER TABLE [dbo].[ArchivedQuestions] WITH NOCHECK
    ADD CONSTRAINT [FK_ArchivedQuestions_Quizes] FOREIGN KEY ([QuizGUID]) REFERENCES [dbo].[Quizes] ([QuizGUID]);


GO
PRINT N'Creating FK_Choices_Questions...';


GO
ALTER TABLE [dbo].[Choices] WITH NOCHECK
    ADD CONSTRAINT [FK_Choices_Questions] FOREIGN KEY ([QuestionGUID]) REFERENCES [dbo].[Questions] ([QuestionGUID]);


GO
PRINT N'Creating FK_Questions_Categories...';


GO
ALTER TABLE [dbo].[Questions] WITH NOCHECK
    ADD CONSTRAINT [FK_Questions_Categories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]);


GO
PRINT N'Creating FK_Questions_Levels...';


GO
ALTER TABLE [dbo].[Questions] WITH NOCHECK
    ADD CONSTRAINT [FK_Questions_Levels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID]);


GO
PRINT N'Creating FK_QuestionTags_Questions...';


GO
ALTER TABLE [dbo].[QuestionTags] WITH NOCHECK
    ADD CONSTRAINT [FK_QuestionTags_Questions] FOREIGN KEY ([QuestionGUID]) REFERENCES [dbo].[Questions] ([QuestionGUID]);


GO
PRINT N'Creating FK_QuestionTags_Tags...';


GO
ALTER TABLE [dbo].[QuestionTags] WITH NOCHECK
    ADD CONSTRAINT [FK_QuestionTags_Tags] FOREIGN KEY ([TagGUID]) REFERENCES [dbo].[Tags] ([TagGUID]);


GO
PRINT N'Creating FK_Quizes_ToLevels...';


GO
ALTER TABLE [dbo].[Quizes] WITH NOCHECK
    ADD CONSTRAINT [FK_Quizes_ToLevels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID]);


GO
PRINT N'Creating FK_Quizes_ToCategories...';


GO
ALTER TABLE [dbo].[Quizes] WITH NOCHECK
    ADD CONSTRAINT [FK_Quizes_ToCategories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]);


GO
PRINT N'Creating FK_TakenQuizes_Quizes...';


GO
ALTER TABLE [dbo].[QuizUserLinks] WITH NOCHECK
    ADD CONSTRAINT [FK_TakenQuizes_Quizes] FOREIGN KEY ([QuizGUID]) REFERENCES [dbo].[Quizes] ([QuizGUID]);


GO
PRINT N'Creating FK_TakenQuizes_Users...';


GO
ALTER TABLE [dbo].[QuizUserLinks] WITH NOCHECK
    ADD CONSTRAINT [FK_TakenQuizes_Users] FOREIGN KEY ([UserGUID]) REFERENCES [dbo].[Users] ([UserGUID]);


GO
PRINT N'Creating [dbo].[Answers_DeleteAnswerText]...';


GO
CREATE PROCEDURE [dbo].[Answers_DeleteAnswerText]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET AnswerText=' '
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID
END
GO
PRINT N'Creating [dbo].[Answers_DeleteGrade]...';


GO
CREATE PROCEDURE [dbo].[Answers_DeleteGrade]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade = 0.0 
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID
END
GO
PRINT N'Creating [dbo].[Answers_Insert]...';


GO
create PROCEDURE [dbo].[Answers_Insert]
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
PRINT N'Creating [dbo].[Answers_ReadAll]...';


GO
create PROCEDURE [dbo].[Answers_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;	
	select QuizGUID,ArchivedQuestionGUID,Grade,AnswerText,UserGUID,ArchivedChoiceGUID 
	from [Answers]	    
END
GO
PRINT N'Creating [dbo].[Answers_ReadByID]...';


GO

create PROCEDURE [dbo].[Answers_ReadByID]
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
PRINT N'Creating [dbo].[Answers_Update]...';


GO
CREATE PROCEDURE [dbo].[Answers_Update]
@QuizGUID uniqueidentifier,
@ArchivedQuestionGUID uniqueidentifier,
@Grade decimal(5, 2),
@AnswerText nvarchar(MAX),
@UserGUID uniqueidentifier,
@ArchivedChoiceGUID uniqueidentifier
AS
BEGIN
UPDATE [Answers] SET Grade=@Grade,AnswerText=@AnswerText
WHERE  QuizGUID=@QuizGUID and ArchivedQuestionGUID=@ArchivedQuestionGUID and ArchivedChoiceGUID=@ArchivedChoiceGUID and UserGUID=@UserGUID
END
GO
PRINT N'Creating [dbo].[ArchivedChoices_Delete]...';


GO
CREATE PROCEDURE [dbo].[ArchivedChoices_Delete]
		@ChoiceGUID uniqueidentifier,
		@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ArchivedChoices
	WHERE  ChoiceGUID = @ChoiceGUID
		   AND ArchivedQuestionGUID = @ArchivedQuestionGUID

END
GO
PRINT N'Creating [dbo].[ArchivedChoices_Insert]...';


GO
CREATE PROCEDURE [dbo].[ArchivedChoices_Insert]
	-- Add the parameters for the stored procedure here
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(5,2),
	@IsCorrect bit
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO ArchivedChoices
	VALUES (@ChoiceGUID,@ArchivedQuestionGUID,@AnswerText,@Value)
END
GO
PRINT N'Creating [dbo].[ArchivedChoices_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[ArchivedChoices_ReadAll]
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ChoiceGUID, ArchivedQuestionGUID, AnswerText, Value
	FROM ArchivedChoices

END
GO
PRINT N'Creating [dbo].[ArchivedChoices_ReadByID]...';


GO
CREATE PROCEDURE [dbo].[ArchivedChoices_ReadByID]
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
PRINT N'Creating [dbo].[ArchivedChoices_Update]...';


GO
CREATE PROCEDURE [dbo].[ArchivedChoices_Update]
	
	@ChoiceGUID uniqueidentifier,
	@ArchivedQuestionGUID uniqueidentifier,
	@AnswerText nvarchar(max),
	@Value decimal(5,2),
	@IsCorrect bit

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
PRINT N'Creating [dbo].[ArchivedQuestions_Delete]...';


GO

CREATE PROCEDURE [dbo].[ArchivedQuestions_Delete]

	@ArchivedQuestionGUID uniqueidentifier
	
AS
BEGIN
	
	DELETE
	FROM [ArchivedQuestions]
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
END
GO
PRINT N'Creating [dbo].[ArchivedQuestions_Insert]...';


GO
CREATE PROCEDURE [dbo].[ArchivedQuestions_Insert]
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
PRINT N'Creating [dbo].[ArchivedQuestions_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[ArchivedQuestions_ReadAll]

	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT ArchivedQuestionGUID, ArchivedQuestionText,QuestionType,LevelGUID,CategoryGUID, QuizGUID from ArchivedQuestions
	
END
GO
PRINT N'Creating [dbo].[ArchivedQuestions_ReadByID]...';


GO
Create PROCEDURE [dbo].[ArchivedQuestions_ReadByID]

	@ArchivedQuestionGUID uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT ArchivedQuestionGUID, ArchivedQuestionText,QuestionType,LevelGUID,CategoryGUID, QuizGUID from ArchivedQuestions
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
END
GO
PRINT N'Creating [dbo].[ArchivedQuestions_Update]...';


GO

Create PROCEDURE [dbo].[ArchivedQuestions_Update]

	@ArchivedQuestionGUID uniqueidentifier,
	@ArchivedQuestionText nvarchar(max),
	@QuestionType smallint,
	@LevelGUID uniqueidentifier,
	@CategoryGUID uniqueidentifier,
	@QuizGUID uniqueidentifier
	
AS
BEGIN
	
	UPDATE [ArchivedQuestions]
	SET ArchivedQuestionText=@ArchivedQuestionText,
	QuestionType = @QuestionType,
	LevelGUID = @LevelGUID,
	CategoryGUID = @CategoryGUID,
	QuizGUID=@QuizGUID
	WHERE ArchivedQuestionGUID=@ArchivedQuestionGUID
END
GO
PRINT N'Creating [dbo].[Categories_Delete]...';


GO
--Categories Delete
create PROCEDURE [dbo].[Categories_Delete]
	@CategoryGUID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Categories 
	WHERE CategoryGUID = @CategoryGUID
END
GO
PRINT N'Creating [dbo].[Categories_Insert]...';


GO
--Categories Insert
create PROCEDURE [dbo].[Categories_Insert]
	@CategoryGUID uniqueidentifier, 
	@CategoryName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Categories] VALUES (@CategoryGUID, @CategoryName)

END
GO
PRINT N'Creating [dbo].[Categories_ReadAll]...';


GO
--Categories Delete
CREATE PROCEDURE [dbo].[Categories_ReadAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT CategoryGUID,CategoryName 
	FROM dbo.Categories 
END
GO
PRINT N'Creating [dbo].[Categories_ReadByID]...';


GO
--Categories Delete
create PROCEDURE [dbo].[Categories_ReadByID]
	@CategoryGUID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;

	SELECT CategoryGUID,CategoryName 
	FROM dbo.Categories 
	WHERE CategoryGUID = @CategoryGUID
END
GO
PRINT N'Creating [dbo].[Categories_Update]...';


GO
--Categories Update
create PROCEDURE [dbo].[Categories_Update]
	@CategoryGUID uniqueidentifier, 
	@CategoryName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Categories
	SET CategoryName = @CategoryName
	WHERE CategoryGUID = @CategoryGUID
END
GO
PRINT N'Creating [dbo].[Choices_Delete]...';


GO

---------------------------------------  DELETE A CHOISE

CREATE PROCEDURE [dbo].[Choices_Delete]
	@ChoiceGUID UNIQUEIDENTIFIER
AS
BEGIN

	DELETE 
	FROM [Choices]
	WHERE ChoiceGUID = @ChoiceGUID

END;
GO
PRINT N'Creating [dbo].[Choices_Insert]...';


GO

---------------------------------------  INSERT A CHOISE
CREATE PROCEDURE [dbo].[Choices_Insert]
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
PRINT N'Creating [dbo].[Choices_ReadAll]...';


GO

---------------------------------------  SELECT ALL CHOISES
CREATE PROCEDURE [dbo].[Choices_ReadAll]

AS
BEGIN

SELECT ChoiceGUID, QuestionGUID, AnswerText, Value FROM [Choices]

END;
GO
PRINT N'Creating [dbo].[Choices_ReadByID]...';


GO
 
---------------------------------------  SELECT ONE CHOISE

CREATE PROCEDURE [dbo].[Choices_ReadByID]
	@ChoiceGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT ChoiceGUID, QuestionGUID, AnswerText, Value 
	FROM [Choices] 
	WHERE ChoiceGUID = @ChoiceGUID

END;
GO
PRINT N'Creating [dbo].[Choices_Update]...';


GO
---------------------------------------  UPDATE A CHOISE

CREATE PROCEDURE [dbo].[Choices_Update]
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
PRINT N'Creating [dbo].[Levels_Delete]...';


GO
create PROCEDURE [dbo].[Levels_Delete]
	@LevelGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	Delete From dbo.Levels
	WHERE LevelGUID = @LevelGUID
END
GO
PRINT N'Creating [dbo].[Levels_Insert]...';


GO
create PROCEDURE [dbo].[Levels_Insert]
	@LevelGUID uniqueidentifier, 
	@LevelName nvarchar(50),
	@Difficulty smallint
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[Levels] VALUES (@LevelGUID, @LevelName, @Difficulty)
END
GO
PRINT N'Creating [dbo].[Levels_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[Levels_ReadAll]
AS
BEGIN
	SET NOCOUNT ON;

	select LevelGUID,LevelName,Difficulty 
	From dbo.Levels
END
GO
PRINT N'Creating [dbo].[Levels_ReadByID]...';


GO
create PROCEDURE [dbo].[Levels_ReadByID]
	@LevelGUID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	select LevelGUID,LevelName,Difficulty 
	From dbo.Levels
	WHERE LevelGUID = @LevelGUID
END
GO
PRINT N'Creating [dbo].[Levels_Update]...';


GO
--Levels Update
create PROCEDURE [dbo].[Levels_Update]
	@LevelGUID uniqueidentifier, 
	@LevelName nvarchar(50),
	@Difficulty smallint

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Levels
	SET LevelName = @LevelName, Difficulty=@Difficulty
	WHERE LevelGUID = @LevelGUID

END
GO
PRINT N'Creating [dbo].[Questions_Delete]...';


GO

---------------------------------------  DELETE A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Delete]
	@QuestionGUID UNIQUEIDENTIFIER

AS
BEGIN

	DELETE 
	FROM Questions
	WHERE QuestionGUID = @QuestionGUID

END;
GO
PRINT N'Creating [dbo].[Questions_Insert]...';


GO

---------------------------------------  INSERT A QUESTION 

CREATE PROCEDURE [dbo].[Questions_Insert]
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
	@TagName nvarchar(50)
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
	@TagName nvarchar(50)
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
	@UserName NVARCHAR(50)
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
	@UserName NVARCHAR(50)

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
	@UserName NVARCHAR(50),
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
	@UserName NVARCHAR(50),
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
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Answers] WITH CHECK CHECK CONSTRAINT [FK_Answers_ArchivedChoice];

ALTER TABLE [dbo].[Answers] WITH CHECK CHECK CONSTRAINT [FK_Answers_ArchivedQuestions1];

ALTER TABLE [dbo].[Answers] WITH CHECK CHECK CONSTRAINT [FK_Answers_TakenQuizes];

ALTER TABLE [dbo].[ArchivedChoices] WITH CHECK CHECK CONSTRAINT [FK_AchivedChoice_ArchivedQuestions];

ALTER TABLE [dbo].[ArchivedQuestions] WITH CHECK CHECK CONSTRAINT [FK_ArchivedQuestions_Categories];

ALTER TABLE [dbo].[ArchivedQuestions] WITH CHECK CHECK CONSTRAINT [FK_ArchivedQuestions_Levels];

ALTER TABLE [dbo].[ArchivedQuestions] WITH CHECK CHECK CONSTRAINT [FK_ArchivedQuestions_Quizes];

ALTER TABLE [dbo].[Choices] WITH CHECK CHECK CONSTRAINT [FK_Choices_Questions];

ALTER TABLE [dbo].[Questions] WITH CHECK CHECK CONSTRAINT [FK_Questions_Categories];

ALTER TABLE [dbo].[Questions] WITH CHECK CHECK CONSTRAINT [FK_Questions_Levels];

ALTER TABLE [dbo].[QuestionTags] WITH CHECK CHECK CONSTRAINT [FK_QuestionTags_Questions];

ALTER TABLE [dbo].[QuestionTags] WITH CHECK CHECK CONSTRAINT [FK_QuestionTags_Tags];

ALTER TABLE [dbo].[Quizes] WITH CHECK CHECK CONSTRAINT [FK_Quizes_ToLevels];

ALTER TABLE [dbo].[Quizes] WITH CHECK CHECK CONSTRAINT [FK_Quizes_ToCategories];

ALTER TABLE [dbo].[QuizUserLinks] WITH CHECK CHECK CONSTRAINT [FK_TakenQuizes_Quizes];

ALTER TABLE [dbo].[QuizUserLinks] WITH CHECK CHECK CONSTRAINT [FK_TakenQuizes_Users];


GO
PRINT N'Update complete.';


GO
