CREATE TABLE [dbo].[ADM_TipoEvaluacion_Indicador](
	[codigo_tei] [int] IDENTITY(1,1) NOT NULL,
	[codigo_tev] [int] NULL,
	[codigo_ind] [int] NULL,
	[cantidad_preguntas_tei] [int] NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_tei] [bit] NULL,
 CONSTRAINT [PK_ADM_TipoEvaluacion_Indicador] PRIMARY KEY CLUSTERED 
(
	[codigo_tei] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_Indicador]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_Indicador_ADM_Indicador] FOREIGN KEY([codigo_ind])
REFERENCES [dbo].[ADM_Indicador] ([codigo_ind])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_Indicador] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_Indicador_ADM_Indicador]
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_Indicador]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_Indicador_ADM_TipoEvaluacion] FOREIGN KEY([codigo_tev])
REFERENCES [dbo].[ADM_TipoEvaluacion] ([codigo_tev])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion_Indicador] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_Indicador_ADM_TipoEvaluacion]
GO


