CREATE PROCEDURE [dbo].[Users_ReadByEmail]
@Username NVARCHAR(50)

AS
BEGIN

	SELECT UserGUID, Username, Usertype, IsActive 
	FROM [Users]
	WHERE Username = @Username

END;