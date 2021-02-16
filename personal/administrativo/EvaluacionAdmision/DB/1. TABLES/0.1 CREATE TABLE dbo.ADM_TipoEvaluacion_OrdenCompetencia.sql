CREATE TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia](
	[codigo_teoc] [int] IDENTITY(1,1) NOT NULL,
	[codigo_tev] [int] NULL,
	[codigo_com] [int] NULL,
	[codigo_cmp] [int] NULL,
	[orden_teoc] [int] NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_teoc] [bit] NULL,
 CONSTRAINT [PK_ADM_TipoEvaluacion_OrdenCompetencia] PRIMARY KEY CLUSTERED 
(
	[codigo_teoc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_ADM_Componente] FOREIGN KEY([codigo_cmp])
REFERENCES [dbo].[ADM_Componente] ([codigo_cmp])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_ADM_Componente]
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_ADM_TipoEvaluacion] FOREIGN KEY([codigo_tev])
REFERENCES [dbo].[ADM_TipoEvaluacion] ([codigo_tev])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_ADM_TipoEvaluacion]
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_CompetenciaAprendizaje] FOREIGN KEY([codigo_com])
REFERENCES [dbo].[CompetenciaAprendizaje] ([codigo_com])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_OrdenCompetencia] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_OrdenCompetencia_CompetenciaAprendizaje]
GO


