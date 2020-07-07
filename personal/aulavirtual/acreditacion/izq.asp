<!--#include file="clsAcreditacion.asp"-->
<%
function abrirseccion(estado,id,nombre)
	if estado=1 then
		abrirseccion="abrirseccionmenu('" & id & "','" & nombre & "',this)"
	else
		abrirseccion="alert('No se han asignado indicadores de evaluación para acceder a la sección')"
	end if
end function

Set acreditacion=new clsacreditacion
	ArrSeccion=acreditacion.ConsultarEvaluacionModeloAcreditacion("1",session("idacreditacion"),0,0)
Set acreditacion=nothing

function cargarseccionelegida()
	idseccion=request.QueryString("idseccion")
	nombreseccion=request.QueryString("nombreseccion")
 
 	if idseccion<>"" then
		cargarseccionelegida="Onload=""abrirseccionmenu('" & idseccion & "','" & nombreseccion & "',secc" & idseccion & ")"""
	end if
end function
%>
<html>
	<head>
		<title>izq</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
		<meta name="ProgId" content="FrontPage.Editor.Document">
		<meta name="Originator" content="Microsoft Visual Studio.NET 7.0">
	    <base target="Contenido">
        <link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
        <script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
        <script language="JavaScript" src="private/validaracreditacion.js"></script>
        <style>
<!--
td           { border-left-width: 1; border-right-width: 1; border-bottom: 1px solid #808080 }
.bordederecho { border-right: 1px solid #808080 }
-->
        </style>
	</head>
	<body topmargin="0" leftmargin="0" class="menuizquierdo" <%=cargarseccionelegida%>>
	<%if IsEmpty(ArrSeccion)=true then%>
				<h5>No se han registrado secciones en el modelo de acreditación seleccionado</h5>
	<%else%>
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tblMenu">
	  <tr>
    	<td width="100%" valign="top" height="25" bgcolor="#E9E9E9" class="e1">&nbsp;Secciones</td>
      </tr>
      <%for i=lbound(ArrSeccion,2) to Ubound(ArrSeccion,2)%>
	      <tr id="secc<%=ArrSeccion(0,i)%>" onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="<%=abrirseccion(Arrseccion(5,I),Arrseccion(0,I),Arrseccion(1,I))%>">
    	    <td width="100%" valign="top" height="25"><b><%=Arrseccion(4,i)%></b>. <%=ArrSeccion(1,i)%></td>
      	</tr>
      <%next%>
    </table>
    <%end if%>
	</body>
</html>