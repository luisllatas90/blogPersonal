<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funciones.asp"-->
<%
'*************************************************************************************
'CV-USAT
'Fecha de Creaci�n: 29/07/2006
'Autor			: Gerardo Chunga Chinguel
'Fecha de Modificaci�n: 08/03/2007
'Observaciones	: Permite procesar la matr�cula, agregado/retiro de asignaturas
'*************************************************************************************

call Enviarfin(session("codigo_usu"),"../../../../")

'*************************************************************************************
'Declarar variables
'*************************************************************************************
Dim CursosProgramados
Dim VecesDesprobados

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

cuotas = 1

if request.Form("chkpermitircruce")="" then
	Verificacodigo_alu=codigo_alu
else
	Verificacodigo_alu=-1
end if

'Sub asignarcontrolesmatricula()

'	CursosProgramados=Request.form("CursosProgramados")	
'	VecesDesprobados=Request.form("VecesDesprobados")
		
'End Sub

Server.ScriptTimeout=400

If Not Response.IsClientConnected Then 
  	'Recetear si se desconect� el cliente
	Shutdownid = Session.SessionID
	'Obtimizar proceso
	Shutdown(Shutdownid)
End If

'on error resume next

'*************************************************************************************
'Agregar nuevos cursos a la matricula
'*************************************************************************************
	if accion="matriculasegura" then
		'asignarcontrolesmatricula
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
			objMatricula.AbrirConexion

		'*************************************************************************************
		'Validar cruces de horarios entre asignaturas marcadas
		'*************************************************************************************
		CursosProgramados=Request.form("CursosProgramados")	
	    VecesDesprobados=Request.form("VecesDesprobados")
		'Se comento porque profesionalizacion no tiene cruce de horarios (esaavedra) : 04.11.2014 - Comentado por CSenmache
		'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",Verificacodigo_alu,codigo_pes,codigo_cac,CursosProgramados)
		'if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false then
			'objMatricula.cerrarconexion
			'set objMatricula=nothing
		%>
		    <!--
			<script>
				window.datos=showModalDialog("frmcruces.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cac=<%=codigo_cac%>&cursosprogramados=<%=cursosprogramados%>","","status:no;resizable:yes;dialogWidth:60;dialogHeight:20" );
				window.history.back(-1)
			</script>
			-->
		<%'else
		    'Validamos que no tengas mas de 2 deudas
		    
		    Set rsDeudas=objMatricula.Consultar("ACAD_RetornaNumeroDeudasPP","FO",codigo_alu)
		    numDeudas = 0
		    if Not(rsDeudas.BOF and rsDeudas.EOF) then
		        if(rsDeudas.fields("numDeudas") > 2 and session("codigo_tfu") <> 146) then
		            numDeudas = rsDeudas.fields("numDeudas")
		        end if
		        
		        if(rsDeudas.fields("numDeudas") > 3 and session("codigo_tfu") = 146) then
		            numDeudas = rsDeudas.fields("numDeudas")
		        end if
		    end if
		    
		    Set rsConvenio=objMatricula.Consultar("ACAD_VerificaConvenioPorAlumno","FO",codigo_alu)
		    TieneConvenio = 1
		    if Not(rsConvenio.BOF and rsConvenio.EOF) then
		        if(rsConvenio.recordCount = 0) then
		            TieneConvenio = 0
		        end if		        
		    end if
		    
		    if(numDeudas > 2 or TieneConvenio = 0) then 
		        mensaje = "location.href='frmadminmatricula.asp'|Su matricula no fue registrada.<br/>Ud. no tiene convenio o tiene m�s de 2 deudas vencidas."
			    tituloboton = "Regresar"
		    else			        		        		        	        
			    '*************************************************************************************
			    'Grabar cabezera y detalle de matr�cula, luego generar pago
			    '*************************************************************************************
			    if VecesDesprobados = "" then
			        VecesDesprobados = ","
			    end if
    			
			    '*************************************************************************************
			    'Comentado el 25/02/2011 por hreyes - cambiado por n�mero de cuotas
			    '*************************************************************************************
			    'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,null)
			    if codigo_mat<=0 then
			        objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false, "ASE", codigo_alu, codigo_cac 
			    end if 
    			
			    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","MAT",usuario,IP,1,"",cuotas,null)
			    'response.write ("E" & "," & codigo_alu & "," & codigo_pes & "," & codigo_cac & "," & "P" & "," & "Matr�cula Web" & "," & CursosProgramados & "," & VecesDesprobados & "," &  "N" & ", " & "P" & "," & "MAT" & "," & usuario & "," & IP & "," & "A")
		        'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados		        
		    end if
		    
		    objMatricula.CerrarConexion
			Set objmatricula=nothing
		'end if
	end if

