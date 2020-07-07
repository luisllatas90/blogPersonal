alter table DetalleActividad_POA add requiere_pto integer default 1;
go


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


GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to usuariogeneral
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IusrReporting
GRANT EXECUTE ON [dbo].[POA_InsertaDetalleActividad] to IUsrVirtualSistema
GO    