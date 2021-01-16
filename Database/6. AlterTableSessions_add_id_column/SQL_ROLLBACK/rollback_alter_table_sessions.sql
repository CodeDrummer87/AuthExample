IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'AuthExample'))
USE AuthExample;

GO

IF OBJECT_ID('Sessions', 'u') IS NOT NULL
ALTER TABLE Sessions
DROP COLUMN Id