USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistFunction('fnGetResultAsTable') = 1 DROP FUNCTION fnGetResultAsTable
GO

CREATE FUNCTION fnGetResultAsTable (@CallType INT)
RETURNS @ResultTable TABLE (
						Id			INT, 
						LocationId	INT, 
						IsVisible	BIT
					) 
AS
BEGIN
	INSERT INTO @ResultTable (Id, LocationId,IsVisible)
	SELECT 1, 1, 0  UNION ALL
	SELECT 2, 10, 1  UNION ALL
	SELECT 3, 223, 0  UNION ALL
	SELECT 4, 3, 1  UNION ALL
	SELECT 5, 312, 0  UNION ALL
	SELECT 6, 31, 1  UNION ALL
	SELECT 7, 6, 0  UNION ALL
	SELECT 8, 7, 1  UNION ALL
	SELECT 9, 45,  0  ;

	RETURN;
END
GO