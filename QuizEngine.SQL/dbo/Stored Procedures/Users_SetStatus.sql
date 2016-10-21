CREATE PROCEDURE [dbo].[Users_SetStatus]
	@UserGUID UNIQUEIDENTIFIER,
	@IsActive BIT
AS
BEGIN
    UPDATE [Users]
	SET IsActive = @IsActive
	WHERE (UserGUID = @UserGUID)
END;
