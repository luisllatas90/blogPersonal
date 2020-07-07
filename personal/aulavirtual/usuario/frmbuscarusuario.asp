<!--#include file="clsusuario.asp"-->
<%
tipo_uap=request.querystring("tipo_uap")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")
modo=request.querystring("modo")

codigo_apl=request.querystring("codigo_apl")
codigo_tfu=request.querystring("codigo_tfu")
if tipo_uap="" then tipo_uap=1

set usuario=new clsusuario
	with usuario
		.Restringir=session("codigo_usu")
		ArrUsuario=.consultar("3",tipo_uap,"","")
		ArrTipo=.consultar("2","","","")
	end with
Set usuario=nothing	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione que usuarios tendrán acceso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmListaCorreos" onsubmit="return validarlistaElegida(this)" method="post" ACTION="procesar.asp?accion=agregarpermisocursovirtual&nombretabla=<%=nombretabla%>&idtabla=<%=idtabla%>&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>&modo=<%=modo%>">
<%botonesaccion%>
<table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-color:#C0C0C0; border-collapse: collapse" bordercolor="#111111" height="80%">
		<tr align="center">
          <td  width="40%"  height="20%">
          <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
            <tr>
              <td width="15%" class="etiqueta">Tipo</td>
              <td width="85%">
	<select name="tipo_uap" onChange="actualizarlista('frmbuscarusuario.asp?tipo_uap='+ this.value + '&nombretabla=<%=nombretabla%>&idtabla=<%=idtabla%>&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>&modo=<%=modo%>')" class="cajas">
	<%If IsEmpty(ArrTipo)=false then
		for i=lbound(ArrTipo,2) to Ubound(ArrTipo,2)%>
			<option value="<%=ArrTipo(0,I)%>" <%=Seleccionar(tipo_uap,ArrTipo(0,i))%>><%=ArrTipo(1,I)%></option>
		<%next
	end if%> </select></td>
            </tr>
            <tr>
              <td width="15%" class="etiqueta">Buscar </td>
              <td width="85%">
              <input  maxLength="100" size="50" name="nombreusuario" class="cajas" ONKEYUP="AutocompletarCombo(this,document.all.ListaDe,'text',true)"></td>
            </tr>
          </table>
          </td>
          <td  width="10%"  height="20%">&nbsp;</td>
          <td width="40%" height="20%" valign="bottom"><b>Usuarios seleccionados</b></td>
          </tr>
		<tr align="center">
          <td  width="40%"  height="70%" valign="top"><select multiple name="ListaDe" size="10" class="cajas" style="height: 100%">
			<%If IsEmpty(ArrUsuario)=False then
				FOR I=Lbound(ArrUsuario,2) to Ubound(ArrUsuario,2)%>
				<option value="<%=ArrUsuario(0,I)%>"><%=ArrUsuario(1,I)%></option>
				<%NEXT
			end if%>
		  </select></td>
          <td  width="10%"  height="70%" valign="top">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onclick="AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onclick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
          <td width="40%" height="70%" valign="top">&nbsp;<select multiple name="ListaPara" size="10" style="height:100%" class="cajas">
		  </select></td>
          </tr>
		</table>
</form>
</body>
</html>