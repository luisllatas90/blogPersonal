<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funciones.asp"-->
<%
'*************************************************************************************
'CV-USAT
'Fecha de Creación: 29/07/2006
'Autor		: Gerardo Chunga Chinguel
'Modificado : Helen Reyes
'Fecha de Modificación: 08/03/2007
'Observaciones	: Permite procesar la matrícula, agregado/retiro de asignaturas
'*************************************************************************************
on error resume next
call Enviarfin(session("codigo_usu"),"../")

'*************************************************************************************
'Declarar variables
'*************************************************************************************
Dim CursosProgramados
Dim VecesDesprobados
Dim cuotas
Dim tipomotivo
Dim descripcionmotivo
Dim objmatricula

accion=Request.querystring("accion")
codigo_mat=request.querystring("codigo_mat")
codigo_cup=request.querystring("codigo_cup")
estado_dma=request.querystring("estado_dma")
'RECIBE EL VALOR DE LA MATRICULA CONDICIONAL
'YPEREZ 27/07/2010
esMatCondicional = request.Form("esMatCondicional")
'---------------------
usuario=session("Usuario_bit")
IP=session("Equipo_bit")
codigo_alu=session("codigo_alu")
tipo_cac=session("tipo_cac")
codigo_cac=session("Codigo_Cac")
codigo_pes=session("Codigo_pes")
tituloboton="Ver estado de cuenta"
codigo_mar = Request.form("cbocodigo_mar")

%>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<%
if request.Form("txtMotivo")="" then
    Motivo = ""
else
    Motivo = request.Form("txtMotivo")
end if

if(session("tipo_Cac") = "N") then 
    cuotas = 5
else
    cuotas = 2
end if

tipomotivo = Request.form("hdmotivo")
descripcionmotivo = Request.form("hdmotivo")

Sub asignarcontrolesmatricula()	

	CursosProgramados=Request.form("CursosProgramados")
	VecesDesprobados=Request.form("VecesDesprobados")
		
End Sub

Server.ScriptTimeout=400

If Not Response.IsClientConnected Then 
  	'Resetear si se desconectó el cliente
	Shutdownid = Session.SessionID
	'Obtimizar proceso
	Shutdown(Shutdownid)
End If

'on error resume next

'*************************************************************************************
'Agregar nuevos cursos a la matricula
'*************************************************************************************
	if accion="matriculasegura" then
		asignarcontrolesmatricula
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion

		'*************************************************************************************
		'Validar cruces de horarios entre asignaturas marcadas
		'*************************************************************************************
		'set rs_cruceshorario = objMatricula.Consultar("sp_validarCrucesMatriculaWebGO","FO",codigo_alu,codigo_cac,cursosprogramados)
		set rs_cruceshorario = objMatricula.Consultar("ACAD_validaCrucesMatriculaGO","FO",codigo_alu,codigo_cac,cursosprogramados)
		if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false then
            response.Write("<script>alert('Cruce de horario')</script>")
		else		
		    '*************************************************************************************
		    'Agregar Bitacora de bloqueos, Grabar cabezera y detalle de matrícula, luego generar pago
		    '*************************************************************************************
		    'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null)
		    if codigo_mat<=0 then			    
			    objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false, "ASE", codigo_alu, codigo_cac 
		    end if 
    								
		    'mensaje = objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,1,"",cuotas,null)
		    mensaje = objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"A",codigo_alu,codigo_pes,codigo_cac,"N","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,1,"",cuotas,tipomotivo, descripcionmotivo, null) ' no vale sp - _'
    		
    		
		    'response.write ("E" & "," & codigo_alu & "," & codigo_pes & "," & codigo_cac & "," & "P" & "," & "Matrícula Web" & "," & CursosProgramados & "," & VecesDesprobados & "," &  "N" & ", " & "P" & "," & "MAT" & "," & usuario & "," & IP & "," & "A")
		    'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados			
		    objMatricula.Ejecutar "MAT_AgregarMotivoMatriculaAutorizada", false, codigo_alu, codigo_cac, lcase(Motivo)
		
		end if
		
		objMatricula.CerrarConexion
		Set objmatricula=nothing
		
		'Si es matricula condicional ejecutar pdf
		'if(tipomotivo = "C") then
			'response.Write "..\..\estudiante\separacion\frmSeparacionDefinitiva.aspx?codUniv=" & session("codigoUniver_alu")
			'\personal\academico\estudiante\separacion\frmSeparacionDefinitiva.aspx	    		        
		'end if					
	
	end if

    '*************************************************************************************
    'Agregado de asignaturas
    '*************************************************************************************
	if accion="agregarcursomatricula" then
        On Error resume Next              

		asignarcontrolesmatricula				
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
        'response.Write cursosprogramados
        '*************************************************************************************
		'Validar cruces de horarios entre asignaturas marcadas
		'*************************************************************************************


		'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)
		set rs_cruceshorario = objMatricula.Consultar("ACAD_validaCrucesMatriculaGO","FO",codigo_alu,codigo_cac,cursosprogramados)
		'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb_v2","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)
		if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false then
		    response.Write("<script>alert('Cruce de horario')</script>")
		else
		    '*************************************************************************************
		    'Agregar Bitacora de bloqueos, Grabar cabezera y detalle de matrícula, luego generar pago
		    '*************************************************************************************
			if codigo_mat<=0 then
			    objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false,"ASE", codigo_alu, codigo_cac 
			end if 
			
			'original			
			'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,1, null, cuotas, null)
			'response.Write("A, " & codigo_alu & ", " & codigo_pes & ", " & codigo_cac  & ", P, Matricula por Asesor, " & CursosProgramados & ", " & VecesDesprobados & ", " & "N,P,AGR, " & usuario & ", " & IP & "," & codigo_mar & ", '', " &  cuotas & ", " & tipomotivo & "," & descripcionmotivo & ", null")		        )			
			mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"A",codigo_alu,codigo_pes,codigo_cac,"N","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar, "", cuotas, tipomotivo, descripcionmotivo,null)
			'response.write(mensaje)
			'***Cambio			
			'response.Write ("aa"&"bb")			
			'mensaje = objMatricula.Ejecutar("AgregarMatriculaWebV2",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,1, null, cuotas,esMatCondicional,null)		        
		    '****Fin Cambio
		       
		    'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados
		    
		    objMatricula.Ejecutar "MAT_AgregarMotivoMatriculaAutorizada", false, codigo_alu, codigo_cac, lcase(Motivo)			
		end if
		objMatricula.CerrarConexion
		Set objmatricula=nothing				
		tituloboton="Regresar"		
	end if

