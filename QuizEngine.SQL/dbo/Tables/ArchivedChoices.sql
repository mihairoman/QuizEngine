CREATE TABLE [dbo].[ArchivedChoices] (
    [ChoiceGUID]           UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [AnswerText]           NVARCHAR (MAX)   NULL,
    [Value]                DECIMAL (5, 2)   NULL,
    [IsCorrect] BIT NULL, 
    [ChoicePosition] INT NULL, 
    CONSTRAINT [PK_AchivedChoice] PRIMARY KEY CLUSTERED ([ChoiceGUID] ASC),
    CONSTRAINT [FK_AchivedChoice_ArchivedQuestions] FOREIGN KEY ([ArchivedQuestionGUID]) REFERENCES [dbo].[ArchivedQuestions] ([ArchivedQuestionGUID])
);

