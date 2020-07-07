<!--#include file="../../../../funciones.asp"-->

<%
codigo_cco=request.querystring("codigo_cco")

Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsLugar=Obj.Consultar("ConsultarLugar","FO",1,0,0,0)
		Set rsEditorial=Obj.Consultar("ConsultarEditorial","FO",1,"%%",0)
		Set rsEspecialidad=Obj.Consultar("ConsultarEspecialidad","FO",5,0,0,0)
		Set rsTipoMaterial=Obj.Consultar("ConsultarTipoMaterial","FO",1,"","")
		Set rsCaractEdicion=Obj.Consultar("BI_ConsultarCaracteristicaEdicion","FO","CB","")		
	Obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es"/>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Registrar nueva bibliografía</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarpedido.js"></script>
<script>

function BuscarAutor(nombre,codigo_cco){
	frmcatalogo.action="frmagregarbibliografia.asp?accion=buscarautor&codigo_cco="+codigo_cco
	frmcatalogo.submit()
}

function GuardarAutor(nombre,codigo_cco){
if (document.all.autor_cat.value!=""){
	frmcatalogo.action="frmagregarbibliografia.asp?accion=guardarautor&nombre="+nombre+"&codigo_cco="+codigo_cco
	frmcatalogo.submit()
	}
else{
	alert("Ingrese los datos del autor en la caja de texto correspondiente.")
}
}

function CancelarBusquedaAutor(nombre,codigo_cco)
{
	frmcatalogo.action="frmagregarbibliografia.asp?accion=cancelarbusqueda&nombre="+nombre+"&codigo_cco="+codigo_cco
	frmcatalogo.submit()
}

</script>

