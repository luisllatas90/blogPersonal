<!--#include file="../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_per=request.querystring("codigo_per")

if codigo_per>0 then
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","1",codigo_cac,codigo_per)
	Obj.CerrarConexion
	Set Obj=nothing
    
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaraula.js"></script>
<title>Pagina nueva 1</title>
</head>

<body topmargin="0" leftmargin="0">
<%if (rsCarga.BOF and rsCarga.EOF) then
  		response.write "<p class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;El profesor seleccionado no tiene carga académica para el ciclo académico</p>"
  else
%>
 
<form name="frmcurso" method="POST" Action="procesar.asp?accion=agregarcursovirtual&codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&login_per=<%=rsCarga("login_per")%>">
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <tr class="etabla">
    <td width="76%" height="26" class="pestanaresaltada" style="text-align: left" colspan="6">
    Carga Académica del Profesor   
    <% if rsCarga("login_per") = "" then  
            response.Write("<font color='red' size='1'>(El docente no tiene un usuario asignado, coordinar con el área de TI)</font>")
       end if %></td>
    <td width="24%" height="26" class="pestanaresaltada" style="text-align: right" colspan="2">
    	    <input type="button" class="guardar2"  value="Habilitar acceso" disabled=true onClick="ConfirmarCurso()" name="cmdGuardar" style="width: 120">
			<input type="button" class="regresar2"  value="Cancelar"  onClick="top.window.close()" name="cmdCancelar"></td>
  </tr>
  <tr class="etabla">
    <td width="3%" height="26">&nbsp;</td>
    <td width="5%" height="26">Nº</td>
    <td width="12%" height="26">Código</td>
    <td width="30%" height="26">Descripción de Asignatura</td>
    <td width="10%" height="26">GH</td>
    <td width="5%" height="26">Ciclo</td>
    <td width="30%" height="26">Escuela Profesional</td>
    <td width="5%" height="26">Matriculados</td>
  </tr>

  <% 
  	do while not rsCarga.EOF 	
 	i=i+1
  %>
  <tr id="fila<%=i%>" >
    <td width="3%" align="center" bgcolor="#FFFFFF" height="20">
    <% if rsCarga("login_per") <> "" then %>
    <input type="checkbox" onClick="validarcargaacademica()" name="chkcursoshabiles" value="<%=rsCarga("codigo_cup")%>" >
    <% end if %>
    </td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=i%>&nbsp;</td>
    <td width="12%" bgcolor="#FFFFFF" height="20"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_Cur")%>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_cpf")%><span style="font-size: 7pt"> (<%=rsCarga("descripcion_pes")%>)</span>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("matriculados")%>&nbsp;</td>
  </tr>
  	<%
  	rsCarga.movenext
  loop
  %>
</table>
</form>

	<%Set rsCarga=nothing
	  end if%>
<span id="mensaje" style="color:#FF0000"></span>
    
</body>
</html>
<%end if%>