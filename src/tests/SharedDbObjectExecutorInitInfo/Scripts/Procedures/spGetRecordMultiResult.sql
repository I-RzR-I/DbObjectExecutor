USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistProcedure('spGetRecordMultiResult') = 1 DROP PROCEDURE spGetRecordMultiResult
GO

CREATE PROCEDURE spGetRecordMultiResult
	@Id INT = NULL
AS
BEGIN
	
	DECLARE @Tbl TABLE(Id INT, Code VARCHAR(10), Name VARCHAR(100), IsActive BIT);
	DECLARE @TblDetails TABLE(Id INT, ParentId INT, Code VARCHAR(10), Name VARCHAR(100), IsActive BIT,
								Address VARCHAR(255), Location VARCHAR(255), Country VARCHAR(255));

	INSERT INTO @Tbl (Id, Code, Name, IsActive)
	SELECT 0 AS Id, 'Code-0' AS Code, 'Name-0' AS Name, CAST(1 AS BIT) AS IsActive  UNION ALL
	SELECT 1, 'Code-1', 'Name-1', CAST(0 AS BIT)  UNION ALL
	SELECT 2, 'Code-2', 'Name-2', CAST(0 AS BIT)  UNION ALL
	SELECT 3, 'Code-3', 'Name-3', CAST(0 AS BIT)  UNION ALL
	SELECT 4, 'Code-4', 'Name-4', CAST(0 AS BIT)  UNION ALL
	SELECT 5, 'Code-5', 'Name-5', CAST(0 AS BIT)  UNION ALL
	SELECT 6, 'Code-6', 'Name-6', CAST(0 AS BIT)  UNION ALL
	SELECT 7, 'Code-7', 'Name-7', CAST(0 AS BIT)  UNION ALL
	SELECT 8, 'Code-8', 'Name-8', CAST(0 AS BIT)  UNION ALL
	SELECT 9, 'Code-9', 'Name-9', CAST(0 AS BIT)  ;

	
	INSERT INTO @TblDetails (Id, ParentId, Code, Name, IsActive, Address, Location, Country)
	SELECT 0, 1, 'Code_0', 'Name_0', CAST(1 AS BIT), 'str. My Home 0', 'Location 0', 'Country 0'  UNION ALL
	SELECT 1, 1, 'Code_1', 'Name_1', CAST(0 AS BIT), 'str. My Home 1', 'Location 1', 'Country 1'  UNION ALL
	SELECT 2, 2, 'Code_2', 'Name_2', CAST(1 AS BIT), 'str. My Home 2', 'Location 2', 'Country 2';

	SELECT * FROM @Tbl;
	SELECT * FROM @TblDetails WHERE ParentId = @Id;

END
GO