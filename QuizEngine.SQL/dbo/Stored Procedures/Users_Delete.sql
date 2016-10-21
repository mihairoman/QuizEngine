
---------------------------------------  DELETE A USER

CREATE PROCEDURE [dbo].[Users_Delete]
	@UserName NVARCHAR(MAX)

AS
BEGIN

	DELETE 
	FROM [Users]
	WHERE Username = @UserName

END;


