CREATE TABLE [dbo].[Permissions]
(
	[UserGUID] UNIQUEIDENTIFIER NOT NULL , 
    [PermissionGUID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Permissions_ToTable] FOREIGN KEY (UserGUID) REFERENCES dbo.Users(UserGUID), 
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([UserGUID], [PermissionGUID]) 
)
