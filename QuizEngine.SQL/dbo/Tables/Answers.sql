CREATE TABLE [dbo].[Answers] (
    [QuizGUID]             UNIQUEIDENTIFIER NOT NULL,
    [ArchivedQuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [Grade]                DECIMAL (5, 2)   NULL,
    [AnswerText]           NVARCHAR (MAX)   NOT NULL,
    [UserGUID]             UNIQUEIDENTIFIER NOT NULL,
    [ArchivedChoiceGUID]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([QuizGUID] ASC, [ArchivedQuestionGUID] ASC, [UserGUID] ASC, [ArchivedChoiceGUID] ASC),
    CONSTRAINT [FK_Answers_ArchivedChoice] FOREIGN KEY ([ArchivedChoiceGUID]) REFERENCES [dbo].[ArchivedChoices] ([ChoiceGUID]),
    CONSTRAINT [FK_Answers_ArchivedQuestions1] FOREIGN KEY ([ArchivedQuestionGUID]) REFERENCES [dbo].[ArchivedQuestions] ([ArchivedQuestionGUID]),
    CONSTRAINT [FK_Answers_TakenQuizes] FOREIGN KEY ([QuizGUID], [UserGUID]) REFERENCES [dbo].[QuizUserLinks] ([QuizGUID], [UserGUID])
);

