CREATE PROCEDURE [dbo].[Users_UpdatePermissions]
	@UserGUID UNIQUEIDENTIFIER,
    @PermissionsListGUID NVARCHAR(MAX)
AS
BEGIN

	DELETE 
	FROM [Permissions]
	WHERE UserGUID = @UserGUID

	IF (LEN(@PermissionsListGUID) > 0)
	BEGIN
		INSERT INTO [Permissions] 
			([UserGUID],[PermissionGUID])
		SELECT @UserGUID, ItemID 
		FROM dbo.SplitStringList(@PermissionsListGUID);
	END;
END;