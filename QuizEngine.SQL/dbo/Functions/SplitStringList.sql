CREATE FUNCTION [dbo].[SplitStringList]
(
	@StringList nvarchar(MAX)
)
RETURNS 
@ParsedList TABLE
(
	ItemID nvarchar(255)
)
AS
BEGIN
	DECLARE @strLen int
	DECLARE @strPos int
	DECLARE @nextStrDelim int
	DECLARE @val varchar(255)

	SET @strLen = len(@StringList)
	IF (@strLen <= 1 AND @StringList <> ',')
		INSERT @ParsedList VALUES (@StringList)
	ELSE
		SET @strPos = 0

		WHILE @strPos <= @strLen
		BEGIN
			SET @nextStrDelim = charindex(',', @StringList, @strPos)
			IF @nextStrDelim = 0
				SET @nextStrDelim = @strLen + 1

			SET @val = substring(@StringList, @strPos, @nextStrDelim - @strPos)

			INSERT @ParsedList VALUES (@val)

			SET @strPos = @nextStrDelim + 1
END

RETURN
END
