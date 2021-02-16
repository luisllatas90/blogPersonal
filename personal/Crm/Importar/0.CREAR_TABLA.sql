--DROP TABLE [InteresadoExcelImportado_CRM]
CREATE TABLE [dbo].[InteresadoExcelImportado_CRM](
	[CODIGO_REG] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_InteresadoExcelImportado_CRM] PRIMARY KEY CLUSTERED,
	[FECHA] [datetime] NULL,
	[ID_TIPODOCUMENTO] [int]  NOT NULL,
	[NRO_DOCUMENTO] [varchar] (50) NULL,
	[APELLIDO_PATERNO] [varchar](200) NOT NULL,
	[APELLIDO_MATERNO] [varchar](200) NULL,
	[NOMBRES] [varchar](400) NOT NULL,
	[CELULAR] [varchar] (50) NULL,
	[EMAIL] [varchar](100) NULL,
	[DIRECCION] [varchar](1000) NULL,
	[ID_DISTRITO] [int]  NULL,
	[DISTRITO] [varchar](200) NULL,
	[PROVINCIA] [varchar](200) NULL,
	[ID_COLEGIO] [int] NULL,
	[COLEGIO] [varchar](500) NULL,
	[ID_NIVEL] [char] (1) NULL,
	[NIVEL] [varchar](50) NULL,
	[ID_CARRERA] [int] NULL,
	[CARRERA] [varchar](200) NULL,
	[CATEGORIA] [varchar](200) NULL,
	[TIPO] [varchar](200) NULL,
	[PROCESADO] [VARCHAR] (200) NOT NULL
)

GO
