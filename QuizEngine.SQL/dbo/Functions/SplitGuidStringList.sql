CREATE FUNCTION [dbo].[SplitGuidStringList]
(
	@GuidsList nvarchar(MAX)
)
RETURNS 
@ParsedList TABLE
(
	ItemID uniqueidentifier
)
AS
BEGIN
	DECLARE @strLen int
	DECLARE @strPos int
	DECLARE @nextStrDelim int
	DECLARE @val uniqueidentifier

	SET @strLen = len(@GuidsList)
	IF (@strLen <= 1) RETURN
	SET @strPos = 0

	WHILE @strPos <= @strLen
	BEGIN
		SET @nextStrDelim = charindex(',', @GuidsList, @strPos)
		IF @nextStrDelim = 0
			SET @nextStrDelim = @strLen + 1

		SET @val = substring(@GuidsList, @strPos, @nextStrDelim - @strPos)

		INSERT @ParsedList VALUES (@val)

		SET @strPos = @nextStrDelim + 1
	END 

RETURN
END
