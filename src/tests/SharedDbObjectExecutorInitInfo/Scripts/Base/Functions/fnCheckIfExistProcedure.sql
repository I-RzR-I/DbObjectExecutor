USE [DbObjectExecutor]
GO

CREATE FUNCTION fnCheckIfExistProcedure(@ProcedureName varchar(200))
RETURNS BIT
AS
BEGIN
    DECLARE @Return bit
    IF EXISTS (SELECT * FROM dbo.sysobjects
               WHERE id = OBJECT_ID(N'[dbo].[' + @ProcedureName + ']') AND xtype = 'P')
        SET @Return = 1
    ELSE
        SET @Return = 0
    RETURN @Return
END
GO