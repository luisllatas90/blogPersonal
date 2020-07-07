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
	    set rs_Agregado = objMatricula.Consultar("MAT_VerificaFechaAgregadoRetiro_2015","FO",codigo_alu,codigo_cac,codigo_dma)		
	    
	    if(rs_Agregado("Mensaje") = "OK") then
	        mensaje = objMatricula.Ejecutar("RetirarCursoMatricula_2015",true,"E",codigo_dma,usuario,27,"Vía Campus estudiante",null)
	    else	      
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
    

	asignarcontrolesmatricula				

		
	    Dim objMatricula
	    Set objMatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	    '************************ VALIDAR CREDITOS PERMITIDOS ************************															
		objMatricula.AbrirConexion							
		set rs_Creditos = objMatricula.Consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu,codigo_pes,codigo_cac)		
		set mensaje = objMatricula.Consultar("MAT_ValidacionMatricula_2015","FO",codigo_alu,codigo_cac,cursosprogramados, rs_Creditos("CreditoMaximoMatricula"))								
		objMatricula.CerrarConexion
								
		If (mensaje.recordcount = 0) then			    		        		        			    		    
	        '************************ primero realizar la validacion de cruces de horario *****************									 
	        objMatricula.AbrirConexion	
		    set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)		    		    		    
			objMatricula.CerrarConexion
		    	'response.Write "sp_validarcrucesmatriculaweb " & codigo_alu & ", " & codigo_pes & ", " & codigo_cac  & ", " & cursosprogramados
		    	dim filas 
		    	filas = 0
		    	filas = rs_cruceshorario.recordcount
		      


		        if filas > 0 then
		        
		             response.redirect("frmcruces2015.asp?codigo_alu=" & codigo_alu & "&codigo_pes=" & codigo_pes & "&codigo_cac=" & codigo_cac & "&cursosprogramados=" & cursosprogramados & "&tipoAccion=M")
		        else
			        '*************************************************************************************
			        'Grabar cabezera y detalle de matrícula, luego generar pago
			        '*************************************************************************************   				       			

			        if(tipo_cac = "E") then					            	            		                	   
			                objMatricula.AbrirConexion	                      
		                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"E",codigo_alu,codigo_pes,codigo_cac,"N","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 1,null)    
		                    objMatricula.CerrarConexion
			                tituloboton = "Regresar"			            			        		                
			        else
			                                 
			            if(tipomotivo = "P" or tipomotivo = "C") then			        
			            	
			                if(acepto = 0) then
			                    mensaje = "location.href='frmmatricula2015.asp'|Su matricula no fue registrada.<br/>Debe aceptar su carta de compromiso"
			                    tituloboton = "Regresar"
			                else
			      				objMatricula.AbrirConexion	                      
			                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"E",codigo_alu,codigo_pes,codigo_cac,"N","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 1,null)    
			                    objMatricula.CerrarConexion
			                    tituloboton = "Regresar"			            
			                end if
			            else
			      
			                if(tipomotivo = "N") then	
			                	objMatricula.AbrirConexion	                      
			                    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"E",codigo_alu,codigo_pes,codigo_cac,"N","Matrícula Web",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null, null, cuotas, tipomotivo, descripcionmotivo, 0, null)    
			                    objMatricula.CerrarConexion
			                    tituloboton = "Regresar"			            
			                end if    
			            end if
			        end if
		            tituloboton="Regresar"			
		        end if	
		    'end if		    
	    end if
		    
	    'objMatricula.CerrarConexion	    
		Set objMatricula = nothing	
		tituloboton="Regresar"	
end if

If Err.Number<>0 then
   
    response.Write "Error # " & Str(Err.Number) & " was generated by " & Err.Source & ControlChars.CrLf & Err.Description
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
	font-size: 11px;
	font-weight: normal;
	color: black;
}
</style>

</head>
<body style="background-color: #EAEAEA">
<%
if instr(mensaje,"|") then
mensaje=split(mensaje,"|")

%>
<table style="height:60%" width="50%" cellpadding="4" align="center" id="table1">
	<tr style="height:50%">
		<td align="center" class="nvomensaje">
			<img src="images/<%=mensaje(3)%>.png">
			<br/>
			<span style="color:<%=mensaje(2)%>"><%=mensaje(1)%></span>
			<br/><br/>
			<span class="nvomensaje2">Recuerde que los agregados, retiros y cambios de grupo de asignaturas, estan disponible &uacute;nicamente los d&iacute;as <b><u>13 y 14 de Agosto.</u></b></span>			
		</td>		
	</tr>	
	<tr style="height:5%">
		<td align="center">
		<input style="width:170px" class="usatBuscar" name="cmdRegresar" type="button" value="<%=tituloboton%>" onclick="<%=mensaje(0)%>" />
		</td>
	</tr>
</table>
<% end if %>
<script type="text/javascript" language="JavaScript" src="private/analyticsEstudiante.js?x=1"></script>
</body>
</html>
<% end if  %>
