USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistProcedure('spNewTableId') = 1 DROP PROCEDURE spNewTableId
GO

CREATE PROCEDURE spNewTableId
    @TableName VARCHAR(50),
    @NextId INT OUTPUT
AS
BEGIN
    BEGIN TRAN
        --  TODO -> SELECT and generate from table
		SET @NextId = DATEPART(ms,GETDATE());
	COMMIT
END
GO