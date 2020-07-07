if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TMP_desayunoPPK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TMP_desayunoPPK]
GO

CREATE TABLE [dbo].[TMP_desayunoPPK] (
	[codigo_des] [int] IDENTITY (1, 1) NOT NULL ,
	[desayuno] [bit] NOT NULL ,
	[codigo_per] [int] NULL 
) ON [PRIMARY]
GO

