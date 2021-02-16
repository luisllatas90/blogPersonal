CREATE TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento](
	[codigo_cee] [int] IDENTITY(1,1) NOT NULL,
	[codigo_cco] [int] NULL,
	[codigo_cpf] [int] NULL,
	[codigo_tev] [int] NULL,
	[cantidad_cee] [int] NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_cee] [bit] NULL,
 CONSTRAINT [PK_ADM_ConfiguracionEvaluacionEvento] PRIMARY KEY CLUSTERED 
(
	[codigo_cee] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_ADM_TipoEvaluacion] FOREIGN KEY([codigo_tev])
REFERENCES [dbo].[ADM_TipoEvaluacion] ([codigo_tev])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento] NOCHECK CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_ADM_TipoEvaluacion]
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_CarreraProfesional] FOREIGN KEY([codigo_cpf])
REFERENCES [dbo].[CarreraProfesional] ([codigo_Cpf])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento] NOCHECK CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_CarreraProfesional]
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_CentroCostos] FOREIGN KEY([codigo_cco])
REFERENCES [dbo].[CentroCostos] ([codigo_Cco])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento] NOCHECK CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_CentroCostos]
GO


