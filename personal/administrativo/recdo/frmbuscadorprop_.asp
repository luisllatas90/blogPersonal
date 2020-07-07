<!--#include file="../../../funciones.asp"-->
<%
Dim tipoConsulta

tabla=request.querystring("tabla")
criterio=request.querystring("criterio")

if criterio<>"" then palabrabusqueda=ReemplazarTildes(criterio)

'Cargar datos del documento
if criterio<>"" then
   Set obj=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		if tabla="procedencia" then
			Set rsDatos=obj.Consultar("ConsultarProcedencia","FO",3,palabrabusqueda)
		end if
		if tabla="destinatario" then
			Set rsDatos=obj.Consultar("ConsultarDestinatario","FO",3,palabrabusqueda)
		end if
		
		if tabla="asunto" then
			Set rsDatos=obj.Consultar("ConsultarArhivoDocumentario","FO",5,0,0,palabrabusqueda)
		end if
		
		if (tabla="origen" or tabla="destino") then
			Set rsDatos=obj.Consultar("ConsultarDestinatario","FO",4,palabrabusqueda)
		end if
	obj.CerrarConexion
    Set obj=nothing

    if not (rsDatos.BOF and rsDatos.EOF) then
	activar=true
	alto="height:100%"
    end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Buscar <%=ucase(tabla)%></title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<script language="javascript">
	var tipoctrl="<%=tabla%>"
	
	function BuscarDatos(criterio)
	{
		window.location.href="frmbuscadorprop.asp?tabla=<%=tabla%>&criterio=" + criterio
	}
	
	function MostrarTexto()
	{
		if (tipoctrl=="procedencia"){
			window.opener.document.all.idprocedencia.value=cboResultado.value
			window.opener.document.all.txtprocedencia.value=cboResultado.options[cboResultado.selectedIndex].text
		}
		if (tipoctrl=="destinatario"){
			window.opener.document.all.iddestinatario.value=cboResultado.value
			window.opener.document.all.txtdestinatario.value=cboResultado.options[cboResultado.selectedIndex].text
		}
		if (tipoctrl=="asunto"){
			window.opener.document.all.asunto.value=cboResultado.options[cboResultado.selectedIndex].text
		}
		if (tipoctrl=="origen"){
			window.opener.document.all.idareaarchivo.value=cboResultado.value
			window.opener.document.all.txtareaarchivo.value=cboResultado.options[cboResultado.selectedIndex].text
		}
		if (tipoctrl=="destino"){
			window.opener.document.all.idareaarchivo2.value=cboResultado.value
			window.opener.document.all.txtareaarchivo2.value=cboResultado.options[cboResultado.selectedIndex].text
		}
		
		window.close();
	}
	
	
	function Accion(modo)
	{
		if (modo=="N"){
			location.href='<%=tabla%>.asp?modo=agregar<%=tabla%>&tabla=<%=tabla%>&id<%=tabla%>=0'
		}
		
		if (modo=="M"){		
			if (cboResultado.value==""){
				alert("Seleccione el dato a modificar")
				return(false)
				cboResultado.focus()
			}
			location.href='<%=tabla%>.asp?modo=modificar<%=tabla%>&tabla=<%=tabla%>&id<%=tabla%>=' + cboResultado.value
		}
	}
</script>
<body onload="document.all.txtcriterio.focus();txtcriterio.select()">

<table style="width: 100%;<%=alto%>">
	<tr>
		<td style="width: 10%; height: 10%" class="etiqueta">Buscar:</td>
		<td style="height: 10%; width: 80%;"><input type="text" class="Cajas2" value="<%=criterio%>" name="txtcriterio" onkeyup="if(event.keyCode==13){BuscarDatos(this.value)}" maxlength="50"></td>
		<td style="height: 10%; width: 10%;">&nbsp;</td>
	</tr>
	<%if activar=true then 'Hay datos%>
	<tr>
		<td colspan="3" style="height: 80%">
		<select name="cboResultado" id="cboResultado" multiple="multiple" style="width:100%;height:100%" ondblclick="MostrarTexto()">
			<%Do while not rsDatos.EOF%>
				<option value="<%=rsDatos(0)%>" <%=SeleccionarItem("cbo",trim(criterio),trim(rsDatos(1)))%>><%=rsDatos(1)%></option>
				<%rsDatos.movenext
			Loop%>
		</select>		
		</td>
	</tr>
	<tr>
		<td colspan="3" style="width: 10%; height: 10%">
		<%if tabla<>"asunto" and tabla<>"origen" and tabla<>"destino" then%>
		<input class="usatnuevo" type="button" value="    Nuevo" onclick="Accion('N')" name="cmdNuevo">
		<input class="modificar" type="button" value="Modificar" onclick="Accion('M')" name="cmdModificar">
		<%end if%>
		<input class="guardar" type="button" value="Agregar..." onclick="MostrarTexto()" name="cmdAgregar">
		</td>
	</tr>
	<%end if
	if (activar=false and criterio<>"") then%>
		<tr><td colspan="2" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado datos con el criterio especificado</td></tr>
	<%end if%>
</table>

</body>

</html>
