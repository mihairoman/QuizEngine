CREATE TABLE [dbo].[Levels] (
    [LevelGUID]  UNIQUEIDENTIFIER NOT NULL,
    [LevelName]  NVARCHAR (MAX)    NOT NULL,
    [Difficulty] SMALLINT         NOT NULL,
    CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED ([LevelGUID] ASC)
);

