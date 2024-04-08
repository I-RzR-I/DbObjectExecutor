IF dbo.fnCheckIfExistTable('Locations') = 0
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION t;

	CREATE TABLE [dbo].[Locations](
		[Id] [int] NOT NULL,
		[Code] [nvarchar](50) NOT NULL,
		[Name] [nvarchar](255) NOT NULL,
		[Description] [nvarchar](512) NOT NULL,
		[IsActive] [bit] NOT NULL
	);
	
	INSERT INTO Locations ([Id],[Code],[Name],[Description],[IsActive])
	SELECT 1, 'Code-1', 'Name-1', 'Description-1', 1 UNION ALL
	SELECT 2, 'Code-2', 'Name-2', 'Description-2', 0 UNION ALL
	SELECT 3, 'Code-3', 'Name-3', 'Description-3', 1 UNION ALL
	SELECT 4, 'Code-4', 'Name-4', 'Description-4', 0 UNION ALL
	SELECT 5, 'Code-5', 'Name-5', 'Description-5', 1 

	COMMIT TRANSACTION t;
	END TRY

	BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT @ErrorMessage = ERROR_MESSAGE()
	,      @ErrorSeverity = ERROR_SEVERITY()
	,      @ErrorState = ERROR_STATE();
	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	ROLLBACK TRANSACTION transaction_nr_1;
	END CATCH;
END;
ELSE
	PRINT('Table already exist');
GO
