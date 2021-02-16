DECLARE @codigo_apl int = 63 -- CRM USAT
DECLARE @codigo_men int = 2759
DECLARE @codigo_tfu int = 1 -- ADMINISTRADOR
DECLARE @codigo_per int = 684 -- ESAAVEDRA

INSERT INTO [BDUSAT].[dbo].[MenuAplicacion]
           ([descripcion_Men],[enlace_Men],[codigo_Apl],[codigoRaiz_Men],[icono_Men],[iconosel_men],[expandible_men],[nivel_men],[orden_men],[variable_men],[formulario_men])
     VALUES
           ('Importar Interesados','Crm/FrmImportarInteresados.aspx',@codigo_apl,2421,'','',NULL,1,2,'menu2',NULL)
