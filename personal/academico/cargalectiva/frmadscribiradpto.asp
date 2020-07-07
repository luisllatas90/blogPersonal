<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_tfu") = "" then
    '2020-06-04-ENevado
    Response.Redirect("../../../sinacceso.html")
end if

codigo_dac=request.querystring("codigo_dac")
modo=request.querystring("modo")
session("codigo_dac1")=request.querystring("codigo_dac")
if session("codigo_tfu")<>27  then
	altadireccion=true
end if


if codigo_dac="" then codigo_dac="-2"
%>
<html>
<head>
<title>Docente-Dpto</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarcargaacademica.js"></script>
</head>
<body>
<%
Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsDpto= obj.Consultar("ConsultarDepartamentoAcademico","FO","TO","")
		if modo="R" and codigo_dac>=0 then
			'Set rsDocente=obj.Consultar("ConsultarDocente","FO","4",codigo_dac,0)  'jala mal el dpto
			Set rsDocente=obj.Consultar("ConsultarDocente","FO","DP",codigo_dac,0)
			
			alto="height=""90%"""
		end if
	obj.CerrarConexion
Set obj=nothing
%>
<body>
<p class="usatTitulo">Registro de Docentes adscritos seg�n Departamento Acad�mico </p>
<form name="frmDpto" METHOD="POST" ACTION="administrarcargalectiva/procesar.asp?accion=AsignarDocenteDepartamento&amp;codigo_dac=0&amp;pag=1&amp;codigoElegido=<%=codigo_dac%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="26%" height="5%">Departamento Acad�mico</td>
    <td width="74%" height="5%"><%call llenarlista("cboDpto","actualizarlista('frmadscribiradpto.asp?modo=R&codigo_dac='+this.value)",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Acad�mico","","")%></td>
  </tr>
  <%if modo="R" and codigo_dac>=0 then%> 
  <tr>
    <td width="26%" height="5%">&nbsp;</td>
    <%if session("codigo_tfu")<>27 then%>
    <td width="74%" height="5%">&nbsp;
	   	<!--<input type="button" class="Agregar2" value="Agregar" NAME="cmdAgregar" onClick="AbrirPopUp('frmlistadocentes.asp?accion=AsignarDocenteDepartamento&codigo_dac=<%=codigo_dac%>','350','550')">-->
   		<!--<input type="button" class="eliminar2" value="Eliminar" NAME="cmdQuitar" onClick="ValidarEliminarDpto()">-->
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" colspan="2" height="95%" valign="top">
    <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
      <tr class="etabla" style="background-color:#e33439; color:White">
	<%if altadireccion=true then%>
        <td width="5%">&nbsp;</td>
	<%end if%>
        <td width="5%">N�</td>
        <td width="9%">C�digo</td>
        <td width="41%">Apellidos y Nombres</td>
        <td width="24%">Email</td>
        <td width="24%">Telefono</td>
        <td width="24%">Celular</td>
        <td width="133%">Estado Actual</td>
      </tr>
      <%Do while Not rsDocente.EOF
      	i=i+1%>
      <tr>
	<%if altadireccion=true then%>
        <td width="5%">	
        <input type="checkbox" name="ListaPara" value="<%=rsDocente("codigo_per")%>">
	</td>
	<%end if%>
        <td width="5%"><%=i%>&nbsp;</td>
        <td width="9%"><%=rsDocente("codigo_per")%>&nbsp;</td>
        <td width="41%"><%=rsDocente("personal")%>&nbsp;</td>
        <td width="24%"><%=rsDocente("email_per")%>&nbsp;</td>
        <td width="24%"><%=rsDocente("telefono_Per")%>&nbsp;</td>
        <td width="24%"><%=rsDocente("celular_Per")%>&nbsp;</td>
        <td width="133%"><%=rsDocente("descripcion_est")%>&nbsp;</td>
      </tr>
      	<%rsDocente.movenext
      Loop%>
    </table>
    </td>
  </tr>
  <%end if%>
</table>
</form>
</body>
</html>