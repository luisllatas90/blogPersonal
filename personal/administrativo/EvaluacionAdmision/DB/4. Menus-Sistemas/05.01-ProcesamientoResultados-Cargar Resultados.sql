--**********************
--SCRIPT PARA CREAR MENU
--**********************

DECLARE @codigo_Men AS INT = 0 , 
		@descripcion_Men AS NVARCHAR(510) = 'Cargar Hoja de Respuestas' , --NOMBRE DEL MENU
		@enlace_Men AS NVARCHAR(510) = '../personal/administrativo/EvaluacionAdmision/ProcesamientoResultados/frmProcesamientoResultados.aspx?mod=1' , --RUTA DEL FORMULARIO
		@codigo_Apl AS INT = 32 , --CODIGO DE LA APLICACION
		@codigoRaiz_Men AS INT = 0, --CODIGO DEL MENU PADRE
		@icono_Men AS NVARCHAR(100) = '' ,
		@iconosel_men AS NVARCHAR(100) = '' ,
		@expandible_men AS NVARCHAR(10) = '' ,
		@nivel_men AS CHAR(1) = 2 ,
		@orden_men AS INT = 1 ,
		@variable_men AS NVARCHAR(100) = 'menu51' ,
		@formulario_men AS NVARCHAR(50) = '' ,
		@codigo_Tfu AS INT = 1 --PERFIL DE USUARIO		
		
		
SELECT @codigoRaiz_Men = codigo_Men FROM dbo.MenuAplicacion(NOLOCK) where codigo_Apl = 32 and descripcion_Men = 'Procesar Resultados'

--INSERTAR LA OPCION DE MENU
INSERT INTO MenuAplicacion (	
	descripcion_Men ,
	enlace_Men ,
	codigo_Apl ,
	codigoRaiz_Men ,
	icono_Men ,
	iconosel_men ,
	expandible_men ,
	nivel_men ,
	orden_men ,
	variable_men ,
	formulario_men 
) VALUES (	
	@descripcion_Men ,
	@enlace_Men ,
	@codigo_Apl ,
	@codigoRaiz_Men ,
	@icono_Men ,
	@iconosel_men ,
	@expandible_men ,
	@nivel_men ,
	@orden_men ,
	@variable_men ,
	@formulario_men 			
)

--OBTENER EL CODIGO DE MENU QUE SE HA INSERTADO
SELECT @codigo_Men = @@IDENTITY

--ASIGNAR LA OPCION DE MENU
INSERT INTO PermisoFuncionUsuario (
	codigo_Apl ,
	codigo_Tfu ,
	codigo_Men
) VALUES (
	@codigo_Apl ,
	@codigo_Tfu ,
	@codigo_Men
)


----ASIGNAR LA OPCION DE MENU
--INSERT INTO PermisoFuncionUsuario (
--	codigo_Apl ,
--	codigo_Tfu ,
--	codigo_Men
--) VALUES (
--	@codigo_Apl ,
--	222,
--	@codigo_Men
--)

----ASIGNAR LA OPCION DE MENU
--INSERT INTO PermisoFuncionUsuario (
--	codigo_Apl ,
--	codigo_Tfu ,
--	codigo_Men
--) VALUES (
--	@codigo_Apl ,
--	78,
--	@codigo_Men
--)

----ASIGNAR LA OPCION DE MENU
--INSERT INTO PermisoFuncionUsuario (
--	codigo_Apl ,
--	codigo_Tfu ,
--	codigo_Men
--) VALUES (
--	@codigo_Apl ,
--	211,
--	@codigo_Men
--)






