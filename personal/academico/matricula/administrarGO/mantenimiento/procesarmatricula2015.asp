<!--#include file="../../../../../NoCache.asp"-->
<!--#include file="../../../../../funciones.asp"-->
<%
'*************************************************************************************
'CV-USAT
'Fecha de Creación: 29/07/2006
'Autor			: Gerardo Chunga Chinguel
'Fecha de Modificación: 08/03/2007
'Observaciones	: Permite procesar la matrícula, agregado/retiro de asignaturas
'*************************************************************************************

call Enviarfin(session("codigo_usu"),"../../../../../")

'*************************************************************************************
'Declarar variables
'*************************************************************************************
Dim CursosProgramados
Dim VecesDesprobados
Dim tipomotivo
Dim descripcionmotivo

accion=Request.querystring("accion")
codigo_mat=request.querystring("codigo_mat")
codigo_cup=request.querystring("codigo_cup")
estado_dma=request.querystring("estado_dma")

usuario=session("Usuario_bit")
IP=session("Equipo_bit")
codigo_alu=session("codigo_alu")
codigo_pes=session("Codigo_pes")
tipo_cac=request.querystring("tipo_cac")
codigo_cac=request.querystring("Codigo_Cac")
tituloboton="Ver estado de cuenta"

'************************ Motivo de matricula ************************
tipomotivo = Request.form("hdmotivo")
descripcionmotivo = Request.form("hdmotivo")
'**********************************************************************

if(session("tipo_Cac") = "E") then
    cuotas = 2
else    
    cuotas = 5
end if
 
'response.Write "<script>alert('Permite: " & request.Form("chkpermitircruce") & " ')</script>"
'if cStr(Request.Form("chkpermitircruce")) = "1" then
'   Verificacodigo_alu=-1	    
'   Verificacursosprog = ",,,"
'else    
'   Verificacodigo_alu=codigo_alu	
'   Verificacursosprog =Request.form("CursosProgramados")
'end if

Verificacodigo_alu=codigo_alu	
Verificacursosprog =Request.form("CursosProgramados")


Sub asignarcontrolesmatricula()
	CursosProgramados=Request.form("CursosProgramados")
	VecesDesprobados=Request.form("VecesDesprobados")
	
End Sub

Server.ScriptTimeout=400

If Not Response.IsClientConnected Then 
  	'Recetear si se desconectó el cliente
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
		set rs_cruceshorario = objMatricula.Consultar("ACAD_validaCrucesMatriculaGO","FO",Verificacodigo_alu,codigo_cac,Verificacursosprog)
		'objMatricula.Ejecutar("ConsultarCursosHabilesMatriculaGO", codigo_alu,codigo_pes,codigo_cac)
        'set rs_cruceshorario = objMatricula.Consultar("MAT_validacrucesesionesGO", codigo_alu,codigo_cac,codigo_pes,@codigo_cup=582986,CursosProgramados
		if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false and cStr(Request.Form("chkpermitircruce")) <> "1" then
		    response.Write("<script>alert('Cruce de horario')</script>")
		    '	objMatricula.cerrarconexion
		    '	set objMatricula=nothing
	    else
	        '*************************************************************************************
		    'Grabar cabezera y detalle de matrícula, luego generar pago
		    '*************************************************************************************
		    if VecesDesprobados = "" then
		        VecesDesprobados = ","
		    end if
    		
		    '*************************************************************************************
		    'Comentado el 25/02/2011 por hreyes - cambiado por número de cuotas
		    '*************************************************************************************
		    'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null)
		    if codigo_mat<=0 then
		        objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false, "ASE", codigo_alu, codigo_cac 
		    end if 
    		
		    'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,1,"",cuotas,null)			
		    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"A",codigo_alu,codigo_pes,codigo_cac,"N","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,1,"",cuotas,tipomotivo, descripcionmotivo, null)            
    		
		    'response.write ("E" & "," & codigo_alu & "," & codigo_pes & "," & codigo_cac & "," & "P" & "," & "Matrícula Web" & "," & CursosProgramados & "," & VecesDesprobados & "," &  "N" & ", " & "P" & "," & "MAT" & "," & usuario & "," & IP & "," & "A")
	        'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados	
		end if		
	    objMatricula.CerrarConexion
		Set objmatricula=nothing
		
	end if

