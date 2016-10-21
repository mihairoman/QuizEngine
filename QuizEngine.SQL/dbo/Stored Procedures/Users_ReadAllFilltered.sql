CREATE PROCEDURE [dbo].[Users_ReadAllFilltered]
	@SortExpression AS NVARCHAR(MAX) = null,
	@FoundPageNumber INT = NULL ,
	@FoundRowspPage INT = NULL
AS
BEGIN

SET NOCOUNT ON;
	Declare @SqlText AS NVARCHAR(MAX);

	DECLARE @ParmDefinition NVARCHAR(MAX);

	DECLARE @SortDirection NVARCHAR(MAX);

	SET @ParmDefinition = N'@FoundPageNumberParam INT,@FoundRowspPageParam INT';

	


	IF (@SortExpression = 'u.UserType ASC' or @SortExpression = 'u.UserType DESC')
		BEGIN

		IF(@SortExpression = 'u.UserType ASC')
			BEGIN
				SET @SortDirection = 'ASC'
			END
		ELSE
			BEGIN
				SET @SortDirection = 'DESC'
			END

		SET @SqlText = '
				SELECT DISTINCT u.UserGUID, u.UserName, u.UserType, u.IsActive
				FROM (SELECT us.UserGUID, us.Username, us.UserType, us.IsActive, ut.TypeName
					  FROM Users us
					  INNER JOIN UserTypes ut
					  ON us.Usertype= ut.TypeID
					  ORDER BY ut.TypeName ' + @SortDirection + '
					  OFFSET ((@FoundPageNumberParam - 1) * @FoundRowspPageParam) ROWS
					  FETCH NEXT @FoundRowspPageParam ROWS ONLY
				 ) u';
		END
	ELSE
		BEGIN
			SET @SqlText = '
				SELECT DISTINCT u.UserGUID, u.UserName, u.UserType, u.IsActive
				FROM [dbo].Users u';

			IF (@SortExpression IS NOT NULL)
			BEGIN
					SET @SqlText = @SqlText + ' Order by ' + @SortExpression;
			END

			SET @SqlText = @SqlText + ' OFFSET ((@FoundPageNumberParam - 1) * @FoundRowspPageParam) ROWS
								FETCH NEXT @FoundRowspPageParam ROWS ONLY';
		END

	EXECUTE sp_executesql 
					  @SqlText,
					  @ParmDefinition,
					  @FoundPageNumber,
					  @FoundRowspPage;
                    
END