'*************************************************************************************
'Retiro de asignaturas
'*************************************************************************************

	if accion="retirarcursomatricula" then	    
		codigo_dma=Request.querystring("codigo_dma")
		codigo_mar = Request.querystring("codigo_mar")
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion	
		mensaje=objMatricula.Ejecutar("RetirarCursoMatricula_Asesor",true,"A",codigo_dma,usuario,codigo_mar,"",null)
		objmatricula.CerrarConexion
		Set objmatricula=nothing
		tituloboton="Regresar"
		
		'response.redirect "detallematricula2015.asp"
	end if
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Resultados de Matrícula</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<style type="text/css">
.nvomensaje {
	border: 1px solid #008080;
	font-size: 14px;
	font-weight: bold;
	background-color: #FBF9C8;
}
</style>
</head>
<body style="background-color: #EAEAEA">
<%
if instr(mensaje,"|") then
mensaje=split(mensaje,"|")
'Asumir que se matrículó para que consulte el detallematrícula
session("Ultimamatricula")=session("descripcion_cac")
%>
<table height="60%" cellpadding="4" align="center" style="width: 50%" id="table1">
	<tr height="50%">
		<td align="center" class="nvomensaje"><%=mensaje(1)%></td>
		</tr>
	<tr height="5%">
		<td align="center">
		<% 
	        Dim rutacarta 
            rutacarta = ""
            if(tipoMotivo = "C" or tipoMotivo = "P") then
                'ruta = "../FrmCartaCompromiso.aspx?param1=" & codigo_alu & "&id=" & usuario
                'rutacarta = "window.open(" & ruta & ",'','menubar=no,status=no,toolbar=no,height=300px,width=500')";                              
                'rutacarta = "location.href='../FrmCartaCompromiso.aspx?param1=" & codigo_alu & "&id=" & session("codigo_Usu") & "&param2=" & codigo_cac & "'"
                rutacarta = mensaje(0)      
            else
                rutacarta = mensaje(0)      
            end if	    
		%>
		<input style="width:170px" class="conforme1" name="cmdRegresar" type="button" value="<%=tituloboton%>" onclick="<%=rutacarta%>" />
		</td>
	</tr>
</table>
<%end if%>
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
</body>
</html>
<%
If Err.Number <> 0 then
	response.Write Err.Description
End If
%>