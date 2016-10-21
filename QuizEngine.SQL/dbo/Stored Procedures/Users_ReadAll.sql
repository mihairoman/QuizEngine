
---------------------------------------  SELECT ALL USERS

CREATE PROCEDURE [dbo].[Users_ReadAll]

AS
BEGIN

	SELECT UserGUID, Username, Usertype, IsActive 
	FROM [Users];

END;


