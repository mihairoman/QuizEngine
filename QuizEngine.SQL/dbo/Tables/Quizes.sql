﻿CREATE TABLE [dbo].[Quizes] (
    [QuizGUID]     UNIQUEIDENTIFIER NOT NULL,
    [LevelGUID]    UNIQUEIDENTIFIER NULL,
    [CategoryGUID] UNIQUEIDENTIFIER NULL,
    [Time] TIME NULL, 
    CONSTRAINT [PK_Quizes] PRIMARY KEY CLUSTERED ([QuizGUID] ASC), 
    CONSTRAINT [FK_Quizes_ToLevels] FOREIGN KEY ([LevelGUID]) REFERENCES [Levels]([LevelGUID]), 
    CONSTRAINT [FK_Quizes_ToCategories] FOREIGN KEY ([CategoryGUID]) REFERENCES [Categories]([CategoryGUID])
);

