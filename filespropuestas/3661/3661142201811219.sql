/* Ult. modif.: 17-04-2017 09:00 a.m.                              
 * Usuario: HCano                              
 * Observacion: Mantenimiento de Programas y Proyectos, VALIDACION presupuesto                        
 * Observacion: Se agrega validacion para Crear accesos                    
 */       
                            
/*Historial de Cambios*/            
/* Codigo  Fecha  Desarrollador  Descripcion*/                        
-- 001  10/08/2017 moises.vilchez   - Capturar el ID de Actividad Poa e Insertar las Actividades            
-- 002  05/09/2017 hcano Verificar  - Si tiene una categoria asignada (Elimina), sino Solo Actualiza ACtividad_POA          
-- 003  24/11/2017 moises.vilchez   - Se Consulto si tenia registros en Detalle de Actividad POA no se puede Eliminar      
-- 004  27/11/2017 moises.vilchez   - Se cambio el ID 55 en tabla apliacion por 69      
-- 005  29/11/2017 moises.vilchez   - Se declaro variable apliación       
-- 006  01/12/2017 moises.vilchez   - Se agrego manejo de transacciones      
-- 007  07/12/2017 moises.vilchez   - Se inicio Variable @codigo_Apl int = 69    
-- 008  12/12/2017 moises.vilchez   - Se Actualiza la vigencia de DetallePresupuesto    
-- 009  13/02/2018 moises.vilchez   - Se indico parametros a insertar en tabla detalleActividadPOA
      
ALTER PROCEDURE [dbo].[POA_ActualizarActividad]          
@abreviatura varchar(10),                             
@resumen_acp nvarchar(400),                            
@egresos decimal(14,2),                            
@ingresos decimal(14,2),                            
@utilidad decimal(14,2),                            
@usuario int,                            
@codigo_tac int,                            
@responsable int,                            
@apoyo_acp int,                            
@codigo_cco int,        
--@codigo_ppr int, -- codigo programa presupuestal se quita se jalara del CCO                                                  
@codigo_poa int,                            
@fecha_ini varchar(15),                            
@fecha_fin varchar(15),                            
@codigo_ejp int,                            
@codigo_acp int, -- para modificar        
@codigo_cpa int=0 --Código de Categoría      
      
