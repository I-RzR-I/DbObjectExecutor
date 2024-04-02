USE [DbObjectExecutor]
GO

IF dbo.fnCheckIfExistProcedure('spGetRecordPaged') = 1 DROP PROCEDURE spGetRecordPaged
GO

CREATE PROCEDURE spGetRecordPaged
    @Skip				INT = 0,
	@Take				INT = 10,
	@OrderBy			VARCHAR(1024),
	@Where				VARCHAR(1024),
	@RowsCount			INT OUTPUT
AS
BEGIN
	DECLARE @MainQuery				NVARCHAR(MAX),
			@CountQuery				NVARCHAR(MAX);
	
	IF (@OrderBy IS NULL) OR (LTRIM(RTRIM(@OrderBy)) = '') SET @OrderBy = ' Id ASC ';
	IF (@Where IS NULL) OR (LTRIM(RTRIM(@Where)) = '') SET @Where = ' (1 = 1) ';
	
	SET @MainQuery = 'SELECT * FROM (
	SELECT 0 AS Id, ''Code-0'' AS Code, ''Name-0'' AS Name, CAST(1 AS BIT) AS IsActive  UNION ALL
	SELECT 1, ''Code-1'', ''Name-1'', CAST(0 AS BIT)  UNION ALL
	SELECT 2, ''Code-2'', ''Name-2'', CAST(0 AS BIT)  UNION ALL
	SELECT 3, ''Code-3'', ''Name-3'', CAST(0 AS BIT)  UNION ALL
	SELECT 4, ''Code-4'', ''Name-4'', CAST(0 AS BIT)  UNION ALL
	SELECT 5, ''Code-5'', ''Name-5'', CAST(0 AS BIT)  UNION ALL
	SELECT 6, ''Code-6'', ''Name-6'', CAST(0 AS BIT)  UNION ALL
	SELECT 7, ''Code-7'', ''Name-7'', CAST(0 AS BIT)  UNION ALL
	SELECT 8, ''Code-8'', ''Name-8'', CAST(0 AS BIT)  UNION ALL
	SELECT 9, ''Code-9'', ''Name-9'', CAST(0 AS BIT)  UNION ALL
	SELECT 10, ''Code-10'', ''Name-10'', CAST(0 AS BIT)  UNION ALL
	SELECT 11, ''Code-11'', ''Name-11'', CAST(0 AS BIT)  UNION ALL
	SELECT 12, ''Code-12'', ''Name-12'', CAST(0 AS BIT)  UNION ALL
	SELECT 13, ''Code-13'', ''Name-13'', CAST(0 AS BIT)  UNION ALL
	SELECT 14, ''Code-14'', ''Name-14'', CAST(0 AS BIT)  UNION ALL
	SELECT 15, ''Code-15'', ''Name-15'', CAST(0 AS BIT)  UNION ALL
	SELECT 16, ''Code-16'', ''Name-16'', CAST(0 AS BIT)  UNION ALL
	SELECT 17, ''Code-17'', ''Name-17'', CAST(0 AS BIT)  UNION ALL
	SELECT 18, ''Code-18'', ''Name-18'', CAST(0 AS BIT)  UNION ALL
	SELECT 19, ''Code-19'', ''Name-19'', CAST(0 AS BIT)  UNION ALL
	SELECT 20, ''Code-20'', ''Name-20'', CAST(0 AS BIT)  UNION ALL
	SELECT 21, ''Code-21'', ''Name-21'', CAST(0 AS BIT)  UNION ALL
	SELECT 22, ''Code-22'', ''Name-22'', CAST(0 AS BIT)  UNION ALL
	SELECT 23, ''Code-23'', ''Name-23'', CAST(0 AS BIT)  UNION ALL
	SELECT 24, ''Code-24'', ''Name-24'', CAST(0 AS BIT)  UNION ALL
	SELECT 25, ''Code-25'', ''Name-25'', CAST(0 AS BIT)  
	) AS T';

	SET @CountQuery = 'SELECT @Records = COUNT(*) FROM (' + @MainQuery + ' WHERE ' + @Where + ') TCOUNT';

	SET @MainQuery = @MainQuery
				+ ' WHERE ' + @Where
				+ ' ORDER BY ' + @OrderBy
				+ ' OFFSET ' + CAST(@Skip AS NVARCHAR(MAX)) + ' ROWS  
				FETCH NEXT ' + CAST(@Take AS NVARCHAR(MAX)) + ' ROWS ONLY';
	
	EXECUTE (@MainQuery)

	EXECUTE sp_executesql @CountQuery, N'@Records INT OUTPUT', @Records = @RowsCount OUTPUT	
END
GO