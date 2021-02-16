CREATE TABLE [dbo].[ADM_Componente](
	[codigo_cmp] [int] IDENTITY(1,1) NOT NULL,
	[nombre_cmp] [varchar](50) NULL,
	[codigo_per_reg] [int] NULL,
	[fecha_reg] [datetime] NULL,
	[codigo_per_act] [int] NULL,
	[fecha_act] [datetime] NULL,
	[estado_cmp] [bit] NULL,
 CONSTRAINT [PK_ADM_Componente] PRIMARY KEY CLUSTERED 
(
	[codigo_cmp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




