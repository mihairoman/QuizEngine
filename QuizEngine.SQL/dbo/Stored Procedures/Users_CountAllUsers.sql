﻿CREATE PROCEDURE [dbo].[Users_CountAllUsers]
AS
BEGIN
	SELECT COUNT(*)
	FROM [Users]
END
