CREATE TABLE [dbo].[ADM_Componente_CompetenciaAprendizaje](
	[codigo_cca] [int] IDENTITY(1,1) NOT NULL,
	[codigo_cmp] [int] NULL,
	[codigo_com] [int] NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_cca] [bit] NULL,
 CONSTRAINT [PK_ADM_Componente_CompetenciaAprendizaje] PRIMARY KEY CLUSTERED 
(
	[codigo_cca] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_Componente_CompetenciaAprendizaje]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_Componente_CompetenciaAprendizaje_ADM_Componente] FOREIGN KEY([codigo_cmp])
REFERENCES [dbo].[ADM_Componente] ([codigo_cmp])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_Componente_CompetenciaAprendizaje] NOCHECK CONSTRAINT [FK_ADM_Componente_CompetenciaAprendizaje_ADM_Componente]
GO

ALTER TABLE [dbo].[ADM_Componente_CompetenciaAprendizaje]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_Componente_CompetenciaAprendizaje_CompetenciaAprendizaje] FOREIGN KEY([codigo_com])
REFERENCES [dbo].[CompetenciaAprendizaje] ([codigo_com])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_Componente_CompetenciaAprendizaje] NOCHECK CONSTRAINT [FK_ADM_Componente_CompetenciaAprendizaje_CompetenciaAprendizaje]
GO


