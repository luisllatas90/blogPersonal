<%@ Language=VBScript %>
<%
    Dim objDeuda

Dim dtmFecha_Deu 
Dim strNumeroBoleta 
Dim strTipoResp_Deu 
Dim lngCodigo_Cli 
Dim intCodigo_Sco 
Dim intCodigo_Cac 
Dim strObservacion_Deu 
Dim dblMontoTotal_Deu 
Dim strMoneda_Deu 
Dim strEstado_Deu 
Dim blnConsideraMora_Deu 
Dim dtmFechaVencimiento_Deu 
Dim intCodigo_Are 
Dim bytCodigo_Emp 

dtmFecha_Deu =cdate(Request.form("txtfecha"))
strNumeroBoleta =""
strTipoResp_Deu =Request.form("txtTipoResp")
lngCodigo_Cli =Request.form("txtCodResp")
intCodigo_Sco =Request.form("cboConcep_Deu")
intCodigo_Cac ="4" 
strObservacion_Deu =Request.form("txtObserv")
dblMontoTotal_Deu =Request.form("txtMon_Deu")
strMoneda_Deu =Request.form("cboMoneda")
strEstado_Deu ="P"
blnConsideraMora_Deu ="0"
dtmFechaVencimiento_Deu =cdate(Request.form("txtfecha"))
'intCodigo_Are ="0"
bytCodigo_Emp ="1"
	
	Set objDeuda= Server.CreateObject("PryUSAT.clsDatDeuda")

	


	'objDeuda.AgregarDeuda dtmFecha_Deu ,trim(strNumeroBoleta) , trim(strTipoResp_Deu) , lngCodigo_Cli , intCodigo_Sco , intCodigo_Cac , trim(strObservacion_Deu ),dblMontoTotal_Deu , strMoneda_Deu , strEstado_Deu ,blnConsideraMora_Deu , dtmFechaVencimiento_Deu , intCodigo_Are , bytCodigo_Emp 
	objDeuda.AgregarDeuda dtmFecha_Deu ,trim(strNumeroBoleta) ,trim(strTipoResp_Deu),lngCodigo_Cli, intCodigo_Sco,  intCodigo_Cac,trim(strObservacion_Deu ),dblMontoTotal_Deu ,  strMoneda_Deu , strEstado_Deu, blnConsideraMora_Deu, dtmFechaVencimiento_Deu , "0", bytCodigo_Emp 
	
	if Err.Description <> ""  Then
		Response.Write ( "<p align=center><B>Error de base de datos [1]: " + Err.Description + "</B></p>")
		Response.Write( "<p align=center>No se ha podido introducir los datos en la base de datos.</p>")
	Else
		'Response.Write ("<p align=center>Sus Datos Se Grabaron Correctamente.</p>")
		a=lngCodigo_Cli
		b=strTipoResp_Deu
		'ruta="frmRegistrarDeuda.asp?" & "id=" & a & "&" & "tr=" & b
		ruta="frmConsultarDeuda.asp?" & "id=" & a & "&" & "tr=" & b
		response.Redirect(ruta)
	end if
	Set objDeuda=Nothing	
%>