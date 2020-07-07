<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Adjuntar archivos...</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
 <script language="JavaScript" src="../../../../private/funciones.js"></script>
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo4 {
	color: #000000;
	font-weight: bold;
	font-size: 10pt;
}
.Estilo5 {color: #000000}
.Estilo7 {color: #000000; font-weight: bold; }
-->
</style>
		<script>
		function ValidarSilabos()
		{
		
			if (frmSubir.filename.value=="")	{
			alert("Debe especificar el Archivo");return false;}
			if (frmSubir.txtdescripcion_prp.value=="")	{
			alert("Debe especificar un nombre para el archivo");return false;}
			frmSubir.submit()
		}
		function salir(){
		window.close()
		}
function SeleccionarFila1()
{

	oRow = window.event.srcElement.parentElement;

	if (oRow.tagName == "TR"){
		AnteriorFila.Typ = "Sel";
		AnteriorFila.className = AnteriorFila.Typ + "Off";
		AnteriorFila = oRow;
	}
	//if (oRow.Typ == "Sel"){
		oRow.Typ ="Selected";
		oRow.className = oRow.Typ;
	//}
}		
		function eliminar(codigo_prp,archivo,codigo_dap,codigo_dip){
		    var codigo_cop = frmSubir.txtcodigo_cop.value
			var archivo = frmSubir.txtnombrearchivo.value
			if (frmSubir.txtnombrearchivo.value!="")	{
			//alert(codigo_cop)
			location.href="guardararchivo.asp?accion=eliminar&codigo_prp=" + codigo_prp + "&archivo=" + archivo + "&codigo_dap=" + codigo_dap + "&codigo_cop=" + codigo_cop	+ "&codigo_dip=" + codigo_dip
			}
			else
			{
			//frmSubir.option.checked=true
			alert("Debe especificar el Archivo");return false;}
		}
		function ponervalortext(nombre){
		//var option="opt" + nombre.id
		SeleccionarFila1()
		frmSubir.txtnombrearchivo.value= nombre
		
		//frmSubir.option.checked=true
		//alert(option)
		}

		</script>
</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>


<body topmargin="0" leftmargin="0" rightmargin="0">

<%
codigo_prp=Request.QueryString("codigo_prp")
codigo_dap=Request.QueryString("codigo_dap")
codigo_dip=Request.QueryString("codigo_dip")
	if codigo_dap="" then
		codigo_dap=0
	else
	
	end if
	if codigo_dip="" then
		codigo_dip=0
	else
	
	end if

	codigo_cop=Request.QueryString("codigo_cop")	

modifica=Request.QueryString("modifica")
%>

		<%''if codigo_dap=0 then%>
			<form id="frmSubir" action="guardararchivo.asp?codigo_prp=<%=codigo_prp%>&codigo_dip=<%=codigo_dip%>&codigo_cop=<%=codigo_cop%>&codigo_dap=<%=codigo_dap%>" method="post" enctype="multipart/form-data">
		<%''else%>
			
		<%''end if%>
		  <input name="doCreate" type="hidden" id="doCreate" value="true">
		  <input name="txtcodigo_cop" type="hidden" id="txtcodigo_cop" value="<%=codigo_Cop%>">
		  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="6%" height="25" class="bordeinf">&nbsp;&nbsp;<img src="../../../../images/menus/attachfiles.gif" width="35" height="35"></td>
              <td width="94%" class="bordeinf Estilo4">&nbsp;&nbsp;Adjuntar archivos              
              </td>
            </tr>
            <tr>
              <td colspan="2" valign="top"><table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0" bordercolor="#111111" style="border-collapse: collapse">
                <tr>
                  <td height="20" colspan="3"><input type="file" name="filename" size="40" class="cajas2"></td>
                </tr>
                <tr>
                  <td height="20" colspan="3" class="bordeinf Estilo5"><strong>Nombre</strong><span class="rojo">
                    &nbsp;&nbsp;<input name="txtdescripcion_prp" type="text" id="txtdescripcion_prp" value="" size="50" maxlength="55" >
                                    <input name="txtnombrearchivo" type="HIDDEN" id="txtnombrearchivo">
                  </span></td>
                </tr>
                <tr>
                  <td width="3%" class="rojo"><img src="../../../../images/menus/NodoAbierto.GIF" width="14" height="10"><br></td>
                  <td height="20" colspan="2"><span class="Estilo7">Archivos adjuntos </span></td>
                </tr>
                <tr>
                  <td colspan="3" align="right" valign="top" class="rojo"><table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0">
                      <tr>
                        <td width="90%" height="100%" valign="top" bgcolor="#FFFFFF" class="contornotabla_azul"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
      <%					
		Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objArchivo.AbrirConexion
		if codigo_Cop <> "" then
			set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","to",codigo_Cop)
		ELSE
			//Response.Write(codigo_dap & "<bt>" & codigo_dip)
			if codigo_dap<>0 then
				set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","CP",codigo_DAP)
			else
				set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","CI",codigo_DiP)
			end if
		end if		
		objArchivo.CerrarConexion
		set objArchivo=nothing							
			  		do while not rsArchivosPrp.eof
			  %>
                            <TR height="25" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" typ="Sel" onClick="ponervalortext('<%=rsArchivosPrp(2)%>')" >
                              <td  width="5%" align="left" valign="middle">&nbsp;</td>
                              <td width="15%" align="center"><a href="../../../../filespropuestas/<%=codigo_prp%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank"> <img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16"  border="0"> </a> </td>
                              <td width="80%" align="left"><%response.write(rsArchivosPrp(3))%></td>
                            </TR>
              <%
			  	rsArchivosPrp.movenext()
				loop						
			  %>
                        </table></td>
                        <td width="10%" align="right" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td valign="top"><table width="100%" border="0" cellspacing="5" cellpadding="0">
                              <tr>
                                <td align="right">
								<input type="button" name="cmdEnviar" onClick="ValidarSilabos()" value=" Agregar" class="attach_prp" style="width: 85" <%''if modifica=2 then%>  <%''end if%>>
								</td>
                              </tr>
                              <tr>
                                <td align="right"><input type="button" name="cmdEnviar2" onClick="eliminar(<%=codigo_prp%>,this.value,<%=codigo_dap%>,<%=codigo_dip%>)" value="Quitar" class="remove_prp" style="width: 85" <%''if modifica=2 then%> <%''end if%>></td>
                              </tr>

                            </table></td>
                          </tr>
                          <tr>
                            <td valign="bottom"><table width="100%" border="0" cellspacing="5" cellpadding="0">
                              <tr>
                                <td align="right"><input type="button" name="cmdEnviar22" onClick="salir()" value="Salir" class="cerrar_prp" style="width: 85"></td>
                              </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
              </table></td>
            </tr>
          </table>
</form>
</body>
</html>

