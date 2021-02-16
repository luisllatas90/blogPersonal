CREATE TABLE [dbo].[ADM_PesoCompetencia](
	[codigo_pcom] [int] IDENTITY(1,1) NOT NULL,
	[codigo_cac] [int] NULL,
	[codigo_cpf] [int] NULL,
	[codigo_com] [int] NULL,
	[peso_pcom] [numeric](8, 2) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_pcom] [bit] NULL,
 CONSTRAINT [PK_ADM_PesoCompetencia] PRIMARY KEY CLUSTERED 
(
	[codigo_pcom] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ADM_PesoCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_PesoCompetencia_CarreraProfesional] FOREIGN KEY([codigo_cpf])
REFERENCES [dbo].[CarreraProfesional] ([codigo_Cpf])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_PesoCompetencia] NOCHECK CONSTRAINT [FK_ADM_PesoCompetencia_CarreraProfesional]
GO

ALTER TABLE [dbo].[ADM_PesoCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_PesoCompetencia_CicloAcademico] FOREIGN KEY([codigo_cac])
REFERENCES [dbo].[CicloAcademico] ([codigo_Cac])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_PesoCompetencia] NOCHECK CONSTRAINT [FK_ADM_PesoCompetencia_CicloAcademico]
GO

ALTER TABLE [dbo].[ADM_PesoCompetencia]  WITH NOCHECK ADD  CONSTRAINT [FK_ADM_PesoCompetencia_CompetenciaAprendizaje] FOREIGN KEY([codigo_com])
REFERENCES [dbo].[CompetenciaAprendizaje] ([codigo_com])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ADM_PesoCompetencia] NOCHECK CONSTRAINT [FK_ADM_PesoCompetencia_CompetenciaAprendizaje]
GO


