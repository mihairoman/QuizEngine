
---------------------------------------  DELETE A CHOISE

CREATE PROCEDURE [dbo].[Choices_Delete]
	@ChoiceGUID UNIQUEIDENTIFIER
AS
BEGIN

	DELETE 
	FROM [Choices]
	WHERE ChoiceGUID = @ChoiceGUID

END;


