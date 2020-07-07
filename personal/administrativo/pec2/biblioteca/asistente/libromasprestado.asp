<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />

<html>
<style type="text/css">
<!--
.Estilo1 {
	color: #006699;
	font-weight: bold;
	font-size: 12px;
}
.Estilo2 {
	color: #000000;
	font-weight: bold;
}
.Estilo5 {color: #FFFFFF; font-weight: bold; }
-->
</style>
<script language="JavaScript" src="../../../../funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script>
function Exportar(){
	frmBuscaLibro.action="XLS_libromasprestado.asp"
	frmBuscaLibro.submit()
}
</script>
<head>

<title>Consulta de Material Bibliográfico más solicitado</title>
<%
libro=Request.Form("TxtTitulo")
fechaInicio=Request.Form("txtFechaInicio")
fechaFin=Request.Form("txtFechaFin")
%>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>

<body>
<form method="post" name="frmBuscaLibro" id="frmBuscaLibro" action="libromasprestado.asp">
  <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td align="center"><span class="Estilo1">Consulta de Material Bibliogr&aacute;fico m&aacute;s solicitado </span></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="33%"><span class="Estilo2">T&iacute;tulo</span></td>
            <td width="3%"><span class="Estilo2">:</span></td>
            <td width="64%"><input name="TxtTitulo" type="text" id="TxtTitulo" value="<%=libro%>"></td>
          </tr>
          <tr>
            <td><span class="Estilo2">Fecha Inicio </span></td>
            <td><span class="Estilo2">:</span></td>
            <td width="39%" bgcolor="#FFFFFF"><span class="Estilo8">
              <input  name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>">
              <input name="Submit2" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" >
            </span></td>
          </tr>
          <tr>
            <td><span class="Estilo2">Fecha Fin</span></td>
            <td><span class="Estilo2">:</span></td>
            <td bgcolor="#FFFFFF"><span class="Estilo8">
              <input name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechaFin%>">
              <input name="Submit3" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaFin')" >
            </span></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td bgcolor="#FFFFFF">&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td bgcolor="#FFFFFF"><input name="Submit" type="submit" class="buscar" value="Consultar" ></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right" bgcolor="#FFFFFF"><input name="CMDExpo" type="button" class="excel2" id="CMDExpo" value="Exportar" onClick="Exportar()"></td>
          </tr>
      </table></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
	<%
	if libro <> "" then

	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 	set RsLibros=objProp.Consultar("BI_LibrosMasConsultados","FO","TO",libro,fechaInicio,fechaFin)
	 
	 objProp.CerrarConexion
	 set objProP=nothing	
	%>
    <tr>
      <td><table width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla_azul">
        <tr>
          <td width="3%" align="center" bgcolor="#006699"><span class="Estilo5">N&ordm;</span></td>
          <td width="9%" align="center" bgcolor="#006699"><span class="Estilo5">C&oacute;digo Dewey </span></td>
          <td width="20%" align="center" bgcolor="#006699"><span class="Estilo5">T&iacute;tulo</span></td>
          <td width="25%" align="center" bgcolor="#006699"><span class="Estilo5">Autor</span></td>
          <td width="9%" align="center" bgcolor="#006699" class="Estilo5">A&ntilde;o</td>
          <td width="10%" align="center" bgcolor="#006699"><span class="Estilo5">Pr&eacute;stamos</span></td>
          <td width="10%" align="center" bgcolor="#006699" class="Estilo5">&nbsp;Originales&nbsp;</td>
          <td width="14%" align="center" bgcolor="#006699" class="Estilo5">&nbsp;Fotocopias&nbsp;</td>
          <td width="14%" align="center" bgcolor="#006699" class="Estilo5">Fecha &Uacute;ltima<br>
            Adquisici&oacute;n</td>
        </tr>
		
		<%
		
		do while not RsLibros.EOF
		i=i+1
		%>
        <tr>
          <td align="center" class="bordeinf"><%=i%></td>
          <td class="bordeinf"><%=RsLibros(1)%></td>
          <td class="bordeinf"><%=RsLibros(2)%></td>
          <td class="bordeinf"><%=RsLibros(3)%></td>
          <td align="center" class="bordeinf"><%=RsLibros("fechapublicacion")%></td>
          <td align="center" class="bordeinf"><%=RsLibros("PRESTAMOS")%></td>
          <td align="center" class="bordeinf"><%=RsLibros("Impreso")%></td>
          <td align="center" class="bordeinf"><%=RsLibros("Fotocopia")%></td>
          <td align="center" class="bordeinf"><%=RsLibros("FECHAINGRESO")%></td>
        </tr>
		<%
		RsLibros.MoveNext
		loop
		%>
      </table></td>

    </tr>
    <tr>
      <td>Libros Consultados:&nbsp; <%Response.Write(RsLibros.RecordCount)%></td>
    </tr>
		  <%end if%>
  </table>
</form>
</body>
</html>
