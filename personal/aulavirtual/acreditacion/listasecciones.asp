<!--#include file="clsAcreditacion.asp"-->
<!--#include file="../clsgrafico.asp"-->
<%

if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

dim acreditacion
dim totalreg
'dim arrEtiquetas()
'dim arrValores()

function abrirseccion(estado,id,nombre)
	if estado=1 then
		abrirseccion="location.href='fravariables.asp?idseccion=" & id & "&nombreseccion=" & ArrSeccion(1,i) & "'"
	else
		abrirseccion="alert('No se han asignado indicadores de evaluación para acceder a la sección')"
	end if
end function

Set acreditacion=new clsacreditacion
	ArrSeccion=acreditacion.ConsultarEvaluacionModeloAcreditacion("1",session("idacreditacion"),0,0)
%>
<HTML>
	<HEAD>
		<title>listasecciones</title>
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
			<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
	</HEAD>
	<body bgcolor="#F8E3C9" background="images/lineas.gif">
		<center>
		<%if IsEmpty(ArrSeccion)=true then%>
			<h5>No se han registrado secciones en el modelo de acreditación seleccionado</h5>
		<%else
			totalreg=int(Ubound(ArrSeccion,2)+1)
			totalfilas=int((totalreg-1)/2)
		%>
		<br>
       	<table width="95%" height="80%" border="0" style="border-collapse: collapse" bordercolor="#111111" cellpadding="0" cellspacing="0">
          <tr>
            <td width="30%" ></td>
            <td width="30%" >
            <%
            '---------------------------------------------            
            'Llenar las secciones al lado central superior
            '---------------------------------------------
            %>
            <table width="95%" border="0" cellpadding="1" cellspacing="0" >
              <tr <%'=acreditacion.fondoseccion(Arrseccion(5,0),Arrseccion(3,0))%>>
                <td width="5%"><%=acreditacion.ImagenAlertaSeccion(Arrseccion(5,0),Arrseccion(3,0))%>&nbsp;</td>
                <td class="contorno" width="95%" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="<%=abrirseccion(Arrseccion(5,0),Arrseccion(0,0),Arrseccion(1,0))%>" width="88%">&nbsp;<b><%=Arrseccion(4,i)%></b>. <%=ArrSeccion(1,0)%>&nbsp;</td>
              </tr>
            </table>
            </td>
            <td width="30%"></td>
          </tr>
          <tr>
            <td width="30%" >
            <%
            '---------------------------------------------            
            'Llenar las secciones al lado izquierdo
            '---------------------------------------------
            for i=1 to totalfilas
            	valor1=formatnumber(ArrSeccion(3,i),1)%>
					<table width="95%" border="0" cellpadding="1" cellspacing="0" >
						<tr <%'=acreditacion.fondoseccion(Arrseccion(5,i),Arrseccion(3,i))%>>
							<td width="5%"><%=acreditacion.ImagenAlertaSeccion(Arrseccion(5,i),valor1)%>&nbsp;</td>
							<td class="contorno" width="95%" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="<%=abrirseccion(Arrseccion(5,I),Arrseccion(0,I),Arrseccion(1,I))%>">&nbsp;<b><%=Arrseccion(4,i)%></b>. <%=ArrSeccion(1,i)%></td>
						</tr>
					</table>
					<br>
            <%next%>
            </td>
            <td height="8" class="e1" align="center" width="30%">
            <img border="0" src="images/portada.gif">
            <BR><%=acreditacion.graficoavanceacreditacion(1,80)%>
            </td>
            <td width="30%" >
            <%
            '---------------------------------------------            
            'Llenar las secciones al lado derecho
            '---------------------------------------------            
            for j=i to (totalfilas * 2)
            	valor2=formatnumber(ArrSeccion(3,j),1)%>
					<table width="95%" border="0" cellpadding="1" cellspacing="0" >
						<tr <%'=acreditacion.fondoseccion(Arrseccion(5,j),Arrseccion(3,j))%>>
							<td class="contorno" width="95%" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="<%=abrirseccion(Arrseccion(5,J),Arrseccion(0,J),Arrseccion(1,J))%>">&nbsp;<b><%=Arrseccion(4,j)%></b>. <%=ArrSeccion(1,j)%></td>
							<td width="5%"><%=acreditacion.ImagenAlertaSeccion(Arrseccion(5,j),valor2)%>&nbsp;</td>
						</tr>
					</table>
					<br>
            <%next%>
            </td>
          </tr>
          <%
            '---------------------------------------------            
            'Llenar las secciones al lado central inferior
            '---------------------------------------------
          filarestante=totalreg mod 2
          
          if (filarestante=0) then
          	valor3=formatnumber(ArrSeccion(3,j),1)%>
          <tr>
            <td width="30%" ></td>
            <td width="30%" >
					<table width="95%" border="0" cellpadding="1" cellspacing="0" >
						<tr <%'=acreditacion.fondoseccion(Arrseccion(5,j),Arrseccion(3,j))%>>
							<td width="5%"><%=acreditacion.ImagenAlertaSeccion(Arrseccion(5,j),valor3)%>&nbsp;</td>
							<td class="contorno" width="95%" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="<%=abrirseccion(Arrseccion(5,J),Arrseccion(0,J),Arrseccion(1,J))%>"><b><%=Arrseccion(4,j)%></b>. <%=ArrSeccion(1,j)%></td>
						</tr>
					</table>
            </td>
            <td width="30%" ></td>
          </tr>
          <%end if%>
        </table>
        </center>
        <%end if%>
	</body>
</HTML>
<%Set acreditacion=nothing%>