CREATE TABLE [dbo].[Choices] (
    [ChoiceGUID]   UNIQUEIDENTIFIER NOT NULL,
    [QuestionGUID] UNIQUEIDENTIFIER NOT NULL,
    [AnswerText]   NVARCHAR (MAX)   NULL,
    [Value]        DECIMAL (5, 2)   NULL,
    [IsCorrect] BIT NULL, 
	[ChoicePosition] [int] NULL,
    CONSTRAINT [PK_Choices] PRIMARY KEY CLUSTERED ([ChoiceGUID] ASC),
    CONSTRAINT [FK_Choices_Questions] FOREIGN KEY ([QuestionGUID]) REFERENCES [dbo].[Questions] ([QuestionGUID]) ON UPDATE CASCADE ON DELETE CASCADE
);

