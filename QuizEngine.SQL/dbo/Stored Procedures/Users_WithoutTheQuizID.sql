CREATE PROCEDURE [dbo].[Users_WithoutTheQuizID]

	@SortExpression nvarchar(max),
	@GivenFoundrowPerPage INT = NULL,
	@GivenFoundPageNumber INT = NULL,
	@GivenQuizGUID UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SqlText NVARCHAR(MAX);
	DECLARE @ParmDefinition NVARCHAR(MAX);

	
	
	Set @SqlText = '(SELECT distinct u.UserGUID, u.Username, ut.TypeName, u.IsActive 
	                FROM Users u, UserTypes ut
                    where u.IsActive=1
					and ut.TypeID=u.Usertype )
					except 
					( select u.UserGUID, u.Username, ut.TypeName , u.IsActive
					from QuizUserLinks q 
					inner join Users u on u.UserGUID=q.UserGUID
					inner join UserTypes ut on ut.TypeID=u.Usertype
					where q.QuizGUID=@QuizGUID )' ;
	

	IF (@SortExpression IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
	IF (@GivenFoundPageNumber IS NOT NULL and @GivenFoundrowPerPage IS NOT NULL)
	BEGIN
			SET @SqlText = @SqlText + ' OFFSET  ((@FoundPageNumber-1)* @FoundrowPerPage)  ROWS FETCH NEXT @FoundrowPerPage ROWS ONLY';
	END
	END
	SET @ParmDefinition = N'@FoundPageNumber INT,@FoundrowPerPage INT, @QuizGUID UNIQUEIDENTIFIER';

	EXECUTE sp_executesql @SqlText, @ParmDefinition,
							@FoundrowPerPage = @GivenFoundrowPerPage,
							@FoundPageNumber = @GivenFoundPageNumber,
							@QuizGUID = @GivenQuizGUID ;
                    
END