<!---#include file="../../NoCache.asp"-->
<html>
<head>
<title>Campus Virtual USAT: <%=session("descripcion_apl")%></title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script src="configmenu.js"></script>
<script type="text/javascript">
timeOut = 3000;
menuBarBgColor = "#DEE0C5" //"#ECE9D8";
subMenuBdrColor = "#8A867A";
subMenuGfxBgColor = "#EFEDDE";
keepMenuBarOnTop = true;
bgColorArray = new Array("#DEE0C5", "#C1D2EE", "#EFEDDE");
bdrColorArray = new Array("#DEE0C5", "#316AC5","#8A867A #8A867A #FCFCF9");
subMenuBgColor = new Array("#FFFFFF", "#C1D2EE", "#C1D2EE");

imagePath = "../../images/";

subMenuItemBdrColor = new Array("#EFEDDE", "#FFFFFF", "#FFFFFF");

TDpart1 = "#316AC5 "+subMenuBgColor[1]+" #316AC5 #316AC5";
TDpart2 = "#316AC5 "+subMenuBgColor[1];
TDpart3 = "#316AC5 #316AC5 #316AC5 "+subMenuBgColor[1]+"";

subMenuItemSelectedBdrColor = new Array(TDpart1, TDpart2, TDpart3);

//crear menú dinamicamente
<%

function verificanulo(byval variable)
		if (IsNull(variable)=true or variable="") then
			verificanulo="null"
		else
			verificanulo="""" & variable & """"
		end if
end function

'----------------------------------------------------------------------------------------
'Cargar submenús (NIVEL 1)---------------------------------------------------------------
'----------------------------------------------------------------------------------------
function CargarMenuNivel2(byval codigo_apl,Byval codigo_tfu,Byval codigo_men)
	Dim rsSubMenu
	
	Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
		Set rsSubMenu=Obj.ConsultarAplicacionUsuario("RS","11",codigo_apl,codigo_tfu,codigo_men)
	Set Obj=nothing

	Do While not rsSubMenu.eof
		variable_men=rsSubMenu("variable_men")
		descripcion_men=rsSubMenu("descripcion_men")
		icono_men=rsSubMenu("icono_men")
		iconosel_men=rsSubMenu("iconosel_men")
		nivel_men=rsSubMenu("nivel_men")
		expandible_men=rsSubMenu("expandible_men")
		enlace_men=rsSubMenu("enlace_men")		

		icono_men=verificanulo(icono_men)
		iconosel_men=verificanulo(iconosel_men)
		enlace_men=verificanulo(enlace_men)	
			
		'Cargar submenú
		if descripcion_men="sep" then
			response.write "AgregarSubMenu('sep');" & vbcrlf
		else
			response.write "AgregarSubMenu('" & variable_men & "','" & descripcion_men &  "'," & icono_men & ","  & iconosel_men &  "," & enlace_men & ",'" & expandible_men & "');" & vbcrlf
		end if
		rsSubMenu.movenext
	Loop
	rsSubMenu.close
	Set rsSubMenu=nothing
end function

'----------------------------------------------------------------------------------------
'Cargar los menús principales (NIVEL 1)--------------------------------------------------
'----------------------------------------------------------------------------------------
function CargarMenuNivel1(byval codigo_apl,Byval codigo_tfu,Byval codigo_men)
	Dim rsMenu
	Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
		Set rsMenu=Obj.ConsultarAplicacionUsuario("RS","11",codigo_apl,codigo_tfu,codigo_men)
	Set Obj=nothing

	if rsMenu.recordcount>0 then
		Do While not rsMenu.eof
			variable_men=rsMenu("variable_men")
			descripcion_men=rsMenu("descripcion_men")
			icono_men=rsMenu("icono_men")
			iconosel_men=rsMenu("iconosel_men")
			nivel_men=rsMenu("nivel_men")
			expandible_men=rsMenu("expandible_men")
			enlace_men=rsMenu("enlace_men")
			if nivel_men="1" then
				nivel_men="true"
			else
				nivel_men="null"
			end if
			
			'Cargar menús padre e hijos y cierra menú
			response.write "AgregarMenu('" & variable_men & "','" & descripcion_men & "','" & nivel_men & "','" & expandible_men & "');" & vbcrlf
			if rsMenu("total_men")>0 then
				call CargarMenuNivel2(codigo_apl,codigo_tfu,rsMenu("codigo_men"))
			end if
			response.write "FinalizarMenu();" & vbcrlf
			rsMenu.movenext
		Loop
	else
		response.write "alert('No se ha definido menús para la aplicación');history.back(-1)" & vbcrlf
	end if
	rsMenu.close
	Set rsMenu=nothing
