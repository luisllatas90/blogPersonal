CREATE TABLE [dbo].[ADM_TipoEvaluacion](
	[codigo_tev] [int] IDENTITY(1,1) NOT NULL,
	[nombre_tev] [varchar](250) NULL,
	[peso_basica_tev] [numeric](8, 2) NULL,
	[peso_intermedia_tev] [numeric](8, 2) NULL,
	[peso_avanzada_tev] [numeric](8, 2) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_tev] [bit] NULL,
	[idArchivosCompartidos] [bigint] NULL,
	[virtual_tev] [bit] NULL,
 CONSTRAINT [PK_ADM_TipoEvaluacion] PRIMARY KEY CLUSTERED 
(
	[codigo_tev] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_TipoEvaluacion_ArchivoCompartido] FOREIGN KEY([idArchivosCompartidos])
REFERENCES [dbo].[ArchivoCompartido] ([IdArchivosCompartidos])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion] NOCHECK CONSTRAINT [FK_ADM_TipoEvaluacion_ArchivoCompartido]
GO

ALTER TABLE [dbo].[ADM_TipoEvaluacion] ADD  DEFAULT ((0)) FOR [virtual_tev]
GO