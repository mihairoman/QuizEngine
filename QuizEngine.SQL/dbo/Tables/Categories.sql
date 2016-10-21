CREATE TABLE [dbo].[Categories] (
    [CategoryGUID] UNIQUEIDENTIFIER NOT NULL,
    [CategoryName] NVARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_Categoryes] PRIMARY KEY CLUSTERED ([CategoryGUID] ASC)
);