end function

'----------------------------------------------------------------------------------------
'Cargar menú del submenú (NIVEL 3)--------------------------------------------------
'----------------------------------------------------------------------------------------
function CargarMenuNivel3(byval codigo_apl,Byval codigo_tfu)
	Dim rsMenu
	Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
		Set rsMenu=Obj.ConsultarAplicacionUsuario("RS","12",codigo_apl,codigo_tfu,0)
	Set Obj=nothing

	if rsMenu.recordcount>0 then
		Do While not rsMenu.eof
			codigo_men=rsMenu("codigo_men")
			variable_men=rsMenu("variableprincipal_men") & "_" & rsMenu("variable_men")
			
			'Cargar el nombre de la variable del submenú
			response.write "AgregarMenu('" & variable_men & "',null,null,true);" & vbcrlf
			'Cargar lista de menús del submenú
			call CargarMenuNivel2(codigo_apl,codigo_tfu,rsMenu("codigo_men"))
			response.write "FinalizarMenu();" & vbcrlf
			rsMenu.movenext
		Loop
	end if
	rsMenu.close
	Set rsMenu=nothing
end function

call CargarMenuNivel1(session("codigo_apl"),session("codigo_tfu"),0)
call CargarMenuNivel3(session("codigo_apl"),session("codigo_tfu"))
%>
FinalizarHTMLMenu();

function cerrarpagina()
{
	fraMDIchild.location.href="about:blank"
}

function SalirSistema()
{
	if (confirm("¿Está seguro que desea salir del Sistema")==true){
		top.window.close()
	}
}
</script>

<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
</head>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" bgcolor="#DEE0C5">
<script>GenerateCode();</script>
<p style="margin-top: 10; margin-bottom: 0">&nbsp;</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="98%">
  <tr>
    <td width="100%" height="5%" style="text-align: left" valign="top">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%" class="bordesup">
  <tr>
    <td width="3%" valign="bottom">
    <a href="../listaaplicaciones.asp">
    <img border="0" src="../../images/inicio.gif" alt="Página principal"></a></td>
    <td>
    <img border="0" src="../../images/back.gif" alt="Página anterior" onClick="history.back(-1)" align="absmiddle"></td>
    <td width="3%">
    <img border="0" src="../../images/forward.gif" alt="Página siguiente" onClick="history.go(+1)" align="top"></td>
    <td width="3%" valign="bottom">
    <img style="cursor:hand" border="0" src="../../images/cerrar.gif" onclick="SalirSistema()"></td>
    <td width="95%" valign="bottom">&nbsp;</td>
  </tr>
</table>
    </td>
  </tr>
  <tr>
    <td width="100%" height="1%" style="text-align: left" valign="top"><img border="0" src="../../images/IN.jpg" width="100%" height="4"></td>
  </tr>
  <tr>
    <td width="100%" height="85%" style="text-align: left" valign="top">
<iframe name="fraMDIchild" height="100%" width="100%" border="0" frameborder="0" src="portada.asp">
El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
    </td>
  </tr>
  <tr>
    <td width="70%" class="franja" height="5%" style="text-align: left">&nbsp; <b>USUARIO: <%=session("nombre_usu")%></b></td>
    <!--<td width="25%" class="franja" height="5"><%=formatdatetime(now,1)%></td>-->
  </tr>
</table>
</body>
</html><%'end if%>