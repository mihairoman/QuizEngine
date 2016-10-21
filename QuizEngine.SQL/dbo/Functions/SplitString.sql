CREATE FUNCTION [dbo].[SplitString]
(
@List NVARCHAR(MAX),
@SplitOn NVARCHAR(5)
)
RETURNS @RtnValue TABLE (Value NVARCHAR(MAX))

AS
BEGIN

-- Charindex = Searches an expression for another expression and returns its starting position if found.
	WHILE(CHARINDEX(@SplitOn,@List)>0)
		BEGIN
			INSERT INTO @RtnValue (Value)
			SELECT Value = LTRIM(RTRIM(SUBSTRING(@List,1,CHARINDEX(@SplitOn,@List)-1))) 

			SET @List = SUBSTRING(@List,CHARINDEX(@SplitOn,@List)+LEN(@SplitOn),LEN(@List))
		END

	INSERT INTO @RtnValue (Value)
	SELECT Value = LTRIM(RTRIM(@List))

	RETURN
END