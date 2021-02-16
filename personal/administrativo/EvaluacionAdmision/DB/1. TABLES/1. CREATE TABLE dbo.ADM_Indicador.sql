CREATE TABLE [dbo].[ADM_Indicador](
	[codigo_ind] [int] IDENTITY(1,1) NOT NULL,
	[codigo_scom] [int] NULL,
	[nombre_ind] [varchar](150) NULL,
	[descripcion_ind] [varchar](500) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_ind] [bit] NULL,
 CONSTRAINT [PK_ADM_Indicador] PRIMARY KEY CLUSTERED 
(
	[codigo_ind] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ADM_Indicador]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_Indicador_ADM_SubCompetencia] FOREIGN KEY([codigo_scom])
REFERENCES [dbo].[ADM_SubCompetencia] ([codigo_scom])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_Indicador] NOCHECK CONSTRAINT [FK_ADM_Indicador_ADM_SubCompetencia]
GO


