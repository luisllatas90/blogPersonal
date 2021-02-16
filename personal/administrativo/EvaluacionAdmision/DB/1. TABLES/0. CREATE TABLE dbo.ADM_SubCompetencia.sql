CREATE TABLE [dbo].[ADM_SubCompetencia](
	[codigo_scom] [int] IDENTITY(1,1) NOT NULL,
	[codigo_com] [int] NULL,
	[nombre_scom] [varchar](500) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_scom] [bit] NULL,
 CONSTRAINT [PK_ADM_SubCompetencia] PRIMARY KEY CLUSTERED 
(
	[codigo_scom] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ADM_SubCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_SubCompetencia_CompetenciaAprendizaje] FOREIGN KEY([codigo_com])
REFERENCES [dbo].[CompetenciaAprendizaje] ([codigo_com])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_SubCompetencia] NOCHECK CONSTRAINT [FK_ADM_SubCompetencia_CompetenciaAprendizaje]
GO


