/*      
* Fecha Mod: 24/10/2017 - 11:17:00am      
* Modificado por: moises.vilchez      
* Descripción: Deriva Propuestas a Rectorado  
 001 24/04/2017 moises.vilchez No Debe Filtrar al Secretario     
*/     
  
ALTER PROCEDURE [dbo].[PRP_DerivarPropuesta_v1]    
@codigo_prp int              
AS              
BEGIN    
	--DECLARE @tipopropuesta int          
	--DECLARE @instancia  char(1)          

	--IF EXISTS (SELECT codigo_ipr FROM InvolucradoPropuesta where codigo_Prp=@codigo_prp and instancia_Ipr in ('A','F'))  --001 Inserta a Rectorado solo si la propuesta tiene mienbros de Consejo de Facultad y Administrativo  
	--	BEGIN    
	--		INSERT INTO InvolucradoPropuesta      
	--		--RECTORADO         
	--		select   
	--		@codigo_prp, v.codigo_Pcc, 'P','A','K','K',getdate(),null      
	--		from     
	--		ConsejoFacultad cjf inner join Consejo c ON (cjf.codigo_con=c.codigo_con)      
	--		inner join vstPersonalCentroCostos v on (cjf.Codigo_Pcc=v.codigo_Pcc)       
	--		inner join Cargo cgo ON (cgo.codigo_Cgo=cjf.codigo_cgo)         
	--		where     
	--		v.estado_Pcc=1 and     
	--		cjf.Estado_Cjf ='A'     
	--		and cjf.codigo_con = 6     
	--		and cjf.Cargo_Cjf is not null      

	--		UPDATE propuesta SET instancia_Prp = 'K', estado_Prp='A' /*, Destino_Prp='K'*/ WHERE codigo_Prp = @codigo_prp     

	--		DECLARE @codigo_Ipr int          
	--		select @codigo_Ipr=codigo_Ipr from InvolucradoPropuesta where codigo_Prp=@codigo_prp and instancia_Ipr='T'    
	--		update InvolucradoPropuesta set veredicto_Ipr='C' where codigo_Ipr=@codigo_Ipr    

	--	END  


	DECLARE @nItem INT = 0      
	SELECT
		@nItem=amp.rectorado_atp   
	FROM   
		propuesta p inner join TipoPropuesta tp ON (p.codigo_Tpr=tp.codigo_Tpr)  
		inner join AmbitoPropuestaTipoPropuesta amp ON (tp.codigo_Tpr=amp.codigo_Tpr)  
	WHERE   
		p.codigo_Prp=@codigo_prp
  
	IF @nItem = 1       
		BEGIN      
			DECLARE @pendiente INT=0      
			select
				@pendiente=COUNT(*)      
			from 
				InvolucradoPropuesta i inner join propuesta p ON (i.codigo_Prp=p.codigo_Prp)      
			where 
				i.codigo_Prp=@codigo_prp      
				and i.instancia_Ipr in ('A','F')
				and i.veredicto_Ipr='P'      
     
			IF @pendiente=0       
				BEGIN      
					IF NOT EXISTS (SELECT codigo_ipr FROM InvolucradoPropuesta ip WHERE ip.codigo_Prp=@codigo_prp AND ip.instancia_Ipr='K')      
					BEGIN      
						INSERT INTO InvolucradoPropuesta --RECTORADO  
						select       
							@codigo_prp,v.codigo_Pcc,'P','A','K','K',getdate(),null        
						from       
							ConsejoFacultad cjf inner join Consejo c ON (cjf.codigo_con=c.codigo_con)        
							inner join vstPersonalCentroCostos v on (cjf.Codigo_Pcc=v.codigo_Pcc)         
							inner join Cargo cgo ON (cgo.codigo_Cgo=cjf.codigo_cgo)           
						where       
							v.estado_Pcc=1 and cjf.Estado_Cjf ='A' and cjf.codigo_con=6 and cjf.Cargo_Cjf is not null
					END      
					UPDATE propuesta SET instancia_Prp = 'K', estado_Prp='A' WHERE codigo_Prp = @codigo_prp       
				END      
		END   
END    


--PRP_DerivarPropuesta_v1 3385 


/*
select * from Propuesta where codigo_Prp=3385
select * from InvolucradoPropuesta where codigo_Prp=3385			--15
*/