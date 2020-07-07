<%
codigo_alu=request.querystring("codigo_alu")
codigo_pes=request.querystring("codigo_pes")
codigo_cur=request.querystring("codigo_cur")

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCursos=obj.Consultar("sp_VerRequisitos_alumno","FO",codigo_alu,codigo_pes,codigo_cur)
	obj.CerrarConexion
Set Obj=nothing

%>

<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Requisitos de la asignatura</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>

<body>

    <table style="width: 100%;">
        <tr>
            <td><p style="width: 80%" class="usatTitulo" >Detalle de Asignaturas requisito</p>
            </td>
            <td align="right">
                &nbsp;
                <img alt="" src="../../Images/cerrar.gif" onclick="javascript:self.parent.tb_remove();window.close();" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
<%
IF Not(rsCursos.BOF and rsCursos.EOF) then
%>
<table class="contornotabla" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="#FFFFFF">
  <tr class="usatCeldaTitulo">
    <td width="90%" bordercolor="#800000" style="background-color: #800000">Requisitos</td>
    <td width="10%" bordercolor="#800000" style="background-color: #800000">Observación</td>
  </tr>
  <%
  Do While Not rsCursos.EOF
   	
  %>
  <tr>
    <td width="90%">
    <%for i=0 to 3*(rsCursos("niv")-1)%>
    <img src="../../../images/blanco.gif">
    <%next%>
    <img src="../../../images/libroabierto.gif">   
    <%=rsCursos("requisito")%>&nbsp;</td>
    <td width="10%"><br><%=rsCursos("observacion")%>&nbsp;</br></td>    
 </tr>
  <%
  	rsCursos.movenext
  Loop
  %>
</table>
<%Else%>
<h4>No se han encontrado requisitos que evaluar para la asignatura seleccionada</h5>

<%end if

rsCursos.close
Set rsCursos=nothing
%>
</body>

</html>