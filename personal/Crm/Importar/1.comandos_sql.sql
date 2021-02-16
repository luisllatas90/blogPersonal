EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'AllowInProcess', 1
EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'DynamicParameters', 1
exec sp_configure 'Advanced', 1
RECONFIGURE
exec sp_configure 'Ad Hoc Distributed Queries', 1
--RECONFIGURE
--exec sp_configure 'xp_cmdshell', 1
RECONFIGURE
GO
sp_configure 'show advanced options', 1
GO
RECONFIGURE WITH OverRide
GO
sp_configure 'Ad Hoc Distributed Queries', 1
GO
RECONFIGURE WITH OverRide
GO