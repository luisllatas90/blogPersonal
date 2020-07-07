<%
if inStr(Request.ServerVariables("HTTP_USER_AGENT"),"python-requests") > 0 then
	response.Redirect("mensajelogueo.asp")   
end if

if inStr(Request.ServerVariables("HTTP_REFERER"),"frmaccesoAulaVirtual") < 0 then
	response.Redirect("mensajelogueo.asp")   
end if

Dim clave 
Dim Hora
Dim pso
Dim psoCifrado
Dim PassPer
Dim PassPerCif

Dim Obj , ObjBD
Dim RS
'Set Obj = Server.CreateObject("PryCifradoNet.ClscifradoNet")
Set ObjBD = Server.CreateObject("PryUSAT.clsAccesoDatos")

	ObjBD.AbrirConexion
	set Rs = objbd.Consultar("MOODLE_ConsultarCodigoAcceso","FO","AL",session("codigo_Usu"))
	ObjBD.CerrarConexion
	
	if Not (rs.eof and rs.bof) then
		Rs.Movefirst
		pso = Rs("codigo_pso")
		PassPer = Rs("ClaveInterna_Pso")
	end if
	
' pso = "123456789" & cstr(pso)
' hora = cstr(replace( replace(replace( replace(NOW,"/",""),":","") ," ",""),".",""))
' hora = StrReverse(hora)

' Randomize
' clavecifrada = obj.Cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))
' Randomize
' valor2 = obj.Cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))
' Randomize
' valor3 = obj.Cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))

' valor4 = obj.Cifrado(valor2,valor3)



' psoCifrado = obj.Cifrado(cstr(pso), cstr(clavecifrada))
' PassPerCif = obj.Cifrado(trim(cstr(PassPer)),cstr(clavecifrada) )

%>
<html>
<head>
<meta name="tipo_contenido" content="text/html;" http-equiv="content-type" charset="utf-8">
<link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>

<center>
<!--
'----------------------------------------------------------------------
'Fecha: 29.10.2012
'Usuario: yperez
'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe - intranet.edu.pe]
'---------------------------------------------------------------------- 
-->
<FORM action="https://intranet.usat.edu.pe/aulavirtual/login/index.php" method="POST" target="_blank">
	<INPUT TYPE="submit" name= "Enviar" VALUE="Click para Ingresar" />
	<INPUT TYPE="Hidden" name= "avm1" VALUE="<% Response.write(psoCifrado) %>"/>
	<INPUT TYPE="Hidden" name= "avm2" VALUE="<% Response.write(valor2)%>"/>
	<INPUT TYPE="Hidden" name= "avm3" VALUE="<% Response.write(clavecifrada) 	%>"/>
	<INPUT TYPE="Hidden" name= "avm4" VALUE="<% Response.write (valor3) %>"/>
	<INPUT Type="Hidden" name ="avm5" Value="<% Response.write (PassPerCif) %>"/> 				
	<INPUT Type="Hidden" name ="avm6" Value="<% Response.write (valor4) %>"/>
	<INPUT Type="Hidden" name ="avm7" Value="<% Response.write (pso)%>"/>
	<INPUT Type="Hidden" name ="avm8" Value="<% Response.write (PassPer) %>"/>
	
</FORM>
</center>
<p style=" color:#661c1d;">Mis Cursos en Aula Virtual</p>
<table  border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
<tr><td colspan="4"></td></tr>

<THEAD>
<tr  class="etabla"><td>Nro.</td><td>Curso</td><td>Docente</td><td>Estado</td></tr>
</THEAD>
<%
    Dim ObjBD2 
    Set ObjBD2 = Server.CreateObject("PryUSAT.clsAccesoDatos")
    ObjBD2.AbrirConexion
    Set rsCursos = ObjBD2.Consultar("MOODLE_ConsultarCursos","FO",session("codigo_Usu"),session("codigo_test"),session("codigo_cac"))
	ObjBD2.CerrarConexion
	set ObjBD2 = nothing
    
 
	if (rsCursos.BOF and rsCursos.EOF) then
	    response.Write("<tr><td colspan='4'>No se encontraron cursos matriculados en el ciclo actual </tr>")
    else
        do while not rsCursos.EOF 
        i=i+1
	    response.Write("<tr>")
	    response.Write("<td>"& i &"</td>")
	    response.Write("<td>" & rsCursos("descripcion_cac") & " - "& rsCursos("nombre_Cur") & " (Grupo: " & rsCursos("grupoHor_Cup") &")</td>")
		response.Write("<td>" & rsCursos("docente") &"</td>")					    									   
	    response.Write("<td colspan='4'>" & rsCursos("estado") &"</td>") 
	    response.Write("</tr>")    
	    rsCursos.movenext
        Loop
        Set rsCursos = nothing
	end if
	
	Set objRS = nothing
 %>
</table>
</html>

