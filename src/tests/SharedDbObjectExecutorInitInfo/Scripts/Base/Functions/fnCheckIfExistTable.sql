USE [DbObjectExecutor]
GO

CREATE FUNCTION fnCheckIfExistTable(@TableName varchar(200))
RETURNS bit
AS
BEGIN
    DECLARE @Return bit
     IF EXISTS (SELECT * FROM dbo.sysobjects
               WHERE id = OBJECT_ID(N'[dbo].[' + @TableName + ']') AND xtype = 'U')
        SET @Return = 1
    ELSE
        SET @Return = 0
    RETURN @Return
END
GO