USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistFunction('fnGetDbType') = 1 DROP FUNCTION fnGetDbType
GO

CREATE FUNCTION fnGetDbType ()
RETURNS varchar(50)
AS
BEGIN
	DECLARE @DbType varchar(50);
	
	--TODO -> Select 
	SET @DbType = 'HeadOffice';
	
	RETURN ISNULL(@DbType, '');
END
GO