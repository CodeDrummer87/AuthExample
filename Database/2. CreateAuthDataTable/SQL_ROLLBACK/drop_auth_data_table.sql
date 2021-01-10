IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'AuthExample'))
USE AuthExample;

GO

IF OBJECT_ID('AuthData', 'u') IS NOT NULL
DROP TABLE AuthData;