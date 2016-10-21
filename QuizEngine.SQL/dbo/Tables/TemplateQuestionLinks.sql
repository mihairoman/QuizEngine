CREATE TABLE [dbo].[TemplateQuestionLinks](
	[QuizTemplateGUID] [uniqueidentifier] NOT NULL,
	[QuestionGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SpecialQuizQuizestionLinks] PRIMARY KEY CLUSTERED 
(
	[QuizTemplateGUID] ASC,
	[QuestionGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TemplateQuestionLinks]  ADD  CONSTRAINT [FK_SpecialQuizQuizestionLinks_SpecialQuizes] FOREIGN KEY([QuizTemplateGUID])
REFERENCES [dbo].[QuizTemplates] ([QuizTemplateGUID])
GO

ALTER TABLE [dbo].[TemplateQuestionLinks] CHECK CONSTRAINT [FK_SpecialQuizQuizestionLinks_SpecialQuizes]