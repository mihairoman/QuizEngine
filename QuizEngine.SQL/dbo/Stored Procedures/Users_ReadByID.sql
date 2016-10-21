
---------------------------------------  SELECT ONE USER

CREATE PROCEDURE [dbo].[Users_ReadByID]
@UserGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT UserGUID, Username, Usertype, IsActive 
	FROM [Users]
	WHERE UserGUID = @UserGUID

END;


