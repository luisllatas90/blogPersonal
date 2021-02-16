--**********************
--SCRIPT PARA CREAR MENU
--**********************

DECLARE @codigo_Men AS INT = 0 , 
		@descripcion_Men AS NVARCHAR(510) = 'Publicar Resultados' , --NOMBRE DEL MENU
		@enlace_Men AS NVARCHAR(510) = '' , --RUTA DEL FORMULARIO
		@codigo_Apl AS INT = 32 , --CODIGO DE LA APLICACION
		@codigoRaiz_Men AS INT = 0, --CODIGO DEL MENU PADRE
		@icono_Men AS NVARCHAR(100) = '' ,
		@iconosel_men AS NVARCHAR(100) = '' ,
		@expandible_men AS NVARCHAR(10) = '' ,
		@nivel_men AS CHAR(1) = 1 ,
		@orden_men AS INT = 6 ,
		@variable_men AS NVARCHAR(100) = 'menu6' ,
		@formulario_men AS NVARCHAR(50) = '' ,
		@codigo_Tfu AS INT = 1 --PERFIL DE USUARIO		
		
		
SELECT @codigoRaiz_Men = codigo_Men FROM dbo.MenuAplicacion(NOLOCK) where codigo_Apl = @codigo_Apl and descripcion_Men = 'Evaluación'

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






