SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/*exec dbo.sp_verdocumentoemitidos @tipo = 'NC', @condicion1 = '80', @condicion2 = '', @condicion3 = ''

select * from  documentodeudacobrar where codigo_docd=78


select* from deudacobrar where codigo_docd=78
select* from detalledeudacobrar where codigo_dpc=836


select * from  dbo.vstdocumentodetalledeudacobrar where codigo_docd=79

*/

ALTER  procedure dbo.sp_verdocumentoemitidos
@tipo varchar(15),@condicion1 varchar(100),@condicion2 varchar(100),@condicion3 varchar(100)
as
	begin
		declare @codigo_cli	int ,@codigo_buscar	int,	@descripcion_ter	varchar(100)
		if @tipo='DIBSN' /*documento de Ingreso por serie y número */
			begin
				select * from vst_veringreso 
						where 	codigo_tdo	=	convert (int,@condicion1) and 
							seriedoc_ing	=	convert (int,@condicion2) and 
							numerodoc_ing	=	convert (int,@condicion3) and estado_ing='R'
			end
		if @tipo='DIBCOD' /*documento de Ingreso por serie y número */
			begin
				select * from vst_veringreso 
						where 	codigo_ing =@condicion1
			end
		if @tipo='DIPI' /*documento pendientes de impresión*/
			begin
				if @condicion1<>11
					begin
						select * from vst_veringreso 
								where 	estadoimpresion_ing=0 and 
									(fecha_ing between convert (datetime,@condicion2) and  convert (datetime,@condicion3)) and  /*no impresos*/
									codigo_Tdo=@condicion1 and estado_ing='R'
					end
				else
					begin
						select * from vst_veringreso 
								where 	estadoimpresion_ing=0 and 
									(fecha_ing between convert (datetime,@condicion2) and  convert (datetime,@condicion3))/*no impresos*/
									and estado_ing='R'
									

					end
			end
		if @tipo='DIBSNIA' /*documento de Ingreso por serie y número, incluir anulados */
			begin
				select * from vst_veringreso 
						where 	codigo_tdo	=	convert (int,@condicion1) and 
							seriedoc_ing	=	convert (int,@condicion2) and 
							numerodoc_ing	=	convert (int,@condicion3) 
			end
		if @tipo='DIBRF' /*documento de Ingreso por rango de fechas */
			begin

				if @condicion1<>11
					begin
						select * from vst_veringreso 
							where 	codigo_tdo	=	convert (int,@condicion1) and 
								(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
								) and estado_ing='R'
					end 
				else
					begin	
						select * from vst_veringreso 
							where 	
								(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
								) and estado_ing='R'
							
					end
					
			end

		if @tipo='DIBRFIA' /*documento de Ingreso por rango de fechas, incluir los anulados */
			begin

				if @condicion1<>11
					begin
						select * from vst_veringreso 
							where 	codigo_tdo	=	convert (int,@condicion1) and 
								(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
								) 
					end 
				else
					begin	
						select * from vst_veringreso 
							where 	
								(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
								) 
							
					end
					
			end
		
		if @tipo='DIBTC' /*documento de Ingreso por tipo cliente y rango de fechas*/
			begin
				select * from vst_veringreso 
						where 	codigo_tcl	=	convert (int,@condicion1) and 
							(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
							) and estado_ing='R' order by descripcion_tip


			end
		if @tipo='DIBTCIA' /*documento de Ingreso por tipo cliente y rango de fechas incluir a los egresados*/
			begin
				select * from vst_veringreso 
						where 	codigo_tcl	=	convert (int,@condicion1) and 
							(fecha_ing	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3)
							)  order by descripcion_tip


			end
		if @tipo='DICCO' /*documento de ingreso consukltado por código*/
			begin
				select * from vst_veringreso  where codigo_ing=@condicion1
			end

		if @tipo='DDI' /*Detalle de Documento de Ingreso*/
			begin
				select * from vstdetalleingreso   where codigo_ing=@condicion1 and estado_ing='R'
			end
		if @tipo='DDIIA' /*Detalle de Documento de Ingreso incluyendo anulados*/
			begin
				select * from vstdetalleingreso   where codigo_ing=@condicion1 
			end

		if @tipo='DEBSN'
			begin
				select * from vstegreso 
					where 	codigo_Tdo=convert (int ,@condicion1) and 
						seriedoc_egr=convert (int ,@condicion2) and
						numerodoc_egr=convert (int ,@condicion3) and estado_egr ='R'
			end
		if @tipo='DEBSNIA'
			begin
				select * from vstegreso 
					where 	codigo_Tdo=convert (int ,@condicion1) and 
						seriedoc_egr=convert (int ,@condicion2) and
						numerodoc_egr=convert (int ,@condicion3) 			
			end

		if @tipo='DEBRF' /*documento de egreso por rango de fechas, no incluir los anulados */
			begin

				if convert (int,@condicion1)<>11
					begin
						select * from vstegreso

								where 	codigo_tdo	=	convert (int,@condicion1) and 
									(fechagen_egr	between 
											convert (datetime,@condicion2) and 
											convert (datetime,@condicion3)
									) and estado_egr='R'
					end
				else
					begin
							select * from vstegreso
								where 	
									(fechagen_egr	between 
											convert (datetime,@condicion2) and 
											convert (datetime,@condicion3)
									) and estado_egr='R'
						
					end
			end

		if @tipo='DEBRFIA' /*documento de egreso por rango de fechas, INCLUIR los anulados */
			begin

				if convert (int,@condicion1)<>11
					begin
						select * from vstegreso

								where 	codigo_tdo	=	convert (int,@condicion1) and 
									(fechagen_egr	between 
											convert (datetime,@condicion2) and 
											convert (datetime,@condicion3)
									) 
					end
				else
					begin
							select * from vstegreso
								where 	
									(fechagen_egr	between 
											convert (datetime,@condicion2) and 
											convert (datetime,@condicion3)
									) 
						
					end
			end

		if @tipo='DEBTC' /*documento de Egreso por tipo cliente y rango de fechas , solo los registrados*/
			begin
				select * from vstegreso 
						where 	codigo_tcl	=	convert (int,@condicion1) and 
							(fechagen_egr	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3) and estado_egr='R'
							)
			end
		if @tipo='DEBTCIA' /*documento de Egreso por tipo cliente y rango de fechas , solo los registrados*/
			begin
				select * from vstegreso 
						where 	codigo_tcl	=	convert (int,@condicion1) and 
							(fechagen_egr	between 
									convert (datetime,@condicion2) and 
									convert (datetime,@condicion3) 
							)
			end

		if @tipo='DECCO' /*Detalle de Documento de Egreso por código*/
			begin
				select * from vstegreso  where codigo_egr=@condicion1
			end
		if @tipo='DDE' /* detalle de documento de egreso*/
			begin
				select * from vstdetalleegreso  where codigo_egr=@condicion1
			end
		if @tipo='ECC' /* Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */
						select * , dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada 
								from vst_verdetalledeudacobrar  where codigo_cli=@codigo_cli and estado_ddc <>'A' and  tipo_Cli=@condicion2 order by descripcion_tip, fechainicio_ddc
					end
				else
					begin
						select * , dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada from vst_verdetalledeudacobrar   where codigo_Cli=@codigo_cli and estado_ddc <>'A' order by descripcion_tip , fechainicio_ddc
					end
				
			end
		if @tipo='ECCP' /* estado de cuenta de cliente (solo las deudas pendientes) Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */
						select *, dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada from vst_verdetalledeudacobrar 
								where codigo_cli=@codigo_cli and tipo_Cli=@condicion2 
										AND importe_ddc- ISNULL(importecancelado_ddc,0)   >0  and estado_ddc<>'A'
										order by descripcion_tip, fechainicio_ddc 
	
					end
				else
					begin
						select *,dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada from vst_verdetalledeudacobrar   
								where 	codigo_Cli=@codigo_cli 
										AND importe_ddc- ISNULL(importecancelado_ddc,0)   >0  and estado_ddc<>'A'
										order by descripcion_tip , fechainicio_ddc
					end				
			end
		if @tipo='ECCC' /* estado de cuenta de cliente (solo las deudas canceladas) Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */

						select *, dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada from vst_verdetalledeudacobrar  where codigo_cli=@codigo_cli and tipo_Cli=@condicion2 
								AND ISNULL(importecancelado_ddc,0) - importe_ddc >=0  and estado_ddc<>'A'
								order by descripcion_tip, fechainicio_ddc
					end
				else
					begin
						select *, dbo.func_calcularmora(codigo_ddc, getdate()) as Moracalculada from vst_verdetalledeudacobrar   where codigo_Cli=@codigo_cli 
											and ISNULL(importecancelado_ddc,0) - importe_ddc >=0  and estado_ddc<>'A'
											order by descripcion_tip , fechainicio_ddc
					end				
			end
		


		if @tipo='DIDPC' /*mostrar los detalles de ingreso por las deudas a cobrar*/
			begin
				select * from  vstdetalleingreso where codigo_ddc=@condicion1 and estado_ing='R' order by fecha_ing desc
			end
		if @tipo='LCI01' 	/*liquidación de caja ingresos por moneda por la fecha inicial*/	
			begin
				select * from  vst_veringreso 
					where 	codigo_Tip	=	@condicion1  and 
					(	convert (datetime, convert (varchar(20),fecha_ing,103))  = convert (datetime, @condicion2 )
					)			
				order by  fecha_ing asc, fechareg_ing asc
			end
		if @tipo='LCI02' 	/*liquidación de caja ingresos por moneda por rango de Fechas*/	
			begin
				select * from  vst_veringreso 
					where 	codigo_Tip	=	@condicion1  and 
					(	convert (datetime, convert (varchar(20),fecha_ing,103))  between
						@condicion2 and @condicion3
					)			
				order by  fecha_ing asc, fechareg_ing asc
			end
		if @tipo='LCI03' 	/*liquidación de caja ingresos por moneda hsta la fecha final enviada*/	
			begin
				select * from  vst_veringreso 
					where 	codigo_Tip	=	@condicion1  and 
					(	convert (datetime, convert (varchar(20),fecha_ing,103))  <=convert (datetime, @condicion2) 
					) and  estado_ing ='R'			
				order by  fecha_ing asc, fechareg_ing asc
			end

		if @tipo='DICAJ' /*Documentos de ingreso por caja de atencion y rango de fechas*/
			begin

				select @descripcion_ter = descripcion_ter from  terminal where codigo_ter=@condicion1
				select * from  vst_veringreso  
						where hostreg_ing=@descripcion_ter and 
							(	convert (datetime, convert (varchar(20),fecha_ing,103))  <=convert (datetime, @condicion2)
							)		and estado_ing ='R'	
						order by  fecha_ing asc, fechareg_ing asc
			end
		if @tipo='DICAJIA' /*Documentos de ingreso por caja de atencion y rango de fechas incluir los anulados*/
			begin

				select @descripcion_ter = descripcion_ter from  terminal where codigo_ter=@condicion1
				select * from  vst_veringreso  
						where hostreg_ing=@descripcion_ter and 
							(	convert (datetime, convert (varchar(20),fecha_ing,103))  <=convert (datetime, @condicion2)
							)		
						order by  fecha_ing asc, fechareg_ing asc
			end

		if @tipo='DECAJ' /*Documentos de egreso por caja de atencion y rango de fechas, que NO esten anulados*/
			begin
				---declare @descripcion_ter	varchar(100)
				select @descripcion_ter = descripcion_ter from  terminal where codigo_ter=@condicion1
				select * from  vstegreso
						where hostreg_egr=@descripcion_ter and 
							(	convert (datetime, convert (varchar(20),fechagen_egr,103))  between 
										convert (datetime, @condicion2) and convert (datetime, @condicion3)
							) and estado_egr='R'
						order by  fechagen_egr asc, fechareg_egr asc
			end
		if @tipo='DECAJIA' /*Documentos de egreso por caja de atencion y rango de fechas, que NO esten anulados*/
			begin
				---declare @descripcion_ter	varchar(100)
				select @descripcion_ter = descripcion_ter from  terminal where codigo_ter=@condicion1
				select * from  vstegreso
						where hostreg_egr=@descripcion_ter and 
							(	convert (datetime, convert (varchar(20),fechagen_egr,103)) between 
										convert (datetime, @condicion2) and convert (datetime, @condicion3)
							) 
						order by  fechagen_egr asc, fechareg_egr asc
			end
		IF @tipo='DDCSA'
			begin
				if  @condicion1='TO'
					begin
						select * from vst_verdetalledeudacobrar where estado_ddc<>'A' and  (isnull(importe_ddc,0)- isnull(importecancelado_ddc,0))<0	
					end
				else
					begin
						select * from vst_verdetalledeudacobrar where estado_ddc<>'A' and  (isnull(importe_ddc,0)- isnull(importecancelado_ddc,0))<0	 and tipo_cli=@condicion1
					end
			end
		if @tipo='CDT' /*consultar deudas que son transferibles (que esten en ConfiguraTransferencia ) , solo las pendientes mas no las renegociadas*/
			begin
				select vst_verdetalledeudacobrar.*, (importe_ddc - importecancelado_ddc) as Saldo_ddc, ConfiguraTransferencia.codigo_cac as CodigoCicloRubroTransferencia  from  vst_verdetalledeudacobrar  
						inner  join 
							ConfiguraTransferencia 
								on 
							vst_verdetalledeudacobrar.tipo_cli =  ConfiguraTransferencia.codigo_itc and vst_verdetalledeudacobrar.codigo_rub =  ConfiguraTransferencia.codigo_rub
					where estado_ddc='P' and codigo_tcl=@condicion1 and codigo_tip=@condicion2 and (importe_ddc- importecancelado_ddc)>0
			
			end
		if @tipo='CGRCLI' /*gastos a rendir por cliente*/
			begin
				select * from  vstdetalleegreso where exigirrendicion_deg =1 and estado_egr ='R' and codigo_tcl =@condicion1 
			end
		if @tipo='CGRCLI2' /*gastos a rendir por cliente*/
			begin
				select * from  vstdetalleegreso where exigirrendicion_deg =1 and estado_egr ='R' and codigo_tcl =@condicion1 and saldorendir_Deg<0
			end

		if @tipo='CGRRFCLI' /*gastos a rendir por cliente*/
			begin
				select * from  vstdetalleegreso where exigirrendicion_deg =1 and estado_egr ='R' and codigo_tcl =@condicion1 and
						(fechagen_egr between @condicion2 and @condicion3)
			end
		if @tipo ='VDREND'
			begin
				declare  @codigo_deg int, @NumeracionAnual_rend int
