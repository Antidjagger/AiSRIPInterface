CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(0, 1), 
    [Name] NVARCHAR(50) NULL, 
    [Login] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(50) NULL, 
    [CompanyID] NCHAR(10) NOT NULL
)
