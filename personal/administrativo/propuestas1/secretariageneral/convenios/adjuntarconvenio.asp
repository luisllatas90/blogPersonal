<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Adjuntar Archivo ...</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
 <script language="JavaScript" src="../../../../../private/funciones.js"></script>
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
   var NombreArch
   var extension
   var ancho
   NombreArch=new String(frmSubir("filename").value)
   ancho=NombreArch.length

   //Validar el tipo de archivo
   if(ancho>4){
	NombreArch=NombreArch.toLocaleLowerCase() 
	extension=NombreArch.substr(ancho-3,3)
	if(extension=="pdf"){
			if (frmSubir.filename.value=="")	{
			alert("Debe especificar el Archivo");return false;}
			if (frmSubir.txtdescripcion_prp.value=="")	{
			alert("Debe especificar un nombre para el archivo");return false;}
			frmSubir.submit()
		}

	else{
		alert("Solo se pueden subir arhivos con extensión PDF")
	}
   }
   else{
	alert("Debe especificar la ruta del archivo PDF")
   }
}		
		
		function ValidarSilabos1()
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
		function eliminar(codigo_cni,archivo){
			var archivo = frmSubir.txtnombrearchivo.value
			if (frmSubir.txtnombrearchivo.value!="")	{
			//alert(codigo_cop)
			location.href="guardarinforme.asp?accion=eliminar&codigo_cni=" + codigo_cni + "&archivo=" + archivo			
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
<script language="JavaScript" src="../../../../../private/funciones.js"></script>


<body topmargin="0" leftmargin="0" rightmargin="0">

<%
codigo_cni=Request.QueryString("codigo_cni")
%>


		<form id="frmSubir" action="guardarinforme.asp?codigo_cni=<%=codigo_cni%>" method="post" enctype="multipart/form-data">
		<input name="doCreate" type="hidden" id="doCreate" value="true">
		  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="6%" height="25" class="bordeinf">&nbsp;&nbsp;<img src="../../../../../images/menus/attachfiles.gif" width="35" height="35"></td>
              <td width="94%" class="bordeinf Estilo4">&nbsp;&nbsp;Adjuntar Informe</td>
            </tr>
            <tr>
              <td colspan="2" valign="top"><table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0" bordercolor="#111111" style="border-collapse: collapse">
                <tr>
                  <td height="20" colspan="3"><input type="file" name="filename" size="40" class="cajas2" <%if modifica=2 then%> disabled="disabled" <%end if%>></td>
                </tr>
                <tr>
                  <td height="20" colspan="3" class="bordeinf Estilo5"><strong>Nombre</strong><span class="rojo">
                    &nbsp;&nbsp;<input name="txtdescripcion_prp" type="text" id="txtdescripcion_prp" value="" size="50" maxlength="55" <%if modifica=2 then%> disabled="disabled" <%end if%>>
                    <input name="txtnombrearchivo" type="hidden" id="txtnombrearchivo">
                  </span></td>
                </tr>
                <tr>
                  <td width="3%" class="rojo"><img src="../../../../../images/menus/NodoAbierto.GIF" width="14" height="10"><br></td>
                  <td height="20" colspan="2"><span class="Estilo7">Informe adjunto (01 archivo) </span></td>
                </tr>
                <tr>
                  <td colspan="3" align="left" valign="top" class="rojo">

                    <table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0">
                      <tr>
                        <td width="90%" height="100%" valign="top" bgcolor="#FFFFFF" class="contornotabla_azul">
						<table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                            <%
					
		if codigo_cni<>"" then
		Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objArchivo.AbrirConexion
			set rsInformePrp=objArchivo.Consultar("ConsultarConvenios","FO","PF",codigo_CNI,0,0)
			//set rsInformePrp=objArchivo.Consultar("ConsultarPropuestas","FO","CP",codigo_prp,"","","")

			
		objArchivo.CerrarConexion
		set objArchivo=nothing							
//			  		do while not rsArchivosPrp.eof
			  if rsInformePrp(0) <> "" then%>
                            <TR height="25" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" typ="Sel" onClick="ponervalortext('<%=rsInformePrp(0)%>')" >
                              <td  width="5%" align="left" valign="middle">&nbsp;</td>
                              <td width="15%" align="center">
							  <%if not isnull(rsInformePrp(1)) then%>
							  <a href="../../../../../convenios/<%=rsInformePrp(0)%>.pdf" target="_blank"> 
							  <img src="../../../../../images/ext/<%=right(rsInformePrp(1),3)%>.gif" width="16" height="16"  border="0">
							   </a> 
							  <%end if%>
							  </td>
                              <td width="80%" align="left"><%response.write(rsInformePrp(1))%></td>
                            </TR>
              <%end if
//			  	rsArchivosPrp.movenext()
//				loop						
	end if%>
                        </table></td>
                        <td width="10%" align="right" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td valign="top"><table width="100%" border="0" cellspacing="5" cellpadding="0">
                              <tr>
                                <td align="right"><input type="button" name="cmdEnviar" onClick="ValidarSilabos()" value=" Agregar" class="attach_prp" style="width: 85" <%if rsInformePrp(1)<>"" then%> disabled="disabled" <%end if%>></td>
                              </tr>
                              <tr>
                                <td align="right"><input type="button" name="cmdEnviar2" onClick="eliminar(<%=codigo_cni%>,this.value)" value="Quitar" class="remove_prp" style="width: 85" <%if modifica=2 then%> disabled="disabled" <%end if%>></td>
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
                  </table>				  </td>
                </tr>

              </table></td>
            </tr>	
          </table>
</form>
</body>
</html>

