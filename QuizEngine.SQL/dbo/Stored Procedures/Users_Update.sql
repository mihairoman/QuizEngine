
---------------------------------------  UPDATE A USER

CREATE PROCEDURE [dbo].[Users_Update]
	@UserGUID UNIQUEIDENTIFIER,
	@UserName NVARCHAR(MAX),
	@UserType SMALLINT,
	@IsActive BIT
AS
BEGIN
	UPDATE [Users] 
	SET  Username = @UserName, Usertype = @UserType, IsActive = @IsActive
	WHERE UserGUID = @UserGUID
END;


