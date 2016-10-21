CREATE PROCEDURE [dbo].[Users_ReadPermissions]
@UserGUID UNIQUEIDENTIFIER

AS
BEGIN

	SELECT UserGUID, PermissionGUID
	FROM [Permissions]
	WHERE UserGUID = @UserGUID

END;