AS                            
BEGIN TRY -- 006 01/12/2017 moises.vilchez - Se agrego manejo de transacciones      
 BEGIN TRANSACTION      
      
 DECLARE @mensaje varchar(5)                          
 DECLARE @validacion int                          
 DECLARE @acumulado decimal(16,2)                        
 DECLARE @presu_disponible decimal(16,2)                  
 DECLARE @cuenta_acceso int -- Para verificar si tiene El Acceso                  
 DECLARE @codigo_Apl int = 69 --005 Se declaro variable apliación     
    
 --Valida que no se cree Centro de Costo en el mismo ejercicio presupuestal                  
 SELECT @validacion = COUNT(codigo_acp) FROM Actividad_POA ap WITH(NOLOCK) inner join Areas_poa ar ON ap.codigo_poa=ar.codigo_poa where ap.codigo_cco=@codigo_cco and ar.codigo_ejp=@codigo_ejp and estado_acp=1 and codigo_acp<>@codigo_acp                   
       
 --Acumulado                    
 SELECT @acumulado=SUM(egresos_acp) FROM Actividad_POA where codigo_poa=@codigo_poa and estado_acp=1 and codigo_acp<>@codigo_acp                    
 --Presupuesto Disponible                    
 SELECT @presu_disponible = (limite_egreso-ISNULL(@acumulado,0)) from Areas_POA where codigo_poa=@codigo_poa                    
               
 DECLARE @etapa int              
 SET @etapa=(SELECT codigo_eta FROM EjercicioPresupuestal where codigo_Ejp=@codigo_ejp)              
               
 IF @etapa<>4              
  BEGIN              
   IF @validacion = 0                          
    BEGIN                       
     IF @presu_disponible>=@egresos OR @etapa = 3              
      -- SE AGREGA SI EL EJERCICIO PRESUPUESTAL ESTA EN ETAPA DE EJECUCION PERMITIRA REGISTRAR NUEVOS PROGRAMAS Y PROYECTOS SIN VALIDAR EL PRESUPUESTO.              
      -- CODIGO_ETA = 3 : EJERCICIO PRESUPUESTAL EN EJECUCION              
      BEGIN                       
       IF @codigo_acp=0                          
        BEGIN                          
         INSERT INTO [BDUSAT].[dbo].[Actividad_POA]                   
          ([abreviatura_acp]                          
          ,[resumen_acp]                          
          ,[egresos_acp]                          
          ,[ingresos_acp]                          
          ,[utilidad_acp]                          
          ,[fecha_reg]                          
          ,[usuario_reg]                          
          ,[codigo_tac]                          
          ,[responsable_acp]                          
          ,[codigo_cco]                          
          ,[fecini_acp]                          
          ,[fecfin_acp]                          
          ,[codigo_poa]                          
          ,[codigo_iep]                          
          ,[apoyo_acp]              
          ,[CreaenEjecucion_acp])                          
         VALUES                          
          (@abreviatura                          
          ,@resumen_acp                          
          ,@egresos                          
          ,@ingresos                          
          ,@utilidad               
          ,GETDATE()                          
          ,@usuario                          
          ,@codigo_tac                          
          ,@responsable                          
          ,@codigo_cco                          
          ,@fecha_ini                          
          ,@fecha_fin                          
          ,@codigo_poa                          
          ,1                          
          ,@apoyo_acp              
          ,CASE WHEN @etapa=3 THEN 1 ELSE 0 END )                 
           
			-- 001 Capturar el ID de Actividad Poa e Insertar las Actividades      
			SET @codigo_acp = IDENT_CURRENT('Actividad_POA') 
			INSERT INTO DetalleActividad_POA (descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, fecha_reg, usuario_reg, responsable_dap, codigo_acp, requiere_pto) -- 009
			SELECT 
				cat.nombre_cat, 100, a.fecini_acp, fecfin_acp, 'A', GETDATE(), @usuario, a.responsable_acp, a.codigo_acp, 1    
			FROM 
				Actividad_POA a inner join CentroCostos cco ON (a.codigo_cco=cco.codigo_Cco)          
				inner join CategoriaProyActividad_POA cpa ON (cpa.codigo_cap=cco.codigo_cpa)          
				inner join CategoriaProgProy_POA cap ON (cpa.codigo_cap=cap.codigo_cap)          
				inner join CategoriaActividad_POA cat ON (cpa.codigo_cat=cat.codigo_cat)          
			WHERE 
				a.codigo_acp=@codigo_acp
				and cpa.estado_cpa=1        
				and cat.estado_cat=1 
                        
         
         UPDATE CentroCostos SET codigo_Per=@responsable,descripcion_Cco=@resumen_acp, codigo_cpa=@codigo_cpa WHERE codigo_Cco=@codigo_cco            
                
         IF @@ERROR <> 0                   
          BEGIN                           
           SET @mensaje='0'                           
          END                             
         ELSE                           
          BEGIN                                
           SET @mensaje=IDENT_CURRENT('Actividad_POA') -- Se Registro                        
           -------VERIFICAR EL ACCESO A INDICADORES Y A REGISTRO DE PROGRAMA/PROYECTO                  
           SELECT @cuenta_acceso = COUNT(*) FROM UsuarioAplicacion WHERE codigo_Uap=@responsable and codigo_Apl=@codigo_Apl and (codigo_Tfu = 154 or codigo_Tfu = 155)                  
           IF @cuenta_acceso = 0                  
            BEGIN                  
             INSERT INTO UsuarioAplicacion ([tipo_Uap],[codigo_Uap],[codigo_Apl],[codigo_Tfu],[restriccion_Uap],[codigoRestriccion_Uap])                   
             VALUES (1,@responsable,@codigo_Apl,155,0,0) -- 50 : Aplicacion Indicadores, 155 : Tipo Funcion : Registro Prog/Proy    -- 004   27/11/2017 moises.vilchez -   Se cambio el ID 55 en tabla apliacion por 69               
             --VALUES (1,@responsable,50,155,0,0) -- 50 : Aplicacion Indicadores, 155 : Tipo Funcion : Registro Prog/Proy                  
            END                   
            -- FIN VERIFICACION                   
          END                             
        END       
       ELSE --Modificar     
        BEGIN               
         DECLARE @responsable_ant int                  
         SET @responsable_ant = (SELECT Responsable_acp FROM Actividad_POA where codigo_acp=@codigo_acp)                   
         --EDICION DE ACTIVIDADES CREADAS EN (INICIO Y PLANIFICACION) PODRAN SER EDITADAS MIENTRAS ESTEN ESTAS ETAPAS              
         -- EDICION DE ACTIVIDADES CREADAS EN EJECUCION PODRAN SER EDITADAS SIN NINGUN PROBLEMA EN LA EJECUCION              
         -- ADMINISTRADOR O DIRECCION DE PLANIFICACION PUEDEN EDITAR SIN VALIDACION DE ETAPA              
         IF (@etapa=3 AND (SELECT CreaEnEjecucion_acp FROM Actividad_POA WHERE codigo_acp=@codigo_acp)=1) OR ((@etapa=1 OR @etapa=2) AND (SELECT CreaEnEjecucion_acp FROM Actividad_POA where codigo_acp=@codigo_acp)<>1) OR ((SELECT COUNT(*) FROM UsuarioAplicacion WHERE codigo_Uap=@usuario and codigo_Tfu IN (1,128) and codigo_Apl=69)>0)              
          BEGIN              
           --Verificamos Si va a cambiar Centro de Costo y Fechas (no editables)                  
           DECLARE @cuenta_mod INT    
           SELECT @cuenta_mod = COUNT(codigo_acp) FROM Actividad_POA WHERE codigo_Acp=@codigo_acp and codigo_cco=@codigo_cco and fecini_acp=@fecha_ini and fecfin_acp=@fecha_fin                  
                     
           -- si va a cambiar verificamos si tiene detalles de presupuesto                  
           --Cuenta detalles de Presupuesto                  
           DECLARE @nro_detpto INT=0                  
           IF @cuenta_mod=0                  
            BEGIN                  
             SELECT @nro_detpto=COUNT(codigo_dpr)     
             FROM Actividad_POA ac WITH(NOLOCK) INNER JOIN DetalleActividad_POA dap WITH(NOLOCK) ON dap.codigo_acp=ac.codigo_acp and dap.estado_dap='A'                  
              INNER JOIN DetallePresupuesto dpr WITH(NOLOCK) ON dpr.codigo_dap=dap.codigo_dap and dpr.vigencia_Dpr=1                  
             WHERE ac.codigo_acp=@codigo_acp                  
            END                  
           IF @nro_detpto=0                  
            BEGIN                  
             --calculo egresos de DETALLE PRESUPUESTO                  
             DECLARE @valida_egresos DECIMAL(18,2)                  
             SET @valida_egresos=ISNULL((SELECT SUM(precioUnitario_Dpr*cantidadTotal_Dpr)     
                    FROM Actividad_POA ac WITH(NOLOCK)                   
                     INNER JOIN DetalleActividad_POA dap WITH(NOLOCK) ON dap.codigo_acp=ac.codigo_acp and dap.estado_dap='A'                  
                     INNER JOIN DetallePresupuesto dpr WITH(NOLOCK) ON dpr.codigo_dap=dap.codigo_dap and dpr.vigencia_Dpr=1                  
                    WHERE ac.codigo_acp=@codigo_acp and vigencia_Dpr=1 and tipo_Dpr='E'),0)                  
                 
             IF @valida_egresos<=@egresos -- EGRESOS ACTUALES REGISTRADOS EN DETALLE PRESUPUESTO DEBEN SER MENORES A EGRESOS DE ACTIVIDAD                  
              BEGIN                    
               IF @responsable <> @responsable_ant                  
                BEGIN                  
                -------VERIFICAR EL ACCESO A INDICADORES Y A REGISTRO DE PROGRAMA/PROYECTO                  
                 SELECT @cuenta_acceso = COUNT(*) FROM UsuarioAplicacion WHERE codigo_Uap=@responsable and codigo_Apl=@codigo_Apl and (codigo_Tfu = 154 or codigo_Tfu = 155)                  
                 IF @cuenta_acceso = 0                  
                  BEGIN                  
                   INSERT INTO UsuarioAplicacion ([tipo_Uap],[codigo_Uap],[codigo_Apl],[codigo_Tfu],[restriccion_Uap],[codigoRestriccion_Uap])                   
                   VALUES (1,@responsable,@codigo_Apl,155,0,0) -- 50 : Aplicacion Indicadores, 155 : Tipo Funcion : Registro Prog/Proy      -- 004   27/11/2017 moises.vilchez -   Se cambio el ID 55 en tabla apliacion por 69            
                   --VALUES (1,@responsable,50,155,0,0) -- 50 : Aplicacion Indicadores, 155 : Tipo Funcion : Registro Prog/Proy                  
                  END                   
                  -------- FIN VERIFICACION                            
                END                  
                       
               UPDATE [BDUSAT].[dbo].[Actividad_POA]                          
               SET [abreviatura_acp] = @abreviatura                          
                ,[resumen_acp] = @resumen_acp                          
                ,[egresos_acp] = @egresos                          
                ,[ingresos_acp] = @ingresos                          
                ,[utilidad_acp] = @utilidad                          
                ,[fecha_mod] = GETDATE()                          
                ,[usuario_mod] = @usuario                          
                ,[codigo_tac] = @codigo_tac                          
                ,[responsable_acp] = @responsable                          
                ,[apoyo_acp] = @apoyo_acp                          
                ,[codigo_cco] = @codigo_cco                          
                ,[fecini_acp] = @fecha_ini                          
                ,[fecfin_acp] = @fecha_fin                          
               WHERE codigo_acp=@codigo_acp                  
    
    
               DECLARE @codigoEjp int    
               SELECT @codigoEjp= COUNT(act.codigo_acp) FROM Actividad_POA act inner join Areas_POA a ON (a.codigo_poa=act.codigo_poa) WHERE act.codigo_acp=@codigo_acp and codigo_ejp >= 9    
               IF @codigoEjp > 0     
                BEGIN    
                 IF (SELECT codigo_cpa FROM CentroCostos where codigo_Cco=@codigo_cco)>0 -- 002 Verificar Si tiene una categoria asignada (Elimina), sino Solo Actualiza ACtividad_POA        
                  BEGIN        
                   -- Verificar que no tenga items en presupuesto          
                   IF (SELECT count(codigo_Dpr)      
                    FROM Actividad_POA a inner join DetalleActividad_POA det ON (a.codigo_acp=det.codigo_acp)       
                     inner join DetallePresupuesto de ON (de.codigo_dap=det.codigo_dap)      
                    WHERE a.codigo_acp=@codigo_acp and de.vigencia_Dpr=1)= 0      
                    BEGIN      
                     -- 003    24/11/2017 moises.vilchez - Se Consulto si tenia registros en Detalle de Actividad POA no se puede Eliminar    
                     --Validación Si tiene al menos un registro NO ELIMINAR Detalle de Actividad, pero si cambia de categoria si eliminar e insertar las nuevas categorias    
                     DECLARE @codigo_cpa_cco INT=0    
                     SELECT @codigo_cpa_cco=codigo_cpa FROM CentroCostos WHERE codigo_Cco in (SELECT codigo_cco FROM Actividad_POA WHERE codigo_acp=@codigo_acp)    
                     IF @codigo_cpa_cco <> @codigo_cpa    
                      BEGIN    
                       --001 Capturar el ID de Actividad Poa e Insertar las Actividades          
                       --DELETE FROM DetalleActividad_POA WHERE codigo_acp=@codigo_acp              
                       update DetalleActividad_POA set estado_dap='I' where codigo_acp=@codigo_acp    
                           
                       UPDATE DetallePresupuesto SET vigencia_Dpr=0 where codigo_dap in (select codigo_dap  from DetalleActividad_POA where estado_dap='I' and codigo_acp=@codigo_acp) --008    
                                                  
                       --Desactivar Items en Presupuesto    
                       UPDATE CentroCostos SET codigo_cpa=@codigo_cpa WHERE codigo_Cco=@codigo_cco     
    
                       --INSERT INTO DetalleActividad_POA          
                       --SELECT cat.nombre_cat, 100, 0, a.fecini_acp, fecfin_acp, 0, null, 'A', GETDATE(), @usuario, null, null, a.responsable_acp, a.codigo_acp, 1    
						INSERT INTO DetalleActividad_POA (descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, fecha_reg, usuario_reg, responsable_dap, codigo_acp, requiere_pto) -- 009
						SELECT cat.nombre_cat, 100, a.fecini_acp, fecfin_acp, 'A', GETDATE(), @usuario, a.responsable_acp, a.codigo_acp, 1                          
                       FROM Actividad_POA a inner join CentroCostos cco ON (a.codigo_cco=cco.codigo_Cco)          
                        inner join CategoriaProyActividad_POA cpa ON (cpa.codigo_cap=cco.codigo_cpa)          
                        inner join CategoriaProgProy_POA cap ON (cpa.codigo_cap=cap.codigo_cap)          
                        inner join CategoriaActividad_POA cat ON (cpa.codigo_cat=cat.codigo_cat)          
                       WHERE a.codigo_acp=@codigo_acp and cpa.estado_cpa=1        
                        and cat.estado_cat=1    
                       -- 001 Capturar el ID de Actividad Poa e Insertar las Actividades         
                      END     
                    END      
                  END        
                END    
       
               --Actualiza Responsable Centro Costos                
               UPDATE CentroCostos SET codigo_Per=@responsable,descripcion_Cco=@resumen_acp, codigo_cpa=@codigo_cpa where codigo_Cco=@codigo_cco                
               UPDATE DetalleActividad_POA SET responsable_dap=@responsable WHERE codigo_acp=@codigo_acp AND estado_dap='A' AND responsable_dap=@responsable_ant            
                   
               IF @@ERROR <> 0                   
                BEGIN                           
                 SET @mensaje='0'                           
                END                                 
               ELSE                           
                BEGIN                                
                 SET @mensaje='1' -- Se Modifico                   
                END                    
              END                  
             ELSE                  
              BEGIN                  
               SET @mensaje='-4' -- Egresos de Actividad Menores a Presupuestado                  
              END                  
            END                  
           ELSE                  
            BEGIN                  
             SET @mensaje='-3' -- Centro de Costo Cuenta con Detalles de Presupuesto no se Puede Editar                  
            END                
          END              
         ELSE              
          BEGIN              
           SET @mensaje='-5' -- ACTIVDAD CREADA EN ETAPA DIFERENTE A LA DEL EJERCICIO ACTUAL              
          END                    
        END                      
      END                    
     ELSE                    
      BEGIN                    
       SET @mensaje='-2' -- PRESUPUESTO DE ACTIVIDAD MAYOR AL PRESUPUESTO DISPONIBLE DE POA                    
      END                        
    END                          
   ELSE                            
    BEGIN                          
     SET @mensaje='-1' -- rSe Encuentra Registrada Actividad con el mismo codigo_cco y codigo_ejp                          
    END                  
  END               
 ELSE              
  BEGIN              
   SET @mensaje='-6' -- ETAPA DE CIERRE NO PUEDE REGISTRAR NI EDITAR NADA              
  END              
                           
  SELECT @mensaje as Mensaje     
    
 COMMIT  TRANSACTION       
END TRY      
      
BEGIN CATCH      
 SET @mensaje='0'      
 SELECT @mensaje as Mensaje      
 ROLLBACK  TRANSACTION      
END CATCH 