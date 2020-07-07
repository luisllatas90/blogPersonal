ALTER PROCEDURE dbo.POA_ListaAsignacionPresupuestal    
@opcion int,    
@codigo_pla varchar(10),    
@ejercicio varchar(10)    
    
AS    
BEGIN    
            
IF @opcion=1 -- SIN LIMITE Y SIN CENTRO DE COSTO    
 BEGIN    
    
  select    
   DISTINCT a.codigo_poa,nombre_poa,p.apellidoPat_Per+' '+p.apellidoMat_Per+' '+p.nombres_Per as Responsable,    
   a.limite_ingreso,    
   a.limite_Egreso,    
   a.utilidad,    
   e.descripcion_Ejp,    
   a.fecha_reg,    
   a.codigo_cco,    
   cco.descripcion_Cco as nombre_cco    
  from    
   Areas_POA a    
   inner join personal p on p.codigo_Per=a.responsable_poa    
   INNER join EjercicioPresupuestal e on e.codigo_Ejp=a.codigo_ejp    
   LEFT join  asignacion_POA ASI on asi.codigo_poa=a.codigo_poa and asi.estado_asp=1    
   left outer join CentroCostos cco On (cco.codigo_cco=a.codigo_Cco)    
  WHERE    
   --((asi.codigo_poa is null) or ( a.utilidad = 0))    
   ((asi.codigo_poa is null) or ( (a.limite_ingreso + a.limite_Egreso) = 0 ))
   and a.estado_poa=1    
   and a.codigo_pla like @codigo_pla    
   and a.codigo_ejp like @ejercicio    
       
 END                
                
IF @opcion=2 -- SIN LIMITE                
 BEGIN                
    
  select         
   a.codigo_poa,nombre_poa,p.apellidoPat_Per+' '+p.apellidoMat_Per+' '+p.nombres_Per as Responsable,        
   a.limite_ingreso,         
   a.limite_Egreso,         
   a.utilidad,            
   e.descripcion_Ejp,        
   a.fecha_reg,         
   a.codigo_cco,         
   cco.descripcion_Cco as nombre_cco               
  from         
   Areas_POA a                
   inner join personal p on p.codigo_Per=a.responsable_poa                
   inner join EjercicioPresupuestal e on e.codigo_Ejp=a.codigo_ejp       
   left outer join CentroCostos cco On (cco.codigo_cco=a.codigo_Cco)         
  where         
   (a.limite_ingreso=0 and a.limite_Egreso=0)     
   and a.estado_poa=1         
   and a.codigo_pla like @codigo_pla      
   and a.codigo_ejp like  @ejercicio       
            
 END                
             
                
IF @opcion=3 -- SIN CENTRO COSTO                
 BEGIN          
       
  select         
   a.codigo_poa,        
   nombre_poa,        
   p.apellidoPat_Per+' '+p.apellidoMat_Per+' '+p.nombres_Per as Responsable,        
   a.limite_ingreso,         
   a.limite_Egreso,         
   a.utilidad,         
   e.descripcion_Ejp,        
   a.fecha_reg,          
   a.codigo_cco,         
   --(select cco.descripcion_Cco from CentroCostos cco where cco.codigo_cco=a.codigo_cco)as nombre_cco              
   cco.descripcion_Cco as nombre_cco    
  from         
   Areas_POA a                
   inner join personal p on p.codigo_Per=a.responsable_poa                
   INNER join EjercicioPresupuestal e on e.codigo_Ejp=a.codigo_ejp                
   LEFT join  asignacion_POA ASI on asi.codigo_poa=a.codigo_poa  and asi.estado_asp=1     
   left outer join CentroCostos cco On (cco.codigo_cco=a.codigo_Cco)    
  WHERE         
   (asi.codigo_poa is null)     
   and a.estado_poa=1           
   and a.codigo_pla like @codigo_pla      
   and a.codigo_ejp like  @ejercicio      
    
 END                
                
IF @opcion=4 --  ASIGNADOS (CON LIMITE PRESUPUESTAL Y CENTRO DE COSTOS)             
 BEGIN                
    
  select         
   DISTINCT a.codigo_poa,nombre_poa,p.apellidoPat_Per+' '+p.apellidoMat_Per+' '+p.nombres_Per as Responsable,        
   a.limite_ingreso,         
   a.limite_Egreso,         
   a.utilidad,           
   e.descripcion_Ejp,        
   a.fecha_reg,         
   a.codigo_cco,         
   cco.descripcion_Cco as nombre_cco    
  from         
   Areas_POA a                
   inner join personal p on p.codigo_Per=a.responsable_poa                
   INNER join EjercicioPresupuestal e on e.codigo_Ejp=a.codigo_ejp                
   LEFT join  asignacion_POA ASI on asi.codigo_poa=a.codigo_poa and asi.estado_asp=1               
   left outer join CentroCostos cco On (cco.codigo_cco=a.codigo_Cco)    
  WHERE         
   asi.codigo_poa is NOT NULL and     
   a.utilidad <> 0 and    
   a.estado_poa=1      
   and a.codigo_pla like @codigo_pla      
   and a.codigo_ejp like @ejercicio       
    
 END                
                
IF @opcion=5 -- TODOS                
 BEGIN          
    
  select         
   a.codigo_poa,        
   nombre_poa,        
   p.apellidoPat_Per+' '+p.apellidoMat_Per+' '+p.nombres_Per as Responsable,        
   a.limite_ingreso,         
   a.limite_Egreso,         
   a.utilidad,              
   e.descripcion_Ejp,        
   a.fecha_reg,         
   a.codigo_cco,         
   --(select cco.descripcion_Cco from CentroCostos cco where cco.codigo_cco=a.codigo_cco)as nombre_cco              
   cco.descripcion_Cco as nombre_cco    
  from         
   Areas_POA a                
   inner join personal p on p.codigo_Per=a.responsable_poa                
   INNER join EjercicioPresupuestal e on e.codigo_Ejp=a.codigo_ejp            
   left outer join CentroCostos cco On (cco.codigo_cco=a.codigo_Cco)    
  where         
   a.estado_poa=1         
   and a.codigo_pla like @codigo_pla      
   and a.codigo_ejp like  @ejercicio      
    
 END                
     
END   