---select* from  rendicion
				select @codigo_deg = codigo_deg , @NumeracionAnual_rend =  NumeracionAnual_rend from dbo.rendicion where codigo_rend=@condicion1 
				select *, @NumeracionAnual_rend as NumeracionAnual_rend from  vstdetalleegreso where codigo_deg=@codigo_deg
			end
		if @tipo='NC01' /*documentos emitidos (Nota de cargo)*/
			begin
				select * from  dbo.vstdocumentodetalledeudacobrar where codigo_docd=@condicion1 
			end
		--- nuevas consultas 22012008
		---------------------------------- deuda a pagar
		if @tipo='ECCPAGAR' /* Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */
						select *  from vst_verdetalledeudaPAGAR  where codigo_cli=@codigo_cli and tipo_Cli=@condicion2 order by descripcion_tip, fechainicio_ddp
					end
				else
					begin
						select * from vst_verdetalledeudaPAGAR   where codigo_Cli=@codigo_cli order by descripcion_tip , fechainicio_ddp
					end
				
			end
		if @tipo='ECCPPAGAR' /* estado de cuenta de cliente (solo las deudas pendientes) Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */
						select *  from vst_verdetalledeudaPAGAR 
								where codigo_cli=@codigo_cli and tipo_Cli=@condicion2 
										AND importe_ddp- ISNULL(importecancelado_ddp,0)   >0  and estado_ddp<>'A'
										order by descripcion_tip, fechainicio_ddp
 
	
					end
				else
					begin
						select * from vst_verdetalledeudaPAGAR   
								where 	codigo_Cli=@codigo_cli 
										AND importe_ddp- ISNULL(importecancelado_ddp,0)   >0  and estado_ddp<>'A'
										order by descripcion_tip , fechainicio_ddp
					end				
			end
		if @tipo='ECCCPAGAR' /* estado de cuenta de cliente (solo las deudas canceladas) Utilizado en el formulario de estado 
				de cuenta del Cliente*/
			begin
				
				select @codigo_Cli	= codigo_Cli from  vst_cliente where codigo_tcl=@condicion1					
				if @condicion2<>'TO'	 /*alumno como */
					begin
						/* extraer el codigo del tipo de  cliente */

						select * from vst_verdetalledeudaPAGAR  where codigo_cli=@codigo_cli and tipo_Cli=@condicion2 
								AND ISNULL(importecancelado_ddp,0) - importe_ddp >=0  and estado_ddp<>'A'
								order by descripcion_tip, fechainicio_ddp
					end
				else
					begin
						select * from vst_verdetalledeudaPAGAR   where codigo_Cli=@codigo_cli 
											and ISNULL(importecancelado_ddp,0) - importe_ddp >=0  and estado_ddp<>'A'
											order by descripcion_tip , fechainicio_ddp
					end				
			end
		if @tipo='PL1'
			begin
				   select tipocuenta_per , dbo.datospersonal.nrocuenta_per  , importe_ddp , dbo.personal.apellidopat_per +  ' ' + dbo.personal.apellidomat_per + ' ' + nombres_per as Personal,  
					    dbo.detalledeudapagar.codigo_ddp ,  dbo.rubro.descripcion_Rub,dbo.tipo_cambio.descripcion_tip , dbo.detalledeudapagar.importe_ddp as importecancelado_ddp , dbo.personal.apellidopat_per , dbo.personal.apellidomat_per , nombres_per   
					     from  dbo.datospersonal     
					      inner join dbo.personal on dbo.datospersonal.codigo_per =dbo.personal.codigo_per  
					      inner join dbo.tipocliente  on  dbo.datospersonal.codigo_per= dbo.tipocliente.codigo_ori and dbo.tipocliente.tipo_Cli='PE'  
					      inner join dbo.deudapagar   on dbo.tipocliente.codigo_tcl =deudapagar.codigo_tcl  
					      inner join dbo.tipo_cambio  on dbo.deudapagar.codigo_tip=dbo.tipo_cambio.codigo_tip  
					      inner join dbo.rubro on dbo.rubro.codigo_rub =dbo.deudapagar.codigo_Rub  
					      inner join dbo.detalledeudapagar on dbo.deudapagar.codigo_dpp =dbo.detalledeudapagar.codigo_Dpp  
					      where codigo_Efi =2 and dbo.deudapagar.codigo_plla=@condicion1 and dbo.rubro.codigo_Rub  = @condicion2 and dbo.detalledeudapagar.estado_ddp <>'A'  /*considerar los nros de cuenta del banco de credito*/  
					       ---and  isnull(dbo.datospersonal.nrocuenta_per,'')<>''  
					  
					    /*select * from  vst_verdetalledeudapagar where estado_ddp<>'A' and importe_ddp<=importecancelado_ddp  
					      and  codigo_plla=@condicion1 and codigo_Rub=@condicion2*/  
			


			end
		if @tipo='MP1' /* mostrar los movimientos de pago activos, pantalla de egresosxcliente*/
			begin
				select * from vstdetalleegreso  where codigo_ddp=@condicion1 and estado_Egr='R'
			end
		if @tipo='2' /*consultar cargos generados x compra*/
			begin
				select * from vst_verdetalledeudapagar where estado_dpp<>'A' and codigo_rco=@condicion1
			end
		if @tipo='3' /*ver cargos a pagar para un cliente , utilizado en la pantalla de asociar documento de logistica a cargo*/
			begin
				select * from vst_verdetalledeudapagar where codigo_tcl =@condicion1 and estado_ddp <>'A' 
			end
		if @tipo='4' /*ver cargos a pagar para un cliente , utilizado en la pantalla de asociar documento de logistica a cargo*/
			begin
				select * from vst_verdetalledeudapagar where codigo_ddp =@condicion1 and estado_ddp <>'A' 
			end
		if @tipo='5' /*ver cargos a pagar para un cliente , utilizado en la pantalla de asociar documento de logistica a detalle de egreso*/
			begin
				select * from vstdetalleegreso where codigo_Deg =@condicion1
			end
		if @tipo='6' /*ver los detalles de ingreso para un codigo_ddc de documento de ingreso activo*/
			begin
				select * from vstdetalleingreso where codigo_Ddc =@condicion1 and estado_ing ='R'
			end
		if @tipo='7' /*para el pago de la planillas de CTS*/
			begin
				select* from detalledeudapagar inner join	
						deudapagar on detalledeudapagar.codigo_dpp = deudapagar.codigo_dpp
					inner join (select * from tipocliente where tipo_cli ='PE') tipocliente on deudapagar.codigo_tcl=tipocliente.codigo_tcl
					inner join personal on tipocliente.codigo_ori= personal.codigo_per ---and tipocliente.tipo_cli='PE'
					inner join datospersonal on personal.codigo_per =datospersonal.codigo_per 
					inner join dbo.rubro on deudapagar.codigo_rub = dbo.rubro.codigo_rub
					inner join dbo.tipo_cambio on dbo.tipo_cambio.codigo_tip= deudapagar.codigo_tip
						where codigo_plla=@condicion1 and estado_ddp<>'A' and deudapagar.codigo_rub =@condicion2  and codigoEfi_cts ='02'order by personal.apellidopat_per , personal.apellidomat_per

			end
		/*verificar pagos pendientes para hoy*/
		if @tipo='8'
			begin
				select count(*) as cantidad from detalledeudapagar where 	(convert (varchar(20), getdate(),103) >= fechainicio_ddp and  convert (varchar(20), getdate(),103) <= fechvenc_ddp) and 
												estado_ddp<>'A' and 
												(importe_ddp - importecancelado_ddp)>0
				
			end
		if @tipo='9'
			begin
				select * from vst_verdetalledeudapagar where (convert (varchar(20), getdate(),103) >= fechainicio_ddp  and convert (varchar(20), getdate(),103) <= fechvenc_ddp) and 
										estado_ddp<>'A' and (importe_ddp - importecancelado_ddp)>0 order by fechvenc_ddp desc
				
			end
		if @tipo='10'
			begin
				select* from  dbo.vstdetalleingreso inner join
					(select codigo_ddc from dbo.deudacobrar inner join dbo.detalledeudacobrar 
							on deudacobrar.codigo_dpc = detalledeudacobrar.codigo_dpc  
								where 	dbo.deudacobrar.codigo_tcl =@condicion1  and 
									(dbo.detalledeudacobrar.importe_ddc - dbo.detalledeudacobrar.importecancelado_ddc )<0 and 
									dbo.detalledeudacobrar.estado_ddc <>'A'
					) x on dbo.vstdetalleingreso.codigo_ddc = x.codigo_ddc and estado_ing<>'A' -- no este anulado
			end
		if @tipo='11' /*consultar cargos generados x compra*/
			begin
				select * from vst_verdetalledeudapagar where estado_dpp<>'A' and codigo_dplla=@condicion1
			end


	end


/*
alter table detalleegreso
add montorendido_deg numeric(20,2) default 0 not null

alter table detalleegreso
add montodevuelto_deg numeric(20,2) default 0 not null

alter table detalleegreso
add saldorendir_deg numeric(20,2) default 0 not null

*/




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