<style type="text/css">
<!--
.Estilo1 {color: #000000}
.Estilo2 {color: #0033FF}
.Estilo4 {
	color: #000000;
	font-size: 9px;
	font-weight: bold;
}
.Estilo6 {color: #000000; font-size: 9px; }
-->
</style>
</head>
<%
titulo_cat= Request.Form("titulo_cat")
if titulo_cat="" then titulo_cat=Request.QueryString("titulo_cat")
autor_cat= Request.Form("autor_cat")
cboautor_cat=Request.Form("cboautor_cat")
cbomateria_cat= Request.Form("cbomateria_cat")
if cbomateria_cat="" then
	cbomateria_cat=0
end if

cboeditorial_cat= Request.Form("cboeditorial_cat")

if cboeditorial_cat="" then
	cboeditorial_cat=609
end if

cbotipoMaterial_cat= Request.Form("cbotipoMaterial_cat")

if cbotipoMaterial_cat="" then
	cbotipoMaterial_cat=1
end if

NumEdic= Request.Form("NumEdic")
cboCaractEdic_cat= Request.Form("cboCaractEdic_cat")
if cboCaractEdic_cat="" then
	cboCaractEdic_cat=9
end if


lugar_cat= Request.Form("lugar_cat")
cbolugar_cat= Request.Form("cbolugar_cat")
if cbolugar_cat=""then
	cbolugar_cat=1
end if

edicion_cat= Request.Form("edicion_cat")
isbn_cat= Request.Form("isbn_cat")

if Request.QueryString("moneda_cat")="" then
	cbomoneda_cat= Request.Form("cbomoneda_cat")
else
	cbomoneda_cat=Request.QueryString("moneda_Cat")
end if


preciounit_cat= Request.Form("preciounit_cat")
if preciounit_cat = ""then preciounit_cat=Request.QueryString("preciounit_Cat")
obs_cat= Request.Form("obs_cat")


NumAut=0

if Request.QueryString("accion")="guardarautor" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
		Obj.Ejecutar "PED_RegistraAutor",false,autor_cat
		Obj.CerrarConexion
	Set Obj=nothing
	
end if

if autor_cat <> "" then
	if Request.QueryString("accion")="cancelarbusqueda" then
		NumAut= 0
		set RsAutor =nothing
	else
		Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
			Obj.AbrirConexion
			set	RsAutor=Obj.Consultar("ConsultarAutor","FO","1",autor_cat,"","")
			Obj.CerrarConexion
		Set Obj=nothing
		NumAut= RsAutor.RecordCount		
		if NumAut=0 then
			Response.Write("<script>alert('Seleccione un autor(es) según las instrucciones, caso contrario regístrelo.')</script>")
		end if
	end if
end if

obs_cat=Request.Form("obs_cat")

%>

<body style="background-color: #F0F0F0">

<form name="frmcatalogo" method="post">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolor="0">
  <tr>
    <td width="35%"><input name="cmdGuardar2" type="button" value="        Guardar" class="guardar_prp"
	
	onClick="ValidarNvaBibliografia(<%=codigo_cco%>)">
      <input name="cmdCerrar" type="button" value="       Cerrar" class="noconforme1">
	<% '' Response.Write(codigo_cco) %>
	  </td>
    <td width="65%"><table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td><span class="Estilo4">Instrucciones para b&uacute;squeda de autor:</span></td>
      </tr>
      <tr>
        <td><span class="Estilo6">Consulte el autor y seleci&oacute;nelo de la lista, en caso no estuviera registrado, escriba los nombres del autor (es) (Apellido, Nombre;) y haga clic en guardar. </span></td>
      </tr>

    </table></td>
  </tr>
</table>
<table width="100%" height="90%"  bgcolor="white" class="contornotabla">
	<tr>
		<td height="5px"></td>
		<td height="5px"></td>
	</tr>
	<tr>
		<td width="83"><span class="Estilo1">Título<font color="#FF0000">*</font></span></td>
	    <td colspan="3"><input name="titulo_cat" type="text" class="Cajas2" value="<%=titulo_cat%>" maxlength="255" /></td>
	</tr>
	<tr>
	  <td><span class="Estilo1">Autor (es)<font color="#FF0000">*</font> </span></td>
	  <td colspan="3"><table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="30%"><input name="autor_cat" type="text" class="Cajas2" id="autor_cat" value="<%=autor_cat%>" maxlength="180" /></td>
          <td width="24%">
		  <span style="width: 5%; height: 5px;"><img src="../../../../images/menus/buscar_small.gif" name="imgBuscar" class="imagen" onClick="BuscarAutor('','<%=codigo_cco %>')"></span>
		  <span class="Estilo1" onClick="BuscarAutor('','<%=codigo_cco %>')" style="cursor:HAND"><strong>Buscar</strong></span>
		  <span onClick="CancelarBusquedaAutor('','<%=codigo_cco %>');" style="cursor:hand"><img src="../../../../images/cerrar.gif" onClick="CancelarBusquedaAutor();" width="20" height="20"></span> 
		  <span onClick="CancelarBusquedaAutor('','<%=codigo_cco %>')" style="cursor:hand"><strong>Cancelar</strong></span></td>
        </tr>
      </table></td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td colspan="3" valign="top">
        <span class="Estilo1">
        <%
		if NumAut >0 then
		%>
		Seleccione un autor de la lista.
        <%
		call llenarlista("cboautor_cat","AlmacenarTextoLista(frmcatalogo.autor_cat,this)",RsAutor,"idAutor","NombreAutor",cboautor_cat,"","","Multiple")
		else
		Response.Write("¿Desea registrar el o los autores?")
		%>
        <span class="Estilo2">(Ejemplo: Zeithaml, Valarie A.; Parasuraman, A.; Berry, Leonard L.)</span>        <img src="../../../../images/guardar.gif" width="16" height="16" style="cursor:hand" alt="Guardar nombre de autor (es)" onClick="GuardarAutor(document.all.autor_cat.value,'<%=codigo_cco%>')" ><span onClick="GuardarAutor(document.all.autor_cat.value,'<%=codigo_cco%>')" style="cursor:hand"><strong>Guardar </strong></span>
        <%end if
		%>
        </span></td>
    </tr>
	<tr>
		<td width="83"><span class="Estilo1">Editorial</span></td>
		<td colspan="3">
		<input name="editorial_cat" type="hidden" value="<%=cboeditorial_cat%>" />
		<%call llenarlista("cboeditorial_cat","AlmacenarTextoLista(frmcatalogo.editorial_cat,this)",rsEditorial,"IdEditorial","NombreEditorial",cboeditorial_cat,"","","")%></td>
	</tr>
	<tr>
	  <td><span class="Estilo1">Tipo Material<font color="#FF0000">*</font> </span></td>
	  <td><input name="tipoMaterial_Cat" type="hidden" value="1" />
        <%call llenarlista("cbotipoMaterial_cat","AlmacenarTextoLista(frmcatalogo.tipoMaterial_Cat,this)",rsTipoMaterial,"IdMaterial","NombreMaterial",cbotipoMaterial_cat,"","","")%></td>
	  <td align="right"><span class="Estilo1">Edici&oacute;n</span></td>
	  <td><table width="100%" border="0">
        <tr>
          <td width="9%"><label>
          <input name="NumEdic" type="text" id="NumEdic" value="<%=NumEdic%>" size="3" maxlength="2">
          </label></td>
          <td width="91%"><input name="CaractEdicion_Cat" type="hidden" id="CaractEdicion_Cat" value="9" />
            <%call llenarlista("cboCaractEdic_cat","AlmacenarTextoLista(frmcatalogo.CaractEdicion_Cat,this)",rsCaractEdicion,"idcaractedicion","abreviatura",cboCaractEdic_cat,"","","")%></td>
        </tr>
      </table></td>
    </tr>
	<tr>
		<td width="83"><span class="Estilo1">Lugar</span></td>
		<td width="378" >
		<input name="lugar_cat" type="hidden" value="<%=cbolugar_cat%>" />
		<%call llenarlista("cbolugar_cat","AlmacenarTextoLista(frmcatalogo.lugar_cat,this)",rsLugar,"IdLugar","NombreLugar",cbolugar_cat,"","","")%>		</td>
		
		<td width="188" align="right"><span class="Estilo1">Año publicación</span></td>
		<td width="354">
		<input name="edicion_cat" type="text" class="Cajas" onKeyPress="validarnumero()" value="<%'=edicion_cat%>" maxlength="4" /></td>
	</tr>
	<tr>
		<td width="83"><span class="Estilo1">ISBN</span></td>
		<td width="140"><input name="isbn_cat" type="text" class="Cajas2" value="<%=isbn_cat%>" maxlength="20" /></td>
		
		<td align="right"><span class="Estilo1">Precio Unitario</span></td>
		<td><select name="cbomoneda_cat" class="Cajas">
		<option value="S" <%if cbomoneda_cat="S" then %> selected="selected" <%end if%>>S/.</option>
		<option value="$" <%if cbomoneda_cat="$" then %> selected="selected" <%end if%>>$</option>
		<option value="E" <%if cbomoneda_cat="E" then %> selected="selected" <%end if%>>E</option>
		<%preciounit_cat=replace(preciounit_cat, ",",".") %>
		</select><input name="preciounit_cat" type="text" class="Cajas" onKeyPress="validarnumero()" value="<%=preciounit_cat%>" size="5" maxlength="5" /></td>
	</tr>
	<tr>
	  <td style="visibility:visible" ><span class="Estilo1">Observaciones</span></td>
	  <td style="visibility:visible" colspan="3"><textarea name="obs_cat" rows="3" class="Cajas2" id="obs_cat" value="<%=autor_cat%>"></textarea></td>
    </tr>
	<tr>
      <td style="visibility:hidden" ><span class="Estilo1">Materia</span></td>
	  <td style="visibility:hidden" colspan="3"><input name="materia_cat" type="hidden" value="<?=cbomateria_cat?>" />
          <%call llenarlista("cbomateria_cat","AlmacenarTextoLista(frmcatalogo.materia_cat,this)",rsEspecialidad,"IdEspecialidad","NombreEspecialidad",cbomateria_cat,"Seleccione la Materia de clasificaci&oacute;n","","")%>      </td>
    </tr>

</table>
</form>
</body>
</html>
