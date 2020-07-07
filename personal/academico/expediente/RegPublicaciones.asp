<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
</head>
<body>
<%
Dim objPubliProf
Dim rsPubliProf
Set objPubliProf=Server.CreateObject("PryUSAT.clsDatPublicacion")

Dim objInvestPubli
Dim rsInvestPubli
Set objInvestPubli=Server.CreateObject("PryUSAT.clsDatPublicacion")

intcodigo_per=request.Form("codigo_per")
strtitulo_pub=request.Form("txttitulo_Pub")
dtfecha_pub=request.form("txtfecha_Pub")
intcodigo_sit=request.Form("cbocodigo_Sit")
strobservaciones=request.Form("atxobservaciones_Pub")
intcodigo_aco=request.Form("cbocodigo_Aco")
intcodigo_tpu=request.Form("cbocodigo_TPu")
intcodigo_mpu=request.Form("cbocodigo_MPu")
strbasado=request.Form("optInvest")
intcodigo_inv=request.Form("cbocodigo_Inv")

Dim objDocentesInvest
Dim rsDocentesInvest
Set objDocentesInvest=Server.CreateObject("PryUSAT.clsDatInvestigacion")
Set rsDocentesInvest=Server.CreateObject("ADODB.Recordset")
Set rsDocentesInvest= objDocentesInvest.ConsultarInvestigacion("RS","IN",intcodigo_inv)
numrec=rsDocentesInvest.recordcount

'response.Write("numero de registros: "&numrec)
'for each dato in request.form
'	response.write dato & ":" & request.form(dato) & "<br>"
'next

Dim objPublicacion
Dim rsPublicacion
Set objPublicacion=Server.CreateObject("PryUSAT.clsDatPublicacion")
objPublicacion.agregarpublicaciones strtitulo_pub,dtfecha_pub,intcodigo_sit,strobservaciones,intcodigo_aco,intcodigo_tpu,intcodigo_mpu
Set rsPublicacion=Server.CreateObject("ADODB.Recordset")
Set rsPublicacion= objPublicacion.ConsultarPublicacion("RS","TI",strtitulo_pub)	

intcodigo_pub=rsPublicacion("codigo_Pub")

'response.Write("publicacion:"&intcodigo_pub&"---Docente:"&intcodigo_per)

if strbasado="0" then
	objPubliProf.AgregarPublicacionesProfesor intcodigo_pub,intcodigo_per
end if
if strbasado="1" then
	do while not rsDocentesInvest.eof
		objPubliProf.AgregarPublicacionesProfesor intcodigo_pub,rsDocentesInvest("codigo_per")
		'response.Write("codigos de docentes: "&trim(rsDocentesInvest("codigo_per")))
		rsDocentesInvest.movenext
	loop
objInvestPubli.AgregarInvestigacionPublicacion intcodigo_pub,intcodigo_inv
end if
set objPubliProf=Nothing
set objInvestPubli=Nothing
set objDocentesInvest=Nothing
set objPublicacion=Nothing
'response.write"<script>window.location.href='frmRegInvestigacion2de2.asp?titulo="&strTitulo_Inv&"'</script>"
%>
<script language="Javascript">
	window.opener.location.reload();window.close()
</script>
</body>
</html>
