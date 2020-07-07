<%
dim ArrAplicacion
if session("codigo_usu")="" then response.redirect "../../tiempofinalizado.asp"

	Set ObjUsuario= Server.CreateObject("AulaVirtual.clsDatAplicacion")
		ArrAplicacion=ObjUsuario.ConsultarAplicacionUsuario("10",session("tipo_usu"),session("codigo_usu"),"")
	Set ObjUsuario=nothing

if IsEmpty(ArrAplicacion)=true then%>
	<script language="Javascript">
		var mensaje="Lo sentimos Ud. no tiene acceso a las Aplicaciones del Campus Virtual"
		mensaje=mensaje + "\nPara cualquier consulta Contáctese con el email: gchunga@usat.edu.pe"
		alert(mensaje)
		top.window.opener=self
		top.window.close()
	</script>
<%else
	tipo=session("codigo_apl")
	select case tipo
		case 1
			tipo=0
		case else
			tipo=1
	end select
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Página Principal del Campus Virtual</title>
<META Http-Equiv="Cache-Control" Content="no-cache">
<META Http-Equiv="Pragma" Content="no-cache">
<META Http-Equiv="Expires" Content="0">
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../private/funcionesaulavirtual.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<script>
	var aplicacionBD = new Array()

	function ObjAplicacion(nmat,tapl,tfu,IDvalor,dapl,eapl)
	{
		this.nummat=nmat
	  	this.tipoapl = tapl
	  	this.tipotfu = tfu
	  	this.ID = IDvalor 	
	  	this.descripcion = dapl
	  	this.enlace = eapl
	}
	
	function AgregarItemObjeto()
	{	
		<%for i=lbound(ArrAplicacion,2) to Ubound(ArrAplicacion,2)
			response.write "aplicacionBD[aplicacionBD.length] = new ObjAplicacion('" & ArrAplicacion(9,i) & "','" & ArrAplicacion(2,i) & "','" & ArrAplicacion(7,i) & "','" & ArrAplicacion(6,i) & "','" & ArrAplicacion(8,i) & "','" & replace(ArrAplicacion(0,i),"/","-") & "')" & vbNewLine & vbtab & vbtab
		next%>
	}

	function AbrirAplicacion(capl)
	{
		ResaltarTab(capl)
		AgregarItemObjeto()
		var i=parseFloat(capl)
		var nmat=aplicacionBD[i].nummat
		var tapl=aplicacionBD[i].tipoapl
		var tfu=aplicacionBD[i].tipotfu
		var capl=aplicacionBD[i].ID
		var dapl=aplicacionBD[i].descripcion
		var eapl=aplicacionBD[i].enlace
	
		if (tapl>1){
			top.location.href="abriraplicacion.asp?codigo_tfu=" + tfu + "&codigo_apl=" + capl + "&enlace_apl=" + eapl + "&tipo_apl=" + tapl
		}
		else{
			var tbl=document.getElementById("tbl" + capl)
			mensaje.innerHTML="<b><font color='#FF0000'>&nbsp;Cargando...</font></b>"

			document.fralista.location.href="cursovirtual/listacursos.asp?tipo_apl=" + tapl + "&codigo_tfu=" + tfu + "&codigo_apl=" + capl + "&descripcion_apl=" + dapl + "&enlace_apl=" + eapl
			spTexto.innerHTML=dapl
			mensaje.innerHTML=".."
		}
	}
	
	function ResaltarTab(numcol)
	{
		for (var c = 0; c < tab.length; c++){
			var Celda=tab[c]	
			if(numcol==c){
				Celda.style.backgroundColor = "#FFCC66"
				//Celda.className="menuresaltado"
			}
			else{
				Celda.style.backgroundColor = "#A4C2C2"
				//Celda.className="menubloqueado"
			}
		}
	}
</script>
<style>
<!--
.menuBar     { border-top: 2px solid #999966; border-bottom: 2px solid #999966; 
               background-color: #A4C2C2 }
.navText {
	border-right:1px solid #808080; color: #26354A;
	letter-spacing:0.0em;
	line-height:16px; font-style:normal; font-variant:normal; font-weight:normal; font-size:10px; font-family:Arial, Helvetica, sans-serif; border-left-width:1; border-top-width:1; border-bottom-width:1;
	cursor:hand
	}
-->
</style>
</head>
<body topmargin="0" leftmargin="0" onLoad="AbrirAplicacion('<%=tipo%>')" background="../../images/fondogris.gif">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr>
    <td width="70%" height="80" colspan="2" background="../../images/logoaulavirtual.jpg">&nbsp;</td>
  </tr>
  <tr>
    <td width="70%" class="franja" height="20" style="text-align: left">&nbsp; USUARIO: <%=session("nombre_usu")%> /&nbsp;</td>
    <td width="30%" class="franja" height="20" ><%=formatdatetime(now,1)%>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" height="25" colspan="2">
    <table border="0" cellpadding="0" cellspacing="0"  style="border-collapse: collapse" bordercolor="#111111" width="100%" class="menuBar" height="100%">
        <tr>
  		<%for j=lbound(ArrAplicacion,2) to Ubound(ArrAplicacion,2)%>
	        <td class="navText" id="tab" onclick="AbrirAplicacion('<%=j%>')" align="center" nowrap="nowrap" width="20%">
	        <img src="<%=ArrAplicacion(1,j)%>" align="absmiddle">&nbsp;<%=UCASE(ArrAplicacion(3,j))%>&nbsp;</td>
	    <%next%>
	    	<%if session("tipo_usu")<>"T" then%>
	    	<td class="navText" nowrap="nowrap" width="20%" align="center" onclick="top.location.href='../acceder.asp?cbxtipo=E'">
			<img src="../../images/cerrar.gif" align="absmiddle">&nbsp;IR A MENÚ PRINCIPAL</td>
			<%end if%>
	    	<td align="center" nowrap="nowrap" width="15%">&nbsp;</td>
        </tr>
    </table>
    </td>
  </tr>
  <tr>
  <td align="center" nowrap="nowrap" width="100%" colspan="2">&nbsp;</td>
  </tr>
</table>
<div align="center">
  <center>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="98%" height="78%" class="contorno">
    <tr>
    <td width="22%" height="98%" bgcolor="#A4C2C2" valign="top">
    <img border="0" src="../../images/imgaula.jpg" width="100%" height="45%">
    <p id="mensaje"></p>
    <ul type="circle">
      <li id="spTexto">&nbsp;</li>
    </ul>
    </td>
    <td width="78%" class="contorno" height="98%" valign="top">
    <iframe name="fralista" height="100%" width="100%" border="0" frameborder="0" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  	</tr>
	</table>
	</center>
</div>
	</td>
  </tr>
</table>
</body>
</html>
<%end if%>