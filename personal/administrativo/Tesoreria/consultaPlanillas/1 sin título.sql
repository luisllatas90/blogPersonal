


select *  from  sysobjects where xtype ='P' order by crdate desc


sp_helptext spConsultarPlanilla
--select * from deudapagar  
CREATE procedure dbo.spConsultarPlanilla  
@tipo varchar(5), @param1 varchar(10),@param2 varchar(10) , @param3 varchar(10), @param4 varchar(10)  
as  
 begin  
  if @tipo='1'  
   begin  
    select dbo.DETALLEPlanilla.codigo_dplla , dbo.conceptosplanilla.descripcion_cplla ,dbo.detalledeudapagar.codigo_ddp  , 'SOLES' AS descripcion_tip,  dbo.detalleplanilla.monto , dbo.vst_cliente.nombres   from  dbo.planilla inner join  
      dbo.DETALLEPlanilla on dbo.planilla.codigo_plla = dbo.detalleplanilla.codigo_plla inner join  
      dbo.conceptosplanilla  on dbo.detalleplanilla.codigo_cplla=dbo.conceptosplanilla.codigo_cplla inner join  
      dbo.vst_cliente on dbo.detalleplanilla.codigo_per=dbo.vst_cliente.codigo_ori and dbo.vst_cliente.tipo_cli='PE'  
      left join dbo.deudapagar on dbo.deudapagar.codigo_dplla=dbo.DETALLEPlanilla.codigo_dplla  
      left join dbo.detalledeudapagar on dbo.detalledeudapagar.codigo_dpp=dbo.deudapagar.codigo_dpp  
       where dbo.planilla.codigo_tplla =@param1 and year(fechaini_plla)=@param2 and month(fechaini_plla)=@param3 and dbo.conceptosplanilla.codigo_cplla = 142  
        and dbo.detalledeudapagar.estado_ddp<>'A'  
      
   end  
 end  
  
  
