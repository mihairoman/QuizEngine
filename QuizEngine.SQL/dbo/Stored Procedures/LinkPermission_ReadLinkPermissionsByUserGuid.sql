CREATE PROCEDURE [dbo].[LinkPermission_ReadLinkPermissionsByUserGuid]
	@userGUID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT Link, lp.PermissionGUID, CSSClass
	FROM [dbo].[LinkPermission] lp
	INNER JOIN [dbo].[Permissions] p
	ON p.PermissionGUID=lp.PermissionGUID
	WHERE p.UserGUID=@userGUID;
END