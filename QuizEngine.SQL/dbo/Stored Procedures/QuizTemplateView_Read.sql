CREATE PROCEDURE [dbo].[QuizTemplateView_Read]
@userGUID as uniqueidentifier,
    @PageNumber as int = NULL,
	@RowPerPage as int = NULL,
	@SortExpression as NVARCHAR(MAX) = 'dbo.QuizTemplates.QuizTemplateGUID ASC '
	
AS
BEGIN
	
	SET NOCOUNT ON;
	DECLARE @SqlText as NVARCHAR(MAX);
	DECLARE @Parameters  as NVARCHAR(MAX);
	SET @Parameters  = N'@userGUID uniqueidentifier,@FoundPageNumber int, @FoundRowPerPage int';
	SET @SqlText = 'SELECT DISTINCT dbo.QuizTemplates.QuizTemplateGUID, dbo.QuizTemplates.TypeName, count(dbo.Questions.QuestionGUID) AS QuestionNumber
	FROM dbo.QuizTemplates JOIN dbo.TemplateQuestionLinks ON dbo.QuizTemplates.QuizTemplateGUID= dbo.TemplateQuestionLinks.QuizTemplateGUID
	JOIN dbo.Questions ON dbo.Questions.QuestionGUID = dbo.TemplateQuestionLinks.QuestionGUID
	WHERE dbo.QuizTemplates.QuizTemplateGUID not in (select QuizGUID from dbo.QuizUserLinks where UserGUID=@userGUID)
	GROUP BY dbo.QuizTemplates.QuizTemplateGUID , dbo.QuizTemplates.TypeName ';
	IF (@SortExpression IS NOT NULL)
		BEGIN
			SET @SqlText = @SqlText + ' ORDER BY ' + @SortExpression;
		END
	IF (@PageNumber IS NOT NULL AND @RowPerPage IS NOT NULL)
		BEGIN
			SET @SqlText = @SqlText + ' OFFSET  ((@FoundPageNumber-1)* @FoundRowPerPage)  ROWS FETCH NEXT @FoundrowPerPage ROWS ONLY';
		END
     EXECUTE sp_executesql @SqlText, @Parameters,
			@userGUID = @userGUID,
			@FoundPageNumber = @PageNumber,
			@FoundRowPerPage = @RowPerPage

  
END