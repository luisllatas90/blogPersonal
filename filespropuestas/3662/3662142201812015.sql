/* SCRIPT DETALLE ACTIVIDAD POA */
----------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[POA_Elimina_DetalleActividad]    
@codigo_acp int,    
@codigo_dap int,    
@usuario int,    
@tipo char(1)    
    
AS    
    
BEGIN    
 if @tipo = '0'    
  BEGIN       
   update     
    DetalleActividad_POA    
   set    
    estado_dap='I',    
    usuario_mod=@usuario,    
    fecha_mod=GETDATE()    
   where     
    codigo_acp=@codigo_acp    
  END    
 else    
  BEGIN    
       
   update     
    DetalleActividad_POA     
   set     
    estado_dap='A'     
   where     
    codigo_acp=@codigo_acp    
    and codigo_dap=@codigo_dap    
  END    
        
END    
GO

GRANT EXECUTE ON [dbo].[POA_Elimina_DetalleActividad] to usuariogeneral
GRANT EXECUTE ON [dbo].[POA_Elimina_DetalleActividad] to IusrReporting
GRANT EXECUTE ON [dbo].[POA_Elimina_DetalleActividad] to IUsrVirtualSistema
GO    


----------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[POA_InsertaDetalleActividad]          
@codigo_dap int,          
@descripcion_dap varchar(200),          
@meta_dap numeric(18,2),           
@fecini_dap varchar(10),           
@fecfin_dap varchar(10),           
@estado_dap char(1),           
@usuario_reg int,           
@responsable_dap int,          
@codigo_acp int,  
@requiere_pto int          
          
AS             
-- select * from DetalleActividad_POA where codigo_acp=6      
BEGIN              
 IF NOT EXISTS(select * from DetalleActividad_POA where codigo_dap=@codigo_dap)        
  BEGIN                 
   insert into DetalleActividad_POA(descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, usuario_reg, responsable_dap, codigo_acp, requiere_pto)              
   values(@descripcion_dap, @meta_dap, @fecini_dap, @fecfin_dap, 'A', @usuario_reg, @responsable_dap, @codigo_acp, @requiere_pto)  
  END        
 ELSE        
  BEGIN         
   update DetalleActividad_POA         
   set         
    descripcion_dap=@descripcion_dap,         
    meta_dap=@meta_dap,         
    fecini_dap=@fecini_dap,         
    fecfin_dap=@fecfin_dap,         
    estado_dap=@estado_dap,         
    fecha_mod=GETDATE(),         
    usuario_mod=@usuario_reg,         
    responsable_dap=@responsable_dap,        
    codigo_acp=@codigo_acp,  
    requiere_pto=@requiere_pto  
   where codigo_dap=@codigo_dap        
        
  END                
END 
GO

GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to usuariogeneral
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IusrReporting
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IUsrVirtualSistema
GO 
----------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[POA_InsertaDetalleActividad]          
@codigo_dap int,          
@descripcion_dap varchar(200),          
@meta_dap numeric(18,2),           
@fecini_dap varchar(10),           
@fecfin_dap varchar(10),           
@estado_dap char(1),           
@usuario_reg int,           
@responsable_dap int,          
@codigo_acp int,  
@requiere_pto int          
          
AS             
-- select * from DetalleActividad_POA where codigo_acp=6      
BEGIN              
 IF NOT EXISTS(select * from DetalleActividad_POA where codigo_dap=@codigo_dap)        
  BEGIN                 
   insert into DetalleActividad_POA(descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, usuario_reg, responsable_dap, codigo_acp, requiere_pto)              
   values(@descripcion_dap, @meta_dap, @fecini_dap, @fecfin_dap, 'A', @usuario_reg, @responsable_dap, @codigo_acp, @requiere_pto)  
  END        
 ELSE        
  BEGIN         
   update DetalleActividad_POA         
   set         
    descripcion_dap=@descripcion_dap,         
    meta_dap=@meta_dap,         
    fecini_dap=@fecini_dap,         
    fecfin_dap=@fecfin_dap,         
    estado_dap=@estado_dap,         
    fecha_mod=GETDATE(),         
    usuario_mod=@usuario_reg,         
    responsable_dap=@responsable_dap,        
    codigo_acp=@codigo_acp,  
    requiere_pto=@requiere_pto  
   where codigo_dap=@codigo_dap        
        
  END                
END 
GO

GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to usuariogeneral
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IusrReporting
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IUsrVirtualSistema
GO 

----------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[POA_ListarDetalleActividad]            
@codigo_acp int=0            
            
AS            
           
BEGIN            
       
 select      
  descripcion_dap,       
  meta_dap,       
  avance_dap,      
  --TOCHAR(fecini_dap, 108),      
  CONVERT(Varchar(10), fecini_dap, 103) AS fecini_dap,        
  --fecini_dap,       
  CONVERT(Varchar(10), fecfin_dap, 103) AS fecfin_dap,        
  --fecfin_dap,       
  (p.apellidoPat_Per +' '+ p.ApellidoMat_per +' '+ p.nombres_Per) as nombreresponsable_dap,   
  DetalleActividad_POA.requiere_pto,  
  codigo_dap,      
  codigo_acp,      
  responsable_dap      
 from       
  DetalleActividad_POA inner join personal p ON (DetalleActividad_POA.responsable_dap=p.codigo_Per)      
 where       
  codigo_acp=@codigo_acp      
  and estado_dap='A'      
  
         
END 
GO

GRANT EXECUTE ON [dbo].[POA_ListarDetalleActividad] to usuariogeneral
GRANT EXECUTE ON [dbo].[POA_ListarDetalleActividad] to IusrReporting
GRANT EXECUTE ON [dbo].[POA_ListarDetalleActividad] to IUsrVirtualSistema
GO 
----------------------------------------------------------------------------------------------------------------


