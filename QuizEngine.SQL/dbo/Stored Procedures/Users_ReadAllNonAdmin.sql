CREATE PROCEDURE [dbo].[Users_ReadAllNonAdmin]
AS
BEGIN
	Select UserGUID, Username, Usertype, IsActive
	FROM [Users]
	WHERE Usertype = 2 OR Usertype = 0
END;