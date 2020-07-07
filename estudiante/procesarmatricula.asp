<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
On Error Resume Next 

if(session("codigo_alu") <> "") then %>
<% 
'*************************************************************************************
'Declarar variables
'*************************************************************************************
Dim CursosProgramados
Dim VecesDesprobados
Dim tipomotivo
Dim descripcionmotivo
Dim mensaje
Dim acepto 

accion=Request.querystring("accion")
'codigo_mat=request.querystring("codigo_mat")
codigo_cup=request.querystring("codigo_cup")

usuario=session("Usuario_bit")
IP=session("Equipo_bit")
codigo_alu=session("codigo_alu")
tipo_cac=session("tipo_cac")
codigo_cac=session("Codigo_Cac")
codigo_pes=session("Codigo_pes")
tituloboton="Ver estado de cuenta"

Sub asignarcontrolesmatricula()
	CursosProgramados=Request.form("CursosProgramados")	
	VecesDesprobados=Request.form("VecesDesprobados")	
End Sub

'*************************************************************************************
'Retiro de asignaturas
'*************************************************************************************
if accion="retirarcursomatricula" then	    
	codigo_dma=Request.querystring("codigo_dma")
			
	Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	objmatricula.AbrirConexion
	    set rs_Agregado = objMatricula.Consultar("MAT_VerificaFechaAgregadoRetiro","FO",codigo_alu,codigo_cac,codigo_dma)		
	    
	    if(rs_Agregado("Mensaje") = "OK") then
	        mensaje = objMatricula.Ejecutar("RetirarCursoMatricula",true,"E",codigo_dma,usuario,27,"Vía Campus estudiante",null)
	    else
	        'mensaje = rs_Agregado("Mensaje")
	        set mensaje = rs_Agregado
	    end if		    		    		
		
	objmatricula.CerrarConexion
	Set objmatricula=nothing
	tituloboton="Regresar"
