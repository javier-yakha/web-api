CREATE TABLE [dbo].[Complaints]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PersonName] NVARCHAR(50) NOT NULL, 
    [PersonApartmentCode] NVARCHAR(50) NOT NULL, 
    [Location] INT NOT NULL DEFAULT 0, 
    [Category] INT NOT NULL DEFAULT 0,
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Status] INT NOT NULL DEFAULT 0 , 
    [DateActivated] DATETIME NOT NULL, 
    [DateDeActivated] DATETIME NULL
)
