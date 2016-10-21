CREATE TABLE [dbo].[ArchivedQuestions] (
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionText] NVARCHAR (MAX)   NOT NULL,
    [QuestionType]         SMALLINT         NOT NULL,
    [LevelGUID]            UNIQUEIDENTIFIER NOT NULL,
    [CategoryGUID]         UNIQUEIDENTIFIER NOT NULL,
    [QuizGUID]             UNIQUEIDENTIFIER NOT NULL,
    [IndexOrder]           SMALLINT         NULL,
    CONSTRAINT [PK_ArchivedQuestions] PRIMARY KEY CLUSTERED ([ArchivedQuestionGUID] ASC),
    CONSTRAINT [FK_ArchivedQuestions_Categories] FOREIGN KEY ([CategoryGUID]) REFERENCES [dbo].[Categories] ([CategoryGUID]),
    CONSTRAINT [FK_ArchivedQuestions_Levels] FOREIGN KEY ([LevelGUID]) REFERENCES [dbo].[Levels] ([LevelGUID]),
    CONSTRAINT [FK_ArchivedQuestions_Quizes] FOREIGN KEY ([QuizGUID]) REFERENCES [dbo].[Quizes] ([QuizGUID])
);



