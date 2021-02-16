CREATE TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento_Peso](
	[codigo_ceep] [int] IDENTITY(1,1) NOT NULL,
	[codigo_cee] [int] NULL,
	[nro_orden_ceep] [int] NULL,
	[peso_ceep] [numeric](8, 2) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_ceep] [int] NULL,
 CONSTRAINT [PK_ADM_ConfiguracionEvaluacionEvento_Peso] PRIMARY KEY CLUSTERED 
(
	[codigo_ceep] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento_Peso]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_Peso_ADM_ConfiguracionEvaluacionEvento] FOREIGN KEY([codigo_cee])
REFERENCES [dbo].[ADM_ConfiguracionEvaluacionEvento] ([codigo_cee])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_ConfiguracionEvaluacionEvento_Peso] NOCHECK CONSTRAINT [FK_ADM_ConfiguracionEvaluacionEvento_Peso_ADM_ConfiguracionEvaluacionEvento]
GO


