<%
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsPermiso=Obj.Consultar("ConsultarHorariosAmbiente","FO",9,codigo_cac,codigo_amb,0,0)
	obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Ambientes por escuela</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function MarcarEscuela()
	{
		var total=0
		parent.document.all.cmdGuardar.disabled=true
		
		for(i=0;i<frmpermisos.elements.length;i++){
			var Control=frmpermisos.elements[i]
        	if(Control.type=="checkbox"){
        		
				if (Control.checked==true)
					{total=total+1}
     		}
		}

		//if (total>0){
			parent.document.all.cmdGuardar.disabled=false
		//}
	}
	
	function AbrirDetalleAsignacion(codigo_aam,codigo_cpf)
	{
		AbrirPopUp("frmdetalleasignacionambiente.asp?codigo_aam=" + codigo_aam + "&codigo_cpf=" + codigo_cpf + "&codigo_cac=<%=codigo_cac%>&codigo_amb=<%=codigo_amb%>","400","600")
	}

</script>
</head>

<body>
<form name="frmpermisos" method="post" action="../procesar.asp?accion=AgregarAsignacionAmbiente&codigo_amb=<%=codigo_amb%>&codigo_cac=<%=codigo_cac%>">
<table style="width: 100%">
	<%i=0
	Do while Not rsPermiso.EOF
		i=i+1
		if isnull(rsPermiso("Marca"))=false then
			marcado="CHECKED"
		else
			marcado=""
		end if
	%>
	<tr id="fila<%=i%>">
		<td width="5%">
		<input name="txtcodigo_cpf<%=i%>" type="hidden" value="<%=rsPermiso("codigo_cpf")%>">
		<input onclick="MarcarEscuela()" name="chk<%=i%>" type="checkbox" value="<%=rsPermiso("codigo_cpf")%>" <%=marcado%>>&nbsp;
		</td>
		<td width="90%"><%=rsPermiso("nombre_cpf")%>&nbsp;</td>
		<td width="5%"><img onclick="AbrirDetalleAsignacion('<%=rsPermiso("Codigo_aam")%>','<%=rsPermiso("codigo_cpf")%>')" class="imagen" alt="Establecer fecha de inicio y fin" src="../../../../images/menu3.gif" width="16" height="16"></td>
	</tr>
	<%
		rsPermiso.movenext
		Loop
	%>
</table>
<input name="nocheck" type="hidden" value="<%=i%>">
</form>
</body>
</html>
