<!--#include file="../../../funciones.asp"-->
<%
Dim contador

codigo_cpf=request.querystring("codigo_cpf")

set obj=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
	set rsEscuela=obj.ConsultarCarreraProfesional("RS","RE","")
set obj=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Planes de Estudio</title>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="private/validarplan.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <tr>
    <td width="40%" class="usattitulo">Planes de Estudio según Escuela Profesional</td>
    <td width="60%"><%call llenarlista("cboEscuela","actualizarlista('lstplanes.asp?codigo_cpf=' + this.value)",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccionar la Escuela Profesional","","")%></td>
  </tr>
  <%if trim(codigo_cpf)<>"-2" then%>
  <tr>
    <td width="100%" colspan="2">
    <%
	  Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanestudio")
		Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",codigo_cpf,"")
  	  Set objPlan=nothing
    %>
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr class="etabla">
        <td width="5%">&nbsp;</td>
        <td width="40%">Plan de Estudio</td>
        <td width="15%">Ciclo</td>
        <td width="15%">Créd. Obligatorios</td>
        <td width="15%">Créd. Electivos</td>
        <td width="20%">Total de horas</td>
        <td width="20%">Acciones</td>
      </tr>
	  <%Do while Not rsPlan.EOF
	  	j=j+1%>    
	  	<tr>
        <td width="5%"><%if rsPlan("cursos")>0 then%><img src="../../../images/mas.gif" id="img<%=rsPlan("codigo_pes")%>" onClick="MostrarTabla(tblplan<%=rsPlan("codigo_pes")%>,this,'../../../images/')"><%end if%>&nbsp;</td>
        <td width="40%"><%=rsPlan("descripcion_pes")%>&nbsp;</td>
        <td width="15%"><%=rsPlan("cicloAcadInicio_Pes")%>&nbsp;</td>
        <td width="15%"><%=rsPlan("totalCreObl_Pes")%>&nbsp;</td>
        <td width="15%"><%=rsPlan("totalCredElecObl_Pes")%>&nbsp;</td>
        <td width="20%"><%=rsPlan("totalHoras_Pes")%>&nbsp;</td>
        <td width="20%">
        <img src="../../../images/guardar.gif" name="cmdGuardar" ALT="Guardar cambios realizados" onClick="Procesarcursos('G',fralista<%=rsPlan("codigo_pes")%>.document.all.frmlistacursos,'<%=rsPlan("codigo_pes")%>')">
        <img src="../../../images/ok.gif" name="cmdActivar" ALT="Activar vigencia a los cursos seleccionados" onClick="Procesarcursos('A',fralista<%=rsPlan("codigo_pes")%>.document.all.frmlistacursos,'<%=rsPlan("codigo_pes")%>')">
        <img src="../../../images/eliminar.gif" name="cmdDesactivar" ALT="Quitar vigencia a los cursos seleccionados" onClick="Procesarcursos('D',fralista<%=rsPlan("codigo_pes")%>.document.all.frmlistacursos,'<%=rsPlan("codigo_pes")%>')">
        </td>
        </tr>
        <%if rsPlan("cursos")>0 then%>
        <tr style="display:none" id="tblplan<%=rsPlan("codigo_pes")%>"><td colspan="7" width="100%" align="right">
		<iframe id="fralista<%=rsPlan("codigo_pes")%>" name="fralista<%=rsPlan("codigo_pes")%>" src="lstcursosplan.asp?codigo_pes=<%=rsPlan("codigo_pes")%>" width="100%" scrolling="no">
        El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
		</td>
		</tr>
		<%end if%>
      	<%rsPlan.movenext
      Loop%>
    </table>
	</td>
  </tr>
  <%end if%>
</table>
</body>
</html>