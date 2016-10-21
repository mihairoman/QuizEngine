CREATE TABLE [dbo].[Questions] (
    [QuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [Text]         NVARCHAR (MAX)   NOT NULL,
    [QuestionType]     SMALLINT NOT NULL,
    [CategoryGUID] UNIQUEIDENTIFIER NOT NULL,
    [LevelGUID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([QuestionGUID] ASC),
    CONSTRAINT [FK_Questions_Categories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]),
    CONSTRAINT [FK_Questions_Levels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID])
    
);

