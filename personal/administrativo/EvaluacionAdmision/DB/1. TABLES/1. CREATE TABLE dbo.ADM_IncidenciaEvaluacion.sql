CREATE TABLE [dbo].[ADM_IncidenciaEvaluacion](
	[codigo_ine] [int] IDENTITY(1,1) NOT NULL,
	[codigo_gru] [int] NULL,
	[descripcion_ine] [varchar](250) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_ine] [bit] NULL,
 CONSTRAINT [PK_ADM_IncidenciaEvaluacion] PRIMARY KEY CLUSTERED 
(
	[codigo_ine] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ADM_IncidenciaEvaluacion]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_IncidenciaEvaluacion_ADM_GrupoAdmisionVirtual] FOREIGN KEY([codigo_gru])
REFERENCES [dbo].[ADM_GrupoAdmisionVirtual] ([codigo_gru])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_IncidenciaEvaluacion] NOCHECK CONSTRAINT [FK_ADM_IncidenciaEvaluacion_ADM_GrupoAdmisionVirtual]
GO


