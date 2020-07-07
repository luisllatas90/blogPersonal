<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../asignarvalores.asp"-->
<!--#include file="../../../../clsmensajes.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../../../../tiempofinalizado.asp"

'On Error resume next

accion=Request.querystring("accion")
codigouniver_alu=request.querystring("codigouniver_alu")
codigo_mat=request.querystring("codigo_mat")
codigo_cup=request.querystring("codigo_cup")
codigo_per=request.querystring("codigo_per")
nombre_per=request.querystring("nombre_per")
codigo_cac=request.querystring("codigo_cac")
codigo_dma=request.querystring("codigo_dma")
 
function MensajeConfirmacion(ByVal mensaje)
	if err.number>0 then
		mensaje="Ha ocurrido un error al procesar la información"
	end if
	response.write "<script>alert('" & mensaje & "');window.opener.location.reload();window.close()</script>"
end function


		if accion="buscarestudiante" then
			Set ObjEstudiante= Server.CreateObject("PryUSAT.clsDatAplicacion")
				Set rsEstudiante=ObjEstudiante.Validar("RS","E",codigouniver_alu,"")
			Set ObjEstudiante=nothing
	
			If rsEstudiante.recordcount=0 then
				response.write MensajeCliente("1","history.back(-1)")
			else
				codigo_alu=rsEstudiante("codigo_alu")
				codigoUniver_alu=rsEstudiante("codigouniver_alu")
				NombreEst=rsEstudiante("alumno")
				CicloActual=rsEstudiante("cicloActual_Alu")
				nombre_cpf=rsEstudiante("nombre_cpf")
				nombre_fac=rsEstudiante("nombre_fac")
				Codigo_Pes=rsEstudiante("codigo_pes")
				Descripcion_pes=rsEstudiante("descripcion_pes")
				TipoPension=rsEstudiante("tipopension_Alu")
				PrecioCredito=rsEstudiante("preciocreditoAct_Alu")
				MonedaPrecioCredito=rsEstudiante("monedapreccred_Alu")
				totalCredAprobados=rsEstudiante("totalCredAprobados")
				codigo_cpf=rsEstudiante("codigo_cpf")
			
				response.redirect "../consultaprivada/certificado.asp?codigo_alu=" & codigo_alu & "&codigouniver_alu=" & codigouniver_alu & "&nombreest=" & nombreest & "&cicloactual=" & cicloactual & "&nombre_cpf=" & nombre_cpf & "&codigo_pes=" & codigo_pes & "&nombre_fac=" & nombre_fac & "&codigo_cpf=" & codigo_cpf
			End if
			Set rsEstudiante=nothing
		end if

		if accion="agregarnotas" then
		    matriculados=request.form("hdtotal")
		    Dim sw 
		    sw = 0
		    set objRec=Server.createObject("PryUSAT.clsAccesoDatos")
	            objRec.AbrirConexion
		            Set rsRec=objRec.Consultar("ACAD_RetornaComplementario","FO", codigo_cup)		
		            'Si no retorna registros NO es examen de recuperacion
		            if not(rsRec.BOF and rsRec.EOF) then				    		    
		                sw = 1  'Es examen de recuperacion
            		    for i=0 to matriculados
            		        notafinal_dma=request.form("txtnotafinal_dma" & i)
            		        if notafinal_dma="" then notafinal_dma=0
            		        if(notafinal_dma > 14) then
            		            sw = 2 'Tiene notas superiores a 14
            		        end if
            		    next 
		            end if		
	            objRec.CerrarConexion
            Set objRec=nothing
		    
		    if(sw = 2) then
		        mensaje="No puede existir notas superiores a 14 en un examen de recuperacion"				
		        pagina="location.href='lstalumnosmatriculados.asp?codigo_cup=" & codigo_cup & "&codigo_cac=" & codigo_cac & "&nivel=" & request.querystring("nivel") &"'"
		        response.write "<Script>alert('" & mensaje & "');" & pagina & "</Script>"		        
		    else
		        nivel=request.querystring("nivel")
			    notaminima_cac=request.form("hdnotaminima_cac")
			    matriculados=request.form("hdtotal")
    			
			    Set objNotas= Server.CreateObject("PryUSAT.clsAccesoDatos")
    			
			    objNotas.AbrirConexion
			    for i=0 to matriculados
				    condicion_dma="D"
				    codigo_dma=request.form("hdcodigo_dma" & i)
				    notafinal_dma=request.form("txtnotafinal_dma" & i)

				    if codigo_dma<>"" then
					    if notafinal_dma="" then notafinal_dma=0
    				
					    if cdbl(notafinal_dma)>=cdbl(notaminima_cac) then
						    condicion_dma="A"
					    end if
					    'Call objNotas.Ejecutar("Agregarnotas",false,codigo_dma,notafinal_dma,condicion_dma)
					    call objNotas.Ejecutar("ActualizarNotaEstudiante",false,0,codigo_dma,notafinal_dma,condicion_dma,codigo_per,"",null)
					    'end if
				    end if
			    Next

			    if (Err.number=0) then
				    'Actualizar Registro como llenado "R"
				    call objNotas.Ejecutar("MAT_CorregirEstadoNota_CUP",False,"I",codigo_cup)
				    objNotas.CerrarConexion
				    mensaje="Se han guardado correctamente los datos"
				    if nivel=0 then
					    mensaje=mensaje & "<br><br>Acuda a la Oficina de EVALUACION Y REGISTROS a firmar el Registro de Notas correspondiente"
				    end if
			    else
				    objNotas.CancelarConexionTrans
				    mensaje="Ha ocurrido un error al grabar el registro de notas<br><br>Consultar a: desarrollosistemas@usat.edu.pe"				
			    end if
			    Set objNotas=nothing
    			
			    pagina="location.href='../administrar/todocurso.asp?codigo_per=" & codigo_per & "&codigo_cac=" & codigo_cac & "'"
    			
			    if nivel=0 then
				    response.write "<h3>" & mensaje & "</h3>"
			    else 'if session("Usuario_bit")<>"USAT\jaraujo" then
				    response.write "<script>alert('" & mensaje & "');" & pagina & "</script>"
			    end if
		    end if
		    
		    
			
		end if
				
		if accion="modificarnota" then
			codigo_aut=request.querystring("codigo_aut")
			notafinal_bin=request.querystring("notafinal_bin")
			notaminima_cac=request.querystring("notaminima_cac")
			motivo_bin=request.querystring("motivo_bin")
			condicion_bin="D"

			if cdbl(notafinal_bin)>=cdbl(notaminima_cac) then
				condicion_bin="A"
			end if
		
			Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
				mensaje=Obj.Ejecutar("ActualizarNotaEstudiante",true,codigo_aut,codigo_dma,notafinal_bin,condicion_bin,session("codigo_usu"),motivo_bin,null)
				obj.CerrarConexion
			Set Obj=nothing

			MensajeConfirmacion mensaje
		end if
		
		if accion="AutorizarRegNotas" then
			modo=request.querystring("modo")
			fechaini_aut=request.querystring("fechaini_aut")
			fechafin_aut=request.querystring("fechafin_aut")
			motivo_aut=request.querystring("motivo_aut")
			
			if fechaini_aut="" then fechaini_aut=date
			if fechafin_aut="" then fechafin_aut=date
			if motivo_aut="" then motivo_aut="-No definido-"
			
			Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
				mensaje=Obj.Ejecutar("AutorizarRegNotas",true,modo,fechaini_aut,fechafin_aut,session("codigo_usu"),codigo_cup,motivo_aut,null)
				obj.CerrarConexion
			Set Obj=nothing
						
			MensajeConfirmacion mensaje
		end if
%>