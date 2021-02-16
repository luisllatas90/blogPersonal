CREATE TABLE [dbo].[ADM_EvaluacionDetalle_Observacion](
	[codigo_edo] [int] IDENTITY(1,1) NOT NULL,
	[codigo_evd] [int] NULL,
	[descripcion_edo] [varchar](250) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_edo] [bit] NULL,
 CONSTRAINT [PK_ADM_EvaluacionDetalle_Observacion] PRIMARY KEY CLUSTERED 
(
	[codigo_edo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ADM_EvaluacionDetalle_Observacion]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_EvaluacionDetalle_Observacion_ADM_EvaluacionDetalle] FOREIGN KEY([codigo_evd])
REFERENCES [dbo].[ADM_EvaluacionDetalle] ([codigo_evd])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_EvaluacionDetalle_Observacion] NOCHECK CONSTRAINT [FK_ADM_EvaluacionDetalle_Observacion_ADM_EvaluacionDetalle]
GO


