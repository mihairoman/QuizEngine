
---------------------------------------  INSERT A USER

CREATE PROCEDURE [dbo].[Users_Insert]
	@UserGUID UNIQUEIDENTIFIER,
	@UserName NVARCHAR(MAX),
	@UserType SMALLINT
AS
BEGIN

	INSERT INTO [Users] (UserGUID, Username, Usertype)
	Values (@UserGUID, @UserName, @UserType)

END;