'*************************************************************************************
'Agregado de asignaturas
'*************************************************************************************
	if accion="agregarcursomatricula" then
		'asignarcontrolesmatricula		
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
		CursosProgramados=Request.form("CursosProgramados")	
	    VecesDesprobados=Request.form("VecesDesprobados")
	    'response.Write (CursosProgramados)
	   
		'*************************************************************************************
		'Validar cruces de horarios entre asignaturas marcadas '
		'No debe validar horarios de profesionalizacion segun esaavedra - Comentado 04.11.2014 - CSenmache
		'*************************************************************************************
		'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",Verificacodigo_alu,codigo_pes,codigo_cac,CursosProgramados)

		'if rs_cruceshorario.eof=false and rs_cruceshorario.bof=false then
		'objMatricula.cerrarconexion
		'set objMatricula=nothing
				
		%>
		<!--<script>
			    window.datos = showModalDialog("frmcruces.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cac=<%=codigo_cac%>&cursosprogramados=<%=CursosProgramados%>", "", "status:no;resizable:yes;dialogWidth:60;dialogHeight:20");
				window.history.back(-1)
			</script>
		-->
		<%
		'else
		'*************************************************************************************
		'Grabar cabezera y detalle de matr�cula, luego generar pago
		'*************************************************************************************
		Set rsDeudas=objMatricula.Consultar("ACAD_RetornaNumeroDeudasPP","FO",codigo_alu)
	    numDeudas = 0
	    if Not(rsDeudas.BOF and rsDeudas.EOF) then
	        if(rsDeudas.fields("numDeudas") > 2 and session("codigo_tfu") <> 146) then
	            numDeudas = rsDeudas.fields("numDeudas")
	        end if
	        
	        if(rsDeudas.fields("numDeudas") > 3 and session("codigo_tfu") = 146) then
	            numDeudas = rsDeudas.fields("numDeudas")
	        end if
	    end if
		    
		Set rsConvenio=objMatricula.Consultar("ACAD_VerificaConvenioPorAlumno","FO",codigo_alu)
	    TieneConvenio = 1
	    if Not(rsConvenio.BOF and rsConvenio.EOF) then	        
	        if(rsConvenio.recordCount = 0) then
	            TieneConvenio = 0
	        end if	        
	    end if
		    
		if(numDeudas > 2 or TieneConvenio = 0) then 
	        mensaje = "location.href='frmadminmatricula.asp'|Su matricula no fue registrada.<br/>Ud. no tiene convenio "& TieneConvenio & " o tiene m�s de 2 deudas vencidas." 
		    tituloboton = "Regresar"
	    else
	        'Cambiado por hreyes
		    if Request.form("cbocodigo_mar")="" or  Request.form("cbocodigo_mar")=null then
			    codigo_mar	=	null
			    obs		=	null
		    else
			    codigo_mar	=	Request.form("cbocodigo_mar")
			    obs		=	Request.form("txtobs")
		    end if
		    ' jmanay: agregado para los motivos de agregado y retiro
		    '//No validaba correctamente --> esto estaba antes jmanay
		    'if Request.form("txtesnuevamatricula")="S"  then
		    '	codigo_mar	=	null
		    '	obs		=	null
		    'else
		    '	codigo_mar	=	Request.form("cbocodigo_mar")
		    '	obs		=	Request.form("txtobs")
		    'end if
    		
		    '*************************************************************************************
		    if codigo_mat<=0 then
		        objMatricula.Ejecutar "AgregarBitacoraBloqueosMatricula", false, "ASE", codigo_alu, codigo_cac 
		    end if 

		    'mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs,null)
		    mensaje=objMatricula.Ejecutar("AgregarMatriculaWeb",True,"A",codigo_alu,codigo_pes,codigo_cac,"P","Matricula por Asesor",CursosProgramados,VecesDesprobados,"N","P","AGR",usuario,IP,codigo_mar,obs, cuotas,null)
	        'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, "MAT", codigo_alu, 0, codigo_cac, CursosProgramados	        
		end if    
		    objMatricula.CerrarConexion
		    Set objmatricula=nothing	
		
		'end if
		tituloboton="Regresar"
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
		mensaje=objMatricula.Ejecutar("sp_modificarmotivo_detallematricula",True,tipo_mar,codigo_dma,codigo_mar,obs,session("Usuario_bit"), null)
		objMatricula.cerrarconexion
		Set objmatricula=nothing
		%>				
			<script type="text/javascript" language="javascript">				
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
		codigo_mar	=	Request.form("cbocodigo_mar")
		obs		=	Request.form("txtobs")
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objMatricula.AbrirConexion
			mensaje=objMatricula.Ejecutar("RetirarCursoMatricula",true,"A",codigo_dma,usuario,codigo_mar,obs,null)			
		objmatricula.CerrarConexion
		Set objmatricula=nothing
		tituloboton="Regresar"
		
		'response.redirect "detallematricula.asp?codigo_cac=" & codigo_cac				
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
		
		response.redirect "detallematricula.asp?codigo_cac=" & codigo_cac
	end if
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Resultados de Matr�cula</title>
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
'Asumir que se matr�cul� para que consulte el detallematr�cula
session("Ultimamatricula")=session("descripcion_cac")
%>
<table height="60%" cellpadding="4" align="center" style="width: 50%" id="table1">
	<tr height="50%">
		<td align="center" class="nvomensaje"><%=mensaje(1)%></td>
	</tr>
	<tr height="5%">
		<td align="center">
		<input style="width:170px" class="conforme1" name="cmdRegresar" type="button" value="<%=tituloboton%>" onclick="<%=mensaje(0)%>">
		</td>
	</tr>
</table>
<%end if%>
</body>
</html>