<!--#include file="clsAcreditacion.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

dim acreditacion
dim totalreg

idseccion=request.QueryString("idseccion")
nombreseccion=request.QueryString("nombreseccion")

Set acreditacion=new clsacreditacion
	with acreditacion
		ArrVariable=.ConsultarEvaluacionModeloAcreditacion("2",idseccion,0,0)
	end with
%>
<HTML>
	<HEAD>
		<title>listasecciones</title>
		<meta name="vs_showGrid" content="True">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
			<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
	</HEAD>
	<body>
		<center>
			<%if IsEmpty(ArrVariable)=true then%>
			<h5>No se han registrado variables en la sección <%=ucase(nombreseccion)%></h5>
			<%else
				totalreg=int(Ubound(ArrVariable,2))%>
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="colorbarra">
          	<tr>
            	<td class="e1">&nbsp;Lista de variables en proceso de evaluación (<%=totalreg+1%>)</td>
        	    <td style="cursor:hand" onClick="AbrirTodasTablas('../images/',AbrirTodo,'tblVariables')" align="right"><IMG id="AbrirTodo" SRC="../../../images/desplegar.gif">&nbsp;Mostrar todo&nbsp;</td>
    	      </tr>
	        </table>
			<br>
			<TABLE cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse;border: 1px solid #808080" ID="tblVariables">
				<TR class="etabla">
					<TD></TD>
					<TD align="left">Descripción de Variables</TD>
					<TD colspan="2">&nbsp;</TD>
				</TR>
				<%for i=lbound(ArrVariable,2) to Ubound(ArrVariable,2)%>
				<TR onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
					<TD valign="top"><IMG SRC="../../../images/final.gif"></TD>
					<TD width="90%"><b><i>VARIABLE <%=ArrVariable(4,i)%></i></b>: <%=ArrVariable(1,i)%></TD>
					<TD align="right"><%=acreditacion.ImagenAlerta(ArrVariable(3,i))%></TD>
				</TR>
					<%call acreditacion.MostrarAtributosEvaluados(ArrVariable(0,i))
				next%>									
			</TABLE>
			<%end if%>
		</center>
		</body>
</HTML>
<%Set acreditacion=nothing%>