'*************************************************************************************
'Agregado de asignaturas
'*************************************************************************************
	if accion="agregarcursomatricula" then	    		
		asignarcontrolesmatricula				
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
			objMatricula.AbrirConexion
        
        
		'*************************************************************************************
		'Validar cruces de horarios entre asignaturas marcadas
		'*************************************************************************************
		on error resume next
		
        if(Verificacodigo_alu > -1) then           
            'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",Verificacodigo_alu,codigo_pes,codigo_cac,Verificacursosprog)		
            set rs_cruceshorario = objMatricula.Consultar("ACAD_validaCrucesMatriculaGO","FO",Verificacodigo_alu,codigo_cac,Verificacursosprog)
	        if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false and cStr(Request.Form("chkpermitircruce")) <> "1" then
	            response.Write("<script>alert('Cruce de horario')</script>")
		        '    objMatricula.cerrarconexion
		        '    set objMatricula=nothing		        	        
	        else
	            '*************************************************************************************
	            'Grabar cabezera y detalle de matrícula, luego generar pago
	            '*************************************************************************************
	            ' jmanay: agregado para los motivos de agregado y retiro	        
                if Request.form("txtesnuevamatricula")="S" then
	                codigo_mar	=	null
	                obs		=	null
                else
	                codigo_mar	=	Request.form("cbocodigo_mar")
	                obs		=	Request.form("txtobs")
                end if
	            '*************************************************************************************	                    
                if codigo_mat<=0 then
                    objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false, "ASE", codigo_alu, codigo_cac 
                end if                 
                'response.Write ("AgregarMatriculaWeb_v1_2015 ,  'S'," & codigo_alu & "," & codigo_pes & ", " & codigo_cac & ",'N', 'Matricula por Asesor'," & "'" & CursosProgramados & "','" & VecesDesprobados &"', N,P,AGR,'" & usuario & "','" & IP & "'," & codigo_mar & ",'" & obs & "'," & cuotas & ",'" & tipomotivo & "','" & descripcionmotivo & "', 'null'")
                'Se cambio el modo de A: Asesor a S: SuperAdmin :P para que pueda saltar las validaciones de verano
                mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"S",codigo_alu,codigo_pes,codigo_cac,"N","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs, cuotas,tipomotivo, descripcionmotivo, null) 	            
                'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados 
                'objMatricula.CerrarConexion 
                'Set objmatricula=nothing 
            end if
        else
            '*************************************************************************************
            'Grabar cabezera y detalle de matrícula, luego generar pago
            '*************************************************************************************
            ' jmanay: agregado para los motivos de agregado y retiro
            if Request.form("txtesnuevamatricula")="S" then
                codigo_mar	=	null
                obs		=	null
            else
                codigo_mar	=	Request.form("cbocodigo_mar")
                obs		=	Request.form("txtobs")
            end if
            '*************************************************************************************	        
            'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs,null)
            'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs, cuotas,null)
            
            mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb_v1_2015",True,"A",codigo_alu,codigo_pes,codigo_cac,"N","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs, cuotas,tipomotivo, descripcionmotivo, null)
            'objMatricula.CerrarConexion
            'Set objmatricula=nothing
        end if	   
         
	    objMatricula.CerrarConexion
        Set objmatricula=nothing	    
	    tituloboton="Regresar"
	
	    If Err.Number <> 0 Then  
            Response.Write (Err.Description)    
        End If
    end if
'*************************************************************************************
'modificacion de motivos
'*************************************************************************************
	if accion="modificarmotivo" then
		codigo_dma	=	Request.querystring("codigo_dma")
		tipo_mar	=	Request.querystring("tipo_mar") 'es decir si es modificaion de motivo de agregado o retiro
		codigo_mar	=	Request.form("cbocodigo_mar")
		obs		=	Request.form("txtobs")
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
		mensaje=objMatricula.Ejecutar("sp_modificarmotivo_detallematricula_2015",True,tipo_mar,codigo_dma,codigo_mar,obs,session("Usuario_bit"), null)		
		objMatricula.cerrarconexion
		Set objmatricula=nothing
		%>				
			<script type="text/javascript" language="javascript">
				alert("<%=mensaje%>") ;
				window.opener.location.href=	window.opener.location.href
				window.close();
			</script>
		<%

	end if 

'*************************************************************************************
'Retiro de asignaturas
'*************************************************************************************

	if accion="retirarcursomatricula" then

		codigo_dma=Request.querystring("codigo_dma")
		codigo_mar	=	Request.querystring("codigo_mar")
		'obs		=	Request.querystring("obs")		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
			mensaje=objMatricula.Ejecutar("RetirarCursoMatriculaAdmin",true,"A",codigo_dma,usuario,codigo_mar,"",null)			
		objmatricula.CerrarConexion
		Set objmatricula=nothing
		tituloboton="Regresar"
		'response.redirect "detallematricula.asp?codigo_cac=" & codigo_cac
		%>		
			<script type="text/javascript" language="javascript">			    
				window.opener.location.href=window.opener.location.href
				window.close();
			</script> 
		<%

		
	end if

'*************************************************************************************
'RESTABLECER MATRICULA
'*************************************************************************************

	if accion="restablecermatricula" then
        
		codigo_mat =Request.querystring("codigo_mat")		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
	    mensaje=objMatricula.Ejecutar("Acad_EliminarMatricula",true,codigo_mat,null)		
		objmatricula.CerrarConexion
		Set objmatricula=nothing
		tituloboton="Regresar"
		avisosretiro=split(mensaje,"|")
		%>		
			<script type="text/javascript" language="javascript">
			    //window.location.href = window.location.href
			    window.location.href = "../mensajes.asp?proceso=RM"
			    window.close();
			</script>
		<%

		
	end if

'*************************************************************************************
'ELIMINAR de asignaturas
'*************************************************************************************
	if accion="eliminarcursomatricula" then
		codigo_dma=Request.querystring("codigo_dma")
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
			mensaje=objMatricula.Ejecutar("EliminarCursoMatriculado",true,codigo_dma,usuario,null)
		objmatricula.CerrarConexion
		Set objmatricula=nothing
		tituloboton="Regresar"
		
		response.redirect "detallematricula2015.asp?codigo_cac=" & codigo_cac
	end if

%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Resultados de Matrícula</title>
<link rel="stylesheet" type="text/css" href="../../../../../private/estilo.css" />
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
		<td align="center" class="nvomensaje"><%=mensaje(1)%> </td>
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