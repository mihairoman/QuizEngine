CREATE TABLE [dbo].[QuizUserLinks] (
    [QuizGUID] UNIQUEIDENTIFIER NOT NULL,
    [UserGUID] UNIQUEIDENTIFIER NOT NULL,
    [Result]   DECIMAL (5, 2)   NULL,
    [QuizDate] DATETIME NOT NULL, 
    [OnlineOrDownloaded] BIT NOT NULL, 
    [IsTaken] BIT NULL, 
    CONSTRAINT [PK_TakenQuizes_1] PRIMARY KEY CLUSTERED ([QuizGUID] ASC, [UserGUID] ASC),
    CONSTRAINT [FK_TakenQuizes_Quizes] FOREIGN KEY ([QuizGUID]) REFERENCES [dbo].[Quizes] ([QuizGUID]),
    CONSTRAINT [FK_TakenQuizes_Users] FOREIGN KEY ([UserGUID]) REFERENCES [dbo].[Users] ([UserGUID])
);

