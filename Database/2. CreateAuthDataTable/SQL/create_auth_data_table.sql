IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'AuthExample'))
USE AuthExample;

GO

IF OBJECT_ID('AuthData', 'u') IS NULL
CREATE TABLE AuthData
(
	LoginId INT IDENTITY(1, 1) PRIMARY KEY,
	Email NCHAR(50),
	Password NVARCHAR(50),
	Salt BINARY(16)
)