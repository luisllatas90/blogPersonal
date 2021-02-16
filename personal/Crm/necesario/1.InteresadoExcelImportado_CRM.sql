
CREATE TABLE [dbo].[InteresadoExcelImportado_CRM](
	[CODIGO_REG] [int] IDENTITY(1,1) NOT NULL,
	[FECHA] [datetime] NULL,
	[DNI] [varchar] (20) NULL,
	[APELLIDO_PATERNO] [varchar] (100) NOT NULL,
	[APELLIDO_MATERNO] [varchar] (100) NULL,
	[NOMBRES] [varchar] (400) NOT NULL,
	[CELULAR] [varchar] (20) NULL,
	[EMAIL] [varchar] (100) NULL,
	[DIRECCION] [varchar] (1000) NULL,
	[ID_DISTRITO] [varchar] (20) NULL,
	[DISTRITO] [varchar] (200) NULL,
	[PROVINCIA] [varchar] (200) NULL,
	[ID_COLEGIO] [varchar] (20) NULL,
	[COLEGIO] [varchar] (500) NULL,
	[ID_NIVEL] [varchar] (20) NULL,
	[NIVEL] [varchar] (50) NULL,
	[ID_CARRERA] [varchar] (20) NULL,
	[CARRERA] [varchar] (200) NULL,
	[ID_EVENTO] [varchar] (20) NULL,
	[CATEGORIA] [varchar] (200) NULL,
	[TIPO] [varchar] (200) NULL,
	[PROCESADO] [varchar] (20) NOT NULL,
	[NOMBRE_ARCHIVO] [varchar] (400) NOT NULL,
	[FECHA_REGISTRO] [datetime] NULL,
	[codigo_per] [varchar] (20) NULL
)


