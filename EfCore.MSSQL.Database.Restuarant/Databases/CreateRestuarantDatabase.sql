IF NOT EXISTS (SELECT [name] FROM sys.databases WHERE [name] = 'RestuarantRecords')
BEGIN
	CREATE DATABASE RestuarantRecords
END
