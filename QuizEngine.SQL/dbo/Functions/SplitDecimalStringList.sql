CREATE FUNCTION [dbo].[SplitDecimalStringList]
(
	@DecimalList NVARCHAR(MAX)
)
RETURNS 
@ParsedList TABLE
(
	ItemID Decimal(5,2)
)
AS
BEGIN
	DECLARE @strLen int
	DECLARE @strPos int
	DECLARE @nextStrDelim int
	DECLARE @val Decimal(5,2)

	SET @strLen = len(@DecimalList)
	IF (@strLen <= 1) RETURN
	SET @strPos = 0

	WHILE @strPos <= @strLen
	BEGIN
		SET @nextStrDelim = charindex(',', @DecimalList, @strPos)
		IF @nextStrDelim = 0
			SET @nextStrDelim = @strLen + 1

		SET @val = convert(decimal(5,2),substring(@DecimalList, @strPos, @nextStrDelim - @strPos))

		INSERT @ParsedList VALUES (@val)

		SET @strPos = @nextStrDelim + 1
	END 

RETURN
END