else
    '*************************************************************************************
    'Agregar nuevos cursos a la matricula
    '*************************************************************************************
    if(Cint(Request.form("hdtipomat")) = 0) then
        tipomotivo = "N"
    end if

    if(Cint(Request.form("hdtipomat")) = 1 or Cint(Request.form("hdtipomat")) = 2) then
        tipomotivo = "P"
    end if

    if(Cint(Request.form("hdtipomat")) = 3 or Cint(Request.form("hdtipomat")) = 4) then
        tipomotivo = "C"
    end if

    descripcionmotivo = Request.form("hdmotivo")   
    
    CreditosxMatricular = 0
    acepto = 0
    cuotas = Request.form("NroCuotas")        
    acepto = Request.Form("txtacepta")    

    'Set obj1=Server.CreateObject("PryUSAT.clsAccesoDatos")	        
    'obj1.AbrirConexion             
    'set rscondicion=obj1.consultar("MAT_BloqueoCondicionMatricula","FO", codigo_alu, codigo_cac)          
    'obj1.CerrarConexion

	asignarcontrolesmatricula				

		
	    Dim objMatricula
	    Set objMatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	    '************************ VALIDAR CREDITOS PERMITIDOS ************************															
		objMatricula.AbrirConexion							
		set rs_Creditos = objMatricula.Consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu,codigo_pes,codigo_cac)		
		set mensaje = objMatricula.Consultar("MAT_ValidacionMatricula","FO",codigo_alu,codigo_cac,cursosprogramados, rs_Creditos("CreditoMaximoMatricula"))								
		
								
		If (mensaje.recordcount = 0) then			    		        		        			    		    
	        '************************ primero realizar la validacion de cruces de horario *****************									    
		    'SET rs_cruceshorario = Server.CreateObject("ADODB.Resultset")		    		    
		    set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)		    		    		    
		        'response.Write "sp_validarcrucesmatriculaweb " & codigo_alu & ", " & codigo_pes & ", " & codigo_cac  & ", " & cursosprogramados		    		    
		        if rs_cruceshorario.recordcount > 0 then
		        'if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false then				        				
				    'response.Write "<BR/>codigo_alu:"& codigo_alu & "<br/>codigo_pes:" & codigo_pes & "<br/>codigo_cac:" & codigo_cac & "<br/>cursosprogramados:" & cursosprogramados & "<br/>tipoAccion:M"								
			        response.redirect("frmcruces.asp?codigo_alu=" & codigo_alu & "&codigo_pes=" & codigo_pes & "&codigo_cac=" & codigo_cac & "&cursosprogramados=" & cursosprogramados & "&tipoAccion=M")
		        else
			        '*************************************************************************************
			        'Grabar cabezera y detalle de matrícula, luego generar pago
			        '*************************************************************************************   				       			    			
			        'pasar = objMatricula.Ejecutar("VerificarCursoRepetido",True,codigo_alu,CursosProgramados,VecesDesprobados,0)			    
			        
			        if(tipo_cac = "E") then					            	            		                	                                    
		                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1",True,"E",codigo_alu,codigo_pes,codigo_cac,"P","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 1,null)    
			                tituloboton = "Regresar"			            			        		                
			        else
			            if(tipomotivo = "P" or tipomotivo = "C") then			        
			                if(acepto = 0) then
			                    mensaje = "location.href='frmmatricula.asp'|Su matricula no fue registrada.<br/>Debe aceptar su carta de compromiso"
			                    tituloboton = "Regresar"
			                else
			                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1",True,"E",codigo_alu,codigo_pes,codigo_cac,"P","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 1,null)    
			                    tituloboton = "Regresar"			            
			                end if
			            else
			                if(tipomotivo = "N") then			        
			                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1",True,"E",codigo_alu,codigo_pes,codigo_cac,"P","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 0, null)    
			                    tituloboton = "Regresar"			            
			                end if    
			            end if
			        end if
			        
			        
    			    
			        'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1",True,"E",codigo_alu,codigo_pes,codigo_cac,"P","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, acepto,null)
    			    
			        'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"E",codigo_alu,codigo_pes,codigo_cac,"P","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, null)
		   	        'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, codigo_alu, 0, codigo_cac 				    
    				
		            tituloboton="Regresar"			
		        end if	
		    'end if		    
	    end if
		    
	    objMatricula.CerrarConexion	    
		Set objMatricula = nothing	
		tituloboton="Regresar"	
end if

If Err.Number<>0 then
    response.Write "Error al realizar la matricula: " & Err.description 
    'session("pagerror")="estudiante/procesarmatricula.asp"
    'session("nroerror")=err.number
    'session("descripcionerror")=err.description        
	'response.write("<script>top.location.href='../error.asp'</script>")
End If

%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Resultados de Matrícula</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css" />
<style type="text/css">
.nvomensaje {
	border: 1px solid #008080;
	font-size: 14px;
	font-weight: bold;
	background-color: #FBF9C8;
}
.nvomensaje2 {
	border: 1px solid #008080;
	font-size: 14px;
	font-weight: bold;
	background-color: #FBF9C8;
	color: #CC3333;;
}
</style>
</head>
<body style="background-color: #EAEAEA">
<%
if instr(mensaje,"|") then
mensaje=split(mensaje,"|")
'if mensaje(0)="" or isnull(mensaje(0))=true then mensaje(0)="location.href='cargando.asp?pagina=../librerianet/academico/adminestadocuenta.aspx?id=" & session("codigo_Usu")
%>
<table style="height:60%" width="50%" cellpadding="4" align="center" id="table1">
	<tr style="height:50%">
		<td align="center" class="nvomensaje"><%=mensaje(1)%></td>		
	</tr>
	<tr>
	<td align="center" class="nvomensaje2">Debes efectuar el pago por matr&iaccute;cula hasta el 16 de Marzo.</td>
	</tr>
	<tr style="height:5%">
		<td align="center">
		<input style="width:170px" class="usatBuscar" name="cmdRegresar" type="button" value="<%=tituloboton%>" onclick="<%=mensaje(0)%>" />
		</td>
	</tr>
</table>
<% end if %>
</body>
</html>
<% end if  %>
