USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistFunction('fnGetHostLocationId') = 1 DROP FUNCTION fnGetHostLocationId
GO

CREATE FUNCTION fnGetHostLocationId()
RETURNS INT
AS
BEGIN
	DECLARE @Result INT;
	
	--TODO -> Select 
	SET @Result = 1;
	
	RETURN @Result
END
GO
	